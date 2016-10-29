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

namespace ClinicaFrba
{
    public partial class FormMain : Form
    {

        SqlConnection conexion;
        int ProximaPosicionEnX = 0, ProximaPosicionEnY = 0;

        public FormMain()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            conexion.Open();

            cargarUsuario();            

            cargarFuncionalidades();

            conexion.Close();           
            
        }

        private void cargarUsuario()
        {
            String query = "SELECT Usua_Nombre, Usua_Apellido FROM CHAMBA.Usuarios WHERE Usua_Id = " + Configuraciones.usuario;

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            if (tabla.Rows.Count == 1)
            {
                lblUsuario.Text = "Usuario: " + tabla.Rows[0]["Usua_Nombre"].ToString() + " " + tabla.Rows[0]["Usua_Apellido"].ToString();
            }
        }

        private void cargarFuncionalidades(){
            String query = "SELECT Func_Id, Func_Descripcion FROM CHAMBA.Funcionalidades JOIN CHAMBA.Funcionalidad_X_Rol ON Func_Id = Func_X_Rol_Funcionalidad WHERE Func_X_Rol_Rol = " + Configuraciones.rol;

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                panelControles.Controls.Add(crearBoton(tabla.Rows[i]["Func_Descripcion"].ToString(), i, decimal.Parse(tabla.Rows[i]["Func_Id"].ToString())));
            }
        }

        private Button crearBoton(String descripcion, int numero, decimal funcionalidad)
        {
            Button boton = new Button();
            boton.Text = descripcion;
            boton.Font = new Font("Tahoma", 14);
            boton.Width = 260;
            boton.Height = 60;
            boton.Tag = funcionalidad;

            boton.Location = new Point(ProximaPosicionEnX, ProximaPosicionEnY);
            ProximaPosicionEnY += boton.Height;
            if (ProximaPosicionEnY > panelControles.Height - 50)
            {
                ProximaPosicionEnY = 0;
                ProximaPosicionEnX += boton.Width;
            }

            boton.Click += new EventHandler(this.abrirABM);
            return boton;
        }

        private void abrirABM(object sender, System.EventArgs e)
        {
            Button botonClickeado = sender as Button;

            switch ((int.Parse(botonClickeado.Tag.ToString())))
            {
                case 1:
                    //ABRIR ABM ROLES
                    break;
                case 2:
                    //ABRIR ABM AFILIADOS
                    Abm_Afiliado.FormAfiliados form2 = new Abm_Afiliado.FormAfiliados();
                    form2.ShowDialog();
                    break;
                case 3:
                    //ABRIR COMPRA BONOS
                    Compra_Bono.FormCompraBonos form3 = new Compra_Bono.FormCompraBonos();
                    form3.ShowDialog();
                    break;
                case 4:
                    //ABRIR PEDIDO DE TURNOS
                    Pedir_Turno.FormSeleccionProf form4 = new Pedir_Turno.FormSeleccionProf();
                    form4.ShowDialog();
                    break;
                case 5:
                    //ABRIR REGISTRO DE LLEGADA
                    Registro_Llegada.FormRegistroLlegada form5 = new Registro_Llegada.FormRegistroLlegada();
                    form5.ShowDialog();
                    break;
                case 6:
                    //ABRIR REGISTRO DE RESULTADOS
                    break;
                case 7:
                    //ABRIR CANCELACION DE TURNOS
                    break;
                case 8:
                    //ABRIR ESTADISTICAS
                    Listados.FormEstadisticas form8 = new Listados.FormEstadisticas();
                    form8.ShowDialog();
                    break;
                case 9:
                    //ABRIR REGISTRO DE AGENDA
                    Registro_agenda.FormRegistro form9 = new Registro_agenda.FormRegistro();
                    form9.ShowDialog();
                    break;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {            
            Configuraciones.formLogin.Show();
            this.Close();
        }

        private void panelControles_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
