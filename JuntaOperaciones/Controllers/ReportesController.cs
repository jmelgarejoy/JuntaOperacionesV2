using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad = Ransa.Entidades.Reporte;
using Negocio = Ransa.LogicaNegocios.Reporte;

namespace JuntaOperaciones.Controllers
{
    public class ReportesController : Controller
    {
        Negocio.Reporte negReporte = new Negocio.Reporte();
        // GET: Reportes
        public ActionResult Transporte()
        {
            return View();
        }
        public ActionResult Index()
        {
            try
            {
               
                var Procesos = negReporte.ConsultaProcesos();
                ViewBag.Procesos = new SelectList(Procesos, "DESCPROC", "DESCPROC");
                
                return View();
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult GetReportes(string PROCESOS)
        {
            List<Entidad.GetReportes> data = new List<Entidad.GetReportes>();

            Entidad.GetReportesQueryInput parametros = new Entidad.GetReportesQueryInput();
            parametros.DESCPROC = PROCESOS;
          
            data = negReporte.ConsultaReportes(parametros);

            return Json(data); ;
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult ExportToExcelPlanificacionTransportesDetalle(string FECHA,string NORSRN,
            string DOCREF,string CONTENEDOR,string PLACA,string BREVETE,string RUCTRANSP)
        {
            Entidad.GetReporteDetalladoQueryInput input = new Entidad.GetReporteDetalladoQueryInput();
            List<Entidad.GetReporteDetalladoDesembarque> LstDetalleDesc = new List<Entidad.GetReporteDetalladoDesembarque>();
            List<Entidad.GetReporteDetalladoEmbarque> LstDetalleEmb = new List<Entidad.GetReporteDetalladoEmbarque>();
            
           
            if (NORSRN != "" || DOCREF != "" || CONTENEDOR != "")
            {
                input.XFECHA = "0";
            }
            else { input.XFECHA = FECHA; }
            if (NORSRN != "")
            {
                input.XNORSRN = NORSRN;
            }
            else
            {
                input.XNORSRN = "0";
            }

            input.XDOCREF = DOCREF;
            input.XCONTENEDOR = CONTENEDOR;
            input.XPLACA = PLACA;
            input.XBREVETE = BREVETE;
            
            if (RUCTRANSP != "")
            {
                input.XRUCTRANSP = RUCTRANSP;
            }
            else
            {
                input.XRUCTRANSP = "0";
            }
            
            LstDetalleDesc = negReporte.ConsultaDetalleDescarga(input);
            LstDetalleEmb = negReporte.ConsultaDetalleEmbarque(input);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage workbook = new ExcelPackage();
            string handle = Guid.NewGuid().ToString();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.Workbook.Properties.Author = "Ransa";
                    workbook.Workbook.Properties.Title = "Reporte de Planificación de Transportes";
                    var worksheet = workbook.Workbook.Worksheets.Add("Embarque");
                    var worksheet2 = workbook.Workbook.Worksheets.Add("Descarga");
                    worksheet.Name = "Embarque";
                    worksheet2.Name = "Descargue";
                    #region reporteEmbarque
                    int fontSizeCab1 = 12;
                    int rowIndex = 1;
                    #region CABECERA
                    using (var celda = worksheet.Cells["A" + rowIndex + ":A" + rowIndex])
                    {
                        celda.Value = "Operador";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                    {
                        celda.Value = "Tipo Operación";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        celda.Value = "Nave-Viaje";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                    {
                        celda.Value = "OS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                    {
                        celda.Value = "Fecha de Ingreso / Salida";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                    {
                        celda.Value = "Cliente";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                    {
                        celda.Value = "BL/BK";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                    {
                        celda.Value = "Contenedor";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                    {
                        celda.Value = "Tipo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        celda.Value = "Placa";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":K" + rowIndex])
                    {
                        celda.Value = "Brevete";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + rowIndex + ":L" + rowIndex])
                    {
                        celda.Value = "Chofer";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + rowIndex + ":M" + rowIndex])
                    {
                        celda.Value = "RUC Transporte";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":N" + rowIndex])
                    {
                        celda.Value = "Nombre Transporte";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + rowIndex + ":O" + rowIndex])
                    {
                        celda.Value = "IMO";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + rowIndex + ":P" + rowIndex])
                    {
                        celda.Value = "IQBF";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region Detalle
                   
                    if (LstDetalleEmb.Count > 0)
                    {
                        rowIndex += 1;
                        for (int x = 0; x < LstDetalleEmb.Count; x++)
                        {
                            using (var celda = worksheet.Cells["A" + rowIndex + ":A" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].OPERADOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].TIPOOPERACION;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].NAVEVIAJE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].ORDENSERVICIO;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                          
                            using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].FechaHoraMovimiento;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].EMBARCADOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].BLBOOKING;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].CONTENEDOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].TIPOCONTENEDOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].PLACA;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["K" + rowIndex + ":K" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].BREVETE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["L" + rowIndex + ":L" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].NOMBRECHOFER;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["M" + rowIndex + ":M" + rowIndex])
                            {
                                if (LstDetalleEmb[x].RUCTRANSPORTE == 0)
                                { celda.Value = ""; }
                                else { celda.Value = LstDetalleEmb[x].RUCTRANSPORTE; }
                                
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["N" + rowIndex + ":N" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].NOMBRETRANSPORTE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["O" + rowIndex + ":O" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].IMO;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet.Cells["P" + rowIndex + ":P" + rowIndex])
                            {
                                celda.Value = LstDetalleEmb[x].IQBF;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            rowIndex += 1;
                        }
                    }

                    #endregion
                    #endregion
                    #region reporteDescarga
                    fontSizeCab1 = 12;
                    rowIndex = 1;
                    #region CABECERA
                    using (var celda = worksheet2.Cells["A" + rowIndex + ":A" + rowIndex])
                    {
                        celda.Value = "Operador";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["B" + rowIndex + ":B" + rowIndex])
                    {
                        celda.Value = "Tipo Operación";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        celda.Value = "Nave-Viaje";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["D" + rowIndex + ":D" + rowIndex])
                    {
                        celda.Value = "OS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["E" + rowIndex + ":E" + rowIndex])
                    {
                        celda.Value = "Fecha de Ingreso / Salida";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["F" + rowIndex + ":F" + rowIndex])
                    {
                        celda.Value = "Cliente";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["G" + rowIndex + ":G" + rowIndex])
                    {
                        celda.Value = "BL/BK";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["H" + rowIndex + ":H" + rowIndex])
                    {
                        celda.Value = "Contenedor";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["I" + rowIndex + ":I" + rowIndex])
                    {
                        celda.Value = "Tipo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        celda.Value = "Placa";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["K" + rowIndex + ":K" + rowIndex])
                    {
                        celda.Value = "Brevete";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["L" + rowIndex + ":L" + rowIndex])
                    {
                        celda.Value = "Chofer";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["M" + rowIndex + ":M" + rowIndex])
                    {
                        celda.Value = "RUC Transporte";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["N" + rowIndex + ":N" + rowIndex])
                    {
                        celda.Value = "Nombre Transporte";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["O" + rowIndex + ":O" + rowIndex])
                    {
                        celda.Value = "IMO";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet2.Cells["P" + rowIndex + ":P" + rowIndex])
                    {
                        celda.Value = "IQBF";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region Detalle

                    if (LstDetalleDesc.Count > 0)
                    {
                        rowIndex += 1;
                        for (int x = 0; x < LstDetalleDesc.Count; x++)
                        {
                            using (var celda = worksheet2.Cells["A" + rowIndex + ":A" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].OPERADOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].TIPOOPERACION;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["C" + rowIndex + ":C" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].NAVEVIAJE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["D" + rowIndex + ":D" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].OSMANIFIESTO;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["E" + rowIndex + ":E" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].FechaHoraMovimiento;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["F" + rowIndex + ":F" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].CLIENTE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["G" + rowIndex + ":G" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].BLBK;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["H" + rowIndex + ":H" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].CONTENEDOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["I" + rowIndex + ":I" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].TIPOCONTENEDOR;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["J" + rowIndex + ":J" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].PLACA;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["K" + rowIndex + ":K" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].BREVETE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["L" + rowIndex + ":L" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].NOMBRECHOFER;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["M" + rowIndex + ":M" + rowIndex])
                            {
                                if (LstDetalleDesc[x].RUCTRANSPORTE == 0)
                                {
                                    celda.Value = "";
                                }
                                else { celda.Value = LstDetalleDesc[x].RUCTRANSPORTE; }
                                
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["N" + rowIndex + ":N" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].NOMBRETRANSPORTE;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["O" + rowIndex + ":O" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].IMO;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            using (var celda = worksheet2.Cells["P" + rowIndex + ":P" + rowIndex])
                            {
                                celda.Value = LstDetalleDesc[x].IQBF;
                                celda.Style.Font.Name = "Calibri";
                                celda.Style.Font.Bold = true;
                                celda.Style.Font.Size = fontSizeCab1;
                                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                celda.AutoFitColumns();
                            }
                            rowIndex += 1;
                        }
                    }

                    #endregion
                    #endregion
                    
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData[handle] = memoryStream.ToArray();


                }
                // Note we are returning a filename as well as the handle
                return Json(new { FileGuid = handle, FileName = "Reporte Detallado de Planificación de transportes (" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ").xlsx" });

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost, HelperController.ValidateHeaderAntiForgeryToken]
        public JsonResult ExportToExcelPlanificacionTransportes(string FECHA,string NORSRN)
        {
            Entidad.GetPlanTransporteQueryInput input = new Entidad.GetPlanTransporteQueryInput();
            if (NORSRN == "")
            {
                input.XFECHA = FECHA;
                input.XNORSRN = "0";
            }
            else
            {
                input.XFECHA = "0";
                input.XNORSRN = NORSRN;
            }
           
            List<Entidad.GetPlanTransporteDescarga> LstDescAPM = new List<Entidad.GetPlanTransporteDescarga>();
            List<Entidad.GetPlanTransporteDescarga> LstDescDPW = new List<Entidad.GetPlanTransporteDescarga>();
            List<Entidad.GetPlanTransporteEmbarque> LstEmbAPM = new List<Entidad.GetPlanTransporteEmbarque>();
            List<Entidad.GetPlanTransporteEmbarque> LstEmbDPW = new List<Entidad.GetPlanTransporteEmbarque>();

            LstDescAPM = negReporte.ConsultaDescargaAPM(input);
            LstDescDPW = negReporte.ConsultaDescargaDPW(input);
            LstEmbAPM = negReporte.ConsultaEmbarqueAPM(input);
            LstEmbDPW = negReporte.ConsultaEmbarqueDPW(input);

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelPackage workbook = new ExcelPackage();

            string handle = Guid.NewGuid().ToString();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.Workbook.Properties.Author = "Ransa";
                    workbook.Workbook.Properties.Title = "Reporte de Planificación de Transportes";
                    var worksheet = workbook.Workbook.Worksheets.Add("Data");
                    worksheet.Name = "Datos";
                    int fontSizeCab1 = 12;
                    //int fontSizeCab2 = 10;

                   
                    #region Report Diseño
                    #region Imagen
                    using (var celda = worksheet.Cells["B" + 2 + ":I" + 12])
                    {
                        celda.Merge = true;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    }
                    byte[] bytesImagen = new System.Drawing.ImageConverter().ConvertTo(Resource1.logo1, typeof(byte[])) as byte[];
                    Stream img = new MemoryStream(bytesImagen);
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(img))
                    // new System.Drawing.ImageConverter().ConvertTo(Resource1.Agencias555, typeof(byte[])) as byte[];
                    {
                        var excelImage = worksheet.Drawings.AddPicture("My Logo", image);
                        //add the image to row 20, column E
                        excelImage.SetPosition(2, 0, 2, 0);
                        excelImage.SetSize(350, 175);
                    }
                    #endregion

                    #region Pronostico
                    using (var celda = worksheet.Cells["J" + 2 + ":J" + 3])
                    {
                        celda.Merge = true;
                        celda.Value ="CAMIONES";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();

                    }
                    using (var celda = worksheet.Cells["K" + 2 + ":M" + 2])
                    {
                        celda.Merge = true;
                       
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia= DateTime.Today;
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0,2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + 2 + ":P" + 2])
                    {
                        celda.Merge = true;
                        
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(1);
                        fechaActual = fechaActual.AddDays(1);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + 2 + ":S" + 2])
                    {
                        //celda.Merge = true;
                        
                         celda.Merge = true;
                          
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(2);
                        fechaActual = fechaActual.AddDays(2);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + 2 + ":V" + 2])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(3);
                        fechaActual = fechaActual.AddDays(3);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + 3 + ":K" + 3])
                    {
                        //celda.Merge = true;
                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + 3 + ":L" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + 3 + ":M" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + 3 + ":N" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + 3 + ":O" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + 3 + ":P" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + 3 + ":Q" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["R" + 3 + ":R" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["S" + 3 + ":S" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + 3 + ":T" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["U" + 3 + ":U" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["V" + 3 + ":V" + 3])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + 4 + ":V" + 12])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        //celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#F0FF00"));
                    }
                    using (var celda = worksheet.Cells["J" + 4 + ":J" + 4])
                    {
                        //celda.Merge = true;

                        celda.Value = "Cantidad de CAJAS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 5 + ":J" + 5])
                    {
                        //celda.Merge = true;

                        celda.Value = "Camiones Necesarios";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 6 + ":J" + 6])
                    {
                        //celda.Merge = true;

                        celda.Value = "Camiones Actuales";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 7 + ":J" + 7])
                    {
                        //celda.Merge = true;

                        celda.Value = "KPI - utilización";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 8 + ":J" + 8])
                    {
                        //celda.Merge = true;

                        celda.Value = "Cancelados";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FE000F"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 9 + ":J" + 9])
                    {
                        //celda.Merge = true;

                        celda.Value = "APOYO EN INLAND";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 10 + ":J" + 10])
                    {
                        //celda.Merge = true;

                        celda.Value = "INOPERATIVOS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 11 + ":J" + 11])
                    {
                        //celda.Merge = true;

                        celda.Value = "CAMIONES ALQUILADOS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 12 + ":J" + 12])
                    {
                        //celda.Merge = true;

                        celda.Value = "CAMIONES RANSA Y ALQUILADOS NEW";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DCE6F1"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region Descarga APMT 
                    #region cabecera Descarga APMT
                    using (var celda = worksheet.Cells["B" + 13 + ":J" + 13])
                    {
                        celda.Merge = true;
                        DateTime fechaActual = DateTime.Now;
                        celda.Value = "CALLAO, " + fechaActual.ToLongDateString().ToUpper();
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00B050"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + 13 + ":M" + 13])
                    {
                        celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00B050"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + 13 + ":P" + 13])
                    {
                        celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00B050"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + 13 + ":S" + 13])
                    {
                        celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00B050"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + 13 + ":V" + 13])
                    {
                        celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00B050"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["B" + 14 + ":V" + 14])
                    {

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + 14 + ":C" + 14])
                    {
                        celda.Value = "DESCARGA APMT";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + 14 + ":H" + 14])
                    {
                        celda.Value = "+48T/D";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 14 + ":J" + 14])
                    {
                        celda.Value = "SITUACION";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + 14 + ":M" + 14])
                    {
                        celda.Merge = true;
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;

                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + 14 + ":P" + 14])
                    {
                        celda.Merge = true;
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(1);
                        fechaActual = fechaActual.AddDays(1);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + 14 + ":S" + 14])
                    {
                        celda.Merge = true;
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(2);
                        fechaActual = fechaActual.AddDays(2);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + 14 + ":V" + 14])
                    {
                        celda.Merge = true;
                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(3);
                        fechaActual = fechaActual.AddDays(3);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["B" + 15 + ":B" + 15])
                    {
                        celda.Value = "ETB";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + 15 + ":C" + 15])
                    {
                        celda.Value = "NAVES";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["D" + 15 + ":D" + 15])
                    {
                        celda.Value = "Total";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["E" + 15 + ":E" + 15])
                    {
                        celda.Value = "Avance";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["F" + 15 + ":F" + 15])
                    {
                        celda.Value = "Saldo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["G" + 15 + ":G" + 15])
                    {
                        celda.Value = "T/D";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + 15 + ":H" + 15])
                    {
                        celda.Value = "VENCE";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["I" + 15 + ":I" + 15])
                    {
                        celda.Value = "OS/Mfto";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 15 + ":J" + 15])
                    {
                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + 15 + ":K" + 15])
                    {
                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + 15 + ":L" + 15])
                    {
                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + 15 + ":M" + 15])
                    {
                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + 15 + ":N" + 15])
                    {
                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + 15 + ":O" + 15])
                    {
                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + 15 + ":P" + 15])
                    {
                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + 15 + ":Q" + 15])
                    {
                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["R" + 15 + ":R" + 15])
                    {
                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["S" + 15 + ":S" + 15])
                    {
                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + 15 + ":T" + 15])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["U" + 15 + ":U" + 15])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["V" + 15 + ":V" + 15])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region Detalle Descarga APMT
                    int rowIndex = 16;
                    int SumaTot = 0;
                    int SumaAvance = 0;
                    int SumaSaldo = 0;
                    
                    if (LstDescAPM.Count > 0)
                    {
                        SumaTot = 0;
                        SumaAvance = 0;
                        SumaSaldo = 0;
                        for (int x = 0; x < LstDescAPM.Count; x++)
                        {
                            if (LstDescAPM[x].Total > 0)
                            {
                                using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                                {
                                    if (!LstDescAPM[x].ETB.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescAPM[x].ETB;
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                                {
                                    celda.Value = LstDescAPM[x].NaveViaje;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                                {
                                    SumaTot = SumaTot + LstDescAPM[x].Total;
                                    celda.Value = LstDescAPM[x].Total.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                                {
                                    SumaAvance = SumaAvance + LstDescAPM[x].Avance;
                                    celda.Value = LstDescAPM[x].Avance.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                                {
                                    SumaSaldo = SumaSaldo + LstDescAPM[x].Saldo;
                                    celda.Value = LstDescAPM[x].Saldo.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                                {
                                    if (!LstDescAPM[x].TerminoDescarga.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescAPM[x].TerminoDescarga.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FE000F"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                                {
                                    if (!LstDescAPM[x].Vence.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescAPM[x].Vence.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                                {

                                    celda.Value = LstDescAPM[x].OSMANIFIESTO.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                                {

                                    celda.Value = LstDescAPM[x].Observacion.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                rowIndex += 1;
                            }
                         
                        }
                        using (var celda = worksheet.Cells["B" + 16 + ":S" + (rowIndex - 1).ToString()])
                        {
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        }
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                        {
                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                        {
                            celda.Value = SumaSaldo;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                        {
                            celda.Value = "Total Impo APMT";
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FE000F"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["K" + rowIndex + ":S" + rowIndex])
                        {
                            celda.Value = "";
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                    }
                    #endregion
                    #endregion
                    #region EMBARQUE APMT
                    #region Cabecera EMBARQUE APMT
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":V" + rowIndex])
                    {
                        
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        celda.Value = "EMBARQUE APMT";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        celda.Value = "SITUACION";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":M" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":P" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(1);
                        fechaActual = fechaActual.AddDays(1);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(2);
                        fechaActual = fechaActual.AddDays(2);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(3);
                        fechaActual = fechaActual.AddDays(3);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr.ToUpper() + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                    {
                        celda.Value = "ETB";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        celda.Value = "Naves";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                    {
                        celda.Value = "Ingresados";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                    {
                        celda.Value = "Avance";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                    {
                        celda.Value = "Saldo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                    {
                        celda.Value = "I. Stacking";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                    {
                        celda.Value = "CutOff Dry / Reefer";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                    {
                        celda.Value = "OS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":K" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + rowIndex + ":L" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + rowIndex + ":M" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":N" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + rowIndex + ":O" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + rowIndex + ":P" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":Q" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["R" + rowIndex + ":R" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["S" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":T" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["U" + rowIndex + ":U" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["V" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region DETALLE EMBARQUE APMT
                    if (LstEmbAPM.Count > 0)
                    {
                        rowIndex += 1;
                        SumaTot = 0;
                        SumaAvance = 0;
                        SumaSaldo = 0;
                        for (int x = 0; x < LstEmbAPM.Count; x++)
                        {
                            if (LstEmbAPM[x].Ingresados > 0)
                            {
                                using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                                {
                                    if (!LstEmbAPM[x].ETB.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbAPM[x].ETB.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstEmbAPM[x].NaveViaje;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaTot = SumaTot + LstEmbAPM[x].Ingresados;
                                    celda.Value = LstEmbAPM[x].Ingresados;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    //celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaAvance = SumaAvance + LstEmbAPM[x].Avance;
                                    celda.Value = LstEmbAPM[x].Avance;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    //celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaSaldo = SumaSaldo + LstEmbAPM[x].Saldo;
                                    celda.Value = LstEmbAPM[x].Saldo;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    //celda.Style.Numberformat.Format = "#";
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                                {
                                    if (!LstEmbAPM[x].InicioStackin.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbAPM[x].InicioStackin.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                                {
                                    if (!LstEmbAPM[x].CutOff.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbAPM[x].CutOff.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstEmbAPM[x].ORDENSERVICIO.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstEmbAPM[x].Observacion;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                                {
                                    //celda.Merge = true;


                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                rowIndex += 1;
                            }
                          
                        }

                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            //celda.Merge = true;
                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                        {
                            //celda.Merge = true;
                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                        {
                            //celda.Merge = true;
                            celda.Value = SumaSaldo;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                        {
                            //celda.Merge = true;
                            celda.Value = "Total Expo APMT";
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                        {
                            //celda.Merge = true;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                    }
                    #endregion
                    #endregion
                    #region Resumen APMT
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "TOTAL CAJAS APMT";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "Camiones en APMT";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region DESCARGA DPW
                    #region CABECERA DESCARGA DPW
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "DESCARGA DPW";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "SITUACION";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":M" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":P" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(1);
                        fechaActual = fechaActual.AddDays(1);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(2);
                        fechaActual = fechaActual.AddDays(2);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(3);
                        fechaActual = fechaActual.AddDays(3);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "ETB";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Naves";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Total";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Avance";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Saldo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "T/D";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "VENCE";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "OS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":K" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + rowIndex + ":L" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + rowIndex + ":M" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":N" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + rowIndex + ":O" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + rowIndex + ":P" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":Q" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["R" + rowIndex + ":R" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["S" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":T" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["U" + rowIndex + ":U" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["V" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    #endregion
                    #region DETALLE DESCARGA DPW
                    if (LstDescDPW.Count > 0)
                    {
                        rowIndex += 1;
                        SumaTot = 0;
                        SumaAvance = 0;
                        SumaSaldo = 0;
                        for (int x = 0; x < LstDescDPW.Count; x++)
                        {
                            if (LstDescDPW[x].Total > 0)
                            {
                                using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                                {
                                    if (!LstDescDPW[x].ETB.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescDPW[x].ETB.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstDescDPW[x].NaveViaje;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaTot = SumaTot + LstDescDPW[x].Total;
                                    celda.Value = LstDescDPW[x].Total;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaAvance = SumaAvance + LstDescDPW[x].Avance;
                                    celda.Value = LstDescDPW[x].Avance;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                                {
                                    //celda.Merge = true;
                                    SumaSaldo = SumaSaldo + LstDescDPW[x].Saldo;
                                    celda.Value = LstDescDPW[x].Saldo;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                                {
                                    if (!LstDescDPW[x].TerminoDescarga.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescDPW[x].TerminoDescarga.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                                {
                                    if (!LstDescDPW[x].Vence.ToString().Equals("0"))
                                    {
                                        celda.Value = LstDescDPW[x].Vence.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstDescDPW[x].OSMANIFIESTO.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstDescDPW[x].Observacion;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                                {
                                    //celda.Merge = true;


                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                rowIndex += 1;
                            }
                         
                        }
                        
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaSaldo;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = "Total Impo DPW";
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                        {
                            //celda.Merge = true;


                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                    }
                    #endregion
                    #endregion
                    #region EMBARQUE DPW
                    #region CABECERA EMBARQUE DPW
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "EMBARQUE DPW";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "SITUACION";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":M" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }

                    using (var celda = worksheet.Cells["N" + rowIndex + ":P" + rowIndex])
                    {
                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(1);
                        fechaActual = fechaActual.AddDays(1);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(2);
                        fechaActual = fechaActual.AddDays(2);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Merge = true;

                        DateTime fechaActual = DateTime.Now;
                        DateTime dia = DateTime.Today;
                        dia = dia.AddDays(3);
                        fechaActual = fechaActual.AddDays(3);
                        string diaStr = fechaActual.ToString("dddd");
                        celda.Value = diaStr + " " + dia.ToString().Substring(0, 2);
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "ETB";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Naves";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Ingresados";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Avance";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Saldo";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "I. Stacking";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "CutOff Dry / Reefer";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "OS";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":K" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["L" + rowIndex + ":L" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["M" + rowIndex + ":M" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["N" + rowIndex + ":N" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/1";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["O" + rowIndex + ":O" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/2";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["P" + rowIndex + ":P" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/0";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["Q" + rowIndex + ":Q" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["R" + rowIndex + ":R" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["S" + rowIndex + ":S" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["T" + rowIndex + ":T" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "07/15";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["U" + rowIndex + ":U" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "15/23";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["V" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "23/07";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }

                    #endregion
                    #region DETALLE EMBARQUE DPW
                    if (LstEmbDPW.Count > 0)
                    {
                        rowIndex += 1;
                        SumaTot = 0;
                        SumaAvance = 0;
                        SumaSaldo = 0;
                        for (int x = 0; x < LstEmbDPW.Count; x++)
                        {
                            if (LstEmbDPW[x].Ingresados > 0)
                            {
                                using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                                {
                                    if (!LstEmbDPW[x].ETB.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbDPW[x].ETB.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["C" + rowIndex + ":C" + rowIndex])
                                {
                                    //celda.Merge = true;

                                    celda.Value = LstEmbDPW[x].NaveViaje;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                                {
                                    SumaTot = SumaTot + LstEmbDPW[x].Ingresados;
                                    celda.Value = LstEmbDPW[x].Ingresados;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                                {
                                    SumaAvance = SumaAvance + LstEmbDPW[x].Avance;
                                    celda.Value = LstEmbDPW[x].Avance;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                                {
                                    SumaSaldo = SumaSaldo + LstEmbDPW[x].Saldo;
                                    celda.Value = LstEmbDPW[x].Saldo;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                                {
                                    if (!LstEmbDPW[x].InicioStackin.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbDPW[x].InicioStackin.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["H" + rowIndex + ":H" + rowIndex])
                                {
                                    if (!LstEmbDPW[x].CutOff.ToString().Equals("0"))
                                    {
                                        celda.Value = LstEmbDPW[x].CutOff.ToString();
                                    }
                                    else
                                    {
                                        celda.Value = "";
                                    }

                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["I" + rowIndex + ":I" + rowIndex])
                                {
                                    celda.Value = LstEmbDPW[x].ORDENSERVICIO.ToString();
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                                {
                                    celda.Value = LstEmbDPW[x].Observacion;
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                                {
                                    celda.Style.Font.Name = "Calibri";
                                    celda.Style.Font.Bold = true;
                                    celda.Style.Font.Size = fontSizeCab1;
                                    celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                                    celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                                    celda.AutoFitColumns();
                                }
                                rowIndex += 1;
                            }
                          
                        }
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                        {
                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["F" + rowIndex + ":F" + rowIndex])
                        {
                            celda.Value = SumaSaldo;
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                        {
                            celda.Value = "Total Expo DPW";
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                        {
                            celda.Style.Font.Name = "Calibri";
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                            celda.AutoFitColumns();
                        }
                    }
                    #endregion
                    #endregion
                    #region Resumen DPW
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;
                        celda.Value = "TOTAL CAJAS DPW";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
            
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Camiones en DPW";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["I" + rowIndex + ":I" + (rowIndex + 2)])
                    {
                        celda.Merge = true;

                        celda.Value = "CAMIONES";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "SINI";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Logisminsa";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    rowIndex += 1;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "Total Camiones Turno";
                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Calibri";
                        celda.Style.Font.Bold = true;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                        celda.AutoFitColumns();
                    }
                    #endregion

                    rowIndex++;
                  
                    worksheet.Protection.IsProtected = false;
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData[handle] = memoryStream.ToArray();

                    #endregion
                }
                // Note we are returning a filename as well as the handle
                return Json(new { FileGuid = handle, FileName = "Data Planificación de transportes (" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ").xlsx" });

            }
            catch (Exception EX)
            {
                throw;
            }
        }
        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
    }
}