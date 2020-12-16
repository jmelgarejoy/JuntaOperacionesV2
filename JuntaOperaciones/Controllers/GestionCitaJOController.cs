using System;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.GestionCita;
using Logica = Ransa.LogicaNegocios.GestionCita;
using System.Configuration;

namespace JuntaOperaciones.Controllers
{
    public class GestionCitaJOController : Controller
    {
        // GET: GestionCitaJO
        Logica.OrdenServicio lgOrdenServicio = new Logica.OrdenServicio();
        Logica.Cita lgCita = new Logica.Cita();
        // GET: CitaJO
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
        public JsonResult CargaMasiva(HttpPostedFileBase FILEARCHIVO)
        {
            string data = string.Empty;
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            try
            {
                using (var package = new ExcelPackage(FILEARCHIVO.InputStream))
                {
                    // get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCita = 3;
                    int colBooking = 4;
                    int colFecCita = 6;
                    int colOBS = 10;
                    int colCitaAnt = 11;
                    int colContendor = 7;

                    string CitaAnt;
                    string Cita;
                    for (int row = 2; worksheet.Cells[row, colCita].Value != null; row++)
                    {
                        if (worksheet.Cells[row, colCitaAnt].Value != null && worksheet.Cells[row, colCitaAnt].Value.ToString() != "")
                        {
                            CitaAnt = worksheet.Cells[row, colCitaAnt].Value.ToString();
                            List<Entidad.ValidaCita> dataCita = new List<Entidad.ValidaCita>();
                            dataCita = ValidarCita(CitaAnt, "U");
                            if (dataCita.Count == 0)
                            {
                                if (data == "")
                                { data = CitaAnt; }
                                else
                                { data = data + ", " + CitaAnt; }

                            }
                        }

                    }
                    if (data != "")
                    {
                        return Json("Las siguientes citas anteriores no existe: " + data);
                    }
                    else
                    {

                        for (int row = 2; worksheet.Cells[row, colCita].Value != null; row++)
                        {
                            if (worksheet.Cells[row, colCita].Value != null && worksheet.Cells[row, colCita].Value.ToString() != "")
                            {
                                Cita = worksheet.Cells[row, colCita].Value.ToString();
                                List<Entidad.ValidaCita> dataCita = new List<Entidad.ValidaCita>();
                                dataCita = ValidarCita(Cita, "N");
                                if (dataCita.Count > 0)
                                {
                                    if (data == "")
                                    { data = Cita; }
                                    else
                                    { data = data + ", " + Cita; }

                                }
                            }

                        }
                        if (data != "")
                        {
                            return Json("Las siguientes citas ya existe: " + data);
                        }
                        else
                        {
                            for (int row = 2; worksheet.Cells[row, colCita].Value != null; row++)
                            {
                                if (worksheet.Cells[row, colBooking].Value != null && worksheet.Cells[row, colBooking].Value.ToString() != "")
                                {
                                    string Booking;
                                    Booking = worksheet.Cells[row, colBooking].Value.ToString();
                                    List<Entidad.OrdenServicio> OrdServ = new List<Entidad.OrdenServicio>();
                                    OrdServ = TraeOrdenServicio(Booking, "U");
                                    if (OrdServ.Count == 0)
                                    {
                                        if (data == "")
                                        { data = Booking; }
                                        else
                                        { data = data + ", " + Booking; }

                                    }
                                }

                            }
                            if (data != "")
                            {
                                return Json("No se encontró una OS para el Booking – " + data);
                            }
                            else
                            {
                                for (int row = 2; worksheet.Cells[row, colCita].Value != null; row++)
                                {
                                    Entidad.CargarCitaQueryInput input = new Entidad.CargarCitaQueryInput();

                                    input.NUMID = System.Guid.NewGuid().ToString();
                                    input.NUMCITA = worksheet.Cells[row, colCita].Value.ToString();
                                    input.NUMBKG = worksheet.Cells[row, colBooking].Value.ToString();
                                    input.NROCONPLAN = worksheet.Cells[row, colContendor].Value.ToString();
                                    //-----------------------------------------------------------------
                                    List<Entidad.OrdenServicio> dataNORSRN = new List<Entidad.OrdenServicio>();

                                    Entidad.OrdenServicioQueryInput parametrosNORSRN = new Entidad.OrdenServicioQueryInput();
                                    parametrosNORSRN.NBKNCN = input.NUMBKG;
                                    parametrosNORSRN.ACCION = "U";
                                    dataNORSRN = lgOrdenServicio.ConsultaOrdenServicio(parametrosNORSRN);
                                    //-----------------------------------------------------------------
                                    if (dataNORSRN[0].NORSRN > 0)
                                    { input.NORSRN = dataNORSRN[0].NORSRN; }
                                    else
                                    { input.NORSRN = 0; }
                                    string strFecCita = worksheet.Cells[row, colFecCita].Value.ToString();
                                    strFecCita = "20" + strFecCita.Substring(6, 2) + strFecCita.Substring(3, 2) + strFecCita.Substring(0, 2);

                                    input.FECCITA = int.Parse(strFecCita);
                                    string strHoraCita = worksheet.Cells[row, colFecCita].Value.ToString();
                                    if (strHoraCita.Length == 17)
                                    {
                                        strHoraCita = strHoraCita.Substring(9, 2) + strHoraCita.Substring(12, 2) + strHoraCita.Substring(15, 2);
                                    }
                                    else
                                    {
                                        strHoraCita = strHoraCita.Substring(9, 1) + strHoraCita.Substring(11, 2) + strHoraCita.Substring(14, 2);
                                    }
                                    
                                    input.HORCITA = strHoraCita;
                                    if (worksheet.Cells[row, colCitaAnt].Value != null && worksheet.Cells[row, colCitaAnt].Value.ToString() != "")
                                    {
                                        input.CITAANT = worksheet.Cells[row, colCitaAnt].Value.ToString();
                                        input.OBSCITA = worksheet.Cells[row, colOBS].Value.ToString();
                                        input.FLGREP = "S";
                                    }
                                    else
                                    {
                                        input.FLGREP = "N";
                                    }

                                    input.ESTCITA = "P";

                                    input.OBSCITA = "";
                                    input.USRREG = (string)Session["Usuario"];
                                    input.USERMOD = (string)Session["Usuario"];
                                    input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                    input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                    input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                    input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                    input.SESTRG = "A";
                                    input.ACCION = "I";
                                    input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; ; //RUC DPW

                                    data = lgCita.Acciones(input);
                                    if (data != "OK")
                                    {
                                        data = data + input.NUMCITA + ", ";

                                    }
                                }
                            }

                        }
                        if (data != "OK")
                        {
                            return Json("Error al cargar as siguientes citas: " + data);
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
            }

            return Json(data);
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetInfoCitas(string ID, string ORDSERV, string NUMBKG, string DESDE, string HASTA, string CONTENEDOR, string ESTDAT, string ACCION)
        {

            List<Entidad.ConsultaCita> data = new List<Entidad.ConsultaCita>();

            Entidad.ConsultaCitaQueryInput parametros = new Entidad.ConsultaCitaQueryInput();
            parametros.NUMID = ID;
            if (ORDSERV != "")
            {
                parametros.NORSRN = int.Parse(ORDSERV);
            }
            else
            {
                parametros.NORSRN = 0;
            }

            parametros.NUMBKG = NUMBKG;
            parametros.FECDESDE = int.Parse(DESDE);
            parametros.FECHASTA = int.Parse(HASTA);
            parametros.NROCON = CONTENEDOR;
            parametros.ESTCITA = ESTDAT;
            parametros.OPEPORT= ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            parametros.ACCION = ACCION;
            data = lgCita.ConsultaCita(parametros);

            return Json(data); ;
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetOrdenServ(string ID, string ACCION)
        {
            List<Entidad.OrdenServicio> data = new List<Entidad.OrdenServicio>();


            data = TraeOrdenServicio(ID, ACCION);
            return Json(data); ;
        }
        public List<Entidad.OrdenServicio> TraeOrdenServicio(string ID, string ACCION)
        {
            List<Entidad.OrdenServicio> data = new List<Entidad.OrdenServicio>();
            Entidad.OrdenServicioQueryInput parametros = new Entidad.OrdenServicioQueryInput();
            parametros.NBKNCN = ID;
            parametros.ACCION = ACCION;
            data = lgOrdenServicio.ConsultaOrdenServicio(parametros);
            return data;
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetCita(string ID, string ACCION)
        {
            List<Entidad.ValidaCita> data = new List<Entidad.ValidaCita>();
            data = ValidarCita(ID, ACCION);
            return Json(data);
        }

        public List<Entidad.ValidaCita> ValidarCita(string ID, string ACCION)
        {
            List<Entidad.ValidaCita> data = new List<Entidad.ValidaCita>();

            Entidad.ValidaCitaQueryInput parametros = new Entidad.ValidaCitaQueryInput();
            parametros.NUMCITA = ID;
            parametros.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"] ; //RUC DPW
            parametros.ACCION = ACCION;
            data = lgCita.ValidaCita(parametros);
            return data;
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult AccionesCita(Entidad.CargarCitaQueryInput form)
        {
            Entidad.CargarCitaQueryInput input = new Entidad.CargarCitaQueryInput();
            string data = string.Empty;
            if (form.ACCION == "I")
            {
                if (form.ACCION == "I")
                {
                    input.NUMID = System.Guid.NewGuid().ToString();
                }

                input.NUMCITA = form.NUMCITA;
                input.NORSRN = form.NORSRN;
                input.NUMBKG = form.NUMBKG;
                input.FECCITA = form.FECCITA;
                input.HORCITA = form.HORCITA.Substring(0, 2) + form.HORCITA.Substring(3, 2) + "00";
                input.CITAANT = form.CITAANT;
                input.ESTCITA = form.ESTCITA;
                input.FLGREP = form.FLGREP;
                input.OBSCITA = form.OBSCITA;
                input.NROCON = form.NROCON;
                input.USRREG = (string)Session["Usuario"];
                input.USERMOD = (string)Session["Usuario"];
                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                input.SESTRG = "A";
                input.ACCION = form.ACCION;
                input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"] ; //RUC DPW
                data = lgCita.Acciones(input);
                if (data == "OK")
                {


                }

            }
            else if (form.ACCION == "D")
            {
                input.USRREG = (string)Session["Usuario"];
                input.USERMOD = (string)Session["Usuario"];
                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                
                input.ACCION = form.ACCION;

                var lista = form.NUMID.Split(',');

                foreach (var item in lista)
                {

                    input.NUMID = item;
                    data = lgCita.Acciones(input);
                }
            }
            return Json(data);



        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult AlerConfig(string PRECODRY, string PRECOREF, string PRECOSTACK, string POSCODRY, string POSCOREF,string HORASLIMIT, string ACCION)
        {
            string data = string.Empty;
            List<Entidad.AlertaQueryInput> LstAlert = new List<Entidad.AlertaQueryInput>();
            Entidad.AlertaQueryInput Alert = new Entidad.AlertaQueryInput();
            if (PRECODRY != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "PRE";
                Alert.DESCALERT = "DRY";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(PRECODRY);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT= ConfigurationManager.AppSettings["OPEPORT"]; 
                LstAlert.Add(Alert);
            }
            if (PRECOREF != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "PRE";
                Alert.DESCALERT = "REF";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(PRECOREF);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                LstAlert.Add(Alert);
            }
            if (PRECOSTACK != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "PRE";
                Alert.DESCALERT = "STK";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(PRECOSTACK);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                LstAlert.Add(Alert);
            }
            if (POSCODRY != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "POS";
                Alert.DESCALERT = "DRY";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(POSCODRY);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                LstAlert.Add(Alert);
            }
            
            if (POSCOREF != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "POS";
                Alert.DESCALERT = "REF";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(POSCOREF);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                LstAlert.Add(Alert);
            }
            if (HORASLIMIT != "")
            {
                Alert = new Entidad.AlertaQueryInput();
                Alert.ACCION = ACCION;
                Alert.TIPALERT = "PRE";
                Alert.DESCALERT = "LIM";
                Alert.SESTRG = "A";
                Alert.TEMPALERT = decimal.Parse(HORASLIMIT);
                Alert.USRREG = (string)Session["Usuario"];
                Alert.USERMOD = (string)Session["Usuario"];
                Alert.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                Alert.HRSREG = (DateTime.Now.ToString("HHmmss"));
                Alert.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                Alert.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                LstAlert.Add(Alert);
            }
            
            foreach (var item in LstAlert)
            {
              
                data = lgCita.AccionesAlert(item);
                if (data == "OK")
                {


                }
            }
            return Json(data);
        }
        
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetConfigAlert( string ACCION)
        {
            List<Entidad.GetAlerta> data = new List<Entidad.GetAlerta>();
            Entidad.GetAlertaQueryInput parametros = new Entidad.GetAlertaQueryInput();
            parametros.ACCION = ACCION;
            parametros.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
            data = lgCita.GetAlerta( parametros);
            return Json(data);
        }

        
       
       
      
       
    }
}