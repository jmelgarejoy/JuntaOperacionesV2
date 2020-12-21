using Newtonsoft.Json;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Reflection;
using Entidad = Ransa.Entidades.GestionCita;
using Logica = Ransa.LogicaNegocios.GestionCita;
namespace CompletarCitaDPW
{
    class Program
    {
        static Logica.CitaDPW lgCitaDPW = new Logica.CitaDPW();
        static Logica.Cita lgCita = new Logica.Cita();
        static void Main(string[] args)
        {
            try
            {
                List<Entidad.BKCitaAutomatica> LstBK = new List<Entidad.BKCitaAutomatica>();
                ValidarCitas();
                CompletarCitasAsignadas();
                LstBK = ConsultaBKCitaAutomatica();
                if (LstBK.Count > 0)
                {
                   
                    foreach (Entidad.BKCitaAutomatica item in LstBK)
                    {
                        try
                        {
                            Entidad.CitaDPWQueryInput input = new Entidad.CitaDPWQueryInput();
                            List<Entidad.DatosBKCitaAutomatica> LstDatosBK = new List<Entidad.DatosBKCitaAutomatica>();
                            List<Entidad.DatosCitaCitaAutomatica> LstDatosCita = new List<Entidad.DatosCitaCitaAutomatica>();
                            LstDatosBK = ConsultaDatosBK(item.NROBOOK, ConfigurationManager.AppSettings["OPEPORT"]);
                            LstDatosCita = ConsultaDatosCitas(item.NROBOOK, ConfigurationManager.AppSettings["OPEPORT"]);
                            if (LstDatosCita.Count == 0 && LstDatosBK.Count > 0)
                            {
                                for (int x = 0; x < LstDatosBK.Count; x++)
                                {
                                    if (string.IsNullOrEmpty(LstDatosBK[x].FLGALERT))
                                    {
                                        AlertaCorreos(LstDatosBK[x].NAVVIAJE, LstDatosBK[x].NROBOOK, LstDatosBK[x].NROCONTE);
                                        ActualizaFlg(LstDatosBK[x].IDRCE, "A");
                                    }
                                }
                            }
                            else
                            {
                                if (LstDatosBK.Count > LstDatosCita.Count)
                                {
                                    int ContCita = 0;
                                    for (int x = 0; x < LstDatosBK.Count; x++)
                                    {
                                        input = new Entidad.CitaDPWQueryInput();
                                        if (validarDatosBK(LstDatosBK[x]))
                                        {
                                            if (x < LstDatosCita.Count)
                                            {       //SELECCIONAR TIPO DE CONTENEDOR
                                                string TipCont = "";
                                                if (LstDatosBK[x].TIPCONT == "ST20")
                                                {
                                                    TipCont = "22GP";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "ST40")
                                                {
                                                    TipCont = "42GP";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "HC20")
                                                {
                                                    TipCont = "25GP";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "HC40")
                                                {
                                                    TipCont = "45GP";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "RE20")
                                                {
                                                    TipCont = "22RT";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "RE40")
                                                {
                                                    TipCont = "42RT";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "RH40")
                                                {
                                                    TipCont = "45RT";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "FR20")
                                                {
                                                    TipCont = "22PC";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "FR40")
                                                {
                                                    TipCont = "42PC";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "OT20")
                                                {
                                                    TipCont = "22UT";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "OT40")
                                                {
                                                    TipCont = "42UT";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "TA20")
                                                {
                                                    TipCont = "22TD";
                                                }
                                                else if (LstDatosBK[x].TIPCONT == "TA40")
                                                {
                                                    TipCont = "42TD";
                                                }

                                                ////SELECCIONAR TIPO DE CONTENEDOR
                                                WsCompletarCita.CompletarCitasExtraportuarioSoapClient Cita = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                                                WsCompletarCita.CompletarCitaRequest request = new WsCompletarCita.CompletarCitaRequest();
                                                WsCompletarCita.CompletarCitaRequestBody requestBody = new WsCompletarCita.CompletarCitaRequestBody();
                                                WsCompletarCita.CompletarCitasExtraportuarioSoap citasExtraportuarioSoap = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                                                WsCompletarCita.CompletarCitaResponseBody responseBody = new WsCompletarCita.CompletarCitaResponseBody();
                                                WsCompletarCita.CompletarCitaResponse response = new WsCompletarCita.CompletarCitaResponse();
                                                requestBody.usuario = ConfigurationManager.AppSettings["Usuario"];
                                                requestBody.clave = ConfigurationManager.AppSettings["Contrasena"];
                                                requestBody.numerocita = LstDatosCita[ContCita].NUMCITA;
                                                requestBody.contenedor = LstDatosBK[x].NROCONTE;
                                                if (TipCont != "")
                                                {
                                                    requestBody.isoType = TipCont;
                                                }
                                                else
                                                {
                                                    requestBody.isoType = "";
                                                }

                                                requestBody.placa = LstDatosBK[x].PLACAVEH;
                                                requestBody.dni = LstDatosBK[x].NDOCCHOFER;
                                                requestBody.ructercerizada = LstDatosBK[x].NRUCTRANPO;
                                                requestBody.precinto1 = LstDatosBK[x].NPRECINTO;
                                                requestBody.precinto2 = "";
                                                requestBody.precinto3 = "";
                                                requestBody.precinto4 = "";
                                                requestBody.tara = LstDatosBK[x].TARACONTE;
                                                requestBody.peso = LstDatosBK[x].PESONETO;
                                                request.Body = requestBody;

                                                response = citasExtraportuarioSoap.CompletarCita(request);
                                                responseBody = response.Body;
                                                var Result = JsonConvert.DeserializeObject<Entidad.CompletarCitaResult>(responseBody.CompletarCitaResult);
                                                if (Result.id != "")
                                                {

                                                    input.NUMID03 = System.Guid.NewGuid().ToString();
                                                    input.NUMCITA = LstDatosCita[ContCita].NUMCITA;
                                                    input.NROCON = LstDatosBK[x].NROCONTE;
                                                    if (TipCont != "")
                                                    {
                                                        input.ISOTYPE = TipCont;
                                                    }
                                                    else
                                                    {
                                                        input.ISOTYPE = "";
                                                    }

                                                    input.NROPLACA = LstDatosBK[x].PLACAVEH;
                                                    input.DOCCHFR = LstDatosBK[x].NDOCCHOFER;
                                                    input.RUCEMP = LstDatosBK[x].NRUCTRANPO;
                                                    input.NROPREC1 = LstDatosBK[x].NPRECINTO;
                                                    input.NROPREC2 = "";
                                                    input.NROPREC3 = "";
                                                    input.NROPREC4 = "";
                                                    input.TARA = decimal.Parse(LstDatosBK[x].TARACONTE);
                                                    input.PESO = decimal.Parse(LstDatosBK[x].PESONETO);
                                                    input.TIPENV = "A";
                                                    input.RPTASERV = Result.descripcion;
                                                    if (Result.id == "0")
                                                    {
                                                        input.IDRPTASERV = "V";
                                                    }
                                                    else
                                                    {
                                                        input.IDRPTASERV = "F";
                                                    }

                                                    input.USRREG = "ENV_AUT";
                                                    input.USERMOD = "ENV_AUT";
                                                    input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                                    input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                                    input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                                    input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                                    input.SESTRG = "A";
                                                    input.ACCION = "I";
                                                    input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                                                    string data = lgCitaDPW.Acciones(input);
                                                    if (data == "OK")
                                                    {
                                                        if (Result.id == "0")
                                                        {
                                                            ActualizaFlg(LstDatosBK[x].IDRCE, "C");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, data, Environment.NewLine, ""));
                                                    }

                                                    ContCita += 1;
                                                }
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(LstDatosBK[x].FLGALERT))
                                                {
                                                    AlertaCorreos(LstDatosBK[x].NAVVIAJE, LstDatosBK[x].NROBOOK, LstDatosBK[x].NROCONTE);
                                                    ActualizaFlg(LstDatosBK[x].IDRCE, "A");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    int ContCita = 0;
                                    for (int x = 0; x < LstDatosBK.Count; x++)
                                    {
                                        input = new Entidad.CitaDPWQueryInput();
                                        if (validarDatosBK(LstDatosBK[x]))
                                        {
                                            //SELECCIONAR TIPO DE CONTENEDOR
                                            string TipCont = "";
                                            if (LstDatosBK[x].TIPCONT == "ST20")
                                            {
                                                TipCont = "22GP";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "ST40")
                                            {
                                                TipCont = "42GP";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "HC20")
                                            {
                                                TipCont = "25GP";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "HC40")
                                            {
                                                TipCont = "45GP";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "RE20")
                                            {
                                                TipCont = "22RT";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "RE40")
                                            {
                                                TipCont = "42RT";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "RH40")
                                            {
                                                TipCont = "45RT";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "FR20")
                                            {
                                                TipCont = "22PC";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "FR40")
                                            {
                                                TipCont = "42PC";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "OT20")
                                            {
                                                TipCont = "22UT";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "OT40")
                                            {
                                                TipCont = "42UT";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "TA20")
                                            {
                                                TipCont = "22TD";
                                            }
                                            else if (LstDatosBK[x].TIPCONT == "TA40")
                                            {
                                                TipCont = "42TD";
                                            }

                                            ////SELECCIONAR TIPO DE CONTENEDOR
                                            WsCompletarCita.CompletarCitasExtraportuarioSoapClient Cita = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                                            WsCompletarCita.CompletarCitaRequest request = new WsCompletarCita.CompletarCitaRequest();
                                            WsCompletarCita.CompletarCitaRequestBody requestBody = new WsCompletarCita.CompletarCitaRequestBody();
                                            WsCompletarCita.CompletarCitasExtraportuarioSoap citasExtraportuarioSoap = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                                            WsCompletarCita.CompletarCitaResponseBody responseBody = new WsCompletarCita.CompletarCitaResponseBody();
                                            WsCompletarCita.CompletarCitaResponse response = new WsCompletarCita.CompletarCitaResponse();
                                            requestBody.usuario = ConfigurationManager.AppSettings["Usuario"];
                                            requestBody.clave = ConfigurationManager.AppSettings["Contrasena"];
                                            requestBody.numerocita = LstDatosCita[ContCita].NUMCITA;
                                            requestBody.contenedor = LstDatosBK[x].NROCONTE;
                                            if (TipCont != "")
                                            {
                                                requestBody.isoType = TipCont;
                                            }
                                            else
                                            {
                                                requestBody.isoType = "";
                                            }

                                            requestBody.placa = LstDatosBK[x].PLACAVEH;
                                            requestBody.dni = LstDatosBK[x].NDOCCHOFER;
                                            //requestBody.ructercerizada = LstDatosBK[x].NRUCTRANPO;
                                            if (!LstDatosBK[x].NRUCTRANPO.Equals("20100039207"))
                                            {
                                                requestBody.ructercerizada = LstDatosBK[x].NRUCTRANPO;
                                            }
                                            requestBody.precinto1 = LstDatosBK[x].NPRECINTO;
                                            requestBody.precinto2 = "";
                                            requestBody.precinto3 = "";
                                            requestBody.precinto4 = "";
                                            requestBody.tara = LstDatosBK[x].TARACONTE;
                                            requestBody.peso = LstDatosBK[x].PESONETO;
                                            request.Body = requestBody;

                                            response = citasExtraportuarioSoap.CompletarCita(request);
                                            responseBody = response.Body;
                                            var Result = JsonConvert.DeserializeObject<Entidad.CompletarCitaResult>(responseBody.CompletarCitaResult);
                                            if (Result.id != "")
                                            {
                                                input.NUMID03 = System.Guid.NewGuid().ToString();
                                                input.NUMCITA = LstDatosCita[ContCita].NUMCITA;
                                                input.NROCON = LstDatosBK[x].NROCONTE;
                                                if (TipCont != "")
                                                {
                                                    input.ISOTYPE = TipCont;
                                                }
                                                else
                                                {
                                                    input.ISOTYPE = "";
                                                }

                                                input.NROPLACA = LstDatosBK[x].PLACAVEH;
                                                input.DOCCHFR = LstDatosBK[x].NDOCCHOFER;
                                                //input.RUCEMP = LstDatosBK[x].NRUCTRANPO;
                                                if (!LstDatosBK[x].NRUCTRANPO.Equals("20100039207"))
                                                {
                                                    input.RUCEMP = LstDatosBK[x].NRUCTRANPO;
                                                }
                                                input.NROPREC1 = LstDatosBK[x].NPRECINTO;
                                                input.NROPREC2 = "";
                                                input.NROPREC3 = "";
                                                input.NROPREC4 = "";
                                                input.TARA = decimal.Parse(LstDatosBK[x].TARACONTE);
                                                input.PESO = decimal.Parse(LstDatosBK[x].PESONETO);
                                                input.TIPENV = "A";
                                                input.RPTASERV = Result.descripcion;
                                                if (Result.id == "0")
                                                {
                                                    input.IDRPTASERV = "V";
                                                }
                                                else
                                                {
                                                    input.IDRPTASERV = "F";
                                                }

                                                input.USRREG = "ENV_AUT";
                                                input.USERMOD = "ENV_AUT";
                                                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                                input.SESTRG = "A";
                                                input.ACCION = "I";
                                                input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                                                string data = lgCitaDPW.Acciones(input);
                                                if (data == "OK")
                                                {
                                                    if (Result.id == "0")
                                                    {
                                                        ActualizaFlg(LstDatosBK[x].IDRCE, "C");
                                                    }
                                                }
                                                else
                                                {
                                                    InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, data, Environment.NewLine, ""));
                                                }
                                                ContCita += 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }
        public static void CompletarCitasAsignadas()
        {
            try
            {

                List<Entidad.DatosBKCitaAsignadaAutomatica> LstDatosCitaAsignada = new List<Entidad.DatosBKCitaAsignadaAutomatica>();
                LstDatosCitaAsignada = ConsultaDatosCitaAsignada(ConfigurationManager.AppSettings["OPEPORT"]);
                if (LstDatosCitaAsignada.Count > 0)
                {
                    foreach (Entidad.DatosBKCitaAsignadaAutomatica item in LstDatosCitaAsignada)
                    {
                        Entidad.CitaDPWQueryInput input = new Entidad.CitaDPWQueryInput();
                        if (validarDatosBKAsignada(item))
                        {
                            string TipCont = "";
                            if (item.TIPCONT == "ST20")
                            {
                                TipCont = "22GP";
                            }
                            else if (item.TIPCONT == "ST40")
                            {
                                TipCont = "42GP";
                            }
                            else if (item.TIPCONT == "HC20")
                            {
                                TipCont = "25GP";
                            }
                            else if (item.TIPCONT == "HC40")
                            {
                                TipCont = "45GP";
                            }
                            else if (item.TIPCONT == "RE20")
                            {
                                TipCont = "22RT";
                            }
                            else if (item.TIPCONT == "RE40")
                            {
                                TipCont = "42RT";
                            }
                            else if (item.TIPCONT == "RH40")
                            {
                                TipCont = "45RT";
                            }
                            else if (item.TIPCONT == "FR20")
                            {
                                TipCont = "22PC";
                            }
                            else if (item.TIPCONT == "FR40")
                            {
                                TipCont = "42PC";
                            }
                            else if (item.TIPCONT == "OT20")
                            {
                                TipCont = "22UT";
                            }
                            else if (item.TIPCONT == "OT40")
                            {
                                TipCont = "42UT";
                            }
                            else if (item.TIPCONT == "TA20")
                            {
                                TipCont = "22TD";
                            }
                            else if (item.TIPCONT == "TA40")
                            {
                                TipCont = "42TD";
                            }
                            WsCompletarCita.CompletarCitasExtraportuarioSoapClient Cita = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                            WsCompletarCita.CompletarCitaRequest request = new WsCompletarCita.CompletarCitaRequest();
                            WsCompletarCita.CompletarCitaRequestBody requestBody = new WsCompletarCita.CompletarCitaRequestBody();
                            WsCompletarCita.CompletarCitasExtraportuarioSoap citasExtraportuarioSoap = new WsCompletarCita.CompletarCitasExtraportuarioSoapClient();
                            WsCompletarCita.CompletarCitaResponseBody responseBody = new WsCompletarCita.CompletarCitaResponseBody();
                            WsCompletarCita.CompletarCitaResponse response = new WsCompletarCita.CompletarCitaResponse();
                            requestBody.usuario = ConfigurationManager.AppSettings["Usuario"];
                            requestBody.clave = ConfigurationManager.AppSettings["Contrasena"];
                            requestBody.numerocita = item.NUMCITA;
                            requestBody.contenedor = item.NROCONTE;
                            if (TipCont != "")
                            {
                                requestBody.isoType = TipCont;
                            }
                            else
                            {
                                requestBody.isoType = "";
                            }

                            requestBody.placa = item.PLACAVEH;
                            requestBody.dni = item.NDOCCHOFER;
                            //requestBody.ructercerizada = item.NRUCTRANPO;
                            if (!item.NRUCTRANPO.Equals("20100039207"))
                            {
                                requestBody.ructercerizada = item.NRUCTRANPO;
                            }
                            requestBody.precinto1 = item.NPRECINTO;
                            requestBody.precinto2 = "";
                            requestBody.precinto3 = "";
                            requestBody.precinto4 = "";
                            requestBody.tara = item.TARACONTE;
                            requestBody.peso = item.PESONETO;
                            request.Body = requestBody;

                            response = citasExtraportuarioSoap.CompletarCita(request);
                            responseBody = response.Body;
                            var Result = JsonConvert.DeserializeObject<Entidad.CompletarCitaResult>(responseBody.CompletarCitaResult);
                            if (Result.id != "")
                            {

                                input.NUMID03 = System.Guid.NewGuid().ToString();
                                input.NUMCITA = item.NUMCITA;
                                input.NROCON = item.NROCONTE;
                                if (TipCont != "")
                                {
                                    input.ISOTYPE = TipCont;
                                }
                                else
                                {
                                    input.ISOTYPE = "";
                                }

                                input.NROPLACA = item.PLACAVEH;
                                input.DOCCHFR = item.NDOCCHOFER;
                                //input.RUCEMP = item.NRUCTRANPO;
                                if (!item.NRUCTRANPO.Equals("20100039207"))
                                {
                                    input.RUCEMP = item.NRUCTRANPO;
                                }
                                input.NROPREC1 = item.NPRECINTO;
                                input.NROPREC2 = "";
                                input.NROPREC3 = "";
                                input.NROPREC4 = "";
                                input.TARA = decimal.Parse(item.TARACONTE);
                                input.PESO = decimal.Parse(item.PESONETO);
                                input.TIPENV = "A";
                                input.RPTASERV = Result.descripcion;
                                if (Result.id == "0")
                                {
                                    input.IDRPTASERV = "V";
                                }
                                else
                                {
                                    input.IDRPTASERV = "F";
                                }

                                input.USRREG = "ENV_AUT";
                                input.USERMOD = "ENV_AUT";
                                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                input.SESTRG = "A";
                                input.ACCION = "I";
                                input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"];
                                string data = lgCitaDPW.Acciones(input);
                                if (data == "OK")
                                {
                                    if (Result.id == "0")
                                    {
                                        ActualizaFlg(item.IDRCE, "C");
                                    }
                                }
                                else
                                {
                                    InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, data, Environment.NewLine, ""));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }
        public static bool validarDatosBKAsignada(Entidad.DatosBKCitaAsignadaAutomatica DatosBK)
        {
            bool Rpta = false;
            string Mensaje = "";
            if (DatosBK.NROCONTE == "")
            {
                Mensaje += "-Contenedor" + "<br/>";
            }
            if (DatosBK.PLACAVEH == "")
            {
                Mensaje += "-Placa" + "<br/>";
            }
            if (DatosBK.NRUCTRANPO == "")
            {
                Mensaje += "-RUC empresa de transporte" + "<br/>";
            }
            if (DatosBK.NDOCCHOFER == "")
            {
                Mensaje += "-DNI del Chofer" + "<br/>";
            }
            if (DatosBK.NPRECINTO == "")
            {
                Mensaje += "-Precinto 1" + "<br/>";
            }
            if (Mensaje == "")
            {
                Rpta = true;
            }
            else
            {
                Rpta = false;
                Mensaje = "Estimados," +
                           "<br/>" +
                           "Por favor, ingresar la siguiente información para el Contenedor " + DatosBK.NROCONTE + ":" +
                           "<br/>" +
                           Mensaje;
                ValidacionAlertaCorreos(Mensaje, DatosBK.NROCONTE, DatosBK.NROBOOK);
            }

            return Rpta;
        }
        public static void ValidarCitas()
        {
            try
            {
                List<Entidad.DatosCitaCitaAutomatica> LstDatosCita = new List<Entidad.DatosCitaCitaAutomatica>();
                Entidad.CargarCitaQueryInput input = new Entidad.CargarCitaQueryInput();
                string data = string.Empty;
                LstDatosCita = ConsultaDatosCitas("", ConfigurationManager.AppSettings["OPEPORT"]);
                if (LstDatosCita.Count > 0)
                {
                    DateTime FechaActual = DateTime.Now;
                    foreach (Entidad.DatosCitaCitaAutomatica item in LstDatosCita)
                    {
                        DateTime FechaCita = new DateTime();
                        int anio, mes, dia, hora, minuto, segundo;
                        anio = int.Parse(item.FECCITA.ToString().Substring(0, 4));
                        mes = int.Parse(item.FECCITA.ToString().Substring(4, 2));
                        dia = int.Parse(item.FECCITA.ToString().Substring(6, 2));
                        hora = int.Parse(Right("000000" + item.HORCITA.ToString(), 6).Substring(0, 2));
                        minuto = int.Parse(Right("000000" + item.HORCITA.ToString(), 6).Substring(2, 2));
                        segundo = int.Parse(Right("000000" + item.HORCITA.ToString(), 6).Substring(4, 2));
                        FechaCita = new DateTime(anio, mes, dia, hora, minuto, segundo);
                        if (item.TEMPALERT > 0)
                        {
                            if (FechaActual.AddHours(double.Parse(item.TEMPALERT.ToString()) * -1) > FechaCita)
                            {
                                input = new Entidad.CargarCitaQueryInput();
                                input.NUMID = item.NUMID;
                                input.USRREG = "ENV_AUT";
                                input.USERMOD = "ENV_AUT";
                                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                input.ACCION = "E";
                                data = lgCita.Acciones(input);
                            }
                        }
                        else
                        {
                            if (FechaActual > FechaCita)
                            {
                                input = new Entidad.CargarCitaQueryInput();
                                input.NUMID = item.NUMID;
                                input.USRREG = "ENV_AUT";
                                input.USERMOD = "ENV_AUT";
                                input.FECREG = (DateTime.Now.ToString("yyyyMMdd"));
                                input.FECMOD = (DateTime.Now.ToString("yyyyMMdd"));
                                input.HRSREG = (DateTime.Now.ToString("HHmmss"));
                                input.HRSMOD = (DateTime.Now.ToString("HHmmss"));
                                input.ACCION = "E";
                                data = lgCita.Acciones(input);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.Instanse.Insert(string.Format(@"Error en el metodo: {0}{1}Mensaje Error:{2}{3}Detalle Error:{4}", MethodBase.GetCurrentMethod().Name, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
            }



        }
        public static string Right(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }
        public static bool validarDatosBK(Entidad.DatosBKCitaAutomatica DatosBK)
        {
            bool Rpta=false;
            string Mensaje="";
            if (DatosBK.NROCONTE == "")
            {
                Mensaje += "-Contenedor" + "<br/>" ;
            }
            if (DatosBK.PLACAVEH == "")
            {
                Mensaje += "-Placa" + "<br/>" ;
            }
            if (DatosBK.NRUCTRANPO == "")
            {
                Mensaje += "-RUC empresa de transporte" + "<br/>" ;
            }
            if (DatosBK.NDOCCHOFER == "")
            {
                Mensaje += "-DNI del Chofer"+ "<br/>" ;
            }
            if (DatosBK.NPRECINTO == "")
            {
                Mensaje += "-Precinto 1"+ "<br/>" ;
            }
            if (Mensaje == "")
            {
                Rpta = true;
            }
            else
            {
                Rpta = false;
                Mensaje = "Estimados," +
                           "<br/>" +
                           "Por favor, ingresar la siguiente información para el Contenedor " + DatosBK.NROCONTE + ":" +
                           "<br/>" +
                           Mensaje;
                ValidacionAlertaCorreos(Mensaje, DatosBK.NROCONTE, DatosBK.NROBOOK);
            }
           
            return Rpta;
        }
        public static void ActualizaFlg(string IDRCE,string ACCION)
        {
            Entidad.ActRCEQueryInput input = new Entidad.ActRCEQueryInput();
            input.IDRCE = IDRCE;
            input.ACCION = ACCION;
            lgCitaDPW.ActualizaRCE(input);
        }
        public static void ValidacionAlertaCorreos(string Mensaje,string Contenedor,string Booking)
        {
            string mensaje = string.Empty;
            mensaje = "<html> " +
                        "<body>" +
                        Mensaje+
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
                Subject = "CITAS DPW - DATOS FALTANTES CONTENEDOR: " + Contenedor+ " - " +Booking,
                IsBodyHtml = true,
                Body = mensaje,
                Priority = MailPriority.High,
            };

            SmtpClient servidorSMTP = new SmtpClient("smtp.office365.com");
            servidorSMTP.Port = 587; // 25;
            servidorSMTP.EnableSsl = true;
            servidorSMTP.UseDefaultCredentials = false;
            servidorSMTP.Credentials = new System.Net.NetworkCredential("Depositotemporal@ransa.net", "C0m3xR4ns4");
            Entidad.ConsultaCorreosQueryInput input = new Entidad.ConsultaCorreosQueryInput();
            List<Entidad.ConsultaCorreos> LstCorreos = new List<Entidad.ConsultaCorreos>();
            input.IN_ID_SLN = "CITADPWAUT";
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
        public static void AlertaCorreos(string NAVVIAJE, string NROBOOK, string NROCONTE)
        {
            string mensaje = string.Empty;
            mensaje = "<html> " +
                        "<body>" +
                            "Estimados,"+
                            "<br/>" +
                            "El presente correo es para informar que no se cuenta con citas disponibles para los siguientes datos:"+
                            "<br/>" +
                            "Nave.Viaje: " + NAVVIAJE +
                            "<br/>" +
                            "Booking: "+NROBOOK+
                              "<br/>" +
                            "Contenedor: " + NROCONTE +
                             "<br/>" +
                            "Atentamente," +
                             "<br/>" +
                            "Sistemas Ransa"+
                        //"<img src = 'cid:" + Path.Combine("Ransa.PNG") +"' /> " +
                        "</body>" +
                      "</html>";
            MailMessage mensajeCorreo = new MailMessage()
            {
                From = new MailAddress("DepositoTemporal@ransa.net", "Deposito Temporal Ransa"),
                //From = new MailAddress("testintegracion@ransa.net", "Deposito Temporal Ransa"),
                Subject = "CITAS DPW - ALERTA CITA FALTANTE CONTENEDOR: "+ NROCONTE,
                IsBodyHtml = true,
                Body = mensaje,
                Priority = MailPriority.High,
            };

            SmtpClient servidorSMTP = new SmtpClient("smtp.office365.com");
            servidorSMTP.Port = 587; // 25;
            servidorSMTP.EnableSsl = true;
            servidorSMTP.UseDefaultCredentials = false;
            servidorSMTP.Credentials = new System.Net.NetworkCredential("Depositotemporal@ransa.net", "C0m3xR4ns4");
            Entidad.ConsultaCorreosQueryInput input = new Entidad.ConsultaCorreosQueryInput();
            List<Entidad.ConsultaCorreos> LstCorreos = new List<Entidad.ConsultaCorreos>();
            input.IN_ID_SLN = "CITADPWAUT";
            LstCorreos=lgCitaDPW.ConsultaCorreosCitaAutomatica(input);
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
        public static List<Entidad.DatosCitaCitaAutomatica> ConsultaDatosCitas(string NROBOOK, string OPEPORT)
        {
            List<Entidad.DatosCitaCitaAutomatica> LstDatosCita = new List<Entidad.DatosCitaCitaAutomatica>();
            Entidad.DatosCitaCitaAutomaticaQueryInput input = new Entidad.DatosCitaCitaAutomaticaQueryInput();
            input.NROBOOK = NROBOOK;
            input.OPEPORT = OPEPORT;
            LstDatosCita = lgCitaDPW.ConsultaDatosCitaCitaAutomatica(input);
            return LstDatosCita;
        }
        public static List<Entidad.DatosBKCitaAsignadaAutomatica> ConsultaDatosCitaAsignada( string OPEPORT)
        {
            List<Entidad.DatosBKCitaAsignadaAutomatica> LstDatosBK = new List<Entidad.DatosBKCitaAsignadaAutomatica>();
            Entidad.DatosBKCitaAutomaticaAsignadaQueryInput input = new Entidad.DatosBKCitaAutomaticaAsignadaQueryInput();
            input.OPEPORT = OPEPORT;
            LstDatosBK = lgCitaDPW.ConsultaDatosBKCitaAsignadaAutomatica(input);
            return LstDatosBK;
        }
        public static List<Entidad.DatosBKCitaAutomatica> ConsultaDatosBK(string NROBOOK, string OPEPORT)
        {
            List<Entidad.DatosBKCitaAutomatica> LstDatosBK = new List<Entidad.DatosBKCitaAutomatica>();
            Entidad.DatosBKCitaAutomaticaQueryInput input = new Entidad.DatosBKCitaAutomaticaQueryInput();
            input.NROBOOK = NROBOOK;
            input.OPEPORT = OPEPORT;
            LstDatosBK = lgCitaDPW.ConsultaDatosBKCitaAutomatica(input);
            return LstDatosBK;
        }
        public static List<Entidad.BKCitaAutomatica> ConsultaBKCitaAutomatica()
        {
            List<Entidad.BKCitaAutomatica> lstCita = new List<Entidad.BKCitaAutomatica>();
            Entidad.BKCitaAutomaticaQueryInput input = new Entidad.BKCitaAutomaticaQueryInput();
            input.OPEPORT = ConfigurationManager.AppSettings["OPEPORT"]; //RUC DPW
            input.FECHA = (DateTime.Now.ToString("yyyy-MM-dd"));
            lstCita = lgCitaDPW.ConsultaBKCitaAutomatica(input);
            return lstCita;
        }
    }
}
