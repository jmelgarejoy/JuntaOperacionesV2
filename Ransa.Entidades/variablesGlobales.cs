using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Ransa.Entidades
{
    public class variablesGlobales
    {

        public static string _esquemaTrabajo = ConfigurationManager.AppSettings["Libreria"];

        public static string _NombreUsuario;
        public static string _correoUsuario;
        public static string _rolUsuario;
        public static string _dptoUsuario;
        public static int _estCuentaUsuario;
        public static string _cuentaUsuario;



    }
}
