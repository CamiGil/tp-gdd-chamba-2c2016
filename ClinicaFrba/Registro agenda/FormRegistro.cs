using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Registro_agenda
{
    public partial class FormRegistro : Form
    {
        SqlConnection conexion;
        public FormRegistro()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            dtpDesde.Value = Configuraciones.fecha;
            dtpHasta.Value = Configuraciones.fecha;
        }

        private void btnRango_Click(object sender, EventArgs e)
        {
            FormRango form = new FormRango(this.lstRangos);
            form.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value > dtpHasta.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser igual o menor a la de fin");
            }
            else
            {

                double totalHoras = 0;
                for (int i = 0; i < lstRangos.Items.Count; i++)
                {
                    totalHoras += int.Parse(lstRangos.Items[i].SubItems[3].Text) - int.Parse(lstRangos.Items[i].SubItems[1].Text);
                    if (int.Parse(lstRangos.Items[i].SubItems[2].Text) < int.Parse(lstRangos.Items[i].SubItems[4].Text)){
                        totalHoras += 0.5;
                    }
                    else if (int.Parse(lstRangos.Items[i].SubItems[2].Text) > int.Parse(lstRangos.Items[i].SubItems[4].Text))
                    {
                        totalHoras -= 0.5;
                    }
                }
                if (totalHoras > 48)
                {
                    MessageBox.Show("El rango especificado supera las 48 horas semanales");
                }else if (totalHoras == 0)
                {
                    MessageBox.Show("Ingrese un rango de horarios");
                }
                else
                {
                    MessageBox.Show(totalHoras.ToString());
                }
            }
        }
    }
}
