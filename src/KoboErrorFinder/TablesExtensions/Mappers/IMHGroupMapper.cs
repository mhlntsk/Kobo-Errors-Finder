using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public interface IMHGroupMapper
    {
        public List<IMyRow> Map(ISheet sheet, Dictionary<string, int> headersOfSheet);
    }
}
