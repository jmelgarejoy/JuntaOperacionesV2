using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.AlertaCita;
using Logica = Ransa.LogicaNegocios.AlertaCita;
namespace JuntaOperaciones.Controllers
{
    public class MonitorAlertaJOController : Controller
    {
        // GET: MonitorAlertaJO
        Logica.AlertaCita lgAlerta = new Logica.AlertaCita();
        public ActionResult Index()
        {
            var ValidarSession = (string)Session["Accesos"];
            if (ValidarSession == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Accesos = ValidarSession;
            }


            return View();
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetAlertasCitas(string ID, string ORDSERV, string DESDE, string HASTA, string CITA, string BOOKING, string CONTENEDOR, string ACCION)
        {
            List<Entidad.GetAlertaCitas> data = new List<Entidad.GetAlertaCitas>();
            Entidad.GetAlertaCitasQueryInput parametros = new Entidad.GetAlertaCitasQueryInput();
            parametros.NUMID = ID;
            if (ORDSERV != "")
            {
                parametros.NORSRN = decimal.Parse(ORDSERV);
            }
            else
            {
                parametros.NORSRN = 0;
            }
            parametros.FECDESDE = decimal.Parse(DESDE);
            parametros.FECHASTA = decimal.Parse(HASTA);
            parametros.NUMCITA = CITA;
            parametros.NUMBKG = BOOKING;
            parametros.NROCON = CONTENEDOR;
            parametros.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
            parametros.ACCION = ACCION;
            data = lgAlerta.GetAlertaCita(parametros);
            return Json(data);
        }
    }
}