namespace gmap_and_database
{
    partial class FormLogin
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroTextBox_user = new MetroFramework.Controls.MetroTextBox();
            this.TextBox_pass = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.label_dbStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.90909F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(453, 220);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_dbStatus);
            this.panel1.Controls.Add(this.metroButton2);
            this.panel1.Controls.Add(this.metroButton1);
            this.panel1.Controls.Add(this.TextBox_pass);
            this.panel1.Controls.Add(this.metroTextBox_user);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 214);
            this.panel1.TabIndex = 0;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(162, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(82, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "User Name :";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(162, 85);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(71, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "Password :";
            // 
            // metroTextBox_user
            // 
            this.metroTextBox_user.Lines = new string[0];
            this.metroTextBox_user.Location = new System.Drawing.Point(259, 50);
            this.metroTextBox_user.MaxLength = 32767;
            this.metroTextBox_user.Name = "metroTextBox_user";
            this.metroTextBox_user.PasswordChar = '\0';
            this.metroTextBox_user.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_user.SelectedText = "";
            this.metroTextBox_user.Size = new System.Drawing.Size(155, 23);
            this.metroTextBox_user.TabIndex = 2;
            this.metroTextBox_user.UseSelectable = true;
            // 
            // TextBox_pass
            // 
            this.TextBox_pass.Lines = new string[0];
            this.TextBox_pass.Location = new System.Drawing.Point(258, 85);
            this.TextBox_pass.MaxLength = 32767;
            this.TextBox_pass.Name = "TextBox_pass";
            this.TextBox_pass.PasswordChar = '*';
            this.TextBox_pass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TextBox_pass.SelectedText = "";
            this.TextBox_pass.Size = new System.Drawing.Size(156, 23);
            this.TextBox_pass.TabIndex = 3;
            this.TextBox_pass.UseSelectable = true;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(339, 149);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 28);
            this.metroButton1.TabIndex = 4;
            this.metroButton1.Text = "Login";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(258, 149);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(75, 28);
            this.metroButton2.TabIndex = 5;
            this.metroButton2.Text = "Cancel";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // label_dbStatus
            // 
            this.label_dbStatus.AutoSize = true;
            this.label_dbStatus.Location = new System.Drawing.Point(6, 195);
            this.label_dbStatus.Name = "label_dbStatus";
            this.label_dbStatus.Size = new System.Drawing.Size(47, 13);
            this.label_dbStatus.TabIndex = 6;
            this.label_dbStatus.Text = "dbstatus";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 300);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.Resizable = false;
            this.Text = "FormLogin";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroTextBox TextBox_pass;
        private MetroFramework.Controls.MetroTextBox metroTextBox_user;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Label label_dbStatus;

    }
}