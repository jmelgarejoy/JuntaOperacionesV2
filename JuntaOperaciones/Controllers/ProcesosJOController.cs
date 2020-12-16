using Newtonsoft.Json;
using Ransa.Entidades.Resultados;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Entidad = Ransa.Entidades.OrdenTrabajo;
using Negocio = Ransa.LogicaNegocios.Procesos;
using Modelo = JuntaOperaciones.Models;

namespace JuntaOperaciones.Controllers
{
    public class ProcesosJOController : Controller
    {
        Negocio.OrdenTrabajo NgOredenTrabajo = new Negocio.OrdenTrabajo();
        // GET: Procesos
        public ActionResult OrdenTra()
        {
            return View();
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetOrdenTrabajo(string FECHA)
        {
            Entidad.OrdenTrabajoQueryinput input = new Entidad.OrdenTrabajoQueryinput();

            var fechaSTR = FECHA.Substring(6, 4) + FECHA.Substring(3, 2) + FECHA.Substring(0, 2);
            //var fechaSTR = Convert.ToDateTime(FECHA).ToString("yyyyMMdd");
            input.FECHA = fechaSTR;
            input.LIBRERIA= (string)Session["Libreria"];
            List<Entidad.OrdenTrabajo> DATAOrdenTrabajo = new List<Entidad.OrdenTrabajo>();

            DATAOrdenTrabajo = NgOredenTrabajo.Consulta(input);


            var GrupoOT = DATAOrdenTrabajo.GroupBy(x => new { x.OBSERVACION, x.CGRONG, x.CSRVNV, x.NORDTR, x.SERVICIO, x.ESTADO, x.CLIENTE, x.AGENTE, x.NORDN1, x.DOCUMENTO, x.CLASE, x.MOTIVO, x.NROEXPED, x.RESP, x.ESTTRANS })
          .Select(g => new Entidad.GroupOT
          {
              OBSERVACION = g.Key.OBSERVACION,
              MOTIVO = g.Key.MOTIVO,
              CLIENTE = "CLIENTE: " + g.Key.CLIENTE,
              DOCUMENTO = g.Key.DOCUMENTO,
              NORDN1 = g.Key.NORDN1,
              AGENTE = "AGENTE DE ADUANA: " + g.Key.AGENTE,
              NORDTR = g.Key.NORDTR,
              CGRONG = g.Key.CGRONG,
              CLASE = g.Key.CLASE,
              CSRVNV = g.Key.CSRVNV,
              ESTADO = g.Key.ESTADO,
              SERVICIO = g.Key.SERVICIO,
              NROEXPED = g.Key.NROEXPED,
              RESP = g.Key.RESP,
              ESTTRANS = g.Key.ESTTRANS,
              CANTSERVICIOS = g.Count()
          }).ToList();

            var GrupoOTTotales = DATAOrdenTrabajo.GroupBy(x => new { x.CSRVNV, x.SERVICIO, x.ESTADO, x.CGRONG })
       .Select(g => new Entidad.GroupOTtotales
       {
           REGIMEN = g.Key.CGRONG == "51" ? "IMPORTACIONES" : "EXPORTACIONES",
           CGRONG = g.Key.CGRONG,
           CSRVNV = g.Key.CSRVNV,
           SERVICIO = g.Key.SERVICIO,
           ESTADO = g.Key.ESTADO,
           CANTSERVICIOS = g.Count()
       }).ToList();

            var GrupoByOrden = DATAOrdenTrabajo.GroupBy(x => new { x.CGRONG, x.NORDTR, x.CSRVNV, x.SERVICIO, x.ESTADO })
        .Select(g => new Entidad.liquidacionGroupOrden
        {
            //NCRRLT = g.Key.NCRRLT,
            NORDTR = g.Key.NORDTR,
            CGRONG = g.Key.CGRONG,
            CSRVNV = g.Key.CSRVNV,
            SERVICIO = g.Key.SERVICIO,
            ESTADO = g.Key.ESTADO,
            CANTSERV = g.Sum(X => X.QSRVC),
            PESOSERV = g.Sum(X => X.PSRVC)
        }).ToList();

            Entidad.OTGlobal global = new Entidad.OTGlobal();
            global.Detallado = DATAOrdenTrabajo;
            global.Grupo = GrupoOT;
            global.GrupoByOrden = GrupoByOrden;
            global.Totales = GrupoOTTotales;
            return Json(global);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult Reprogramar(string FECHA, string ORDEN)
        {
            string Resultado = "";

            Entidad.ReprogramarInput input = new Entidad.ReprogramarInput();

            var fechaSTR = FECHA.Substring(6, 4) + FECHA.Substring(3, 2) + FECHA.Substring(0, 2);
            input.FECHA = Convert.ToDecimal(fechaSTR);
            input.ORDEN = Convert.ToDecimal(ORDEN);
            input.LIBRERIA= (string)Session["Libreria"];
            Resultado = NgOredenTrabajo.Reprogramar(input);
            if (Resultado == "OK")
            {
                DateTimeOffset do1 = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, new TimeSpan(-5, 0, 0));
                string fecha = do1.Year.ToString();
                fecha += do1.Month.ToString().Length == 1 ? '0' + do1.Month.ToString() : do1.Month.ToString();
                fecha += do1.Day.ToString().Length == 1 ? '0' + do1.Day.ToString() : do1.Day.ToString();

                long Fultac = long.Parse(fecha);
                long Hultac = long.Parse(DateTime.Now.ToString("HHmmss"));

                Entidad.OrdenTrabajoAuditoriaInput auditoria = new Entidad.OrdenTrabajoAuditoriaInput();
                auditoria.NORDTR = long.Parse(ORDEN);
                auditoria.SESFAC = "R";
                auditoria.OBSERV = "Orden Rerogramada.";
                auditoria.USUARIO = (string)Session["Usuario"];
                auditoria.FECCRE = Fultac;
                auditoria.HORCRE = Hultac;
                auditoria.LIBRERIA= (string)Session["Libreria"];
                var tempo = NgOredenTrabajo.AuditoriaOrdenTrabajo(auditoria);

                Entidad.OrdenQueryinput inputpororden = new Entidad.OrdenQueryinput();
                inputpororden.IN_TIPO = 3;
                inputpororden.IN_DOCUMENTO = ORDEN;
                inputpororden.IN_LIBRERIA= (string)Session["Libreria"];
                var data = NgOredenTrabajo.ConsultaPorOrden(inputpororden);

                var temp = data.Where(x => x.ESTADO == "R").ToList<Entidad.Orden>();

                Entidad.Mail inputMail = new Entidad.Mail();
                inputMail.Fecha = FECHA;
                inputMail.Orden = ORDEN;
                inputMail.Hora = "";
                inputMail.Tipo = "R";
                inputMail.detalle = temp;

                EnviarMail(inputMail);
            }

            return Json(Resultado);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult Programar(string FECHA, string ORDEN, string HORA)
        {
            string Resultado = "";

            Entidad.ProgramarInput input = new Entidad.ProgramarInput();

            //var fechaSTR = Convert.ToDateTime(FECHA).ToString("yyyyMMdd");
            //var horaSTR = Convert.ToDateTime(HORA).ToString("HHmmss");
            var fechaSTR = FECHA.Substring(6, 4) + FECHA.Substring(3, 2) + FECHA.Substring(0, 2);
            var horaSTR = Convert.ToDateTime(HORA).ToString("HHmmss");


            input.FECHA = Convert.ToDecimal(fechaSTR);
            input.HORA = Convert.ToDecimal(horaSTR);
            input.ORDEN = Convert.ToDecimal(ORDEN);
            input.LIBRERIA= (string)Session["Libreria"];
            Resultado = NgOredenTrabajo.Programar(input);

            if (Resultado == "OK")
            {
                DateTimeOffset do1 = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, new TimeSpan(-5, 0, 0));
                string fecha = do1.Year.ToString();
                fecha += do1.Month.ToString().Length == 1 ? '0' + do1.Month.ToString() : do1.Month.ToString();
                fecha += do1.Day.ToString().Length == 1 ? '0' + do1.Day.ToString() : do1.Day.ToString();

                long Fultac = long.Parse(fecha);
                long Hultac = long.Parse(DateTime.Now.ToString("HHmmss"));

                Entidad.OrdenTrabajoAuditoriaInput auditoria = new Entidad.OrdenTrabajoAuditoriaInput();
                auditoria.NORDTR = long.Parse(ORDEN);
                auditoria.SESFAC = "T";
                auditoria.OBSERV = "Orden Programada.";
                auditoria.USUARIO = (string)Session["Usuario"];
                auditoria.FECCRE = Fultac;
                auditoria.HORCRE = Hultac;
                auditoria.USUARIO = (string)Session["Usuario"];
                auditoria.LIBRERIA = (string)Session["Libreria"];
                var tempo = NgOredenTrabajo.AuditoriaOrdenTrabajo(auditoria);

                Entidad.OrdenQueryinput inputpororden = new Entidad.OrdenQueryinput();
                inputpororden.IN_TIPO = 3;
                inputpororden.IN_DOCUMENTO = ORDEN;
                inputpororden.IN_LIBRERIA = (string)Session["Libreria"];

                var data = NgOredenTrabajo.ConsultaPorOrden(inputpororden);

                var temp = data.Where(x => x.ESTADO == "T").ToList<Entidad.Orden>();

                Entidad.Mail inputMail = new Entidad.Mail();
                inputMail.Fecha = FECHA;
                inputMail.Orden = ORDEN;
                inputMail.Hora = HORA;
                inputMail.Tipo = "T";
                inputMail.detalle = temp;

                EnviarMail(inputMail);
            }


            return Json(Resultado);
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult FalsoServicio(string ORDEN, string CODSERV)
        {
            string Resultado = "";

            Entidad.FalsoServicioInput input = new Entidad.FalsoServicioInput();
            input.ORDEN = Convert.ToDecimal(ORDEN);
            input.CODSERV = Convert.ToDecimal(CODSERV);
            input.LIBRERIA = (string)Session["Libreria"];
            Resultado = NgOredenTrabajo.FalsoServicio(input);
            if (Resultado == "OK")
            {
                DateTimeOffset do1 = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, new TimeSpan(-5, 0, 0));
                string fecha = do1.Year.ToString();
                fecha += do1.Month.ToString().Length == 1 ? '0' + do1.Month.ToString() : do1.Month.ToString();
                fecha += do1.Day.ToString().Length == 1 ? '0' + do1.Day.ToString() : do1.Day.ToString();

                long Fultac = long.Parse(fecha);
                long Hultac = long.Parse(DateTime.Now.ToString("HHmmss"));

                Entidad.OrdenTrabajoAuditoriaInput auditoria = new Entidad.OrdenTrabajoAuditoriaInput();
                auditoria.NORDTR = long.Parse(ORDEN);
                auditoria.SESFAC = "X";                
                auditoria.OBSERV = CODSERV + ", Falso Servicio ";
                auditoria.FECCRE = Fultac;
                auditoria.HORCRE = Hultac;
                auditoria.LIBRERIA = (string)Session["Libreria"];
                var tempo = NgOredenTrabajo.AuditoriaOrdenTrabajo(auditoria);

                Entidad.OrdenQueryinput inputpororden = new Entidad.OrdenQueryinput();
                inputpororden.IN_TIPO = 3;
                inputpororden.IN_DOCUMENTO = ORDEN;
                inputpororden.IN_LIBRERIA= (string)Session["Libreria"];
                var data = NgOredenTrabajo.ConsultaPorOrden(inputpororden);

                var temp = data.Where(x => x.ESTADO == "X" && x.CODSERV == input.CODSERV).ToList<Entidad.Orden>();

                Entidad.Mail inputMail = new Entidad.Mail();
                inputMail.Fecha = "";
                inputMail.Orden = ORDEN;
                inputMail.Hora = "";
                inputMail.Tipo = "X";
                inputMail.detalle = temp;

                EnviarMail(inputMail);
            }

            return Json(Resultado);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult EnviarMail(Entidad.Mail inputMail)
        {
            string TIPO; string FECHA; string ORDEN; string HORA; string CONTACTO; string MAIL; string DOCUMENTO; string REGIMEN; string SERVICIO;
            TIPO = inputMail.Tipo;
            FECHA = inputMail.Fecha;
            HORA = inputMail.Hora;
            ORDEN = inputMail.Orden;
            MAIL = inputMail.detalle[0].EMAIL;
            CONTACTO = inputMail.detalle[0].CONTACTO;
            DOCUMENTO = inputMail.detalle[0].DOCUMENTO;
            REGIMEN = inputMail.detalle[0].REGIMEN;
            SERVICIO = inputMail.detalle[0].SERVICIO;

            string Resultado = "";
            MailService.envioCorreoSoapClient mail = new MailService.envioCorreoSoapClient();

            MailService.ArrayOfString con = new MailService.ArrayOfString();
            MailService.ArrayOfString conCC = new MailService.ArrayOfString();
            MailService.ArrayOfString conLO = new MailService.ArrayOfString();
            MailService.ArrayOfString Archivos = new MailService.ArrayOfString();

            con.Add(MAIL);
            conCC.Add("MAltunaB@ransa.net");
            //var ListaCC = NgOredenTrabajo.ConsultaContactoCC();

            //foreach (var item in ListaCC)
            //{
            //    conCC.Add(item.DIR_COR);
            //}

            var GrupoOT = inputMail.detalle.GroupBy(x => new { x.CONTENEDOR, x.CLASE, x.MOTIVO })
          .Select(g => new Entidad.ContenedoresGroup
          {
              MOTIVO = g.Key.MOTIVO,
              CONTENEDOR = g.Key.CONTENEDOR,
              CLASE = g.Key.CLASE

          }).ToList();

            string ESTADOSOLICITUD = "";
            string Hora = "";
            string fecha = "";
            if (TIPO == "R")
            {
                fecha = " para la fecha " + FECHA;
                ESTADOSOLICITUD = " REPROGRAMADA ";
            }
            else if (TIPO == "T")
            {
                ESTADOSOLICITUD = " PROGRAMADA ";
                Hora = " a las " + HORA;
                fecha = " para la fecha " + FECHA;
            }
            else if (TIPO == "X")
            {
                ESTADOSOLICITUD = " FALSO SERVICIO ";

            }

            var TipoDoc = "";

            if (REGIMEN == "IMPORTACION")
            {
                TipoDoc = " BL Nº ";
            }
            else
            {
                TipoDoc = " BOOKING Nº ";
            }

            string Asunto = "Ransa Comercial –  ORDEN DE TRABAJO Nº " + ORDEN + " Estado: " + ESTADOSOLICITUD + TipoDoc + DOCUMENTO;
            string Cuerpo = "<p>Estimado(a) Sr(ta).:  <b>" + CONTACTO + "</b></p></br></br>";

            Cuerpo += "<p> Le informamos que su No. Orden de Trabajo  " + ORDEN;

            if (TIPO != "X")
            {
                Cuerpo += " ha sido " + ESTADOSOLICITUD + fecha + Hora + "</p></br></br>";
            }
            else
            {
                Cuerpo += "el " + SERVICIO + " ha sido macado como " + ESTADOSOLICITUD + "</p></br></br>";
            }



            Cuerpo += "<table style='width: 100%;' ><thead style='background-color: #4CAF50; color: white; '><tr><td>Contenedor</td><td>Clase</td><td>Tipo de Servicio</td></tr></thead>";
            Cuerpo += "<tbody>";
            foreach (var item in GrupoOT)
            {
                Cuerpo += "<tr><td>" + item.CONTENEDOR + "</td><td>" + item.CLASE + "</td><td>" + item.MOTIVO + "</td></tr>";
            }
            Cuerpo += "</tbody></table></br></br>";

            Cuerpo += "Este correo es informativo, favor no responder a esta dirección de correo.Si requiere mayor información sobre el contenido de este mensaje, comunicarse a Servicios Depósito Temporal.</br></br>";

            Cuerpo += "<p>SERVICIOS DEPOSITO TEMPORAL</br>";
            //Cuerpo += "Cel 966 942 858 </br>";
            Cuerpo += "E-mail: MAltunaB@ransa.net</p>";


            Resultado = mail.EnvioCorreoDepositoTemporal(Asunto, Cuerpo, con, conCC, conLO, Archivos, MailService.MailPriority.Normal);
            Resultado = "OK";


            return Json(Resultado);
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult EnviarMail2(Entidad.Mail inputMail)
        {
            string TIPO; string FECHA; string ORDEN; string HORA; string CONTACTO; string MAIL; string DOCUMENTO; string REGIMEN; string SERVICIO;
            TIPO = inputMail.Tipo;
            FECHA = inputMail.Fecha;
            HORA = inputMail.Hora;
            ORDEN = inputMail.Orden;
            MAIL = inputMail.detalle[0].EMAIL;
            CONTACTO = inputMail.detalle[0].CONTACTO;
            DOCUMENTO = inputMail.detalle[0].DOCUMENTO;
            REGIMEN = inputMail.detalle[0].REGIMEN;
            SERVICIO = inputMail.detalle[0].SERVICIO;

            string Resultado = "";
            MailService.envioCorreoSoapClient mail = new MailService.envioCorreoSoapClient();

            MailService.ArrayOfString con = new MailService.ArrayOfString();
            MailService.ArrayOfString conCC = new MailService.ArrayOfString();
            MailService.ArrayOfString conLO = new MailService.ArrayOfString();
            MailService.ArrayOfString Archivos = new MailService.ArrayOfString();

            con.Add(MAIL);
            conCC.Add(MAIL);
            //var ListaCC = NgOredenTrabajo.ConsultaContactoCC();

            //foreach (var item in ListaCC)
            //{
            //    conCC.Add(item.DIR_COR);
            //}

            var GrupoOT = inputMail.detalle.GroupBy(x => new { x.CONTENEDOR, x.CLASE })
          .Select(g => new Entidad.ContenedoresGroup
          {
              CONTENEDOR = g.Key.CONTENEDOR,
              CLASE = g.Key.CLASE

          }).ToList();

            string ESTADOSOLICITUD = "";
            string Hora = "";
            string fecha = "";
            if (TIPO == "R")
            {
                fecha = " para la fecha " + FECHA;
                ESTADOSOLICITUD = " REPROGRAMADA ";
            }
            else if (TIPO == "T")
            {
                ESTADOSOLICITUD = " PROGRAMADA ";
                Hora = " a las " + HORA;
                fecha = " para la fecha " + FECHA;
            }
            else if (TIPO == "X")
            {
                ESTADOSOLICITUD = " FALSO SERVICIO ";

            }

            var TipoDoc = "";

            if (REGIMEN == "IMPORTACION")
            {
                TipoDoc = " BL Nº ";
            }
            else
            {
                TipoDoc = " BOOKING Nº ";
            }

            string Asunto = "Ransa Comercial –  ORDEN DE TRABAJO Nº " + ORDEN + " Estado: " + ESTADOSOLICITUD + TipoDoc + DOCUMENTO;
            string Cuerpo = "<p>Sres. <b>" + CONTACTO + "</b></p></br></br>";
            Cuerpo += "<p> Le informamos que el servicio solicitado fue registrado con éxito No.Orden de Trabajo" + ORDEN;
            Cuerpo += "Estaremos enviando hora de programación del servicio de su solicitud a partir de las 17:00 horas";
            Cuerpo += "</p>";
            Cuerpo += "<p> Este correo es informativo, favor no responder a esta dirección de correo. Si requiere mayor información sobre el contenido de este mensaje, comunicarse a Servicios Depósito Temporal.";
            Cuerpo += "</p>";

            if (TIPO != "X")
            {
                Cuerpo += " ha sido " + ESTADOSOLICITUD + fecha + Hora + "</p></br></br>";
            }
            else
            {
                Cuerpo += "el " + SERVICIO + " ha sido macado como " + ESTADOSOLICITUD + "</p></br></br>";
            }

            Cuerpo += "<table style='width: 100%;' ><thead style='background-color: #4CAF50; color: white; '><tr><td>Contenedor</td><td>Clase</td></tr></thead>";
            Cuerpo += "<tbody>";
            foreach (var item in GrupoOT)
            {
                Cuerpo += "<tr><td>" + item.CONTENEDOR + "</td><td>" + item.CLASE + "</td></tr>";
            }
            Cuerpo += "</tbody></table></br></br>";
            Cuerpo += "<p>Atentamente,<p></br>";
            Cuerpo += "<p>Ransa Comercial<p>";
            Resultado = mail.EnvioCorreoDepositoTemporal(Asunto, Cuerpo, con, conCC, conLO, Archivos, MailService.MailPriority.Normal);
            Resultado = "OK";


            return Json(Resultado);
        }
        public ActionResult Liquidacion()
        {
            return View();
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetOrdenTrabajoLiquidacion(string FECHA)
        {
            Entidad.OrdenTrabajoQueryinput input = new Entidad.OrdenTrabajoQueryinput();

            var fechaSTR = FECHA.Substring(6, 4) + FECHA.Substring(3, 2) + FECHA.Substring(0, 2);
            input.FECHA = fechaSTR;
            input.LIBRERIA= (string)Session["Libreria"];
            List<Entidad.OrdenTrabajo> DATAOrdenTrabajo = new List<Entidad.OrdenTrabajo>();

            DATAOrdenTrabajo = NgOredenTrabajo.Consulta(input);
            

            var Grupo = DATAOrdenTrabajo.GroupBy(x => new { x.CGRONG, x.NORDTR, x.ESTADO, x.CLIENTE, x.DOCUMENTO, x.NORDN1, x.AGENTE })
          .Select(g => new Entidad.liquidacionGroup
          {
              NORDTR = g.Key.NORDTR,
              CGRONG = g.Key.CGRONG,
              ESTADO = g.Key.ESTADO,
              DOCUMENTO = g.Key.DOCUMENTO,
              NORDN1 = g.Key.NORDN1,
              CLIENTE = g.Key.CLIENTE,
              AGENTE = g.Key.AGENTE,
              CANTSERV = g.Sum(X => X.QSRVC),
              //PesoSERV = g.Sum(X => X.PSRVC),
          }).ToList();

            var GrupoByOrden = DATAOrdenTrabajo.GroupBy(x => new { x.CGRONG, x.NORDTR, x.CSRVNV, x.SERVICIO, x.ESTADO })
          .Select(g => new Entidad.liquidacionGroupOrden
          {
              //NCRRLT = g.Key.NCRRLT,
              NORDTR = g.Key.NORDTR,
              CGRONG = g.Key.CGRONG,
              CSRVNV = g.Key.CSRVNV,
              SERVICIO = g.Key.SERVICIO,
              ESTADO = g.Key.ESTADO,
              CANTSERV = g.Sum(X => X.QSRVC),
              PESOSERV = g.Sum(X => X.PSRVC)
          }).ToList();



            Entidad.Liquidacion global = new Entidad.Liquidacion();
            global.Detalle = DATAOrdenTrabajo;
            global.Grupo = Grupo;
            global.GrupoByOrden = GrupoByOrden;

            return Json(global);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult LiquidarServicio(string ORDEN, string CODSERV, string CANTSOLI, string PESOSOLI,
            string TRF, string MONEDA, string CUADRILLA, string OBSERVACION, string CANTATEN, string PESOATEN,
             string FACTURAR, string VALORIZADO, string FINIREE, string HINIREE, string FFINREE, string HFINREE, string ZONA, string FECHA)
        {
            string Resultado = "";

            var fechaSTR = FECHA.Substring(6, 4) + FECHA.Substring(3, 2) + FECHA.Substring(0, 2);
            var HoraSTR = DateTime.Now.ToString("HHmmss");


            Entidad.LiquidarServicioInput input = new Entidad.LiquidarServicioInput();
            input.IN_NORDTR = Convert.ToDecimal(ORDEN);
            input.IN_CSRVNV = Convert.ToDecimal(CODSERV);
            input.IN_NCRRLT = 1;
            input.IN_CCMPN = "EZ";
            input.IN_CDVSN = "N";
            input.IN_CRBCTC = Convert.ToDecimal(CODSERV); //Preguntar
            input.IN_QSRVC = Convert.ToDecimal(CANTSOLI);
            input.IN_PSRVC = Convert.ToDecimal(PESOSOLI);
            input.IN_ITRFSR = Convert.ToDecimal(TRF);
            input.IN_CMNDA5 = Convert.ToDecimal(MONEDA);
            input.IN_CPRVD = "";
            input.IN_CCDRLL = Convert.ToDecimal(CUADRILLA);
            input.IN_TOBSRV = OBSERVACION;
            input.IN_QATNAN = Convert.ToDecimal(CANTATEN);
            input.IN_PATNAN = Convert.ToDecimal(PESOATEN);
            input.IN_TOBSR1 = OBSERVACION;
            input.IN_FLIQSR = Convert.ToDecimal(fechaSTR);
            input.IN_HLIQSR = Convert.ToDecimal(HoraSTR);
            input.IN_ULIQSR = (string)Session["Usuario"];
            input.IN_SESTSR = "L";
            input.IN_SESTRG = "A";
            input.IN_TOBSR = OBSERVACION;
            input.IN_SQNCB = FACTURAR;
            input.IN_SESFAC = "";
            input.IN_SVLRZ = VALORIZADO;
            input.IN_FINREE = FINIREE == "" ? 0 : Convert.ToDecimal(Convert.ToDateTime(FINIREE).ToString("yyyyMMdd"));
            input.IN_HINREE = HINIREE == "" ? 0 : Convert.ToDecimal(Convert.ToDateTime(HINIREE).ToString("HHmmss"));
            input.IN_FFIREE = FFINREE == "" ? 0 : Convert.ToDecimal(Convert.ToDateTime(FFINREE).ToString("yyyyMMdd"));
            input.IN_HFIREE = HFINREE == "" ? 0 : Convert.ToDecimal(Convert.ToDateTime(HFINREE).ToString("HHmmss"));
            input.IN_CZNLLN = 1;
            input.IN_LIBRERIA = (string)Session["Libreria"];
            Resultado = NgOredenTrabajo.LiquidarServicio(input);
            if (Resultado == "OK")
            {
                DateTimeOffset do1 = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, new TimeSpan(-5, 0, 0));
                string fecha = do1.Year.ToString();
                fecha += do1.Month.ToString().Length == 1 ? '0' + do1.Month.ToString() : do1.Month.ToString();
                fecha += do1.Day.ToString().Length == 1 ? '0' + do1.Day.ToString() : do1.Day.ToString();

                long Fultac = long.Parse(fecha);
                long Hultac = long.Parse(DateTime.Now.ToString("HHmmss"));

                Entidad.OrdenTrabajoAuditoriaInput auditoria = new Entidad.OrdenTrabajoAuditoriaInput();
                auditoria.NORDTR = long.Parse(ORDEN);
                auditoria.SESFAC = "L";
                auditoria.OBSERV = CODSERV + ", Servicio Liquidado ";
                auditoria.FECCRE = Fultac;
                auditoria.HORCRE = Hultac;
                auditoria.LIBRERIA = (string)Session["Libreria"];
                var tempo = NgOredenTrabajo.AuditoriaOrdenTrabajo(auditoria);
                
            }
            return Json(Resultado);
        }

        public ActionResult Reporte()
        {
            return View();
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetReportesOT(string DESDE, string HASTA, string SERVICIO, string DOCUMENTO, string OT, string REPORTE)
        {
            var strDesde = DESDE.Substring(6, 4) + DESDE.Substring(3, 2) + DESDE.Substring(0, 2);
            var strHasta = HASTA.Substring(6, 4) + HASTA.Substring(3, 2) + HASTA.Substring(0, 2);

            var fechadesde = DESDE != "" ? long.Parse(strDesde) : 0;
            var fechahasta = HASTA != "" ? long.Parse(strHasta) : 0;

            if (OT != "")
            {
                fechadesde = 0;
                fechahasta = 0;
            }

            Entidad.ReportesOTQueryinput input = new Entidad.ReportesOTQueryinput();
            input.DESDE = fechadesde;
            input.HASTA = fechahasta;
            input.SERVICIO = SERVICIO != "" ? long.Parse(SERVICIO) : 0;
            input.DOCUMENTO = DOCUMENTO;
            input.OT = OT != "" ? long.Parse(OT) : 0;
            input.LIBRERIA= (string)Session["Libreria"];
            List<Entidad.OrdenTrabajo> DATAOrdenTrabajo = new List<Entidad.OrdenTrabajo>();

            DATAOrdenTrabajo = NgOredenTrabajo.ReporteOT(input);

            return Json(DATAOrdenTrabajo);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetProveedores()
        {

            List<Entidad.ProveedoresLiq> data = new List<Entidad.ProveedoresLiq>();

            data = NgOredenTrabajo.ConsultaProveedores();

            return Json(data);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetDetalleRecursosLiq(string OT, string SERVICIO)
        {

            List<Entidad.RecursosLiq> data = new List<Entidad.RecursosLiq>();
            Entidad.RecursosLiqQueryinput input = new Entidad.RecursosLiqQueryinput();
            input.OT = long.Parse(OT);
            input.SERVICIO = long.Parse(SERVICIO);
            
            data = NgOredenTrabajo.ConsultaRecursosLIQ(input);

            return Json(data);
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetHistorial(string OT)
        {
            Entidad.Historialinput input = new Entidad.Historialinput();
            input.DOCUMENTO = long.Parse(OT);
            var data = NgOredenTrabajo.ConsultaHistorial(input);

            return Json(data);
        }


        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult AddRecurso(string ORDEN, string CODSERV, string CANTSER, string CODREC, string TIPOORD, string TIPOREC, string OBSERVACION)
        {
            string Resultado = "";
            var fechaSTR = DateTime.Now.ToString("yyyyMMdd");
            var HoraSTR = DateTime.Now.ToString("HHmmss");

            Entidad.RecursosLiqInput input = new Entidad.RecursosLiqInput();
            input.IN_NORDTR = Convert.ToDecimal(ORDEN);
            input.IN_CSRVNV = Convert.ToDecimal(CODSERV);
            input.IN_NCRRLT = 1;
            input.IN_CODREC = Convert.ToDecimal(CODREC);
            input.IN_TIPORD = Convert.ToDecimal(TIPOORD);
            input.IN_TIPREC = Convert.ToDecimal(TIPOREC);
            input.IN_CANTUSA = Convert.ToDecimal(CANTSER);
            input.IN_OBSERV = OBSERVACION;
            input.IN_FECCRE = Convert.ToDecimal(fechaSTR);
            input.IN_HORCRE = Convert.ToDecimal(HoraSTR);
            input.IN_USUCRE = (string)Session["Usuario"];
            input.IN_SESTRG = "A";
            input.IN_LIBRERIA= (string)Session["Libreria"];
            Resultado = NgOredenTrabajo.AddRecurso(input);

            return Json(Resultado);
        }


    }

}


