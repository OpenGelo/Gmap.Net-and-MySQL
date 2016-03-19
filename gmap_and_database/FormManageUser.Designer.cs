namespace gmap_and_database
{
    partial class FormManageUser
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton_DeleteUser = new MetroFramework.Controls.MetroButton();
            this.metroButton_AddUser = new MetroFramework.Controls.MetroButton();
            this.metroButton_EditUser = new MetroFramework.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroButton_SearchUser = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(738, 384);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(538, 346);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroButton1);
            this.panel1.Controls.Add(this.metroButton_DeleteUser);
            this.panel1.Controls.Add(this.metroButton_AddUser);
            this.panel1.Controls.Add(this.metroButton_EditUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(547, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 346);
            this.panel1.TabIndex = 1;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(40, 179);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(106, 37);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "Load Table";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton_DeleteUser
            // 
            this.metroButton_DeleteUser.Location = new System.Drawing.Point(40, 126);
            this.metroButton_DeleteUser.Name = "metroButton_DeleteUser";
            this.metroButton_DeleteUser.Size = new System.Drawing.Size(106, 37);
            this.metroButton_DeleteUser.TabIndex = 2;
            this.metroButton_DeleteUser.Text = "Delete User";
            this.metroButton_DeleteUser.UseSelectable = true;
            this.metroButton_DeleteUser.Click += new System.EventHandler(this.metroButton_DeleteUser_Click);
            // 
            // metroButton_AddUser
            // 
            this.metroButton_AddUser.Location = new System.Drawing.Point(40, 21);
            this.metroButton_AddUser.Name = "metroButton_AddUser";
            this.metroButton_AddUser.Size = new System.Drawing.Size(106, 36);
            this.metroButton_AddUser.TabIndex = 1;
            this.metroButton_AddUser.Text = "Add New User";
            this.metroButton_AddUser.UseSelectable = true;
            this.metroButton_AddUser.Click += new System.EventHandler(this.metroButton_AddUser_Click);
            // 
            // metroButton_EditUser
            // 
            this.metroButton_EditUser.Location = new System.Drawing.Point(40, 73);
            this.metroButton_EditUser.Name = "metroButton_EditUser";
            this.metroButton_EditUser.Size = new System.Drawing.Size(106, 37);
            this.metroButton_EditUser.TabIndex = 0;
            this.metroButton_EditUser.Text = "Edit User";
            this.metroButton_EditUser.UseSelectable = true;
            this.metroButton_EditUser.Click += new System.EventHandler(this.metroButton_EditUser_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 355);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.metroTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroButton_SearchUser);
            this.splitContainer1.Size = new System.Drawing.Size(538, 26);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 2;
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(175, 0);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.Size = new System.Drawing.Size(266, 26);
            this.metroTextBox1.TabIndex = 0;
            this.metroTextBox1.UseSelectable = true;
            // 
            // metroButton_SearchUser
            // 
            this.metroButton_SearchUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroButton_SearchUser.Location = new System.Drawing.Point(0, 0);
            this.metroButton_SearchUser.Name = "metroButton_SearchUser";
            this.metroButton_SearchUser.Size = new System.Drawing.Size(93, 26);
            this.metroButton_SearchUser.TabIndex = 0;
            this.metroButton_SearchUser.Text = "Search";
            this.metroButton_SearchUser.UseSelectable = true;
            // 
            // FormManageUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 464);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(778, 464);
            this.Name = "FormManageUser";
            this.Text = "ManageUser";
            this.Load += new System.EventHandler(this.FormManageUser_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton metroButton_DeleteUser;
        private MetroFramework.Controls.MetroButton metroButton_AddUser;
        private MetroFramework.Controls.MetroButton metroButton_EditUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroButton metroButton_SearchUser;
        private MetroFramework.Controls.MetroButton metroButton1;

    }
}