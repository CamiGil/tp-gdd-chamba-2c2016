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

namespace ClinicaFrba.AbmRol
{
    public partial class FormModif : Form
    {
        SqlConnection conexion;
        SqlDataReader data;

        public FormModif()
        {
            InitializeComponent();
        }

        private void FormModif_Load(object sender, EventArgs e)
        {

        }
    }
}
