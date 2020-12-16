using Ransa.AccesoDatos.Utilitario;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.GestionCita;
namespace Ransa.AccesoDatos.GestionCita
{
    public class CitaDPW
    {
        DataTable dtResultado;
        public string Acciones(Entidad.CitaDPWQueryInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_JNTAOPE_ACCIONES_CITADPW2", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
        public List<Entidad.ConsultaCitaDPW> ConsultaCitaDPW(Entidad.ConsultaCitaDPWQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_CITADPW2", Parametros);

            return dtResultado.ToList<Entidad.ConsultaCitaDPW>();
        }
        public List<Entidad.ConsultaContenedor> ConsultaDatosContenedor(Entidad.ConsultaContenedorQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_DATCONT_COMPLETACITA", Parametros);

            return dtResultado.ToList<Entidad.ConsultaContenedor>();
        }
        public List<Entidad.BKCitaAutomatica> ConsultaBKCitaAutomatica(Entidad.BKCitaAutomaticaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_BKAUT_CITADPW", Parametros);

            return dtResultado.ToList<Entidad.BKCitaAutomatica>();
        }
        public List<Entidad.DatosBKCitaAutomatica> ConsultaDatosBKCitaAutomatica(Entidad.DatosBKCitaAutomaticaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_DATOSBK_AUT_CITADPW", Parametros);

            return dtResultado.ToList<Entidad.DatosBKCitaAutomatica>();
        }
        
              public List<Entidad.DatosBKCitaAsignadaAutomatica> ConsultaDatosBKCitaAsignadaAutomatica(Entidad.DatosBKCitaAutomaticaAsignadaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_DATOSBK_ASIGNADA_AUT_CITADPW", Parametros);

            return dtResultado.ToList<Entidad.DatosBKCitaAsignadaAutomatica>();
        }
        public List<Entidad.DatosCitaCitaAutomatica> ConsultaDatosCitaCitaAutomatica(Entidad.DatosCitaCitaAutomaticaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_DATOSCITA_AUT_CITADPW", Parametros);

            return dtResultado.ToList<Entidad.DatosCitaCitaAutomatica>();
        }
        public List<Entidad.ConsultaCorreos> ConsultaCorreosCitaAutomatica(Entidad.ConsultaCorreosQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "LIBORDAG.SP_SEL_CORREO_DESTINATARIO", Parametros);

            return dtResultado.ToList<Entidad.ConsultaCorreos>();
        }
        public string ActualizaRCE(Entidad.ActRCEQueryInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_JNTAOPE_UPD_RCE", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }


    }
}
