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
    public partial class FormCrear : Form
    {
        SqlConnection conexion;
        SqlCommand cargarFuncionalidades, existeRol, crearRolNuevo, RolId, FuncId, asignarFunc;
        SqlDataReader data;
        decimal rol = 0;
        List<String> funcion = new List<String>();

        public FormCrear()
        {
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            InitializeComponent();
        }

        private void FormCrear_Load(object sender, EventArgs e)
        {
            conexion.Open();
            cargarFuncionalidades = new SqlCommand("CHAMBA.CargarFuncionalidades", conexion);
            cargarFuncionalidades.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cargarFuncionalidades);
            DataTable table = new DataTable();
            adapter.Fill(table);
            SqlDataReader reader = cargarFuncionalidades.ExecuteReader();
            listBox1.DataSource = table;
            listBox1.DisplayMember = "Func_Descripcion";
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Boolean validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || (int)listBox2.Items.Count == 0)
            {
                String mensaje = "Los campos nombre y funcionalidades son obligatorios";
                String caption = "Error al crear el rol";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                return false;
            }
            else
            {
                conexion.Open();
                existeRol = new SqlCommand("CHAMBA.ExisteRol", conexion);
                existeRol.CommandType = CommandType.StoredProcedure;
                existeRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = existeRol.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = existeRol.ExecuteReader(); 
                var existeR = resultado.Value;
                data.Close();
                conexion.Close();

                if ((int)existeR == 1)
                {
                    String mensaje = "El rol ya existe, ingrese otro nombre";
                    String caption = "Error al crear el rol";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;
                }
                else
                   return true;
            }
        }

        private void crearNuevoRol()
        {
            conexion.Open();
            crearRolNuevo = new SqlCommand("CHAMBA.CrearRolNuevo", conexion);
            crearRolNuevo.CommandType = CommandType.StoredProcedure;
            crearRolNuevo.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
            crearRolNuevo.ExecuteNonQuery();


            RolId = new SqlCommand("CHAMBA.ObtenerRolId", conexion);
            RolId.CommandType = CommandType.StoredProcedure;
            RolId.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
            var resultado = RolId.Parameters.Add("@Valor", SqlDbType.Decimal);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = RolId.ExecuteReader(); 
            conexion.Close();

            var idRol = resultado.Value;
            rol = decimal.Parse(idRol.ToString());
            data.Close(); 

            crearFuncionalidades();

        }

        private void crearFuncionalidades()
        {

            List<decimal> ids = new List<decimal>();


            for (int i = 0; i < funcion.Count(); i++)
            {
                conexion.Open();
                FuncId = new SqlCommand("CHAMBA.ObtenerFuncionalidadId", conexion);
                FuncId.CommandType = CommandType.StoredProcedure;
                FuncId.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = funcion.ElementAt(i).ToString();
                var resultado = FuncId.Parameters.Add("@Valor", SqlDbType.Decimal);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = FuncId.ExecuteReader();
                var id = resultado.Value;
                decimal aniadir = decimal.Parse(id.ToString());
                ids.Add(aniadir);
                data.Close();
                conexion.Close();
            }

            for (int i = 0; i < ids.Count(); i++)
            {

                conexion.Open();
                asignarFunc = new SqlCommand("CHAMBA.AsignarFuncionalidad", conexion);
                asignarFunc.CommandType = CommandType.StoredProcedure;
                asignarFunc.Parameters.Add("@idRol", SqlDbType.Decimal).Value = rol;
                asignarFunc.Parameters.Add("@idFunc", SqlDbType.Decimal).Value = ids.ElementAt(i);
                asignarFunc.ExecuteNonQuery();
                conexion.Close();

            }

            rol = 0;
            String mensaje = "El rol se ha creado exitosamente";
            String caption = "Rol creado";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            this.Close();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                crearNuevoRol();
                cleanForm();
            }
        }

        private void cleanForm()
        {
            textBox1.Clear();
            listBox2.Items.Clear();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            if (funcion.Contains(text))
            {

                String mensaje = "Esta funcionalidad ya ha sido ingresada";
                String caption = "Funcionalidad duplicada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                listBox2.DisplayMember = "Func_Descripcion";
                listBox2.Items.Add((DataRowView)listBox1.SelectedItem);

                funcion.Add(text);

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string text = listBox2.GetItemText(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);

            funcion.Remove(text);
        }
    }
}
