using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NSubstitute;

namespace KoboErrorFinderTests.ModulsTests.MappersTests
{
    [TestFixture]
    public class AbstractMapperTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void MapDate_DateColumnExistsAndIsValidDate_ShouldSetMyRowDate()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Date of consultation", 0 }
            };
            var basicRow = new BasicRow();

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");
            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("11-жовт.-2023");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapDate(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.Date.Should().Be(new DateOnly(2023, 10, 11));
        }

        [Test]
        public void MapDate_DateColumnExistsAndIsInvalidDate_ShouldThrowException()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Date of consultation", 0 }
            };
            var basicRow = new BasicRow();

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");
            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("InvalidValue");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act and Assert
            Assert.Throws<Exception>(() => basicMapper.MapDate(headersOfSheet, basicRow, rowFromTable));
        }

        [Test]
        public void MapDate_DateColumnDoesNotExist_ShouldNotSetMyRowDate()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>();
            var basicRow = new BasicRow();
            var rowFromTable = Substitute.For<IRow>();

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapDate(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.Date.Should().Be(default(DateOnly));
        }
    }
}
