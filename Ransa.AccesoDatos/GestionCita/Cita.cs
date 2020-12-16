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
    public class Cita
    {
        DataTable dtResultado;
        public string Acciones(Entidad.CargarCitaQueryInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_JNTAOPE_ACCIONES_CITA2", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
        public string AccionesAlert(Entidad.AlertaQueryInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_JNTAOPE_ACCIONES_ALERT", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }
        public List<Entidad.GetAlerta> GetAlerta(Entidad.GetAlertaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_SEL_ALERTA", Parametros);

            return dtResultado.ToList<Entidad.GetAlerta>();
        }
        
     

        public List<Entidad.ValidaCita> ValidaCita(Entidad.ValidaCitaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_VALIDA_CITA", Parametros);

            return dtResultado.ToList<Entidad.ValidaCita>();
        }
        public List<Entidad.ConsultaCita> ConsultaCita(Entidad.ConsultaCitaQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_CITA", Parametros);

            return dtResultado.ToList<Entidad.ConsultaCita>();
        }


    }
}
