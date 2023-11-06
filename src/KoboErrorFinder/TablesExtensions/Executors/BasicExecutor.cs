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
        protected readonly IBasicMapper _mapper;
        protected readonly IBasicOperator _operator;
        protected readonly IBasicPrinter _printer;
        public List<IMyRow> rows { get; set; }
        public List<IError> errors { get; set; }
        public BasicExecutor(IBasicMapper basicMapper, IBasicOperator basicOperator, IBasicPrinter basicPrinter)
        {
            _mapper = basicMapper;
            _operator = basicOperator;
            _printer = basicPrinter;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            rows = _mapper.Map(sheet, headersOfSheet);

            errors = _operator.Check(rows);

            _printer.Print(errors, rows);
        }
    }
}
