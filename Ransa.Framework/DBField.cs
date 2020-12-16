namespace Ransa.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class DBField : Attribute
    {
        #region Propiedades y campos
        public string Name { get; set; }

        public DBField(string name)
        {
            Name = name;
        }
        #endregion
    }
}
