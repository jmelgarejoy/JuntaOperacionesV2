namespace Ransa.AccesoDatos.Utilitario
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using Ransa.Framework;
    using IBM.Data.DB2.iSeries;
    using System.Configuration;

    // using System.Data.Common;

    public static class DB2Helper
    {
        #region Métodos y funciones
        public static DataTable ExecuteDb2Query(CommandType commandType, string instructionSQL, Object parameters = null)
        {
            try
            {

                var strEsquema = ConfigurationManager.AppSettings["Esquema"];
                Ambito.Esquema = strEsquema;

                Contexto contexto = new Contexto(Ambito.Esquema);

                iDB2Connection conexion = new iDB2Connection();
                conexion = contexto.getConexionRNSLIB;

                if (conexion.State == ConnectionState.Open)
                    conexion.Close();

                conexion.Open();

                iDB2Command dbCmd = new iDB2Command();
                dbCmd.Connection = conexion;
                dbCmd.CommandType = commandType;
                dbCmd.CommandText = instructionSQL;

                iDB2Parameter[] db2Parameters = GetParameters(parameters);

                if (db2Parameters.Length > 0)
                {
                    dbCmd.Parameters.AddRange(db2Parameters);
                }

                iDB2DataAdapter da = new iDB2DataAdapter();
                da.SelectCommand = dbCmd;

                DataTable dtResult = new DataTable();
                da.Fill(dtResult);

                conexion.Close();

                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static object ExecuteDb2Scalar(CommandType commandType, string instructionSQL, Object parameters = null)
        {
            object rpta;
            try
            {
                var strEsquema = ConfigurationManager.AppSettings["Esquema"];
                Ambito.Esquema = strEsquema;

                Contexto contexto = new Contexto(Ambito.Esquema);

                iDB2Connection conexion = new iDB2Connection();
                conexion = contexto.getConexionRNSLIB;

                if (conexion.State == ConnectionState.Open)
                    conexion.Close();

                conexion.Open();

                iDB2Command dbCmd = new iDB2Command();
                dbCmd.Connection = conexion;
                dbCmd.CommandType = commandType;
                dbCmd.CommandText = instructionSQL;

                iDB2Parameter[] db2Parameters = GetParameters(parameters);

                if (db2Parameters.Length > 0)
                {
                    dbCmd.Parameters.AddRange(db2Parameters);
                }

                rpta = dbCmd.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return rpta; 
            }
           

            return rpta;
        }

        private static iDB2Parameter[] GetParameters(Object parameters)
        {
            List<iDB2Parameter> listaParametro = new List<iDB2Parameter>();

            if (parameters == null)
            {
                return listaParametro.ToArray();
            }
            foreach (PropertyInfo propiedad in parameters.GetType().GetProperties())
            {

                dynamic valor = propiedad.GetValue(parameters, null);
                Type type = Nullable.GetUnderlyingType(propiedad.PropertyType);
                type = type ?? propiedad.PropertyType;

                object[] propertyList = propiedad.GetCustomAttributes(typeof(DBParameter), true);
                if (propertyList.Length > 0)
                {
                    string nombreCampo = (propertyList[0] as DBParameter).NombreCampo.ToString();
                    string TipoDatoDB2 = (propertyList[0] as DBParameter).TipoDatoDB2.ToString();

                    iDB2DbType typeDate = new iDB2DbType();
                    switch (TipoDatoDB2)
                    {
                        case DBDataType.VarChar:
                            typeDate = iDB2DbType.iDB2VarChar;
                            break;
                        case DBDataType.Char:
                            typeDate = iDB2DbType.iDB2Char;
                            break;
                        case DBDataType.Decimal:
                            typeDate = iDB2DbType.iDB2Decimal;
                            break;
                        case DBDataType.Numeric:
                            typeDate = iDB2DbType.iDB2Numeric;
                            break;
                        case DBDataType.Date:
                            typeDate = iDB2DbType.iDB2Date;
                            break;
                        case DBDataType.Integer:
                            typeDate = iDB2DbType.iDB2Integer;
                            break;
                    }


                    listaParametro.Add(type == typeof(DataTable)
                                           ? new iDB2Parameter
                                           {
                                               ParameterName = "@" + nombreCampo,
                                               Value = valor,
                                               iDB2DbType = typeDate
                                           }
                                           : new iDB2Parameter
                                           {
                                               ParameterName = "@" + nombreCampo,
                                               Value = valor
                                           });
                }
            }
            return listaParametro.ToArray();
        }
        #endregion
    }
}
