using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinderTests.TablesExtensionsTests.MappersTests
{
    public class AbstractAgeValueMapperTests : BaseTest
    {
        [Test]
        public void MapAgeUnit_DateColumnExist_ShouldSetMyRowAgeUnit()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Age unit", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Months");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapAgeUnit(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.AgeUnit.Should().Be("Months");

        }

        [Test]
        public void MapAgeUnit_DateColumnDoesNotExist_ShouldNotSetMyRowAgeUnit()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Months");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapAgeUnit(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.AgeUnit.Should().Be(null);

        }




        [Test]
        public void MapAgeValue_DateColumnExist_ShouldSetMyRowAgeValue()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Age value", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("11");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapAgeValue(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.AgeValue.Should().Be("11");

        }

        [Test]
        public void MapAgeValue_DateColumnDoesNotExist_ShouldNotSetMyRowAgeValue()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var basicRow = new BasicRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("11");
            rowFromTable.Cells.Add(cell);

            var basicMapper = new BasicMapper();

            // Act
            basicMapper.MapAgeValue(headersOfSheet, basicRow, rowFromTable);

            // Assert
            basicRow.AgeValue.Should().Be(null);

        }
    }
}
