using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public abstract class AbstractPrinter
    {        
        public void Print(List<IError> errors, List<IMyRow> ambulanseRows)
        {
            if (errors == null || errors.Count == 0)
            {
                Console.WriteLine($"No errors were found in this table.");
                return;
            }

            MakeSpecificPrinting(errors, ambulanseRows);
        }
        public abstract void MakeSpecificPrinting(List<IError> errors, List<IMyRow> ambulanseRows);

    }
}
