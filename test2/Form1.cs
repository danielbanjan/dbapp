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
        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = untxtbox;
            untxtbox.Focus();
            this.tabPage2.Hide();
            this.Synonymspage.Hide();
            this.tabPage3.Hide();
            tabs.TabPages.Remove(tabPage2);
            tabs.TabPages.Remove(Synonymspage);
            tabs.TabPages.Remove(tabPage3);
            untxtbox.Text = Settings.Default["Username"].ToString();
            pwtxtbox.Text = Settings.Default["Password"].ToString();
            rmbr_cb.Checked = Convert.ToBoolean(Settings.Default["Remember"]);

        }
 
        private void loginbtn_Click(object sender, EventArgs e)
        {
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

            var users = new string[4] {"admin", "daniel" ,"test","andra"};
            var passwords = new string[4] { "admin" , "Daniel.91" ,  "test", "andra" };
            for (var i = 0; i < users.Length; i++)
            {
                if (users[i] == untxtbox.Text && passwords[i] == pwtxtbox.Text)
                {
                    tabs.TabPages.Insert(1, tabPage2);
                    tabs.TabPages.Insert(2, Synonymspage);
                    tabs.TabPages.Insert(3, tabPage3);
                    this.tabPage2.Show();
                    this.Synonymspage.Show();
                    this.tabPage3.Show();

                    tabs.SelectedTab = tabPage2;
                }
                else
                {
                    unlbl.ForeColor = System.Drawing.Color.Red;
                    unlbl.Text = "Invalid username!";
                    pwlbl.ForeColor = System.Drawing.Color.Red;
                    pwlbl.Text = "Invalid password!";
                    untxtbox.Clear();
                    pwtxtbox.Clear();
                    untxtbox.Focus();
                }


            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            String st = "DELETE FROM Users WHERE username = '" + emailtxtbox.Text + "'";
            String dss = "DELETE FROM SavedSearch WHERE contactemail = '" + emailtxtbox.Text + "'";
            if(checkbox.SelectedItems.Count == 0 || string.IsNullOrWhiteSpace(emailtxtbox.Text))
            {
                sclbl.ForeColor = System.Drawing.Color.Red;
                sclbl.Text = "Please select at least one country!";
            }
            foreach(string s in checkbox.CheckedItems)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + s].ConnectionString);
                SqlCommand sqlcom = new SqlCommand(st, conn);
                SqlCommand sqlcom2 = new SqlCommand(dss, conn);
                SqlCommand sqlcheck = new SqlCommand("select userid from users where username = '" + emailtxtbox.Text + "'", conn);
 
                try
                {
                    conn.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occurred "+ ex.Message);
                }
                if (sqlcheck.ExecuteScalar() != null)
                {
                    try
                    {
                        String res = Convert.ToString(sqlcheck.ExecuteScalar());
                        String dm = "Delete from memberships where userid ='" + res+"'";
                        SqlCommand sqlcom3 = new SqlCommand(dm, conn);
                        sqlcom3.ExecuteNonQuery();
                        sqlcom.ExecuteNonQuery();
                        sqlcom2.ExecuteNonQuery();
                        conn.Close();
                        if (sacb.Checked == false)
                        {
                            MessageBox.Show("Deleted " + emailtxtbox.Text + " user from " + s + " portal.");
                        }
                        else
                        {
                            MessageBox.Show("Deleted " + emailtxtbox.Text + " user from all portals.");
                        }

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else { MessageBox.Show("No users with that email on "+s+" portal."); }
                
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

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'italyDataSet.config_geo_location_synonym' table. You can move, or remove it, as needed.
            this.config_geo_location_synonymTableAdapter.Fill(this.italyDataSet.config_geo_location_synonym);
            // TODO: This line of code loads data into the 'loginsDataSet.Users' table. You can move, or remove it, as needed.
            System.Windows.Forms.ToolTip sctt = new System.Windows.Forms.ToolTip();
            sctt.SetToolTip(this.sclbl, "Select a country by checking an item from the list\n below or use Select ALL checkbox to select all countries.");
            System.Windows.Forms.ToolTip remembertt = new System.Windows.Forms.ToolTip();
            remembertt.SetToolTip(this.rmbr_cb, "Check this box if you want the application\n to save your login credentials.");
        }

        private void usersBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void emailtxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            this.AcceptButton = deletebtn;
        }

        private void config_geo_location_synonymBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.config_geo_location_synonymBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.italyDataSet);
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred ",ex.Message);
            }

        }

        private void config_geo_location_synonymDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.tableAdapterManager.UpdateAll(this.italyDataSet);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred "+ ex.Message);
            }
        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        
        private void deletebtnjob_Click(object sender, EventArgs e)
        {
            String st;
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
                        st = "DELETE FROM JobTitles_JobWordsCategoryMapping WHERE phrase = '" + lines[j] + "'";
                        MessageBox.Show(st);
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
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabPage3)
            {
                jttxtbox.Focus();
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            String st;
            int number;
            bool result = Int32.TryParse(jttxtbox.Text, out number);
            if (result == true)
            { st = "select * from JobTitles_JobWordsCategoryMapping where categoryID = '" + jttxtbox.Text + "'"; }
            else
            { st = "select * from JobTitles_JobWordsCategoryMapping where phrase = '" + jttxtbox.Text + "'"; }
            if (string.IsNullOrWhiteSpace(jttxtbox.Text))
            {
                st = "select * from JobTitles_JobWordsCategoryMapping";
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
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
