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

namespace MinIProjectReserve
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAB109PC06\SQLEXPRESS; Initial Catalog=Intel; Integrated Security=True;"))
            {
                sqlCon.Open(); // Open SQL connection

                // Check if the room is available
                string availabilityQuery = "SELECT Availability FROM Mandja WHERE Company = @Company";

                SqlCommand availabilityCmd = new SqlCommand(availabilityQuery, sqlCon);
                availabilityCmd.Parameters.AddWithValue("@Company", comboBox1.Text);
                string availabilityResult = availabilityCmd.ExecuteScalar().ToString().Trim();

                if (availabilityResult.Equals("avaible", StringComparison.OrdinalIgnoreCase))
                {
                    // Room is available, proceed with booking
                    string insertQuery = "INSERT INTO Sanduci VALUES (@Name, @Phone, @Date, @Company)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, sqlCon);

                    // Set parameters based on selected values
                    insertCmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    insertCmd.Parameters.AddWithValue("@Phone", textBox2.Text);
                    insertCmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                    insertCmd.Parameters.AddWithValue("@Company", comboBox1.Text);


                    insertCmd.ExecuteNonQuery();

                    // Update room availability status
                    string updateAvailabilityQuery = "UPDATE Mandja SET Availability = 'NOT avaible' WHERE Company = @Company";
                    SqlCommand updateAvailabilityCmd = new SqlCommand(updateAvailabilityQuery, sqlCon);
                    updateAvailabilityCmd.Parameters.AddWithValue("@Company", comboBox1.Text);
                    updateAvailabilityCmd.ExecuteNonQuery();

                    MessageBox.Show("Booking Successful!");
                }
                else
                {
                    // Room is not available
                    MessageBox.Show("Sorry, the room is not available for booking.");
                }
            }
        }
    }
}
