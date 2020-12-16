using System;
using System.Configuration;

namespace Ransa.Framework
{
    public class InsertLog
    {
        #region Singleton
        private static InsertLog _InsertLog = null;

        private InsertLog() { }

        public static InsertLog Instanse
        {
            get
            {
                if (_InsertLog != null) return _InsertLog;
                _InsertLog = new InsertLog();
                return _InsertLog;
            }
        }
        #endregion

        private bool WriteLogs = Convert.ToBoolean(ConfigurationManager.AppSettings["WriteLogs"]);					//true o false
        private string LogCarga = ConfigurationManager.AppSettings["Log"];		//Ruta y nombre del archivo sin .txt

        public bool Insert(Exception ex)
        {
            if (WriteLogs)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format(LogCarga, DateTime.Now.ToString("yyyyMMdd")), true))
                {
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Source: " + ex.Source);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- TargetSite: " + ex.TargetSite);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Message: " + ex.Message);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- StackTrace: " + ex.StackTrace);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- InnerException: " + ex.InnerException);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- HResult: " + ex.HResult);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Data: " + ex.Data);

                    file.WriteLine(" -------------------------------------------------------------------------------------------- ");
                    file.WriteLine();
                }
            }
            return true;
        }

        public bool Insert(string Message)
        {
            if (WriteLogs)
            {
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format(LogCarga, DateTime.Now.ToString("yyyyMMdd")), true))
                    {
                        file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + Environment.NewLine + Message);

                        file.WriteLine(" -------------------------------------------------------------------------------------------- ");
                        file.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return true;
        }

        public bool Insert(string Metodo, string Message)
        {
            if (WriteLogs)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format(LogCarga, DateTime.Now.ToString("yyyyMMdd")), true))
                {
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Metodo: " + Metodo);
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Message: " + Message);

                    file.WriteLine(" -------------------------------------------------------------------------------------------- ");
                    file.WriteLine();
                }
            }
            return true;
        }

        public bool Correo(string Para, string UserCC, string asuntoCorreo, string cuerpoCorreo)
        {
            if (WriteLogs)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format(LogCarga, DateTime.Now.ToString("yyyyMMdd")), true))
                {
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Envio de Correo: ");
                    file.WriteLine("-  Para: " + Para);
                    file.WriteLine("-  CC: " + UserCC);
                    file.WriteLine("-  Asunto: " + asuntoCorreo);
                    file.WriteLine("-  Cuerpo: " + cuerpoCorreo);

                    file.WriteLine(" ----------------------- ");
                    file.WriteLine();
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format(LogCarga, DateTime.Now.ToString("yyyyMMdd")), true))
                {
                    file.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " -- Envio de Correo: ");
                    file.WriteLine("-  Para: " + Para);
                    file.WriteLine("-  CC: " + UserCC);
                    file.WriteLine("-  Asunto: " + asuntoCorreo);
                    file.WriteLine("-  Cuerpo: " + cuerpoCorreo);

                    file.WriteLine(" ----------------------- ");
                    file.WriteLine();
                }
            }
            return true;
        }
    }
}
