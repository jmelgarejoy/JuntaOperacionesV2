using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.GestionCita
{
    public class OrdenServicioQueryInput
    {
        /// <summary>
        /// Booking
        /// </summary>
        [DBParameter("NBKNCN", DBDataType.VarChar)]
        public string NBKNCN { get; set; }
        /// <summary>
        /// ACCION
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class OrdenServicio
    {

        /// <summary>
        /// Numero de placa.
        /// </summary>
        [DBField("NORSRN")]
        public decimal NORSRN { get; set; }
    }
}

