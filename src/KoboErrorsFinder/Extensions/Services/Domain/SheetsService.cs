using KoboErrorFinder.Extensions.Services;
using KoboErrorFinder.TablesExtensions.Executors;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public class SheetsService : ISheetsService
    {
        private readonly IHeadersService _headersService;
        private readonly IConductorService _conductorService;
        public SheetsService(IHeadersService headersService, IConductorService conductorService)
        {
            _headersService = headersService;
            _conductorService = conductorService;
        }
        public void CheckAllSheets(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs);

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);

                    IRow firstRowWithHeaders = sheet.GetRow(0); // Перший рядок - рядок з заголовками 
                    var headersOfSheet = _headersService.GetHeadersDictionary(firstRowWithHeaders); // Заголовки парсяться в словник

                    IExecutor executor = _conductorService.GetExecutor(sheet.SheetName); // Тут починається розподілення
                    executor.Execute(sheet, headersOfSheet);
                }
            }
        }
    }
}
