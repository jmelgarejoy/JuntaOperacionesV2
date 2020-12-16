using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuntaOperaciones.Models
{
    public class PlanificacionModel
    {

    }

    public class Planificacion
    {

        public string IDJTAOPE { get; set; }

        public string FCINPLN { get; set; }

        public string HORAINI { get; set; }

        public string FCFNPLN { get; set; }

        public string HORAFIN { get; set; }

        public int CNTTUR3 { get; set; }

        public int CNTTUR1 { get; set; }

        public int CNTTUR2 { get; set; }

        public string AUTH1 { get; set; }

        public string AUTH2 { get; set; }

        public string AUTH3 { get; set; }

        public string AUTH4 { get; set; }
        public string AUTH1OBS { get; set; }

        public string AUTH2OBS { get; set; }

        public string AUTH3OBS { get; set; }

        public string AUTH4OBS { get; set; }

        public string USERCRE { get; set; }

        public decimal FECHCRE { get; set; }

        public decimal HORCRE { get; set; }

        public string USERUPD { get; set; }

        public decimal FECHUPD { get; set; }

        public decimal HORUPD { get; set; }

        public string ESTADO { get; set; }
        public string ACCION { get; set; }
        public string DETALLE { get; set; }
        //List<PlanificacionDetalle> DETALLE { get; set; }

        public Planificacion()
        {
            IDJTAOPE = string.Empty;
            FCINPLN = string.Empty;
            HORAINI = string.Empty;
            FCFNPLN = string.Empty;
            HORAFIN = string.Empty;
            CNTTUR3 = 0;
            CNTTUR1 = 0;
            CNTTUR2 = 0;
            AUTH1 = string.Empty;
            AUTH2 = string.Empty;
            AUTH3 = string.Empty;
            AUTH4 = string.Empty;
            USERCRE = string.Empty;
            FECHCRE = 0;
            HORCRE = 0;
            USERUPD = string.Empty;
            FECHUPD = 0;
            HORUPD = 0;
            ESTADO = string.Empty;
          
            //DETALLE = new List<PlanificacionDetalle>();
        }

        public class PlanificacionDetalle
        {

            public string IDJTAOPE { get; set; }
            public decimal ORDEN { get; set; }
            public string CODNAVE { get; set; }
            public string CONTENE { get; set; }
            public string CLASE { get; set; }
            public string OPEPORTU { get; set; }
            public int TAMANIO { get; set; }
            public decimal PESOMAN { get; set; }
            public string TIPOCONT { get; set; }
            public string REFRIGER { get; set; }
            public string FCHFNDSC { get; set; }
            public string HORFNDSC { get; set; }
            public string TIPOPLAN { get; set; }
            public string FCHCUTOFF { get; set; }
            public string HORCUTOFF { get; set; }
            public string FCHCTOFFR { get; set; }
            public string HORCTOFFR { get; set; }
            public string ESTADO { get; set; }
            public PlanificacionDetalle()
            {
                IDJTAOPE = string.Empty;
                ORDEN = 0;
                CODNAVE = string.Empty;
                CONTENE = string.Empty;
                CLASE = string.Empty;
                OPEPORTU  = string.Empty;
                TAMANIO = 0;
                PESOMAN = 0;
                TIPOCONT = string.Empty;
                REFRIGER = string.Empty;
                FCHFNDSC = string.Empty;
                HORFNDSC = string.Empty;
                TIPOPLAN = string.Empty;
                FCHCUTOFF = string.Empty;
                HORCUTOFF = string.Empty;
                FCHCTOFFR = string.Empty;
                HORCTOFFR = string.Empty;
                ESTADO = string.Empty;
            }

        }
    }
}