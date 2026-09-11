namespace UI.Login
{
    partial class LoginUI
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
            loginGroupBox = new GroupBox();
            loginUIButtonIniciarSesion = new Button();
            textBoxContrasena = new TextBox();
            textBoxUsername = new TextBox();
            loginUILabelContrasena = new Label();
            loginUILabelUsername = new Label();
            loginGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // loginGroupBox
            // 
            loginGroupBox.Anchor = AnchorStyles.None;
            loginGroupBox.Controls.Add(loginUIButtonIniciarSesion);
            loginGroupBox.Controls.Add(textBoxContrasena);
            loginGroupBox.Controls.Add(textBoxUsername);
            loginGroupBox.Controls.Add(loginUILabelContrasena);
            loginGroupBox.Controls.Add(loginUILabelUsername);
            loginGroupBox.Location = new Point(402, 179);
            loginGroupBox.Name = "loginGroupBox";
            loginGroupBox.Size = new Size(420, 190);
            loginGroupBox.TabIndex = 0;
            loginGroupBox.TabStop = false;
            loginGroupBox.Text = "Inicio de sesión";
            // 
            // loginUIButtonIniciarSesion
            // 
            loginUIButtonIniciarSesion.Anchor = AnchorStyles.Top;
            loginUIButtonIniciarSesion.Location = new Point(157, 142);
            loginUIButtonIniciarSesion.Name = "loginUIButtonIniciarSesion";
            loginUIButtonIniciarSesion.Size = new Size(106, 32);
            loginUIButtonIniciarSesion.TabIndex = 4;
            loginUIButtonIniciarSesion.Text = "Iniciar sesión";
            loginUIButtonIniciarSesion.UseVisualStyleBackColor = true;
            loginUIButtonIniciarSesion.Click += loginUIButtonIniciarSesion_Click;
            // 
            // textBoxContrasena
            // 
            textBoxContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxContrasena.Location = new Point(123, 97);
            textBoxContrasena.Name = "textBoxContrasena";
            textBoxContrasena.Size = new Size(271, 23);
            textBoxContrasena.TabIndex = 3;
            textBoxContrasena.UseSystemPasswordChar = true;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxUsername.Location = new Point(123, 52);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(271, 23);
            textBoxUsername.TabIndex = 2;
            // 
            // loginUILabelContrasena
            // 
            loginUILabelContrasena.AutoSize = true;
            loginUILabelContrasena.Location = new Point(24, 100);
            loginUILabelContrasena.Name = "loginUILabelContrasena";
            loginUILabelContrasena.Size = new Size(67, 15);
            loginUILabelContrasena.TabIndex = 1;
            loginUILabelContrasena.Text = "Contraseña";
            // 
            // loginUILabelUsername
            // 
            loginUILabelUsername.AutoSize = true;
            loginUILabelUsername.Location = new Point(24, 55);
            loginUILabelUsername.Name = "loginUILabelUsername";
            loginUILabelUsername.Size = new Size(60, 15);
            loginUILabelUsername.TabIndex = 0;
            loginUILabelUsername.Text = "Username";
            // 
            // LoginUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1224, 548);
            ControlBox = false;
            Controls.Add(loginGroupBox);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(900, 500);
            Name = "LoginUI";
            Text = "LoginUI";
            WindowState = FormWindowState.Maximized;
            loginGroupBox.ResumeLayout(false);
            loginGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox loginGroupBox;
        private Label loginUILabelUsername;
        private Label loginUILabelContrasena;
        private TextBox textBoxUsername;
        private TextBox textBoxContrasena;
        private Button loginUIButtonIniciarSesion;
    }
}