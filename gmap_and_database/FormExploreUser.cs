using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace gmap_and_database
{
    public partial class FormExploreUser : MetroForm
    {
        public FormExploreUser()
        {
            InitializeComponent();

            this.Text = GlobalVar.headerUser_string;
        }
    }
}
