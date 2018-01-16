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


namespace GetDataOverview
{

    class Connection
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        public string ConnectDis
        {


            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }
}
