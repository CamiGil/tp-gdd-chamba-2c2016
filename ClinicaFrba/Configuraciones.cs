﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClinicaFrba
{
    class Configuraciones
    {
        public static FormLogin formLogin;
        public static String datosConexion = "Data Source=localhost\\SQLSERVER2012;Initial Catalog=GD2C2016;Persist Security Info=True;User ID=gd;Password=gd2016";
        public static int cantMaxIntentosLogin = 3;
        public static int usuario;
        public static int rol;
    }
}
