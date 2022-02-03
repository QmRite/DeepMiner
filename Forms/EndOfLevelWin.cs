using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace DeepMiner
{
    public partial class EndOfLevelWin : Form
    {
        private static WindowsMediaPlayer music;
        public EndOfLevelWin()
        {
            InitVisual();
            InitMusic();

            var nextLevelButton = new Button
            {
                Location = new Point(30, 110),
                Size = new Size(110, 40),
                Text = "Next level",

            };
            nextLevelButton.Click += (sender, args) =>
            {
                MainMenu.game.RestartGame(MainMenu.correntLevel + 1);
                MainMenu.game.Show();
                Close();
                music.controls.stop();

            };
            Controls.Add(nextLevelButton);

            var mainMenuButton = new Button
            {
                Location = new Point(165, 110),
                Size = new Size(110, 40),
                Text = "Main menu"
            };
            mainMenuButton.Click += (sender, args) =>
            {
                music.controls.stop();
                Close();
                MainMenu.game.Hide();
                var mainMenu = new MainMenu();
                mainMenu.Show();
            };
            Controls.Add(mainMenuButton);

            InitializeComponent();
        }

        private void InitVisual()
        {
            Cursor.Show();
            Cursor = MainMenu.cursor;
            BackgroundImage = Image.FromFile("data\\win.png");
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        private static void InitMusic()
        {
            music = new WindowsMediaPlayer();
            music.URL = "data\\music\\winsound.mp3" ;
            music.settings.volume = 10;
            music.controls.play();
        }
        private void EndOfLevelWin_Load(object sender, EventArgs e)
        {

        }
    }
}
