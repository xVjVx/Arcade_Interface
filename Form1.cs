using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


/*
    *****TODO*****
*/


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

            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            btnBack = new Button();
            btnBack.Text = "< ESC";
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

            PopUpAnimation(btnBack);

            btnBack.Click += (s, ev) => {
                string currentPath = (string)btnBack.Tag;
                if (currentPath != null && currentPath != rootPath)
                {
                    string parentFolder = Directory.GetParent(currentPath).FullName;
                    LoadGames(parentFolder);
                }
            };

            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            LoadGames(rootPath);
        }

        private void PopUpAnimation(Button btn)
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

            string[] foundFolders = Directory.GetDirectories(path);

            foreach (string folder in foundFolders)
            {
                Button btn = new Button();

                string folderName = Path.GetFileName(folder).ToUpper();

                btn.Width = flowLayoutPanel1.Width - 40;
                btn.Height = 100;

                float fontSize = 32f;

                Font currentFont = new Font("Press Start 2P", fontSize, FontStyle.Bold);

                TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;

                int availableWidth = btn.Width - 60;

                Size textSize = TextRenderer.MeasureText(folderName, currentFont);

                while (TextRenderer.MeasureText(folderName, currentFont, new Size(int.MaxValue, int.MaxValue), flags).Width > availableWidth)
                {
                    fontSize -= 2.0f;
                    if (fontSize < 8) break;
                    currentFont = new Font("Press Start 2P", fontSize, FontStyle.Bold);
                }

                btn.Font = currentFont;
                btn.Text = folderName;

                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Margin = new Padding(0, 0, 0, 30);
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.Transparent;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.ForeColor = Color.Black;
                btn.Padding = new Padding(10, 0, 0, 0);

                PopUpAnimation(btn);

                btn.Click += (s, ev) => {
                    LoadGames(folder);
                };

                flowLayoutPanel1.Controls.Add(btn);
            }

            Label spacer = new Label();
            spacer.Width = 1;
            spacer.Height = 50;
            spacer.BackColor = Color.Transparent;
            spacer.Margin = new Padding(0);

            flowLayoutPanel1.Controls.Add(spacer);
        }
    }
}