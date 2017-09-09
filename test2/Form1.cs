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
using System.Net.Mail;
using System.Net;
using System.Reflection;

namespace test2
{
    public partial class Form1 : Form
    {
        SqlDataAdapter sda_sj;
        SqlDataAdapter sda_f;
        SqlDataAdapter sda_db;
        SqlDataAdapter sda_o;
        DataTable dt_sj;
        DataTable dt_f;
        DataTable dt_db;
        DataTable dt_o;
        SqlCommandBuilder scb_f;
        SqlCommandBuilder scb_db;
        SqlCommandBuilder scb_o;
        SqlCommandBuilder scb_sj;
   
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip sctt = new System.Windows.Forms.ToolTip();
            sctt.SetToolTip(this.sclbl, "Select a country by checking an item from the list\n below or use Select ALL checkbox to select all countries.");
            System.Windows.Forms.ToolTip remembertt = new System.Windows.Forms.ToolTip();
            remembertt.SetToolTip(this.rmbr_cb, "Check this box if you want the application\n to save your login credentials.");
            if (tablecb.SelectedItem == null) { tablecb.SelectedItem = "Job Words Category Mapping"; }
            sj_dgv.Hide();
            dgvlbl.Hide();
            hidegridsandlabels();
        }
        public void hidegridsandlabels()
        {
            fd_dgv.Hide();
            deboost_dgv.Hide();
            override_dgv.Hide();
            fclbl.Hide();
            deblbl.Hide();
            owlbl.Hide();
        }
        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = untxtbox;
            untxtbox.Focus();
            hidepages();
            untxtbox.Text = Settings.Default["Username"].ToString();
            pwtxtbox.Text = Settings.Default["Password"].ToString();
            rmbr_cb.Checked = Convert.ToBoolean(Settings.Default["Remember"]);
        }
        public void hidepages()
        {
            tabs.TabPages.Remove(tabPage2);
            tabs.TabPages.Remove(tabPage3);
            tabs.TabPages.Remove(tabPage4);
            tabs.TabPages.Remove(tabPage5);
        }

        public void showpages()
        {
            tabs.TabPages.Insert(1, tabPage2);
            tabs.TabPages.Insert(2, tabPage3);
            tabs.TabPages.Insert(3, tabPage4);
            tabs.TabPages.Insert(4, tabPage5);
        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (loginbtn.Text == "Logout")
            {
                this.ActiveControl = untxtbox;
                hidepages();
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
                showpages();
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
            var confirmResult = MessageBox.Show("Are you sure to Update the table?",
                                     "Confirm Update!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var flag = 0;
                String st = "";
                String dss = "";
                if (checkbox.SelectedItems.Count == 0 || string.IsNullOrWhiteSpace(emailtxtbox.Text))
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
        }

        private void deletebtnjob_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update the table?",
                                     "Confirm Update!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
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
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            String st = "";
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
            if (string.IsNullOrWhiteSpace(jttxtbox.Text) && tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
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
                    sda_sj = new SqlDataAdapter(sqlcom);
                    dt_sj= new DataTable();
                    sda_sj.Fill(dt_sj);
                    sj_dgv.DataSource = dt_sj;
                    sj_dgv.Show();
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
                    string[] lines = jttxtbox.Text.Split(new string[] { "\r\n", "\n", "\t" }, StringSplitOptions.None);
                    for (int j = 0; j < lines.Length; j = j + 1)
                    {
                        if (tablecb.SelectedItem.ToString() == "Job Words Category Mapping")
                        {
                            st = "insert into JobTitles_JobWordsCategoryMapping (Phrase,CategoryID) values (N'" + lines[j] + "','" + lines[j + 1] + "')";
                            j++;
                        }
                        if (tablecb.SelectedItem.ToString() == "Job Titles Local Job Category")
                        {
                            st = "insert into JobTitles_LocalJobCategory (ID,LocalDisplayName,Keywords) values(N'" + lines[j] + "','" + lines[j + 1] + "','" + lines[j + 2] + "')";
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
            var confirmResult = MessageBox.Show("Are you sure to Update the table?",
                                     "Confirm Update!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                scb_f = new SqlCommandBuilder(sda_f);
                sda_f.Update(dt_f);
                scb_db = new SqlCommandBuilder(sda_db);
                sda_db.Update(dt_db);
                scb_o = new SqlCommandBuilder(sda_o);
                sda_o.Update(dt_o);
            }
        }
        private void Feature_deboost_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fdo_clb_SelectedIndexChanged(this, EventArgs.Empty);
        }
        public void ascunsgrids()
        {
            for (int i = 0; i < fdo_clb.Items.Count; i++)
            {
                if (fdo_clb.GetItemCheckState(0) == CheckState.Unchecked)
                {
                    fclbl.Hide();
                    fd_dgv.Hide();
                }
                if (fdo_clb.GetItemCheckState(1) == CheckState.Unchecked)
                {
                    deblbl.Hide();
                    deboost_dgv.Hide();
                }
                if (fdo_clb.GetItemCheckState(2) == CheckState.Unchecked)
                {
                    owlbl.Hide();
                    override_dgv.Hide();
                }
            }
        }
        private void fdo_clb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ascunsgrids();
            if (Feature_deboost_combobox.SelectedItem == null)
            {
                MessageBox.Show("Please select a country first!.");
                deselect_all(fdo_clb, sa_fdo_cb);
            }
            else
            {
                String st = "";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + Feature_deboost_combobox.SelectedItem.ToString()].ConnectionString);
                foreach (string s in fdo_clb.CheckedItems)
                {
                    if (s == "Featured Campaigns")
                    {
                        st = "select id,campaign_name,categorykey,domain,filter,status,boost,start_time from Config_Customerv2_Campaign";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_f = new SqlDataAdapter(sqlcom);
                            dt_f = new DataTable();
                            sda_f.Fill(dt_f);
                            fd_dgv.DataSource = dt_f;
                            fd_dgv.Show();
                            fclbl.Show();
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    if (s == "Deboost Campaigns")
                    {
                        st = "select id,categorykey,domain,filter,status,start_time from Config_Customerv2_Deboosting";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_db = new SqlDataAdapter(sqlcom);
                            dt_db = new DataTable();
                            sda_db.Fill(dt_db);
                            deboost_dgv.DataSource = dt_db;
                            deboost_dgv.Show();
                            deblbl.Show();
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    if (s == "Override Settings")
                    {
                        st = "select * from Config_CategoryTree_PremiumAds";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_o = new SqlDataAdapter(sqlcom);
                            dt_o = new DataTable();
                            sda_o.Fill(dt_o);
                            override_dgv.DataSource = dt_o;
                            override_dgv.Show();
                            owlbl.Show();
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void sa_fdo_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (sa_fdo_cb.Checked == true)
            {
                for (int i = 0; i < fdo_clb.Items.Count; i++)
                {
                    fdo_clb.SetItemChecked(i, true);
                }
                sa_fdo_cb.Text = "Deselect All";
                fdo_clb_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else
            {
                deselect_all(fdo_clb, sa_fdo_cb);
            }
        }
        public void deselect_all(CheckedListBox checks, CheckBox setitems)
        {
            for (int i = 0; i < checks.Items.Count; i++)
            {
                checks.SetItemChecked(i, false);
            }
            setitems.Text = "Select All";
            setitems.Checked = false;
            hidegridsandlabels();
        }

        private void upj_btn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update the table?",
                                     "Confirm Update!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                scb_sj = new SqlCommandBuilder(sda_sj);
                sda_sj.Update(dt_sj);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.link1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://testch.hugintechnologies.com/login-saved-search");
        }

        static bool IsValidMailAddress(string mailAddress)
        {
            return Regex.IsMatch(mailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        public string[] Changelinkstotest()
        {
            string[] links = links_tb.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int j = 0; j < links.Length; j++)
            {
                links[j] = links[j].Replace("www.oferte360.ro", "testro.hugintechnologies.com");
                links[j] = links[j].Replace("www.allekleinanzeigen.at", "testat.hugintechnologies.com");
                links[j] = links[j].Replace("www.alleannoncer.dk", "testdk.hugintechnologies.com");
                links[j] = links[j].Replace("www.ilmoitusopas.fi", "testfi.hugintechnologies.com");
                links[j] = links[j].Replace("www.liquida.it/annunci", "testit.hugintechnologies.com/annunci");
                links[j] = links[j].Replace("kleinanzeige.focus.de", "testde.hugintechnologies.com");
                links[j] = links[j].Replace("www.rubrikk.no", "testno.hugintechnologies.com");
                links[j] = links[j].Replace("www.misclasificados.es", "testes.hugintechnologies.com");
                links[j] = links[j].Replace("www.annonsguide.se", "testse.hugintechnologies.com");
                links[j] = links[j].Replace("www.allekleinanzeigen.ch", "testch.hugintechnologies.com");
                links[j] = links[j].Replace("www.newsnow.co.uk/classifieds", "testuk.hugintechnologies.com/classifieds");
                links[j] = links[j].Replace("www.mesannoncesfrance.fr", "testfr.hugintechnologies.com");
                links[j] = links[j].Replace("www.zbiorogloszen.pl", "testpl.hugintechnologies.com");
                links[j] = links[j].Replace("www.allclassifieds.ie", "testie.hugintechnologies.com");
                links[j] = links[j].Replace("www.allezoekertjes.be", "testbe.hugintechnologies.com");
                links[j] = links[j].Replace("www.the-star.co.ke/classifieds", "testke.hugintechnologies.com/classifieds");
                links[j] = links[j].Replace("www.ananzi.co.za/ads", "testza.hugintechnologies.com/ads");
                links[j] = links[j].Replace("www.todoanuncios.com.ar", "testar.hugintechnologies.com");
                links[j] = links[j].Replace("www.acheiclassificado.com.br", "testbr.hugintechnologies.com");
                links[j] = links[j].Replace("www.allclassifieds.ca", "testca.hugintechnologies.com");
                links[j] = links[j].Replace("www.todoclasificados.mx", "testmx.hugintechnologies.com");
                links[j] = links[j].Replace("www.todoclasificados.co", "testco.hugintechnologies.com");
                links[j] = links[j].Replace("www.todoclasificados.cl", "testcl.hugintechnologies.com");
                links[j] = links[j].Replace("bdnews24.com/classifieds", "testbd.hugintechnologies.com/classifieds");
                links[j] = links[j].Replace("www.vietnamplus.vn/raovat", "testvn.hugintechnologies.com/raovat");
                links[j] = links[j].Replace("www.findads.com.au", "testau.hugintechnologies.com");
                links[j] = links[j].Replace("www.adsafari.in", "testin.hugintechnologies.com");
                links[j] = links[j].Replace("www.ilanbul.com.tr", "testtr.hugintechnologies.com");
            }
            return links;
        }
        public void hidessmailstuff()
        {
            ss_gb.Hide();
            link5.Hide();
            link6.Hide();
            link7.Hide();
            link8.Hide();
            link9.Hide();
            link10.Hide();
            link11.Hide();
            link12.Hide();
            link13.Hide();
            link14.Hide();
        }
        public void showssmailstuff()
        {
            ss_gb.Show();
            link5.Show();
            link6.Show();
            link7.Show();
            link8.Show();
            link9.Show();
            link10.Show();
            link11.Show();
            link12.Show();
            link13.Show();
            link14.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showssmailstuff();
            string [] links = Changelinkstotest();
            links_tb.Clear();
            if (links.Length < 5)
            {
                hidessmailstuff();
            }
            for (int j = 0; j < links.Length; j++)
            {
                links_tb.AppendText(links[j]+"\n");
            }            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string Themessage = @"<html>
                              <body>
                                <table width=""100%"">
                                <tr>
                                    <td style=""font-style:arial; color:maroon; font-weight:bold"">
                                   Hi! <br>
                                    <img src=cid:myImageID>
                                    </td>
                                </tr>
                                </table>
                                </body>
                                </html>";
            sendHtmlEmail("danitestrubrikk@gmail.com", "danitestrubrikk@gmail.com", Themessage, "Test1", "Test HTML Email", "smtp.gmail.com", 25);
            
        }
        protected void sendHtmlEmail(string from_Email, string to_Email, string body, string from_Name, string Subject, string SMTP_IP, Int32 SMTP_Server_Port)
        {
            //create an instance of new mail message
            MailMessage mail = new MailMessage();

            //set the HTML format to true
            mail.IsBodyHtml = true;

            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            //Add Image
            LinkedResource theEmailImage = new LinkedResource("../../Resources/logo.png");
            theEmailImage.ContentId = "myImageID";

            //Add the Image to the Alternate view
            htmlView.LinkedResources.Add(theEmailImage);

            //Add view to the Email Message
            mail.AlternateViews.Add(htmlView);

            //set the "from email" address and specify a friendly 'from' name
            mail.From = new MailAddress(from_Email, from_Name);

            //set the "to" email address
            mail.To.Add(to_Email);

            //set the Email subject
            mail.Subject = Subject;

            //set the SMTP info
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("danitestrubrikk@gmail.com", "Daniel.91");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = cred;
            //send the email
            smtp.Send(mail);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (IsValidMailAddress(emailto_tb.Text)&& !string.IsNullOrWhiteSpace(links_tb.Text))
            {
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;

                string userName = "danitest";

                string emailFrom = "danitestrubrikk@gmail.com";
                string password = "Daniel.91";
                string emailTo = "danitestrubrikk@gmail.com"; ;

                // Here you can put subject of the mail
                string subject = "Test Subject";
                // Body of the mail
                string body = "<div style='width: 500px; height: 266px;font-family: arial,sans-serif; font-size: 17px;'>";
                body += "<h3 style='background-color: blueviolet; margin-top:0px;'>Aspen Reporting Tool</h3>";
                //body += "<img src ="/Resources/logo.png">";
                body += "<br />";
                body += "Dear " + userName + ",";
                body += "<br />";
                body += "<p>";
                body += "Thank you for registering </p>";
                body += "<p><a href='Http://google.com'>Click Here</a>To finalize the registration process</p>";
                body += " <br />";
                body += "Thanks,";
                body += "<br />";
                body += "<b>The Team</b>";
                body += "</div>";
                // this is done using  using System.Net.Mail; & using System.Net; 
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        smtp.Timeout = 10000;
                    }
                }
            }
            else
            {
                emailto_lbl.ForeColor = System.Drawing.Color.Red;
                emailto_lbl.Text = "Invalid email address!";
            }
        }

        private void linkLabel1_LinkClicked(object sender, EventArgs e)
        {

        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(link1.Text);
        }

        private void emailto_tb_TextChanged(object sender, EventArgs e)
        {
            emailto_lbl.ForeColor = System.Drawing.Color.Black;
            emailto_lbl.Text = "Email to:";
        }

        public void openinbrowser(int linknr)
        {
            string[] links = Changelinkstotest();
            string numelink = "link" + linknr;
            ((LinkLabel)this.Controls.Find(numelink, true)[0]).LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start('"' +links[linknr] + '"');
        }

        public void copytoclip(int nr)
        {
            string[] links = Changelinkstotest();
            Clipboard.SetText(links[nr]);
        }

        private void link2_Click(object sender, EventArgs e)
        {

        }

        private void link2_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(sender.ToString());
        }
    }
}
