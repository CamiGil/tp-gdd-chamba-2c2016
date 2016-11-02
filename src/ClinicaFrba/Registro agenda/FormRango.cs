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

    public partial class FormRango : Form
    {
        ListView lstRangos;
        SqlConnection conexion;
        public FormRango(ListView lstRangos)
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.lstRangos = lstRangos;
        }

        private void FormRango_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> dias = new Dictionary<int, string>();
            for (int i = 1; i <= 6; i++)
            {
                bool existe = false;
                for (int j = 0; j < lstRangos.Items.Count; j++)
                {
                    if (lstRangos.Items[j].Text == getDia(i))
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    dias.Add(i, getDia(i));
                }
            }
            if (dias.Count == 0)
            {
                MessageBox.Show("Ya ha cargado rangos para todos los dias");
                this.Close();
            }
            else
            {
                cboDia.DataSource = new BindingSource(dias, null);
                cboDia.DisplayMember = "Value";
                cboDia.ValueMember = "Key";
            }
            

            conexion.Open();
            String query = "SELECT DISTINCT Espe_Codigo, Espe_Descripcion FROM CHAMBA.Especialidades "+
                "JOIN CHAMBA.Tipo_Especialidad ON Tipo_Espe_Especialidad = Espe_Codigo " +
                "JOIN CHAMBA.Tipo_Especialidad_X_Profesional ON Tipo_Espe_X_Prof_Tipo_Especialidad = Tipo_Espe_Codigo "+
                "JOIN CHAMBA.Profesionales ON Tipo_Espe_Especialidad = Espe_Codigo " +
                "WHERE Tipo_Espe_X_Prof_Profesional = " + Configuraciones.usuario + " " +
                "ORDER BY Espe_Descripcion";

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            cboEspecialidad.DataSource = tabla;
            cboEspecialidad.DisplayMember = "Espe_Descripcion";
            cboEspecialidad.ValueMember = "Espe_Codigo";

            conexion.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cboDia.Text != "" && cboHoraDesde.Text != "" && cboHoraHasta.Text != "" && cboMinutoDesde.Text != "" && cboMinutoHasta.Text != "" && cboEspecialidad.Text != "")
            {
                if (horasValidas())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = cboDia.Text;
                    item.Tag = cboDia.SelectedValue;
                    item.SubItems.Add(cboHoraDesde.Text);
                    item.SubItems.Add(cboMinutoDesde.Text);
                    item.SubItems.Add(cboHoraHasta.Text);
                    item.SubItems.Add(cboMinutoHasta.Text);
                    item.SubItems.Add(cboEspecialidad.Text).Tag = cboEspecialidad.SelectedValue;
                    lstRangos.Items.Add(item);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("El rango de horarios no es valido. Verifique que se encuentre dentro del rango de atencion y que el horario de inicio no sea mayor al de fin");
                }
            }
            else
            {
                MessageBox.Show("Complete todos los datos solicitados");
            }
            
        }

        private bool horasValidas()
        {
            if (cboDia.Text == "Sabado")
            {
                if (int.Parse(cboHoraDesde.Text) < 10)
                {
                    return false;
                }
                if (int.Parse(cboHoraHasta.Text) > 15 || int.Parse(cboHoraDesde.Text) == 15 && int.Parse(cboMinutoHasta.Text) > 0)
                {
                    return false;
                }
            }
            else
            {
                if (int.Parse(cboHoraDesde.Text) < 7)
                {
                    return false;
                }
                if (int.Parse(cboHoraHasta.Text) > 20 || int.Parse(cboHoraDesde.Text) == 20 && int.Parse(cboMinutoHasta.Text) > 0)
                {
                    return false;
                }
            }

            if (int.Parse(cboHoraDesde.Text) > int.Parse(cboHoraHasta.Text))            
                return false;
            
            else if (int.Parse(cboHoraDesde.Text) == int.Parse(cboHoraHasta.Text) && int.Parse(cboMinutoDesde.Text) >= int.Parse(cboMinutoHasta.Text))
                return false;

            return true;
        }

        private String getDia(int dia)
        {
            switch (dia)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miercoles";
                case 4:
                    return "Jueves";
                case 5:
                    return "Viernes";
                default:
                    return "Sabado";
            }
        }
    }
}
