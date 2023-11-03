using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.Services;

namespace KoboErrorFinder.Moduls.Printers
{
    public class MHGroupPrinter : AbstractPrinter
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            Console.WriteLine("=== Імітація роботи MHGroupPrinting ===");
        }
    }
}
