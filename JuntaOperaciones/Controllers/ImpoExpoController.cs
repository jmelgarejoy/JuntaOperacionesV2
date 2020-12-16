using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globales = Ransa.Entidades.variablesGlobales;
using Entidad = Ransa.Entidades.JuntaOperativa;
using Negocio = Ransa.LogicaNegocios.JuntaOperativa;
using Ransa.Framework;
using System.Web.Helpers;

namespace JuntaOperaciones.Controllers
{
    public class ImpoExpoController : Controller
    {
        Negocio.Exportaciones exportaciones = new Negocio.Exportaciones();
        // GET: IMPOEXPO
        Negocio.Importaciones importaciones = new Negocio.Importaciones();


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetExportaciones(string desde, string hasta)
        {

            List<Entidad.Exportaciones> _resultExportaciones = new List<Entidad.Exportaciones>();
            List<Entidad.Exportaciones> resultadoDetalle = new List<Entidad.Exportaciones>();
            List<Entidad.ExportacionAgrupado> _resultExportacionesAgrupado = new List<Entidad.ExportacionAgrupado>();

            Entidad.ExportacionesInput parametros = new Entidad.ExportacionesInput();
            parametros.IN_DESDE = desde;
            parametros.IN_HASTA = hasta;
            _resultExportaciones = exportaciones.Consultar(parametros);

            var resultado = _resultExportaciones.Where(x => x.FLGETR == "N" && x.FLGOPP != "").AsEnumerable<Entidad.Exportaciones>();
            foreach (var item in resultado)
            {
                resultadoDetalle.Add(new Entidad.Exportaciones()
                {
                    NORSRN = item.NORSRN,
                    TCMPVP = item.TCMPVP.Trim(),
                    NBKNCN = item.NBKNCN.Trim(),
                    CPRCN6 = item.CPRCN6.Trim(),
                    NSRCN6 = item.NSRCN6.Trim(),
                    TPOBKN = item.TPOBKN.Trim(),
                    TTMNCN = item.TTMNCN,
                    CTPOC2 = item.CTPOC2.Trim(),
                    NGSLCN = item.NGSLCN,
                    FPSDSL = item.FPSDSL,
                    HPSDSL = item.HPSDSL,
                    FLGETR = item.FLGETR == "T" ? "SI" : "",
                    IMO = item.IMO.Trim(),
                    IQBF = item.IQBF.Trim(),
                    FLGPRC = item.FLGPRC.Trim(),
                    NPRGS2 = item.NPRGS2.Trim(),
                    INGRESOSALIDA = item.FPSDSL > 0 ? "S" : "I",
                    CONTENEDOR = item.CPRCN6 + item.NSRCN6,
                    FECHASALIDA = Helper.DevolverFormatoFecha(item.FPSDSL),
                    HORASALIDA = Helper.DevolverFormatoHora(item.HPSDSL),
                    FECHAHORASALIDA = Helper.DevolverFormatoFecha(item.FPSDSL) + ' ' + Helper.DevolverFormatoHora(item.HPSDSL),
                    PESO = item.PESO,
                    TEMBR1 = item.TEMBR1.Trim(),
                    NORDEM = item.NORDEM,
                    FCOFF1 = item.FCOFF1,
                    HCOFF1 = item.HCOFF1,
                    EXCLUSIVO = item.EXCLUSIVO.Trim(),
                    FECHAHORACUTOFF = Helper.DevolverFormatoFecha(item.FCOFF1) + " " + Helper.DevolverFormatoHora(item.HCOFF1),
                    FLGOPP = item.FLGOPP.Trim(),
                    TARA = item.TARA,
                    FECHAINGRESO = Helper.DevolverFormatoFecha(item.FGINAL),
                    HORAINGRESO = Helper.DevolverFormatoHora(item.HGINAL),
                    FECHAHORAINGRESO = Helper.DevolverFormatoFecha(item.FGINAL) + ' ' + Helper.DevolverFormatoHora(item.HGINAL),
                    PESONETO = item.PESO > 0 ? item.PESO - item.TARA : 0,
                    FECDOC = item.FECDOC,
                    FECHAENTRDOC = Helper.DevolverFormatoFecha(item.FECDOC),
                    HORDOC = item.HORDOC,
                    HORAENTRDOC = Helper.DevolverFormatoHora(item.HORDOC),
                    FECHAHORAENTRDOC = Helper.DevolverFormatoFecha(item.FECDOC) + ' ' + Helper.DevolverFormatoHora(item.HORDOC),
                    //CONTENE = item.CONTENE.Trim(),
                    REFRIGER = item.REFRIGER.Trim(),
                    SPRPRP = item.SPRPRP.Trim(),
                    CVPRCN = item.CVPRCN.Trim(),
                    QCNTSL = item.QCNTSL

                });
            }

            var ResultCantidad = resultadoDetalle.GroupBy(x => new { x.NORSRN, x.TCMPVP, x.TTMNCN, x.REFRIGER, x.QCNTSL, x.NBKNCN });

            var grupo = ResultCantidad.Select(g => new Entidad.ExportacionAgrupadoTotales
            {
                orden = int.Parse(g.Key.NORSRN.ToString()),
                nave = g.Key.TCMPVP.Trim(),
                reef = g.Key.REFRIGER,
                tamanio = g.Key.TTMNCN,
                booking = g.Key.NBKNCN,
                cantidad = g.Select(s => s.QCNTSL).Distinct().Single()
            });

            var Result = resultadoDetalle.GroupBy(x => new { x.NORSRN, x.TCMPVP, x.FECHAHORACUTOFF });
            _resultExportacionesAgrupado = Result.Select(g => new Entidad.ExportacionAgrupado
            {
                orden = int.Parse(g.Key.NORSRN.ToString()),
                nave = g.Key.TCMPVP.Trim(),
                CutOff = g.Key.FECHAHORACUTOFF,
                TotalManifestado20 = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "NO" && x.tamanio == 20).Sum(s => s.cantidad),
                TotalManifestado40 = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "NO" && x.tamanio == 40).Sum(s => s.cantidad),
                TotalManifestado20Ree = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "SI" && x.tamanio == 20).Sum(s => s.cantidad),
                TotalManifestado40Ree = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "SI" && x.tamanio == 20).Sum(s => s.cantidad),
                Total20Recibido = g.Count(r => r.TTMNCN == 20 & r.REFRIGER == "NO"),
                Total20RecibidoReef = g.Count(r => r.TTMNCN == 20 & r.REFRIGER == "SI"),
                Total20RecibidoCD = g.Count(r => r.TTMNCN == 20 & r.NORDEM >= 1 & r.REFRIGER == "NO"),
                Total20RecibidoCDReef = g.Count(r => r.TTMNCN == 20 & r.NORDEM >= 1 & r.REFRIGER == "SI"),
                Total20RecibidoSD = g.Count(r => r.TTMNCN == 20 & r.NORDEM == 0 & r.REFRIGER == "NO"),
                Total20RecibidoSDReef = g.Count(r => r.TTMNCN == 20 & r.NORDEM == 0 & r.REFRIGER == "SI"),
                Total40Recibido = g.Count(r => r.TTMNCN == 40 & r.REFRIGER == "NO"),
                Total40RecibidoReef = g.Count(r => r.TTMNCN == 40 & r.REFRIGER == "SI"),
                Total40RecibidoCD = g.Count(r => r.TTMNCN == 40 & r.NORDEM >= 1 & r.REFRIGER == "NO"),
                Total40RecibidoCDReef = g.Count(r => r.TTMNCN == 40 & r.NORDEM >= 1 & r.REFRIGER == "SI"),
                Total40RecibidoSD = g.Count(r => r.TTMNCN == 40 & r.NORDEM == 0 & r.REFRIGER == "NO"),
                Total40RecibidoSDReef = g.Count(r => r.TTMNCN == 40 & r.NORDEM == 0 & r.REFRIGER == "SI"),
                Faltan20 = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "NO" && x.tamanio == 20).Sum(s => s.cantidad) - g.Count(r => r.TTMNCN == 20 & r.REFRIGER == "NO"),
                Faltan40 = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "NO" && x.tamanio == 40).Sum(s => s.cantidad) - g.Count(r => r.TTMNCN == 40 & r.REFRIGER == "NO"), 
                Faltan20Ree = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "SI" && x.tamanio == 20).Sum(s => s.cantidad) - g.Count(r => r.TTMNCN == 20 & r.REFRIGER == "SI"),
                Faltan40Ree = grupo.Where(x => x.orden == g.Key.NORSRN && x.reef == "SI" && x.tamanio == 20).Sum(s => s.cantidad) - g.Count(r => r.TTMNCN == 20 & r.REFRIGER == "SI")
            }).ToList<Entidad.ExportacionAgrupado>();

            // _resultExportacionesAgrupado = Result;

            List<Entidad.DatosExportacion> Resultado = new List<Entidad.DatosExportacion>();
            Resultado.Add(new Entidad.DatosExportacion()
            {
                Detallado = resultadoDetalle,
                Agrupado = _resultExportacionesAgrupado
            });

            var TT = Json(Resultado);
            return TT;
        }

        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetImportaciones(string desde, string hasta)
        {
            List<Entidad.Importaciones> _resultImportaciones = new List<Entidad.Importaciones>();
            List<Entidad.ImportacionesAgrupado> _resultImportacionesAgrupado = new List<Entidad.ImportacionesAgrupado>();

            Entidad.ImportacionesQueryInput parametros = new Entidad.ImportacionesQueryInput();
            parametros.DESDE = desde;
            parametros.HASTA = hasta;

            _resultImportaciones = importaciones.Consultar(parametros);

            var Result = _resultImportaciones.GroupBy(x => new { x.NORSRN, x.TCMPVP, x.FINDSC })
               .Select(g => new Entidad.ImportacionesAgrupado
               {
                   orden = int.Parse(g.Key.NORSRN.ToString()),
                   nave = g.Key.TCMPVP.Trim(),
                   fechaEta = Helper.DevolverFormatoFecha(g.Key.FINDSC),
                   Faltan20 = g.Count(r => r.TTMNCN1 == 20 && r.NBLRCR == 0 && r.SCNRFG == "N"),
                   Faltan40 = g.Count(r => r.TTMNCN1 == 40 && r.NBLRCR == 0 && r.SCNRFG == "N"),
                   Faltan20Ree = g.Count(r => r.TTMNCN1 == 20 && r.NBLRCR == 0 && r.SCNRFG == "S"),
                   Faltan40Ree = g.Count(r => r.TTMNCN1 == 40 && r.NBLRCR == 0 && r.SCNRFG == "S"),
                   FaltanCargaSuelta = g.Where(x => x.CTPCR1 == "CS").Sum(r => r.PBRKLM),
                   Recibido20 = g.Count(r => r.TTMNCN1 == 20 && r.NBLRCR > 0 && r.SCNRFG == "N"),
                   Recibido40 = g.Count(r => r.TTMNCN1 == 40 && r.NBLRCR > 0 && r.SCNRFG == "N"),
                   Recibido20Ree = g.Count(r => r.TTMNCN1 == 20 && r.NBLRCR > 0 && r.SCNRFG == "S"),
                   Recibido40Ree = g.Count(r => r.TTMNCN1 == 40 && r.NBLRCR > 0 && r.SCNRFG == "S"),
                   RecibidoCargaSuelta = g.Where(x => x.CTPCR1 == "CS").Sum(r => r.QPBRCR),
                   Manifestado20 = g.Count(r => r.TTMNCN1 == 20 && r.SCNRFG == "N"),
                   Manifestado40 = g.Count(r => r.TTMNCN1 == 40 && r.SCNRFG == "N"),
                   Manifestado20Ree = g.Count(r => r.TTMNCN1 == 20 && r.SCNRFG == "S"),
                   Manifestado40Ree = g.Count(r => r.TTMNCN1 == 40 && r.SCNRFG == "S"),
                   ManifestadoCargaSuelta = g.Where(x => x.CTPCR1 == "CS").Sum(r => r.PBRKLM + r.QPBRCR)
               });

            _resultImportacionesAgrupado = Result.ToList<Entidad.ImportacionesAgrupado>();

            List<Entidad.DatosImportacion> Resultado = new List<Entidad.DatosImportacion>();
            Resultado.Add(new Entidad.DatosImportacion()
            {
                Detallado = _resultImportaciones,
                Agrupado = _resultImportacionesAgrupado
            });

            var TT = Json(Resultado);

            return TT;
        }


    }
}