using KoboErrorFinder.TablesExtensions.Executors;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    /// <summary>
    /// Сonduct the further execution of the program depending on the table type (depending on the MSF`s project). Calls sheet by sheet.
    /// </summary>
    public class ConductorService : IConductorService
    {
        public ConductorService()
        {
                
        }
        private readonly Dictionary<string, IExecutor> executors = new Dictionary<string, IExecutor>();
        public void RegisterTable(string nameOfTable, IExecutor executor)
        {
            if (!executors.ContainsKey(nameOfTable))
            {
                executors.Add(nameOfTable, executor);
            }
            else
            {
                throw new Exception("ConductorService already has this executor");
            }
        }

        public IExecutor GetExecutor(string sheetName)
        {
            return executors[sheetName];
        }
    }
}
