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
        List<string> dList = new List<string>();

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
            SqlDataAdapter cmd = new SqlDataAdapter();
            SqlCommand dataIntoList = new SqlCommand("SELECT name FROM sys.Tables", myConnection); // <-- Gets all the tablenames from the database.
            try
            {
                myConnection.Open();

              //  while (reader.Read())
              //  {
              //     string tablename = reader.GetValue(0).ToString(); // <-- adds the value from the first index (0) to tableName0
              //      tableListBox.Items.Add(tablename);
              //      tableListBox.Items.Add(0);
              //  }

                DataTable dataTable = new DataTable();
                cmd.SelectCommand = dataIntoList;
                SqlDataReader reader = dataIntoList.ExecuteReader();
                myConnection.Close(); //<--- used a cmd.SelectCommand so connection has to be closed before a new cmd.Command can be used

                myConnection.Open();
                cmd.Fill(dataTable).ToString();
                foreach(DataRow data in dataTable.Rows)
                {
                    tableListBox.Items.Add(data["name"].ToString()); // <--SQL command has to be included.
                }

            }
            finally
            {
                myConnection.Close();
            }
        }

        public void Search()
        {
            SqlConnection myConnection = new SqlConnection(myConString);
            SqlCommand search = new SqlCommand("SELECT userName, password FROM Accounts WHERE userName = @searchInput",myConnection);
            search.Parameters.Add(new SqlParameter("@searchInput", searchBox.Text));
            commands.SelectCommand = search;

            try
            {
                myConnection.Open();
                SqlDataReader read = search.ExecuteReader();
                while (read.Read())
                {
                    userLbl.Text = read.GetValue(0).ToString();
                    passLbl.Text = read.GetValue(1).ToString();
                }
            }
            finally
            {
                myConnection.Close();
            }
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
