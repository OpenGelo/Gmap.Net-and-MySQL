using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace gmap_and_database
{
    public partial class FormLogin : MetroForm
    {
        public FormLogin()
        {
            InitializeComponent();

            ConnectDB();
        }

        void ConnectDB()
        {
            try
            {
                string myConnection = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection myConn = new MySqlConnection(myConnection);
                MySqlDataAdapter myDataAdapter = new MySqlDataAdapter();
                myDataAdapter.SelectCommand = new MySqlCommand("select *database.mapdatabase ;", myConn);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(myDataAdapter);
                myConn.Open();

                //MessageBox.Show("Connected");
                label_dbStatus.Text = "Connected to database";
                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string myConn = "datasource=localhost;port=3306;username=root;password=root";
                string Query = "SELECT *FROM mapdatabase.user_table where user_name='" + this.metroTextBox_user.Text + "'and password='" + this.TextBox_pass.Text + "';";
                MySqlConnection ConnDb = new MySqlConnection(myConn);
                MySqlCommand sqlCommand = new MySqlCommand(Query, ConnDb);

                MySqlDataReader myReader;
                ConnDb.Open();
                myReader = sqlCommand.ExecuteReader();
                int count = 0;
                while (myReader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("Login Success");
                    GlobalVar.userActive = metroTextBox_user.Text;
                    Form1 formAplikasi = new Form1();
                    this.Hide();
                    formAplikasi.ShowDialog();
                    
                    
                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate username and password");
                }
                else
                    MessageBox.Show("Username and Password is not Correct...");
                ConnDb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
           

        }

    }
}
