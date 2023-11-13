using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public class MHGroupMapper : AbstractProviderCodeValueMapper, IMapper<MHGroupMapper>
    {
        public override MHGroupRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
        {
            MHGroupRow myRow = new MHGroupRow();

            MapTotalNumberOfParticipants(headersOfSheet, myRow, rowFromTable);

            MapFemaleCount(headersOfSheet, myRow, rowFromTable);
            MapMaleCount(headersOfSheet, myRow, rowFromTable);

            MapY0_4Count(headersOfSheet, myRow, rowFromTable);
            MapY5_9Count(headersOfSheet, myRow, rowFromTable);
            MapY10_14Count(headersOfSheet, myRow, rowFromTable);
            MapY15_19Count(headersOfSheet, myRow, rowFromTable);
            MapY20_44Count(headersOfSheet, myRow, rowFromTable);
            MapY45_64Count(headersOfSheet, myRow, rowFromTable);
            MapY65_PlusCount(headersOfSheet, myRow, rowFromTable);

            return myRow;
        }
        public void MapTotalNumberOfParticipants(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Total number of participants"))
            {
                int columnIndex = headersOfSheet["Total number of participants"];
                myRow.TotalNumberOfParticipants = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapFemaleCount(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Female"))
            {
                int columnIndex = headersOfSheet["Female"];
                myRow.Female = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapMaleCount(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Male"))
            {
                int columnIndex = headersOfSheet["Male"];
                myRow.Male = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY0_4Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("0-4y"))
            {
                int columnIndex = headersOfSheet["0-4y"];
                myRow.Y0_4 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY5_9Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("5-9y"))
            {
                int columnIndex = headersOfSheet["5-9y"];
                myRow.Y5_9 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY10_14Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("10-14y"))
            {
                int columnIndex = headersOfSheet["10-14y"];
                myRow.Y10_14 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY15_19Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("15-19y"))
            {
                int columnIndex = headersOfSheet["15-19y"];
                myRow.Y15_19 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY20_44Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("20-44y"))
            {
                int columnIndex = headersOfSheet["20-44y"];
                myRow.Y20_44 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY45_64Count(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("45-64y"))
            {
                int columnIndex = headersOfSheet["45-64y"];
                myRow.Y45_64 = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }
        public void MapY65_PlusCount(Dictionary<string, int> headersOfSheet, MHGroupRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("65+y"))
            {
                int columnIndex = headersOfSheet["65+y"];
                myRow.Y65_Plus = Convert.ToInt32(rowFromTable.GetCell(columnIndex)?.ToString());
            }
        }

    }
}
