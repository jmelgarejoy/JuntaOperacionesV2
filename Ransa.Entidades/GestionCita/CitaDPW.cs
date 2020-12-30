using Ransa.Entidades.Comun;
using Ransa.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Entidades.GestionCita
{
    public class DatosBKCitaAutomaticaAsignadaQueryInput
    {

       
        ///<summary>Operador portuario</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
    public class DatosBKCitaAsignadaAutomatica
    {

        ///<summary>IDRCE</summary>
        [DBField("IDRCE")]
        public string IDRCE { get; set; }

        ///<summary>PLACAVEH</summary>
        [DBField("PLACAVEH")]
        public string PLACAVEH { get; set; }

        ///<summary>NDOCCHOFER</summary>
        [DBField("NDOCCHOFER")]
        public string NDOCCHOFER { get; set; }

        ///<summary>NRUCTRANPO</summary>
        [DBField("NRUCTRANPO")]
        public string NRUCTRANPO { get; set; }
        ///<summary>PESONETO</summary>
        [DBField("PESONETO")]
        public string PESONETO { get; set; }

        ///<summary>NPRECINTO</summary>
        [DBField("NPRECINTO")]
        public string NPRECINTO { get; set; }

        ///<summary>TARACONTE</summary>
        [DBField("TARACONTE")]
        public string TARACONTE { get; set; }
        ///<summary>FLGALERT</summary>
        [DBField("FLGALERT")]
        public string FLGALERT { get; set; }
        ///<summary>NROBOOK</summary>
        [DBField("NROBOOK")]
        public string NROBOOK { get; set; }
        ///<summary>NAVVIAJE</summary>
        [DBField("NAVVIAJE")]
        public string NAVVIAJE { get; set; }
        ///<summary>NROCONTE</summary>
        [DBField("NROCONTE")]
        public string NROCONTE { get; set; }

        ///<summary>TIPCONT</summary>
        [DBField("TIPCONT")]
        public string TIPCONT { get; set; }
        ///<summary>NUMCITA</summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
        ///<summary>NUMID</summary>
        [DBField("NUMID")]
        public string NUMID { get; set; }
    }
    public class ActRCEQueryInput
    {
        ///<summary>Id Solucion</summary>
        [DBParameter("IDRCE", DBDataType.VarChar)]
        public string IDRCE { get; set; }
        ///<summary>Accion</summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class ConsultaCorreosQueryInput
    {
        ///<summary>Id Solucion</summary>
        [DBParameter("IN_ID_SLN", DBDataType.VarChar)]
        public string IN_ID_SLN { get; set; }
    }
    public class ConsultaCorreos
    {
        ///<summary>Id Solucion</summary>
        [DBField("ID_SLN")]
        public string ID_SLN { get; set; }
         
        ///<summary>Tipo Destinatario</summary>
        [DBField("TIP_DST")]
        public string TIP_DST { get; set; }
        
        ///<summary>Direccion Correo</summary>
        [DBField("DIR_COR")]
        public string DIR_COR { get; set; }
    }
    public class DatosCitaCitaAutomaticaQueryInput
    {

        ///<summary>Operador portuario</summary>
        [DBParameter("NROBOOK", DBDataType.VarChar)]
        public string NROBOOK { get; set; }
        ///<summary>Operador portuario</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
    public class DatosCitaCitaAutomatica
    {
        ///<summary>NUMID</summary>
        [DBField("NUMID")]
        public string NUMID { get; set; }
        ///<summary>NUMCITA</summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }
        ///<summary>FECCITA</summary>
        [DBField("FECCITA")]
        public decimal FECCITA { get; set; }
        ///<summary>HORCITA</summary>
        [DBField("HORCITA")]
        public decimal HORCITA { get; set; }
        ///<summary>TEMPALERT</summary>
        [DBField("TEMPALERT")]
        public decimal TEMPALERT { get; set; }
    }
    public class DatosBKCitaAutomaticaQueryInput
    {
        
        ///<summary>Operador portuario</summary>
        [DBParameter("NROBOOK", DBDataType.VarChar)]
        public string NROBOOK { get; set; }
        ///<summary>Operador portuario</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
    public class DatosBKCitaAutomatica
    {
        
        ///<summary>IDRCE</summary>
        [DBField("IDRCE")]
        public string IDRCE { get; set; }
        
        ///<summary>PLACAVEH</summary>
        [DBField("PLACAVEH")]
        public string PLACAVEH { get; set; }

        ///<summary>NDOCCHOFER</summary>
        [DBField("NDOCCHOFER")]
        public string NDOCCHOFER { get; set; }

        ///<summary>NRUCTRANPO</summary>
        [DBField("NRUCTRANPO")]
        public string NRUCTRANPO { get; set; }
        ///<summary>PESONETO</summary>
        [DBField("PESONETO")]
        public string PESONETO { get; set; }

        ///<summary>NPRECINTO</summary>
        [DBField("NPRECINTO")]
        public string NPRECINTO { get; set; }
        
        ///<summary>TARACONTE</summary>
        [DBField("TARACONTE")]
        public string TARACONTE { get; set; }
        ///<summary>FLGALERT</summary>
        [DBField("FLGALERT")]
        public string FLGALERT { get; set; }
        ///<summary>NROBOOK</summary>
        [DBField("NROBOOK")]
        public string NROBOOK { get; set; }
        ///<summary>NAVVIAJE</summary>
        [DBField("NAVVIAJE")]
        public string NAVVIAJE { get; set; }
        ///<summary>NROCONTE</summary>
        [DBField("NROCONTE")]
        public string NROCONTE { get; set; }

        ///<summary>TIPCONT</summary>
        [DBField("TIPCONT")]
        public string TIPCONT { get; set; }
    }
    public class BKCitaAutomaticaQueryInput
    {
        ///<summary>Operador portuario</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        ///<summary>FECHA</summary>
        [DBParameter("FECHA", DBDataType.VarChar)]
        public string FECHA { get; set; }
    }
    public class BKCitaAutomatica
    {
        
       
        
             ///<summary>NROBOOK</summary>
        [DBField("NROBOOK")]
        public string NROBOOK { get; set; }
        
          
       

    }
    public class ConsultaContenedorQueryInput
    {
        ///<summary>NROCONTE</summary>
        [DBParameter("NROCONTE", DBDataType.VarChar)]
        public string NROCONTE { get; set; }

        ///<summary>OPEPORT</summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        ///<summary>ACCION</summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class ConsultaContenedor
    {
        ///<summary>IDRCE</summary>
        [DBField("IDRCE")]
        public string IDRCE { get; set; }
        ///<summary>PLACAVEH</summary>
        [DBField("PLACAVEH")]
        public string PLACAVEH { get; set; }

        ///<summary>NDOCCHOFER</summary>
        [DBField("NDOCCHOFER")]
        public string NDOCCHOFER { get; set; }
        ///<summary>NRUCTRANPO</summary>
        [DBField("NRUCTRANPO")]
        public string NRUCTRANPO { get; set; }
        
        ///<summary>PESONETO</summary>
        [DBField("PESONETO")]
        public string PESONETO { get; set; }

        ///<summary>NPRECINTO</summary>
        [DBField("NPRECINTO")]
        public string NPRECINTO { get; set; }

        ///<summary>TARACONTE</summary>
        [DBField("TARACONTE")]
        public string TARACONTE { get; set; }
        ///<summary>TIPCONT</summary>
        [DBField("TIPCONT")]
        public string TIPCONT { get; set; }
        ///<summary>NUMBKG</summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }

    }
    public class ConsultaCitaDPWQueryInput
    {
        /// <summary>
        /// NUMID03
        /// </summary>
        [DBParameter("NUMID03", DBDataType.VarChar)]
        public string NUMID03 { get; set; }
        /// <summary>
        /// fecha desde
        /// </summary>
        [DBParameter("FECDESDE", DBDataType.Numeric)]
        public decimal FECDESDE { get; set; }

        /// <summary>
        /// fecha hasta
        /// </summary>
        [DBParameter("FECHASTA", DBDataType.Numeric)]
        public decimal FECHASTA { get; set; }
        /// <summary>
        /// Operador portuario
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        /// <summary>
        /// Accion
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class ConsultaCitaDPW : Auditoria
    {
        /// <summary>
        /// Numero de id.
        /// </summary>
        [DBField("NUMID03")]
        public string NUMID03 { get; set; }



        /// <summary>
        /// Numero de cita
        /// </summary>
        [DBField("NUMCITA")]
        public string NUMCITA { get; set; }

        /// <summary>
        /// Numero de contenedor
        /// </summary>
        [DBField("NROCON")]
        public string NROCON { get; set; }

        /// <summary>
        /// Tipo contenedor
        /// </summary>
        [DBField("ISOTYPE")]
        public string ISOTYPE { get; set; }

        /// <summary>
        /// Numero de placa
        /// </summary>
        [DBField("NROPLACA")]
        public string NROPLACA { get; set; }

        /// <summary>
        /// Documento de chofer
        /// </summary>
        [DBField("DOCCHFR")]
        public string DOCCHFR { get; set; }

        /// <summary>
        /// RUC empresa transportista
        /// </summary>
        [DBField("RUCEMP")]
        public string RUCEMP { get; set; }

        /// <summary>
        /// Precinto 1
        /// </summary>
        [DBField("NROPREC1")]
        public string NROPREC1 { get; set; }
        /// <summary>
        /// Precinto 2
        /// </summary>
        [DBField("NROPREC2")]
        public string NROPREC2 { get; set; }
        /// <summary>
        /// Precinto 3
        /// </summary>
        [DBField("NROPREC3")]
        public string NROPREC3 { get; set; }
        /// <summary>
        /// Precinto 4
        /// </summary>
        [DBField("NROPREC4")]
        public string NROPREC4 { get; set; }
        /// <summary>
        /// TARA
        /// </summary>
        [DBField("TARA")]
        public decimal TARA { get; set; }
        /// <summary>
        /// PESO
        /// </summary>
        [DBField("PESO")]
        public decimal PESO { get; set; }
        /// <summary>
        /// NUMBKG
        /// </summary>
        [DBField("NUMBKG")]
        public string NUMBKG { get; set; }
        /// <summary>
        /// RPTASERV
        /// </summary>
        [DBField("RPTASERV")]
        public string RPTASERV { get; set; }

    }
    public class CitaDPWFormInput 
    {
       
        /// <summary>
        /// Id de Cita03
        /// </summary>
        [DBParameter("NUMID03", DBDataType.VarChar)]
        public string NUMID03 { get; set; }

        /// <summary>
        /// Numero de cita
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }
        /// <summary>
        /// Id de IDRCE
        /// </summary>
        [DBParameter("IDRCE", DBDataType.VarChar)]
        public string IDRCE { get; set; }
        /// <summary>
        /// Numero de contenedor
        /// </summary>
        [DBParameter("NROCON", DBDataType.VarChar)]
        public string NROCON { get; set; }

        /// <summary>
        /// Tipo contenedor
        /// </summary>
        [DBParameter("ISOTYPE", DBDataType.VarChar)]
        public string ISOTYPE { get; set; }

        /// <summary>
        /// Numero de placa
        /// </summary>
        [DBParameter("NROPLACA", DBDataType.VarChar)]
        public string NROPLACA { get; set; }

        /// <summary>
        /// Documento de chofer
        /// </summary>
        [DBParameter("DOCCHFR", DBDataType.VarChar)]
        public string DOCCHFR { get; set; }

        /// <summary>
        /// RUC empresa transportista
        /// </summary>
        [DBParameter("RUCEMP", DBDataType.VarChar)]
        public string RUCEMP { get; set; }

        /// <summary>
        /// Precinto 1
        /// </summary>
        [DBParameter("NROPREC1", DBDataType.VarChar)]
        public string NROPREC1 { get; set; }
        /// <summary>
        /// Precinto 2
        /// </summary>
        [DBParameter("NROPREC2", DBDataType.VarChar)]
        public string NROPREC2 { get; set; }
        /// <summary>
        /// Precinto 3
        /// </summary>
        [DBParameter("NROPREC3", DBDataType.VarChar)]
        public string NROPREC3 { get; set; }
        /// <summary>
        /// Precinto 4
        /// </summary>
        [DBParameter("NROPREC4", DBDataType.VarChar)]
        public string NROPREC4 { get; set; }
        /// <summary>
        /// TARA
        /// </summary>
        [DBParameter("TARA", DBDataType.Numeric)]
        public decimal TARA { get; set; }
        /// <summary>
        /// PESO
        /// </summary>
        [DBParameter("PESO", DBDataType.Numeric)]
        public decimal PESO { get; set; }

        /// <summary>
        /// TIPO ENVIO (M=MANUAL,A AUTOMATICO)
        /// </summary>
        [DBParameter("TIPENV", DBDataType.VarChar)]
        public string TIPENV { get; set; }

        /// <summary>
        /// RPTASERV DESCRIPCION RESPUESTA SERVIDOR
        /// </summary>
        [DBParameter("RPTASERV", DBDataType.VarChar)]
        public string RPTASERV { get; set; }

        /// <summary>
        /// ID REPSUESTA SERVIDOR (V=VERDADERO, F=FALSO)
        /// </summary>
        [DBParameter("IDRPTASERV", DBDataType.VarChar)]
        public string IDRPTASERV { get; set; }
        /// <summary>
        /// RUC OPERADOR PORTUARIO
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
        /// <summary>
        /// ACCION
        /// </summary>
        [DBParameter("ACCION", DBDataType.VarChar)]
        public string ACCION { get; set; }
    }
    public class CitaDPWQueryInput : AuditoriaParam
    {
        /// <summary>
        /// Id de Cita03
        /// </summary>
        [DBParameter("NUMID03", DBDataType.VarChar)]
        public string NUMID03 { get; set; }

        /// <summary>
        /// Numero de cita
        /// </summary>
        [DBParameter("NUMCITA", DBDataType.VarChar)]
        public string NUMCITA { get; set; }

        /// <summary>
        /// Numero de contenedor
        /// </summary>
        [DBParameter("NROCON", DBDataType.VarChar)]
        public string NROCON { get; set; }

        /// <summary>
        /// Tipo contenedor
        /// </summary>
        [DBParameter("ISOTYPE", DBDataType.VarChar)]
        public string ISOTYPE { get; set; }

        /// <summary>
        /// Numero de placa
        /// </summary>
        [DBParameter("NROPLACA", DBDataType.VarChar)]
        public string NROPLACA { get; set; }

        /// <summary>
        /// Documento de chofer
        /// </summary>
        [DBParameter("DOCCHFR", DBDataType.VarChar)]
        public string DOCCHFR { get; set; }

        /// <summary>
        /// RUC empresa transportista
        /// </summary>
        [DBParameter("RUCEMP", DBDataType.VarChar)]
        public string RUCEMP { get; set; }

        /// <summary>
        /// Precinto 1
        /// </summary>
        [DBParameter("NROPREC1", DBDataType.VarChar)]
        public string NROPREC1 { get; set; }
        /// <summary>
        /// Precinto 2
        /// </summary>
        [DBParameter("NROPREC2", DBDataType.VarChar)]
        public string NROPREC2 { get; set; }
        /// <summary>
        /// Precinto 3
        /// </summary>
        [DBParameter("NROPREC3", DBDataType.VarChar)]
        public string NROPREC3 { get; set; }
        /// <summary>
        /// Precinto 4
        /// </summary>
        [DBParameter("NROPREC4", DBDataType.VarChar)]
        public string NROPREC4 { get; set; }
        /// <summary>
        /// TARA
        /// </summary>
        [DBParameter("TARA", DBDataType.Numeric)]
        public decimal TARA { get; set; }
        /// <summary>
        /// PESO
        /// </summary>
        [DBParameter("PESO", DBDataType.Numeric)]
        public decimal PESO { get; set; }
        
        /// <summary>
        /// TIPO ENVIO (M=MANUAL,A AUTOMATICO)
        /// </summary>
        [DBParameter("TIPENV", DBDataType.VarChar)]
        public string TIPENV { get; set; }
        
        /// <summary>
        /// RPTASERV DESCRIPCION RESPUESTA SERVIDOR
        /// </summary>
        [DBParameter("RPTASERV", DBDataType.VarChar)]
        public string RPTASERV { get; set; }
        
            /// <summary>
            /// ID REPSUESTA SERVIDOR (V=VERDADERO, F=FALSO)
            /// </summary>
        [DBParameter("IDRPTASERV", DBDataType.VarChar)]
        public string IDRPTASERV { get; set; }
        /// <summary>
        /// RUC OPERADOR PORTUARIO
        /// </summary>
        [DBParameter("OPEPORT", DBDataType.VarChar)]
        public string OPEPORT { get; set; }
    }
}
