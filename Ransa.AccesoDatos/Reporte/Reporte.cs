using Ransa.AccesoDatos.Utilitario;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.Reporte;
namespace Ransa.AccesoDatos.Reporte
{
    public class Reporte
    {
        DataTable dtResultado;

        public List<Entidad.GetProcesos> ConsultaProcesos()
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_REPORTE_PROCESOS");

            return dtResultado.ToList<Entidad.GetProcesos>();
        }
        public List<Entidad.GetReportes> ConsultaReportes(Entidad.GetReportesQueryInput parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_REPORTE_REPORTES", parametros);

            return dtResultado.ToList<Entidad.GetReportes>();
        }
        public List<Entidad.GetPlanTransporteDescarga> ConsultaDescargaAPM(Entidad.GetPlanTransporteQueryInput parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_REPORTE_DESCARGA_APM", parametros);

            return dtResultado.ToList<Entidad.GetPlanTransporteDescarga>();
        }
        public List<Entidad.GetPlanTransporteDescarga> ConsultaDescargaDPW(Entidad.GetPlanTransporteQueryInput parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_REPORTE_DESCARGA_DPW", parametros);

            return dtResultado.ToList<Entidad.GetPlanTransporteDescarga>();
        }
        public List<Entidad.GetPlanTransporteEmbarque> ConsultaEmbarqueAPM(Entidad.GetPlanTransporteQueryInput parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_REPORTE_EMBARQUE_APM", parametros);

            return dtResultado.ToList<Entidad.GetPlanTransporteEmbarque>();
        }
        public List<Entidad.GetPlanTransporteEmbarque> ConsultaEmbarqueDPW(Entidad.GetPlanTransporteQueryInput parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_REPORTE_EMBARQUE_DPW", parametros);

            return dtResultado.ToList<Entidad.GetPlanTransporteEmbarque>();
        }



        
    }
}
