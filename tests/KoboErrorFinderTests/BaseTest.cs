using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinderTests
{
    public abstract class BaseTest
    {
        protected XSSFWorkbook workbook;
        protected ISheet sheet;

        [OneTimeSetUp]
        public void SetUp()
        {
            workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet("Sheet");
        }
    }
}