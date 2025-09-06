using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Linq;
using WMPLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace pacmanGame
{
    public partial class game : Form
    {
        private WindowsMediaPlayer gamePlayer;
        bool goup, godown, goleft, goright, isGameOver;
        private int userId;
        private int currentUserId;
        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;
        int level;
        int coinValue, cherryValue, strawberryValue;
        int lastLevelChecked = 0; // to detect when level changes

        public game(int userId)
        {
            InitializeComponent();
            this.KeyPreview = true;  // ADD THIS LINE

            this.userId = userId;
            currentUserId = userId;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
            resetGame();
        }

        private void game_Load(object sender, EventArgs e)
        {
            hideGameover();
            PlayGameSound();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goup = true;
            if (e.KeyCode == Keys.Down) godown = true;
            if (e.KeyCode == Keys.Left) goleft = true;
            if (e.KeyCode == Keys.Right) goright = true;
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goup = false;
            if (e.KeyCode == Keys.Down) godown = false;
            if (e.KeyCode == Keys.Left) goleft = false;
            if (e.KeyCode == Keys.Right) goright = false;

            if (e.KeyCode == Keys.Enter && isGameOver)
            {
                hideGameover();
                resetGame();
            }
        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            txtRemarks.Text = ""; // Clear remarks every tick unless game over

            UpdateLevelAndValues();

            Rectangle nextPosition;

            if (goleft)
            {
                nextPosition = new Rectangle(pacman.Left - playerSpeed, pacman.Top, pacman.Width, pacman.Height);
                if (!IsWallCollision(nextPosition))
                {
                    pacman.Left -= playerSpeed;
                    pacman.Image = Properties.Resources.left;
                }
            }
            if (goright)
            {
                nextPosition = new Rectangle(pacman.Left + playerSpeed, pacman.Top, pacman.Width, pacman.Height);
                if (!IsWallCollision(nextPosition))
                {
                    pacman.Left += playerSpeed;
                    pacman.Image = Properties.Resources.right;
                }
            }
            if (godown)
            {
                nextPosition = new Rectangle(pacman.Left, pacman.Top + playerSpeed, pacman.Width, pacman.Height);
                if (!IsWallCollision(nextPosition))
                {
                    pacman.Top += playerSpeed;
                    pacman.Image = Properties.Resources.down;
                }
            }
            if (goup)
            {
                nextPosition = new Rectangle(pacman.Left, pacman.Top - playerSpeed, pacman.Width, pacman.Height);
                if (!IsWallCollision(nextPosition))
                {
                    pacman.Top -= playerSpeed;
                    pacman.Image = Properties.Resources.Up;
                }
            }

            // Screen wrapping (optional)
            if (pacman.Left < -10) pacman.Left = ClientSize.Width - 10;
            if (pacman.Left > ClientSize.Width - 10) pacman.Left = -10;
            if (pacman.Top < -10) pacman.Top = ClientSize.Height - 10;
            if (pacman.Top > ClientSize.Height - 10) pacman.Top = 0;

            // Collect coins and fruits, update score and hide collected items
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible)
                {
                    string tag = (string)x.Tag;

                    if (tag == "coin" && pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        eatSound();
                        score += coinValue;
                        x.Visible = false;
                    }
                    else if (tag == "cherry" && pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        eatSound();
                        score += cherryValue;
                        x.Visible = false;
                    }
                    else if (tag == "strawberry" && pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        eatSound();
                        score += strawberryValue;
                        x.Visible = false;
                    }

                    if (tag == "wall")
                    {
                        // Red and Yellow ghosts reverse on wall collision
                        if (redGhost.Bounds.IntersectsWith(x.Bounds))
                            redGhostSpeed = -redGhostSpeed;
                        if (yellowGhost.Bounds.IntersectsWith(x.Bounds))
                            yellowGhostSpeed = -yellowGhostSpeed;

                        // Pink ghost ignores walls, so no collision handling here
                    }

                    if (tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                            gameOver("You Lose!");
                    }
                }
            }

            // Ghost movements

            // Red Ghost
            Rectangle nextRedPos = new Rectangle(redGhost.Left + redGhostSpeed, redGhost.Top, redGhost.Width, redGhost.Height);
            if (!IsWallCollision(nextRedPos))
            {
                redGhost.Left += redGhostSpeed;
            }
            else
            {
                redGhostSpeed = -redGhostSpeed;
            }

            // Yellow Ghost
            Rectangle nextYellowPos = new Rectangle(yellowGhost.Left - yellowGhostSpeed, yellowGhost.Top, yellowGhost.Width, yellowGhost.Height);
            if (!IsWallCollision(nextYellowPos))
            {
                yellowGhost.Left -= yellowGhostSpeed;
            }
            else
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }

            // Pink Ghost moves freely ignoring walls
            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            // Pink ghost bounces only on screen edges
            if (pinkGhost.Top < 0 || pinkGhost.Bottom > ClientSize.Height)
            {
                pinkGhostY = -pinkGhostY;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Right > ClientSize.Width)
            {
                pinkGhostX = -pinkGhostX;
            }
        }

        private void UpdateLevelAndValues()
        {
            
            // Calculate level dynamically
            int newLevel = (score / 50) + 1;

            if (newLevel != lastLevelChecked)
            {
                level = newLevel;
                txtLevel.Text = "Level: " + level;

                // Update coin values with level multiplier
                coinValue = 1 * level;
                cherryValue = 5 * level;
                strawberryValue = 10 * level;

                // Increase ghost speeds
                int baseSpeed = 5;
                redGhostSpeed = Math.Sign(redGhostSpeed) * (baseSpeed + (level - 1) * 2);
                yellowGhostSpeed = Math.Sign(yellowGhostSpeed) * (baseSpeed + (level - 1) * 2);
                pinkGhostX = Math.Sign(pinkGhostX) * (baseSpeed + (level - 1) * 2);
                pinkGhostY = Math.Sign(pinkGhostY) * (baseSpeed + (level - 1) * 2);

                // Reset all coins and fruits visibility and reassign cherries/strawberries
                ResetCoinsForLevel();

                lastLevelChecked = level;
            }
        }

        private void ResetCoinsForLevel()
        {
            List<PictureBox> coinList = new List<PictureBox>();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    PictureBox pic = (PictureBox)x;
                    if ((string)pic.Tag == "coin" || (string)pic.Tag == "cherry" || (string)pic.Tag == "strawberry")
                    {
                        pic.Visible = true;
                        pic.Image = Properties.Resources.coin;
                        pic.Tag = "coin";
                        coinList.Add(pic);
                    }
                }
            }

            // Randomly assign 2 cherries and 2 strawberries each reset
            Random rnd = new Random();
            var shuffled = coinList.OrderBy(c => rnd.Next()).ToList();

            if (shuffled.Count >= 4)
            {
                for (int i = 0; i < 2; i++)
                {
                    shuffled[i].Image = Properties.Resources.cherry;
                    shuffled[i].Tag = "cherry";
                }
                for (int i = 2; i < 4; i++)
                {
                    shuffled[i].Image = Properties.Resources.strawberry;
                    shuffled[i].Tag = "strawberry";
                }
            }
        }

        private void resetGame()
        {
            score = 0;
            level = 1;
            lastLevelChecked = 0; // force first update
            coinValue = 1;
            cherryValue = 5;
            strawberryValue = 10;
            playerSpeed = 8;

            isGameOver = false;

            pacman.Left = 322;
            pacman.Top = 438;
            redGhost.Left = 235;
            redGhost.Top = 278;
            yellowGhost.Left = 100;
            yellowGhost.Top = 165;
            pinkGhost.Left = 46;
            pinkGhost.Top = 484;

            txtScore.Text = "Score: 0";
            txtLevel.Text = "Level: 1";
            txtRemarks.Text = ""; // Clear remarks on reset

            // Set base ghost speeds
            int baseSpeed = 5;
            redGhostSpeed = baseSpeed;
            yellowGhostSpeed = baseSpeed;
            pinkGhostX = baseSpeed;
            pinkGhostY = baseSpeed;

            ResetCoinsForLevel();

            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtRemarks.Text = message;  // Show game status here
            insertGameStats(userId, level, score);
            showGameover();
        }

        private bool IsWallCollision(Rectangle nextPosition)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "wall")
                {
                    if (nextPosition.IntersectsWith(x.Bounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void hideGameover()
        {
            goBanner.Visible = false;
            goLbl.Visible = false;
            exitBtn.Visible = false;
            restartBtn.Visible = false;
        }
        private void showGameover()
        {
            goSound();
            goBanner.Visible = true;
            goLbl.Visible = true;
            exitBtn.Visible = true;
            restartBtn.Visible = true;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            main main = new main(userId);
            main.Show();

            StopGameSound();
            this.Hide();
        }
        private void insertGameStats(int userId, int level, int score)
        {
            using (OdbcConnection conn = db_con.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO game_stats_tbl (userId, [level], [score]) VALUES (?, ?, ?)";

                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", userId);
                        cmd.Parameters.AddWithValue("?", level);
                        cmd.Parameters.AddWithValue("?", score);

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private async void PlayGameSound()
        {
            if (gamePlayer == null)
            {
                gamePlayer = new WindowsMediaPlayer();  // Initialize if not already
            }

            string soundPath = System.IO.Path.Combine(Application.StartupPath, "game.mp3");

            if (!System.IO.File.Exists(soundPath))
            {
                MessageBox.Show("The audio file 'game.wav' is missing.");
                return;
            }

            gamePlayer.URL = soundPath;
            gamePlayer.settings.volume = 0;  // Start from 0 for fade-in
            gamePlayer.settings.setMode("loop", true);  // Loop the sound
            gamePlayer.controls.play();

            // Fade-in effect
            for (int vol = 0; vol <= 100; vol += 5)
            {
                if (gamePlayer != null && gamePlayer.settings != null)
                {
                    gamePlayer.settings.volume = vol;
                }
                await Task.Delay(20);  // Smooth volume increase over time
            }
        }

        private async void StopGameSound()
        {
            if (gamePlayer != null)
            {
                for (int vol = gamePlayer.settings.volume; vol >= 0; vol -= 5)
                {
                    gamePlayer.settings.volume = vol;
                    await Task.Delay(20);  // Smooth decrease
                }

                gamePlayer.controls.stop();
                gamePlayer = null;
            }
        }

        private void restartBtn_Click(object sender, EventArgs e)
        {
            resetGame();
            hideGameover();
            clickedSound();
        }
        private void clickedSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "click.mp3");
            player.URL = soundPath;
            player.controls.play();
        }
        private void goSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "lost.mp3");
            player.URL = soundPath;
            player.controls.play();
        }
        private void levelupSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "levelup.mp3");
            player.URL = soundPath;
            player.controls.play();
        }
        private void eatSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "eat.mp3");
            player.URL = soundPath;
            player.controls.play();
        }
    }
}
