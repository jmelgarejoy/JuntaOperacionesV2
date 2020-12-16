using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.JuntaOperativa
{
    //public class DashBoard
    //{
    //    /// <summary>
    //    /// ID Junta de Operaciones
    //    /// </summary>
    //    [DBField("IDJTAOPE")]
    //    public string IDJTAOPE { get; set; }
    //    /// <summary>
    //    /// Orden de Servicio
    //    /// </summary>
    //    [DBField("ORDEN")]
    //    public decimal ORDEN { get; set; }
    //    /// <summary>
    //    /// COD NAVE
    //    /// </summary>
    //    [DBField("CODNAVE")]
    //    public string CODNAVE { get; set; }
    //    /// <summary>
    //    /// nombre NAVE
    //    /// </summary>
    //    [DBField("NOMNAVE")]
    //    public string NOMNAVE { get; set; }
    //    /// <summary>
    //    /// Tipo Planificacion; I=Importación, E=Exportación
    //    /// </summary>
    //    [DBField("TIPOPLAN")]
    //    public string TIPOPLAN { get; set; }
    //    /// <summary>
    //    /// Contenedor
    //    /// </summary>
    //    [DBField("CONTENE")]
    //    public string CONTENE { get; set; }
    //    /// <summary>
    //    /// Operador Portuario
    //    /// </summary>
    //    [DBField("OPEPORTU")]
    //    public string OPEPORTU { get; set; }
    //    /// <summary>
    //    /// Tipo de Contenedor
    //    /// </summary>
    //    [DBField("TIPOCONT")]
    //    public string TIPOCONT { get; set; }
    //    /// <summary>
    //    /// Clase de Contenedor
    //    /// </summary>
    //    [DBField("CLASE")]
    //    public string CLASE { get; set; }
    //    /// <summary>
    //    /// Tamaño de Contenedor
    //    /// </summary>
    //    [DBField("TAMANIO")]
    //    public string TAMANIO { get; set; }
    //    /// <summary>
    //    /// Refrigerado? SI/NO
    //    /// </summary>
    //    [DBField("REFRIGER")]
    //    public string REFRIGER { get; set; }
    //    /// <summary>
    //    /// Fecha inicio Planificación
    //    /// </summary>
    //    [DBField("FCINPLN")]
    //    public decimal FCINPLN { get; set; }
    //    /// <summary>
    //    /// Hora inicio Planificación
    //    /// </summary>
    //    [DBField("HORAINI")]
    //    public decimal HORAINI { get; set; }
    //    /// <summary>
    //    /// Fecha Fin de Operación.
    //    /// </summary>
    //    [DBField("FECFINOPERA")]
    //    public decimal FECFINOPERA { get; set; }
    //    /// <summary>
    //    /// Hora fin de operación.
    //    /// </summary>
    //    [DBField("HORFINOPERA")]
    //    public decimal HORFINOPERA { get; set; }
    //    /// <summary>
    //    /// Fecha Fin de Descarga.
    //    /// </summary>
    //    [DBField("FECFINDESCA")]
    //    public decimal FECFINDESCA { get; set; }
    //    /// <summary>
    //    /// Hora fin de descarga.
    //    /// </summary>
    //    [DBField("HORFINDESCA")]
    //    public decimal HORFINDESCA { get; set; }
    //    /// <summary>
    //    /// Fecha Cutoff NAve
    //    /// </summary>
    //    [DBField("FECCUTOFF")]
    //    public decimal FECCUTOFF { get; set; }
    //    /// <summary>
    //    /// Hora Cutoff de Nave.
    //    /// </summary>
    //    [DBField("HORCUTOFF")]
    //    public decimal HORCUTOFF { get; set; }
    //    /// <summary>
    //    /// Guia de Ingreso.
    //    /// </summary>
    //    [DBField("GUIAINGRESO")]
    //    public decimal GUIAINGRESO { get; set; }
    //    /// <summary>
    //    /// Guia de Salida.
    //    /// </summary>
    //    [DBField("GUIASALIDA")]
    //    public decimal GUIASALIDA { get; set; }
    //}
    public class DashBoardQueryInput
    {
        /// <summary>
        /// Fecha Planificación
        /// </summary>
        [DBParameter("FECHA", DBDataType.VarChar)]
        public string FECHA { get; set; }
        /// <summary>
        /// Accion T=Todos, F= por Fecha
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }

    public class DashBoard
    {
        /// <summary>
        /// COD NAVE
        /// </summary>
        [DBField("CODNAVE")]
        public string CODNAVE { get; set; }
        /// <summary>
        /// nombre NAVE
        /// </summary>
        [DBField("NOMNAVE")]
        public string NOMNAVE { get; set; }
        /// <summary>
        /// Numero de Viaje
        /// </summary>
        [DBField("NVJES")]
        public decimal NVJES { get; set; }
        /// <summary>
        /// Año de manifiesto
        /// </summary>
        [DBField("AMNFS")]
        public decimal AMNFS { get; set; }
        /// <summary>
        /// Numero de Manifiesto
        /// </summary>
        [DBField("NMNFEN")]
        public decimal NMNFEN { get; set; }
        /// <summary>
        /// Manifestado Exportacion
        /// </summary>
        [DBField("MANEXPO")]
        public decimal MANEXPO { get; set; }
        /// <summary>
        /// Enviados Exportación
        /// </summary>
        [DBField("ENVIEXPO")]
        public int ENVIEXPO { get; set; }
        /// <summary>
        /// Manifestado Importación
        /// </summary>
        [DBField("MANIMPO")]
        public int MANIMPO { get; set; }
        /// <summary>
        /// Recibidos Importación
        /// </summary>
        [DBField("RECIIMPO")]
        public int RECIIMPO { get; set; }
        /// <summary>
        /// Fecha fin de Operación.
        /// </summary>
        [DBField("FECFINOPERA")]
        public decimal FECFINOPERA { get; set; }
        /// <summary>
        /// Hora fin de Operación.
        /// </summary>
        [DBField("HORFINOPERA")]
        public decimal HORFINOPERA { get; set; }
        /// <summary>
        /// Fecha fin de Descarga.
        /// </summary>
        [DBField("FECFINDESCA")]
        public decimal FECFINDESCA { get; set; }
        /// <summary>
        /// Hora fin de Descarga.
        /// </summary>
        [DBField("HORFINDESCA")]
        public decimal HORFINDESCA { get; set; }
        /// <summary>
        /// Fecha Cutoff.
        /// </summary>
        [DBField("FECCUTOFF")]
        public decimal FECCUTOFF { get; set; }
        /// <summary>
        /// Hora Cutoff.
        /// </summary>
        [DBField("HORCUTOFF")]
        public decimal HORCUTOFF { get; set; }
        /// <summary>
        /// estado
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
    }
    public class DashBoardGroup
    {
        /// <summary>
        /// Cantidad de Naves
        /// </summary>
        [DBField("CANTNAVE")]
        public decimal CANTNAVE { get; set; }
        /// <summary>
        /// Manifestado Exportacion
        /// </summary>
        [DBField("MANEXPO")]
        public decimal MANEXPO { get; set; }
        /// <summary>
        /// Enviados Exportación
        /// </summary>
        [DBField("ENVIEXPO")]
        public int ENVIEXPO { get; set; }
        /// <summary>
        /// Manifestado Importación
        /// </summary>
        [DBField("MANIMPO")]
        public int MANIMPO { get; set; }
        /// <summary>
        /// Recibidos Importación
        /// </summary>
        [DBField("RECIIMPO")]
        public int RECIIMPO { get; set; }
        
    }

    public class DashBoardGeneral
    {
        public List<DashBoardGroup> Global { get; set; }
        public List<DashBoard> Diario { get; set; }
    }

}
