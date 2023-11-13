using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public class HeadersService : IHeadersService
    {
        public Dictionary<string, int> GetHeadersDictionary(IRow firstRowWithHeaders)
        {
            Dictionary<string, int> headers = new Dictionary<string, int>();

            for (int cellId = 0; cellId < firstRowWithHeaders.LastCellNum; cellId++)
            {
                ICell cell = firstRowWithHeaders.GetCell(cellId);
                if (cell != null)
                {
                    string headerText = cell.ToString();
                    headers[headerText] = cellId;
                }
            }

            return headers;
        }
    }
}
