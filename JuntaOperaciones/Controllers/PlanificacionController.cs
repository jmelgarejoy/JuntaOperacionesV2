using Newtonsoft.Json;
using Ransa.Entidades.Resultados;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.JuntaOperativa;
using Negocio = Ransa.LogicaNegocios.JuntaOperativa;
using Modelo = JuntaOperaciones.Models;

namespace JuntaOperaciones.Controllers
{
    public class PlanificacionController : Controller
    {
        Negocio.Planificacion negPlanificacion = new Negocio.Planificacion();
        // GET: Planificacion
        public ActionResult Index()
        {
            try
            {
                var input = new Entidad.PlanificacionQueryInput();
                input.ESTADO = "P";
                input.FECHA = 0;
                input.ACCION = "T";
                input.IDJTAOPE = "";
                var Pendientes = negPlanificacion.ConsultaMaestro(input);
                ViewBag.Pendientes = new SelectList(Pendientes, "IDJTAOPE", "IDJTAOPE");
                ViewBag.Autoriz = (int)Session["AUTORIZ"];
                return View();
            }
            catch (Exception)
            {

                return View("Error");
            }
           
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult AccionesPlanificacion(Modelo.Planificacion planificacion)
        {
            Entidad.PlanificacionInput Maestro = new Entidad.PlanificacionInput();
            Resultados resultJson = new Resultados();

            if (planificacion.ACCION == "I")
            {
                Maestro.IDJTAOPE = planificacion.IDJTAOPE;
                Maestro.USERCRE = planificacion.USERCRE;
                Maestro.FCFNPLN = Convert.ToDecimal(planificacion.FCFNPLN);
                Maestro.FCINPLN = Convert.ToDecimal(planificacion.FCINPLN);
                Maestro.FECHCRE = Convert.ToDecimal(planificacion.FECHCRE);
                Maestro.HORCRE = Convert.ToDecimal(planificacion.HORCRE);
                Maestro.HORAINI = Convert.ToDecimal(planificacion.HORAINI);
                Maestro.HORAFIN = Convert.ToDecimal(planificacion.HORAFIN);
                Maestro.ACCION = planificacion.ACCION;
                Maestro.CNTTUR1 = planificacion.CNTTUR1;
                Maestro.CNTTUR2 = planificacion.CNTTUR2;
                Maestro.CNTTUR3 = planificacion.CNTTUR3;
                Maestro.AUTH1 = planificacion.AUTH1;
                Maestro.AUTH1OBS = planificacion.AUTH1OBS;
                Maestro.AUTH2 = planificacion.AUTH2;
                Maestro.AUTH2OBS = planificacion.AUTH2OBS;
                Maestro.AUTH3 = planificacion.AUTH3;
                Maestro.AUTH3OBS = planificacion.AUTH3OBS;
                Maestro.AUTH4 = planificacion.AUTH4;
                Maestro.AUTH4OBS = planificacion.AUTH4OBS;
                Maestro.ESTADO = planificacion.ESTADO;
                List<Entidad.PlanificacionDetalleInput> DetallePlan = new List<Entidad.PlanificacionDetalleInput>();
                DetallePlan = JsonConvert.DeserializeObject<List<Entidad.PlanificacionDetalleInput>>(planificacion.DETALLE);

                var ResultMaestro = negPlanificacion.AccionesMaestro(Maestro);


                foreach (var objDetalle in DetallePlan)
                {
                    if (objDetalle.REFRIGER == "N")
                    {
                        objDetalle.REFRIGER = "NO";
                    }
                    else if (objDetalle.REFRIGER == "S")
                    {
                        objDetalle.REFRIGER = "SI";
                    }
                    Entidad.PlanificacionDetalleInput dataDetalle = new Entidad.PlanificacionDetalleInput();
                    dataDetalle.IDJTAOPE = objDetalle.IDJTAOPE;
                    dataDetalle.ORDEN = objDetalle.ORDEN;
                    dataDetalle.CODNAVE = objDetalle.CODNAVE;
                    dataDetalle.CLASE = objDetalle.CLASE;
                    dataDetalle.CONTENE = objDetalle.CONTENE;
                    dataDetalle.OPEPORTU = objDetalle.OPEPORTU;
                    dataDetalle.TAMANIO = objDetalle.TAMANIO;
                    dataDetalle.PESOMAN = objDetalle.PESOMAN;
                    dataDetalle.TIPOCONT = objDetalle.TIPOCONT;
                    dataDetalle.REFRIGER = objDetalle.REFRIGER;
                    dataDetalle.FCHFNDSC = objDetalle.FCHFNDSC;
                    dataDetalle.HORFNDSC = objDetalle.HORFNDSC;
                    dataDetalle.TIPOPLAN = objDetalle.TIPOPLAN;
                    dataDetalle.FCHCUTOFF = objDetalle.FCHCUTOFF;
                    dataDetalle.HORCUTOFF = objDetalle.HORCUTOFF;
                    dataDetalle.FCHCTOFFR = objDetalle.FCHCTOFFR;
                    dataDetalle.HORCTOFFR = objDetalle.HORCTOFFR;
                    dataDetalle.ESTADO = objDetalle.ESTADO;
                    dataDetalle.ACCION = planificacion.ACCION;
                    var ResultDetalle = negPlanificacion.AccionesDetalle(dataDetalle);
                }
                resultJson.error = false;
                resultJson.mensaje = "OK";
                resultJson.data = null;
                resultJson.total = DetallePlan.Count;

            }
            else
            {
                if(planificacion.AUTH1== (string)Session["KeyAut"])
                {
                    planificacion.AUTH1 = (string)Session["Usuario"];
                    Maestro.IDJTAOPE = planificacion.IDJTAOPE;
                    Maestro.ACCION = planificacion.ACCION;
                    Maestro.AUTH1 = planificacion.AUTH1;
                    Maestro.AUTH1OBS = planificacion.AUTH1OBS;
                    Maestro.AUTH2 = planificacion.AUTH1;
                    Maestro.AUTH2OBS = planificacion.AUTH1OBS;
                    Maestro.AUTH3 = planificacion.AUTH1;
                    Maestro.AUTH3OBS = planificacion.AUTH1OBS;
                    Maestro.AUTH4 = planificacion.AUTH1;
                    Maestro.AUTH4OBS = planificacion.AUTH1OBS;
                    Maestro.ESTADO = planificacion.ESTADO;

                    var ResultMaestro = negPlanificacion.AccionesMaestro(Maestro);
                    resultJson.error = false;
                    resultJson.mensaje = "OK";
                    resultJson.data = null;
                    resultJson.total = 0;
                }
                else
                {
                    resultJson.error = false;
                    resultJson.mensaje = "Contraseña incorrecta.";
                    resultJson.data = null;
                    resultJson.total = 0;
                }
                
            }


            return Json(resultJson);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetPlanificaciones(string IDJTAOPE, string ESTADO, long FECHA, string ACCION)
        {
            Entidad.PlanificacionQueryInput input = new Entidad.PlanificacionQueryInput();
            input.ACCION = ACCION;
            input.ESTADO = ESTADO;
            input.FECHA = FECHA;
            input.IDJTAOPE = IDJTAOPE;

            List<Entidad.Planificacion> Resultado = new List<Entidad.Planificacion>();

            Resultado = negPlanificacion.ConsultaMaestro(input);

            Entidad.PlanificacionDetalleQueryInput inputDet = new Entidad.PlanificacionDetalleQueryInput();
            inputDet.ACCION = "U";
            inputDet.ESTADO = " ";
            inputDet.IDJTAOPE = IDJTAOPE;

            List<Entidad.PlanificacionDetalle> resultadoDet = new List<Entidad.PlanificacionDetalle>();
            resultadoDet = negPlanificacion.ConsultaDetalle(inputDet);

            Entidad.PlanificacionFULL DatosFull = new Entidad.PlanificacionFULL();
            DatosFull.Simple = Resultado;
            DatosFull.Detalle = resultadoDet;

            return Json(DatosFull);
        }


    }
}