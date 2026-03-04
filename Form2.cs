using AxWMPLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace Arcade_Interface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.enableContextMenu = false;

            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string videoPath = Path.Combine(myDocuments, "Jogos/intro.mp4");

            axWindowsMediaPlayer1.URL = videoPath;

            axWindowsMediaPlayer1.KeyDownEvent += (s, e) => { CloseIntro(); };

            axWindowsMediaPlayer1.MouseDownEvent += (s, e) => { CloseIntro(); };

            axWindowsMediaPlayer1.PlayStateChange += (s, e) => {
                if (e.newState == 8)
                {
                    CloseIntro();
                }
            };
        }

        private void CloseIntro()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.Close();
        }
    }
}