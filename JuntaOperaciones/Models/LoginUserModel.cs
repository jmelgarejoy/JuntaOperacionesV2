using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JuntaOperaciones.Models
{
    public class LoginUserModel
    {

        public string Usuario { get; set; }
        public string Clave { get; set; }
    }

    public class ErrorUSerModel
    {
        [DisplayName("Error:")]
        public string Mensaje { get; set; }
    }



}
