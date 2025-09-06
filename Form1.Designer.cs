namespace pacmanGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pacmanLogo = new System.Windows.Forms.PictureBox();
            this.loginBtn = new System.Windows.Forms.PictureBox();
            this.registerBtn = new System.Windows.Forms.PictureBox();
            this.closeBtn = new System.Windows.Forms.PictureBox();
            this.loginBanner = new System.Windows.Forms.PictureBox();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.usernameField = new System.Windows.Forms.TextBox();
            this.passwordField = new System.Windows.Forms.TextBox();
            this.backBtn = new System.Windows.Forms.Button();
            this.loginBtn1 = new System.Windows.Forms.Button();
            this.registerBanner = new System.Windows.Forms.PictureBox();
            this.usernameLbl1 = new System.Windows.Forms.Label();
            this.passwordLbl1 = new System.Windows.Forms.Label();
            this.usernameField1 = new System.Windows.Forms.TextBox();
            this.passwordField1 = new System.Windows.Forms.TextBox();
            this.backBtn1 = new System.Windows.Forms.Button();
            this.registerBtn1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pacmanLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registerBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registerBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pacmanLogo
            // 
            this.pacmanLogo.BackColor = System.Drawing.Color.Transparent;
            this.pacmanLogo.Image = ((System.Drawing.Image)(resources.GetObject("pacmanLogo.Image")));
            this.pacmanLogo.Location = new System.Drawing.Point(123, 60);
            this.pacmanLogo.Name = "pacmanLogo";
            this.pacmanLogo.Size = new System.Drawing.Size(389, 145);
            this.pacmanLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pacmanLogo.TabIndex = 1;
            this.pacmanLogo.TabStop = false;
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn.Image = ((System.Drawing.Image)(resources.GetObject("loginBtn.Image")));
            this.loginBtn.Location = new System.Drawing.Point(97, 236);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(232, 48);
            this.loginBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loginBtn.TabIndex = 2;
            this.loginBtn.TabStop = false;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // registerBtn
            // 
            this.registerBtn.BackColor = System.Drawing.Color.Transparent;
            this.registerBtn.Image = ((System.Drawing.Image)(resources.GetObject("registerBtn.Image")));
            this.registerBtn.Location = new System.Drawing.Point(335, 236);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(232, 48);
            this.registerBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.registerBtn.TabIndex = 3;
            this.registerBtn.TabStop = false;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
            this.closeBtn.Location = new System.Drawing.Point(216, 295);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(232, 48);
            this.closeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeBtn.TabIndex = 4;
            this.closeBtn.TabStop = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // loginBanner
            // 
            this.loginBanner.BackColor = System.Drawing.Color.Transparent;
            this.loginBanner.Image = ((System.Drawing.Image)(resources.GetObject("loginBanner.Image")));
            this.loginBanner.Location = new System.Drawing.Point(88, 60);
            this.loginBanner.Name = "loginBanner";
            this.loginBanner.Size = new System.Drawing.Size(479, 295);
            this.loginBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loginBanner.TabIndex = 5;
            this.loginBanner.TabStop = false;
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.BackColor = System.Drawing.Color.Gold;
            this.usernameLbl.Location = new System.Drawing.Point(192, 188);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(68, 13);
            this.usernameLbl.TabIndex = 19;
            this.usernameLbl.Text = "USERNAME";
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.BackColor = System.Drawing.Color.Gold;
            this.passwordLbl.Location = new System.Drawing.Point(192, 216);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(70, 13);
            this.passwordLbl.TabIndex = 20;
            this.passwordLbl.Text = "PASSWORD";
            // 
            // usernameField
            // 
            this.usernameField.Location = new System.Drawing.Point(286, 185);
            this.usernameField.Name = "usernameField";
            this.usernameField.Size = new System.Drawing.Size(162, 20);
            this.usernameField.TabIndex = 21;
            // 
            // passwordField
            // 
            this.passwordField.Location = new System.Drawing.Point(286, 212);
            this.passwordField.Name = "passwordField";
            this.passwordField.Size = new System.Drawing.Size(162, 20);
            this.passwordField.TabIndex = 22;
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(202, 266);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(117, 23);
            this.backBtn.TabIndex = 23;
            this.backBtn.Text = "BACK";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // loginBtn1
            // 
            this.loginBtn1.Location = new System.Drawing.Point(325, 266);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(117, 23);
            this.loginBtn1.TabIndex = 24;
            this.loginBtn1.Text = "LOGIN";
            this.loginBtn1.UseVisualStyleBackColor = true;
            this.loginBtn1.Click += new System.EventHandler(this.loginBtn1_Click);
            // 
            // registerBanner
            // 
            this.registerBanner.BackColor = System.Drawing.Color.Transparent;
            this.registerBanner.Image = ((System.Drawing.Image)(resources.GetObject("registerBanner.Image")));
            this.registerBanner.Location = new System.Drawing.Point(88, 60);
            this.registerBanner.Name = "registerBanner";
            this.registerBanner.Size = new System.Drawing.Size(479, 295);
            this.registerBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.registerBanner.TabIndex = 25;
            this.registerBanner.TabStop = false;
            // 
            // usernameLbl1
            // 
            this.usernameLbl1.AutoSize = true;
            this.usernameLbl1.BackColor = System.Drawing.Color.Gold;
            this.usernameLbl1.Location = new System.Drawing.Point(192, 188);
            this.usernameLbl1.Name = "usernameLbl1";
            this.usernameLbl1.Size = new System.Drawing.Size(68, 13);
            this.usernameLbl1.TabIndex = 26;
            this.usernameLbl1.Text = "USERNAME";
            // 
            // passwordLbl1
            // 
            this.passwordLbl1.AutoSize = true;
            this.passwordLbl1.BackColor = System.Drawing.Color.Gold;
            this.passwordLbl1.Location = new System.Drawing.Point(192, 216);
            this.passwordLbl1.Name = "passwordLbl1";
            this.passwordLbl1.Size = new System.Drawing.Size(70, 13);
            this.passwordLbl1.TabIndex = 27;
            this.passwordLbl1.Text = "PASSWORD";
            // 
            // usernameField1
            // 
            this.usernameField1.Location = new System.Drawing.Point(286, 185);
            this.usernameField1.Name = "usernameField1";
            this.usernameField1.Size = new System.Drawing.Size(162, 20);
            this.usernameField1.TabIndex = 28;
            // 
            // passwordField1
            // 
            this.passwordField1.Location = new System.Drawing.Point(286, 212);
            this.passwordField1.Name = "passwordField1";
            this.passwordField1.Size = new System.Drawing.Size(162, 20);
            this.passwordField1.TabIndex = 29;
            // 
            // backBtn1
            // 
            this.backBtn1.Location = new System.Drawing.Point(202, 266);
            this.backBtn1.Name = "backBtn1";
            this.backBtn1.Size = new System.Drawing.Size(117, 23);
            this.backBtn1.TabIndex = 30;
            this.backBtn1.Text = "BACK";
            this.backBtn1.UseVisualStyleBackColor = true;
            this.backBtn1.Click += new System.EventHandler(this.backBtn1_Click);
            // 
            // registerBtn1
            // 
            this.registerBtn1.Location = new System.Drawing.Point(325, 266);
            this.registerBtn1.Name = "registerBtn1";
            this.registerBtn1.Size = new System.Drawing.Size(117, 23);
            this.registerBtn1.TabIndex = 31;
            this.registerBtn1.Text = "REGISTER";
            this.registerBtn1.UseVisualStyleBackColor = true;
            this.registerBtn1.Click += new System.EventHandler(this.registerBtn1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(641, 397);
            this.Controls.Add(this.registerBtn1);
            this.Controls.Add(this.backBtn1);
            this.Controls.Add(this.passwordField1);
            this.Controls.Add(this.usernameField1);
            this.Controls.Add(this.passwordLbl1);
            this.Controls.Add(this.usernameLbl1);
            this.Controls.Add(this.registerBanner);
            this.Controls.Add(this.loginBtn1);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.passwordField);
            this.Controls.Add(this.usernameField);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.loginBanner);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.pacmanLogo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pacmanLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registerBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registerBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pacmanLogo;
        private System.Windows.Forms.PictureBox loginBtn;
        private System.Windows.Forms.PictureBox registerBtn;
        private System.Windows.Forms.PictureBox closeBtn;
        private System.Windows.Forms.PictureBox loginBanner;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.TextBox usernameField;
        private System.Windows.Forms.TextBox passwordField;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button loginBtn1;
        private System.Windows.Forms.PictureBox registerBanner;
        private System.Windows.Forms.Label usernameLbl1;
        private System.Windows.Forms.Label passwordLbl1;
        private System.Windows.Forms.TextBox usernameField1;
        private System.Windows.Forms.TextBox passwordField1;
        private System.Windows.Forms.Button backBtn1;
        private System.Windows.Forms.Button registerBtn1;

    }
}

