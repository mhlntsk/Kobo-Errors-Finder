using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public interface IMHGroupPrinter
    {
        public void Print(List<IError> errors, List<IMyRow> ambulanseRows);
    }
}
