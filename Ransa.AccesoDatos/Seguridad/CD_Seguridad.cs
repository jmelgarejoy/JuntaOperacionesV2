using Ransa.Framework;
using Ransa.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ransa.AccesoDatos.Utilitario;

namespace Ransa.AccesoDatos.Seguridad
{
    public class CD_SeguridadUsuarios
    {
        DataTable dtResultado;

        public List<CE_Usuario> Consultar(UsuarioQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PPUSUARIOS", Parametros);
            return dtResultado.ToList<CE_Usuario>();
        }

        public List<CE_Usuario> Consultar(UsuarioNombreQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PPUSUARIOSNAME ", Parametros);
            return dtResultado.ToList<CE_Usuario>();
        }

        public string AccionesUsuario(UsuariosInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_ACCIONES_PPUSUARIO", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }

    }
    public class CD_SeguridadModulos
    {
        DataTable dtResultado;

        public List<CE_Modulo> Consultar(ModeloQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PPMODULOS", Parametros);
            return dtResultado.ToList<CE_Modulo>();
        }


        public string AccionesModulos(ModulosInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_ACCIONES_PPMODULOS", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }

    }

    public class CD_SeguridadAccesos
    {
        DataTable dtResultado;

        public List<CE_AccesosUsuario> Consultar(AccesosQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PPACCESOUSUARIO", Parametros);
            return dtResultado.ToList<CE_AccesosUsuario>();
        }

        public List<CE_Modulo> ModulosNotUser(AccesosQueryInput Parametros)
        {
            dtResultado = DB2Helper.ExecuteDb2Query(CommandType.StoredProcedure, "SP_CONSULTA_PPACCESOUSUARIO", Parametros);
            return dtResultado.ToList<CE_Modulo>();
        }


        public string AccionesAccesos(AccesoInput Parametros)
        {
            var Resultado = DB2Helper.ExecuteDb2Scalar(CommandType.StoredProcedure, "SP_ACCIONES_PPACCESOUSUARIO", Parametros);
            if (Resultado == null) Resultado = "OK";
            return Resultado.ToString();
        }

    }
}
