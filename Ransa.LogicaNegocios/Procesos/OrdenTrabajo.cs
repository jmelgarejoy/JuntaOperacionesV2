using Entidad = Ransa.Entidades.OrdenTrabajo;
using Acceso = Ransa.AccesoDatos.Procesos;
using System.Collections.Generic;


namespace Ransa.LogicaNegocios.Procesos
{
    public class OrdenTrabajo
    {
        Acceso.OrdenTrabajo ejecuta = new Acceso.OrdenTrabajo();

        public List<Entidad.ContactosCC> ConsultaContactoCC()
        {
            return ejecuta.ConsultaContactoCC();
        }
        public List<Entidad.Orden> ConsultaPorOrden(Entidad.OrdenQueryinput Parametros)
        {
            return ejecuta.ConsultaPorOrden(Parametros);
        }

        public List<Entidad.OrdenTrabajo> Consulta(Entidad.OrdenTrabajoQueryinput Parametros)
        {

            return ejecuta.Consulta(Parametros);
        }

        public List<Entidad.OrdenTrabajo> ReporteOT(Entidad.ReportesOTQueryinput Parametros)
        {

            return ejecuta.ReporteOT(Parametros);
        }

        public string Programar(Entidad.ProgramarInput Parametros)
        {

            return ejecuta.Programar(Parametros);
        }
        public string Reprogramar(Entidad.ReprogramarInput Parametros)
        {

            return ejecuta.Reprogramar(Parametros);
        }
        public string FalsoServicio(Entidad.FalsoServicioInput Parametros)
        {
            return ejecuta.FalsoServicio(Parametros);
        }
        public string LiquidarServicio(Entidad.LiquidarServicioInput Parametros)
        {

            return ejecuta.LiquidarServicio(Parametros);
        }
        public List<Entidad.ProveedoresLiq> ConsultaProveedores()
        {
            return ejecuta.ConsultaProveedores();
        }
        public List<Entidad.RecursosLiq> ConsultaRecursosLIQ(Entidad.RecursosLiqQueryinput Parametros)
        {
            return ejecuta.ConsultaRecursosLIQ(Parametros);
        }
        public List<Entidad.Historial> ConsultaHistorial(Entidad.Historialinput Parametros)
        {
            return ejecuta.ConsultaHistorial(Parametros);
        }
            public string AddRecurso(Entidad.RecursosLiqInput Parametros)
        {
            return ejecuta.AddRecurso(Parametros);
        }
        public string AuditoriaOrdenTrabajo(Entidad.OrdenTrabajoAuditoriaInput Parametros)
        {
            return ejecuta.AuditoriaOrdenTrabajo(Parametros);
        }
    }
}
