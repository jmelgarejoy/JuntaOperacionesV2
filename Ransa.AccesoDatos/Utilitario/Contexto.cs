
namespace Ransa.AccesoDatos.Utilitario
{
    using Ransa.Framework;
    using IBM.Data.DB2.iSeries;

    public class Contexto
    {
        public Contexto(string esquema)
        {
            Ambito.Esquema = esquema;
        }
        #region Propiedades y campos
        public iDB2Connection getConexionRNSLIB
        {
            get
            {
                return (new iDB2Connection(CadenaConexionRNSLIB()));
            }
        }
        #endregion

        #region Métodos y funciones
        private string CadenaConexionRNSLIB()
        {
            switch (Ambito.Esquema)
            {
                case Ambito.Desarrollo:
                    return Recursos.CadenaConexion.RNSLIB_Desarrollo;
                case Ambito.Testing:
                    return Recursos.CadenaConexion.RNSLIB_Testing;
                case Ambito.Produccion:
                    return Recursos.CadenaConexion.RNSLIB_Produccion;
            }
            return string.Empty;
        }

        #endregion
    }
}
