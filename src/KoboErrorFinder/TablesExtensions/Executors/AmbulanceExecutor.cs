using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using NPOI.SS.UserModel;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using KoboErrorFinder.Entities.Errors;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public class AmbulanceExecutor : IExecutor
    {
        protected readonly IAmbulanceMapper _mapper;
        protected readonly IAmbulanceOperator _operator;
        protected readonly IAmbulancePrinter _printer;
        public List<IMyRow> mapedRowsFromTable { get; set; }
        public List<IError> errorsInRows { get; set; }
        public AmbulanceExecutor(IAmbulanceMapper ambulanceMapper, IAmbulanceOperator ambulanceOperator, IAmbulancePrinter ambulancePrinter)
        {
            _mapper = ambulanceMapper;
            _operator = ambulanceOperator;
            _printer = ambulancePrinter;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

            errorsInRows = _operator.Check(mapedRowsFromTable);

            _printer.Print(errorsInRows, mapedRowsFromTable);
        }
    }
}
