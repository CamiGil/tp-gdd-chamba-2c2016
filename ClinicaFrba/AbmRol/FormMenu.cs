using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.AbmRol
{
    public partial class FormMenu : Form
    {
        AbmRol.FormCrear crearRol = new AbmRol.FormCrear();
        AbmRol.FormModif modificarRol = new AbmRol.FormModif();
        AbmRol.FormElim eliminarRol = new AbmRol.FormElim();

        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            crearRol.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            modificarRol.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            eliminarRol.Show();
            this.Close();
        }
    }
}
