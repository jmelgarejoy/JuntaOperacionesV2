using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JuntaOperaciones.Models;
using Newtonsoft.Json;
using Ransa.Entidades.Seguridad;
using Entidad = Ransa.Entidades.JuntaOperativa;
using Negocio = Ransa.LogicaNegocios.JuntaOperativa;

namespace JuntaOperaciones.Controllers
{
    public class HomeController : Controller
    {
        Negocio.DashBoard negDashBoard = new Negocio.DashBoard();
        public ActionResult Index()
        {


            var ValidarSession = (string)Session["Usuario"];
            if (ValidarSession == null)
            {
                //return RedirectToAction("Error");
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.NombreUsuario = ValidarSession;
            }

            return View();
        }
        public ActionResult Salir()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Login(int? error = 0)
        {
            ErrorUSerModel MensajeError = new ErrorUSerModel();

            if (error == 0)
            {
                MensajeError.Mensaje = "";
            }
            else if (error == 2)
            {
                MensajeError.Mensaje = "Usuario y/o Clave no deben estar vacios!.";
            }

            else
            {
                MensajeError.Mensaje = "Usuario y/o Contraseña no existe!";
            }
            return View(MensajeError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(LoginUserModel objUser)
        {
            Ransa.LogicaNegocios.Login.Usuario Validar = new Ransa.LogicaNegocios.Login.Usuario();

            if (ModelState.IsValid)
            {
                string Usuario = "";
                if (objUser.Usuario != null) Usuario = objUser.Usuario.ToString().ToUpper();

                string Clave = "";
                if (objUser.Clave != null) Clave = objUser.Clave.ToString();
                if (Usuario == "" || Clave == "")
                {
                    return RedirectToAction("Login", new { error = 2 });
                }

                var ValidadorJSON = Validar.validarUsuario(Usuario, Clave, string.Empty);

                var JsonData = JsonConvert.DeserializeObject<AccesoUsuarioOutput>(ValidadorJSON);

                if (JsonData.IDUSER != 0)
                {
                    Session["Usuario"] = objUser.Usuario.ToString().ToUpper();
                    Session["KeyAut"] = objUser.Clave.ToString();
                    Session["ROL"] = JsonData.NVLACC;
                    Session["AUTORIZ"] = JsonData.AUTORIZ;
                    Session["Accesos"] = JsonConvert.SerializeObject(JsonData.PERMISOS);
                    Session["Libreria"] = JsonData.NOMLIB;
                    Session.Timeout = 1440;
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("Login", new { error = 1 });
                }
            }
            return View(objUser);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetDashBoard(string Fecha, string Accion)
        {
            Entidad.DashBoardQueryInput input = new Entidad.DashBoardQueryInput();
            input.ACCION = Accion;
            input.FECHA = Fecha;

            List<Entidad.DashBoard> Resultado = new List<Entidad.DashBoard>();

            Resultado = negDashBoard.Consultar(input);

            var Result = Resultado.GroupBy(x => new { })
               .Select(g => new Entidad.DashBoardGroup
               {
                   CANTNAVE = g.Count(),
                   MANEXPO = g.Sum(r => r.MANEXPO),
                   RECIIMPO = g.Sum(r => r.RECIIMPO),
                   MANIMPO = g.Sum(r => r.MANIMPO),
                   ENVIEXPO = g.Sum(r => r.ENVIEXPO),
               });

            input.ACCION = "F";

            List<Entidad.DashBoard> ResulGroupDiario = new List<Entidad.DashBoard>();
            ResulGroupDiario =  negDashBoard.Consultar(input);

            Entidad.DashBoardGeneral general = new Entidad.DashBoardGeneral();
            general.Global = Result.ToList();
            general.Diario = ResulGroupDiario.ToList();

            return Json(general);
        }


    }
}
