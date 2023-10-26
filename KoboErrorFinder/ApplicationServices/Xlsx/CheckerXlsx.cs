using KoboErrorFinder.ApplicationServices;
using KoboErrorFinder.DomainServices;
using KoboErrorFinder.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinder.ApplicationServices.Xlsx
{
    public class CheckerXlsx
    {
        public bool GoOnWithCurrentFile { get; set; } = true;
        public void CheckFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs);

                do
                {
                    var scanService = new ScanService();
                    scanService.Scan(workbook);

                    Console.Write("\n\tПродовжити в цьому ж файлі? (Y \\ N): ");
                    char goOnWithCurrentFile = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    if (goOnWithCurrentFile != 'y' && goOnWithCurrentFile != 'т')
                    {
                        GoOnWithCurrentFile = false;
                    }

                } while (GoOnWithCurrentFile);
            }
        }
    }
}
