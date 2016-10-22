﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Listados
{
    public partial class FormProfesionalesConsultados : Form
    {
        SqlConnection conexion;
        public FormProfesionalesConsultados()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormProfesionalesConsultados_Load(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand cargar = new SqlCommand("SELECT Plan_Descripcion, Plan_Codigo FROM CHAMBA.Planes", conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cboPlan.DataSource = table;
            cboPlan.DisplayMember = "Plan_Descripcion";
            cboPlan.ValueMember = "Plan_Codigo";


            cargar = new SqlCommand("SELECT Espe_Descripcion, Espe_Codigo FROM CHAMBA.Especialidades ORDER BY Espe_Descripcion", conexion);
            adapter = new SqlDataAdapter(cargar);
            table = new DataTable();
            adapter.Fill(table);
            cboEspecialidad.DataSource = table;
            cboEspecialidad.DisplayMember = "Espe_Descripcion";
            cboEspecialidad.ValueMember = "Espe_Codigo";

            conexion.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboAño.Text != "" && cboEspecialidad.Text != "" && cboPlan.Text != "" && cboMes.Text != "")
            {
                conexion.Open();
                SqlCommand cargar = new SqlCommand("CHAMBA.ProfesionalesConsultados", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("@Plan", SqlDbType.Decimal).Value = cboPlan.SelectedValue;
                cargar.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = cboEspecialidad.SelectedValue;
                cargar.Parameters.Add("@Mes", SqlDbType.Int).Value = cboMes.SelectedValue;
                cargar.Parameters.Add("@Año", SqlDbType.VarChar).Value = cboAño.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cargar);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                conexion.Close();
            }
            else
            {
                MessageBox.Show("Complete los criterios de busqueda");
            
            }
        }

        private void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, string> meses = new Dictionary<int, string>();
            if (cboSemestre.Text == "1")
            {

                meses.Add(1, "Enero");
                meses.Add(2, "Febrero");
                meses.Add(3, "Marzo");
                meses.Add(4, "Abril");
                meses.Add(5, "Mayo");
                meses.Add(6, "Junio");

            }
            else
            {
                meses.Add(7, "Julio");
                meses.Add(8, "Agosto");
                meses.Add(9, "Septiembre");
                meses.Add(10, "Octubre");
                meses.Add(11, "Noviembre");
                meses.Add(12, "Diciembre");
            }
            cboMes.DataSource = new BindingSource(meses, null);
            cboMes.DisplayMember = "Value";
            cboMes.ValueMember = "Key";
        }
    }
}
