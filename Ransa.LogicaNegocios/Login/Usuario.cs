


namespace Ransa.LogicaNegocios.Login
{
    using Newtonsoft.Json;
    using Ransa.AccesoDatos.Seguridad;
    using Ransa.Entidades;
    using Ransa.Entidades.Seguridad;
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;

    public class Usuario
    {
        public string validarUsuario(string usuario, string clave, string dominio)
        {
            dominio = (dominio == "") ? "GRUPORANSA" : dominio;
            string rpta = "";
            AccesoUsuarioOutput usuarios = new AccesoUsuarioOutput();

            DirectoryEntry domain = new DirectoryEntry("LDAP://" + dominio);

            using (DirectorySearcher Searcher = new DirectorySearcher(dominio))
            {
                //Searcher.Filter = "(&(objectCategory=user)(ANR=" + usuario + " * ))"; // busca todas las cuentas que se parezcan 
                Searcher.Filter = "(SAMAccountName=" + usuario + ")"; // "(SAMAccountName=" & usuario & ")"; // filtra por usuario especifico
                Searcher.SearchScope = SearchScope.Subtree; // Start at the top and keep drilling down

                Searcher.PropertiesToLoad.Add("sAMAccountName"); // Load User ID
                Searcher.PropertiesToLoad.Add("displayName"); // Load Display Name
                Searcher.PropertiesToLoad.Add("givenName"); // Load Users first name
                Searcher.PropertiesToLoad.Add("sn");   // Load Users last name
                Searcher.PropertiesToLoad.Add("distinguishedName");   // Users Distinguished name  

                Searcher.PropertiesToLoad.Add("proxyAddresses");   // correo del usuario
                Searcher.PropertiesToLoad.Add("department");   // area de trabajo 
                Searcher.PropertiesToLoad.Add("title");   // rol del usuario  
                Searcher.PropertiesToLoad.Add("userAccountControl");   // Users Distinguished name  
                Searcher.Sort.PropertyName = "sAMAccountName"; // Sort by user ID
                Searcher.Sort.Direction = System.DirectoryServices.SortDirection.Ascending; // A-Zt)

                using (var users = Searcher.FindAll()) // Users contains our searh results
                {
                    if (users.Count > 0)
                    {
                        foreach (SearchResult User in users) // goes throug each user in the search resultsg
                        {
                            variablesGlobales._estCuentaUsuario = Convert.ToInt32(User.Properties["userAccountControl"][0]);
                            int flagExists = variablesGlobales._estCuentaUsuario & 0x2;
                            if (flagExists > 0)
                            {

                                usuarios.IDUSER = 0;
                                usuarios.USERNM = "La cuenta de usuario se encuentra deshabilitada";
                                usuarios.NVLACC = "SIN ACCESO";
                                usuarios.PERMISOS = new List<CE_AccesosUsuario>();
                                rpta = JsonConvert.SerializeObject(usuarios);

                            }

                            System.DirectoryServices.DirectoryEntry Entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + dominio, usuario, clave);
                            System.DirectoryServices.DirectorySearcher valSearcher = new System.DirectoryServices.DirectorySearcher(Entry);
                            valSearcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;

                            try
                            {
                                System.DirectoryServices.SearchResult Results = valSearcher.FindOne();
                            }
                            catch (Exception ex)
                            {
                                //rpta = "[{id=0, mensaje = '"+ ex.Message + "'}]";
                                rpta = "{\"id\":0, \"mensaje\": \"" + ex.Message + "\"}";
                                return rpta;
                            }

                            if (User.Properties.Contains("displayName"))
                            {
                                variablesGlobales._NombreUsuario = System.Convert.ToString(User.Properties["displayName"][0]);
                            }

                            variablesGlobales._rolUsuario = (User.Properties["title"].Count > 0) ? System.Convert.ToString(User.Properties["title"][0]) : "";
                            variablesGlobales._dptoUsuario = (User.Properties["department"].Count > 0) ? System.Convert.ToString(User.Properties["department"][0]) : "";
                            variablesGlobales._correoUsuario = (User.Properties["proxyAddresses"].Count > 0) ? System.Convert.ToString(User.Properties["proxyAddresses"][0]) : "";
                            variablesGlobales._cuentaUsuario = (User.Properties["sAMAccountName"].Count > 0) ? System.Convert.ToString(User.Properties["sAMAccountName"][0]).ToUpper() : "";

                            usuarios = ValidarAccesos(variablesGlobales._cuentaUsuario);

                            rpta = JsonConvert.SerializeObject(usuarios);
                        }
                    }
                    else
                    {
                        usuarios.IDUSER = 0;
                        usuarios.USERNM = "No EXiste";
                        usuarios.NVLACC = "SIN ACCESO";
                        usuarios.PERMISOS = new List<CE_AccesosUsuario>();
                        rpta = JsonConvert.SerializeObject(usuarios);

                    }
                }
            }
            return rpta;
        }

        public AccesoUsuarioOutput ValidarAccesos(string Nombre)
        {
            List<CE_AccesosUsuario> ResultAcceso = new List<CE_AccesosUsuario>();
            CD_SeguridadAccesos acceso = new CD_SeguridadAccesos();
            CD_SeguridadUsuarios usuarios = new CD_SeguridadUsuarios();

            List<CE_Usuario> Usuario = new List<CE_Usuario>();

            UsuarioNombreQueryInput parametros = new UsuarioNombreQueryInput();
            parametros.USERNM = Nombre;
            parametros.ACCION = "U";
            Usuario = usuarios.Consultar(parametros);


            AccesosQueryInput Parametros = new AccesosQueryInput();
            Parametros.IDUSER = Usuario[0].IDUSER;
            Parametros.IDMDLO = 0;
            Parametros.ACCION = "U";

            ResultAcceso = acceso.Consultar(Parametros);

            AccesoUsuarioOutput Result = new AccesoUsuarioOutput();
            Result.IDUSER = Usuario[0].IDUSER;
            Result.USERNM = Usuario[0].USERNM;
            Result.NVLACC = Usuario[0].NVLACC;
            Result.AUTORIZ = Usuario[0].AUTORIZ;
            Result.NOMLIB = Usuario[0].NOMLIB;
            Result.PERMISOS = ResultAcceso;

            return Result;

        }


    }
}
