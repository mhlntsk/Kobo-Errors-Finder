using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities.Rows.Abstractions;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
