using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.Reporte
{
    public class GetProcesos
    {
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBField("DESCPROC")]
        public string DESCPROC { get; set; }
    }
    public class GetReportesQueryInput
    {


        ///<summary>Operador portuario</summary>
        [DBParameter("DESCPROC", DBDataType.VarChar)]
        public string DESCPROC { get; set; }
    }
    public class GetReportes
    {


        ///<summary>ID</summary>
        [DBField("IDPROC")]
        public int IDPROC { get; set; }
        ///<summary>descripcion proceso</summary>
        [DBField("DESCPROC")]
        public string DESCPROC { get; set; }
        ///<summary>reporte</summary>
        [DBField("NOMREPORT")]
        public string NOMREPORT { get; set; }
        ///<summary>desc reporte</summary>
        [DBField("DESCREPORT")]
        public string DESCREPORT { get; set; }
    }
    public class GetPlanTransporteQueryInput
    {
        ///<summary>Operador portuario</summary>
        [DBParameter("XFECHA", DBDataType.Numeric)]
        public string XFECHA { get; set; }
    }
    public class GetPlanTransporteDescarga
    {
        ///<summary>ETB</summary>
        [DBField("ETB")]
        public decimal ETB { get; set; }
        ///<summary>NaveViaje</summary>
        [DBField("NaveViaje")]
        public string NaveViaje { get; set; }
        ///<summary>FechaArribo</summary>
        [DBField("FechaArribo")]
        public decimal FechaArribo { get; set; }
        ///<summary>Total</summary>
        [DBField("Total")]
        public int Total { get; set; }
        ///<summary>Avance</summary>
        [DBField("Avance")]
        public int Avance { get; set; }
        ///<summary>Saldo</summary>
        [DBField("Saldo")]
        public int Saldo { get; set; }
        ///<summary>TerminoDescarga</summary>
        [DBField("TerminoDescarga")]
        public decimal TerminoDescarga { get; set; }
        ///<summary>Vence</summary>
        [DBField("Vence")]
        public decimal Vence { get; set; }
        ///<summary>OSManifiesto</summary>
        [DBField("OSManifiesto")]
        public string OSManifiesto { get; set; }
        ///<summary>Observacion</summary>
        [DBField("Observacion")]
        public string Observacion { get; set; }
    }
    public class GetPlanTransporteEmbarque
    {
        ///<summary>ETB</summary>
        [DBField("ETB")]
        public decimal ETB { get; set; }
        ///<summary>NaveViaje</summary>
        [DBField("NaveViaje")]
        public string NaveViaje { get; set; }
        ///<summary>FechaArribo</summary>
        [DBField("FechaArribo")]
        public decimal FechaArribo { get; set; }
        ///<summary>Ingresados</summary>
        [DBField("Ingresados")]
        public int Ingresados { get; set; }
        ///<summary>Avance</summary>
        [DBField("Avance")]
        public int Avance { get; set; }
        ///<summary>Saldo</summary>
        [DBField("Saldo")]
        public int Saldo { get; set; }
        ///<summary>InicioStackin</summary>
        [DBField("InicioStackin")]
        public decimal InicioStackin { get; set; }
        ///<summary>CutOff</summary>
        [DBField("CutOff")]
        public decimal CutOff { get; set; }
        ///<summary>OrdenServicio</summary>
        [DBField("OrdenServicio")]
        public string OrdenServicio { get; set; }
        ///<summary>Observacion</summary>
        [DBField("Observacion")]
        public string Observacion { get; set; }
    }
}
