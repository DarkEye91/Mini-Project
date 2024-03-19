using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinIProjectReserve
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAB109PC06\SQLEXPRESS; Initial Catalog=Intel; Integrated Security=True;"))
            {
                bool a = false;
                bool b = false;
                bool c = false;
                bool d = false;


                sqlCon.Open(); // Open SQL connection

                // Start with a base SQL query
                string query = "SELECT * FROM Mandja WHERE 1 = 1";

                // Create a list to store the conditions for filtering
                List<string> conditions = new List<string>();

                // Check each ComboBox and add a condition if an item is selected
                if (comboBox1.SelectedItem != null)
                {
                    a = true;
                    conditions.Add("Company = @restaurant");
                }

                if (comboBox2.SelectedItem != null)
                {

                    b = true;
                    conditions.Add("Foodtype = @foodtype");
                }

                if (comboBox3.SelectedItem != null)
                {
                    c = true;
                    conditions.Add("Pricetype = @price");
                }

                if (comboBox4.SelectedItem != null)
                {
                    d = true;
                    conditions.Add("Speed = @speed");
                }



                // combine the conditions into the SQL query
                if (conditions.Count > 0)
                {
                    query += " AND " + string.Join(" AND ", conditions);
                }

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                if (a)
                    cmd.Parameters.AddWithValue("@restaurant", comboBox1.SelectedItem.ToString());
                if (b)
                    cmd.Parameters.AddWithValue("@foodtype", comboBox2.SelectedItem.ToString());
                if (c)
                    cmd.Parameters.AddWithValue("@price", comboBox3.SelectedItem.ToString());
                if (d)
                    cmd.Parameters.AddWithValue("@speed", comboBox4.SelectedItem.ToString());


                // Use SqlDataAdapter to fetch data and populate DataGridView
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fom = new();
            fom.ShowDialog();
        }
    }
}
