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
    public partial class MainMenu : Form
    {
        private Button exit;
        private Button start;

        public static GameScene game;
        public static int correntLevel;
        public static Cursor cursor;

        private static WindowsMediaPlayer music;
        public MainMenu()
        {
            InitViusal();
            InitMusic();

            start = new Button
            {
                Location = new Point(40, 250),
                Size = new Size(200, 50),
                Text = "Start!"
            };
            Controls.Add(start);
            start.Click += (sender, args) =>
            {
                CreateSelectLevelMenu();
            };

            exit = new Button
            {
                Location = new Point(40, 300),
                Size = new Size(200, 35),
                Text = "Exit"
            };
            Controls.Add(exit);
            exit.Click += (sender, args) =>
            {
                Application.Exit();
            };

            InitializeComponent();
        }

        private void CreateSelectLevelMenu()
        {
            BackgroundImage = Image.FromFile("data\\selectLevel.png"); ;
            Controls.Clear();
            CreateSelectLevelButtion();
        }

        private void CreateSelectLevelButtion()
        {
            var levelButtons = new Button[4];

            for (var i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i] = new Button
                {
                    Location = new Point(40, 150 + 47 * i),
                    Size = new Size(200, 45),                    
                    Name = i.ToString()
                };
                levelButtons[i].Text = i != levelButtons.Length - 1 ? 
                    "Level " + (i + 1).ToString() : "Generate random level";

                levelButtons[i].Click += (sender, args) =>
                {
                    var button = sender as Button;
                    correntLevel = int.Parse(button.Name);
                    if (game != null)
                    {
                        game.RestartGame(correntLevel);
                        game.Show();
                    }
                    else
                    {
                        game = new GameScene();
                        game.Show();
                    }
                    music.controls.stop();
                    Close();
                };
                Controls.Add(levelButtons[i]);
            }
        }
        private static void InitMusic()
        {
            music = new WindowsMediaPlayer();
            music.URL = "data\\music\\MainMenuMusic.mp3";
            music.settings.volume = 10;
            music.controls.play();
        }
        private void InitViusal()
        {
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            BackgroundImage = Image.FromFile("data\\logo.png");
            base.Cursor = new Cursor("data\\cursor\\cursor.cur");
            StartPosition = FormStartPosition.CenterScreen;
            cursor = Cursor;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
 }

