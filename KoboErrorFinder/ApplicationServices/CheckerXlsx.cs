using KoboErrorFinder.ApplicationServices;
using KoboErrorFinder.DomainServices;
using KoboErrorFinder.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinder.Services
{
    public static class CheckerXlsx
    {
        public static void CheckFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs);
                int sheetCount = workbook.NumberOfSheets;


                var sheetService = new SheetService();
                ISheet sheet = sheetService.GetSheet(sheetCount, workbook);


                var scanService = new ScanService();
                scanService.ScanSheet(sheet);
            }
        }
    }
}
