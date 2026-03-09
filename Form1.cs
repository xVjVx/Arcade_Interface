using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace Arcade_Interface
{
    public partial class Form1 : Form
    {
        // Global variables
        string rootPath;
        Button btnBack;

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(205, 205, 205);
            flowLayoutPanel1.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
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

            btnBack.Click += (s, ev) => {
                string currentPath = (string)btnBack.Tag;
                if (currentPath != null && currentPath != rootPath)
                { 
                    panelDetalhesJogo.Visible = false;
                    panelImagemFundo.Visible = true;

                    string parentFolder = Directory.GetParent(currentPath).FullName;
                    LoadGames(parentFolder);
                }
            };

            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            LoadGames(rootPath);
        }

        private void buttonAnimation(Button btn)
        {
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, ev) => {
                btn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2395DB");
                btn.Padding = new Padding(30, 0, 0, 0);
            };

            btn.MouseLeave += (s, ev) => {
                btn.ForeColor = Color.Black;
                btn.Padding = new Padding(10, 0, 0, 0);
            };
        }

        private void LoadGames(string path)
        {
            btnBack.Tag = path;

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

                buttonAnimation(btn);

                if (isRoot)
                {
                    btn.Click += (s, ev) => {
                        LoadGames(item);
                    };
                }
                else
                {
                    btn.MouseDown += (s, ev) =>
                    {
                        if (ev.Button == MouseButtons.Left)
                        {
                            if (ev.Clicks == 1)
                            {
                                ShowGameDetails(item);
                            }
                            else if (ev.Clicks == 2)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(item);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Erro ao abrir o jogo: " + ex.Message);
                                }
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
}