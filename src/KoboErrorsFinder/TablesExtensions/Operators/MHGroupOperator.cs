using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public class MHGroupOperator : AbstractOperator, IOperator<MHGroupOperator>
    {
        public List<IError> errors { get; set; } = new List<IError>();
        public List<IError> Check(List<IMyRow> rows)
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
