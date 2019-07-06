namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.connectionTag = new System.Windows.Forms.TabControl();
            this.mysqlPage = new System.Windows.Forms.TabPage();
            this.mysqlPasswordTextBox = new System.Windows.Forms.TextBox();
            this.mysqlPasswordLabel = new System.Windows.Forms.Label();
            this.mysqlUsernameTextBox = new System.Windows.Forms.TextBox();
            this.mysqlUsernameLabel = new System.Windows.Forms.Label();
            this.mysqlPortTextBox = new System.Windows.Forms.TextBox();
            this.mysqlPortLabel = new System.Windows.Forms.Label();
            this.mysqlIpTextBox = new System.Windows.Forms.TextBox();
            this.mysqlIpLabel = new System.Windows.Forms.Label();
            this.sqlServerPage = new System.Windows.Forms.TabPage();
            this.sqlServerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.sqlServerPasswordLabel = new System.Windows.Forms.Label();
            this.sqlServerUsernameTextBox = new System.Windows.Forms.TextBox();
            this.sqlServerUsernameLabel = new System.Windows.Forms.Label();
            this.sqlServerDbTextBox = new System.Windows.Forms.TextBox();
            this.sqlServerDbLabel = new System.Windows.Forms.Label();
            this.sqlServerIpTextBox = new System.Windows.Forms.TextBox();
            this.sqlServerIpLabel = new System.Windows.Forms.Label();
            this.oraclePage = new System.Windows.Forms.TabPage();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.tableNameTextBox = new System.Windows.Forms.TextBox();
            this.frameworkLabel = new System.Windows.Forms.Label();
            this.frameworName = new System.Windows.Forms.ListBox();
            this.run = new System.Windows.Forms.Button();
            this.connectionTag.SuspendLayout();
            this.mysqlPage.SuspendLayout();
            this.sqlServerPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionTag
            // 
            this.connectionTag.Controls.Add(this.mysqlPage);
            this.connectionTag.Controls.Add(this.sqlServerPage);
            this.connectionTag.Controls.Add(this.oraclePage);
            this.connectionTag.Location = new System.Drawing.Point(12, 12);
            this.connectionTag.Name = "connectionTag";
            this.connectionTag.SelectedIndex = 0;
            this.connectionTag.Size = new System.Drawing.Size(327, 161);
            this.connectionTag.TabIndex = 0;
            // 
            // mysqlPage
            // 
            this.mysqlPage.Controls.Add(this.mysqlPasswordTextBox);
            this.mysqlPage.Controls.Add(this.mysqlPasswordLabel);
            this.mysqlPage.Controls.Add(this.mysqlUsernameTextBox);
            this.mysqlPage.Controls.Add(this.mysqlUsernameLabel);
            this.mysqlPage.Controls.Add(this.mysqlPortTextBox);
            this.mysqlPage.Controls.Add(this.mysqlPortLabel);
            this.mysqlPage.Controls.Add(this.mysqlIpTextBox);
            this.mysqlPage.Controls.Add(this.mysqlIpLabel);
            this.mysqlPage.Location = new System.Drawing.Point(4, 22);
            this.mysqlPage.Name = "mysqlPage";
            this.mysqlPage.Padding = new System.Windows.Forms.Padding(3);
            this.mysqlPage.Size = new System.Drawing.Size(319, 135);
            this.mysqlPage.TabIndex = 0;
            this.mysqlPage.Text = "mysql";
            this.mysqlPage.UseVisualStyleBackColor = true;
            // 
            // mysqlPasswordTextBox
            // 
            this.mysqlPasswordTextBox.Location = new System.Drawing.Point(79, 90);
            this.mysqlPasswordTextBox.Name = "mysqlPasswordTextBox";
            this.mysqlPasswordTextBox.Size = new System.Drawing.Size(100, 21);
            this.mysqlPasswordTextBox.TabIndex = 7;
            // 
            // mysqlPasswordLabel
            // 
            this.mysqlPasswordLabel.AutoSize = true;
            this.mysqlPasswordLabel.Location = new System.Drawing.Point(44, 93);
            this.mysqlPasswordLabel.Name = "mysqlPasswordLabel";
            this.mysqlPasswordLabel.Size = new System.Drawing.Size(29, 12);
            this.mysqlPasswordLabel.TabIndex = 6;
            this.mysqlPasswordLabel.Text = "密码";
            // 
            // mysqlUsernameTextBox
            // 
            this.mysqlUsernameTextBox.Location = new System.Drawing.Point(79, 62);
            this.mysqlUsernameTextBox.Name = "mysqlUsernameTextBox";
            this.mysqlUsernameTextBox.Size = new System.Drawing.Size(100, 21);
            this.mysqlUsernameTextBox.TabIndex = 5;
            // 
            // mysqlUsernameLabel
            // 
            this.mysqlUsernameLabel.AutoSize = true;
            this.mysqlUsernameLabel.Location = new System.Drawing.Point(32, 65);
            this.mysqlUsernameLabel.Name = "mysqlUsernameLabel";
            this.mysqlUsernameLabel.Size = new System.Drawing.Size(41, 12);
            this.mysqlUsernameLabel.TabIndex = 4;
            this.mysqlUsernameLabel.Text = "用户名";
            // 
            // mysqlPortTextBox
            // 
            this.mysqlPortTextBox.Location = new System.Drawing.Point(79, 34);
            this.mysqlPortTextBox.Name = "mysqlPortTextBox";
            this.mysqlPortTextBox.Size = new System.Drawing.Size(100, 21);
            this.mysqlPortTextBox.TabIndex = 3;
            // 
            // mysqlPortLabel
            // 
            this.mysqlPortLabel.AutoSize = true;
            this.mysqlPortLabel.Location = new System.Drawing.Point(44, 37);
            this.mysqlPortLabel.Name = "mysqlPortLabel";
            this.mysqlPortLabel.Size = new System.Drawing.Size(29, 12);
            this.mysqlPortLabel.TabIndex = 2;
            this.mysqlPortLabel.Text = "端口";
            // 
            // mysqlIpTextBox
            // 
            this.mysqlIpTextBox.Location = new System.Drawing.Point(79, 7);
            this.mysqlIpTextBox.Name = "mysqlIpTextBox";
            this.mysqlIpTextBox.Size = new System.Drawing.Size(100, 21);
            this.mysqlIpTextBox.TabIndex = 1;
            // 
            // mysqlIpLabel
            // 
            this.mysqlIpLabel.AutoSize = true;
            this.mysqlIpLabel.Location = new System.Drawing.Point(8, 10);
            this.mysqlIpLabel.Name = "mysqlIpLabel";
            this.mysqlIpLabel.Size = new System.Drawing.Size(65, 12);
            this.mysqlIpLabel.TabIndex = 0;
            this.mysqlIpLabel.Text = "主机名或IP";
            // 
            // sqlServerPage
            // 
            this.sqlServerPage.Controls.Add(this.sqlServerPasswordTextBox);
            this.sqlServerPage.Controls.Add(this.sqlServerPasswordLabel);
            this.sqlServerPage.Controls.Add(this.sqlServerUsernameTextBox);
            this.sqlServerPage.Controls.Add(this.sqlServerUsernameLabel);
            this.sqlServerPage.Controls.Add(this.sqlServerDbTextBox);
            this.sqlServerPage.Controls.Add(this.sqlServerDbLabel);
            this.sqlServerPage.Controls.Add(this.sqlServerIpTextBox);
            this.sqlServerPage.Controls.Add(this.sqlServerIpLabel);
            this.sqlServerPage.Location = new System.Drawing.Point(4, 22);
            this.sqlServerPage.Name = "sqlServerPage";
            this.sqlServerPage.Padding = new System.Windows.Forms.Padding(3);
            this.sqlServerPage.Size = new System.Drawing.Size(319, 135);
            this.sqlServerPage.TabIndex = 1;
            this.sqlServerPage.Text = "sqlServer";
            this.sqlServerPage.UseVisualStyleBackColor = true;
            // 
            // sqlServerPasswordTextBox
            // 
            this.sqlServerPasswordTextBox.Location = new System.Drawing.Point(81, 93);
            this.sqlServerPasswordTextBox.Name = "sqlServerPasswordTextBox";
            this.sqlServerPasswordTextBox.Size = new System.Drawing.Size(100, 21);
            this.sqlServerPasswordTextBox.TabIndex = 15;
            // 
            // sqlServerPasswordLabel
            // 
            this.sqlServerPasswordLabel.AutoSize = true;
            this.sqlServerPasswordLabel.Location = new System.Drawing.Point(46, 96);
            this.sqlServerPasswordLabel.Name = "sqlServerPasswordLabel";
            this.sqlServerPasswordLabel.Size = new System.Drawing.Size(29, 12);
            this.sqlServerPasswordLabel.TabIndex = 14;
            this.sqlServerPasswordLabel.Text = "密码";
            // 
            // sqlServerUsernameTextBox
            // 
            this.sqlServerUsernameTextBox.Location = new System.Drawing.Point(81, 65);
            this.sqlServerUsernameTextBox.Name = "sqlServerUsernameTextBox";
            this.sqlServerUsernameTextBox.Size = new System.Drawing.Size(100, 21);
            this.sqlServerUsernameTextBox.TabIndex = 13;
            // 
            // sqlServerUsernameLabel
            // 
            this.sqlServerUsernameLabel.AutoSize = true;
            this.sqlServerUsernameLabel.Location = new System.Drawing.Point(34, 68);
            this.sqlServerUsernameLabel.Name = "sqlServerUsernameLabel";
            this.sqlServerUsernameLabel.Size = new System.Drawing.Size(41, 12);
            this.sqlServerUsernameLabel.TabIndex = 12;
            this.sqlServerUsernameLabel.Text = "用户名";
            // 
            // sqlServerDbTextBox
            // 
            this.sqlServerDbTextBox.Location = new System.Drawing.Point(81, 37);
            this.sqlServerDbTextBox.Name = "sqlServerDbTextBox";
            this.sqlServerDbTextBox.Size = new System.Drawing.Size(100, 21);
            this.sqlServerDbTextBox.TabIndex = 11;
            // 
            // sqlServerDbLabel
            // 
            this.sqlServerDbLabel.AutoSize = true;
            this.sqlServerDbLabel.Location = new System.Drawing.Point(34, 40);
            this.sqlServerDbLabel.Name = "sqlServerDbLabel";
            this.sqlServerDbLabel.Size = new System.Drawing.Size(41, 12);
            this.sqlServerDbLabel.TabIndex = 10;
            this.sqlServerDbLabel.Text = "数据库";
            // 
            // sqlServerIpTextBox
            // 
            this.sqlServerIpTextBox.Location = new System.Drawing.Point(81, 10);
            this.sqlServerIpTextBox.Name = "sqlServerIpTextBox";
            this.sqlServerIpTextBox.Size = new System.Drawing.Size(100, 21);
            this.sqlServerIpTextBox.TabIndex = 9;
            // 
            // sqlServerIpLabel
            // 
            this.sqlServerIpLabel.AutoSize = true;
            this.sqlServerIpLabel.Location = new System.Drawing.Point(10, 13);
            this.sqlServerIpLabel.Name = "sqlServerIpLabel";
            this.sqlServerIpLabel.Size = new System.Drawing.Size(65, 12);
            this.sqlServerIpLabel.TabIndex = 8;
            this.sqlServerIpLabel.Text = "主机名或IP";
            // 
            // oraclePage
            // 
            this.oraclePage.Location = new System.Drawing.Point(4, 22);
            this.oraclePage.Name = "oraclePage";
            this.oraclePage.Padding = new System.Windows.Forms.Padding(3);
            this.oraclePage.Size = new System.Drawing.Size(319, 135);
            this.oraclePage.TabIndex = 2;
            this.oraclePage.Text = "oracle";
            this.oraclePage.UseVisualStyleBackColor = true;
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(348, 23);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(29, 12);
            this.tableNameLabel.TabIndex = 0;
            this.tableNameLabel.Text = "表名";
            // 
            // tableNameTextBox
            // 
            this.tableNameTextBox.Location = new System.Drawing.Point(383, 20);
            this.tableNameTextBox.Name = "tableNameTextBox";
            this.tableNameTextBox.Size = new System.Drawing.Size(100, 21);
            this.tableNameTextBox.TabIndex = 1;
            // 
            // frameworkLabel
            // 
            this.frameworkLabel.AutoSize = true;
            this.frameworkLabel.Location = new System.Drawing.Point(348, 63);
            this.frameworkLabel.Name = "frameworkLabel";
            this.frameworkLabel.Size = new System.Drawing.Size(29, 12);
            this.frameworkLabel.TabIndex = 3;
            this.frameworkLabel.Text = "框架";
            // 
            // frameworName
            // 
            this.frameworName.FormattingEnabled = true;
            this.frameworName.ItemHeight = 12;
            this.frameworName.Items.AddRange(new object[] {
            "mybatis",
            "entity framework"});
            this.frameworName.Location = new System.Drawing.Point(383, 57);
            this.frameworName.Name = "frameworName";
            this.frameworName.Size = new System.Drawing.Size(138, 64);
            this.frameworName.TabIndex = 4;
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(383, 127);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(75, 23);
            this.run.TabIndex = 5;
            this.run.Text = "生成";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 644);
            this.Controls.Add(this.run);
            this.Controls.Add(this.frameworName);
            this.Controls.Add(this.frameworkLabel);
            this.Controls.Add(this.tableNameTextBox);
            this.Controls.Add(this.tableNameLabel);
            this.Controls.Add(this.connectionTag);
            this.Name = "Form1";
            this.Text = "Form1";
            this.connectionTag.ResumeLayout(false);
            this.mysqlPage.ResumeLayout(false);
            this.mysqlPage.PerformLayout();
            this.sqlServerPage.ResumeLayout(false);
            this.sqlServerPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl connectionTag;
        private System.Windows.Forms.TabPage mysqlPage;
        private System.Windows.Forms.TabPage sqlServerPage;
        private System.Windows.Forms.TabPage oraclePage;
        private System.Windows.Forms.Label mysqlIpLabel;
        private System.Windows.Forms.TextBox mysqlIpTextBox;
        private System.Windows.Forms.Label mysqlPortLabel;
        private System.Windows.Forms.TextBox mysqlPortTextBox;
        private System.Windows.Forms.Label mysqlUsernameLabel;
        private System.Windows.Forms.TextBox mysqlUsernameTextBox;
        private System.Windows.Forms.Label mysqlPasswordLabel;
        private System.Windows.Forms.TextBox mysqlPasswordTextBox;
        private System.Windows.Forms.TextBox sqlServerPasswordTextBox;
        private System.Windows.Forms.Label sqlServerPasswordLabel;
        private System.Windows.Forms.TextBox sqlServerUsernameTextBox;
        private System.Windows.Forms.Label sqlServerUsernameLabel;
        private System.Windows.Forms.TextBox sqlServerDbTextBox;
        private System.Windows.Forms.Label sqlServerDbLabel;
        private System.Windows.Forms.TextBox sqlServerIpTextBox;
        private System.Windows.Forms.Label sqlServerIpLabel;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.TextBox tableNameTextBox;
        private System.Windows.Forms.Label frameworkLabel;
        private System.Windows.Forms.ListBox frameworName;
        private System.Windows.Forms.Button run;
    }
}

