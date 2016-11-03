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
            dtpDesde.MinDate = Configuraciones.fecha;
            dtpHasta.MinDate = Configuraciones.fecha;
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
                    conexion.Open();
                    int i = 0;
                    DateTime fecha = dtpDesde.Value;
                    while (fecha <= dtpHasta.Value)
                    {
                        ListViewItem registro = obtenerRegistroParaFecha(fecha);
                        

                        if (registro != null) {
                            DateTime horarioInicio = new DateTime(fecha.Year,fecha.Month,fecha.Day, int.Parse(registro.SubItems[1].Text), int.Parse(registro.SubItems[2].Text),0);
                            DateTime horarioFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, int.Parse(registro.SubItems[3].Text), int.Parse(registro.SubItems[4].Text),0);

                            while (horarioInicio < horarioFin)
                            {
                                SqlCommand crearAgenda = new SqlCommand("CHAMBA.AGREGAR_DISPONIBILIDAD_EN_AGENDA", conexion);

                                crearAgenda.CommandType = CommandType.StoredProcedure;
                                crearAgenda.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = Configuraciones.usuario;
                                crearAgenda.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = decimal.Parse(registro.SubItems[5].Tag.ToString());
                                crearAgenda.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = horarioInicio;

                                crearAgenda.ExecuteNonQuery();
                                horarioInicio = horarioInicio.AddMinutes(30);
                                i++;
                            }                            
                        }

                        fecha = fecha.AddDays(1);
                    }
                    conexion.Close();
                    MessageBox.Show("Agenda registrada exitosamente. Turnos agregados: "+i);
                    this.Close();
                }
            }
        }

        private ListViewItem obtenerRegistroParaFecha(DateTime fecha)
        {

            for(int i = 0; i < lstRangos.Items.Count; i++)
            {
                if (int.Parse(lstRangos.Items[i].Tag.ToString()) == (int)fecha.DayOfWeek)
                {
                    return lstRangos.Items[i];
                }
            }
            return null;

        }
    }
}
