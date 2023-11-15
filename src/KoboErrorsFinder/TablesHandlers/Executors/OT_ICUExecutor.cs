using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public class OT_ICUExecutor : IExecutor
    {
        protected readonly IMapper<OT_ICUMapper> _mapper;
        protected readonly IOperator<BasicOperator> _operator;
        protected readonly IPrinter<BasicPrinter> _printer;
        public List<IMyRow> mapedRowsFromTable { get; set; }
        public List<IError> errorsInRows { get; set; }
        public OT_ICUExecutor(IMapper<OT_ICUMapper> mapper,
                           IOperator<BasicOperator> Operator,
                           IPrinter<BasicPrinter> printer)
        {
            _mapper = mapper;
            _operator = Operator;
            _printer = printer;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

            errorsInRows = _operator.Check(mapedRowsFromTable);

            _printer.Print(errorsInRows, mapedRowsFromTable);
        }
    }
}
