using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public abstract class AbstractPrinter
    {
        private readonly string[] WhatICanDo;
        private readonly int randomizer;
        public AbstractPrinter()
        {
            WhatICanDo = new string[]
            {
                "you can go drink coffee!",
                "and now live with the thought that it's just a bug! XD",
                "(maybe 😂)",
                "occupy your time with something, and drop 2 hryvnias on drones, greedy boy: https://send.monobank.ua/jar/dzBdJ3737",
                "time to donate to the Ukrainian Armed Forces! https://prytulafoundation.org/donation",
                "and now don't be a coward and donate on MSF - https://www.msf.org/donate",
                "so look at Hetman's cute face and drop your money on United24 - https://u24.gov.ua/"
            };

            Random random = new Random();
            randomizer = random.Next(0, WhatICanDo.Length);
        }
        public void Print(List<IError> errors, List<IMyRow> ambulanseRows)
        {
            if (errors == null || errors.Count == 0)
            {
                Console.WriteLine($"No errors were found in this table, {WhatICanDo[randomizer]}");
                return;
            }

            MakeSpecificPrinting(errors, ambulanseRows);
        }
        public abstract void MakeSpecificPrinting(List<IError> errors, List<IMyRow> ambulanseRows);

    }
}
