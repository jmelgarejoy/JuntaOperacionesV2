using System;
using Ransa.AccesoDatos.Utilitario;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ransa.Framework;
using Entidad = Ransa.Entidades.OrdenTrabajo;
using System.Configuration;

namespace Ransa.AccesoDatos.Procesos
{
    public class OrdenTrabajo
    {
        DataTable dtResultado;


        public List<Entidad.ContactosCC> ConsultaContactoCC()
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.Text, "CALL DC@RNSLIB.SP_RANSA_DT_SOLICITUD_VIRTUAL_CORREOS('SOLVP3029')", null);

            return dtResultado.ToList<Entidad.ContactosCC>();
        }
        public List<Entidad.Orden> ConsultaPorOrden(Entidad.OrdenQueryinput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_ORDENTRABAJO_TODOS", Parametros);

            return dtResultado.ToList<Entidad.Orden>();
        }
        public List<Entidad.OrdenTrabajo> Consulta(Entidad.OrdenTrabajoQueryinput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_OT_DETALLE_TODOS", Parametros);

            return dtResultado.ToList<Entidad.OrdenTrabajo>();
        }

        public List<Entidad.OrdenTrabajo> ReporteOT(Entidad.ReportesOTQueryinput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_REPORTES_OT_WEB_TODOS", Parametros);

            return dtResultado.ToList<Entidad.OrdenTrabajo>();
        }

        public string Reprogramar(Entidad.ReprogramarInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_REPROGRAMAR_ORDEN_TRABAJO_WEB_TODOS", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }

        public string Programar(Entidad.ProgramarInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_PROGRAMAR_ORDEN_TRABAJO_WEB_TODOS", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }

        public string LiquidarServicio(Entidad.LiquidarServicioInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_LIQUIDAR_ORD_TRABAJO_WEB_TODOS", Parametros);
            if (Resultado == null) Resultado = "OK";

            return Resultado.ToString();
        }
        public string FalsoServicio(Entidad.FalsoServicioInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_FALSO_SERVICIO_OT_TODOS", Parametros);
            if (Resultado == null) Resultado = "OK";

            return Resultado.ToString();
        }

        public List<Entidad.ProveedoresLiq> ConsultaProveedores()
        {
            var libreria = ConfigurationManager.AppSettings["Libreria"];

            var Resultado = DB2Helper.ExecuteDb2Query(CommandType.Text, "SELECT IDPROV, RAZCOMER FROM  " + libreria + ".PROVEELIQCAB_WEB WHERE SESTRG  != '*'", null);

            return Resultado.ToList<Entidad.ProveedoresLiq>();
        }

        public List<Entidad.RecursosLiq> ConsultaRecursosLIQ(Entidad.RecursosLiqQueryinput Parametros)
        {
            var libreria = ConfigurationManager.AppSettings["Libreria"];

            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_RECURSOSLIQ", Parametros);

            return dtResultado.ToList<Entidad.RecursosLiq>();
        }

        public List<Entidad.Historial> ConsultaHistorial(Entidad.Historialinput Parametros)
        {
            var libreria = ConfigurationManager.AppSettings["Libreria"];

            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_AUDITORIA_ORD_TRABAJO",Parametros);

            return dtResultado.ToList<Entidad.Historial>();
        }

        public string AddRecurso(Entidad.RecursosLiqInput Parametros)
        {

            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_CREAR_RECURSO_LIQ_OT_TODOS", Parametros);
            if (Resultado == null) Resultado = "OK";

            return Resultado.ToString();
        }

        public string AuditoriaOrdenTrabajo(Entidad.OrdenTrabajoAuditoriaInput Parametros)
        {

            Entidad.OrdenTrabajoAuditoriaInput det = new Entidad.OrdenTrabajoAuditoriaInput();
            det.NORDTR = Parametros.NORDTR;
            det.SESFAC = Parametros.SESFAC;
            det.OBSERV = Parametros.OBSERV;
            det.USUARIO = Parametros.USUARIO;
            det.FECCRE = Parametros.FECCRE;
            det.HORCRE = Parametros.HORCRE;
            det.LIBRERIA = Parametros.LIBRERIA;
            var strdata = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_REGISTRAR_AUDITORIA_ORD_TRABAJO_TODOS", det);
            if (strdata == null) strdata = "OK";

            return strdata.ToString();
        }
    }
}
