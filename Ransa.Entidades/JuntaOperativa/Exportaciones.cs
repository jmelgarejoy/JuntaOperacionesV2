using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.JuntaOperativa
{
    public class ExportacionesInput
    {

        /// <summary> Clase Contenedor </summary>
        [DBParameter("IN_DESDE", DBDataType.Integer)]
        public string IN_DESDE { get; set; }

        [DBParameter("IN_HASTA", DBDataType.Integer)]
        public string IN_HASTA { get; set; }

    }
    public class Exportaciones
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("NORSRN")]
        public decimal NORSRN { get; set; }
        /// <summary>
        /// Nombre de Nave
        /// </summary>
        [DBField("CVPRCN")]
        public string CVPRCN { get; set; }
        /// <summary>
        /// Nombre de Nave
        /// </summary>
        [DBField("TCMPVP")]
        public string TCMPVP { get; set; }
        /// <summary>
        /// Booking
        /// </summary>
        [DBField("NBKNCN")]
        public string NBKNCN { get; set; }
        /// <summary>
        /// Fecha de presentación autorizada
        /// </summary>
        [DBField("FPRSAT")]
        public decimal FPRSAT { get; set; }
        /// <summary>
        /// Sigla de Contenedor
        /// </summary>
        [DBField("CPRCN6")]
        public string CPRCN6 { get; set; }
        /// <summary>
        /// Numero de Serie de Contenedor
        /// </summary>
        [DBField("NSRCN6")]
        public string NSRCN6 { get; set; }
        /// <summary>
        /// tipo de Carga (L=LLeno, V=Vacio)
        /// </summary>
        [DBField("TPOBKN")]
        public string TPOBKN { get; set; }
        /// <summary>
        /// Tamaño de contenedor
        /// </summary>
        [DBField("TTMNCN")]
        public decimal TTMNCN { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("CTPOC2")]
        public string CTPOC2 { get; set; }
        /// <summary>
        /// Guia de Salida
        /// </summary>
        [DBField("NGSLCN")]

        public decimal NGSLCN { get; set; }
        /// <summary>
        /// Fecha de Ingreso
        /// </summary>
        [DBField("FGINAL")]
        public decimal FGINAL { get; set; }
        /// <summary>
        /// Hora de Ingreso
        /// </summary>
        [DBField("HGINAL")]
        public decimal HGINAL { get; set; }
        /// <summary>
        /// Fecha de Salida
        /// </summary>
        [DBField("FPSDSL")]
        public decimal FPSDSL { get; set; }
        /// <summary>
        /// Hora de Salida
        /// </summary>
        [DBField("HPSDSL")]
        public decimal HPSDSL { get; set; }
        /// <summary>
        /// Flag de Transito
        /// </summary>
        [DBField("FLGETR")]
        public string FLGETR { get; set; }
        /// <summary>
        /// Imo
        /// </summary>
        [DBField("IMO")]
        public string IMO { get; set; }
        /// <summary>
        /// Iqbf
        /// </summary>

        [DBField("IQBF")]
        public string IQBF { get; set; }
        /// <summary>
        /// Canal Asignado
        /// </summary>
        [DBField("FLGPRC")]
        public string FLGPRC { get; set; }
        /// <summary>
        /// Presinto de Salida
        /// </summary>
        [DBField("NPRGS2")]
        public string NPRGS2 { get; set; }
        /// <summary>
        /// Embarcador
        /// </summary>
        [DBField("TEMBR1")]
        public string TEMBR1 { get; set; }
        /// <summary>
        /// Documento Aduanero
        /// </summary>

        [DBField("NORDEM")]
        public decimal NORDEM { get; set; }
        /// <summary>
        /// PESO
        /// </summary>

        [DBField("PESO")]
        public decimal PESO { get; set; }
        /// <summary>
        /// Ingreso/Salida
        /// </summary>

        [DBField("INGRESOSALIDA")]
        public string INGRESOSALIDA { get; set; }
        /// <summary>
        /// Contenedor
        /// </summary>

        [DBField("CONTENEDOR")]
        public string CONTENEDOR { get; set; }
        /// <summary>
        /// Fecha de Salida Formato
        /// </summary>

        [DBField("FECHAINGRESO")]
        public string FECHAINGRESO { get; set; }
        /// <summary>
        /// Hora de Ingreso Formato
        /// </summary>
        [DBField("HORAINGRESO")]
        public string HORAINGRESO { get; set; }
        /// <summary>
        /// Fecha y Hora de Ingreso Formato
        /// </summary>
        [DBField("FECHAHORAINGRESO")]
        public string FECHAHORAINGRESO { get; set; }
        /// <summary>
        /// Fecha de Ingreso Formato
        /// </summary>

        [DBField("FECHASALIDA")]
        public string FECHASALIDA { get; set; }
        /// <summary>
        /// Hora de Salida Formato
        /// </summary>
        [DBField("HORASALIDA")]
        public string HORASALIDA { get; set; }
        /// <summary>
        /// Fecha y Hora de Salida Formato
        /// </summary>
        [DBField("FECHAHORASALIDA")]
        public string FECHAHORASALIDA { get; set; }
        /// <summary>
        /// Operador Portuario
        /// </summary>
        [DBField("FLGOPP")]
        public string FLGOPP { get; set; }
        /// <summary>
        /// FECHA CUTOFF
        /// </summary>
        [DBField("FCOFF1")]
        public decimal FCOFF1 { get; set; }
        /// <summary>
        /// HORA CUTOFF
        /// </summary>
        [DBField("HCOFF1")]
        public decimal HCOFF1 { get; set; }
        /// <summary>
        /// FECHA Y HORA CUTOFF FORMATO
        /// </summary>
        [DBField("FECHAHORACUTOFF")]
        public string FECHAHORACUTOFF { get; set; }
        /// <summary>
        /// TARA
        /// </summary>
        [DBField("TARA")]
        public decimal TARA { get; set; }
        /// <summary>
        /// PESO NETO MERCADERIA
        /// </summary>
        [DBField("PESONETO")]
        public decimal PESONETO { get; set; }
        /// <summary>
        /// FECHA ENTREGA DOCUMENTO
        /// </summary>
        [DBField("FECDOC")]
        public decimal FECDOC { get; set; }
        /// <summary>
        /// FECHA ENTREGA DOCUMENTO FORMATO
        /// </summary>
        [DBField("FECHAENTRDOC")]
        public string FECHAENTRDOC { get; set; }
        /// <summary>
        /// HORA ENTREGA DOCUMENTO
        /// </summary>
        [DBField("HORDOC")]
        public decimal HORDOC { get; set; }
        /// <summary>
        /// HORA ENTREGA DOCUMENTO FORMATO
        /// </summary>
        [DBField("HORAENTRDOC")]
        public string HORAENTRDOC { get; set; }
        /// <summary>
        /// FECHA Y HORA ENTREGA DOCUMENTO FORMATO
        /// </summary>
        [DBField("FECHAHORAENTRDOC")]
        public string FECHAHORAENTRDOC { get; set; }
      
        /// <summary>
        /// EXCLUSICO/CONSOLIDADO = (SI/NO)
        /// </summary>
        [DBField("EXCLUSIVO")]
        public string EXCLUSIVO { get; set; }
        /// <summary>
        /// Refrigerado
        /// </summary>
        [DBField("REFRIGER")]
        public string REFRIGER { get; set; }
        /// <summary>
        /// Tipo de Contenedor
        /// </summary>
        [DBField("SPRPRP")]
        public string SPRPRP { get; set; }
        /// <summary>
        /// total Cont. Manifestados
        /// </summary>
        [DBField("QCNTSL")]
        public decimal QCNTSL { get; set; }



    }
    public class ExportacionAgrupado
    {
        public decimal orden { get; set; }
        public string nave { get; set; }
        public string CutOff { get; set; }
        public decimal TotalManifestado20 { get; set; }
        public decimal TotalManifestado40 { get; set; }
        public decimal TotalManifestado20Ree { get; set; }
        public decimal TotalManifestado40Ree { get; set; }
        public decimal Total20Recibido { get; set; }
        public decimal Total20RecibidoReef { get; set; }
        public decimal Total20RecibidoCD { get; set; }
        public decimal Total20RecibidoCDReef { get; set; }
        public decimal Total20RecibidoSD { get; set; }
        public decimal Total20RecibidoSDReef { get; set; }
        public decimal Total40Recibido { get; set; }
        public decimal Total40RecibidoReef { get; set; }
        public decimal Total40RecibidoCD { get; set; }
        public decimal Total40RecibidoCDReef { get; set; }
        public decimal Total40RecibidoSD { get; set; }
        public decimal Total40RecibidoSDReef { get; set; }
        public decimal Faltan20 { get; set; }
        public decimal Faltan20Ree { get; set; }
        public decimal Faltan40 { get; set; }
        public decimal Faltan40Ree { get; set; }

    }
    public class ExportacionAgrupadoTotales
    {
        public decimal orden { get; set; }
        public string nave { get; set; }
        public string booking { get; set; }
        public decimal cantidad { get; set; }
        public decimal tamanio { get; set; }
        public string reef { get; set; }
    }

    public class DatosExportacion
    {
        public List<Exportaciones> Detallado { get; set; }
        public List<ExportacionAgrupado> Agrupado { get; set; }

        public DatosExportacion()
        {
            Detallado = new List<Exportaciones>();
            Agrupado = new List<ExportacionAgrupado>();
        }

    }
}
