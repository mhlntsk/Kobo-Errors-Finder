using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;

namespace KoboErrorFinderTests.TablesHandlersTests.MappersTests
{
    [TestFixture]
    public class BasicMapperTests : BaseTest
    {
        [Test]
        public void MapMsfPatientId_DateColumnExist_ShouldSetMyRowMSFPatientID()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "MSF Patient ID", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("DE-TET-MH-210823-003");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapMsfPatientId(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.MSFPatientID.Should().Be("DE-TET-MH-210823-003");

        }

        [Test]
        public void MapMsfPatientId_DateColumnDoesNotExist_ShouldNotSetMyRowMSFPatientID()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("DE-TET-MH-210823-003");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapMsfPatientId(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.MSFPatientID.Should().Be(null);

        }




        [Test]
        public void MapSex_DateColumnExist_ShouldSetMyRowSex()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Sex", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Female");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapSex(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.Sex.Should().Be("Female");

        }

        [Test]
        public void MapSex_DateColumnDoesNotExist_ShouldNotSetMyRowSex()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Female");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapSex(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.Sex.Should().Be(null);

        }
    }
}
