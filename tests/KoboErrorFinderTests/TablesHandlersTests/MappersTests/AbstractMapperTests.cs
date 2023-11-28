using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NSubstitute;

namespace KoboErrorFinderTests.TablesHandlersTests.MappersTests
{
    [TestFixture]
    public class AbstractMapperTests
    {
        private ISheet sheet;

        [SetUp]
        public void SetUp()
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet("Sheet");
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

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("InvalidValue");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act and Assert
            Action action = () => basicMapper.MapDate(headersOfSheet, basicRow, rowFromTable);
            action.Should().Throw<Exception>().Where(ex => ex.Message == "Не вдається розпарсити дату");

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



        [Test]
        public void Map_WithValidData_ShouldReturnMappedRows()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Date of consultation", 0 },
            };

            IRow row = sheet.CreateRow(1);

            ICell cell0 = row.CreateCell(0);
            cell0.SetCellValue("11-жовт.-2023");
            row.Cells.Add(cell0);

            var basicMapper = new BasicMapper();

            // Act
            var result = basicMapper.Map(sheet, headersOfSheet);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.First().Date.Should().Be(new DateOnly(2023, 10, 11));
        }
        [Test]
        public void Map_WithValidData_ShouldCallInternalMethod()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Date of consultation", 0 },
            };

            IRow row0 = sheet.CreateRow(0);
            IRow row1 = sheet.CreateRow(1);
            IRow row2 = sheet.CreateRow(2);
            IRow row3 = sheet.CreateRow(3);

            ICell cell0_0 = row0.CreateCell(0);
            ICell cell0_1 = row1.CreateCell(0);
            ICell cell0_2 = row2.CreateCell(0);
            ICell cell0_3 = row3.CreateCell(0);

            cell0_0.SetCellValue("10-жовт.-2023");
            cell0_1.SetCellValue("11-жовт.-2008");
            cell0_2.SetCellValue("12-жовт.-1087");
            cell0_3.SetCellValue("13-жовт.-1975");

            row0.Cells.Add(cell0_0);
            row1.Cells.Add(cell0_1);
            row2.Cells.Add(cell0_2);
            row3.Cells.Add(cell0_3);

            var basicMapper = Substitute.For<AbstractMapper>();

            // Act
            var result = basicMapper.Map(sheet, headersOfSheet);

            // Assert
            basicMapper.Received(3).MakeSpecificMapping(Arg.Any<Dictionary<string, int>>(), Arg.Any<IRow>());
        }
        [Test]
        public void Map_WithInvalidData_ShouldReturnEmptyList()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Date of consultation", 0 },
            };

            IRow row = sheet.CreateRow(1);

            ICell cell0 = null;
            row.Cells.Add(cell0);

            var basicMapper = new BasicMapper();

            // Act
            var result = basicMapper.Map(sheet, headersOfSheet);

            // Assert
            Assert.Multiple(() => {
                result.Should().NotBeNull();
                result.Count.Should().Be(0);
            });
        }
    }
}
