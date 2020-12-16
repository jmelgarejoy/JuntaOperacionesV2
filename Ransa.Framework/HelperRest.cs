using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Framework
{
    public class HelperRest
    {
        public enum Method { GET, POST }

        /// <summary>
        /// Concatena los parámetros a una cadena de texto compatible con el API Rest
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Parametros concatenados en formato URL, no establece el caracter "?" al principio
        /// pero sí los "&" separadores</returns>
        /// <history>Creado 26/11/2019</history>
        private static string ConcatParams(Dictionary<string, string> parameters)
        {
            bool FirstParam = true;
            StringBuilder Parametros = null;

            if (parameters != null)
            {
                Parametros = new StringBuilder();
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    Parametros.Append(FirstParam ? "" : "&");
                    Parametros.Append(param.Key + "=" + System.Web.HttpUtility.UrlEncode(param.Value));
                    FirstParam = false;
                }
            }

            return Parametros == null ? string.Empty : Parametros.ToString();
        }


        public static R GetResponse_GET<R>(string url, string verboHTTP,object request, string token) where R : new()
        {
            R response = new R();
            string result;
            string json = JsonConvert.SerializeObject(request);
            var parametrosConcatenados = ConcatParams( JsonConvert.DeserializeObject<Dictionary<string, string>>(json));


            string urlConParametros = url + "?" + parametrosConcatenados;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlConParametros);
            httpWebRequest.Method = verboHTTP;
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers.Add("Authorization", "bearer " + token);
            

            try
            {
                using (var responseServer = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    if (httpWebRequest.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(responseServer.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                            response = JsonConvert.DeserializeObject<R>(result);
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            result = error;
                            response = JsonConvert.DeserializeObject<R>(result);
                        }
                    }

                }
            }
            
            return response;
        }

        public static R GetResponse_POST<R>(object request, string verboHTTP, string url, string token) where R : new()
        {


            R response = new R();
            string result;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = verboHTTP;
            httpWebRequest.Headers.Add("Authorization", "bearer " + token);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(request);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (var responseServer = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    if (httpWebRequest.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(responseServer.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                            response = JsonConvert.DeserializeObject<R>(result);
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            result = error;
                            response = JsonConvert.DeserializeObject<R>(result);
                        }
                    }

                }
            }

           

        //    return result;
//

            return response;
        }




    }
}
