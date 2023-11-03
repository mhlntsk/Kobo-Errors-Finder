using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;

namespace KoboErrorFinder.Extensions.Checkers
{
    public class MHGroupOperator : AbstractOperator
    {
        List<IError> errors = new List<IError>();
        public override void Check(List<IMyRow> rows)
        {
            Console.WriteLine("=== Імітація роботи MHGroupChecker ===");
        }
        public override IError GetError()
        {
            return new MHGroupError();
        }
    }
}
