using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SQLTest3
{
    public partial class Form1 : Form
    {
        DataSet dataSet = new DataSet();
        SqlDataAdapter commands = new SqlDataAdapter();
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
            passBox.PasswordChar = '*'; // sets input in textBox *
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            string userInput = userBox.Text.ToString();
            string passInput = passBox.Text.ToString();

            SqlConnection myConnection = new SqlConnection(connectionString);
            commands.InsertCommand = new SqlCommand("INSERT INTO Accounts (userName, password) VALUES (@user, @pass)", myConnection);
            commands.InsertCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = userInput;
            commands.InsertCommand.Parameters.Add("@pass", SqlDbType.VarChar).Value = passInput;
            if (userInput != "" && passInput != "")
            {
                try
                {
                    myConnection.Open();
                    commands.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Account created!");

                }
                catch (OleDbException ex)
                {
                   MessageBox.Show(ex.Message);
                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a username&password.");
            }
           


        }

        private void getBtn_Click(object sender, EventArgs e)
        {
            string userInput = userBox.Text.ToString();
            string passInput = passBox.Text.ToString();
            SqlConnection myConnection = new SqlConnection(connectionString);

            SqlCommand login = new SqlCommand("SELECT userName,password FROM Accounts WHERE userName=@user and password=@pass", myConnection);
            login.Parameters.Add(new SqlParameter("@user", userInput)); 
            login.Parameters.Add(new SqlParameter("@pass", passInput));
            if (userInput != "" && passInput != "")
            {
                try
                {
                    myConnection.Open();
                    SqlDataReader reader = login.ExecuteReader(); // <--- requires open connection
                    if (reader.Read())
                    {
                        myConnection.Close();
                        reader.Close();
                        MessageBox.Show("Login succesfull!");
                    }
                    else
                    {
                        MessageBox.Show("Login Failed.");
                    }
                    
                }
                finally
                {
                    myConnection.Close();
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
