using Ransa.AccesoDatos.Utilitario;
using Ransa.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Entidad = Ransa.Entidades.JuntaOperativa;

namespace Ransa.AccesoDatos.JuntaOperativa
{
    public class Exportaciones
    {
        DataTable dtResultado;

        public List<Entidad.Exportaciones> Consultar(Entidad.ExportacionesInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_JNTAOPEEXPO", Parametros);
            return dtResultado.ToList<Entidad.Exportaciones>();
        }
    }
}
