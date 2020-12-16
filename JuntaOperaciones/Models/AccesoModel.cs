using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuntaOperaciones.Models
{
    public class AccesoModel
    {
        public int IDUSER { get; set; }
        public int IDMODULO { get; set; }
        public int INSERT { get; set; }
        public int UPDATE { get; set; }
        public int DELETE { get; set; }
        public int EXPORT { get; set; }
        public int PRINT { get; set; }
        public string ESTADO { get; set; }
        public string ACCION { get; set; }

    }
}