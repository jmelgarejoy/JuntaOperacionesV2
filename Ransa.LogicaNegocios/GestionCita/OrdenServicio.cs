using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = Ransa.Entidades.GestionCita;
using Acceso = Ransa.AccesoDatos.GestionCita;
namespace Ransa.LogicaNegocios.GestionCita
{
    public class OrdenServicio
    {
        Acceso.OrdenServicio ejecuta = new Acceso.OrdenServicio();
        public List<Entidad.OrdenServicio> ConsultaOrdenServicio(Entidad.OrdenServicioQueryInput Parametros)
        {
            return ejecuta.ConsultaOrdenServicio(Parametros);
        }
    }
}