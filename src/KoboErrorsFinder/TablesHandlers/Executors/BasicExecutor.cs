using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public class BasicExecutor : IExecutor
    {
        protected readonly IMapper<BasicMapper> _mapper;
        protected readonly IOperator<BasicOperator> _operator;
        protected readonly IPrinter<BasicPrinter> _printer;
        public List<IMyRow> mapedRowsFromTable { get; set; }
        public List<IError> errorsInRows { get; set; }
        public BasicExecutor(IMapper<BasicMapper> basicMapper, IOperator<BasicOperator> basicOperator, IPrinter<BasicPrinter> basicPrinter)
        {
            _mapper = basicMapper;
            _operator = basicOperator;
            _printer = basicPrinter;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

            errorsInRows = _operator.Check(mapedRowsFromTable);

            _printer.Print(errorsInRows, mapedRowsFromTable);
        }
    }
}
