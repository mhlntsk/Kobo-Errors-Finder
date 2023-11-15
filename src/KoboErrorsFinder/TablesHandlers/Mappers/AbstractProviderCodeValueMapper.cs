using KoboErrorFinder.Entities.Rows.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public abstract class AbstractProviderCodeValueMapper : AbstractMapper
    {
        public void MapProviderCode(Dictionary<string, int> headersOfSheet, IProviderCodeRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Provider code"))
            {
                int columnIndex = headersOfSheet["Provider code"];
                myRow.ProviderCode = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }

    }
}
