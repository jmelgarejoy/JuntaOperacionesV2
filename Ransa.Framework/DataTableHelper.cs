namespace Ransa.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    public static class DataTableHelper
    {
        #region Métodos y funciones
        public static List<TSource> ToList<TSource>(this DataTable data) where TSource : new()
        {
            var listaDatos = new List<TSource>();
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var propiedadesObjeto = (from PropertyInfo propiedad in typeof(TSource).GetProperties(flags)
                                     select new
                                     {
                                         Campo = propiedad.GetCustomAttributes(true).Any() ? (propiedad.GetCustomAttributes(true).ElementAt(0) as DBField).Name ?? propiedad.Name : propiedad.Name,
                                         Nombre = propiedad.Name,
                                         Tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType
                                     }).ToList();

            foreach (DataRow fila in data.AsEnumerable().ToList())
            {
                var entidad = new TSource();

                foreach (var propiedad in propiedadesObjeto)
                {
                    // establece equivalencia entre campo de bd y propiedad de objeto (campo y tipo deben ser iguales)
                    if (fila.Table.Columns.IndexOf(propiedad.Campo) >= 0
                        && fila[propiedad.Campo].GetType() == propiedad.Tipo)
                    {
                        PropertyInfo infoPropiedad = entidad.GetType().GetProperty(propiedad.Nombre);
                        var valor = (fila[propiedad.Campo] == DBNull.Value) ? null : fila[propiedad.Campo]; //si campo en base de datos es nullable

                        infoPropiedad.SetValue(entidad, valor, null);
                    }
                }
                listaDatos.Add(entidad);
            }

            return listaDatos;
        }
        #endregion
    }
}
