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
        protected readonly IMapper<MHGroupMapper> _mapper;
        protected readonly IOperator<MHGroupOperator> _operator;
        protected readonly IPrinter<MHGroupPrinter> _printer;
        public List<IMyRow> mapedRowsFromTable { get; set; }
        public List<IError> errorsInRows { get; set; }
        public MHGroupExecutor(IMapper<MHGroupMapper> mHGroupMapper, IOperator<MHGroupOperator> mHGroupOperator, IPrinter<MHGroupPrinter> mHGroupPrinter)
        {
            _mapper = mHGroupMapper;
            _operator = mHGroupOperator;
            _printer = mHGroupPrinter;
        }
        public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

            errorsInRows = _operator.Check(mapedRowsFromTable);

            _printer.Print(errorsInRows, mapedRowsFromTable);
        }
    }
}
