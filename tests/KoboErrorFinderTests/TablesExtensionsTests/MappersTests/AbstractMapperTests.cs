using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NSubstitute;

namespace KoboErrorFinderTests.TablesExtensionsTests.MappersTests
{
    [TestFixture]
    public class AbstractMapperTests : BaseTest
    {
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
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }
    }
}
