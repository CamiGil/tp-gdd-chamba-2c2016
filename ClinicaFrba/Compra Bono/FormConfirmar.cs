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

namespace ClinicaFrba.Compra_Bono
{
    public partial class FormConfirmar : Form
    {
        SqlConnection conexion;
        decimal afiliado;

        public FormConfirmar()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        public void obtenerDatos(decimal afiliado, decimal cantidad)
        {
            conexion.Open();

            this.afiliado = afiliado;

            SqlCommand precioDeBono = new SqlCommand("CHAMBA.ObtenerPrecioDeBono", conexion);

            precioDeBono.CommandType = CommandType.StoredProcedure;
            precioDeBono.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = afiliado;
            precioDeBono.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;

            var afiliadoNombre = precioDeBono.Parameters.Add("@AfiliadoNombre", SqlDbType.VarChar, 510);
            var unitario = precioDeBono.Parameters.Add("@Unitario", SqlDbType.Int);
            var total = precioDeBono.Parameters.Add("@Total", SqlDbType.Int);
            afiliadoNombre.Direction = ParameterDirection.Output;
            unitario.Direction = ParameterDirection.Output;
            total.Direction = ParameterDirection.Output;

            SqlDataReader data = precioDeBono.ExecuteReader();
            data.Close();

            txtAfiliado.Text = afiliadoNombre.Value.ToString();
            txtCantidad.Text = cantidad.ToString();
            txtUnitario.Text = unitario.Value.ToString();
            txtTotal.Text = total.Value.ToString();

            conexion.Close();
        }

        private void FormConfirmar_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            conexion.Open();


            SqlCommand comprarBonos = new SqlCommand("CHAMBA.ComprarBonos", conexion);

            comprarBonos.CommandType = CommandType.StoredProcedure;
            comprarBonos.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = afiliado;
            comprarBonos.Parameters.Add("@Cantidad", SqlDbType.Int).Value = txtCantidad.Text;
            comprarBonos.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;

            comprarBonos.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Compra efectuada exitosamente");

            this.DialogResult = DialogResult.OK;
        }
    }
}
