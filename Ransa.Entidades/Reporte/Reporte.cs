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
        ///<summary>XFECHA</summary>
        [DBParameter("XFECHA", DBDataType.Numeric)]
        public string XFECHA { get; set; }
        ///<summary>XNORSRN</summary>
        [DBParameter("XNORSRN", DBDataType.Numeric)]
        public string XNORSRN { get; set; }
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
        [DBField("OSMANIFIESTO")]
        public string OSMANIFIESTO { get; set; }
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
        [DBField("ORDENSERVICIO")]
        public decimal ORDENSERVICIO { get; set; }


      
        ///<summary>Observacion</summary>
        [DBField("Observacion")]
        public string Observacion { get; set; }
    }
    public class GetReporteDetalladoQueryInput
    {
        ///<summary>XFECHA</summary>
        [DBParameter("XFECHA", DBDataType.Numeric)]
        public string XFECHA { get; set; }
        ///<summary>XNORSRN</summary>
        [DBParameter("XNORSRN", DBDataType.Numeric)]
        public string XNORSRN { get; set; }

        ///<summary>XDOCREF</summary>
        [DBParameter("XDOCREF", DBDataType.VarChar)]
        public string XDOCREF { get; set; }

        ///<summary>XCONTENEDOR</summary>
        [DBParameter("XCONTENEDOR", DBDataType.VarChar)]
        public string XCONTENEDOR { get; set; }

        ///<summary>XPLACA</summary>
        [DBParameter("XPLACA", DBDataType.VarChar)]
        public string XPLACA { get; set; }

        ///<summary>XBREVETE</summary>
        [DBParameter("XBREVETE", DBDataType.VarChar)]
        public string XBREVETE { get; set; }
        ///<summary>XRUCTRANSP</summary>
        [DBParameter("XRUCTRANSP", DBDataType.Numeric)]
        public string XRUCTRANSP { get; set; }

        
    }
    public class GetReporteDetalladoEmbarque
    {
        ///<summary>OPERADOR</summary>
        [DBField("Operador")]
        public string OPERADOR { get; set; }
        ///<summary>TIPOOPERACION</summary>
        [DBField("TipoOperacion")]
        public string TIPOOPERACION { get; set; }
        ///<summary>NAVEVIAJE</summary>
        [DBField("NaveViaje")]
        public string NAVEVIAJE { get; set; }
        ///<summary>ORDENSERVICIO</summary>
        [DBField("OSManifiesto")]
        public decimal ORDENSERVICIO { get; set; }

        ///<summary>FechaHoraMovimiento</summary>
        [DBField("FechaHoraMovimiento")]
        public string FechaHoraMovimiento { get; set; }

        ///<summary>EMBARCADOR</summary>
        [DBField("Cliente")]
        public string EMBARCADOR { get; set; }
        ///<summary>BLBOOKING</summary>
        [DBField("BLBK")]
        public string BLBOOKING { get; set; }
        ///<summary>CONTENEDOR</summary>
        [DBField("Contenedor")]
        public string CONTENEDOR { get; set; }
        ///<summary>TIPOCONTENEDOR</summary>
        [DBField("TipoContenedor")]
        public string TIPOCONTENEDOR { get; set; }
        ///<summary>PLACA</summary>
        [DBField("Placa")]
        public string PLACA { get; set; }
        ///<summary>BREVETE</summary>
        [DBField("Brevete")]
        public string BREVETE { get; set; }
        ///<summary>NOMBRECHOFER</summary>
        [DBField("NombreChofer")]
        public string NOMBRECHOFER { get; set; }
        ///<summary>RUCTRANSPORTE</summary>
        [DBField("RUCTransporte")]
        public decimal RUCTRANSPORTE { get; set; }
        ///<summary>NOMBRETRANSPORTE</summary>
        [DBField("NombreTransporte")]
        public string NOMBRETRANSPORTE { get; set; }
        ///<summary>IMO</summary>
        [DBField("IMO")]
        public string IMO { get; set; }
        ///<summary>IQBF</summary>
        [DBField("IQBF")]
        public string IQBF { get; set; }
        
    }
    public class GetReporteDetalladoDesembarque
    {
        ///<summary>OPERADOR</summary>
        [DBField("Operador")]
        public string OPERADOR { get; set; }
        ///<summary>TIPOOPERACION</summary>
        [DBField("TipoOperacion")]
        public string TIPOOPERACION { get; set; }
        ///<summary>NAVEVIAJE</summary>
        [DBField("NaveViaje")]
        public string NAVEVIAJE { get; set; }
        
        ///<summary>OSMANIFIESTO</summary>
        [DBField("OSManifiesto")]
        public string OSMANIFIESTO { get; set; }

        ///<summary>FechaHoraMovimiento</summary>
        [DBField("FechaHoraMovimiento")]
        public string FechaHoraMovimiento { get; set; }

        ///<summary>CLIENTE</summary>
        [DBField("Cliente")]
        public string CLIENTE { get; set; }
        ///<summary>BLBK</summary>
        [DBField("BLBK")]
        public string BLBK { get; set; }
      
        ///<summary>CONTENEDOR</summary>
        [DBField("Contenedor")]
        public string CONTENEDOR { get; set; }
        ///<summary>TIPOCONTENEDOR</summary>
        [DBField("TipoContenedor")]
        public string TIPOCONTENEDOR { get; set; }
        ///<summary>PLACA</summary>
        [DBField("Placa")]
        public string PLACA { get; set; }
        ///<summary>BREVETE</summary>
        [DBField("Brevete")]
        public string BREVETE { get; set; }
        ///<summary>NOMBRECHOFER</summary>
        [DBField("NombreChofer")]
        public string NOMBRECHOFER { get; set; }
        ///<summary>RUCTRANSPORTE</summary>
        [DBField("RUCTransporte")]
        public decimal RUCTRANSPORTE { get; set; }
        ///<summary>NOMBRETRANSPORTE</summary>
        [DBField("NombreTransporte")]
        public string NOMBRETRANSPORTE { get; set; }
        ///<summary>IMO</summary>
        [DBField("IMO")]
        public string IMO { get; set; }
        ///<summary>IQBF</summary>
        [DBField("IQBF")]
        public string IQBF { get; set; }

    }
}
