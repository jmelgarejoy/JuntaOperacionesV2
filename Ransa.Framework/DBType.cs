namespace Ransa.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class DBType : Attribute
    {
        #region Propiedades y campos
        public string Name { get; set; }

        public DBType(string name)
        {
            Name = name;
        }
        #endregion
    }
}
