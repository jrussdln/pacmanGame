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
    public partial class main : Form
    {
        private int userId;
        private int currentUserId;
        private WindowsMediaPlayer gamePlayer;
        public main(int userId)
        {

            
            InitializeComponent();
            this.userId = userId;
            currentUserId = userId;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void main_Load(object sender, EventArgs e)
        {
            hideRanks();
            hideHistory();
            hideSettings();
            PlayGameSound();
        }
        private void showUi()
        {
            pacmanLogo.Visible = true;
            playBtn.Visible = true;
            rankBtn.Visible = true;
            historyBtn.Visible = true;
            settingsBtn.Visible = true;
        }
        private void hideUi()
        {
            pacmanLogo.Visible = false;
            playBtn.Visible = false;
            rankBtn.Visible = false;
            historyBtn.Visible = false;
            settingsBtn.Visible = false;
        }
        private void showRanks()
        {
            ranksBanner.Visible = true;
            ranksXBtn.Visible = true;
            ranksTbl.Visible = true;
           
        }
        private void hideRanks()
        {
            ranksBanner.Visible = false;
            ranksXBtn.Visible = false;
            ranksTbl.Visible = false;

        }
        private void showHistory()
        {
            historyBanner.Visible = true;
            historyXBtn.Visible = true;
            historyTbl.Visible = true;

        }
        private void hideHistory()
        {
            historyBanner.Visible = false;
            historyXBtn.Visible = false;
            historyTbl.Visible = false;

        }
        private void showSettings()
        {
            settingsBanner.Visible = true;
            settingsXBtn.Visible = true;
            usernameField.Visible = true;
            passwordField.Visible = true;
            deleteAccountBtn.Visible = true;
            logoutBtn.Visible = true;
            updateBtn.Visible = true;
            passwordLbl.Visible = true;
            usernameLbl.Visible = true;

        }
        private void hideSettings()
        {
            settingsBanner.Visible = false;
            settingsXBtn.Visible = false;
            usernameField.Visible = false;
            passwordField.Visible = false;
            deleteAccountBtn.Visible = false;
            logoutBtn.Visible = false;
            updateBtn.Visible = false;
            passwordLbl.Visible = false;
            usernameLbl.Visible = false;

        }
        private void clickedSound()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string soundPath = System.IO.Path.Combine(Application.StartupPath, "click.mp3");
            player.URL = soundPath;
            player.controls.play();
        }
        private void LoadRank()
        {

            try
            {
                ranksTbl.View = View.Details;  // Show as table

                // Add columns if not exist yet+iop[]\jkl[rqw 
                if (ranksTbl.Columns.Count == 0)
                {
                    ranksTbl.Columns.Add("#", 80, HorizontalAlignment.Center);
                    ranksTbl.Columns.Add("Username", 160, HorizontalAlignment.Center);
                    ranksTbl.Columns.Add("High Score", 160, HorizontalAlignment.Center);
                }

                ranksTbl.Items.Clear();

                using (OdbcConnection connection = db_con.GetConnection())
                {
                    connection.Open();

                    string query = @"
            SELECT 
                user_tbl.userId,
                MAX(game_stats_tbl.score) AS Highscore,
                user_tbl.username
            FROM 
                game_stats_tbl
            INNER JOIN 
                user_tbl ON game_stats_tbl.userId = user_tbl.userId
            GROUP BY 
                user_tbl.userId, user_tbl.username
            ";

                    using (OdbcCommand cmd = new OdbcCommand(query, connection))
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        int counter = 1;

                        while (reader.Read())
                        {
                            try
                            {
                                string score = reader["Highscore"].ToString();
                                string username = reader["username"].ToString();

                                ListViewItem item = new ListViewItem(counter.ToString());
                                item.SubItems.Add(username);
                                item.SubItems.Add(score);

                                ranksTbl.Items.Add(item);
                                counter++;
                            }
                            catch (Exception innerEx)
                            {
                                MessageBox.Show("Error reading row: " + innerEx.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading game stats: " + ex.Message + "\n" + ex.StackTrace);
            }

        }
        private void LoadHistory(int userId)
        {
            try
            {
                // Start updating the ListView to prevent flicker
                historyTbl.BeginUpdate();

                // Clear any previous content
                historyTbl.Clear();

                // Setup ListView properties
                historyTbl.View = View.Details;
                historyTbl.FullRowSelect = true;
                historyTbl.GridLines = true;

                // Define columns
                historyTbl.Columns.Add("#", 35, HorizontalAlignment.Center);
                historyTbl.Columns.Add("Level", 50, HorizontalAlignment.Center);
                historyTbl.Columns.Add("Score", 110, HorizontalAlignment.Center);
                historyTbl.Columns.Add("Date Played", 140, HorizontalAlignment.Center);

                // Fetch data
                using (OdbcConnection connection = db_con.GetConnection())
                {   
                    connection.Open();

                    string query = @"
                        SELECT [level], [score], [created_at]
                        FROM game_stats_tbl
                        WHERE [userId] = ?
                    ";


                    using (OdbcCommand cmd = new OdbcCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("?", userId); // ✅ Correct for ODBC


                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            int rowCount = 0;

                            while (reader.Read())
                            {
                                rowCount++;

                                string level = reader["level"].ToString();
                                string score = reader["score"].ToString();
                                string datePlayed;

                                if (!reader.IsDBNull(reader.GetOrdinal("created_at")))
                                {
                                    try
                                    {
                                        DateTime dt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                                        datePlayed = dt.ToString("yyyy-MM-dd hh:mm tt");
                                    }
                                    catch
                                    {
                                        datePlayed = reader["created_at"].ToString(); // fallback to raw string
                                    }
                                }
                                else
                                {
                                    datePlayed = "N/A";
                                }

                                ListViewItem item = new ListViewItem(rowCount.ToString());
                                item.SubItems.Add(level);
                                item.SubItems.Add(score);
                                item.SubItems.Add(datePlayed);

                                historyTbl.Items.Add(item);
                            }
                        }
                    }
                }

                // Ensure the table is visible
                historyTbl.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading game history: " + ex.Message, "Load History", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure UI is updated no matter what
                historyTbl.EndUpdate();
            }
        }

        private void LoadSettings(int userId)
        {
            try
            {
                using (OdbcConnection connection = db_con.GetConnection())
                {
                    connection.Open();

                    string query = @"
                SELECT username, [password] 
                FROM user_tbl 
                WHERE userId = ?
            ";

                    using (OdbcCommand cmd = new OdbcCommand(query, connection))
                    {
                        cmd.Parameters.Add("?", OdbcType.Int).Value = userId;

                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usernameField.Text = reader["username"] != DBNull.Value ? reader["username"].ToString() : "";
                                passwordField.Text = reader["password"] != DBNull.Value ? reader["password"].ToString() : "";
                            }
                            else
                            {
                                MessageBox.Show("User not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user settings: " + ex.Message);
            }
        }
        private void playBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            game game = new game(userId);
            game.Show();
            StopGameSound();
            this.Hide();
        }

        private void rankBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            hideUi();
            showRanks();
            LoadRank();
        }

        private void historyBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            hideUi();
            showHistory();
            LoadHistory(userId);
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            hideUi();
            showSettings();
            LoadSettings(userId);
        }

        private void ranksXBtn_Click(object sender, EventArgs e)
        {
            showUi();
            hideRanks();
            clickedSound();

        }

        private void historyXBtn_Click(object sender, EventArgs e)
        {
            showUi();
            hideHistory();
            clickedSound();
        }

        private void settingsXBtn_Click(object sender, EventArgs e)
        {
            showUi();
            hideSettings();
            clickedSound();
        }

        private void deleteAccountBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this account? This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (OdbcConnection connection = db_con.GetConnection())
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM user_tbl WHERE [userId] = ?";

                        using (OdbcCommand cmd = new OdbcCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.Add("?", OdbcType.Int).Value = currentUserId;

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Account deleted successfully.");
                                Form1 Form1 = new Form1();  // Pass userId here
                                Form1.Show();

                                this.Hide();
                                StopGameSound();
                            }
                            else
                            {
                                MessageBox.Show("Delete failed: User not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting account: " + ex.Message);
                }
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            Form1 Form1 = new Form1();
            this.Hide();
            StopGameSound();
            Form1.Show();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            clickedSound();
            try
            {
                using (OdbcConnection connection = db_con.GetConnection())
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE user_tbl
                SET [username] = ?, [password] = ?
                WHERE [userId] = ?
            ";

                    using (OdbcCommand cmd = new OdbcCommand(updateQuery, connection))
                    {
                        cmd.Parameters.Add("?", OdbcType.VarChar).Value = usernameField.Text;
                        cmd.Parameters.Add("?", OdbcType.VarChar).Value = passwordField.Text;
                        cmd.Parameters.Add("?", OdbcType.Int).Value = currentUserId;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User information updated successfully.");

                            hideSettings();
                            showUi();
                        }
                        else
                        {
                            MessageBox.Show("Update failed: User not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message);
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
