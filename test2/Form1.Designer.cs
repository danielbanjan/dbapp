namespace test2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rmbr_cb = new System.Windows.Forms.CheckBox();
            this.pwlbl = new System.Windows.Forms.Label();
            this.unlbl = new System.Windows.Forms.Label();
            this.pwtxtbox = new System.Windows.Forms.TextBox();
            this.untxtbox = new System.Windows.Forms.TextBox();
            this.loginbtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.emailtxtbox = new System.Windows.Forms.RichTextBox();
            this.sclbl = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sacb = new System.Windows.Forms.CheckBox();
            this.deletebtn = new System.Windows.Forms.Button();
            this.checkbox = new System.Windows.Forms.CheckedListBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pmcb = new System.Windows.Forms.CheckBox();
            this.tablecb = new System.Windows.Forms.ComboBox();
            this.jttxtbox = new System.Windows.Forms.RichTextBox();
            this.jttxtboxlbl = new System.Windows.Forms.Label();
            this.dgvlbl = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Insertbtn = new System.Windows.Forms.Button();
            this.searchbtn = new System.Windows.Forms.Button();
            this.deletebtnjob = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.sclbl1 = new System.Windows.Forms.Label();
            this.sacbjt = new System.Windows.Forms.CheckBox();
            this.jobtitlecb = new System.Windows.Forms.CheckedListBox();
            this.config_geo_location_synonymBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.italyDataSet = new test2.ItalyDataSet();
            this.config_geo_location_synonymTableAdapter = new test2.ItalyDataSetTableAdapters.config_geo_location_synonymTableAdapter();
            this.tableAdapterManager = new test2.ItalyDataSetTableAdapters.TableAdapterManager();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.config_geo_location_synonymBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.italyDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.rmbr_cb);
            this.tabPage1.Controls.Add(this.pwlbl);
            this.tabPage1.Controls.Add(this.unlbl);
            this.tabPage1.Controls.Add(this.pwtxtbox);
            this.tabPage1.Controls.Add(this.untxtbox);
            this.tabPage1.Controls.Add(this.loginbtn);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(747, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Login";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::test2.Properties.Resources.dBTechnologies_logo;
            this.pictureBox1.Location = new System.Drawing.Point(379, 125);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 141);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // rmbr_cb
            // 
            this.rmbr_cb.AutoSize = true;
            this.rmbr_cb.Location = new System.Drawing.Point(246, 203);
            this.rmbr_cb.Name = "rmbr_cb";
            this.rmbr_cb.Size = new System.Drawing.Size(94, 17);
            this.rmbr_cb.TabIndex = 6;
            this.rmbr_cb.Text = "Remember me";
            this.rmbr_cb.UseVisualStyleBackColor = true;
            // 
            // pwlbl
            // 
            this.pwlbl.AutoSize = true;
            this.pwlbl.Location = new System.Drawing.Point(104, 187);
            this.pwlbl.Name = "pwlbl";
            this.pwlbl.Size = new System.Drawing.Size(53, 13);
            this.pwlbl.TabIndex = 5;
            this.pwlbl.Text = "Password";
            // 
            // unlbl
            // 
            this.unlbl.AutoSize = true;
            this.unlbl.Location = new System.Drawing.Point(104, 145);
            this.unlbl.Name = "unlbl";
            this.unlbl.Size = new System.Drawing.Size(55, 13);
            this.unlbl.TabIndex = 4;
            this.unlbl.Text = "Username";
            // 
            // pwtxtbox
            // 
            this.pwtxtbox.Location = new System.Drawing.Point(107, 203);
            this.pwtxtbox.Name = "pwtxtbox";
            this.pwtxtbox.PasswordChar = '*';
            this.pwtxtbox.Size = new System.Drawing.Size(100, 20);
            this.pwtxtbox.TabIndex = 3;
            // 
            // untxtbox
            // 
            this.untxtbox.Location = new System.Drawing.Point(107, 161);
            this.untxtbox.Name = "untxtbox";
            this.untxtbox.Size = new System.Drawing.Size(100, 20);
            this.untxtbox.TabIndex = 2;
            // 
            // loginbtn
            // 
            this.loginbtn.Location = new System.Drawing.Point(246, 161);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(75, 23);
            this.loginbtn.TabIndex = 0;
            this.loginbtn.Text = "Login";
            this.loginbtn.UseVisualStyleBackColor = true;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.emailtxtbox);
            this.tabPage2.Controls.Add(this.sclbl);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.sacb);
            this.tabPage2.Controls.Add(this.deletebtn);
            this.tabPage2.Controls.Add(this.checkbox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(747, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Delete Users";
            // 
            // emailtxtbox
            // 
            this.emailtxtbox.AcceptsTab = true;
            this.emailtxtbox.Location = new System.Drawing.Point(0, 26);
            this.emailtxtbox.Name = "emailtxtbox";
            this.emailtxtbox.Size = new System.Drawing.Size(327, 239);
            this.emailtxtbox.TabIndex = 10;
            this.emailtxtbox.Text = "";
            // 
            // sclbl
            // 
            this.sclbl.AutoSize = true;
            this.sclbl.Location = new System.Drawing.Point(571, 13);
            this.sclbl.Name = "sclbl";
            this.sclbl.Size = new System.Drawing.Size(84, 13);
            this.sclbl.TabIndex = 9;
            this.sclbl.Text = "Select a country";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::test2.Properties.Resources.dBTechnologies_logo;
            this.pictureBox2.Location = new System.Drawing.Point(0, 306);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(327, 153);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter username you want to delete";
            // 
            // sacb
            // 
            this.sacb.AutoSize = true;
            this.sacb.BackColor = System.Drawing.Color.Transparent;
            this.sacb.Location = new System.Drawing.Point(481, 12);
            this.sacb.Name = "sacb";
            this.sacb.Size = new System.Drawing.Size(70, 17);
            this.sacb.TabIndex = 3;
            this.sacb.Text = "Select All";
            this.sacb.UseVisualStyleBackColor = false;
            this.sacb.CheckedChanged += new System.EventHandler(this.sacb_CheckedChanged);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(358, 29);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(75, 23);
            this.deletebtn.TabIndex = 2;
            this.deletebtn.Text = "Delete";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // checkbox
            // 
            this.checkbox.BackColor = System.Drawing.SystemColors.Window;
            this.checkbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkbox.CheckOnClick = true;
            this.checkbox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkbox.FormattingEnabled = true;
            this.checkbox.Items.AddRange(new object[] {
            "Argentina",
            "Australia",
            "Austria",
            "Bangladesh",
            "Belgium",
            "Brazil",
            "Canada",
            "Chile",
            "Colombia",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "India",
            "Ireland",
            "Italy",
            "Kenya",
            "Mexico",
            "Nederland",
            "Norway",
            "Poland",
            "Romania",
            "SouthAfrica",
            "Spain",
            "Sweden",
            "Switzerland",
            "Turkey",
            "UK",
            "Vietnam"});
            this.checkbox.Location = new System.Drawing.Point(480, 29);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(263, 422);
            this.checkbox.TabIndex = 1;
            // 
            // tabs
            // 
            this.tabs.AccessibleName = "";
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.ShowToolTips = true;
            this.tabs.Size = new System.Drawing.Size(755, 481);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pmcb);
            this.tabPage3.Controls.Add(this.tablecb);
            this.tabPage3.Controls.Add(this.jttxtbox);
            this.tabPage3.Controls.Add(this.jttxtboxlbl);
            this.tabPage3.Controls.Add(this.dgvlbl);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.Insertbtn);
            this.tabPage3.Controls.Add(this.searchbtn);
            this.tabPage3.Controls.Add(this.deletebtnjob);
            this.tabPage3.Controls.Add(this.pictureBox4);
            this.tabPage3.Controls.Add(this.sclbl1);
            this.tabPage3.Controls.Add(this.sacbjt);
            this.tabPage3.Controls.Add(this.jobtitlecb);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(747, 455);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Job Titles";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pmcb
            // 
            this.pmcb.AutoSize = true;
            this.pmcb.Location = new System.Drawing.Point(384, 29);
            this.pmcb.Name = "pmcb";
            this.pmcb.Size = new System.Drawing.Size(87, 17);
            this.pmcb.TabIndex = 26;
            this.pmcb.Text = "Partial match";
            this.pmcb.UseVisualStyleBackColor = true;
            // 
            // tablecb
            // 
            this.tablecb.FormattingEnabled = true;
            this.tablecb.Items.AddRange(new object[] {
            "Whitelisted Job Titles",
            "Job Words Category Mapping",
            "Job Titles Local Job Category"});
            this.tablecb.Location = new System.Drawing.Point(302, 112);
            this.tablecb.Name = "tablecb";
            this.tablecb.Size = new System.Drawing.Size(173, 21);
            this.tablecb.TabIndex = 25;
            // 
            // jttxtbox
            // 
            this.jttxtbox.AcceptsTab = true;
            this.jttxtbox.Location = new System.Drawing.Point(0, 24);
            this.jttxtbox.Name = "jttxtbox";
            this.jttxtbox.Size = new System.Drawing.Size(295, 288);
            this.jttxtbox.TabIndex = 23;
            this.jttxtbox.Text = "";
            // 
            // jttxtboxlbl
            // 
            this.jttxtboxlbl.AutoSize = true;
            this.jttxtboxlbl.Location = new System.Drawing.Point(6, 6);
            this.jttxtboxlbl.Name = "jttxtboxlbl";
            this.jttxtboxlbl.Size = new System.Drawing.Size(246, 13);
            this.jttxtboxlbl.TabIndex = 22;
            this.jttxtboxlbl.Text = "Enter Phrase or CategoryID to search for mappings";
            // 
            // dgvlbl
            // 
            this.dgvlbl.AutoSize = true;
            this.dgvlbl.Location = new System.Drawing.Point(302, 210);
            this.dgvlbl.Name = "dgvlbl";
            this.dgvlbl.Size = new System.Drawing.Size(79, 13);
            this.dgvlbl.TabIndex = 20;
            this.dgvlbl.Text = "Search Results";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(302, 229);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(442, 220);
            this.dataGridView1.TabIndex = 19;
            // 
            // Insertbtn
            // 
            this.Insertbtn.Location = new System.Drawing.Point(302, 53);
            this.Insertbtn.Name = "Insertbtn";
            this.Insertbtn.Size = new System.Drawing.Size(75, 23);
            this.Insertbtn.TabIndex = 16;
            this.Insertbtn.Text = "Insert";
            this.Insertbtn.UseVisualStyleBackColor = true;
            this.Insertbtn.Click += new System.EventHandler(this.Insertbtn_Click);
            // 
            // searchbtn
            // 
            this.searchbtn.Location = new System.Drawing.Point(302, 24);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(75, 23);
            this.searchbtn.TabIndex = 15;
            this.searchbtn.Text = "Search";
            this.searchbtn.UseVisualStyleBackColor = true;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click);
            // 
            // deletebtnjob
            // 
            this.deletebtnjob.Location = new System.Drawing.Point(302, 82);
            this.deletebtnjob.Name = "deletebtnjob";
            this.deletebtnjob.Size = new System.Drawing.Size(75, 23);
            this.deletebtnjob.TabIndex = 14;
            this.deletebtnjob.Text = "Delete";
            this.deletebtnjob.UseVisualStyleBackColor = true;
            this.deletebtnjob.Click += new System.EventHandler(this.deletebtnjob_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::test2.Properties.Resources.dBTechnologies_logo;
            this.pictureBox4.Location = new System.Drawing.Point(0, 318);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(295, 141);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // sclbl1
            // 
            this.sclbl1.AutoSize = true;
            this.sclbl1.Location = new System.Drawing.Point(572, 7);
            this.sclbl1.Name = "sclbl1";
            this.sclbl1.Size = new System.Drawing.Size(84, 13);
            this.sclbl1.TabIndex = 12;
            this.sclbl1.Text = "Select a country";
            // 
            // sacbjt
            // 
            this.sacbjt.AutoSize = true;
            this.sacbjt.BackColor = System.Drawing.Color.Transparent;
            this.sacbjt.Location = new System.Drawing.Point(482, 6);
            this.sacbjt.Name = "sacbjt";
            this.sacbjt.Size = new System.Drawing.Size(70, 17);
            this.sacbjt.TabIndex = 11;
            this.sacbjt.Text = "Select All";
            this.sacbjt.UseVisualStyleBackColor = false;
            this.sacbjt.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // jobtitlecb
            // 
            this.jobtitlecb.BackColor = System.Drawing.SystemColors.Window;
            this.jobtitlecb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.jobtitlecb.CheckOnClick = true;
            this.jobtitlecb.ForeColor = System.Drawing.SystemColors.WindowText;
            this.jobtitlecb.FormattingEnabled = true;
            this.jobtitlecb.Items.AddRange(new object[] {
            "Argentina",
            "Australia",
            "Austria",
            "Bangladesh",
            "Belgium",
            "Brazil",
            "Canada",
            "Chile",
            "Colombia",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "India",
            "Ireland",
            "Italy",
            "Kenya",
            "Mexico",
            "Nederland",
            "Norway",
            "Poland",
            "Romania",
            "SouthAfrica",
            "Spain",
            "Sweden",
            "Switzerland",
            "Turkey",
            "UK",
            "Vietnam"});
            this.jobtitlecb.Location = new System.Drawing.Point(481, 24);
            this.jobtitlecb.Name = "jobtitlecb";
            this.jobtitlecb.Size = new System.Drawing.Size(263, 197);
            this.jobtitlecb.TabIndex = 10;
            // 
            // config_geo_location_synonymBindingSource
            // 
            this.config_geo_location_synonymBindingSource.DataMember = "config_geo_location_synonym";
            this.config_geo_location_synonymBindingSource.DataSource = this.italyDataSet;
            // 
            // italyDataSet
            // 
            this.italyDataSet.DataSetName = "ItalyDataSet";
            this.italyDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // config_geo_location_synonymTableAdapter
            // 
            this.config_geo_location_synonymTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.config_geo_location_synonymTableAdapter = this.config_geo_location_synonymTableAdapter;
            this.tableAdapterManager.UpdateOrder = test2.ItalyDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // Form1
            // 
            this.AcceptButton = this.loginbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(756, 484);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.config_geo_location_synonymBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.italyDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox rmbr_cb;
        private System.Windows.Forms.Label pwlbl;
        private System.Windows.Forms.Label unlbl;
        private System.Windows.Forms.TextBox pwtxtbox;
        private System.Windows.Forms.TextBox untxtbox;
        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox sacb;
        private System.Windows.Forms.Button deletebtn;
        private System.Windows.Forms.CheckedListBox checkbox;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.Label sclbl;
        private ItalyDataSet italyDataSet;
        private System.Windows.Forms.BindingSource config_geo_location_synonymBindingSource;
        private ItalyDataSetTableAdapters.config_geo_location_synonymTableAdapter config_geo_location_synonymTableAdapter;
        private ItalyDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label sclbl1;
        private System.Windows.Forms.CheckBox sacbjt;
        private System.Windows.Forms.CheckedListBox jobtitlecb;
        private System.Windows.Forms.Button deletebtnjob;
        private System.Windows.Forms.Label dgvlbl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Insertbtn;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.Label jttxtboxlbl;
        private System.Windows.Forms.RichTextBox jttxtbox;
        private System.Windows.Forms.RichTextBox emailtxtbox;
        private System.Windows.Forms.ComboBox tablecb;
        private System.Windows.Forms.CheckBox pmcb;
    }
}

