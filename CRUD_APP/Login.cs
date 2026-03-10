using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_APP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //username and password from the text
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            //check if username or pasword is empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password");
                return;
            }
            //create database connection
            DBConnect db = new DBConnect();

            try
            {
                //OPEN DATABASE
                db.Open();
                string query = "SELECT COUNT(*) FROM users WHERE username=@username AND  password=@password"; 
                //create MYSQL query
               MySql.Data.MySqlClient.MySqlCommand cmd =
                new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                //add parameters to prevent sql injection
                cmd.Parameters.AddWithValue("@username", username );
                cmd.Parameters.AddWithValue("@password", password);
                //exexcute query and get result number
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose(); // relsease command resources

                if (count == 1)
                {
                    MessageBox.Show("login succesful!");
                    //open dashboad
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("invalid username or password");
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }
    }
}
