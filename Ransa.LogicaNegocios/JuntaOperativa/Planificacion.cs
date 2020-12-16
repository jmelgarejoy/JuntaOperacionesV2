using Entidad = Ransa.Entidades.JuntaOperativa;
using Acceso = Ransa.AccesoDatos.JuntaOperativa;
using System.Collections.Generic;

namespace Ransa.LogicaNegocios.JuntaOperativa
{
    public class Planificacion
    {

        Acceso.Planificacion planificacion = new Acceso.Planificacion();

        public List<Entidad.Planificacion> ConsultaMaestro(Entidad.PlanificacionQueryInput Parametros)
        {
            return planificacion.ConsultaMaestro(Parametros);
        }

        public List<Entidad.PlanificacionDetalle> ConsultaDetalle(Entidad.PlanificacionDetalleQueryInput Parametros)
        {
            return planificacion.ConsultaDetalle(Parametros);
        }

        public string AccionesMaestro(Entidad.PlanificacionInput Parametros)
        {
            string Resultado = "";
            Resultado = planificacion.AccionesMaestro(Parametros);
            return Resultado;
        }

        public string AccionesDetalle(Entidad.PlanificacionDetalleInput Parametros)
        {
            string Resultado = "";
            Resultado = planificacion.AccionesDetalle(Parametros);
            return Resultado;
        }
    }
}
