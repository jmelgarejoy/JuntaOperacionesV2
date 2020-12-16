using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.AlertaCita;
using Acceso = Ransa.AccesoDatos.AlertaCita;
namespace Ransa.LogicaNegocios.AlertaCita
{
    public class AlertaCita
    {
        Acceso.AlertaCita ejecuta = new Acceso.AlertaCita();
        public List<Entidad.CitasPendientesAlert> GetCitasPendientesAlerts(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            return ejecuta.GetCitasPendientesAlerts(Parametros);
        }
        public List<Entidad.CitasPendientesAlert> GetCitasPendientesAlertsStk(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            return ejecuta.GetCitasPendientesAlertsStk(Parametros);
        }
        public List<Entidad.CitasPendientesPrimeraAlert> GetCitasPendientesPrimeraAlerts(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            return ejecuta.GetCitasPendientesPrimeraAlerts(Parametros);
        }
        public List<Entidad.CitasPendientesPrimeraAlert> GetCitasPendientesPrimeraAlertsStk(Entidad.CitasPendientesAlertQueryInput Parametros)
        {
            return ejecuta.GetCitasPendientesPrimeraAlertsStk(Parametros);
        }
        public List<Entidad.TiempoAlerta> GetTiempoAlert(Entidad.TiempoAlertaQueryInput Parametros)
        {
            return ejecuta.GetTiempoAlert(Parametros);
        }
        public string AccionesEnvioAlert(Entidad.EnvioAlertaQueryInput Parametros)
        {
            string Resultado = "";
            Resultado = ejecuta.AccionesEnvioAlert(Parametros);
            return Resultado;
        }
        public List<Entidad.GetAlertaCitas> GetAlertaCita(Entidad.GetAlertaCitasQueryInput Parametros)
        {
            return ejecuta.GetAlertaCita(Parametros);
        }
    }
}
