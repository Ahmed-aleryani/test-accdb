using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Asaad
{

    public partial class Form1 : Form
    {
        OleDbConnection connection;
        public Form1()
        {
            InitializeComponent();
            try
            {
                connection = new OleDbConnection();
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=student.accdb;Persist Security Info=False;";

                MessageBox.Show("connected");

                string usrdb, passdb;
                connection.Open();
                OleDbCommand select = connection.CreateCommand();
                select.CommandText = "select * from users";
                select.ExecuteNonQuery();
                OleDbDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    usrdb = reader[1].ToString();
                    passdb = reader[2].ToString();
              
                    listBox1.Items.Add("User: " + usrdb + " - pass: " + passdb);
                }
                connection.Close();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox2.Text;
            string user = textBox1.Text;
            string usrdb = "", passdb = "";

            connection.Open();
        
            OleDbCommand select = connection.CreateCommand();
            select.CommandText = "select * from users";
            select.ExecuteNonQuery();
            OleDbDataReader reader = select.ExecuteReader();
            reader.Read();
            
                usrdb = reader[1].ToString();
                passdb = reader[2].ToString();
            connection.Close();
            if (user == usrdb && pass == passdb)
            {
                MessageBox.Show(usrdb);
                Form2 obj = new Form2();
                obj.Show();
                listBox1.Items.Add("User: " + usrdb + " - pass: " + passdb);
              /*  this.Visible = false;*/
            }

            else
            {
                connection.Open();
                MessageBox.Show("invalid username and password..adding new user to db");
                OleDbCommand insert = connection.CreateCommand();
                insert.CommandText =string.Format("INSERT INTO 'users' ({0},{1},{2})", 1,user,pass);
                insert.ExecuteNonQuery();

                listBox1.Items.Add("User: " + user + " - pass: " + pass);
                connection.Close();
             
            }
           
        }
    }
}
