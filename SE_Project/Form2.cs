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
    public partial class Form2 : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\navne\\source\\repos\\SE_Project\\SE_Project\\Database1.mdf;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();  
            form4.ShowDialog(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();  
            form5.ShowDialog(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();  
            form6.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text.Trim(); // Get the file name from the text box

            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Please enter a file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQL query to search for the file in the database
            string query = "SELECT file_name, file_address, file_hash FROM file_info WHERE file_name LIKE @fileName";

            try
            {
                connect.Open();
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@fileName", "%" + fileName + "%"); // Using wildcards to search for partial matches

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read(); // Read the first matching record
                    string foundFileName = reader["file_name"].ToString();
                    string fileAddress = reader["file_address"].ToString();
                    string fileHash = reader["file_hash"].ToString();

                    // Display the results in the label
                    label3.Text = $"File found:\nName: {foundFileName}\nAddress: {fileAddress}\nHash: {fileHash}";
                }
                else
                {
                    label3.Text = "File not found."; // Display if no file is found
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
