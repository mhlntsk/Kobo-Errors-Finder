using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public interface IOperator<T> where T : class
    {
        protected List<IError> errors { get; set; }
        public abstract List<IError> Check(List<IMyRow> rows);
    }
}
