using Newtonsoft.Json;
using Ransa.Entidades.Resultados;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.OrdenTrabajo;
using Negocio = Ransa.LogicaNegocios.Procesos;
using Modelo = JuntaOperaciones.Models;

namespace JuntaOperaciones.Controllers
{
    public class ProcesosController : Controller
    {
        Negocio.OrdenTrabajo NgOredenTrabajo = new Negocio.OrdenTrabajo();
        // GET: Procesos
        public ActionResult OrdenTra()
        {
            return View();
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetOrdenTrabajo( string fecha)
        {
            Entidad.OrdenTrabajoQueryinput input = new Entidad.OrdenTrabajoQueryinput();

            var fechaSTR = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
            input.FECHA = fechaSTR;
            input.LIBRERIA = (string)Session["Libreria"];
            List<Entidad.OrdenTrabajo> InputOrdenTrabajo = new List<Entidad.OrdenTrabajo>();

            InputOrdenTrabajo = NgOredenTrabajo.Consulta(input);

            return Json(InputOrdenTrabajo);
        }

        //[HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        //public JsonResult GetDespachadores(int codAgen)
        //{

        //    List<Entidad.Despachador> Resultado = new List<Entidad.Despachador>();

        //    Resultado = NgOredenTrabajo.ConsultaDespachador(codAgen);

        //    return Json(Resultado);
        //}

        //[HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        //public JsonResult GetServicios(int tipo)
        //{

        //    List<Entidad.Servicios> Resultado = new List<Entidad.Servicios>();

        //    Resultado = NgOredenTrabajo.ConsultaServicios(tipo);

        //    return Json(Resultado);
        //}

        //[HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        //public JsonResult CrearOrden(List<Entidad.OrdenTrabajoInput> input)
        //{
        //    var usuario = (string)Session["Usuario"];

        //    input[0].USUARIO = usuario;
        //    string orden ="";
        //    if (input[0].OTRABAJO > 0)
        //    {
        //        orden = NgOredenTrabajo.ActualizarOrdenTrabajo(input);
        //    }
        //    else
        //    {
        //        orden = NgOredenTrabajo.CrearOrdenTrabajo(input);
        //    }


        //    Entidad.OrdenTrabajoQueryinput inputOT = new Entidad.OrdenTrabajoQueryinput();
        //    inputOT.IN_TIPO = 3;
        //    inputOT.IN_DOCUMENTO = orden;

        //    List<Entidad.OrdenTrabajo> Resultado = new List<Entidad.OrdenTrabajo>();

        //    Resultado = NgOredenTrabajo.Consulta(inputOT);

        //    return Json(Resultado);
        //}

    }
}