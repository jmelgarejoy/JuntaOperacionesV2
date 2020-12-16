using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using OEA.Framework.Token;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ransa.Framework
{

    public static class Helper
    {
        static List<string> listaMensajes = new List<string>();

        public static string DevolverFormatoFecha(decimal Valor)
        {
            string result = "";
            try
            {
                if (Valor == 0)
                {
                    result = "";
                }
                else
                {
                    result = Valor.ToString().Substring(6, 2) + "/" + Valor.ToString().Substring(4, 2) + "/" + Valor.ToString().Substring(0, 4);
                }
                return result;
            }
            catch (Exception)
            {

                return "";
            }


        }

        public static string DevolverFormatoHora(decimal Valor)
        {
            string result = "";
            try
            {
                if (Valor == 0)
                {
                    result = "";
                }
                else
                {
                    result = Valor.ToString();
                    if (result.Length < 6)
                    {
                        int total = 6 - result.Length;
                        for (int i = 0; i < total; i++)
                        {
                            result = "0" + result;
                        }
                    }

                    result = result.Substring(0, 2) + ":" + result.Substring(2, 2) + ":" + result.Substring(4, 2);

                }
                return result;
            }
            catch (Exception)
            {

                return "";
            }


        }
        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (props[i].PropertyType == typeof(string))
                    {
                        values[i] = props[i].GetValue(item) ?? string.Empty;
                    }
                    else if (props[i].PropertyType == typeof(int) || props[i].PropertyType == typeof(decimal))
                    {
                        values[i] = props[i].GetValue(item) ?? 0;
                    }
                    else
                    {
                        values[i] = props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static List<string> ValidarCamposRequeridos<TEntidad>(this TEntidad request)
        {
            listaMensajes = new List<string>();
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var propiedades = ObtenerPropiedades(request, flags);

            ValidarCamposObligatorios(request, propiedades, flags);

            return listaMensajes;
        }

        public static dynamic ObtenerPropiedades<T>(T obj, BindingFlags flags)
        {
            var propiedades = (from PropertyInfo propiedad in typeof(T).GetProperties(flags)
                               select new
                               {
                                   Requerido = propiedad.GetCustomAttributes(true).Any()
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio) != null
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio).Requerido : false : false,
                                   LongMin = propiedad.GetCustomAttributes(true).Any()
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio) != null
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio).LongitudMinima : 0 : 0,
                                   LongMax = propiedad.GetCustomAttributes(true).Any()
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio) != null
                                   ? (propiedad.GetCustomAttributes(true).ElementAt(0) as Obligatorio).LongitudMaxima : 0 : 0,
                                   Campo = propiedad.GetCustomAttributes(true).Any() ? propiedad.Name : propiedad.Name,
                                   Nombre = propiedad.Name,
                                   Tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType
                               }).ToList();
            return propiedades;
        }

        public static void ValidarCamposObligatorios<U>(U request, dynamic propiedades, BindingFlags flags)
        {
            string mensaje = "El campo {0} es obligatorio.";
            string mensajeLogMin = "El campo {0} debe contener una longitud minima de {1}.";
            string mensajeLogmax = "El campo {0} debe contener una longitud maxima de {1}.";

            foreach (var propiedad in propiedades)
            {
                var valor = request.GetType().GetProperty(propiedad.Campo).GetValue(request, null);

                if (propiedad.Requerido)
                {
                    if (propiedad.Tipo == typeof(string))
                    {
                        valor = valor == null ? string.Empty : valor;
                        if (string.IsNullOrEmpty(valor))
                        {
                            listaMensajes.Add(string.Format(mensaje, propiedad.Nombre));
                        }
                        else if (valor.Length < propiedad.LongMin)
                        {
                            listaMensajes.Add(string.Format(mensajeLogMin, propiedad.Nombre, propiedad.LongMin));
                        }
                        else if (valor.Length > propiedad.LongMax)
                        {
                            listaMensajes.Add(string.Format(mensajeLogmax, propiedad.Nombre, propiedad.LongMax));
                        }
                    }
                    else if (propiedad.Tipo == typeof(int))
                    {
                        valor = valor == null ? string.Empty : valor.ToString();
                        if (string.IsNullOrEmpty(valor))
                        {
                            listaMensajes.Add(string.Format(mensaje, propiedad.Nombre));
                        }
                        else if (propiedad.LongMin > 0 && valor.Length < propiedad.LongMin)
                        {
                            listaMensajes.Add(string.Format(mensajeLogMin, propiedad.Nombre, propiedad.LongMin));
                        }
                        else if (propiedad.LongMax > 0 && valor.Length > propiedad.LongMax)
                        {
                            listaMensajes.Add(string.Format(mensajeLogmax, propiedad.Nombre, propiedad.LongMax));
                        }
                    }
                    else if (propiedad.Tipo.Name.Contains("List"))
                    {
                        if (valor.Count > 0)
                        {
                            for (var i = 0; i < valor.Count; i++)
                            {
                                ValidarCamposObligatorios(valor[i], ObtenerPropiedades(valor[i], flags), flags);
                            }
                        }
                    }
                    else
                    {
                        if (valor == null)
                        {
                            listaMensajes.Add(string.Format(mensaje, propiedad.Nombre));
                        }
                    }
                }
                else
                {
                    if (propiedad.LongMin != 0 && propiedad.LongMax != 0)
                    {
                        if (propiedad.Tipo == typeof(string))
                        {
                            valor = valor == null ? string.Empty : valor;
                            if (!string.IsNullOrEmpty(valor) && valor.Length < propiedad.LongMin)
                            {
                                listaMensajes.Add(string.Format(mensajeLogMin, propiedad.Nombre, propiedad.LongMin));
                            }
                            else if (!string.IsNullOrEmpty(valor) && valor.Length > propiedad.LongMax)
                            {
                                listaMensajes.Add(string.Format(mensajeLogmax, propiedad.Nombre, propiedad.LongMax));
                            }
                        }
                        else if (propiedad.Tipo == typeof(int))
                        {
                            valor = valor == null ? string.Empty : valor.ToString();
                            if (!string.IsNullOrEmpty(valor) && valor.Length < propiedad.LongMin)
                            {
                                listaMensajes.Add(string.Format(mensajeLogMin, propiedad.Nombre, propiedad.LongMin));
                            }
                            else if (!string.IsNullOrEmpty(valor) && valor.Length > propiedad.LongMax)
                            {
                                listaMensajes.Add(string.Format(mensajeLogmax, propiedad.Nombre, propiedad.LongMax));
                            }
                        }
                        else
                        {
                            if (valor == null)
                            {
                                listaMensajes.Add(string.Format(mensaje, propiedad.Nombre));
                            }
                        }
                    }
                    else if (propiedad.Tipo.Name.Contains("List"))
                    {
                        if (valor.Count > 0)
                        {
                            for (var i = 0; i < valor.Count; i++)
                            {
                                ValidarCamposObligatorios(valor[i], ObtenerPropiedades(valor[i], flags), flags);
                            }
                        }
                    }
                }
            }
        }

        public static string SerializarJsonObjecto(object objeto)
        {
            string jsonrpt = JsonConvert.SerializeObject(objeto, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy" });// HH:mm:ss

            return jsonrpt;
        }

        public static string DeserializarJsonObjecto2<T>(T request)
        {
            String jsonrpt = "";
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, request);
                jsonrpt = Encoding.Default.GetString(stream.ToArray());
            }

            return jsonrpt;
        }

        private static void ObtenerNroParentencis(string jsonrpt, out int nroParentecisAbiertos, out int posicionUltimoParentecis)
        {
            bool flagEncontro = false;
            nroParentecisAbiertos = 0;
            posicionUltimoParentecis = 0;
            char caracterInicio = '{';
            char caracterfin = '}';
            int contCaracterInicio = 0;
            //int contCaracterfin = 0;
            for (int i = 0; i < jsonrpt.Length; i++)
            {
                if (jsonrpt[i] == caracterInicio)
                {
                    contCaracterInicio++;
                    nroParentecisAbiertos++;
                }
                if (jsonrpt[i] == caracterfin)
                {
                    //contCaracterfin++;
                    contCaracterInicio--;
                }

                if (jsonrpt[i] == caracterfin && contCaracterInicio == 0)
                {
                    posicionUltimoParentecis = i;
                    flagEncontro = true;
                    break;
                    ;
                }
            }

            if (flagEncontro == false)
            {
                nroParentecisAbiertos = 0;
                posicionUltimoParentecis = jsonrpt.Length - 1;
            }
        }

        private static void ObtenerPosicionNroParentencis(string jsonrpt, out int posicionPrimerParentecis)
        {
            char caracterInicio = '{';
            posicionPrimerParentecis = 0;
            for (int i = 0; i < jsonrpt.Length; i++)
            {
                if (jsonrpt[i] == caracterInicio)
                {
                    posicionPrimerParentecis = i;
                    break;
                }
            }
        }

        public static T ConvertirAObjeto<T>(string nombreAtributoCopiar, string json)
        {
            Object response = null;
            try
            {
                string[] array = json.Split(new String[] { nombreAtributoCopiar }, StringSplitOptions.None);
                if (array.Length > 1)
                {
                    string jsonrpt = array[1];
                    int nroParentecisAbiertos = 0, posicionUltimoParentecis = 0, posicionPrimerParentecis = 0;
                    ObtenerPosicionNroParentencis(jsonrpt, out posicionPrimerParentecis);

                    string jsonrptParte1 = jsonrpt.Remove(0, posicionPrimerParentecis);
                    ObtenerNroParentencis(jsonrptParte1, out nroParentecisAbiertos, out posicionUltimoParentecis);

                    string jsonrptParte2 = jsonrptParte1.Remove(posicionUltimoParentecis + 1, jsonrptParte1.Length - (posicionUltimoParentecis + 1));

                    jsonrptParte2 = jsonrptParte2.Replace("\\", "\\\\");

                    response = (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonrptParte2, typeof(T), new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, });
                }
            }
            catch (Exception)
            {
                //error al deserializar
            }

            return (T)response;
        }

        /// <summary>
        /// Metodo general para invocar un servicio externo enviando el request en formato JSON.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="resquest">Objeto a enviar</param>
        /// <param name="url">URL a invocar</param>
        /// <param name="verboHTTP">POST, GET</param>
        /// <returns></returns>
        public static R InvocarServicio<R>(object request, string verboHTTP, string url, string token) where R : new()
        {
            R response = new R();
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Headers["Authorization"] = "bearer " + token;
            webClient.Encoding = Encoding.UTF8;
           
            string cadena = webClient.UploadString(url,verboHTTP, JsonConvert.SerializeObject(request));

            response = JsonConvert.DeserializeObject<R>(cadena);
            return response;
        }

        public static R InvocarServicio2<R>(object request, string verboHTTP, string url) where R : new()
        {
            R response = new R();
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;

            string cadena = webClient.UploadString(url, verboHTTP, JsonConvert.SerializeObject(request));

            response = JsonConvert.DeserializeObject<R>(cadena);
            return response;
        }


        public static ResponseToken InvocarServicioToken(RequestToken request, string url, string verboHTTP)
        {
            ResponseToken response = new ResponseToken();
            try
            {
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = verboHTTP;
                req.ContentType = "application/x-www-form-urlencoded";

                // Metodo modificado
                string postData = "client_id=" + request.client_id + "&client_secret=" + request.client_secret + "&grant_type=" + request.grant_type;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();

                StreamWriter sr = new StreamWriter(dataStream);
                sr.Write(postData);

                dataStream.Write(byteArray, 0, byteArray.Length);

                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string resp = reader.ReadToEnd();

                response = JsonConvert.DeserializeObject<ResponseToken>(resp);
                
            }catch(Exception ex)
            {

            }

            return response;
        }

        public static int ObtenerCodigoEstadoHTTPExcepcion(Exception ex)
        {
            int response = 0;
            FieldInfo ddd = ex.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name.Equals("m_Response"));
            var ccc = ddd.GetValue(ddd);
            return response;
        }
    }
}
