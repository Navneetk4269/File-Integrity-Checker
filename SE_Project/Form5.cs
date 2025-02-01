using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project
{
    public partial class Form5 : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\navne\\source\\repos\\SE_Project\\SE_Project\\Database1.mdf;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text.Trim();

            // Check if the input is not empty
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Please enter the file name to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Delete file record from the database
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                // Delete file details from the file_info table
                string deleteFileQuery = "DELETE FROM [file_info] WHERE file_name = @fileName";
                using (SqlCommand cmd = new SqlCommand(deleteFileQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@fileName", fileName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("File deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear(); // Clear the textbox after successful deletion
                    }
                    else
                    {
                        MessageBox.Show("File not found. Please check the file name.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
