using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.GestionCita;
using Logica = Ransa.LogicaNegocios.GestionCita;
using JuntaOperaciones.WsCompletarCita;
namespace JuntaOperaciones.Controllers
{
    public class CitaDPWJOController : Controller
    {
        Logica.CitaDPW lgCita = new Logica.CitaDPW();
        
        // GET: CitaDPWJO
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
    public JsonResult AccionesCita(Entidad.CitaDPWFormInput form)
    {
        Entidad.CitaDPWQueryInput input = new Entidad.CitaDPWQueryInput();
        string data = string.Empty;
        if (form.ACCION == "I" || form.ACCION=="U")
        {        
            CompletarCitasExtraportuarioSoapClient Cita = new CompletarCitasExtraportuarioSoapClient();
            CompletarCitaRequest request = new CompletarCitaRequest();
            CompletarCitaRequestBody requestBody = new CompletarCitaRequestBody();
            CompletarCitasExtraportuarioSoap citasExtraportuarioSoap = new CompletarCitasExtraportuarioSoapClient();
            CompletarCitaResponseBody responseBody = new CompletarCitaResponseBody();
            CompletarCitaResponse response = new CompletarCitaResponse();
            requestBody.usuario = ConfigurationManager.AppSettings["Usuario"];
            requestBody.clave = ConfigurationManager.AppSettings["Contrasena"];
            requestBody.numerocita = form.NUMCITA;
            requestBody.contenedor = form.NROCON;
            requestBody.isoType = form.ISOTYPE.Substring(7, 4);
            requestBody.placa = form.NROPLACA;
            requestBody.dni = form.DOCCHFR;
            //requestBody.ructercerizada = form.RUCEMP;
                if (!form.RUCEMP.Equals("20100039207"))
                {
                    requestBody.ructercerizada = form.RUCEMP;
                }
                requestBody.precinto1 = form.NROPREC1;
            requestBody.precinto2 = form.NROPREC2;
            requestBody.precinto3 = form.NROPREC3;
            requestBody.precinto4 = form.NROPREC4;
            requestBody.tara = form.TARA.ToString();
            requestBody.peso = form.PESO.ToString();
            request.Body = requestBody;

            response = citasExtraportuarioSoap.CompletarCita(request);
            responseBody = response.Body;
            var Result = JsonConvert.DeserializeObject<Entidad.CompletarCitaResult>(responseBody.CompletarCitaResult);
            if (Result.id == "0")
            {
                    if (form.ACCION == "I")
                    {
                        input.NUMID03 = System.Guid.NewGuid().ToString();

                    }
                    else
                    {
                        input.NUMID03 = form.NUMID03;
                    }

                input.NUMCITA = form.NUMCITA;
                input.NROCON = form.NROCON;
                input.ISOTYPE = form.ISOTYPE.Substring(7, 4);
                input.NROPLACA = form.NROPLACA;
                input.DOCCHFR = form.DOCCHFR;
                //input.RUCEMP = form.RUCEMP;
                    if (!form.RUCEMP.Equals("20100039207"))
                    {
                        input.RUCEMP = form.RUCEMP;
                    }
                    input.NROPREC1 = form.NROPREC1;
                input.NROPREC2 = form.NROPREC2;
                input.NROPREC3 = form.NROPREC3;
                input.NROPREC4 = form.NROPREC4;
                input.TARA = form.TARA;
                input.PESO = form.PESO;
                input.TIPENV = "M";
                input.RPTASERV = Result.descripcion;
                input.IDRPTASERV = "V";
                input.USRREG = (string)Session["Usuario"];
                input.USERMOD = (string)Session["Usuario"];
                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                input.SESTRG = "A";
                input.ACCION = form.ACCION;
                input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; 
                data = lgCita.Acciones(input);
                    if (data == "OK")
                    {
                        if (input.ACCION == "I")
                        {
                            ActualizaFlg(form.IDRCE, "C");
                        }
                        
                    }
            }
            else
            {
                data = Result.descripcion;
            }
            //---------------------------------------



        }

        return Json(data);



    }
        public  void ActualizaFlg(string IDRCE, string ACCION)
        {
            Entidad.ActRCEQueryInput input = new Entidad.ActRCEQueryInput();
            input.IDRCE = IDRCE;
            input.ACCION = ACCION;
            lgCita.ActualizaRCE(input);
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
    public JsonResult GetInfoCitasDPW(string NUMID03,string DESDE, string HASTA, string ACCION)
    {

        List<Entidad.ConsultaCitaDPW> data = new List<Entidad.ConsultaCitaDPW>();

        Entidad.ConsultaCitaDPWQueryInput parametros = new Entidad.ConsultaCitaDPWQueryInput();
        parametros.NUMID03 = NUMID03;
        parametros.FECDESDE = int.Parse(DESDE);
        parametros.FECHASTA = int.Parse(HASTA);
        parametros.OPEPORT= ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
        parametros.ACCION = ACCION;
        data = lgCita.ConsultaCitaDPW(parametros);

        return Json(data); ;
    }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetContenedorInfo(string ID, string ACCION)
        {

            List<Entidad.ConsultaContenedor> data = new List<Entidad.ConsultaContenedor>();

            Entidad.ConsultaContenedorQueryInput parametros = new Entidad.ConsultaContenedorQueryInput();
            parametros.NROCONTE = ID;
           parametros.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            parametros.ACCION = ACCION;
            data = lgCita.ConsultaDatosContenedor(parametros);
            
            return Json(data); ;
        }
        
    }
}