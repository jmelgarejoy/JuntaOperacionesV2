namespace Ransa.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DBParameter : Attribute
    {
        public DBParameter(string nombreCampo, string tipoDatoDB2)
        {
            NombreCampo = nombreCampo;
            TipoDatoDB2 = tipoDatoDB2;
        }

        public string NombreCampo { get; internal set; }
        public string TipoDatoDB2 { get; internal set; }
    }
}
