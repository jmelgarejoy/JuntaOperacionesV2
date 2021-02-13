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
        public JsonResult ExportToExcelPlanificacionTransportes(string FECHA)
        {
            Entidad.GetPlanTransporteQueryInput input = new Entidad.GetPlanTransporteQueryInput();
            input.XFECHA = FECHA;
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
                    int fontSizeCab2 = 10;

                   
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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

                        celda.Value = "cantidad de CAJAS";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 5 + ":J" + 5])
                    {
                        //celda.Merge = true;

                        celda.Value = "camiones Necesarios";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 6 + ":J" + 6])
                    {
                        //celda.Merge = true;

                        celda.Value = "camiones Actuales";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 7 + ":J" + 7])
                    {
                        //celda.Merge = true;

                        celda.Value = "KPI - utilización";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 8 + ":J" + 8])
                    {
                        //celda.Merge = true;

                        celda.Value = "Cancelados";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FE000F"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 9 + ":J" + 9])
                    {
                        //celda.Merge = true;

                        celda.Value = "APOYO EN INLAND";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 10 + ":J" + 10])
                    {
                        //celda.Merge = true;

                        celda.Value = "INOPERATIVOS";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 11 + ":J" + 11])
                    {
                        //celda.Merge = true;

                        celda.Value = "CAMIONES ALQUILADOS";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
                        celda.AutoFitColumns();
                    }
                    using (var celda = worksheet.Cells["J" + 12 + ":J" + 12])
                    {
                        //celda.Merge = true;

                        celda.Value = "CAMIONES RANSA Y ALQUILADOS NEW";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
                        celda.Style.Font.Size = fontSizeCab1;
                        celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                        celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EBF1DE"));
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                    #region Dest Descarga APMT
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
                            using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                celda.Value = LstDescAPM[x].ETB;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaAvance = SumaAvance + LstDescAPM[x].Avance;
                                celda.Value = LstDescAPM[x].Avance.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaSaldo = SumaSaldo + LstDescAPM[x].Saldo;
                                celda.Value = LstDescAPM[x].Saldo.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                            using (var celda = worksheet.Cells["G" + rowIndex + ":G" + rowIndex])
                            {

                                celda.Value = LstDescAPM[x].TerminoDescarga.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescAPM[x].Vence.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescAPM[x].OSManifiesto.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                        
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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

                            using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                //celda.Merge = true;

                                celda.Value = LstEmbAPM[x].ETB;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Value = LstEmbAPM[x].Ingresados.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaAvance = SumaAvance + LstEmbAPM[x].Avance;
                                celda.Value = LstEmbAPM[x].Avance.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaSaldo = SumaSaldo + LstEmbAPM[x].Saldo;
                                celda.Value = LstEmbAPM[x].Saldo.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                //celda.Merge = true;

                                celda.Value = LstEmbAPM[x].InicioStackin;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                //celda.Merge = true;

                                celda.Value = LstEmbAPM[x].CutOff;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstEmbAPM[x].OrdenServicio;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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


                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
                            celda.Style.Font.Size = fontSizeCab1;
                            celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            celda.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            celda.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#000000"));
                            celda.AutoFitColumns();
                        }
                        using (var celda = worksheet.Cells["E" + rowIndex + ":E" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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


                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                    #region Resumen APMT
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "TOTAL CAJAS APMT";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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

                    #endregion
                    #region DESCARGA DPW
                    #region CABECERA DESCARGA DPW
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                            using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                //celda.Merge = true;

                                celda.Value = LstDescDPW[x].ETB;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescDPW[x].NaveViaje;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaTot = SumaTot + LstDescDPW[x].Total;
                                celda.Value = LstDescDPW[x].Total.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaAvance = SumaAvance + LstDescDPW[x].Avance;
                                celda.Value = LstDescDPW[x].Avance.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaSaldo = SumaSaldo + LstDescDPW[x].Saldo;
                                celda.Value = LstDescDPW[x].Saldo.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescDPW[x].TerminoDescarga;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescDPW[x].Vence;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescDPW[x].OSManifiesto;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstDescDPW[x].Observacion;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                            using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                            {
                                //celda.Merge = true;


                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                        }
                        rowIndex += 1;
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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

                            celda.Value = SumaAvance;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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

                            celda.Value = SumaSaldo;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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

                            celda.Value = "Total Impo DPW";
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                        using (var celda = worksheet.Cells["K" + rowIndex + ":V" + rowIndex])
                        {
                            //celda.Merge = true;


                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                    }
                    #endregion

                    #endregion
                    #region EMBARQUE DPW
                    #region CABECERA EMBARQUE DPW
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["B" + rowIndex + ":V" + rowIndex])
                    {
                        //celda.Merge = true;

                        
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                    #endregion
                    if (LstEmbDPW.Count > 0)
                    {
                        rowIndex += 1;
                        SumaTot = 0;
                        SumaAvance = 0;
                        SumaSaldo = 0;
                        for (int x = 0; x < LstEmbDPW.Count; x++)
                        {
                            using (var celda = worksheet.Cells["B" + rowIndex + ":B" + rowIndex])
                            {
                                //celda.Merge = true;

                                celda.Value = LstEmbDPW[x].ETB;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaTot = SumaTot + LstEmbDPW[x].Ingresados;
                                celda.Value = LstEmbDPW[x].Ingresados.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaAvance = SumaAvance + LstEmbDPW[x].Avance;
                                celda.Value = LstEmbDPW[x].Avance.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                SumaSaldo = SumaSaldo + LstEmbDPW[x].Saldo;
                                celda.Value = LstEmbDPW[x].Saldo.ToString();
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                //celda.Merge = true;

                                celda.Value = LstEmbDPW[x].InicioStackin;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                                //celda.Merge = true;

                                celda.Value = LstEmbDPW[x].CutOff;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstEmbDPW[x].OrdenServicio;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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

                                celda.Value = LstEmbDPW[x].Observacion;
                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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


                                celda.Style.Font.Name = "Arial";
                                celda.Style.Font.Bold = false;
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
                        rowIndex += 1;
                        using (var celda = worksheet.Cells["D" + rowIndex + ":D" + rowIndex])
                        {
                            //celda.Merge = true;

                            celda.Value = SumaTot;
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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

                            celda.Value = "Total Expo DPW";
                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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


                            celda.Style.Font.Name = "Arial";
                            celda.Style.Font.Bold = false;
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
                    rowIndex += 2;
                    using (var celda = worksheet.Cells["J" + rowIndex + ":J" + rowIndex])
                    {
                        //celda.Merge = true;

                        celda.Value = "TOTAL CAJAS DPW";
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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


                        celda.Style.Font.Name = "Arial";
                        celda.Style.Font.Bold = false;
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
                  
                    worksheet.Protection.IsProtected = true;
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData[handle] = memoryStream.ToArray();
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