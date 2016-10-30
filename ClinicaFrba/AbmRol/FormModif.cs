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
        SqlCommand cargarRoles, habilitado, funcPorRol, existeRol, cambiarNombre, idRol, idFunc, eliminarFunc, asignarFunc, cargarFunc, habilitar;
        SqlDataReader data;
        List<String> funcion = new List<String>();
        List<String> funcionesViejas = new List<String>();
        string rol;

        public FormModif()
        {
            InitializeComponent();
        }

        private void FormModif_Load(object sender, EventArgs e)
        {
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            conexion.Open();

            cargarRoles = new SqlCommand("CHAMBA.CargarRoles", conexion);
            cargarRoles.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cargarRoles);
            DataTable table = new DataTable();
            conexion.Close();
            adapter.Fill(table);
            comboBox2.DataSource = table;
            comboBox2.DisplayMember = "Rol_Nombre";

            cargarFuncionalidades();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rol = comboBox2.Text.ToString();
            int habilitado = estaHabilitado(rol);
            if (habilitado == 0)
            button6.Visible = true;
            else
            button6.Visible = false;
            label3.Visible = true;
            label6.Visible = true;
            textBox1.Visible = true;
            listBox1.Visible = true;
            listBox2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            textBox1.Text = rol;
            button2.Visible = true;

            cargarFuncionalidadesPorRol(rol);
        }

        private int estaHabilitado(String rol)
        {
            conexion.Open();
            habilitado = new SqlCommand("CHAMBA.RolHabilitado", conexion);
            habilitado.CommandType = CommandType.StoredProcedure;
            habilitado.Parameters.Add("@nombre", SqlDbType.VarChar).Value = rol;
            var resultado = habilitado.Parameters.Add("@Valor", SqlDbType.Bit);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = habilitado.ExecuteReader();
            var habi = resultado.Value;
            int respuesta = (int)habi;
            conexion.Close();
            data.Close();
            return respuesta;
        }

        private void cargarFuncionalidadesPorRol(String rol)
        {

            List<String> funcionalidades = new List<string>();
            listBox2.Items.Clear();
            funcionesViejas.Clear();
            funcionalidades.Clear();
            funcion.Clear();
            conexion.Open();
            funcPorRol = new SqlCommand("CHAMBA.FuncionalidadesPorRol", conexion);

            funcPorRol.CommandType = CommandType.StoredProcedure;
            funcPorRol.Parameters.Add("@Rol", SqlDbType.VarChar).Value = rol;

            SqlDataAdapter adapter = new SqlDataAdapter(funcPorRol);
            SqlDataReader reader = funcPorRol.ExecuteReader();

            while (reader.Read())
            {
                funcionalidades.Add(reader.GetString(0)); //Specify column index 
            }



            listBox2.Items.AddRange(funcionalidades.ToArray());
            reader.Close();

            listBox2.DisplayMember = "Func_Descripcion";
            conexion.Close();

            for (int i = 0; i < listBox2.Items.Count; i++)
            {

                string text = listBox2.GetItemText(listBox2.Items[i]);

                funcion.Add(text);
                funcionesViejas.Add(text);
            }

        }

        private void validarCampos()
        {

            if (string.IsNullOrEmpty(textBox1.Text) || (int)listBox2.Items.Count == 0)
            {
                String mensaje = "Los campos nombre y funcionalidades son obligatorios";
                String caption = "Error al modificar el rol";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

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

                if ((int)existeR == 1 && !(rol.Equals(textBox1.Text)))
                {
                    String mensaje = "El rol ya existe, ingrese otro nombre";
                    String caption = "Error al modificar el rol";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
                else
                    modificarRol();

            }

        }

        private void modificarRol()
        {

            conexion.Open();
            cambiarNombre = new SqlCommand("Chamba.ModificarNombreRol", conexion);
            cambiarNombre.CommandType = CommandType.StoredProcedure;
            cambiarNombre.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
            cambiarNombre.Parameters.Add("@anterior", SqlDbType.VarChar).Value = rol;
            cambiarNombre.ExecuteNonQuery();


            idRol = new SqlCommand("CHAMBA.ObtenerRolId", conexion);
            idRol.CommandType = CommandType.StoredProcedure;
            idRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
            var resultado = idRol.Parameters.Add("@Valor", SqlDbType.Decimal);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = idRol.ExecuteReader();


            var idResult = resultado.Value;
            decimal id = (decimal.Parse(idResult.ToString()));
            data.Close();

            eliminarFunc = new SqlCommand("CHAMBA.EliminarFuncionalidades", conexion);
            eliminarFunc.CommandType = CommandType.StoredProcedure;
            eliminarFunc.Parameters.Add("@rol", SqlDbType.Decimal).Value = id;
            eliminarFunc.ExecuteNonQuery();
            conexion.Close();

            List<decimal> ids = new List<decimal>();


            for (int i = 0; i < funcion.Count(); i++)
            {
                conexion.Open();
                idFunc = new SqlCommand("CHAMBA.ObtenerFuncionalidadId", conexion);
                idFunc.CommandType = CommandType.StoredProcedure;
                idFunc.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = funcion.ElementAt(i).ToString();
                var resultado2 = idFunc.Parameters.Add("@Valor", SqlDbType.Decimal);
                resultado2.Direction = ParameterDirection.ReturnValue;
                data = idFunc.ExecuteReader();
                var id2 = resultado2.Value;
                decimal aniadir = decimal.Parse(id2.ToString());
                ids.Add(aniadir);
                data.Close();
                conexion.Close();
            }


            for (int i = 0; i < ids.Count(); i++)
            {

                conexion.Open();
                asignarFunc = new SqlCommand("CHAMBA.AsignarFuncionalidad", conexion);
                asignarFunc.CommandType = CommandType.StoredProcedure;
                asignarFunc.Parameters.Add("@idRol", SqlDbType.Decimal).Value = id;
                asignarFunc.Parameters.Add("@idFunc", SqlDbType.Decimal).Value = ids.ElementAt(i);
                asignarFunc.ExecuteNonQuery();
                conexion.Close();

            }

            String mensaje = "El rol se ha modificado correctamente";
            String caption = "Rol modificado";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            this.Close();

        }

        private void cargarFuncionalidades()
        {
            conexion.Open();
            cargarFunc = new SqlCommand("CHAMBA.CargarFuncionalidades", conexion);
            cargarFunc.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cargarFunc);
            DataTable table = new DataTable();
            adapter.Fill(table);
            SqlDataReader reader = cargarFunc.ExecuteReader();
            listBox1.DataSource = table;
            listBox1.DisplayMember = "Func_Descripcion";
            conexion.Close();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            string text = listBox2.GetItemText(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);

            funcion.Remove(text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conexion.Open();
            habilitar = new SqlCommand("CHAMBA.HabilitarRol", conexion);
            habilitar.CommandType = CommandType.StoredProcedure;
            habilitar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
            habilitar.ExecuteNonQuery();
            conexion.Close();

            String mensaje = "El rol ha sido habilitado";
            String caption = "Rol modificado";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            button6.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarCampos();
            cleanForm();
        }

        private void cleanForm()
        {
            comboBox2.SelectedIndex = -1;
            button6.Visible = false;
            button6.Visible = false;
            label3.Visible = false;
            label6.Visible = false;
            textBox1.Clear();
            textBox1.Visible = false;
            listBox1.Visible = false;
            listBox2.Items.Clear();
            listBox2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = false;

        }



    }
}
