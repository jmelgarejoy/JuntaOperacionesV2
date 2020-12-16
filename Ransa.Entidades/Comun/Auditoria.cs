using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.Comun
{
    public class Auditoria
    {
        /// <summary>
        /// Fecha creación
        /// </summary>
        [DBField("FECREG")]
        public string FECREG { get; set; }
        /// <summary>
        /// Hora creación
        /// </summary>
        [DBField("HRSREG")]
        public Nullable<decimal> HRSREG { get; set; }
        /// <summary>
        /// Usuario de creacion
        /// </summary>
        [DBField("USRREG")]
        public string USRREG { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        [DBField("FECMOD")]
        public Nullable<decimal> FECMOD { get; set; }
        /// <summary>
        /// Hora modificacion
        /// </summary>
        [DBField("HRSMOD")]
        public Nullable<decimal> HRSMOD { get; set; }
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        [DBField("USERMOD")]
        public string USERMOD { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("SESTRG")]
        public string SESTRG { get; set; }

    }
    public class AuditoriaParam
    {

        /// <summary>
        /// Fecha creación
        /// </summary>
        [DBParameter("FECREG", DBDataType.VarChar)]
        public string FECREG { get; set; }
        /// <summary>
        /// Hora creación
        /// </summary>
        [DBParameter("HRSREG", DBDataType.VarChar)]
        public string HRSREG { get; set; }
        /// <summary>
        /// Usuario de creacion
        /// </summary>
        [DBParameter("USRREG", DBDataType.VarChar)]
        public string USRREG { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        [DBParameter("FECMOD", DBDataType.VarChar)]
        public string FECMOD { get; set; }
        /// <summary>
        /// Hora modificacion
        /// </summary>
        [DBParameter("HRSMOD", DBDataType.VarChar)]
        public string HRSMOD { get; set; }
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        [DBParameter("USERMOD", DBDataType.VarChar)]
        public string USERMOD { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("SESTRG", DBDataType.VarChar)]
        public string SESTRG { get; set; }
        /// <summary>
        /// Acción
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }



}

