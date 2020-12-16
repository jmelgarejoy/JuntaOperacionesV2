using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.GestionCita;
using Acceso = Ransa.AccesoDatos.GestionCita;
namespace Ransa.LogicaNegocios.GestionCita
{
    public class CitaDPW
    {
        Acceso.CitaDPW ejecuta = new Acceso.CitaDPW();
        public string Acciones(Entidad.CitaDPWQueryInput Parametros)
        {
            string Resultado = "";
            Resultado = ejecuta.Acciones(Parametros);
            return Resultado;
        }
        public List<Entidad.ConsultaCitaDPW> ConsultaCitaDPW(Entidad.ConsultaCitaDPWQueryInput Parametros)
        {
            return ejecuta.ConsultaCitaDPW(Parametros);
        }
        public List<Entidad.ConsultaContenedor> ConsultaDatosContenedor(Entidad.ConsultaContenedorQueryInput Parametros)
        {

            return ejecuta.ConsultaDatosContenedor(Parametros);
            
        }
        public List<Entidad.BKCitaAutomatica> ConsultaBKCitaAutomatica(Entidad.BKCitaAutomaticaQueryInput Parametros)
        {
            return ejecuta.ConsultaBKCitaAutomatica(Parametros);
        }
        public List<Entidad.DatosBKCitaAutomatica> ConsultaDatosBKCitaAutomatica(Entidad.DatosBKCitaAutomaticaQueryInput Parametros)
        {
            return ejecuta.ConsultaDatosBKCitaAutomatica(Parametros);
        }
        
        public List<Entidad.DatosBKCitaAsignadaAutomatica> ConsultaDatosBKCitaAsignadaAutomatica(Entidad.DatosBKCitaAutomaticaAsignadaQueryInput Parametros)
        {
            return ejecuta.ConsultaDatosBKCitaAsignadaAutomatica(Parametros);
        }
        public List<Entidad.DatosCitaCitaAutomatica> ConsultaDatosCitaCitaAutomatica(Entidad.DatosCitaCitaAutomaticaQueryInput Parametros)
        {
            return ejecuta.ConsultaDatosCitaCitaAutomatica(Parametros);
        }
        public List<Entidad.ConsultaCorreos> ConsultaCorreosCitaAutomatica(Entidad.ConsultaCorreosQueryInput Parametros)
        {
            return ejecuta.ConsultaCorreosCitaAutomatica(Parametros);
        }
        public string ActualizaRCE(Entidad.ActRCEQueryInput Parametros)
        {
            string Resultado = "";
            Resultado = ejecuta.ActualizaRCE(Parametros);
            return Resultado;
        }
    }
}