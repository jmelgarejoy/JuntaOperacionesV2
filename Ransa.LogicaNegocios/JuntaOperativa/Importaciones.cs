using Entidad = Ransa.Entidades.JuntaOperativa;
using Acceso = Ransa.AccesoDatos.JuntaOperativa;
using System.Collections.Generic;

namespace Ransa.LogicaNegocios.JuntaOperativa
{
    public class Importaciones
    {
        Acceso.Importaciones importaciones = new Acceso.Importaciones();

        public List<Entidad.Importaciones> Consultar(Entidad.ImportacionesQueryInput Parametros)
        {
            return importaciones.Consultar(Parametros);
        }

    }
}
