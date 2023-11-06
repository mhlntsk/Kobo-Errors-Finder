using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public interface IExecutor
    {
        protected List<IMyRow> rows { get; set; }
        protected List<IError> errors { get; set; }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet);
    }
}
