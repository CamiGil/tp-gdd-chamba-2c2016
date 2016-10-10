using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Listados
{
    public partial class FormEstadisticas : Form
    {
        public FormEstadisticas()
        {
            InitializeComponent();
        }

        private void btnEspecialidadesCanceladas_Click(object sender, EventArgs e)
        {
            FormEspecialidadesCanceladas form = new FormEspecialidadesCanceladas();
            form.ShowDialog();
        }

        private void btnProfesionalesConsultados_Click(object sender, EventArgs e)
        {
            FormProfesionalesConsultados form = new FormProfesionalesConsultados();
            form.ShowDialog();
        }

        private void btnProfesionalesHoras_Click(object sender, EventArgs e)
        {
            FormProfesionalesHoras form = new FormProfesionalesHoras();
            form.ShowDialog();
        }

        private void btnAfiliadosBonos_Click(object sender, EventArgs e)
        {
            FormAfiliadosBonos form = new FormAfiliadosBonos();
            form.ShowDialog();
        }

        private void btnEspecialidadesBonos_Click(object sender, EventArgs e)
        {
            FormEspecialidadesBonos form = new FormEspecialidadesBonos();
            form.ShowDialog();
        }
    }
}
