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
    public partial class AccessReport : Form
    {
        public AccessReport()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            LoadAccessReport();
        }


        private void LoadAccessReport()
        {
            try
            {
                // Updated SQL: Uses 'created_date' to match your schema
                string query = @"SELECT l.created_date AS 'Date', u.username AS 'User', 
                                 r.resource_name AS 'Resource', c.category_name AS 'Category'
                                 FROM logs l
                                 JOIN users u ON l.user_id = u.user_id
                                 JOIN learning_resources r ON l.resource_id = r.resource_id
                                 JOIN resource_categories c ON r.category_id = c.category_id
                                 ORDER BY l.created_date DESC";

                using (var conn = DatabaseConnection.GetConnection())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Display data in the DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }



        private void AccessReport_Load(object sender, EventArgs e)
        {
            // Auto-load report when form opens
            LoadAccessReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageUsers res = new ManageUsers();
            res.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignCredential res = new AssignCredential();
            res.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetUSerPassword res = new ResetUSerPassword();
            res.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FYPProjectApproval res = new FYPProjectApproval();
            res.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CategorizeResources res = new CategorizeResources();
            res.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SystemLogsandAudit res = new SystemLogsandAudit();
            res.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ExportData res = new ExportData();
            res.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
             AccessReport res = new AccessReport();
            res.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
