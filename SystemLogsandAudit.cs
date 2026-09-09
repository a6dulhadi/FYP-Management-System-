using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VP_PBE.Student;           
using VP_PBE.Services;
namespace VP_PBE.Admin
{
    public partial class SystemLogsandAudit : Form
    {
        public SystemLogsandAudit()
        {
            InitializeComponent();
        }

        private void LoadLogs()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Select only the columns you requested: Date, User, and Status
                    string query = "SELECT created_date AS 'Date', username AS 'User', status AS 'Status' FROM users";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSystemLogs.DataSource = dt; // Ensure your grid is named dgvSystemLogs

                    // Optional: Format the date column to look cleaner
                    dgvSystemLogs.Columns["Date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading system logs: " + ex.Message);
            }
        }

        private void SystemLogs_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            ManageUsers res = new ManageUsers();
            res.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            AssignCredential res = new AssignCredential();
            res.Show();
            this.Hide();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ResetUSerPassword res = new ResetUSerPassword();
            res.Show();
            this.Hide();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FYPProjectApproval res = new FYPProjectApproval();
            res.Show();
            this.Hide();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CategorizeResources res = new CategorizeResources();
            res.Show();
            this.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SystemLogsandAudit res = new SystemLogsandAudit();
            res.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ExportData res = new ExportData();
            res.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AccessReport res = new AccessReport();
            res.Show();
            this.Hide();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // Confirm logout
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session
                SessionManager.ClearSession();

                // Open login form
                Form3 loginForm = new Form3();
                loginForm.Show();

                // Close dashboard
                this.Close();
            }
        }

        private void SystemLogsandAudit_Load(object sender, EventArgs e)
        {

        }
    }
}
