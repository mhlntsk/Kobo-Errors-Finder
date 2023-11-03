using KoboErrorFinder.Extensions.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinder.Services
{
    public class SheetsService
    {
        public void CheckFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs);

                int sheetCount = workbook.NumberOfSheets;
                
                var headersService = new HeadersService();

                var conductorService = new ConductorService();

                for (int i = 0; i < sheetCount; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);

                    IRow firstRowWithHeaders = sheet.GetRow(0); // Перший рядок - рядок з заголовками 
                    var headersOfSheet = headersService.GetHeadersDictionary(firstRowWithHeaders); // Заголовки парсяться в словник

                    conductorService.SetChekingRegardToTypeOfTheSheet(sheet, headersOfSheet); // Тут починається розподілення
                }
            }
        }
    }
}
