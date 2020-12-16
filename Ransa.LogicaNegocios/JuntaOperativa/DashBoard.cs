using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.JuntaOperativa;
using Acceso = Ransa.AccesoDatos.JuntaOperativa;

namespace Ransa.LogicaNegocios.JuntaOperativa
{
    public class DashBoard
    {

        Acceso.DashBoard ejecuta = new Acceso.DashBoard();

        public List<Entidad.DashBoard> Consultar(Entidad.DashBoardQueryInput Parametros)
        {
            return ejecuta.Consulta(Parametros);
        }
    }
}
