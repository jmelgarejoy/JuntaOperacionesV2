using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ransa.Entidades.Seguridad
{
    #region USUARIOS
    /// <summary>
    /// Entidad de Seguridad, Usuarios
    /// </summary>
    public class CE_Usuario
    {

        /// <summary>
        /// ID del Usuario
        /// </summary>
        [DBField("IDUSER")]
        public int IDUSER { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBField("USERNM")]
        public string USERNM { get; set; }
        /// <summary>
        /// Nivel de Acceso 
        /// </summary>
        [DBField("NVLACC")]
        public string NVLACC { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("SESTRG")]
        public string SESTRG { get; set; }
        /// <summary>
        /// Autoriza
        /// </summary>
        [DBField("AUTORIZ")]
        public int AUTORIZ { get; set; }
        /// <summary>
        /// Autoriza
        /// </summary>
        [DBField("NOMLIB")]
        public string NOMLIB { get; set; }


    }

    public class UsuariosInput
    {
        /// <summary>
        /// Id del Usuario
        /// </summary>
        [DBParameter("IDUSER", DBDataType.Integer)]
        public int IDUSER { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBParameter("USERNM", DBDataType.VarChar)]
        public string USERNM { get; set; }
        /// <summary>
        /// Nivel de Acceso 
        /// </summary>
        [DBParameter("NVLACC", DBDataType.VarChar)]
        public string NVLACC { get; set; }
        /// <summary>
        /// Autoriza
        /// </summary>
        [DBParameter("AUTORIZ", DBDataType.Integer)]
        public int AUTORIZ { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("SESTRG", DBDataType.VarChar)]
        public string SESTRG { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }


    public class UsuarioQueryInput
    {
        /// <summary>
        /// Id del Usuario
        /// </summary>
        [DBParameter("IDUSER", DBDataType.Integer)]
        public int IDUSER { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class UsuarioNombreQueryInput
    {
        /// <summary>
        /// Id del Usuario
        /// </summary>
        [DBParameter("USERNM", DBDataType.VarChar)]
        public string USERNM { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBParameter("ACCION", DBDataType.Char)]
        public string ACCION { get; set; }
    }
    #endregion

    #region Modulos
    /// <summary>
    /// Entidad de Seguridad, Modulos del sistema
    /// </summary>
    public class CE_Modulo
    {
        /// <summary>
        /// ID MODULO
        /// </summary>
        [DBField("IDMDLO")]
        public decimal IDMDLO { get; set; }
        /// <summary>
        /// Nombre del Modulo
        /// </summary>
        [DBField("NMMDLO")]
        public string NMMDLO { get; set; }
        /// <summary>
        /// Nombre del MENU
        /// </summary>
        [DBField("NMMENU")]
        public string NMMENU { get; set; }
        /// <summary>
        /// Llama a la Vista
        /// </summary>
        [DBField("PPVISTA")]
        public string PPVISTA { get; set; }
        /// <summary>
        /// Llama a la Vista
        /// </summary>
        [DBField("PPCNTRL")]
        public string PPCNTRL { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("SESTRG")]
        public string SESTRG { get; set; }
    }
    public class ModulosInput
    {
        /// <summary>
        /// ID MODULO
        /// </summary>
        [DBParameter("IDMDLO", DBDataType.Integer)]
        public decimal IDMDLO { get; set; }
        /// <summary>
        /// Nombre del Modulo
        /// </summary>
        [DBParameter("NMMDLO", DBDataType.VarChar)]
        public string NMMDLO { get; set; }
        /// <summary>
        /// Nombre del MENU
        /// </summary>
        [DBParameter("NMMENU", DBDataType.VarChar)]
        public string NMMENU { get; set; }
        /// <summary>
        /// Llama a la Vista
        /// </summary>
        [DBParameter("PPVISTA", DBDataType.VarChar)]
        public string PPVISTA { get; set; }
        /// <summary>
        /// Llama al Controlador
        /// </summary>
        [DBParameter("PPCNTRL", DBDataType.VarChar)]
        public string PPCNTRL { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("SESTRG", DBDataType.VarChar)]
        public string SESTRG { get; set; }

        /// <summary>
        /// ACCION (I= Insert, U= Update, D= Delete)
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }

    public class ModeloQueryInput
    {
        /// <summary>
        /// Id del Modulo
        /// </summary>
        [DBParameter("IDMDLO", DBDataType.Integer)]
        public int IDMDLO { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBParameter("ACCION", DBDataType.Char)]
        public string ACCION { get; set; }
    }

    #endregion

    #region Accesos
    /// <summary>
    /// Entidad de Seguridad, Nivel de Acceso de Usuarios
    /// </summary>
    public class CE_AccesosUsuario
    {
        /// <summary>
        /// ID del Usuario
        /// </summary>
        [DBField("IDUSER")]
        public int IDUSER { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBField("USERNM")]
        public string USERNM { get; set; }
        /// <summary>
        /// ID MODULO
        /// </summary>
        [DBField("IDMDLO")]
        public int IDMDLO { get; set; }
        /// <summary>
        /// Nombre del Módulo
        /// </summary>
        [DBField("NMMDLO")]
        public string NMMDLO { get; set; }
        /// <summary>
        /// Nombre del Menu
        /// </summary>
        [DBField("NMMENU")]
        public string NMMENU { get; set; }
        /// <summary>
        /// Nombre de la vista
        /// </summary>
        [DBField("PPVISTA")]
        public string PPVISTA { get; set; }
        /// <summary>
        /// Nombre del Controlador
        /// </summary>
        [DBField("PPCNTRL")]
        public string PPCNTRL { get; set; }
        /// <summary>
        /// Puede Agregar? SI/NO
        /// </summary>
        [DBField("PPINSERT")]
        public string PPINSERT { get; set; }
        /// <summary>
        /// Puede Modifica? SI/NO
        /// </summary>
        [DBField("PPUPDATE")]
        public string PPUPDATE { get; set; }
        /// <summary>
        /// Puede Borrar? SI/NO
        /// </summary>
        [DBField("PPDELETE")]
        public string PPDELETE { get; set; }
        /// <summary>
        /// Puede Borrar? SI/NO
        /// </summary>
        [DBField("PPEXPORT")]
        public string PPEXPORT { get; set; }
        /// <summary>
        /// Puede Imprimir? SI/NO
        /// </summary>
        [DBField("PPPRINT")]
        public string PPPRINT { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBField("SESTRG")]
        public string SESTRG { get; set; }
    }

    public class AccesoInput
    {
        /// <summary>
        /// ID del Usuario
        /// </summary>
        [DBParameter("IDUSER", DBDataType.Integer)]
        public int IDUSER { get; set; }
        /// <summary>
        /// ID MODULO
        /// </summary>
        [DBParameter("IDMDLO", DBDataType.Integer)]
        public int IDMDLO { get; set; }
        /// <summary>
        /// Puede Agregar? SI/NO
        /// </summary>
        [DBParameter("PPINSERT", DBDataType.Integer)]
        public int PPINSERT { get; set; }
        /// <summary>
        /// Puede Modifica? SI/NO
        /// </summary>
        [DBParameter("PPUPDATE", DBDataType.Integer)]
        public int PPUPDATE { get; set; }
        /// <summary>
        /// Puede Borrar? SI/NO
        /// </summary>
        [DBParameter("PPDELETE", DBDataType.Integer)]
        public int PPDELETE { get; set; }
        /// <summary>
        /// Puede Borrar? SI/NO
        /// </summary>
        [DBParameter("PPEXPORT", DBDataType.Integer)]
        public int PPEXPORT { get; set; }
        /// <summary>
        /// Puede Imprimir? SI/NO
        /// </summary>
        [DBParameter("PPPRINT", DBDataType.Integer)]
        public int PPPRINT { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [DBParameter("SESTRG", DBDataType.VarChar)]
        public string SESTRG { get; set; }
        /// <summary>
        /// ACCION (I= Insert, U= Update, D= Delete)
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class AccesosQueryInput
    {
        /// <summary>
        /// ID del Usuario
        /// </summary>
        [DBParameter("IDUSER", DBDataType.Integer)]
        public int IDUSER { get; set; }
        /// <summary>
        /// Id del Modulo
        /// </summary>
        [DBParameter("IDMDLO", DBDataType.Integer)]
        public int IDMDLO { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }

    public class AccesoUsuarioOutput
    {
        public int IDUSER { get; set; }
        public string USERNM { get; set; }
        public string NVLACC { get; set; }
        public int AUTORIZ { get; set; }
        public string NOMLIB { get; set; }
        public List<CE_AccesosUsuario> PERMISOS { get; set; }


        public AccesoUsuarioOutput()
        {
            IDUSER = 0;
            USERNM = string.Empty;
            NVLACC = "SIN ACCESO";
            AUTORIZ = 0;
            PERMISOS = new List<CE_AccesosUsuario>();
        }


    }
    #endregion
}
