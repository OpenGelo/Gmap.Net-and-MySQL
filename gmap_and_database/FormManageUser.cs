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
    public partial class FormManageUser : MetroForm
    {
        public FormManageUser()
        {
            InitializeComponent();

            load_TableUser();
        }

        private void metroButton_AddUser_Click(object sender, EventArgs e)
        {
            GlobalVar.headerUser_string = "Add New User";
            FormExploreUser explore_user = new FormExploreUser();
            explore_user.ShowDialog();


        }

        private void metroButton_EditUser_Click(object sender, EventArgs e)
        {
            GlobalVar.headerUser_string = "Edit User";
            FormExploreUser explore_user = new FormExploreUser();
            explore_user.ShowDialog();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            load_TableUser();

        }

        void load_TableUser()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=root";
            string Query = "SELECT iduser as 'ID', user_name as 'Name', xxx, yyy FROM mapdatabase.user_table";
            MySqlConnection ConnDb = new MySqlConnection(constring);
            MySqlCommand sqlCommand = new MySqlCommand(Query, ConnDb);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = sqlCommand;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormManageUser_Load(object sender, EventArgs e)
        {
            load_TableUser();
        }

        private void metroButton_DeleteUser_Click(object sender, EventArgs e)
        {

        }

        
    }
}
