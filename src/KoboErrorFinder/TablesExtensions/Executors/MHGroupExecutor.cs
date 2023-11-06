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
using NPOI.SS.Formula.Functions;
using KoboErrorFinder.Entities.Errors;

namespace KoboErrorFinder.TablesExtensions.Executors
{
    public class MHGroupExecutor : IExecutor
    {
        protected readonly IMHGroupMapper _mapper;
        protected readonly IMHGroupOperator _operator;
        protected readonly IMHGroupPrinter _printer;
        public List<IMyRow> rows { get; set; }
        public List<IError> errors { get; set; }
        public MHGroupExecutor(IMHGroupMapper mHGroupMapper, IMHGroupOperator mHGroupOperator, IMHGroupPrinter mHGroupPrinter)
        {
            _mapper = mHGroupMapper;
            _operator = mHGroupOperator;
            _printer = mHGroupPrinter;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            rows = _mapper.Map(sheet, headersOfSheet);

            errors = _operator.Check(rows);

            _printer.Print(errors, rows);
        }
    }
}
