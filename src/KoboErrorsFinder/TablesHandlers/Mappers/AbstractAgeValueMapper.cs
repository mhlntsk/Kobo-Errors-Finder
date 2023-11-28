using KoboErrorFinder.Entities.Rows.Abstractions;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public abstract class AbstractAgeValueMapper : AbstractMapper
    {
        public void MapAgeUnit(Dictionary<string, int> headersOfSheet, IAgeRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Age unit"))
            {
                int columnIndex = headersOfSheet["Age unit"];
                myRow.AgeUnit = rowFromTable.GetCell(columnIndex)?.ToString();
            }

        }
        public void MapAgeValue(Dictionary<string, int> headersOfSheet, IAgeRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Age value"))
            {
                int columnIndex = headersOfSheet["Age value"];
                myRow.AgeValue = rowFromTable.GetCell(columnIndex)?.ToString();
            }

            if (string.IsNullOrEmpty(myRow.AgeValue))
            {
                if (headersOfSheet.ContainsKey("Age"))
                {
                    int columnIndex = headersOfSheet["Age"];
                    myRow.AgeValue = rowFromTable.GetCell(columnIndex)?.ToString();
                }
            }
        }
    }
}
