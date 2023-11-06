using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public class MHGroupPrinter : AbstractPrinter, IMHGroupPrinter
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            Console.WriteLine("=== Імітація роботи MHGroupPrinting ===");
        }
    }
}
