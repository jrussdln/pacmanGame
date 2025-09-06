using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Linq;
using WMPLib;
using System.Threading.Tasks;

namespace pacmanGame
{
    public partial class Form1 : Form
    {
        private WindowsMediaPlayer gamePlayer;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hideLogin();
            hideRegister();
            PlayGameSound();
        }

        private void showLogin()
        {
            loginBanner.Visible = true;
            usernameField.Visible = true;
            passwordField.Visible = true;
            usernameLbl.Visible = true;
            passwordLbl.Visible = true;
            backBtn.Visible = true;
            loginBtn1.Visible = true;
        }
        private void hideLogin()
        {
            loginBanner.Visible = false;
            usernameField.Visible = false;
            passwordField.Visible = false;
            usernameLbl.Visible = false;
            passwordLbl.Visible = false;
            backBtn.Visible = false;
            loginBtn1.Visible = false;
        }
        private void showRegister()
        {
            registerBanner.Visible = true;
            usernameField1.Visible = true;
            passwordField1.Visible = true;
            usernameLbl1.Visible = true;
            passwordLbl1.Visible = true;
            backBtn1.Visible = true;
            registerBtn1.Visible = true;
        }
        private void hideRegister()
        {
            registerBanner.Visible = false;
            usernameField1.Visible = false;
            passwordField1.Visible = false;
            usernameLbl1.Visible = false;
            passwordLbl1.Visible = false;
            backBtn1.Visible = false;
            registerBtn1.Visible = false;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            StopGameSound();
            Application.Exit();
        }
        private void loginBtn1_Click(object sender, EventArgs e)
        {
            clickedSound();
            string username = usernameField.Text.Trim();
            string password = passwordField.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            using (OdbcConnection connection = db_con.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT userId FROM user_tbl WHERE username = ? AND password = ?";
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);
                        command.Parameters.AddWithValue("?", password);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            
                            MessageBox.Show("Login successful!");
                            main main = new main(userId);
                            main.Show();
                            this.Hide();
                            StopGameSound();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during login: " + ex.Message);
                }
            }
        }
        private void registerBtn_Click(object sender, EventArgs e)
        {
            showRegister();
            clickedSound();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            hideLogin();
            clickedSound();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            showLogin();
            clickedSound();
        }
        private void clickedSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "click.mp3");
            player.URL = soundPath;
            player.controls.play();
        }

        private void backBtn1_Click(object sender, EventArgs e)
        {
            hideRegister();
            clickedSound();
        }

        private void registerBtn1_Click(object sender, EventArgs e)
        {
            clickedSound();
            var fields = new[] {
                usernameField1.Text.Trim(), passwordField1.Text.Trim()
            };

            if (fields.Any(f => string.IsNullOrEmpty(f)))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            using (OdbcConnection conn = db_con.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO user_tbl (username, [password]) VALUES (?, ?)";

                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        foreach (var field in fields)
                            cmd.Parameters.AddWithValue("?", field);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Signup success!");
                            showLogin();
                            // Ensure safe UI update
                            this.Invoke((MethodInvoker)delegate
                            {
                                showLogin();
                                hideRegister();
                            });
                        }
                        else
                        {
                            MessageBox.Show("Signup failed.");
                        }
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
    }
}
