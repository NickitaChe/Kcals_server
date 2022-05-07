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

        //Page2
        






        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _SQL.update_Gird(dataGridView1, "SELECT * FROM Products order by ~Id");
            _SQL.update_Gird(dataGridView4, "SELECT * FROM Dishes");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _SQL.Command($"INSERT INTO [PRODUCTS]	([Name], [Kcal_in_100g],[Proteins], [Fat], [Carbohydrates]) VALUES(N'{textBox1.Text}',{numericUpDown1.Value},{numericUpDown2.Value},{numericUpDown3.Value},{numericUpDown4.Value})");
            _SQL.update_Gird(dataGridView1, "SELECT * FROM Products");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _SQL.update_Gird(dataGridView1, "SELECT * FROM Products order by ~Id");
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            _SQL.update_Gird(dataGridView2, $"SELECT Name, Id FROM Products WHERE Name LIKE N'%{textBox9.Text}%' order by id");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ProdId = 0;
            int DishId = 0;
            if (e.RowIndex != null)
            {
                ProdId = _SQL.getInt($"SELECT TOP(1) * FROM(SELECT TOP({e.RowIndex + 1}) * FROM Products WHERE Name LIKE N'%{textBox9.Text}%' ORDER BY Id) AS a ORDER BY Id DESC");
                if (_SQL.getInt($"SELECT Count(*) From Dishes where[Name] LIKE N'%{textBox7.Text}%'") > 0)
                {
                    DishId = _SQL.getInt($"SELECT Id From Dishes where[Name] LIKE N'%{textBox7.Text}%'");
                }
                _SQL.Command($"Insert into Dish_composition(Dish_Id, Product_Id)  Values({DishId},{ProdId})");
                _SQL.Command($"SELECT Products.Id,Kcal_in_100g,Proteins,Fat,Carbohydrates From Dish_composition " +
                    $"join Dishes on Dish_composition.Dish_Id = Dishes.Id" +
                    $"join Products on Dish_composition.Product_Id = Products.Id " +
                    $"Where Dish_Id = {DishId}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _SQL.Command($"insert into Dishes Values (N'{textBox7.Text}')");
            _SQL.update_Gird(dataGridView4, "SELECT * FROM Dishes");
        }
    }
}
