using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient; // <-- allows for Sql methods.
using System.Configuration;  // <-- allows for connectionStrings.
using System.Data.OleDb; // <-- added this 4 teh lulz, some kind exception catch

namespace GetDataOverview
{
    public partial class Form1 : Form
    {

        DataSet dataSet = new DataSet();
        private Connection connection = new Connection();
        SqlDataAdapter commands = new SqlDataAdapter();
        private string myConString;

        public Form1()
        {
            InitializeComponent();
        }

        private void getDataBtn_Click(object sender, EventArgs e)
        {
            connection.ConnectDis = myConString;
            SqlConnection myConnection = new SqlConnection(myConString);

            SqlCommand getTableData = new SqlCommand("SELECT * FROM Accounts", myConnection);
            try
            {
                myConnection.Open();

            }
            finally
            {
                myConnection.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
