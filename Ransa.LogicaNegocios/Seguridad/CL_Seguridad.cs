using System;
using System.Collections.Generic;
using System.Text;
using Ransa.Entidades.Seguridad;
using Ransa.AccesoDatos.Seguridad;


namespace Ransa.LogicaNegocios.Seguridad
{
    public class CL_SeguridadUsuarios
    {

        CD_SeguridadUsuarios seguridadUsuarios = new CD_SeguridadUsuarios();

        public List<CE_Usuario> Consultar(UsuarioQueryInput Parametros)
        {
            return seguridadUsuarios.Consultar(Parametros);
        }

        public string AccionesUsuario(UsuariosInput Parametros)
        {
            string Resultado = "";
            Resultado = seguridadUsuarios.AccionesUsuario(Parametros);
            return Resultado;
        }

    }

    public class CL_SeguridadModulos
    {

        CD_SeguridadModulos seguridadModulos = new CD_SeguridadModulos();

        public List<CE_Modulo> Consultar(ModeloQueryInput Parametros)
        {
            return seguridadModulos.Consultar(Parametros);
        }

        public string AccionesModulos(ModulosInput Parametros)
        {
            string Resultado = "";
            Resultado = seguridadModulos.AccionesModulos(Parametros);
            return Resultado;
        }

    }
    public class CL_SeguridadAccesos
    {

        CD_SeguridadAccesos seguridadAccesos = new CD_SeguridadAccesos();

        public List<CE_AccesosUsuario> Consultar(AccesosQueryInput Parametros)
        {
            return seguridadAccesos.Consultar(Parametros);
        }

        public List<CE_Modulo> ModulosNotUser(AccesosQueryInput Parametros)
        {
            return seguridadAccesos.ModulosNotUser(Parametros);
        }

        public string AccionesAccesos(AccesoInput Parametros)
        {
            string Resultado = "";
            Resultado = seguridadAccesos.AccionesAccesos(Parametros);
            return Resultado;
        }

    }
}
