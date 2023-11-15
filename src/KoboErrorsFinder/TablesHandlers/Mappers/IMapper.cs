using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public interface IMapper<T> where T : class
    {
        public List<IMyRow> Map(ISheet sheet, Dictionary<string, int> headersOfSheet);
    }
}
