using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder;
using KoboErrorFinder;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using KoboErrorFinder.Entities.Errors;
using NPOI.SS.Formula.Functions;

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
