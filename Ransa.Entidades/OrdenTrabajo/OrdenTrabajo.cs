using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.OrdenTrabajo
{
    public class Mail
    {
        public string Orden { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Tipo { get; set; }
        public List<Orden> detalle { get; set; }
    }

    public class ContenedoresGroup
    {
        public string CONTENEDOR { get; set; }
        public string CLASE { get; set; }
        public string MOTIVO { get; set; }
    }

    public class Orden
    {
        /// <summary>
        /// Orden de Trabajo 
        /// </summary>
        [DBField("OTRABAJO")]
        public decimal OTRABAJO { get; set; }
        /// <summary>
        /// Orden de servicio (Recalada)
        /// </summary>
        [DBField("RECALADA")]
        public decimal RECALADA { get; set; }
        /// <summary>
        /// Codigo de la Nave
        /// </summary>
        [DBField("CODNAVE")]
        public string CODNAVE { get; set; }
        /// <summary>
        /// Codigo de la Nave
        /// </summary>
        [DBField("NAVE")]
        public string NAVE { get; set; }
        /// <summary>
        /// Numero de viajes
        /// </summary>
        [DBField("NVJES")]
        public decimal NVJES { get; set; }
        /// <summary>
        /// Tipo de Regimen (1=Importación, 2=Exportación)
        /// </summary>
        [DBField("CTPOOP")]
        public decimal CTPOOP { get; set; }
        /// <summary>
        /// Exportación, Importación
        /// </summary>
        [DBField("REGIMEN")]
        public string REGIMEN { get; set; }
        /// <summary>
        /// Codigo de Linea Naviera
        /// </summary>
        [DBField("CODLINEA")]
        public decimal CODLINEA { get; set; }
        /// <summary>
        /// Linea Naviera
        /// </summary>
        [DBField("LINEA")]
        public string LINEA { get; set; }
        /// <summary>
        /// Almacen
        /// </summary>
        [DBField("ALMACEN")]
        public string ALMACEN { get; set; }
        /// <summary>
        /// Codigo de Puerto Origen
        /// </summary>
        [DBField("CODPTOORI")]
        public string CODPTOORI { get; set; }
        /// <summary>
        /// Codigo de Puerto Origen
        /// </summary>
        [DBField("UBICACION")]
        public string UBICACION { get; set; }
        /// <summary>
        /// Puerto Origen
        /// </summary>
        [DBField("PTOORIGEN")]
        public string PTOORIGEN { get; set; }
        /// <summary>
        /// Codigo de Cliente
        /// </summary>
        [DBField("CODCLIE")]
        public decimal CODCLIE { get; set; }
        /// <summary>
        /// Cliente
        /// </summary>
        [DBField("CLIENTE")]
        public string CLIENTE { get; set; }
        /// <summary>
        /// Cliente
        /// </summary>
        [DBField("CODAGEN")]
        public decimal CODAGEN { get; set; }
        /// <summary>
        /// Agente
        /// </summary>
        [DBField("AGENTE")]
        public string AGENTE { get; set; }
        /// <summary>
        /// Correlativo
        /// </summary>
        [DBField("ITEM")]
        public decimal ITEM { get; set; }
        /// <summary>
        /// Conyenedor
        /// </summary>
        [DBField("CONTENEDOR")]
        public string CONTENEDOR { get; set; }
        /// <summary>
        /// tipo
        /// </summary>
        [DBField("TIPO")]
        public string TIPO { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("CLASE")]
        public string CLASE { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("CODSERV")]
        public decimal CODSERV { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("SERVICIO")]
        public string SERVICIO { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("UNIDADSERVICIO")]
        public string UNIDADSERVICIO { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("CANTSOLIC")]
        public decimal CANTSOLIC { get; set; }
        /// <summary>
        /// BL Master
        /// </summary>
        [DBField("BLMASTER")]
        public string BLMASTER { get; set; }
        /// <summary>
        /// Codigo puerto Llegada
        /// </summary>
        [DBField("CODPTOLLE")]
        public string CODPTOLLE { get; set; }
        /// <summary>
        /// puerto Llegada
        /// </summary>
        [DBField("PTOLLEGADA")]
        public string PTOLLEGADA { get; set; }
        /// <summary>
        /// cODIGO CLIENTE A FACTURAR
        /// </summary>
        [DBField("FACTURAR")]
        public decimal FACTURAR { get; set; }
        /// <summary>
        /// cODIGO CLIENTE A FACTURAR
        /// </summary>
        [DBField("CORRELATIVO")]
        public decimal CORRELATIVO { get; set; }
        /// <summary>
        /// bl O bOOKING
        /// </summary>
        [DBField("DOCUMENTO")]
        public string DOCUMENTO { get; set; }
        /// <summary>
        /// FECHA ORDEn TRABAJO
        /// </summary>
        [DBField("FECHA")]
        public string FECHA { get; set; }
        /// <summary>
        /// cODIGO Despachador
        /// </summary>
        [DBField("CODDESP")]
        public decimal CODDESP { get; set; }
        /// <summary>
        /// cODIGO Despachador
        /// </summary>
        [DBField("DESPACHADOR")]
        public string DESPACHADOR { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBField("DESCRIPCION")]
        public string DESCRIPCION { get; set; }
        /// <summary>
        /// USUARIO
        /// </summary>
        [DBField("USUARIO")]
        public string USUARIO { get; set; }
        /// <summary>
        /// A quien cobrar?
        /// </summary>
        [DBField("COBRAR")]
        public string COBRAR { get; set; }
        /// <summary>
        /// C = Consolidado
        /// </summary>
        [DBField("CONSOLIDADO")]
        public string CONSOLIDADO { get; set; }
        /// <summary>
        /// C = Consolidado
        /// </summary>
        [DBField("GUIACONT")]
        public decimal GUIACONT { get; set; }
        /// <summary>
        /// Codigo de Proveedor
        /// </summary>
        [DBField("CODPROVEEDOR")]
        public decimal CODPROVEEDOR { get; set; }
        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("PROVEEDOR")]
        public string PROVEEDOR { get; set; }
        /// <summary>
        /// ?
        /// </summary>
        [DBField("PESO")]
        public decimal PESO { set; get; }
        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("INFORMACION")]
        public string INFORMACION { get; set; }

        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("MOTIVO")]
        public string MOTIVO { get; set; }

        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("EMAIL")]
        public string EMAIL { get; set; }

        /// <summary>
        /// proveedor
        /// </summary>
        [DBField("CONTACTO")]
        public string CONTACTO { get; set; }

    }
    public class OrdenQueryinput
    {
        [DBParameter("IN_TIPO", DBDataType.Numeric)]
        public decimal IN_TIPO { get; set; }

        [DBParameter("IN_DOCUMENTO", DBDataType.VarChar)]
        public string IN_DOCUMENTO { get; set; }

        [DBParameter("IN_LIBRERIA", DBDataType.VarChar)]
        public string IN_LIBRERIA { get; set; }

    }
    public class ContactosCC
    {
        [DBField("ID_SLN")]
        public string ID_SLN { set; get; }

        [DBField("TIP_DST")]
        public string TIP_DST { set; get; }

        [DBField("NOMBRE_DEST")]
        public string NOMBRE_DEST { set; get; }

        [DBField("DIR_COR")]
        public string DIR_COR { set; get; }

    }
    public class OrdenTrabajo
    {
        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("CGRONG")]
        public string CGRONG { get; set; }
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }
        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBField("CSRVNV")]
        public decimal CSRVNV { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("SERVICIO")]
        public string SERVICIO { get; set; }
        /// <summary>
        /// Correlativo
        /// </summary>
        [DBField("NCRRLT")]
        public decimal NCRRLT { get; set; }
        /// <summary>
        /// Fecha orden trabajo
        /// </summary>
        [DBField("FPRGOT")]
        public decimal FPRGOT { get; set; }
        /// <summary>
        /// Sigla 
        /// </summary>
        [DBField("CPRCN1")]
        public string CPRCN1 { get; set; }
        /// <summary>
        /// Serie 
        /// </summary>
        [DBField("NSRCN1")]
        public string NSRCN1 { get; set; }
        /// <summary>
        /// Cantidad Servicios
        /// </summary>
        [DBField("QSRVC")]
        public decimal QSRVC { get; set; }
        /// <summary>
        /// Peso Servicios
        /// </summary>
        [DBField("PSRVC")]
        public decimal PSRVC { get; set; }
        /// <summary>
        ///  ESTADO
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
        /// <summary>
        /// Cliente
        /// </summary>
        [DBField("CCLNT")]
        public decimal CCLNT { get; set; }
        /// <summary>
        ///  CLIENTE
        /// </summary>
        [DBField("CLIENTE")]
        public string CLIENTE { get; set; }
        /// <summary>
        ///  BL o Booking
        /// </summary>
        [DBField("DOCUMENTO")]
        public string DOCUMENTO { get; set; }

        /// <summary>
        /// Orden de Servicio
        /// </summary>
        [DBField("NORDN1")]
        public decimal NORDN1 { get; set; }
        /// <summary>
        /// Orden de Servicio
        /// </summary>
        [DBField("CODAGEN")]
        public decimal CODAGEN { get; set; }
        /// <summary>
        ///  CLIENTE
        /// </summary>
        [DBField("AGENTE")]
        public string AGENTE { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("MOTIVO")]
        public string MOTIVO { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("CLASE")]
        public string CLASE { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("UBICACION")]
        public string UBICACION { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("IMO")]
        public string IMO { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("OBSERVACION")]
        public string OBSERVACION { get; set; }
        /// <summary>
        /// NROEXPED
        /// </summary>
        [DBField("NROEXPED")]
        public string NROEXPED { get; set; }
        /// <summary>
        /// RESP
        /// </summary>
        [DBField("RESP")]
        public string RESP { get; set; }
        /// <summary>
        /// ESTTRANS
        /// </summary>
        [DBField("ESTTRANS")]
        public string ESTTRANS { get; set; }
        

    }
    public class OrdenTrabajoInput
    {
        /// <summary>
        /// Orden de Trabajo 
        /// </summary>
        [DBParameter("OTRABAJO", DBDataType.Numeric)]
        public decimal OTRABAJO { get; set; }
        /// <summary>
        /// Orden de servicio (Recalada)
        /// </summary>
        [DBParameter("RECALADA", DBDataType.Numeric)]
        public decimal RECALADA { get; set; }
        /// <summary>
        /// Codigo de la Nave
        /// </summary>
        [DBParameter("CODNAVE", DBDataType.VarChar)]
        public string CODNAVE { get; set; }
        /// <summary>
        /// Numero de viajes
        /// </summary>
        [DBParameter("NVJES", DBDataType.Numeric)]
        public decimal NVJES { get; set; }
        /// <summary>
        /// Tipo de Regimen (1=Importación, 2=Exportación)
        /// </summary>
        [DBParameter("CTPOOP", DBDataType.Numeric)]
        public decimal CTPOOP { get; set; }
        /// <summary>
        /// Codigo de Linea Naviera
        /// </summary>
        [DBParameter("LINEA", DBDataType.Numeric)]
        public decimal LINEA { get; set; }
        /// <summary>
        /// Codigo de Puerto Origen
        /// </summary>
        [DBParameter("CODPTOORI", DBDataType.VarChar)]
        public string CODPTOORI { get; set; }
        /// <summary>
        /// Codigo de Cliente
        /// </summary>
        [DBParameter("CODCLIE", DBDataType.Numeric)]
        public decimal CODCLIE { get; set; }
        /// <summary>
        /// Cliente
        /// </summary>
        [DBParameter("CODAGEN", DBDataType.Numeric)]
        public decimal CODAGEN { get; set; }
        /// <summary>
        /// Correlativo
        /// </summary>
        [DBParameter("ITEM", DBDataType.Numeric)]
        public decimal ITEM { get; set; }
        /// <summary>
        /// Conyenedor
        /// </summary>
        [DBParameter("CONTENEDOR", DBDataType.VarChar)]
        public string CONTENEDOR { get; set; }
        /// <summary>
        /// tipo
        /// </summary>
        [DBParameter("TIPO", DBDataType.VarChar)]
        public string TIPO { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBParameter("CLASE", DBDataType.VarChar)]
        public string CLASE { get; set; }
        /// <summary>
        /// Servicio (JSON)
        /// </summary>
        [DBParameter("SERVICIO", DBDataType.VarChar)]
        public string SERVICIO { get; set; }

        /// <summary>
        /// Servicio
        /// </summary>
        [DBParameter("CANTSOLIC", DBDataType.Integer)]
        public int CANTSOLIC { get; set; }
        /// <summary>
        /// BL Master
        /// </summary>
        [DBParameter("BLMASTER", DBDataType.VarChar)]
        public string BLMASTER { get; set; }
        /// <summary>
        /// Codigo puerto Llegada
        /// </summary>
        [DBParameter("CODPTOLLE", DBDataType.Numeric)]
        public decimal CODPTOLLE { get; set; }
        /// <summary>
        /// cODIGO CLIENTE A FACTURAR
        /// </summary>
        [DBParameter("FACTURAR", DBDataType.Numeric)]
        public decimal FACTURAR { get; set; }
        /// <summary>
        /// cODIGO CLIENTE A FACTURAR
        /// </summary>
        [DBParameter("CORRELATIVO", DBDataType.Numeric)]
        public decimal CORRELATIVO { get; set; }
        /// <summary>
        /// bl O bOOKING
        /// </summary>
        [DBParameter("DOCUMENTO", DBDataType.VarChar)]
        public string DOCUMENTO { get; set; }
        /// <summary>
        /// FECHA
        /// </summary>
        [DBParameter("FECHA", DBDataType.VarChar)]
        public string FECHA { get; set; }
        /// <summary>
        /// cODIGO Despachador
        /// </summary>
        [DBParameter("CODDESP", DBDataType.Numeric)]
        public decimal CODDESP { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter("DESCRIPCION", DBDataType.VarChar)]
        public string DESCRIPCION { get; set; }
        /// <summary>
        /// USUARIO
        /// </summary>
        [DBParameter("USUARIO", DBDataType.VarChar)]
        public string USUARIO { get; set; }
        /// <summary>
        /// A quien cobrar?
        /// </summary>
        [DBParameter("COBRAR", DBDataType.VarChar)]
        public string COBRAR { get; set; }
        /// <summary>
        /// Codigo Servicio
        /// </summary>
        [DBParameter("CODSERV", DBDataType.Numeric)]
        public decimal CODSERV { get; set; }
        /// <summary>
        /// Codigo Servicio Old
        /// </summary>
        [DBParameter("CODSERVOLD", DBDataType.Numeric)]
        public decimal CODSERVOLD { get; set; }
        /// <summary>
        /// C = Consolidado
        /// </summary>
        [DBParameter("CONSOLIDADO", DBDataType.VarChar)]
        public string CONSOLIDADO { get; set; }
        /// <summary>
        /// C = Consolidado
        /// </summary>
        [DBParameter("GUIACONT", DBDataType.VarChar)]
        public decimal GUIACONT { get; set; }

    }
    public class OrdenTrabajoQueryinput
    {

        [DBParameter("FECHA", DBDataType.VarChar)]
        public string FECHA { get; set; }
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { get; set; }

    }
    public class ReportesOTQueryinput
    {

        [DBParameter("DESDE", DBDataType.Numeric)]
        public decimal DESDE { get; set; }

        [DBParameter("HASTA", DBDataType.Numeric)]
        public decimal HASTA { get; set; }

        [DBParameter("SERVICIO", DBDataType.Numeric)]
        public decimal SERVICIO { get; set; }

        [DBParameter("DOCUMENTO", DBDataType.VarChar)]
        public string DOCUMENTO { get; set; }

        [DBParameter("OT", DBDataType.Numeric)]
        public decimal OT { get; set; }
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { get; set; }
    }
    public class GroupOT
    {

        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("CGRONG")]
        public string CGRONG { get; set; }
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }
        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBField("CSRVNV")]
        public decimal CSRVNV { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("SERVICIO")]
        public string SERVICIO { get; set; }
        /// <summary>
        /// Cant. Servicios
        /// </summary>
        [DBField("CANTSERVICIOS")]
        public decimal CANTSERVICIOS { get; set; }
        /// <summary>
        ///  ESTADO
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
        /// <summary>
        ///  CLIENTE
        /// </summary>
        [DBField("CLIENTE")]
        public string CLIENTE { get; set; }
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDN1")]
        public decimal NORDN1 { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("DOCUMENTO")]
        public string DOCUMENTO { get; set; }
        /// <summary>
        ///  CLIENTE
        /// </summary>
        [DBField("AGENTE")]
        public string AGENTE { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBParameter("CLASE", DBDataType.VarChar)]
        public string CLASE { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBParameter("UBICACION", DBDataType.VarChar)]
        public string UBICACION { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBParameter("MOTIVO", DBDataType.VarChar)]
        public string MOTIVO { get; set; }
        /// <summary>
        /// Clase
        /// </summary>
        [DBField("OBSERVACION")]
        public string OBSERVACION { get; set; }
        /// NROEXPED
        /// </summary>
        [DBField("NROEXPED")]
        public string NROEXPED { get; set; }
        /// <summary>
        /// RESP
        /// </summary>
        [DBField("RESP")]
        public string RESP { get; set; }
        /// <summary>
        /// ESTTRANS
        /// </summary>
        [DBField("ESTTRANS")]
        public string ESTTRANS { get; set; }
    }
    public class GroupOTtotales
    {
        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("REGIMEN")]
        public string REGIMEN { get; set; }
        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("CGRONG")]
        public string CGRONG { get; set; }
        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBField("CSRVNV")]
        public decimal CSRVNV { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("SERVICIO")]
        public string SERVICIO { get; set; }
        /// <summary>
        /// Cant. Servicios
        /// </summary>
        [DBField("CANTSERVICIOS")]
        public decimal CANTSERVICIOS { get; set; }
        /// <summary>
        ///  ESTADO
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
    }
    public class OTGlobal
    {
        public List<OrdenTrabajo> Detallado { set; get; }
        public List<GroupOT> Grupo { set; get; }
        public List<liquidacionGroupOrden> GrupoByOrden { set; get; }
        public List<GroupOTtotales> Totales { set; get; }

    }
    public class ProgramarInput
    {

        [DBParameter("FECHA", DBDataType.Numeric)]
        public decimal FECHA { get; set; }

        [DBParameter("HORA", DBDataType.Numeric)]
        public decimal HORA { get; set; }

        [DBParameter("ORDEN", DBDataType.Numeric)]
        public decimal ORDEN { get; set; }
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { get; set; }


    }
    public class ReprogramarInput
    {

        [DBParameter("FECHA", DBDataType.Numeric)]
        public decimal FECHA { get; set; }

        [DBParameter("ORDEN", DBDataType.Numeric)]
        public decimal ORDEN { get; set; }
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { get; set; }

    }
    public class Liquidacion
    {
        public List<OrdenTrabajo> Detalle { set; get; }
        public List<liquidacionGroup> Grupo { set; get; }
        public List<liquidacionGroupOrden> GrupoByOrden { set; get; }

    }
    public class liquidacionGroup
    {
        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("CGRONG")]
        public string CGRONG { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDN1")]
        public decimal NORDN1 { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("CANTSERV")]
        public decimal CANTSERV { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("PesoSERV")]
        public decimal PesoSERV { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [DBField("CLIENTE")]
        public string CLIENTE { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [DBField("DOCUMENTO")]
        public string DOCUMENTO { get; set; }
        /// <summary>
        ///  CLIENTE
        /// </summary>
        [DBField("AGENTE")]
        public string AGENTE { get; set; }
    }
    public class liquidacionGroupOrden
    {
        /// <summary>
        /// Regimen
        /// </summary>
        [DBField("CGRONG")]
        public string CGRONG { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }

        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBField("CSRVNV")]
        public decimal CSRVNV { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        [DBField("SERVICIO")]
        public string SERVICIO { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("CANTSERV")]
        public decimal CANTSERV { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("PESOSERV")]
        public decimal PESOSERV { get; set; }

        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NCRRLT")]
        public decimal NCRRLT { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }
    }
    public class FalsoServicioInput
    {
        [DBParameter("ORDEN", DBDataType.Numeric)]
        public decimal ORDEN { set; get; }

        [DBParameter("CODSERV", DBDataType.Numeric)]
        public decimal CODSERV { get; set; }
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { get; set; }
    }
    public class LiquidarServicioInput
    {
        [DBParameter("IN_NORDTR", DBDataType.Numeric)]
        public decimal IN_NORDTR { set; get; }

        [DBParameter("IN_CSRVNV", DBDataType.Numeric)]
        public decimal IN_CSRVNV { get; set; }

        [DBParameter("IN_NCRRLT", DBDataType.Numeric)]
        public decimal IN_NCRRLT { get; set; }

        [DBParameter("IN_CCMPN", DBDataType.VarChar)]
        public string IN_CCMPN { get; set; }

        [DBParameter("IN_CDVSN", DBDataType.VarChar)]
        public string IN_CDVSN { get; set; }

        [DBParameter("IN_CRBCTC", DBDataType.Numeric)]
        public decimal IN_CRBCTC { get; set; }

        [DBParameter("IN_QSRVC", DBDataType.Numeric)]
        public decimal IN_QSRVC { get; set; }

        [DBParameter("IN_PSRVC", DBDataType.Numeric)]
        public decimal IN_PSRVC { get; set; }

        [DBParameter("IN_ITRFSR", DBDataType.Numeric)]
        public decimal IN_ITRFSR { get; set; }

        [DBParameter("IN_CMNDA5", DBDataType.Numeric)]
        public decimal IN_CMNDA5 { get; set; }

        [DBParameter("IN_CPRVD", DBDataType.VarChar)]
        public string IN_CPRVD { get; set; }

        [DBParameter("IN_CCDRLL", DBDataType.Numeric)]
        public decimal IN_CCDRLL { get; set; }

        [DBParameter("IN_TOBSRV", DBDataType.VarChar)]
        public string IN_TOBSRV { get; set; }

        [DBParameter("IN_QATNAN", DBDataType.Numeric)]
        public decimal IN_QATNAN { get; set; }

        [DBParameter("IN_PATNAN", DBDataType.Numeric)]
        public decimal IN_PATNAN { get; set; }

        [DBParameter("IN_TOBSR1", DBDataType.VarChar)]
        public string IN_TOBSR1 { get; set; }

        [DBParameter("IN_FLIQSR", DBDataType.Numeric)]
        public decimal IN_FLIQSR { get; set; }

        [DBParameter("IN_HLIQSR", DBDataType.Numeric)]
        public decimal IN_HLIQSR { get; set; }

        [DBParameter("IN_ULIQSR", DBDataType.VarChar)]
        public string IN_ULIQSR { get; set; }

        [DBParameter("IN_SESTSR", DBDataType.VarChar)]
        public string IN_SESTSR { get; set; }

        [DBParameter("IN_SESTRG", DBDataType.VarChar)]
        public string IN_SESTRG { get; set; }

        [DBParameter("IN_TOBSR", DBDataType.VarChar)]
        public string IN_TOBSR { get; set; }

        [DBParameter("IN_SQNCB", DBDataType.VarChar)]
        public string IN_SQNCB { get; set; }

        [DBParameter("IN_SESFAC", DBDataType.VarChar)]
        public string IN_SESFAC { get; set; }

        [DBParameter("IN_SVLRZ", DBDataType.VarChar)]
        public string IN_SVLRZ { get; set; }

        [DBParameter("IN_FINREE", DBDataType.Numeric)]
        public decimal IN_FINREE { get; set; }

        [DBParameter("IN_HINREE", DBDataType.Numeric)]
        public decimal IN_HINREE { get; set; }

        [DBParameter("IN_FFIREE", DBDataType.Numeric)]
        public decimal IN_FFIREE { get; set; }

        [DBParameter("IN_HFIREE", DBDataType.Numeric)]
        public decimal IN_HFIREE { get; set; }

        [DBParameter("IN_CZNLLN", DBDataType.Numeric)]
        public decimal IN_CZNLLN { get; set; }
        [DBParameter("IN_LIBRERIA", DBDataType.VarChar)]
        public string IN_LIBRERIA { get; set; }
    }
    public class ProveedoresLiq
    {
        /// <summary>
        /// id Proveedor
        /// </summary>
        [DBField("IDPROV")]
        public long IDPROV { get; set; }

        /// <summary>
        /// id Proveedor
        /// </summary>
        [DBField("RAZCOMER")]
        public string RAZCOMER { get; set; }

    }
    public class RecursosLiq
    {
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }
        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBField("CSRVNV")]
        public decimal CSRVNV { get; set; }
        /// <summary>
        /// Correlativo
        /// </summary>
        [DBField("NCRRLT")]
        public decimal NCRRLT { get; set; }
        /// <summary>
        /// Tipo Recurso
        /// </summary>
        [DBField("TIPREC")]
        public decimal TIPREC { get; set; }
        /// <summary>
        /// Codigo Recurso
        /// </summary>
        [DBField("CODREC")]
        public decimal CODREC { get; set; }
        /// <summary>
        /// Recurso
        /// </summary>
        [DBField("RECURSO")]
        public string RECURSO { get; set; }
        /// <summary>
        /// Unidad de Recurso
        /// </summary>
        [DBField("UNIREC")]
        public string UNIREC { get; set; }
        /// <summary>
        /// Unidad de Servicio
        /// </summary>
        [DBField("UNISERV")]
        public string UNISERV { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("TIPORD")]
        public decimal TIPORD { get; set; }
        /// <summary>
        /// Unidad de Servicio
        /// </summary>
        [DBField("TIPORDDESC")]
        public string TIPORDDESC { get; set; }
        /// <summary>
        /// Unidad de Servicio
        /// </summary>
        [DBField("OBSERV")]
        public string OBSERV { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("CANTUSA")]
        public decimal CANTUSA { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("FECCRE")]
        public decimal FECCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("HORCRE")]
        public decimal HORCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("USUCRE")]
        public decimal USUCRE { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("SESTRG")]
        public decimal SESTRG { get; set; }


    }
    public class RecursosLiqInput
    {
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBParameter("IN_NORDTR", DBDataType.Numeric)]
        public decimal IN_NORDTR { get; set; }
        /// <summary>
        /// Codigo de Servicio
        /// </summary>
        [DBParameter("IN_CSRVNV", DBDataType.Numeric)]
        public decimal IN_CSRVNV { get; set; }
        /// <summary>
        /// Correlativo
        /// </summary>
        [DBParameter("IN_NCRRLT", DBDataType.Numeric)]
        public decimal IN_NCRRLT { get; set; }
        /// <summary>
        /// Tipo Recurso
        /// </summary>
        [DBParameter("IN_TIPREC", DBDataType.Numeric)]
        public decimal IN_TIPREC { get; set; }
        /// <summary>
        /// Codigo Recurso
        /// </summary>
        [DBParameter("IN_CODREC", DBDataType.Numeric)]
        public decimal IN_CODREC { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBParameter("IN_TIPORD", DBDataType.Numeric)]
        public decimal IN_TIPORD { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBParameter("IN_CANTUSA", DBDataType.Numeric)]
        public decimal IN_CANTUSA { get; set; }
        /// <summary>
        /// Unidad de Servicio
        /// </summary>
        [DBParameter("IN_OBSERV", DBDataType.VarChar)]
        public string IN_OBSERV { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBParameter("IN_FECCRE", DBDataType.Numeric)]
        public decimal IN_FECCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBParameter("IN_HORCRE", DBDataType.Numeric)]
        public decimal IN_HORCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBParameter("IN_USUCRE", DBDataType.VarChar)]
        public string IN_USUCRE { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("IN_SESTRG", DBDataType.VarChar)]
        public string IN_SESTRG { get; set; }

        /// <summary>
        ///Libreria
        /// </summary>
        [DBParameter("IN_LIBRERIA", DBDataType.VarChar)]
        public string IN_LIBRERIA { get; set; }
    }
    public class RecursosLiqQueryinput
    {

        [DBParameter("OT", DBDataType.Numeric)]
        public decimal OT { get; set; }

        [DBParameter("SERVICIO", DBDataType.Numeric)]
        public decimal SERVICIO { get; set; }

    }
    public class Historial
    {
        /// <summary>
        /// Orden de Trabajo
        /// </summary>
        [DBField("NORDTR")]
        public decimal NORDTR { get; set; }
        
        /// <summary>
        /// Unidad de Servicio
        /// </summary>
        [DBField("OBSERV")]
        public string OBSERV { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("SESFAC")]
        public string SESFAC { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("FECCRE")]
        public decimal FECCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("HORCRE")]
        public decimal HORCRE { get; set; }
        /// <summary>
        /// Tipo de Orden
        /// </summary>
        [DBField("USUARIO")]
        public string USUARIO { get; set; }
        


    }

    public class Historialinput
    {

        [DBParameter("DOCUMENTO", DBDataType.Numeric)]
        public decimal DOCUMENTO { get; set; }

       

    }

    public class OrdenTrabajoAuditoriaInput
    {
        /// <summary>
        /// Orden de Servicio
        /// </summary>
        [DBParameter("NORDTR", DBDataType.Numeric)]
        public decimal NORDTR { set; get; }
        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("SESFAC", DBDataType.VarChar)]
        public string SESFAC { set; get; }
        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("OBSERV", DBDataType.VarChar)]
        public string OBSERV { set; get; }

        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("USUARIO", DBDataType.VarChar)]
        public string USUARIO { set; get; }
        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("FECCRE", DBDataType.Numeric)]
        public decimal FECCRE { set; get; }
        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("HORCRE", DBDataType.Numeric)]
        public decimal HORCRE { set; get; }
        /// <summary>
        /// ?
        /// </summary>
        [DBParameter("LIBRERIA", DBDataType.VarChar)]
        public string LIBRERIA { set; get; }

    }
}

