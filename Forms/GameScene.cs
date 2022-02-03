using DeepMiner.Domain;
using DeepMiner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace DeepMiner
{
    public partial class GameScene : Form
    {
        private Domain.Player player;
        private static Timer timer;
        private static Timer countdown;
        private static DateTime correntTime;
        private Level[] levels;
        private static WindowsMediaPlayer music;
        private Map map;
        private static Label scoreLabel;
        private static int score;
        private static Label countdownLabel;
        public static int CamDelta { get; private set; }

        public GameScene()
        {
            InitTimers();
            InitializeComponent();
            InitVisualComponents();
            IniMusic();
        }

        private void IniMusic()
        {
            music = new WMPLib.WindowsMediaPlayer();
            music.URL = "data\\music\\gameMusic.mp3";
            music.settings.volume = 25;
            music.controls.play();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            player.StopMove(e);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            player.Act(e);
        }

        public void InitVisualComponents()
        {
            CamDelta = 0;
            levels = Level.LoadLevels().ToArray();
            map = new Map(levels[MainMenu.correntLevel].map);
            correntTime = levels[MainMenu.correntLevel].time;
            score = levels[MainMenu.correntLevel].score;
            InitWindowSettings();
            InitControls();
            player = new Domain.Player(new Point(Map.GetPixWidth / 2, 14));             
            KeyDown += new KeyEventHandler(OnKeyDown);
            KeyUp += new KeyEventHandler(OnKeyUp);
        }

        public void InitTimers()
        {
            timer = new Timer();
            timer.Interval = 75;
            timer.Tick += new EventHandler(Update);

            countdown = new Timer();
            countdown.Interval = 1000;
            countdown.Tick += new EventHandler(CountDown);
            countdown.Start();
            timer.Start();
        }

        public void InitWindowSettings()
        {
            Cursor.Hide();

            BackgroundImage = Image.FromFile("data\\back.png"); ;
            StartPosition = FormStartPosition.CenterScreen;
            Width = Map.GetPixWidth + 16;
            Height = Map.CellSize * 7 + 32;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            FormClosing += ApplicationClosing;
        }

        public void InitControls()
        {

            countdownLabel = new Label
            {
                Location = new Point(0, 0),
                Size = new Size(85, 20),
                Text = "Time: " + correntTime.ToString().Substring(13),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            Controls.Add(countdownLabel);

            scoreLabel = new Label
            {
                Location = new Point(ClientSize.Width - 80, 0),
                Size = new Size(80, 20),
                Text = "Score: " + score.ToString(),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            Controls.Add(scoreLabel);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            player.PlayAnimation(g);
            map.DrawMap(g);
        }

        private void Update(object sender, EventArgs e)
        {
            if (!PhysicsController.IsCollide(player, new Point(player.dirX, player.dirY)))
            {
                if (PhysicsController.IsFall(player))
                {
                    if (Math.Abs(CamDelta) < Map.GetPixHeight - 8 * Map.CellSize)
                        CamDelta -= 10;
                    PhysicsController.Falling(player);
                }
                else if (player.isMoving)
                    player.Move();
            }
            Invalidate();
        }

        public static void ChangeScore(Blocks block)
        {
            if (scoreLabel.Text.Length > 0)
            {
                var isScoreCorrect = int.TryParse(scoreLabel.Text.Split(' ')[1], out score);
                if (isScoreCorrect)
                {
                    switch (block)
                    {
                        case Blocks.Coal:
                            score -= 5;
                            if (score < 0)
                                score = 0;
                            break;
                        case Blocks.Iron:
                            score -= 10;
                            if (score < 0)
                                score = 0;
                            break;
                        case Blocks.Gold:
                            score -= 15;
                            if (score < 0)
                                score = 0;
                            break;
                        case Blocks.Emerald:
                            score -= 20;
                            if (score < 0)
                                score = 0;
                            break;
                        case Blocks.Diamond:
                            score -= 30;
                            if (score < 0)
                                score = 0;
                            break;
                        default:
                            score -= 0;
                            break;
                    }
                    scoreLabel.Text = "Score: " + score.ToString();
                }
            }
        }

        private void CountDown(object sender, EventArgs e)
        {
            correntTime = correntTime.Subtract(new TimeSpan(0, 0, 1));
            countdownLabel.Text = "Time: " + correntTime.ToString().Substring(13);
            if (correntTime.Ticks == new TimeSpan(00, 00, 00).Ticks || score <= 0)
                FinishGameCheck();
        }

        public static void FinishGameCheck()
        {
            music.controls.stop();
            timer.Stop();
            countdown.Stop();
            if (score > 0)
                new EndOfLevelFail().ShowDialog();                  
            else
                new EndOfLevelWin().ShowDialog();               
        }

        public void RestartGame(int currentLevel)
        {
            music.controls.play();
            map = new Map(levels[currentLevel].map);
            correntTime = levels[currentLevel].time;
            score = levels[currentLevel].score;
            
            Width = Map.GetPixWidth + 16;
            Height = Map.CellSize * 7 + 32;

            Controls.Clear();
            InitControls();

            timer.Start();
            countdown.Start();
            player.pos = new Point(Map.GetPixWidth / 2, 14);
            CamDelta = 0;           
        }

        private void ApplicationClosing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
