using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public interface IExecutor
    {
        protected List<IMyRow> mapedRowsFromTable { get; set; }
        protected List<IError> errorsInRows { get; set; }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet);
    }
}
