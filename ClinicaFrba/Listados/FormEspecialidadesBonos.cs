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
    public partial class FormEspecialidadesBonos : Form
    {
        SqlConnection conexion;
        public FormEspecialidadesBonos()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormEspecialidadesBonos_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboAño.Text != "" && cboSemestre.Text != "")
            {
                conexion.Open();
                SqlCommand cargar = new SqlCommand("CHAMBA.EspecialidadesBonos", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("@Semestre", SqlDbType.Int).Value = cboSemestre.Text;
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
    }
}