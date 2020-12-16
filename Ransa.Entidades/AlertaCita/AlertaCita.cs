using Ransa.Entidades.Comun;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.AlertaCita
{
    public class GetAlertaCitas
    {
        /// <summary>
        /// TIPO ALERTA
        /// </summary>
        [DBField("TIPALERT")]
        public string TIPALERT { get; set; }
        /// <summary>
        /// DESC ALERTA
        /// </summary>
        [DBField("DESCALERT")]
        public string DESCALERT { get; set; }
        /// <summary>
        /// MENSAJE ALERTA
        /// </summary>
        [DBField("MNSJALERT")]
        public string MNSJALERT { get; set; }
        /// <summary>
        /// NAVE VIAJE
        /// </summary>
        [DBField("NAVVIAJE")]
        public string NAVVIAJE { get; set; }
        /// <summary>
        /// BOOKING
        /// </summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }
        /// <summary>
        /// CONTENEDOR
        /// </summary>
        [DBField("NROCON")]
        public string NROCON { get; set; }
        /// <summary>
        /// PLACA
        /// </summary>
        [DBField("NROPLACA")]
        public string NROPLACA { get; set; }
        /// <summary>
        /// ROLEADO
        /// </summary>
        [DBField("ROLEADO")]
        public string ROLEADO { get; set; }
        /// <summary>
        /// NUMCITA
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
        /// <summary>
        /// FECCITA
        /// </summary>
        [DBField("FECCITA")]
        public string FECCITA { get; set; }
        /// <summary>
        /// ESTCITA
        /// </summary>
        [DBField("ESTCITA")]
        public string ESTCITA { get; set; }

    }
    public class GetAlertaCitasQueryInput
    {
        /// <summary>
        /// numero de ID
        /// </summary>
        [DBParameter("NUMID", DBDataType.VarChar)]
        public string NUMID { get; set; }
        /// <summary>
        /// numero de ORDEN DE SERVICIO
        /// </summary>
        [DBParameter("NORSRN", DBDataType.Numeric)]
        public decimal NORSRN { get; set; }
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
        /// numero de CITA
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }
        /// <summary>
        /// numero de booking
        /// </summary>
        [DBParameter("NUMBKG", DBDataType.VarChar)]
        public string NUMBKG { get; set; }
        /// <summary>
        /// numero de contenedor
        /// </summary>
        [DBParameter("NROCON", DBDataType.VarChar)]
        public string NROCON { get; set; }
        /// <summary>
        /// operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        /// <summary>
        /// ACCION
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class EnvioAlertaQueryInput : AuditoriaParam
    {

        /// <summary>
        /// NUMID04
        /// </summary>
        [DBParameter("NUMID04", DBDataType.VarChar)]
        public string NUMID04 { get; set; }
        
        /// <summary>
        /// NUMID01
        /// </summary>
        [DBParameter("NUMID01", DBDataType.VarChar)]
        public string NUMID01 { get; set; }
        
        /// <summary>
        /// NUMCITA
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }
        
        /// <summary>
        /// Tipo alerta
        /// </summary>
        [DBParameter("HRSVEN", DBDataType.VarChar)]
        public string HRSVEN { get; set; }
        
        /// <summary>
        /// Tipo alerta
        /// </summary>
        [DBParameter("TIPALERT", DBDataType.VarChar)]
        public string TIPALERT { get; set; } 
         /// <summary>
        /// OPEPORT
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
         public string OPEPORT { get; set; }
        
    }
    public class TiempoAlertaQueryInput
    {
        /// <summary>
        /// Tipo alerta
        /// </summary>
        [DBParameter("TIPALERT", DBDataType.VarChar)]
        public string TIPALERT { get; set; }
        /// <summary>
        /// Desc alerta
        /// </summary>
        [DBParameter("DESCALERT", DBDataType.VarChar)]
        public string DESCALERT { get; set; }
        /// <summary>
        /// Operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
    public class TiempoAlerta
    {
        /// <summary>
        /// Tipo de alerta
        /// </summary>
        [DBField("TIPALERT")]
        public string TIPALERT { get; set; }
        /// <summary>
        /// Desc de alerta
        /// </summary>
        [DBField("DESCALERT")]
        public string DESCALERT { get; set; }
        /// <summary>
        /// Tiempo de alerta
        /// </summary>
        [DBField("TEMPALERT")]
        public decimal TEMPALERT { get; set; }
    }
    public class CitasPendientesAlertQueryInput
    {
       
        /// <summary>
        /// Operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
       
    }
    public class CitasPendientesAlert
    {
        /// <summary>
        /// Numero de ID.
        /// </summary>
        [DBField("NUMID")]
        public string NUMID { get; set; }
        /// <summary>
        /// Numero de CITA.
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
        /// <summary>
        /// Numero de BOOKING.
        /// </summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }
        
        /// <summary>
        /// Numero de TIPO DE ALERTA (DRY, REEFER).
        /// </summary>
        [DBField("DESCALERT")]
        public string DESCALERT { get; set; }
        
        /// <summary>
        /// FECHA CUT OFF.
        /// </summary>
        [DBField("FCOFF")]
        public decimal FCOFF { get; set; }
        
             /// <summary>
        /// HORA CUT OFF
        /// </summary>
        [DBField("HCOFF")]
        public decimal HCOFF { get; set; }
        /// <summary>
        /// TIPO ALERTA ('PRE','POS') EN CASO TUVIERA UNA ANTERIOR
        /// </summary>
        [DBField("TIPALERT")]
        public string TIPALERT { get; set; }
        /// <summary>
        /// FECHA ULTIMA ALERTA
        /// </summary>
        [DBField("FECREG")]
        public decimal FECREG { get; set; }
        /// <summary>
        /// HORA ULTIMA ALERTA
        /// </summary>
        [DBField("HRSREG")]
        public decimal HRSREG { get; set; }
    }
    public class CitasPendientesPrimeraAlert
    {
        /// <summary>
        /// Numero de ID.
        /// </summary>
        [DBField("NUMID")]
        public string NUMID { get; set; }
        /// <summary>
        /// Numero de CITA.
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
        /// <summary>
        /// Numero de BOOKING.
        /// </summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }

        /// <summary>
        /// Numero de TIPO DE ALERTA (DRY, REEFER).
        /// </summary>
        [DBField("DESCALERT")]
        public string DESCALERT { get; set; }

        /// <summary>
        /// FECHA CUT OFF.
        /// </summary>
        [DBField("FCOFF")]
        public decimal FCOFF { get; set; }

        /// <summary>
        /// HORA CUT OFF
        /// </summary>
        [DBField("HCOFF")]
        public decimal HCOFF { get; set; }

    }
}
