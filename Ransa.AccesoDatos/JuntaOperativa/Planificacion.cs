using System;
using Ransa.AccesoDatos.Utilitario;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ransa.Framework;
using Entidad = Ransa.Entidades.JuntaOperativa;

namespace Ransa.AccesoDatos.JuntaOperativa
{
    public class Planificacion
    {
        DataTable dtResultado;

        public List<Entidad.Planificacion> ConsultaMaestro(Entidad.PlanificacionQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PLANIFICACION_CAB", Parametros);

            return dtResultado.ToList<Entidad.Planificacion>();
        }
        public List<Entidad.PlanificacionDetalle> ConsultaDetalle(Entidad.PlanificacionDetalleQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PLANIFICACION_DET", Parametros);

            return dtResultado.ToList<Entidad.PlanificacionDetalle>();
        }
        public string AccionesMaestro(Entidad.PlanificacionInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_ACCIONES_PPJNTAOPEM", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
        public string AccionesDetalle(Entidad.PlanificacionDetalleInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_ACCIONES_PPJNTAOPED", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
    }
}
