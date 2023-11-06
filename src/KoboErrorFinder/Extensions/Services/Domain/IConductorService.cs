using KoboErrorFinder.TablesExtensions.Executors;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public interface IConductorService
    {
        public void RegisterTable(string nameOfTable, IExecutor executor);
        public IExecutor GetExecutor(string sheetName);
    }
}
