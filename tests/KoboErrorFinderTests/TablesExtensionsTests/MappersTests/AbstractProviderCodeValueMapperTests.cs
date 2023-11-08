using FluentAssertions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinderTests.TablesExtensionsTests.MappersTests
{
    public class AbstractProviderCodeValueMapperTests : BaseTest
    {
        [Test]
        public void MapProviderCode_DateColumnExist_ShouldSetMyRowProviderCode()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Provider code", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Psy 2");
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapProviderCode(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.ProviderCode.Should().Be("Psy 2");

        }

        [Test]
        public void MapProviderCode_DateColumnDoesNotExist_ShouldNotSetMyRowProviderCode()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Psy 2");
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapProviderCode(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.ProviderCode.Should().Be(null);

        }


    }
}
