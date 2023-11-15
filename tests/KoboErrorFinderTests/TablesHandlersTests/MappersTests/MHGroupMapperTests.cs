using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;

namespace KoboErrorFinderTests.TablesExtensionsTests.MappersTests
{
    [TestFixture]
    public class MHGroupMapperTests : BaseTest
    {
        [Test]
        public void MapTotalNumberOfParticipants_DateColumnExist_ShouldSetMyRowTotalNumberOfParticipants()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Total number of participants", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(45);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapTotalNumberOfParticipants(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.TotalNumberOfParticipants.Equals(45);

        }

        [Test]
        public void MapTotalNumberOfParticipants_DateColumnDoesNotExist_ShouldNotSetMyRowTotalNumberOfParticipants()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(45);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapTotalNumberOfParticipants(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.TotalNumberOfParticipants.Equals(0);

        }




        [Test]
        public void MapFemaleCount_DateColumnExist_ShouldSetMyRowFemale()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Female", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(25);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapFemaleCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Female.Equals(25);

        }

        [Test]
        public void MapFemaleCount_DateColumnDoesNotExist_ShouldNotSetMyRowFemale()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(25);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapFemaleCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Female.Equals(0);

        }



        [Test]
        public void MapMaleCount_DateColumnExist_ShouldSetMyRowMale()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "Male", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(20);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapMaleCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Male.Equals(20);

        }

        [Test]
        public void MapMaleCount_DateColumnDoesNotExist_ShouldNotSetMyRowMale()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(20);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapMaleCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Male.Equals(0);

        }



        [Test]
        public void MapY0_4Count_DateColumnExist_ShouldSetMyRowY0_4()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "0-4y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY0_4Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y0_4.Equals(5);

        }

        [Test]
        public void MapY0_4Count_DateColumnDoesNotExist_ShouldNotSetMyRowY0_4()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY0_4Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y0_4.Equals(0);

        }



        [Test]
        public void MapY5_9Count_DateColumnExist_ShouldSetMyRowY5_9()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "5-9y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY5_9Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y5_9.Equals(5);

        }

        [Test]
        public void MapY5_9Count_DateColumnDoesNotExist_ShouldNotSetMyRowY5_9()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue("Female");
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY5_9Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y5_9.Equals(0);

        }



        [Test]
        public void MapY10_14Count_DateColumnExist_ShouldSetMyRowY10_14()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "10-14y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY10_14Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y10_14.Equals(5);

        }

        [Test]
        public void MapY10_14Count_DateColumnDoesNotExist_ShouldNotSetMyRowY10_14()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY10_14Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y10_14.Equals(0);

        }



        [Test]
        public void MapY15_19Count_DateColumnExist_ShouldSetMyRowY15_19()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "15-19y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY15_19Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y15_19.Equals(5);

        }

        [Test]
        public void MapY15_19Count_DateColumnDoesNotExist_ShouldNotSetMyRowY15_19()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY15_19Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y15_19.Equals(0);

        }



        [Test]
        public void MapY20_44Count_DateColumnExist_ShouldSetMyRowY20_44()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "20-44y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY20_44Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y20_44.Equals(5);

        }

        [Test]
        public void MapY20_44Count_DateColumnDoesNotExist_ShouldNotSetMyRowY20_44()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY20_44Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y20_44.Equals(0);

        }



        [Test]
        public void MapY45_64Count_DateColumnExist_ShouldSetMyRowY45_64()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "45-64y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY45_64Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y45_64.Equals(5);

        }

        [Test]
        public void MapY45_64Count_DateColumnDoesNotExist_ShouldNotSetMyRowY45_64()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY45_64Count(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y45_64.Equals(0);

        }



        [Test]
        public void MapY65_PlusCount_DateColumnExist_ShouldSetMyRowY65_Plus()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "65+y", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY65_PlusCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y65_Plus.Equals(5);

        }

        [Test]
        public void MapY65_PlusCount_DateColumnDoesNotExist_ShouldNotSetMyRowY65_Plus()
        {
            // Arrange
            var headersOfSheet = new Dictionary<string, int>
            {
                { "DateColumnDoesNotExist", 0 }
            };
            var mhGroupRow = new MHGroupRow();

            IRow rowFromTable = sheet.CreateRow(0);
            ICell cell = rowFromTable.CreateCell(0);

            cell.SetCellValue(5);
            rowFromTable.Cells.Add(cell);

            var mhGroupMapper = new MHGroupMapper();

            // Act
            mhGroupMapper.MapY65_PlusCount(headersOfSheet, mhGroupRow, rowFromTable);

            // Assert
            mhGroupRow.Y65_Plus.Equals(0);

        }
    }
}
