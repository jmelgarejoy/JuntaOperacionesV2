using Ransa.Entidades.Comun;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.GestionCita
{
    public class GetAlertaQueryInput
    {
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
    public class GetAlerta
    {

        ///<summary>Tipo de alerta</summary>
        [DBField("TIPALERT")]
        public string TIPALERT { get; set; }
        ///<summary>descripcion alerta</summary>
        [DBField("DESCALERT")]
        public string DESCALERT { get; set; }
        ///<summary>Tiempo de alerta</summary>
        [DBField("TEMPALERT")]
        public decimal TEMPALERT { get; set; }
    }
    public class AlertaQueryInput: AuditoriaParam
    {
        ///<summary>Tipo de alerta</summary>
        [DBParameter("TIPALERT", DBDataType.VarChar)]
        public string TIPALERT { get; set; }
        ///<summary>descripcion alerta</summary>
        [DBParameter("DESCALERT", DBDataType.VarChar)]
        public string DESCALERT { get; set; }
        ///<summary>Tiempo de alerta</summary>
        [DBParameter("TEMPALERT", DBDataType.Numeric)]
        public decimal TEMPALERT { get; set; }
        ///<summary>Operador portuario</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
   
}
