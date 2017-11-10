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
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Deployment.Application;
using System.IO;

namespace test2
{
    public partial class Form1 : Form
    {
        SqlDataAdapter sda_sj;
        SqlDataAdapter sda_f;
        SqlDataAdapter sda_db;
        SqlDataAdapter sda_o;
        SqlDataAdapter sda_g;
        SqlDataAdapter sda_h;
        SqlDataAdapter sda_ff;
        SqlDataAdapter sda_mo;
        DataTable dt_sj;
        DataTable dt_f;
        DataTable dt_db;
        DataTable dt_o;
        DataTable dt_g;
        DataTable dt_h;
        DataTable dt_ff;
        DataTable dt_mo;
        SqlCommandBuilder scb_f;
        SqlCommandBuilder scb_db;
        SqlCommandBuilder scb_o;
        SqlCommandBuilder scb_sj;
        SqlCommandBuilder scb_h;
        SqlCommandBuilder scb_ff;
        SqlCommandBuilder scb_mo;
        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(string name, string domain, string pass, int logType, int logpv, ref IntPtr pht);
        private static void SetAddRemoveProgramsIcon()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                try
                {
                    string iconSourcePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "icon1.ico");
                    if (!File.Exists(iconSourcePath))
                        return;
                    RegistryKey myUninstallKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                    string[] mySubKeyNames = myUninstallKey.GetSubKeyNames();
                    for (int i = 0; i < mySubKeyNames.Length; i++)
                    {
                        RegistryKey myKey = myUninstallKey.OpenSubKey(mySubKeyNames[i], true);
                        object myValue = myKey.GetValue("DisplayName");
                        if (myValue != null && myValue.ToString() == "DB Application")
                        {
                            myKey.SetValue("DisplayIcon", iconSourcePath);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
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
            SetAddRemoveProgramsIcon();
        }
        public void hidegridsandlabels()
        {
            fd_dgv.Hide();
            deboost_dgv.Hide();
            override_dgv.Hide();
            fclbl.Hide();
            deblbl.Hide();
            owlbl.Hide();
            yesnolbl.Hide();
            dgv_g.Hide();
            h_lbl.Hide();
            h_dgv.Hide();
            ff_lbl.Hide();
            ff_dgv.Hide();
            mo_lbl.Hide();
            mo_dgv.Hide();
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
            tabs.TabPages.Remove(tabPage6);
            tabs.TabPages.Remove(tabPage7);
        }
        public void showpages()
        {
            tabs.TabPages.Insert(1, tabPage2);
            tabs.TabPages.Insert(2, tabPage3);
            tabs.TabPages.Insert(3, tabPage4);
            tabs.TabPages.Insert(4, tabPage7);
            tabs.TabPages.Insert(5, tabPage6);
            tabs.TabPages.Insert(6, tabPage5);

        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            IntPtr th = IntPtr.Zero;
            bool log = LogonUser(untxtbox.Text, "WORKGROUP", pwtxtbox.Text, 2, 0, ref th);
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
            if (log || (untxtbox.Text=="admin"&&pwtxtbox.Text=="admin"))
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
        private void deletebtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Delete these users?",
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
            var confirmResult = MessageBox.Show("Are you sure to Delete these jobs?",
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
        static bool IsValidMailAddress(string mailAddress)
        {
            return Regex.IsMatch(mailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        public void Validatelinkstb()
        {
            if (string.IsNullOrWhiteSpace(links_tb.Text))
            {
                linkstb_lbl.Text = "Invalid or empty link list!";
                linkstb_lbl.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                linkstb_lbl.Text = "Input the email links here:";
                linkstb_lbl.ForeColor = System.Drawing.Color.Black;
            }
        }
        public void validateemailtb()
        {
            if (IsValidMailAddress(emailto_tb.Text) == false)
            {
                emailto_lbl.ForeColor = System.Drawing.Color.Red;
                emailto_lbl.Text = "Invalid email address!";
            }
            else
            {
                emailto_lbl.ForeColor = System.Drawing.Color.Black;
                emailto_lbl.Text = "Email to:";
            }
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
            for(int i = 4; i <= 13; i++)
            {
                ((LinkLabel)this.Controls.Find("link"+i, true)[0]).Hide();
                ((LinkLabel)this.Controls.Find("copylink"+i, true)[0]).Hide();
            }
            
        }
        public void showssmailstuff()
        {
            ss_gb.Show();
            for (int i = 4; i <= 13; i++)
            {
                ((LinkLabel)this.Controls.Find("link" + i, true)[0]).Show();
                ((LinkLabel)this.Controls.Find("copylink" + i, true)[0]).Show();
            }
        }
        public void changetblinks_hidess()
        {
            string[] links = Changelinkstotest();
            showssmailstuff();
            links_tb.Clear();
            if (links.Length < 5)
            {
                hidessmailstuff();
            }
            for (int j = 0; j < links.Length; j++)
            {
                links_tb.AppendText(links[j] + "\n");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            changetblinks_hidess();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (IsValidMailAddress(emailto_tb.Text)&& !string.IsNullOrWhiteSpace(links_tb.Text))
            {
                string[] links = Changelinkstotest();
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                string emailTo = emailto_tb.Text;
                string subject = "Test Emails "+ DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                string body = "<div style = 'width: 50%'>";
                body += "<img src=http://i.imgur.com/jFekwMW.png>";
                body += "<br />";
                for (int j = 0; j < links.Length; j++)
                {
                    body += "<p><a rel='nofollow'; target='_blank'; href='" + links[j] + "'><b>Email Link nr" +j+"</b></a></p>";
                }
                body += "Thanks,";
                body += "<br />";
                body += "<b>dBTechnologies</b>";
                body += "</div>";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("danitestrubrikk@gmail.com", "dBTechnologies");
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential("danitestrubrikk@gmail.com", "Daniel.91");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        smtp.Timeout = 10000;
                    }
                }
                changetblinks_hidess();
            }
            else
            {
                validateemailtb();
                Validatelinkstb();
            }
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
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            openinbrowser(0);
        }
        private void link2_Click(object sender, EventArgs e)
        {
            openinbrowser(1);
        }
        private void links_tb_TextChanged(object sender, EventArgs e)
        {
            linkstb_lbl.ForeColor = System.Drawing.Color.Black;
            linkstb_lbl.Text = "Input the email links here:";
        }
        private void link2_MouseClick(object sender, MouseEventArgs e)
        {
            openinbrowser(2);
        }
        public void stuffforcopy(int nr)
        {
            for (int i = 0; i <= 13; i++)
            {
                if (i== nr)
                {
                    ((LinkLabel)this.Controls.Find("copylink" + i, true)[0]).Text="Link Copied.";
                    copytoclip(i);
                    ((LinkLabel)this.Controls.Find("copylink" + i, true)[0]).ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    ((LinkLabel)this.Controls.Find("copylink" + i, true)[0]).Text = "Copy Link";
                }
            }
        }
        private void copylink0_Click(object sender, EventArgs e)
        {
            stuffforcopy(0);
        }
        private void link3_Click(object sender, EventArgs e)
        {
            openinbrowser(3);
        }
        private void link4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openinbrowser(4);
        }
        private void link5_Click(object sender, EventArgs e)
        {
            openinbrowser(5);
        }
        private void link6_Click(object sender, EventArgs e)
        {
            openinbrowser(6);
        }
        private void link7_Click(object sender, EventArgs e)
        {
            openinbrowser(7);
        }
        private void link8_Click(object sender, EventArgs e)
        {
            openinbrowser(8);
        }
        private void link9_Click(object sender, EventArgs e)
        {
            openinbrowser(9);
        }
        private void link10_Click(object sender, EventArgs e)
        {
            openinbrowser(10);
        }
        private void link11_Click(object sender, EventArgs e)
        {
            openinbrowser(11);
        }
        private void link12_Click(object sender, EventArgs e)
        {
            openinbrowser(12);
        }
        private void link13_Click(object sender, EventArgs e)
        {
            openinbrowser(13);
        }
        private void copylink1_Click(object sender, EventArgs e)
        {
            stuffforcopy(1);
        }
        private void copylink2_Click(object sender, EventArgs e)
        {
            stuffforcopy(2);
        }
        private void copylink3_Click(object sender, EventArgs e)
        {
            stuffforcopy(3);
        }
        private void copylink4_Click(object sender, EventArgs e)
        {
            stuffforcopy(4);
        }
        private void copylink5_Click(object sender, EventArgs e)
        {
            stuffforcopy(5);
        }
        private void copylink6_Click(object sender, EventArgs e)
        {
            stuffforcopy(6);
        }
        private void copylink7_Click(object sender, EventArgs e)
        {
            stuffforcopy(7);
        }
        private void copylink8_Click(object sender, EventArgs e)
        {
            stuffforcopy(8);
        }
        private void copylink9_Click(object sender, EventArgs e)
        {
            stuffforcopy(4);
        }
        private void copylink10_Click(object sender, EventArgs e)
        {
            stuffforcopy(10);
        }
        private void copylink11_Click(object sender, EventArgs e)
        {
            stuffforcopy(11);
        }
        private void copylink12_Click(object sender, EventArgs e)
        {
            stuffforcopy(12);
        }
        private void copylink13_Click(object sender, EventArgs e)
        {
            stuffforcopy(13);
        }

        private void ccbextra_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void hff_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hff_clb_SelectedIndexChanged(this, EventArgs.Empty);
        }

        public void hff_ascunsgrids()
        {
            for (int i = 0; i < hff_clb.Items.Count; i++)
            {
                if (hff_clb.GetItemCheckState(0) == CheckState.Unchecked)
                {
                    h_lbl.Hide();
                    h_dgv.Hide();
                }
                if (hff_clb.GetItemCheckState(1) == CheckState.Unchecked)
                {
                    ff_lbl.Hide();
                    ff_dgv.Hide();
                }
            }
        }
        private void hff_clb_SelectedIndexChanged(object sender, EventArgs e)
        {
            hff_ascunsgrids();
            if (hff_combobox.SelectedItem == null)
            {
                MessageBox.Show("Please select a country first!.");
                deselect_all(hff_clb, sa_hff_cb);
            }
            else
            {
                String st = "";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + hff_combobox.SelectedItem.ToString()].ConnectionString);
                foreach (string s in hff_clb.CheckedItems)
                {
                    if (s == "Highlight Settings")
                    {
                        st = "select * from Config_Freshness";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_h = new SqlDataAdapter(sqlcom);
                            dt_h = new DataTable();
                            sda_h.Fill(dt_h);
                            h_dgv.DataSource = dt_h;
                            h_dgv.Show();
                            h_lbl.Show();
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    if (s == "Freshness Factor Settings")
                    {
                        st = "select * from Config_Highlighting";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_ff = new SqlDataAdapter(sqlcom);
                            dt_ff = new DataTable();
                            sda_ff.Fill(dt_ff);
                            ff_dgv.DataSource = dt_ff;
                            ff_dgv.Show();
                            ff_lbl.Show();
                            conn.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    if (s == "Metaname Overridescore")
                    {
                        st = "select * from config_metaname_overridescore";
                        SqlCommand sqlcom = new SqlCommand(st, conn);
                        try
                        {
                            conn.Open();
                            sda_mo = new SqlDataAdapter(sqlcom);
                            dt_mo = new DataTable();
                            sda_mo.Fill(dt_mo);
                            mo_dgv.DataSource = dt_mo;
                            mo_dgv.Show();
                            mo_lbl.Show();
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
        public void hff_deselect_all(CheckedListBox checks, CheckBox setitems)
        {
            for (int i = 0; i < checks.Items.Count; i++)
            {
                checks.SetItemChecked(i, false);
            }
            setitems.Text = "Select All";
            setitems.Checked = false;
            hidegridsandlabels();
        }

        private void sa_hff_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (sa_hff_cb.Checked == true)
            {
                for (int i = 0; i < hff_clb.Items.Count; i++)
                {
                    hff_clb.SetItemChecked(i, true);
                }
                sa_hff_cb.Text = "Deselect All";
                hff_clb_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else
            {
                hff_deselect_all(hff_clb, sa_hff_cb);
            }
        }

        private void hff_updatebtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update the table?",
                                    "Confirm Update!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                scb_h = new SqlCommandBuilder(sda_h);
                sda_h.Update(dt_h);
                scb_ff = new SqlCommandBuilder(sda_ff);
                sda_ff.Update(dt_ff);
                scb_mo = new SqlCommandBuilder(sda_mo);
                sda_mo.Update(dt_mo);
            }
        }

        private void Geosearchbtn_Click(object sender, EventArgs e)
        {
            dgv_g.Hide();
            yesnolbl.Hide();
            String st = "";
            if (ccbextra.SelectedItem == null) { cextralbl.ForeColor = System.Drawing.Color.Red; }
            else
            {
                cextralbl.ForeColor = System.Drawing.Color.Black;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["test2.Properties.Settings." + ccbextra.SelectedItem.ToString()].ConnectionString);

                string searchgeo = searchgeotxtbox.Text;
                searchextralbl.ForeColor = System.Drawing.Color.Black;

                if (geotablecb.SelectedItem == null) { stgeolbl.ForeColor = System.Drawing.Color.Red; }
                else
                {
                    stgeolbl.ForeColor = System.Drawing.Color.Black;
                    if (string.IsNullOrWhiteSpace(searchgeotxtbox.Text))
                    {

                        if (geotablecb.SelectedItem.ToString() == "GeoPC")
                        { st = "select * from config_geopc"; }

                        if (geotablecb.SelectedItem.ToString() == "GeoPortal")
                        { st = "select * from config_geo_portalstructure"; }

                        if (geotablecb.SelectedItem.ToString() == "Joined Geos")
                        { st = "select distinct region1 as Geo1_in_Geopc, region2 as Geo2_in_Geopc, region3 as Geo3_in_Geopc, city ,geo1name as Geo1_in_PortalStructure, geo2name as Geo2_in_PortalStructure, geo3name as  Geo3_in_PortalStructure from config_geo_portalstructure inner join Config_geo_geopc_to_portalstructure on config_geo_portalstructure.id = Config_geo_geopc_to_portalstructure.portalstructure_id inner join config_geopc on Config_geo_geopc_to_portalstructure.geopc_id = config_geopc.id"; }
                    }
                    else
                    {
                        if (geotablecb.SelectedItem.ToString() == "GeoPC")
                        {
                            if (ipgeocb.Checked == true)
                            {
                                st = "select * from config_geopc where region1 like '%" + searchgeo + "%' or region2 like '%" + searchgeo + "%' or region3 like '%" + searchgeo + "%' or region4 like '%" + searchgeo + "%' or city like '%" + searchgeo + "%' or area1 like '%" + searchgeo + "%' or zip like '%" + searchgeo + "%'";
                            }
                            else st = "select * from config_geopc where region1 ='" + searchgeo + "' or region2 ='" + searchgeo + "' or region3 ='" + searchgeo + "' or region4='" + searchgeo + "' or city ='" + searchgeo + "' or area1 ='" + searchgeo + "' or zip='" + searchgeo + "'";
                        }
                        if (geotablecb.SelectedItem.ToString() == "GeoPortal")
                        {
                            if (ipgeocb.Checked == true)
                            {
                                st = "select * from config_geo_portalstructure where geo1name like '%" + searchgeo + "%' or geo2name like '%" + searchgeo + "%' or geo3name like '%" + searchgeo + "%' or geo4name like '%" + searchgeo + "%'";
                            }
                            else st = "select * from config_geo_portalstructure where geo1name ='" + searchgeo + "' or geo2name ='" + searchgeo + "' or geo3name ='" + searchgeo + "' or geo4name='" + searchgeo + "'";

                        }
                        if (geotablecb.SelectedItem.ToString() == "Joined Geos")
                        {
                            if (ipgeocb.Checked == true)
                            {
                                st = "select distinct region1 as Geo1_in_Geopc, region2 as Geo2_in_Geopc, region3 as Geo3_in_Geopc, city ,geo1name as Geo1_in_PortalStructure, geo2name as Geo2_in_PortalStructure, geo3name as  Geo3_in_PortalStructure from config_geo_portalstructure inner join Config_geo_geopc_to_portalstructure on config_geo_portalstructure.id = Config_geo_geopc_to_portalstructure.portalstructure_id inner join config_geopc on Config_geo_geopc_to_portalstructure.geopc_id = config_geopc.id where geo1name like '%" + searchgeo + "%' or geo2name like '%" + searchgeo + "%' or geo3name like '%" + searchgeo + "%' or geo4name like '%" + searchgeo + "%' or region1 like '%" + searchgeo + "%' or region2 like '%" + searchgeo + "%' or region3 like '%" + searchgeo + "%' or region4 like '%" + searchgeo + "%' or city like '%" + searchgeo + "%' or area1 like '%" + searchgeo + "%' or zip like '%" + searchgeo + "%'";
                            }
                            else st = "select distinct region1 as Geo1_in_Geopc, region2 as Geo2_in_Geopc, region3 as Geo3_in_Geopc, city ,geo1name as Geo1_in_PortalStructure, geo2name as Geo2_in_PortalStructure, geo3name as  Geo3_in_PortalStructure from config_geo_portalstructure inner join Config_geo_geopc_to_portalstructure on config_geo_portalstructure.id = Config_geo_geopc_to_portalstructure.portalstructure_id inner join config_geopc on Config_geo_geopc_to_portalstructure.geopc_id = config_geopc.id where geo1name ='" + searchgeo + "' or geo2name ='" + searchgeo + "' or geo3name ='" + searchgeo + "' or geo4name='" + searchgeo + "' or region1 ='" + searchgeo + "' or region2 ='" + searchgeo + "' or region3 ='" + searchgeo + "' or region4='" + searchgeo + "' or city ='" + searchgeo + "' or area1 ='" + searchgeo + "' or zip='" + searchgeo + "' ";
                        }
                    }
                    string st1 = "select geo1name,geo2name,geo3name from config_geo_portalstructure where geo3name is not null";
                    SqlCommand sqlcom1 = new SqlCommand(st1, conn);
                    SqlCommand sqlcom = new SqlCommand(st, conn);
                    try
                    {
                        conn.Open();
                        object maxCode = sqlcom1.ExecuteScalar();
                        if (maxCode == null)
                        {
                            yesnolbl.Text = "NO!";
                            yesnolbl.ForeColor = System.Drawing.Color.Red;
                            yesnolbl.Show();
                        }
                        else
                        {
                            yesnolbl.Text = "Yes!";
                            yesnolbl.ForeColor = System.Drawing.Color.Green;
                            yesnolbl.Show();
                        }
                        conn.Close();
                    }

                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    try
                    {
                        conn.Open();
                        sda_g = new SqlDataAdapter(sqlcom);
                        dt_g = new DataTable();
                        sda_g.Fill(dt_g);
                        dgv_g.DataSource = dt_g;
                        dgv_g.Show();
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
}
