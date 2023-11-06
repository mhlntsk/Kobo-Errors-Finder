using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public class MHGroupOperator : AbstractOperator, IMHGroupOperator
    {
        List<IError> errors = new List<IError>();
        public override List<IError> Check(List<IMyRow> rows)
        {
            Console.WriteLine("=== Імітація роботи MHGroupChecker ===");

            return errors;
        }
        public override IError GetError()
        {
            return new MHGroupError();
        }
    }
}
