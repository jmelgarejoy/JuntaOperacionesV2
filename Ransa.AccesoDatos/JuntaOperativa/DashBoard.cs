using System;
using Ransa.AccesoDatos.Utilitario;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ransa.Framework;
using Entidad = Ransa.Entidades.JuntaOperativa;


namespace Ransa.AccesoDatos.JuntaOperativa
{
    public class DashBoard
    {
        DataTable dtResultado;

        public List<Entidad.DashBoard> Consulta(Entidad.DashBoardQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_JNTADASHBOARDM", Parametros);

            return dtResultado.ToList<Entidad.DashBoard>();
        }
    }
}
