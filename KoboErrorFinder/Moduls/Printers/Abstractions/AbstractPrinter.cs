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

namespace KoboErrorFinder.Services
{
    public abstract class AbstractPrinter
    {
        private readonly string[] WhatICanDo;
        private readonly int randomizer;
        public AbstractPrinter()
        {
            WhatICanDo = new string[]
            {
                "можна йти пити каву!",
                "а тепер живи з думкою, що це просто баг!))",
                "тьотя Галя все правильно заповнила.",
                "Відпочивай далі!",
                "не переживай сонце, у тебе ще є таблиця OPD/NCD!",
                "займи свій час чимось, і скинь 2 гривні Стерненку на дрони, жлобяра: https://send.monobank.ua/jar/dzBdJ3737",
                "час донатити на ЗСУ!!! https://prytulafoundation.org/donation",
                "а тепер не будь букою і донать на \"Повернись живим\" - https://savelife.in.ua/donate/#donate-army-card-monthly",
                "тож подивись на милу мордашку Гетьмана і скинь грошики на United24 - https://u24.gov.ua/"
            };

            Random random = new Random();
            randomizer = random.Next(0, 9);
        }
        public void Print(List<IError> errors, List<IMyRow> ambulanseRows)
        {
            if (errors == null || errors.Count == 0)
            {
                Console.WriteLine($"Помилок в цій таблиці не виявлено, {WhatICanDo[randomizer]}");
                return;
            }

            MakeSpecificPrinting(errors, ambulanseRows);
        }
        public abstract void MakeSpecificPrinting(List<IError> errors, List<IMyRow> ambulanseRows);

    }
}
