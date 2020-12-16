using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.JuntaOperativa
{
    /// <summary>
    /// COLSULTA DE NAVES DE IMPORTACIONES
    /// </summary>
    public class Importaciones
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("NORSRN")]
        public decimal NORSRN { get; set; }
        /// <summary>
        /// codigo de Nave
        /// </summary>
        [DBField("CVPRCN")]
        public string CVPRCN { get; set; }
        /// <summary>
        /// nombre de Nave
        /// </summary>
        [DBField("TCMPVP")]
        public string TCMPVP { get; set; }
        /// <summary>
        /// numero de viajes
        /// </summary>
        [DBField("NVJES")]
        public decimal NVJES { get; set; }
        /// <summary>
        /// Clase de Orden 1=Impo, 2=Expo
        /// </summary>
        [DBField("CTPOOP")]
        public decimal CTPOOP { get; set; }
        /// <summary>
        /// Cod Vía de Transporte
        /// </summary>
        [DBField("CVSTR1")]
        public decimal CVSTR1 { get; set; }
        /// <summary>
        /// año de manifiesto
        /// </summary>
        [DBField("AMNFS")]
        public decimal AMNFS { get; set; }
        /// <summary>
        /// numero de manifiesto
        /// </summary>
        [DBField("NMNFEN")]
        public decimal NMNFEN { get; set; }
        /// <summary>
        /// Cod Compania Transporte
        /// </summary>
        [DBField("CCMPTR")]
        public decimal CCMPTR { get; set; }
        /// <summary>
        /// Cod Línea
        /// </summary>
        [DBField("CLNNSS")]
        public decimal CLNNSS { get; set; }
        /// <summary>
        /// Cod Puerto Llegada Nave
        /// </summary>
        [DBField("CPRLLN")]
        public string CPRLLN { get; set; }
        /// <summary>
        /// Cod Propiet Contenedor
        /// </summary>
        [DBField("CPRCN3")]
        public string CPRCN3 { get; set; }
        /// <summary>
        /// Num Serie de Contenedor
        /// </summary>
        [DBField("NSRCN3")]
        public string NSRCN3 { get; set; }
        /// <summary>
        /// Contenedor
        /// </summary>
        [DBField("CONTENE")]
        public string CONTENE { get; set; }
        /// <summary>
        /// Conocimiento de Embarque
        /// </summary>
        //[DBField("NCNEM1")]
        //public string NCNEM1 { get; set; }
        /// <summary>
        /// Cod Clase Contendedor
        /// </summary>
        [DBField("CTPOC2")]
        public string CTPOC2 { get; set; }
        /// <summary>
        /// Operador Portuario
        /// </summary>
        [DBField("FLGOPP")]
        public string FLGOPP { get; set; }
        /// <summary>
        /// Cod Tipo Carga
        /// </summary>
        [DBField("CTPCR1")]
        public string CTPCR1 { get; set; }
        /// <summary>
        /// Num Bultos Manifestados
        /// </summary>
        [DBField("NBLTMN")]
        public decimal NBLTMN { get; set; }
        /// <summary>
        /// Can Kilos Bruto Manifestado
        /// </summary>
        [DBField("PBRKLM")]
        public decimal PBRKLM { get; set; }
        /// <summary>
        /// Num Bultos Recepcionados Ransa
        /// </summary>
        [DBField("NBLRCR")]
        public decimal NBLRCR { get; set; }
        /// <summary>
        /// Peso Bruto Recepcionado Ransa
        /// </summary>
        [DBField("QPBRCR")]
        public decimal QPBRCR { get; set; }
        /// <summary>
        /// Flg Prod Perecible/Peligroso
        /// </summary>
        [DBField("SPRPRP")]
        public string SPRPRP { get; set; }
        /// <summary>
        /// Tamaño Contenedor
        /// </summary>
        [DBField("TTMNCN1")]
        public decimal TTMNCN1 { get; set; }
        /// <summary>
        /// Flg Estado Contenedor Imp. L/V
        /// </summary>
        [DBField("SESCNI")]
        public string SESCNI { get; set; }
        /// <summary>
        /// Flg Refrigerado S/N
        /// </summary>
        [DBField("SCNRFG")]
        public string SCNRFG { get; set; }
        /// <summary>
        /// Fecha Inicio Descarga 
        /// </summary>
        [DBField("FINDSC")]
        public decimal FINDSC { get; set; }
        /// <summary>
        /// Fecha Fin Descarga 
        /// </summary>
        [DBField("FFNDSC")]
        public decimal FFNDSC { get; set; }
        /// <summary>
        /// Hora Fin Descarga 
        /// </summary>
        [DBField("HFNDSC")]
        public decimal HFNDSC { get; set; }
        /// <summary>
        /// Cantidad de Conocimientos de embarque
        /// </summary>
        [DBField("CANTCONO")]
        public int CANTCONO { get; set; }
        /// <summary>
        /// Exclusivo SI/NO
        /// </summary>
        [DBField("EXCLUSIVO")]
        public string EXCLUSIVO { get; set; }

    }

    public class ImportacionesQueryInput
    {
        /// <summary>
        /// Fecha Desde
        /// </summary>
        [DBParameter("DESDE", DBDataType.VarChar)]
        public string DESDE { get; set; }
        /// <summary>
        /// Fecha Hasta
        /// </summary>
        [DBParameter("HASTA", DBDataType.VarChar)]
        public string HASTA { get; set; }
    }

    public class ImportacionesAgrupado
    {
        public decimal orden { get; set; }
        public string nave { get; set; }
        public string fechaEta { get; set; }
        public decimal Manifestado20 { get; set; }
        public decimal Manifestado40 { get; set; }
        public decimal Manifestado20Ree { get; set; }
        public decimal Manifestado40Ree { get; set; }
        public decimal ManifestadoCargaSuelta { get; set; }
        public decimal Recibido20 { get; set; }
        public decimal Recibido40 { get; set; }
        public decimal Recibido20Ree { get; set; }
        public decimal Recibido40Ree { get; set; }
        public decimal RecibidoCargaSuelta { get; set; }
        public decimal Faltan20 { get; set; }
        public decimal Faltan20Ree { get; set; }
        public decimal Faltan40 { get; set; }
        public decimal Faltan40Ree { get; set; }
        public decimal FaltanCargaSuelta { get; set; }

    }

    public  class DatosImportacion
    {
        public List<Importaciones> Detallado { get; set; }
        public List<ImportacionesAgrupado> Agrupado { get; set; }

        public DatosImportacion()
        {
            Detallado = new List<Importaciones>();
            Agrupado = new List<ImportacionesAgrupado>();
        }

    }

   


}
