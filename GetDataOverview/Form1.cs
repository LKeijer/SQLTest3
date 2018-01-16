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
        string myConString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        public void DataInGridView()
        {
            connection.ConnectDis = myConString;
            SqlConnection myConnection = new SqlConnection(myConString);

            SqlCommand getTableData = new SqlCommand("SELECT * FROM Accounts", myConnection);
            try
            {
                myConnection.Open();
                commands.SelectCommand = getTableData;
                commands.Fill(dataSet);
                dataGrid.DataSource = dataSet.Tables[0];

            }
            finally
            {
                myConnection.Close();
            }

        }

        public void ListBox()
        {
            connection.ConnectDis = myConString;
            SqlConnection myConnection = new SqlConnection(myConString);
            SqlCommand commands = new SqlCommand("SELECT name FROM sys.Tables", myConnection);
            try
            {
                myConnection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                if (reader.Read())
                {
                    string tableName0 = reader.GetValue(0).ToString();
                    tableListBox.Items.Add(tableName0);
                    List<string> tableNames = new List<string>();

                }

            }
            finally
            {
                myConnection.Close();
            }
        }

        public void Search()
        {

        }

        private void getDataBtn_Click(object sender, EventArgs e)
        {
            DataInGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void getTablesBtn_Click(object sender, EventArgs e)
        {
            ListBox();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
