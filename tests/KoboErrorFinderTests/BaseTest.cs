using Bogus;
using KoboErrorFinderTests.FakeData;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KoboErrorFinderTests
{
    public abstract class BaseTest
    {
        protected Random Random { get; private set; }
        protected Faker Faker { get; private set; }
        protected FakeDataGenerator FakeDataGenerator { get; private set; }
        protected XSSFWorkbook workbook { get; private set; }
        protected ISheet sheet { get; set; }

        [OneTimeSetUp]
        public void SetupBeforeEachTest()
        {
            Faker = new Faker("uk");
            Random = new Random();
            FakeDataGenerator = new FakeDataGenerator();

            workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet("Sheet");
        }
    }
}