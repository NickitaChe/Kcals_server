using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Ccals_server
{
    internal class SQLConnector
    {
        private SqlConnection sqlConnection = null;
        SqlCommand command;
        public SQLConnector(string str)
        {
            sqlConnection = new SqlConnection(str);
            sqlConnection.Open();
        }

        public SqlConnection getSQL()
        {
            return sqlConnection;
        }

        public int getInt(string str)
        {
            try
            {
                command = new SqlCommand(
                str,
                sqlConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            int temp = Int32.Parse(command.ExecuteScalar().ToString());
            return temp;
        }


        public void Command(string str)
        {
            try
            {
                command = new SqlCommand(
                        str,
                        sqlConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        public void update_Gird(DataGridView dataGrid, string strSQL = "")
        {
            try
            {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                strSQL,
                sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            dataGrid.DataSource = null;
            dataGrid.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


















    }
}
