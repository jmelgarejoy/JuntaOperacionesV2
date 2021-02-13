using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.Reporte;
using Acceso = Ransa.AccesoDatos.Reporte;
namespace Ransa.LogicaNegocios.Reporte
{
    public class Reporte
    {
        Acceso.Reporte ejecuta = new Acceso.Reporte();
        public List<Entidad.GetProcesos> ConsultaProcesos()
        {
            return ejecuta.ConsultaProcesos();
        }
        public List<Entidad.GetReportes> ConsultaReportes(Entidad.GetReportesQueryInput parametros)
        {
            return ejecuta.ConsultaReportes(parametros);
        }
        public List<Entidad.GetPlanTransporteDescarga> ConsultaDescargaAPM(Entidad.GetPlanTransporteQueryInput parametros)
        {
            return ejecuta.ConsultaDescargaAPM(parametros);
        }
        public List<Entidad.GetPlanTransporteDescarga> ConsultaDescargaDPW(Entidad.GetPlanTransporteQueryInput parametros)
        {
            return ejecuta.ConsultaDescargaDPW(parametros);
        }
        public List<Entidad.GetPlanTransporteEmbarque> ConsultaEmbarqueAPM(Entidad.GetPlanTransporteQueryInput parametros)
        {
            return ejecuta.ConsultaEmbarqueAPM(parametros);
        }
        public List<Entidad.GetPlanTransporteEmbarque> ConsultaEmbarqueDPW(Entidad.GetPlanTransporteQueryInput parametros)
        {
            return ejecuta.ConsultaEmbarqueDPW(parametros);
        }
    }
}
