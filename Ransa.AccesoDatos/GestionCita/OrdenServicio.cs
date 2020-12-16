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
    public class OrdenServicio
    {
        DataTable dtResultado;
        public List<Entidad.OrdenServicio> ConsultaOrdenServicio(Entidad.OrdenServicioQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_JNTAOPE_CONSULTA_ORDENSRV", Parametros);

            return dtResultado.ToList<Entidad.OrdenServicio>();
        }
    }
}