using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Net.Configuration;

namespace DB_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Adobe\Comp 1\VS Saves\DB_Test\DB1.mdf;Integrated Security=True");
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBox_ID.Text))
            {
                MessageBox.Show("Please Enter ID");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBox_Name.Text))
            {
                MessageBox.Show("Please Enter Name");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBox_Email.Text))
            {
                MessageBox.Show("Please Enter Email");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBox_Phone.Text))
            {
                MessageBox.Show("Please Enter Phone");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBox_Address.Text))
            {
                MessageBox.Show("Please Enter Address");
                return;
            }

            try
            {
                connection.Open();

                SqlCommand checkingduplicate = new SqlCommand(@"SELECT COUNT (*) FROM UserInfo WHERE ID = '"+ txtBox_ID.Text +"'", connection);
                int count = 0;
                count = Convert.ToInt32(checkingduplicate.ExecuteScalar());
                if (count == 1)
                {
                    connection.Close();
                    MessageBox.Show("User ID Already Exists! Please use another ID.");
                    return;
                }

                SqlCommand UserInformation = new SqlCommand(@"INSERT INTO UserInfo (ID, Name, Email, Phone, Address) VALUES ('" + txtBox_ID.Text + "', '" + txtBox_Name.Text + "', '" + txtBox_Email.Text + "', '" + txtBox_Phone.Text + "', '" + txtBox_Address.Text + "')", connection);
                UserInformation.ExecuteNonQuery();
                MessageBox.Show("Data Successfully Saved in Database!");
                connection.Close();
                reset();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                connection.Close();
            }

           
        }

        public void reset()
        {
            txtBox_ID.Text = null;
            txtBox_Name.Text = null;
            txtBox_Email.Text = null;
            txtBox_Address.Text = null;
            txtBox_Phone.Text = null;
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBox_ID.Text))
            {
                MessageBox.Show("Please Enter ID");
                return;
            }

            try {
                connection.Open();

                SqlCommand read = new SqlCommand(@"SELECT * FROM UserInfo WHERE ID = '" + txtBox_ID.Text + "'", connection);
                SqlDataReader reader = read.ExecuteReader();

                while (reader.Read())
                {
                    txtBox_Name.Text = reader.GetString(1);
                    txtBox_Email.Text = reader.GetString(2);
                    txtBox_Phone.Text = reader.GetString(3);
                    txtBox_Address.Text = reader.GetString(4);
                }
                connection.Close();

            }
            catch (Exception exc)
            {
                connection.Close();
                MessageBox.Show(exc.Message);
            }
        }
    }
}
