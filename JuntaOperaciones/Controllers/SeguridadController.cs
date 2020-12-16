using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuntaOperaciones.Models;

using Ransa.LogicaNegocios.Seguridad;
using Ransa.Entidades.Seguridad;
using System.Web.Mvc;

namespace JuntaOperaciones.Controllers
{
    public class SeguridadController : Controller
    {
        CL_SeguridadUsuarios seguridadUsuarios = new CL_SeguridadUsuarios();
        CL_SeguridadModulos seguridadModulos = new CL_SeguridadModulos();
        CL_SeguridadAccesos seguridadAccesos = new CL_SeguridadAccesos();


        public ActionResult Index()
        {
           
            var ValidarSession = (string)Session["Usuario"];
            if (ValidarSession == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                ViewBag.NombreUsuario = ValidarSession;
            }

            return View();
        }
        #region Opciones para Usuario
        [HttpPost]
        public string AccionesUsuario(UsuariosModel objUser)
        {
            string mensaje = "";
            if (ModelState.IsValid)
            {
                UsuariosInput Usuario = new UsuariosInput();
                Usuario.USERNM = objUser.USUARIO.ToUpper();
                Usuario.NVLACC = objUser.NIVEL;
                Usuario.SESTRG = objUser.ESTADO;
                Usuario.IDUSER = objUser.ID;
                Usuario.AUTORIZ = objUser.AUTORIZ;
                Usuario.ACCION = objUser.ACCION;

                mensaje = seguridadUsuarios.AccionesUsuario(Usuario);
            }

            return mensaje;
        }

        [HttpPost]
        public JsonResult DeleteUsuario(UsuariosModel objUser)
        {
            UsuariosInput Usuario = new UsuariosInput();
            Usuario.USERNM = objUser.USUARIO.ToUpper();
            Usuario.NVLACC = objUser.NIVEL;
            Usuario.SESTRG = objUser.ESTADO;
            Usuario.IDUSER = objUser.ID;
            Usuario.ACCION = objUser.ACCION;

            var mensaje = seguridadUsuarios.AccionesUsuario(Usuario);
            UsuarioQueryInput parametros = new UsuarioQueryInput();
            parametros.IDUSER = 0;
            parametros.ACCION = "T";


            var TT = Json(seguridadUsuarios.Consultar(parametros));

            return TT;

        }

        [HttpPost]
        public JsonResult GetUsuarios()
        {
            List<CE_Usuario> Usuario = new List<CE_Usuario>();

            UsuarioQueryInput parametros = new UsuarioQueryInput();
            parametros.IDUSER = 0;
            parametros.ACCION = "T";
            Usuario = seguridadUsuarios.Consultar(parametros);

            var TT = Json(Usuario);

            return TT;
        }

        [HttpPost]
        public JsonResult UsuarioGetbyID(int ID)
        {
            List<CE_Usuario> Usuario = new List<CE_Usuario>();

            UsuarioQueryInput parametros = new UsuarioQueryInput();
            parametros.IDUSER = ID;
            parametros.ACCION = "U";
            Usuario = seguridadUsuarios.Consultar(parametros);

            var TT = Json(Usuario);

            return TT;
        }

        #endregion

        #region Opciones para Modulos
        [HttpPost]
        public string AccionesModulo(ModulosModel objModelo)
        {
            var mensaje = "";
            if (ModelState.IsValid)
            {
                ModulosInput Modelo = new ModulosInput();
                Modelo.IDMDLO = objModelo.ID;
                Modelo.NMMDLO = objModelo.MODULO;
                Modelo.NMMENU = objModelo.MENU;
                Modelo.PPVISTA = objModelo.VISTA;
                Modelo.PPCNTRL = objModelo.CONTROLADOR;
                Modelo.SESTRG = objModelo.ESTADO;
                Modelo.ACCION = objModelo.ACCION;

                mensaje = seguridadModulos.AccionesModulos(Modelo);
              
            }

            return mensaje;
        }

        [HttpPost]
        public JsonResult DeleteModulo(ModulosModel objModelo)
        {
            ModulosInput Modelo = new ModulosInput();
            Modelo.IDMDLO = objModelo.ID;
            Modelo.NMMDLO = objModelo.MODULO;
            Modelo.SESTRG = objModelo.ESTADO;
            Modelo.ACCION = objModelo.ACCION;

            var mensaje = seguridadModulos.AccionesModulos(Modelo);
            ModeloQueryInput parametros = new ModeloQueryInput();
            parametros.IDMDLO = 0;
            parametros.ACCION = "T";


            var TT = Json(seguridadModulos.Consultar(parametros));

            return TT;

        }

        [HttpPost]
        public JsonResult ModuloGetbyID(int ID)
        {
            List<CE_Modulo> modulo = new List<CE_Modulo>();

            ModeloQueryInput parametros = new ModeloQueryInput();
            parametros.IDMDLO = ID;
            parametros.ACCION = "U";
            modulo = seguridadModulos.Consultar(parametros);

            var TT = Json(modulo);

            return TT;
        }

        [HttpPost]
        public JsonResult GetModulos()
        {
            List<CE_Modulo> modulo = new List<CE_Modulo>();

            ModeloQueryInput parametros = new ModeloQueryInput();
            parametros.IDMDLO = 0;
            parametros.ACCION = "T";
            modulo = seguridadModulos.Consultar(parametros);

            var TT = Json(modulo);

            return TT;
        }

        #endregion

        #region Opciones para Accesos
        [HttpPost]
        public JsonResult AccesosGetbyUser(int ID)
        {
            List<CE_AccesosUsuario> accesos = new List<CE_AccesosUsuario>();

            AccesosQueryInput parametros = new AccesosQueryInput();
            parametros.IDUSER = ID;
            parametros.IDMDLO = 0;
            parametros.ACCION = "U";
            accesos = seguridadAccesos.Consultar(parametros);

            var TT = Json(accesos);

            return TT;
        }

        [HttpPost]
        public JsonResult AccesosGetbyUserModulo(int IDUSER, int IDMDLO)
        {
            List<CE_AccesosUsuario> accesos = new List<CE_AccesosUsuario>();

            AccesosQueryInput parametros = new AccesosQueryInput();
            parametros.IDUSER = IDUSER;
            parametros.IDMDLO = IDMDLO;
            parametros.ACCION = "M";
            accesos = seguridadAccesos.Consultar(parametros);

            var TT = Json(accesos);

            return TT;
        }

        [HttpPost]
        public JsonResult ModulosGetNotUser(AccesoModel objModulo)
        {
            List<CE_Modulo> modulos = new List<CE_Modulo>();

            AccesosQueryInput parametros = new AccesosQueryInput();
            parametros.IDUSER = objModulo.IDUSER;
            parametros.IDMDLO = 0;
            parametros.ACCION = "A";
            modulos = seguridadAccesos.ModulosNotUser(parametros);

            var TT = Json(modulos);

            return TT;
        }

        [HttpPost]
        public JsonResult AccionesAccesos(AccesoModel objAcceso)
        {

            AccesoInput acceso = new AccesoInput();
            acceso.IDUSER = objAcceso.IDUSER;
            acceso.IDMDLO = objAcceso.IDMODULO;
            acceso.PPINSERT = objAcceso.INSERT;
            acceso.PPUPDATE = objAcceso.UPDATE;
            acceso.PPDELETE = objAcceso.DELETE;
            acceso.PPEXPORT = objAcceso.EXPORT;
            acceso.PPPRINT = objAcceso.PRINT;
            acceso.SESTRG = objAcceso.ESTADO;
            acceso.ACCION = objAcceso.ACCION;

            var mensaje = seguridadAccesos.AccionesAccesos(acceso);

            List<CE_AccesosUsuario> accesos = new List<CE_AccesosUsuario>();
            AccesosQueryInput parametros = new AccesosQueryInput();
            parametros.IDUSER = objAcceso.IDUSER;
            parametros.IDMDLO = 0;
            parametros.ACCION = "U";
            accesos = seguridadAccesos.Consultar(parametros);

            var TT = Json(accesos);

            return TT;

        }

        [HttpPost]
        public JsonResult DeleteAcceso(AccesoModel objAcceso)
        {
            AccesoInput acceso = new AccesoInput();

            acceso.IDUSER = objAcceso.IDUSER;
            acceso.IDMDLO = objAcceso.IDMODULO;
            acceso.SESTRG = objAcceso.ESTADO;
            acceso.ACCION = objAcceso.ACCION;

            var mensaje = seguridadAccesos.AccionesAccesos(acceso);
            AccesosQueryInput parametros = new AccesosQueryInput();
            parametros.IDUSER = objAcceso.IDUSER;
            parametros.IDMDLO = 0;
            parametros.ACCION = "U";


            var TT = Json(seguridadAccesos.Consultar(parametros));

            return TT;

        }
        #endregion

    }
}