using Ransa.AccesoDatos.Utilitario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ransa.Framework;
using Entidad = Ransa.Entidades.JuntaOperativa;


namespace Ransa.AccesoDatos.JuntaOperativa
{
    public class Importaciones

    {
        DataTable dtResultado;

        public List<Entidad.Importaciones> Consultar(Entidad.ImportacionesQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_JNTAOPEIMPO", Parametros);
            return dtResultado.ToList<Entidad.Importaciones>();
        }
    }
}
