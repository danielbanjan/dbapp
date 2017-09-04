using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test2.Properties;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace test2
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.config_geo_location_synonymTableAdapter.Fill(this.italyDataSet.config_geo_location_synonym);
            System.Windows.Forms.ToolTip sctt = new System.Windows.Forms.ToolTip();
            sctt.SetToolTip(this.sclbl, "Select a country by checking an item from the list\n below or use Select ALL checkbox to select all countries.");
            System.Windows.Forms.ToolTip remembertt = new System.Windows.Forms.ToolTip();
            remembertt.SetToolTip(this.rmbr_cb, "Check this box if you want the application\n to save your login credentials.");
            if (tablecb.SelectedItem == null) { tablecb.SelectedItem = "Job Words Category Mapping"; }
            dataGridView1.Hide();
            dgvlbl.Hide();
            fd_dgv.Hide();
        }

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = untxtbox;
            untxtbox.Focus();
            this.tabPage2.Hide();
            this.tabPage3.Hide();
            tabs.TabPages.Remove(tabPage2);
            tabs.TabPages.Remove(tabPage3);
            tabs.TabPages.Remove(tabPage4);
            untxtbox.Text = Settings.Default["Username"].ToString();
            pwtxtbox.Text = Settings.Default["Password"].ToString();
            rmbr_cb.Checked = Convert.ToBoolean(Settings.Default["Remember"]);
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (loginbtn.Text == "Logout")
            {
                this.ActiveControl = untxtbox;
                tabs.TabPages.Remove(tabPage2);
                tabs.TabPages.Remove(tabPage3);
                tabs.TabPages.Remove(tabPage4);
                this.tabPage2.Hide();
                this.tabPage3.Hide();
                this.tabPage4.Hide();
                untxtbox.Text = "";
                pwtxtbox.Text = "";
                untxtbox.Focus();
                rmbr_cb.Checked = false;
                loginbtn.Text = "Login"; 
            }
            if (rmbr_cb.Checked == true)
            {
                Settings.Default["Username"] = untxtbox.Text;
                Settings.Default["Password"] = pwtxtbox.Text;
                Settings.Default["Remember"] = true;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default["Username"] = "";
                Settings.Default["Password"] = "";
                Settings.Default["Remember"] = false;
                Settings.Default.Save();
            }
            if ("admin" != untxtbox.Text && "admin" != pwtxtbox.Text)
            {
                unlbl.ForeColor = System.Drawing.Color.Red;
                unlbl.Text = "Invalid username!";
                pwlbl.ForeColor = System.Drawing.Color.Red;
                pwlbl.Text = "Invalid password!";
                untxtbox.Clear();
                pwtxtbox.Clear();
                untxtbox.Focus();
            }
            else
            {
                tabs.TabPages.Insert(1, tabPage2);
                tabs.TabPages.Insert(2, tabPage3);
                tabs.TabPages.Insert(3, tabPage4);
                this.tabPage2.Show();
                this.tabPage3.Show();
                this.tabPage4.Show();
                unlbl.ForeColor = System.Drawing.Color.Black;
                pwlbl.ForeColor = System.Drawing.Color.Black;
                unlbl.Text = "Username";
                pwlbl.Text = "Password";
                untxtbox.Text = Settings.Default["Username"].ToString();
                pwtxtbox.Text = Settings.Default["Password"].ToString();
                rmbr_cb.Checked = Convert.ToBoolean(Settings.Default["Remember"]);
                loginbtn.Text = "Logout";
                tabs.SelectedTab = tabPage2;
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            var flag = 0;
            String st = "";
            String dss = "";
            if(checkbox.SelectedItems.Count == 0 || string.IsNullOrWhiteSpace(emailtxtbox.Text))
            {
                sclbl.ForeColor = System.Drawing.Color.Red;
                sclbl.Text = "Please select at least one country!";
            }
            foreach (string s in checkbox.CheckedItems)
            { 
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + s].ConnectionString);
                string[] lines = emailtxtbox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int j = 0; j < lines.Length; j++)
                {
                    st = "DELETE FROM Users WHERE username = '" + lines[j] + "'";
                    dss = "DELETE FROM SavedSearch WHERE contactemail = '" + lines[j] + "'";
                    SqlCommand sqlcom = new SqlCommand(st, conn);
                    SqlCommand sqlcom2 = new SqlCommand(dss, conn);
                    SqlCommand sqlcheck = new SqlCommand("select userid from users where username = '" + lines[j] + "'", conn);
                    conn.Open();
                    var a = sqlcheck.ExecuteScalar();
                    conn.Close();
                    if (a != null)
                    {
                        conn.Close();
                        try
                        {
                            conn.Open();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred " + ex.Message);
                        }
                        try
                        {
                            String res = Convert.ToString(sqlcheck.ExecuteScalar());
                            String dm = "Delete from memberships where userid ='" + res + "'";
                            SqlCommand sqlcom3 = new SqlCommand(dm, conn);
                            sqlcom3.ExecuteNonQuery();
                            sqlcom.ExecuteNonQuery();
                            sqlcom2.ExecuteNonQuery();
                            conn.Close();
                            flag = 1;
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No users with that email on " + s + " portal.");
                    }
                } 
            }
            if (flag == 1)
            {
                MessageBox.Show("Deleted users from all selected portals.");
            } 
        }

        private void deletebtnjob_Click(object sender, EventArgs e)
        {
            String st="";
            if (jobtitlecb.SelectedItems.Count == 0 || string.IsNullOrWhiteSpace(jttxtbox.Text))
            {
                sclbl1.ForeColor = System.Drawing.Color.Red;
                jttxtboxlbl.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                foreach (string s in jobtitlecb.CheckedItems)
                {
                    sclbl1.ForeColor = System.Drawing.Color.Black;
                    jttxtboxlbl.ForeColor = System.Drawing.Color.Black;
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + s].ConnectionString);
                    string[] lines = jttxtbox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
                        {
                            if (pmcb.Checked == true)
                            {
                                st = "DELETE FROM JobTitles_JobWordsCategoryMapping WHERE phrase like '%" + lines[j] + "%'";
                            }

                            else st = "DELETE FROM JobTitles_JobWordsCategoryMapping WHERE phrase = '" + lines[j] + "'";
                        }
                        if (tablecb.SelectedItem.ToString() == "Whitelisted Job Titles")
                        {
                            if (pmcb.Checked == true)
                            {
                                st = "DELETE FROM JobTitles_WhitelistedJobTitles where JobTitle like '%" + jttxtbox.Text + "%'";
                            }
                            else st = "DELETE FROM JobTitles_WhitelistedJobTitles where JobTitle = '" + jttxtbox.Text + "'";
                        }
                        if (tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
                        {
                            if (pmcb.Checked == true)
                            {
                                st = "DELETE FROM JobTitles_LocalJobCategory where LocalDisplayName like '%" + jttxtbox.Text + "%'";
                            }
                            else st = "DELETE FROM JobTitles_LocalJobCategory where LocalDisplayName = '" + jttxtbox.Text + "'";
                        }
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sqlcom.ExecuteNonQuery();
                            conn.Close();
                        }

                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("Mappings deleted!");
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            String st="";
            int number;
            bool result = Int32.TryParse(jttxtbox.Text, out number);
            if (pmcb.Checked == true)
            {
                if (tablecb.SelectedItem.ToString() == "Whitelisted Job Titles")
                {
                    st = "select * from JobTitles_WhitelistedJobTitles where JobTitle like '%" + jttxtbox.Text + "%'";
                }
                if (tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
                {
                    st = "select * from JobTitles_JobWordsCategoryMapping where phrase like '%" + jttxtbox.Text + "%'";
                }
                if (tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
                {
                    st = "select * from JobTitles_LocalJobCategory where LocalDisplayName like '%" + jttxtbox.Text + "%'";
                }
            }
            else
            {
                if (tablecb.SelectedItem.ToString() == "Whitelisted Job Titles")
                {
                    st = "select jobtitle from JobTitles_WhitelistedJobTitles where JobTitle = '" + jttxtbox.Text + "'";
                }
                if (tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
                {
                    st = "select * from JobTitles_JobWordsCategoryMapping where phrase = '" + jttxtbox.Text + "'";
                }
                if (tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
                {
                    st = "select * from JobTitles_LocalJobCategory where LocalDisplayName = '" + jttxtbox.Text + "'";
                }
            }
            if (result == true)
            {
                st = "select * from JobTitles_JobWordsCategoryMapping where categoryID = " + jttxtbox.Text + "";
            }
            if (string.IsNullOrWhiteSpace(jttxtbox.Text)&& tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
            {
                st = "select * from JobTitles_JobWordsCategoryMapping";
            }
            if (string.IsNullOrWhiteSpace(jttxtbox.Text) && tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
            {
                st = "select * from JobTitles_LocalJobCategory";
            }
            if (string.IsNullOrWhiteSpace(jttxtbox.Text) && tablecb.SelectedItem.ToString() == "Whitelisted Job Titles")
            {
                st = "select jobtitle from JobTitles_WhitelistedJobTitles";
            }

            if (jobtitlecb.SelectedItems.Count != 1)
            {
                sclbl1.ForeColor = System.Drawing.Color.Red;
                jttxtboxlbl.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                sclbl1.ForeColor = System.Drawing.Color.Black;
                jttxtboxlbl.ForeColor = System.Drawing.Color.Black;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + Convert.ToString(jobtitlecb.SelectedItem)].ConnectionString);
                SqlCommand sqlcom = new SqlCommand(st, conn);
                try
                {
                    conn.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(sqlcom);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Show();
                    dgvlbl.Show();
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            String st = "";
            if (jobtitlecb.SelectedItems.Count == 0 || string.IsNullOrWhiteSpace(jttxtbox.Text))
            {
                sclbl1.ForeColor = System.Drawing.Color.Red;
                jttxtboxlbl.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                foreach (string s in jobtitlecb.CheckedItems)
                {
                    sclbl1.ForeColor = System.Drawing.Color.Black;
                    jttxtboxlbl.ForeColor = System.Drawing.Color.Black;
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + s].ConnectionString);
                    string[] lines = jttxtbox.Text.Split(new string[] { "\r\n", "\n","\t" }, StringSplitOptions.None);
                    for (int j = 0; j < lines.Length; j=j+1)
                    {
                        if (tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
                        {
                            st = "insert into JobTitles_JobWordsCategoryMapping (Phrase,CategoryID) values (N'" + lines[j] + "','"+lines[j+1]+"')";
                            j++;
                        }
                        if (tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
                        {
                            st = "insert into JobTitles_LocalJobCategory (ID,LocalDisplayName,Keywords) values(N'" + lines[j] + "','" + lines[j + 1] + "','"+lines[j+2]+"')";
                            j = j + 2;
                        }
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sqlcom.ExecuteNonQuery();  
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("Mappings Added!");
            }
        }

        private void sacb_CheckedChanged(object sender, EventArgs e)
        {
            if (sacb.Checked == true)
            {
                for (int i = 0; i < checkbox.Items.Count; i++)
                {
                    checkbox.SetItemChecked(i, true);
                }
                sacb.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < checkbox.Items.Count; i++)
                {
                    checkbox.SetItemChecked(i, false);
                }
                sacb.Text = "Select All";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (sacbjt.Checked == true)
            {
                for (int i = 0; i < jobtitlecb.Items.Count; i++)
                {
                    jobtitlecb.SetItemChecked(i, true);
                }
                sacbjt.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < jobtitlecb.Items.Count; i++)
                {
                    jobtitlecb.SetItemChecked(i, false);
                }
                sacbjt.Text = "Select All";
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabPage3)
            {
                jttxtbox.Focus();
            }
        }

        private void tablecb_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            tablecb.DroppedDown = true;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            //select* from Config_CategoryTree_PremiumAds
            //    select* from[dbo].[Config_Customerv2_Campaign]
            //    select * from Config_Customerv2_Deboosting 
    }

        private void Feature_deboost_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String st = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + Feature_deboost_combobox.SelectedItem.ToString()].ConnectionString);
            foreach (string s in fdo_clb.CheckedItems)
            {
                if (s == "Featured Campaigns")
                {
                    MessageBox.Show(s);
                    st = "select id,campaign_name,categorykey,domain,filter,status,boost,start_time from Config_Customerv2_Campaign";
                    SqlCommand sqlcom = new SqlCommand(st, conn);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter adp = new SqlDataAdapter(sqlcom);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        fd_dgv.DataSource = dt;
                        fd_dgv.Show();
                        fd_dgv.Show();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (s == "Deboost Campaigns")
                {
                    MessageBox.Show(s);
                    st = "select id,categorykey,domain,filter,status,boost,start_time from Config_Customerv2_Deboosting";
                    SqlCommand sqlcom = new SqlCommand(st, conn);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter adp = new SqlDataAdapter(sqlcom);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        deboost_dgv.DataSource = dt;
                        deboost_dgv.Show();
                        deboost_dgv.Show();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (s == "Override Settings")
                {
                    MessageBox.Show(s);
                    st = "select * from Config_CategoryTree_PremiumAds";
                    SqlCommand sqlcom = new SqlCommand(st, conn);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter adp = new SqlDataAdapter(sqlcom);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        override_dgv.DataSource = dt;
                        override_dgv.Show();
                        override_dgv.Show();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //private void config_geo_location_synonymDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        this.tableAdapterManager.UpdateAll(this.italyDataSet);
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("An error occurred " + ex.Message);
        //    }
        //}
        //private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //}
        //private void emailtxtbox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    this.AcceptButton = deletebtn;
        //}
    }
}
