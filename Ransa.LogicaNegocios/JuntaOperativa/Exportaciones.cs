using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.JuntaOperativa;
using Acceso = Ransa.AccesoDatos.JuntaOperativa;

namespace Ransa.LogicaNegocios.JuntaOperativa
{
    public class Exportaciones
    {
        Acceso.Exportaciones exportaciones = new Acceso.Exportaciones();

        public List<Entidad.Exportaciones> Consultar(Entidad.ExportacionesInput Parametros)
        {
            return exportaciones.Consultar(Parametros);
        }
    }
}
