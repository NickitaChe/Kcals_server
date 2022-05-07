using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ccals_server
{
    public partial class Form1 : Form
    {
        static string ConnectionString = "Data Source=DESKTOP-PNB865S;Initial Catalog=Kcal;Integrated Security=True";
        SQLConnector _SQL = new SQLConnector(ConnectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _SQL.update_Gird(dataGridView1, "SELECT * FROM Products");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _SQL.Command($"INSERT INTO [PRODUCTS]	([Name], [Kcal_in_100g]) VALUES(N'{textBox1.Text}',{numericUpDown1.Value})");
        }
    }
}
