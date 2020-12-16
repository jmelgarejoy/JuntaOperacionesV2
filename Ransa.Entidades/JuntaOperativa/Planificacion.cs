using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.JuntaOperativa
{
    public class Planificacion
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("IDJTAOPE")]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Fecha de Planificación
        /// </summary>
        [DBField("FCINPLN")]
        public decimal FCINPLN { get; set; }
        /// <summary>
        /// Hora Inicio de Planificación
        /// </summary>
        [DBField("HORAINI")]
        public decimal HORAINI { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("FCFNPLN")]
        public decimal FCFNPLN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("HORAFIN")]
        public decimal HORAFIN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("CNTTUR3")]
        public decimal CNTTUR3 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("CNTTUR1")]
        public decimal CNTTUR1 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("CNTTUR2")]
        public decimal CNTTUR2 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH1")]
        public string AUTH1 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH2")]
        public string AUTH2 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH3")]
        public string AUTH3 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH4")]
        public string AUTH4 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH1OBS")]
        public string AUTH1OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH2OBS")]
        public string AUTH2OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH3OBS")]
        public string AUTH3OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("AUTH4OBS")]
        public string AUTH4OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("USERCRE")]
        public string USERCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("FECHCRE")]
        public decimal FECHCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("HORCRE")]
        public decimal HORCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("USERUPD")]
        public string USERUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("FECHUPD")]
        public decimal FECHUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("HORUPD")]
        public decimal HORUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }

    }
    public class PlanificacionQueryInput
    {
        /// <summary>
        /// NRO de Planificacion
        /// </summary>
        [DBParameter("IDJTAOPE", DBDataType.VarChar)]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Estado (P=Pendiente, A=Activo)
        /// </summary>
        [DBParameter("ESTADO", DBDataType.VarChar)]
        public string ESTADO { get; set; }
        /// <summary>
        /// Fecha Planificación
        /// </summary>
        [DBParameter("FECHA", DBDataType.Numeric)]
        public decimal FECHA { get; set; }
        /// <summary>
        /// ACCION (T=Todos, F=Fecha)
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }
    public class PlanificacionFULL
    {
        public List<Planificacion> Simple { get; set; }
        public List<PlanificacionDetalle> Detalle { get; set; }
        public PlanificacionFULL()
        {
            Simple = new List<Planificacion>();
            Detalle = new List<PlanificacionDetalle>();
        }
    }
    public class PlanificacionInput
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("IDJTAOPE", DBDataType.VarChar)]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Fecha de Planificación
        /// </summary>
        [DBParameter("FCINPLN", DBDataType.Numeric)]
        public decimal FCINPLN { get; set; }
        /// <summary>
        /// Hora Inicio de Planificación
        /// </summary>
        [DBParameter("HORAINI", DBDataType.Numeric)]
        public decimal HORAINI { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FCFNPLN", DBDataType.Numeric)]
        public decimal FCFNPLN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORAFIN", DBDataType.Numeric)]
        public decimal HORAFIN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CNTTUR3", DBDataType.Numeric)]
        public decimal CNTTUR3 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CNTTUR1", DBDataType.Numeric)]
        public decimal CNTTUR1 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CNTTUR2", DBDataType.Numeric)]
        public decimal CNTTUR2 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH1", DBDataType.VarChar)]
        public string AUTH1 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH2", DBDataType.VarChar)]
        public string AUTH2 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH3", DBDataType.VarChar)]
        public string AUTH3 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH4", DBDataType.VarChar)]
        public string AUTH4 { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("USERCRE", DBDataType.VarChar)]
        public string USERCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FECHCRE", DBDataType.Numeric)]
        public decimal FECHCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORCRE", DBDataType.Numeric)]
        public decimal HORCRE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("USERUPD", DBDataType.VarChar)]
        public string USERUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FECHUPD", DBDataType.Numeric)]
        public decimal FECHUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORUPD", DBDataType.Numeric)]
        public decimal HORUPD { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("ESTADO", DBDataType.VarChar)]
        public string ESTADO { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH1OBS", DBDataType.VarChar)]
        public string AUTH1OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH2OBS", DBDataType.VarChar)]
        public string AUTH2OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH3OBS", DBDataType.VarChar)]
        public string AUTH3OBS { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("AUTH4OBS", DBDataType.VarChar)]
        public string AUTH4OBS { get; set; }

    }
    public class PlanificacionDetalle
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("IDJTAOPE")]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBField("ORDEN")]
        public decimal ORDEN { get; set; }
        /// <summary>
        /// cod de Nave
        /// </summary>
        [DBField("CODNAVE")]
        public string CODNAVE { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("NOMNAVE")]
        public string NOMNAVE { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("CONTENE")]
        public string CONTENE { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("OPEPORTU")]
        public string OPEPORTU { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("CLASE")]
        public string CLASE { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("TAMANIO")]
        public decimal TAMANIO { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("PESOMAN")]
        public decimal PESOMAN { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("TIPOCONT")]
        public string TIPOCONT { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("REFRIGER")]
        public string REFRIGER { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("TIPOPLAN")]
        public string TIPOPLAN { get; set; }
        /// <summary>
        /// codNave de servicio
        /// </summary>
        [DBField("ESTADO")]
        public string ESTADO { get; set; }

    }
    public class PlanificacionDetalleInput
    {
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("IDJTAOPE", DBDataType.VarChar)]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("ORDEN", DBDataType.Numeric)]
        public decimal ORDEN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CODNAVE", DBDataType.VarChar)]
        public string CODNAVE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CONTENE", DBDataType.VarChar)]
        public string CONTENE { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("CLASE", DBDataType.VarChar)]
        public string CLASE { get; set; }
        /// <summary>
        /// Operador Portuario
        /// </summary>
        [DBParameter("OPEPORTU", DBDataType.VarChar)]
        public string OPEPORTU { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("TAMANIO", DBDataType.Numeric)]
        public decimal TAMANIO { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("PESOMAN", DBDataType.Numeric)]
        public decimal PESOMAN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("TIPOCONT", DBDataType.VarChar)]
        public string TIPOCONT { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("REFRIGER", DBDataType.VarChar)]
        public string REFRIGER { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FCHFNDSC", DBDataType.Numeric)]
        public Nullable<decimal> FCHFNDSC { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORFNDSC", DBDataType.Numeric)]
        public Nullable<decimal> HORFNDSC { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("TIPOPLAN", DBDataType.VarChar)]
        public string TIPOPLAN { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FCHCUTOFF", DBDataType.Numeric)]
        public Nullable<decimal> FCHCUTOFF { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORCUTOFF", DBDataType.Numeric)]
        public Nullable<decimal> HORCUTOFF { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("FCHCTOFFR", DBDataType.Numeric)]
        public Nullable<decimal> FCHCTOFFR { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("HORCTOFFR", DBDataType.Numeric)]
        public Nullable<decimal> HORCTOFFR { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("ESTADO", DBDataType.VarChar)]
        public string ESTADO { get; set; }
        /// <summary>
        /// Orden de servicio
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }
    public class PlanificacionDetalleQueryInput
    {
        /// <summary>
        /// NRO de Planificacion
        /// </summary>
        [DBParameter("IDJTAOPE", DBDataType.VarChar)]
        public string IDJTAOPE { get; set; }
        /// <summary>
        /// Estado (P=Pendiente, A=Activo)
        /// </summary>
        [DBParameter("ESTADO", DBDataType.VarChar)]
        public string ESTADO { get; set; }
        /// <summary>
        /// ACCION (T=Todos, F=Fecha)
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }

    }
}
