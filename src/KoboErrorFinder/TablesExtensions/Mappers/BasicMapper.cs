using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public class BasicMapper : AbstractAgeValueMapper, IBasicMapper
    {
        public override IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
        {
            BasicRow myRow = new BasicRow();

            MapMsfPatientId(headersOfSheet, myRow, rowFromTable);

            MapSex(headersOfSheet, myRow, rowFromTable);

            MapAgeUnit(headersOfSheet, myRow, rowFromTable);

            MapAgeValue(headersOfSheet, myRow, rowFromTable);

            MapDate(headersOfSheet, myRow, rowFromTable);

            return myRow;
        }

        public void MapMsfPatientId(Dictionary<string, int> headersOfSheet, BasicRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("MSF Patient ID"))
            {
                int columnIndex = headersOfSheet["MSF Patient ID"];
                myRow.MSFPatientID = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }
        public void MapSex(Dictionary<string, int> headersOfSheet, BasicRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Sex"))
            {
                int columnIndex = headersOfSheet["Sex"];
                myRow.Sex = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }

    }
}
