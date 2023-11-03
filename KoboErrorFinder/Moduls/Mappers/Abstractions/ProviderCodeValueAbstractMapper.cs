using KoboErrorFinder.Entities.Rows.Abstractions;
using KoboErrorFinder.Mappers;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Moduls.DataMappers.Abstractions
{
    public abstract class ProviderCodeValueAbstractMapper : AbstractMapper
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
