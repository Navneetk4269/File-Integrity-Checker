using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace SE_Project
{
    public partial class Form3 : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\navne\\source\\repos\\SE_Project\\SE_Project\\Database1.mdf;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox2.Text.Trim();
            string fileAddress = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileAddress))
            {
                MessageBox.Show("Please enter both file name and file address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(fileAddress))
            {
                MessageBox.Show("File not found. Please check the file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileHash;
            try
            {
                fileHash = CalculateSHA256(fileAddress);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied. Try running the application as an administrator or checking file permissions.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating file hash: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string insertFileQuery = "INSERT INTO [file_info] ([file_name], [file_address], [file_hash]) VALUES (@fileName, @fileAddress, @fileHash)";
                using (SqlCommand cmd = new SqlCommand(insertFileQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@fileAddress", fileAddress);
                    cmd.Parameters.AddWithValue("@fileHash", fileHash);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("File added successfully with hash!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;  // Set selected file path to textBox1
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
