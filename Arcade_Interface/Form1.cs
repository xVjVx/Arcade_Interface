using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;

namespace Arcade_Interface
{
    public partial class Form1 : Form
    {
        private const int HomeHotKeyId = 0xA11;
        private const int WmHotKey = 0x0312;
        private const uint ModNoRepeat = 0x4000;

        // Global variables
        string rootPath;
        Button btnBack;
        Button selectedButton;
        string currentPath;
        Button lastActivatedButton;
        DateTime lastActivationTime;
        Process runningGameProcess;
        readonly Timer gameShutdownTimer;
        bool homeHotKeyRegistered;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(205, 205, 205);
            flowLayoutPanel1.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            gameShutdownTimer = new Timer();
            gameShutdownTimer.Interval = 1500;
            gameShutdownTimer.Tick += GameShutdownTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 intro = new Form2();
            intro.ShowDialog();

            panelDetalhesJogo.Visible = false;
            panelImagemFundo.Visible = true;

            // Path to "Documentos/Jogos"
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            rootPath = Path.Combine(myDocuments, "Jogos");

            // If the folder doesn't exist
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
                MessageBox.Show("Pasta Jogos criada nos Documentos!");
            }

            // Back button configuration
            btnBack = new ArcadeButton();
            btnBack.Text = "< BACK";
            btnBack.Font = new Font("Press Start 2P", 12, FontStyle.Bold);
            btnBack.Location = new Point(20, 20);
            btnBack.Size = new Size(150, 50);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.BackColor = Color.Transparent;
            btnBack.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBack.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBack.ForeColor = Color.Black;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Visible = false;

            buttonAnimation(btnBack);

            btnBack.Click += (s, ev) => NavigateBack();

            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            LoadGames(rootPath);
        }

        private void buttonAnimation(Button btn)
        {
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, ev) => {
                if (btn == btnBack)
                {
                    UpdateButtonStyle(btn, true);
                    return;
                }

                SetSelectedButton(btn);
            };

            btn.MouseLeave += (s, ev) => {
                if (btn == btnBack)
                {
                    UpdateButtonStyle(btn, false);
                    return;
                }

                UpdateButtonStyle(btn, btn == selectedButton);
            };
        }

        private void LoadGames(string path)
        {
            currentPath = path;
            btnBack.Tag = path;
            lastActivatedButton = null;

            if (path == rootPath)
            {
                btnBack.Visible = false;
            }
            else
            {
                btnBack.Visible = true;
            }

            flowLayoutPanel1.Controls.Clear();

            if (!Directory.Exists(path)) return;

            bool isRoot = (path == rootPath);

            string[] itemsFound;

            if (isRoot)
            {
                itemsFound = Directory.GetDirectories(path);
            }
            else
            {
                string[] jogosExe = Directory.GetFiles(path, "*.exe");
                string[] jogosAtalhos = Directory.GetFiles(path, "*.lnk");
                string[] jogosPy = Directory.GetFiles(path, "*.py");

                itemsFound = jogosExe.Concat(jogosAtalhos).Concat(jogosPy).ToArray();
            }

            foreach (string item in itemsFound)
            {
                Button btn = new ArcadeButton();

                string itemName = isRoot ? Path.GetFileName(item) : Path.GetFileNameWithoutExtension(item);
                itemName = itemName.ToUpper();

                btn.Width = 700;
                btn.Height = 100;

                float fontSize = 32f;
                Font currentFont = new Font("Press Start 2P", fontSize, FontStyle.Bold);
                TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
                int availableWidth = btn.Width - 60;

                while (TextRenderer.MeasureText(itemName, currentFont, new Size(int.MaxValue, int.MaxValue), flags).Width > availableWidth)
                {
                    fontSize -= 2.0f;
                    if (fontSize < 8) break;
                    currentFont = new Font("Press Start 2P", fontSize, FontStyle.Bold);
                }

                btn.Font = currentFont;
                btn.Text = itemName;

                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Margin = new Padding(0, 0, 0, 30);
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.Transparent;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.ForeColor = Color.Black;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.Tag = item;

                buttonAnimation(btn);

                if (isRoot)
                {
                    btn.Click += (s, ev) => {
                        SetSelectedButton(btn);
                        OpenMenu(item);
                    };
                }
                else
                {
                    btn.MouseDown += (s, ev) =>
                    {
                        if (ev.Button == MouseButtons.Left)
                        {
                            SetSelectedButton(btn);
                            if (ev.Clicks == 1)
                            {
                                ShowGameDetails(item);
                            }
                            else if (ev.Clicks == 2)
                            {
                                LaunchGame(item);
                            }
                        }
                    };
                }
                flowLayoutPanel1.Controls.Add(btn);
            }

            Label spacer = new Label();
            spacer.Width = 1;
            spacer.Height = 50;
            spacer.BackColor = Color.Transparent;
            spacer.Margin = new Padding(0);

            flowLayoutPanel1.Controls.Add(spacer);

            SelectFirstMenuItem();
        }

        private void SelectFirstMenuItem()
        {
            Button firstButton = GetMenuButtons().FirstOrDefault();
            if (firstButton != null)
            {
                SetSelectedButton(firstButton);
            }
            else
            {
                selectedButton = null;
            }
        }

        private Button[] GetMenuButtons()
        {
            return flowLayoutPanel1.Controls.OfType<Button>().ToArray();
        }

        private void SetSelectedButton(Button button)
        {
            if (button == null)
            {
                return;
            }

            selectedButton = button;

            foreach (Button menuButton in GetMenuButtons())
            {
                UpdateButtonStyle(menuButton, menuButton == selectedButton);
            }
        }

        private void UpdateButtonStyle(Button button, bool isSelected)
        {
            button.ForeColor = isSelected
                ? ColorTranslator.FromHtml("#2395DB")
                : Color.Black;
            button.Padding = isSelected
                ? new Padding(30, 0, 0, 0)
                : new Padding(10, 0, 0, 0);
        }

        private void MoveSelection(int direction)
        {
            Button[] menuButtons = GetMenuButtons();
            if (menuButtons.Length == 0)
            {
                return;
            }

            int currentIndex = Array.IndexOf(menuButtons, selectedButton);
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = Math.Max(0, Math.Min(menuButtons.Length - 1, currentIndex + direction));
            }

            SetSelectedButton(menuButtons[currentIndex]);
            flowLayoutPanel1.ScrollControlIntoView(menuButtons[currentIndex]);
            lastActivatedButton = null;
        }

        private void ActivateSelectedItem()
        {
            if (selectedButton == null || selectedButton.Tag == null)
            {
                return;
            }

            string selectedPath = selectedButton.Tag.ToString();
            if (Directory.Exists(selectedPath))
            {
                OpenMenu(selectedPath);
                return;
            }

            DateTime now = DateTime.UtcNow;
            if (lastActivatedButton == selectedButton &&
                (now - lastActivationTime).TotalMilliseconds <= SystemInformation.DoubleClickTime)
            {
                lastActivatedButton = null;
                LaunchGame(selectedPath);
                return;
            }

            ShowGameDetails(selectedPath);
            lastActivatedButton = selectedButton;
            lastActivationTime = now;
        }

        private void OpenMenu(string path)
        {
            panelDetalhesJogo.Visible = false;
            panelImagemFundo.Visible = true;
            LoadGames(path);
        }

        private void NavigateBack()
        {
            if (String.IsNullOrEmpty(currentPath) || currentPath == rootPath)
            {
                return;
            }

            DirectoryInfo parent = Directory.GetParent(currentPath);
            if (parent != null)
            {
                OpenMenu(parent.FullName);
            }
        }

        private void LaunchGame(string gamePath)
        {
            try
            {
                runningGameProcess = Process.Start(gamePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o jogo: " + ex.Message);
            }
        }

        private void ResetArcade()
        {
            if (String.IsNullOrEmpty(rootPath))
            {
                return;
            }

            CloseRunningGame();
            OpenMenu(rootPath);
            Show();
            Activate();
        }

        private void CloseRunningGame()
        {
            if (runningGameProcess == null)
            {
                return;
            }

            try
            {
                if (runningGameProcess.HasExited)
                {
                    ClearRunningGame();
                    return;
                }

                if (runningGameProcess.CloseMainWindow())
                {
                    gameShutdownTimer.Start();
                }
                else
                {
                    runningGameProcess.Kill();
                    ClearRunningGame();
                }
            }
            catch
            {
                ClearRunningGame();
            }
        }

        private void GameShutdownTimer_Tick(object sender, EventArgs e)
        {
            if (runningGameProcess == null)
            {
                gameShutdownTimer.Stop();
                return;
            }

            try
            {
                if (!runningGameProcess.HasExited)
                {
                    runningGameProcess.Kill();
                }
            }
            catch
            {
                // The game process may already have exited between the checks.
            }

            ClearRunningGame();
        }

        private void ClearRunningGame()
        {
            gameShutdownTimer.Stop();

            if (runningGameProcess != null)
            {
                runningGameProcess.Dispose();
                runningGameProcess = null;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up:
                    MoveSelection(-1);
                    return true;
                case Keys.Down:
                    MoveSelection(1);
                    return true;
                case Keys.Left:
                    NavigateBack();
                    return true;
                case Keys.Right:
                case Keys.Enter:
                    ActivateSelectedItem();
                    return true;
                case Keys.Home:
                    ResetArcade();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            homeHotKeyRegistered = RegisterHotKey(Handle, HomeHotKeyId, ModNoRepeat, (uint)Keys.Home);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (homeHotKeyRegistered)
            {
                UnregisterHotKey(Handle, HomeHotKeyId);
                homeHotKeyRegistered = false;
            }

            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmHotKey && m.WParam.ToInt32() == HomeHotKeyId)
            {
                ResetArcade();
                return;
            }

            base.WndProc(ref m);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            gameShutdownTimer.Stop();
            gameShutdownTimer.Dispose();
            base.OnFormClosed(e);
        }

        private void ShowGameDetails(string pathExe)
        {
            panelImagemFundo.Visible = false;
            panelDetalhesJogo.Visible = true;

            string pathPng = Path.ChangeExtension(pathExe, ".png");

            try
            {
                if (picIconeJogo.Image != null)
                {
                    picIconeJogo.Image.Dispose();
                    picIconeJogo.Image = null;
                }

                if (File.Exists(pathPng))
                {
                    picIconeJogo.Image = Image.FromFile(pathPng);
                }
                else
                {
                    Icon icone = Icon.ExtractAssociatedIcon(pathExe);
                    if (icone != null)
                    {
                        picIconeJogo.Image = icone.ToBitmap();
                    }
                }
            }
            catch
            {
                picIconeJogo.Image = null;
            }

            string pathTxt = Path.ChangeExtension(pathExe, ".txt");

            if (File.Exists(pathTxt))
            {
                string[] lines = File.ReadAllLines(pathTxt);

                if (lines.Length > 0)
                {
                    lblTagsJogo.Text = "Tags: " + lines[0];
                }

                if (lines.Length > 1)
                {
                    string description = "";
                    for (int i = 1; i < lines.Length; i++)
                    {
                        description += lines[i] + Environment.NewLine;
                    }
                    lblDescricaoJogo.Text = description;
                }
                else
                {
                    lblDescricaoJogo.Text = "Sem descrição disponível.";
                }
            }
            else
            {
                lblTagsJogo.Text = "Tags: N/A";
                lblDescricaoJogo.Text = "Ficheiro de informações não encontrado para este jogo.";
            }
        }
    }

    public class ArcadeButton : Button
    {
        private int borderSize = 3;
        private int borderRadius = 20;
        private Color borderColor = Color.Black;

        public ArcadeButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 2, this.Height - 2);

            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                this.Region = new Region(pathSurface);

                pevent.Graphics.DrawPath(penBorder, pathBorder);
            }
        }
    }

    public class HighQualityPictureBox : PictureBox
    {
        public HighQualityPictureBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (Image == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
            {
                return;
            }

            float scale = Math.Min(
                (float)ClientSize.Width / Image.Width,
                (float)ClientSize.Height / Image.Height);
            int width = Math.Max(1, (int)Math.Round(Image.Width * scale));
            int height = Math.Max(1, (int)Math.Round(Image.Height * scale));
            Rectangle destination = new Rectangle(
                (ClientSize.Width - width) / 2,
                (ClientSize.Height - height) / 2,
                width,
                height);

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.DrawImage(Image, destination, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
        }
    }}
