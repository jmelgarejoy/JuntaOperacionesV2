using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using Entidad = Ransa.Entidades.AlertaCita;
using EntidadGC = Ransa.Entidades.GestionCita;
using Logica = Ransa.LogicaNegocios.AlertaCita;
using LogicaGC = Ransa.LogicaNegocios.GestionCita;

namespace AlertaCita
{
     class Program
    {
        static LogicaGC.CitaDPW lgCitaDPW = new LogicaGC.CitaDPW();
        static Logica.AlertaCita lgCitaAlert = new Logica.AlertaCita();
        static LogicaGC.Cita lgCita = new LogicaGC.Cita();
        static void Main(string[] args)
        {
            try
            {
                DateTime FechaActual = DateTime.Now;
                EnvioPrimeraAlertaStk(FechaActual);
                EnvioAlertaStk(FechaActual);
                EnvioPrimeraAlerta(FechaActual);
                EnvioAlerta(FechaActual);
            }
            catch (Exception ex)
            {
            }
        }
        public static void EnvioAlerta(DateTime FechaActual)
        {
            try
            {
                List<Entidad.CitasPendientesAlert> LstAlert = new List<Entidad.CitasPendientesAlert>();
                LstAlert = ConsultaPendienteAlert();
                string Mensaje = "";
                if (LstAlert.Count > 0)
                {
                    foreach (Entidad.CitasPendientesAlert item in LstAlert)
                    {
                        try
                        {
                            //TIENE FECHA DE CUTOFF
                            if (item.FCOFF > 0)
                            {
                                if (item.FECREG > 0)
                                {
                                    //TIENE ALERTA ANTERIOR
                                    DateTime FechaCAlertAnt = new DateTime();
                                    int anio, mes, dia, hora, minuto, segundo;
                                    anio = int.Parse(item.FECREG.ToString().Substring(0, 4));
                                    mes = int.Parse(item.FECREG.ToString().Substring(4, 2));
                                    dia = int.Parse(item.FECREG.ToString().Substring(6, 2));
                                    hora = int.Parse(Right("000000" + item.HRSREG.ToString(), 6).Substring(0, 2));
                                    minuto = int.Parse(Right("000000" + item.HRSREG.ToString(), 6).Substring(2, 2));
                                    segundo = int.Parse(Right("000000" + item.HRSREG.ToString(), 6).Substring(4, 2));
                                    FechaCAlertAnt = new DateTime(anio, mes, dia, hora, minuto, segundo);

                                    DateTime FechaCutOff = new DateTime();
                                    anio = int.Parse(item.FCOFF.ToString().Substring(0, 4));
                                    mes = int.Parse(item.FCOFF.ToString().Substring(4, 2));
                                    dia = int.Parse(item.FCOFF.ToString().Substring(6, 2));
                                    hora = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(0, 2));
                                    minuto = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(2, 2));
                                    segundo = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(4, 2));
                                    FechaCutOff = new DateTime(anio, mes, dia, hora, minuto, segundo);

                                    if (FechaActual < FechaCutOff.AddMinutes(3))
                                    {
                                        //previo
                                        if (FechaCAlertAnt.AddHours(1).AddMinutes(3) > FechaActual && FechaCAlertAnt.AddHours(1).AddMinutes(-3) < FechaActual)
                                        {
                                            string data = "";
                                            Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                            input.NUMID04 = System.Guid.NewGuid().ToString();
                                            input.NUMID01 = item.NUMID;
                                            input.NUMCITA = item.NUMCITA;
                                            TimeSpan span = FechaCutOff.Subtract(FechaActual);
                                            input.HRSVEN = span.Hours.ToString();
                                            input.TIPALERT = "PRE";
                                            input.USRREG = "ENV_AUT";
                                            input.USERMOD = "ENV_AUT";
                                            input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                            input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                            input.SESTRG = "A";
                                            input.ACCION = "I";
                                            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                            data = lgCitaAlert.AccionesEnvioAlert(input);
                                            Mensaje = "Estimados," +
                                                      "<br/>" +
                                                  "Faltan " + span.Hours.ToString() + " horas para el vencimiento del cut off de la cita " + item.NUMCITA +
                                                  "<br/>";

                                            AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG,input.TIPALERT);
                                        }
                                    }
                                    else
                                    {
                                        //posterior
                                        //OBTENER HORAS PREVIA AL CUTOFF
                                        decimal Halert = ObtenerHorasAlerta("POS", item.DESCALERT);
                                        if (Halert > 0)
                                        {
                                            DateTime FechaMaxAlert = FechaCutOff.AddHours(double.Parse(Halert.ToString()));
                                            if (FechaActual < FechaMaxAlert)
                                            {
                                                if (FechaCAlertAnt.AddHours(1).AddMinutes(3) > FechaActual  &&  FechaCAlertAnt.AddHours(1).AddMinutes(-3) < FechaActual)// //
                                                {
                                                    string data = "";
                                                    Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                                    input.NUMID04 = System.Guid.NewGuid().ToString();
                                                    input.NUMID01 = item.NUMID;
                                                    input.NUMCITA = item.NUMCITA;
                                                    TimeSpan span = FechaActual.Subtract(FechaCutOff);
                                                    input.HRSVEN = span.Hours.ToString();
                                                    input.TIPALERT = "POS";
                                                    input.USRREG = "ENV_AUT";
                                                    input.USERMOD = "ENV_AUT";
                                                    input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                                    input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                                    input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                                    input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                                    input.SESTRG = "A";
                                                    input.ACCION = "I";
                                                    input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                                    data = lgCitaAlert.AccionesEnvioAlert(input);
                                                    if (FechaActual.AddHours(1) > FechaMaxAlert)
                                                    {
                                                        EntidadGC.CargarCitaQueryInput input2 = new EntidadGC.CargarCitaQueryInput();
                                                        input2.NUMID = item.NUMID;
                                                        input2.ACCION = "C";
                                                        data = lgCita.Acciones(input2);
                                                    }
                                                    Mensaje = "Estimados," +
                                                    "<br/>" +
                                                        "Han pasado " + span.Hours.ToString() + " horas del vencimiento del cut off de la cita " + item.NUMCITA +
                                                    "<br/>";

                                                    AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG, input.TIPALERT);
                                                }
                                            }
                                        }
                                    }
                                }



                            }

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
          
        }
        public static void EnvioAlertaStk(DateTime FechaActual)
        {
            try
            {
                List<Entidad.CitasPendientesAlert> LstAlert = new List<Entidad.CitasPendientesAlert>();
                LstAlert=ConsultaPendienteAlertStk();
                string Mensaje = "";
                if (LstAlert.Count > 0)
                {
                    foreach (Entidad.CitasPendientesAlert item in LstAlert)
                    {
                        try
                        {
                            //TIENE FECHA DE CUTOFF
                            if (item.FCOFF > 0)
                            {
                                DateTime FechaStk = new DateTime();
                                int anio, mes, dia, hora, minuto, segundo;
                                anio = int.Parse(item.FCOFF.ToString().Substring(0, 4));
                                mes = int.Parse(item.FCOFF.ToString().Substring(4, 2));
                                dia = int.Parse(item.FCOFF.ToString().Substring(6, 2));
                                hora = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(0, 2));
                                minuto = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(2, 2));
                                segundo = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(4, 2));
                                FechaStk = new DateTime(anio, mes, dia, hora, minuto, segundo);
                                if (FechaActual > FechaStk)
                                {
                                    decimal Halert = ObtenerHorasAlerta("PRE", item.DESCALERT);
                                    if (Halert > 0)
                                    {
                                        decimal DIFHORAS;
                                        TimeSpan span = FechaActual.Subtract(FechaStk);
                                        DIFHORAS = decimal.Parse(span.Hours.ToString());
                                        if (DIFHORAS == 12 || DIFHORAS == 24)
                                        {
                                            string data = "";
                                            Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                            input.NUMID04 = System.Guid.NewGuid().ToString();
                                            input.NUMID01 = item.NUMID;
                                            input.NUMCITA = item.NUMCITA;
                                            input.HRSVEN = (DIFHORAS).ToString();
                                            input.TIPALERT = "STK";
                                            input.USRREG = "ENV_AUT";
                                            input.USERMOD = "ENV_AUT";
                                            input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                            input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                            input.SESTRG = "A";
                                            input.ACCION = "I";
                                            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                            data = lgCitaAlert.AccionesEnvioAlert(input);
                                            if (DIFHORAS == 24)
                                            {
                                                EntidadGC.CargarCitaQueryInput input2 = new EntidadGC.CargarCitaQueryInput();
                                                input2.NUMID = item.NUMID;
                                                input2.ACCION = "T";
                                                data = lgCita.Acciones(input2);
                                            }
                                            Mensaje = "Estimados," +
                                              "<br/>" +
                                                  "La fecha de la cita "+item.NUMCITA+" es "+ span.Hours.ToString() + " horas mayor que la fecha de stacking " +
                                              "<br/>";

                                            AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG, input.TIPALERT);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
            catch (Exception ex)
            { }
            
        }
        public static void EnvioPrimeraAlertaStk(DateTime FechaActual)
        {
            try
            {
                List<Entidad.CitasPendientesPrimeraAlert> LstAlert = new List<Entidad.CitasPendientesPrimeraAlert>();
                LstAlert = ConsultaPendientePrimeraAlertStk();
                string Mensaje = "";
                if (LstAlert.Count > 0)
                {
                    foreach (Entidad.CitasPendientesPrimeraAlert item in LstAlert)
                    {
                        try
                        {
                            if (item.FCOFF > 0)
                            {
                                DateTime FechaStk = new DateTime();
                                int anio, mes, dia, hora, minuto, segundo;
                                anio = int.Parse(item.FCOFF.ToString().Substring(0, 4));
                                mes = int.Parse(item.FCOFF.ToString().Substring(4, 2));
                                dia = int.Parse(item.FCOFF.ToString().Substring(6, 2));
                                hora = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(0, 2));
                                minuto = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(2, 2));
                                segundo = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(4, 2));
                                FechaStk = new DateTime(anio, mes, dia, hora, minuto, segundo);
                                if (FechaActual > FechaStk)
                                {
                                    decimal Halert = ObtenerHorasAlerta("PRE", item.DESCALERT);
                                    if (Halert > 0)
                                    {
                                        decimal DIFHORAS;
                                        TimeSpan span = FechaActual.Subtract(FechaStk);
                                        DIFHORAS = decimal.Parse(span.Hours.ToString());
                                        if (DIFHORAS > Halert)
                                        {
                                            string data = "";
                                            Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                            input.NUMID04 = System.Guid.NewGuid().ToString();
                                            input.NUMID01 = item.NUMID;
                                            input.NUMCITA = item.NUMCITA;
                                            input.HRSVEN = (DIFHORAS).ToString();
                                            input.TIPALERT = "STK";
                                            input.USRREG = "ENV_AUT";
                                            input.USERMOD = "ENV_AUT";
                                            input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                            input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                            input.SESTRG = "A";
                                            input.ACCION = "I";
                                            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                            data = lgCitaAlert.AccionesEnvioAlert(input);
                                            Mensaje = "Estimados," +
                                                "<br/>" +
                                                    "La fecha de la cita " + item.NUMCITA + " es " + span.Hours.ToString() + " horas mayor que la fecha de stacking " +
                                                "<br/>";

                                            AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG, input.TIPALERT);

                                        }
                                    }
                                }
                               
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        public static void EnvioPrimeraAlerta(DateTime FechaActual)
        {
            try
            {
                List<Entidad.CitasPendientesPrimeraAlert> LstAlert = new List<Entidad.CitasPendientesPrimeraAlert>();
                LstAlert = ConsultaPendientePrimeraAlert();
                string Mensaje="";
                if (LstAlert.Count > 0)
                {
                    foreach (Entidad.CitasPendientesPrimeraAlert item in LstAlert)
                    {
                        try
                        {
                            if (item.FCOFF > 0)
                            {
                                
                                DateTime FechaCutOff = new DateTime();
                                int anio, mes, dia, hora, minuto, segundo;
                                anio = int.Parse(item.FCOFF.ToString().Substring(0, 4));
                                mes = int.Parse(item.FCOFF.ToString().Substring(4, 2));
                                dia = int.Parse(item.FCOFF.ToString().Substring(6, 2));
                                hora = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(0, 2));
                                minuto = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(2, 2));
                                segundo = int.Parse(Right("000000" + item.HCOFF.ToString(), 6).Substring(4, 2));
                                FechaCutOff = new DateTime(anio, mes, dia, hora, minuto, segundo);
                                //OBTENER HORAS PREVIA AL CUTOFF
                                if (FechaCutOff > FechaActual)
                                {
                                    //Previa al cutoff
                                    decimal Halert = ObtenerHorasAlerta("PRE", item.DESCALERT);
                                    if (Halert > 0)
                                    {
                                        DateTime FechaAlert = FechaCutOff.AddHours(double.Parse(Halert.ToString()) * -1);
                                        if (FechaActual > FechaAlert.AddMinutes(-3))//&& FechaActual < FechaAlert.AddMinutes(3)
                                        {
                                            //Envio alerta
                                            //registra alerta
                                            string data = "";
                                            Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                            input.NUMID04 = System.Guid.NewGuid().ToString();
                                            input.NUMID01 = item.NUMID;
                                            input.NUMCITA = item.NUMCITA;
                                            TimeSpan span = FechaCutOff.Subtract(FechaActual);
                                            input.HRSVEN = span.Hours.ToString();
                                            input.TIPALERT = "PRE";
                                            input.USRREG = "ENV_AUT";
                                            input.USERMOD = "ENV_AUT";
                                            input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                            input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                            input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                            input.SESTRG = "A";
                                            input.ACCION = "I";
                                            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                            data = lgCitaAlert.AccionesEnvioAlert(input);
                                            Mensaje = "Estimados," +
                                                        "<br/>" +
                                                    "Faltan " + span.Hours.ToString() + " horas para el vencimiento del cut off de la cita " + item.NUMCITA +
                                                    "<br/>";

                                            AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG, input.TIPALERT);

                                        }

                                    }
                                }
                                else
                                {
                                    //Previa al cutoff
                                    decimal Halert = ObtenerHorasAlerta("POS", item.DESCALERT);
                                    if (Halert > 0)
                                    {
                                        DateTime FechaMaxAlert = FechaCutOff.AddHours(double.Parse(Halert.ToString()));
                                        if (FechaActual < FechaMaxAlert)
                                        {
                                          
                                                string data = "";
                                                Entidad.EnvioAlertaQueryInput input = new Entidad.EnvioAlertaQueryInput();
                                                input.NUMID04 = System.Guid.NewGuid().ToString();
                                                input.NUMID01 = item.NUMID;
                                                input.NUMCITA = item.NUMCITA;
                                                TimeSpan span = FechaMaxAlert.Subtract(FechaActual);
                                               
                                                input.HRSVEN = span.Hours.ToString();
                                                input.TIPALERT = "POS";
                                                input.USRREG = "ENV_AUT";
                                                input.USERMOD = "ENV_AUT";
                                                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                                input.SESTRG = "A";
                                                input.ACCION = "I";
                                                input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
                                                data = lgCitaAlert.AccionesEnvioAlert(input);
                                                if (span.Hours < 1)
                                                {
                                                    EntidadGC.CargarCitaQueryInput input2 = new EntidadGC.CargarCitaQueryInput();
                                                    input2.NUMID = item.NUMID;
                                                    input2.ACCION = "C";
                                                    data = lgCita.Acciones(input2);
                                                }
                                            Mensaje = "Estimados," +
                                                    "<br/>" +
                                                "Han pasado " + span.Hours.ToString() + " horas del vencimiento del cut off de la cita " + item.NUMCITA +
                                                "<br/>";

                                            AlertaCorreos(Mensaje, item.NUMCITA, item.NUMBKG, input.TIPALERT);

                                        }
                                    }
                                }
                               

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            
        }
        public static void AlertaCorreos(string Mensaje, string Cita, string Booking,string TIPALERT)
        {
            string mensaje = string.Empty;
            mensaje = "<html> " +
                        "<body>" +
                        Mensaje +
                          "<br/>" +
                            "Atentamente," +
                             "<br/>" +
                            "Sistemas Ransa" +
                        //"<img src = 'cid:" + Path.Combine("Ransa.PNG") +"' /> " +
                        "</body>" +
                      "</html>";
            MailMessage mensajeCorreo = new MailMessage()
            {
                From = new MailAddress("DepositoTemporal@ransa.net", "Deposito Temporal Ransa"),
                //From = new MailAddress("testintegracion@ransa.net", "Deposito Temporal Ransa"),
                Subject = "CITAS DPW - DATOS FALTANTES CONTENEDOR: " + Cita + " - " + Booking,
                IsBodyHtml = true,
                Body = mensaje,
                Priority = MailPriority.High,
            };

            SmtpClient servidorSMTP = new SmtpClient("smtp.office365.com");
            servidorSMTP.Port = 587; // 25;
            servidorSMTP.EnableSsl = true;
            servidorSMTP.UseDefaultCredentials = false;
            servidorSMTP.Credentials = new System.Net.NetworkCredential("Depositotemporal@ransa.net", "C0m3xR4ns4");
            EntidadGC.ConsultaCorreosQueryInput input = new EntidadGC.ConsultaCorreosQueryInput();
            List<EntidadGC.ConsultaCorreos> LstCorreos = new List<EntidadGC.ConsultaCorreos>();
            if (TIPALERT == "PRE")
            {
                input.IN_ID_SLN = "CITADPWPRV";
            }
            else if (TIPALERT == "POS")
            {
                input.IN_ID_SLN = "CITADPWPST";
            }
            else
            {
                input.IN_ID_SLN = "CITADPWSTK";
            }
            
            LstCorreos = lgCitaDPW.ConsultaCorreosCitaAutomatica(input);
            
            for (int x = 0; x < LstCorreos.Count; x++)
            {
                if (LstCorreos[x].TIP_DST.ToString() == "TO")
                {
                    mensajeCorreo.To.Add(LstCorreos[x].DIR_COR.ToString());
                }
                if (LstCorreos[x].TIP_DST.ToString() == "CC")
                {
                    mensajeCorreo.CC.Add(LstCorreos[x].DIR_COR.ToString());
                }
                if (LstCorreos[x].TIP_DST.ToString() == "BCC")
                {
                    mensajeCorreo.Bcc.Add(LstCorreos[x].DIR_COR.ToString());
                }
            }
            servidorSMTP.Send(mensajeCorreo);
        }

        public static decimal ObtenerHorasAlerta(string TipAlert, string DescAlert)
        {
            decimal result=0;
            List<Entidad.TiempoAlerta> LstAlert = new List<Entidad.TiempoAlerta>();
            Entidad.TiempoAlertaQueryInput input = new Entidad.TiempoAlertaQueryInput();
            input.DESCALERT = DescAlert;
            input.TIPALERT = TipAlert;
            input.OPEPORT= ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            LstAlert = lgCitaAlert.GetTiempoAlert(input);
            if (LstAlert.Count > 0)
            {
                result = LstAlert[0].TEMPALERT;
            }
            return result;
        }
        public static string Right(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        
        public static List<Entidad.CitasPendientesPrimeraAlert> ConsultaPendientePrimeraAlert()
        {
            List<Entidad.CitasPendientesPrimeraAlert> LstAlert = new List<Entidad.CitasPendientesPrimeraAlert>();
            Entidad.CitasPendientesAlertQueryInput input = new Entidad.CitasPendientesAlertQueryInput();
            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            LstAlert = lgCitaAlert.GetCitasPendientesPrimeraAlerts(input);
            return LstAlert;
        }
        
        public static List<Entidad.CitasPendientesPrimeraAlert> ConsultaPendientePrimeraAlertStk()
        {
            List<Entidad.CitasPendientesPrimeraAlert> LstAlert = new List<Entidad.CitasPendientesPrimeraAlert>();
            Entidad.CitasPendientesAlertQueryInput input = new Entidad.CitasPendientesAlertQueryInput();
            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            LstAlert = lgCitaAlert.GetCitasPendientesPrimeraAlertsStk(input);
            return LstAlert;
        }
        public static List<Entidad.CitasPendientesAlert> ConsultaPendienteAlert()
        {
            List<Entidad.CitasPendientesAlert> LstAlert = new List<Entidad.CitasPendientesAlert>();
            Entidad.CitasPendientesAlertQueryInput input = new Entidad.CitasPendientesAlertQueryInput();
            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            LstAlert = lgCitaAlert.GetCitasPendientesAlerts(input);
            return LstAlert;
        }
        public static List<Entidad.CitasPendientesAlert> ConsultaPendienteAlertStk()
        {
            List<Entidad.CitasPendientesAlert> LstAlert = new List<Entidad.CitasPendientesAlert>();
            Entidad.CitasPendientesAlertQueryInput input = new Entidad.CitasPendientesAlertQueryInput();
            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            LstAlert = lgCitaAlert.GetCitasPendientesAlertsStk(input);
            return LstAlert;
        }
    }
}
