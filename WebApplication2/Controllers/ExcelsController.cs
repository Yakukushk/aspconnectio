using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelView;

using OfficeOpenXml;
using OfficeOpenXml.Style;
namespace WebApplication2.Controllers
{
    public class ExcelsController : Controller
    {
        IExcel excel = null;
        List<Excels> excels = new List<Excels>();
        public ExcelsController(IExcel _excel) {
            excel = _excel;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult SaveExcels(List<Excels> _excels)
        {
            excels = excel.SaveExcels(_excels);
            return Json(excels);
        }
        public string GenerateAndDownloadExcel(int  excelID, string name)
        {
            List<Excels> excels = excel.GetExcels();
            var dataTable = CommonMethod.ConvertListDataTable(excels);
            dataTable.Columns.Remove("Id");
            byte[] file = null;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage()) {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Excels");
                ws.Cells["A1"].Value = "City Name";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 16;
                ws.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells["A2"].Value = "Country List";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells["A3"].LoadFromDataTable(dataTable, true);
                ws.Cells["A3:C3"].Style.Font.Bold = true;
                ws.Cells["A3:C3"].Style.Font.Size = 12;
                ws.Cells["A3:C3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A3:C3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);
                ws.Cells["A3:C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A3:C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                pck.Save();
                file = pck.GetAsByteArray();
            }
            return Convert.ToBase64String(file);

        }
    }
}
