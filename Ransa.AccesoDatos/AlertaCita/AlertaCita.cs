using Ransa.AccesoDatos.Utilitario;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.AlertaCita;
namespace Ransa.AccesoDatos.AlertaCita
{
    public class AlertaCita
    {
        DataTable dtResultado;
        public List<Entidad.CitasPendientesAlert> GetCitasPendientesAlerts(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_ALERTA", Parametros);

            return dtResultado.ToList<Entidad.CitasPendientesAlert>();
        }
        public List<Entidad.CitasPendientesAlert> GetCitasPendientesAlertsStk(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_ALERTA_STK", Parametros);

            return dtResultado.ToList<Entidad.CitasPendientesAlert>();
        }

        public List<Entidad.CitasPendientesPrimeraAlert> GetCitasPendientesPrimeraAlerts(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_PRIMERA_ALERTA", Parametros);

            return dtResultado.ToList<Entidad.CitasPendientesPrimeraAlert>();

        }
        public List<Entidad.CitasPendientesPrimeraAlert> GetCitasPendientesPrimeraAlertsStk(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_PRIMERA_ALERTA_STK", Parametros);

            return dtResultado.ToList<Entidad.CitasPendientesPrimeraAlert>();

        }
        
        public List<Entidad.TiempoAlerta> GetTiempoAlert(Entidad.TiempoAlertaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_TIEMPO_ALERTA", Parametros);

            return dtResultado.ToList<Entidad.TiempoAlerta>();
        }
        public string AccionesEnvioAlert(Entidad.EnvioAlertaQueryInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_JNTAOPE_ACCIONES_ENVIO_ALERT", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
        public List<Entidad.GetAlertaCitas> GetAlertaCita(Entidad.GetAlertaCitasQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_SEL_ALERTA_CITA", Parametros);

            return dtResultado.ToList<Entidad.GetAlertaCitas>();
        }
    }
}
