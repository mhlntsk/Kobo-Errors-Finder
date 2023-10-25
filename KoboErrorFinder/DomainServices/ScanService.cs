using KoboErrorFinder.ApplicationServices;
using KoboErrorFinder.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.DomainServices
{
    public class ScanService
    {
        public void ScanSheet(ISheet sheet) 
        {
            IRow firstRowWithHeaders = sheet.GetRow(0); // Перший рядок - заголовок                

            if (firstRowWithHeaders != null)
            {
                Console.WriteLine($"------------- Початок сканування файлу {sheet.SheetName} -------------");

                List<Patient> patients = new List<Patient>();

                var headersService = new HeadersService();
                Dictionary<string, int> headersOfSheet = headersService.GetHeadersDictionary(firstRowWithHeaders);

                var readerService = new ReaderXlsx();
                readerService.ReadSheet(sheet, headersOfSheet, patients);

                var checkerServicecs = new CheckerService();
                checkerServicecs.Check(patients);

                Console.WriteLine($"\n------------- Кінець сканування файлу {sheet.SheetName} -------------\n\n\n\n\n\n");
            }
            else
            {
                Console.WriteLine("Заголовок рядку не знайдено.");
            }
        }
    }
}
