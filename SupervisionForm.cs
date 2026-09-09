using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using VP_PBE.Student;
using VP_PBE.Services;

namespace VP_PBE.Lecture
{
    public partial class SupervisionForm : Form
    {
       
        public SupervisionForm()
        {
            InitializeComponent();
        }

        // ================= LOAD FORM =================
        private void SupervisionForm_Load(object sender, EventArgs e)
        {
          
        }



        private void LoadSupervisionData()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT supervision_id, student_name, progress_notes, feedback, last_updated FROM student_supervision";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSupervision.DataSource = dt; // Ensure this matches your grid name
            }
        }




       

        // ================= SAVE FEEDBACK =================
        private void btnSaveFeedback_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO student_supervision (student_name, feedback) VALUES (@name, @feed)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // You might want to add a Student ComboBox to your UI to get the name
                        cmd.Parameters.AddWithValue("@name", "Selected Student");
                        cmd.Parameters.AddWithValue("@feed", txtFeedback.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSupervisionData();
                MessageBox.Show("Feedback saved!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // ================= UPDATE PROGRESS =================
        private void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            // This logic would typically use an UPDATE SQL command if a row is selected in the grid
            if (dgvSupervision.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvSupervision.CurrentRow.Cells["supervision_id"].Value);

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE student_supervision SET progress_notes = @notes WHERE supervision_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@notes", txtProgressNotes.Text);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSupervisionData();
                MessageBox.Show("Progress updated!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // ================= BACK =================
        private void btnBack_Click(object sender, EventArgs e)
        {
            LecturerDashboard dash = new LecturerDashboard();
            dash.Show();
            this.Close();
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSupervision.Rows[e.RowIndex];
                txtProgressNotes.Text = row.Cells["progress_notes"].Value?.ToString();
                txtFeedback.Text = row.Cells["feedback"].Value?.ToString();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LecturerDashboard fyp = new LecturerDashboard();
            fyp.Show();
        }

        private void btnFYP_Click(object sender, EventArgs e)
        {
            FYPManagementForm fyp = new FYPManagementForm();
            fyp.Show();
            this.Hide();
        }

        private void btnSupervision_Click(object sender, EventArgs e)
        {
            SupervisionForm sup = new SupervisionForm();
            sup.Show();
            this.Hide();
        }

        private void btnResources_Click(object sender, EventArgs e)
        {
            ResourceManagementForm res = new ResourceManagementForm();
            res.Show();
            this.Hide();
        }

        private void btnQuestionBank_Click(object sender, EventArgs e)
        {
            QuestionBankForm qb = new QuestionBankForm();
            qb.Show();
            this.Hide();
        }

        private void btnPastQuestions_Click(object sender, EventArgs e)
        {
            PastQuestionForm pq = new PastQuestionForm();
            pq.Show();
            this.Hide();
        }

        private void btnGrading_Click(object sender, EventArgs e)
        {
            GradingForm grade = new GradingForm();
            grade.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO student_supervision (student_name, progress_notes, feedback) VALUES (@name, @notes, @feedback)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@notes", txtProgressNotes.Text);
                    cmd.Parameters.AddWithValue("@feedback", txtFeedback.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadSupervisionData();
            MessageBox.Show("Student added successfully!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvSupervision.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvSupervision.CurrentRow.Cells["supervision_id"].Value);

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE student_supervision SET progress_notes=@notes, feedback=@feedback WHERE supervision_id=@id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@notes", txtProgressNotes.Text);
                    cmd.Parameters.AddWithValue("@feedback", txtFeedback.Text);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadSupervisionData();
            MessageBox.Show("Record updated!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvSupervision.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvSupervision.CurrentRow.Cells["supervision_id"].Value);

            if (MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM student_supervision WHERE supervision_id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSupervisionData();
            }
        }
    }
}