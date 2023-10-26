using KoboErrorFinder.ApplicationServices.Xlsx;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.DomainServices
{
    public class ScanService
    {
        public void Scan(XSSFWorkbook workbook) 
        {
            int sheetCount = workbook.NumberOfSheets;

            var sheetService = new SheetChoosingServiceXlsx();
            ISheet sheet = sheetService.GetSheet(sheetCount, workbook);


            Console.WriteLine($"------------- Початок сканування файлу {sheet.SheetName} -------------");


            IRow firstRowWithHeaders = sheet.GetRow(0); // Перший рядок - заголовки               

            var headersService = new HeadersService();
            Dictionary<string, int> headersOfSheet = headersService.GetHeadersDictionary(firstRowWithHeaders);

            var readerService = new MapperXlsxService();
            var patients = readerService.ScanSheet(sheet, headersOfSheet);

            var checkerServicecs = new CheckerService();
            checkerServicecs.Check(patients);

            Console.WriteLine($"\n------------- Кінець сканування файлу {sheet.SheetName} -------------\n\n\n");
        }
    }
}
