using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.GestionCita;
using Acceso = Ransa.AccesoDatos.GestionCita;
namespace Ransa.LogicaNegocios.GestionCita
{
    public class Cita
    {
        Acceso.Cita ejecuta = new Acceso.Cita();
        public string Acciones(Entidad.CargarCitaQueryInput Parametros)
        {
            string Resultado = "";
            Resultado = ejecuta.Acciones(Parametros);
            return Resultado;
        }
        
       public string AccionesAlert(Entidad.AlertaQueryInput Parametros)
        {
            string Resultado = "";
            Resultado = ejecuta.AccionesAlert(Parametros);
            return Resultado;
        }
        public List<Entidad.GetAlerta> GetAlerta(Entidad.GetAlertaQueryInput Parametros)
        {
            return ejecuta.GetAlerta(Parametros);
        }
     
        
        public List<Entidad.ValidaCita> ValidaCita(Entidad.ValidaCitaQueryInput Parametros)
        {
            return ejecuta.ValidaCita(Parametros);
        }
        public List<Entidad.ConsultaCita> ConsultaCita(Entidad.ConsultaCitaQueryInput Parametros)
        {
            return ejecuta.ConsultaCita(Parametros);
        }
    }
}
