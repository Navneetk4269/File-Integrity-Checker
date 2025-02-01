using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project
{
    public partial class Form6 : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\navne\\source\\repos\\SE_Project\\SE_Project\\Database1.mdf;Integrated Security=True");
        public Form6()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text.Trim();
            string fileAddress = textBox2.Text.Trim();

            // Check if the file name and address are entered
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileAddress))
            {
                MessageBox.Show("Please enter both the file name and file address to check.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve stored hash from the database
            string storedHash = GetStoredHash(fileName);
            if (storedHash == null)
            {
                MessageBox.Show("File not found in the database. Please check the file name.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate the hash of the file at the specified address
            string currentHash;
            try
            {
                currentHash = CalculateSHA256(fileAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating file hash: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Compare the hashes
            if (storedHash == currentHash)
            {
                MessageBox.Show("File integrity is intact. The file has not been modified.", "Integrity Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("File integrity check failed. The file has been modified.", "Integrity Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog.FileName; // Set selected file path to textBox2
                }
            }
        }
        private string GetStoredHash(string fileName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT file_hash FROM [file_info] WHERE file_name = @fileName";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving stored hash: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }
        private string CalculateSHA256(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
