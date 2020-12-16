using Ransa.Entidades.Comun;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.GestionCita
{
    
    public class ConsultaCita
    {
        /// <summary>
        /// Numero de ID.
        /// </summary>
        [DBField("NUMID")]
        public string NUMID { get; set; }
        /// <summary>
        /// Numero de cita.
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }

        /// <summary>
        /// Orden Servicio.
        /// </summary>
        [DBField("NORSRN")]
        public decimal NORSRN { get; set; }
        /// <summary>
        /// Numero de booking.
        /// </summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }
        /// <summary>
        /// Fecha Cita
        /// </summary>
        [DBField("FECCITA")]
        public string FECCITA { get; set; }
        /// <summary>
        /// Hora Cita
        /// </summary>
        [DBField("HORCITA")]
        public decimal HORCITA { get; set; }
        /// <summary>
        /// Cita Anterior
        /// </summary>
        [DBField("CITAANT")]
        public string CITAANT { get; set; }
        /// <summary>
        /// Estado Cita
        /// </summary>
        [DBField("ESTCITA")]
        public string ESTCITA { get; set; }
        /// <summary>
        /// Flag Reprogramacion
        /// </summary>
        [DBField("FLGREP")]
        public string FLGREP { get; set; }
        /// <summary>
        /// Observacion Cita
        /// </summary>
        [DBField("OBSCITA")]
        public string OBSCITA { get; set; }
        /// <summary>
        /// NroCont
        /// </summary>
        [DBField("NROCON")]
        public string NROCON { get; set; }
        /// <summary>
        /// NaveViaje
        /// </summary>
        [DBField("NAVVIAJE")]
        public string NAVVIAJE { get; set; }
        /// <summary>
        /// IQBF
        /// </summary>
        [DBField("IQBF")]
        public string IQBF { get; set; }
        /// <summary>
        /// LAR
        /// </summary>
        [DBField("LAR")]
        public string LAR { get; set; }
        /// <summary>
        /// ROL
        /// </summary>
        [DBField("ROL")]
        public string ROL { get; set; }
        /// <summary>
        /// NROPLACA
        /// </summary>
        [DBField("NROPLACA")]
        public string NROPLACA { get; set; }
        /// <summary>
        /// ALERT
        /// </summary>
        [DBField("ALERT")]
        public string ALERT { get; set; }
        /// <summary>
        /// DOCCHFR
        /// </summary>
        [DBField("DOCCHFR")]
        public string DOCCHFR { get; set; }
        /// <summary>
        /// NROCONPLAN
        /// </summary>
        [DBField("NROCONPLAN")]
        public string NROCONPLAN { get; set; }
        

    }
    public class ConsultaCitaQueryInput
    {
        /// <summary>
        /// numero de ID
        /// </summary>
        [DBParameter("NUMID", DBDataType.VarChar)]
        public string NUMID { get; set; }
        /// <summary>
        /// numero de orden de servicio
        /// </summary>
        [DBParameter("NORSRN", DBDataType.Numeric)]
        public decimal NORSRN { get; set; }

        /// <summary>
        /// numero de booking
        /// </summary>
        [DBParameter("NUMBKG", DBDataType.VarChar)]
        public string NUMBKG { get; set; }

        /// <summary>
        /// fecha desde
        /// </summary>
        [DBParameter("FECDESDE", DBDataType.Numeric)]
        public decimal FECDESDE { get; set; }

        /// <summary>
        /// fecha hasta
        /// </summary>
        [DBParameter("FECHASTA", DBDataType.Numeric)]
        public decimal FECHASTA { get; set; }

        /// <summary>
        /// numero de contenedor
        /// </summary>
        [DBParameter("NROCON", DBDataType.VarChar)]
        public string NROCON { get; set; }

        /// <summary>
        /// Estado Cita
        /// </summary>
        [DBParameter("ESTCITA", DBDataType.VarChar)]
        public string ESTCITA { get; set; }
        /// <summary>
        /// Operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        /// <summary>
        /// Accion
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }
    public class ValidaCitaQueryInput
    {
        /// <summary>
        /// numero de cita
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }
        /// <summary>
        /// Operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        /// <summary>
        /// ACCION
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class ValidaCita
    {

        /// <summary>
        /// Numero de placa.
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
    }
    public class CargarCitaQueryInput : AuditoriaParam
    {




        /// <summary>
        /// Id de Cita
        /// </summary>
        [DBParameter("NUMID", DBDataType.VarChar)]
        public string NUMID { get; set; }

        /// <summary>
        /// numero de cita
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }
        //NORSRN NUMERIC(10,0),	
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("NORSRN", DBDataType.Numeric)]
        public decimal NORSRN { get; set; }
        //NUMBKG VARCHAR(25),
        /// <summary>
        /// booking
        /// </summary>
        [DBParameter("NUMBKG", DBDataType.VarChar)]
        public string NUMBKG { get; set; }
        //FECCITA NUMERIC(8,0),
        /// <summary>
        /// Fecha cita
        /// </summary>
        [DBParameter("FECCITA", DBDataType.Numeric)]
        public decimal FECCITA { get; set; }
        //HORCITA NUMERIC(6,0),
        /// <summary>
        /// Hora cita
        /// </summary>
        [DBParameter("HORCITA", DBDataType.VarChar)]
        public string HORCITA { get; set; }

        /// <summary>
        /// Cita Anterior
        /// </summary>
        [DBParameter("CITAANT", DBDataType.VarChar)]
        public string CITAANT { get; set; }
        //ESTCITA VARCHAR(1),
        /// <summary>
        /// Estado Cita
        /// </summary>
        [DBParameter("ESTCITA", DBDataType.VarChar)]
        public string ESTCITA { get; set; }
        //FLGREP VARCHAR(1),
        /// <summary>
        /// Flag reprogramacion
        /// </summary>
        [DBParameter("FLGREP", DBDataType.VarChar)]
        public string FLGREP { get; set; }
        //OBSCITA VARCHAR(250),
        /// <summary>
        /// observaciones cinta
        /// </summary>
        [DBParameter("OBSCITA", DBDataType.VarChar)]
        public string OBSCITA { get; set; }
        //OPEPORT VARCHAR(15),
        /// <summary>
        /// Ruc operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        //OPEPORT VARCHAR(15),
        /// <summary>
        /// numero contenedor
        /// </summary>
        [DBParameter("NROCON", DBDataType.VarChar)]
        public string NROCON { get; set; }
        /// <summary>
        /// numero contenedor  planificado
        /// </summary>
        [DBParameter("NROCONPLAN", DBDataType.VarChar)]
        public string NROCONPLAN { get; set; }


    }
}
