using FluentAssertions;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities.Rows.Abstractions;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using NSubstitute;

namespace KoboErrorFinderTests.TablesExtensionsTests.MappersTests
{
    [TestFixture]
    public class AmbulanceMapperTests : BaseTest
    {
        [Test]
        public void MapAutoFormId_DateColumnExist_ShouldSetMyRowAuto_form_id()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "auto_form_id", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Ambulance-230803-Команда 10-3.03.08");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapAutoFormId(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.Auto_form_id.Should().Be("Ambulance-230803-Команда 10-3.03.08");

        }

        [Test]
        public void MapAutoFormId_DateColumnDoesNotExist_ShouldNotSetMyRowAuto_form_id()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Ambulance-230803-Команда 10-3.03.08");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapAutoFormId(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.Auto_form_id.Should().Be(null);

        }




        [Test]
        public void MapRegionOfPatient_DateColumnExist_ShouldSetMyRowRegionOfPatient()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Region of patient", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Donetsk");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapRegionOfPatient(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.RegionOfPatient.Should().Be("Donetsk");

        }

        [Test]
        public void MapRegionOfPatient_DateColumnDoesNotExist_ShouldNotSetMyRowRegionOfPatient()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Donetsk");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapRegionOfPatient(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.RegionOfPatient.Should().Be(null);

        }




        [Test]
        public void MapOtherPatientLocationSpecify_DateColumnExist_ShouldSetOtherPatientLocationSpecify()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Other Patient location: specify", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Tokmakivka");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapOtherPatientLocationSpecify(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.OtherPatientLocationSpecify.Should().Be("Tokmakivka");

        }

        [Test]
        public void MapOtherPatientLocationSpecify_DateColumnDoesNotExist_ShouldNotSetMyRowOtherPatientLocationSpecify()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Tokmakivka");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapOtherPatientLocationSpecify(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.OtherPatientLocationSpecify.Should().Be(null);

        }




        [Test]
        public void MapTypeOfRequester_DateColumnExist_ShouldSetMyRowTypeOfRequester()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Type of requester", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("MSF Train");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapTypeOfRequester(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.TypeOfRequester.Should().Be("MSF Train");

        }

        [Test]
        public void MapTypeOfRequester_DateColumnDoesNotExist_ShouldNotSetMyRowTypeOfRequester()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var ambulanceRow = new AmbulanceRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("MSF Train");
            rowFromTable.Cells.Add(cell);

            var ambulanceMapper = new AmbulanceMapper();

            // Act
            ambulanceMapper.MapTypeOfRequester(headersOfSheet, ambulanceRow, rowFromTable);

            // Assert
            ambulanceRow.TypeOfRequester.Should().Be(null);

        }
    }
}
