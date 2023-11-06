using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities.Rows.Abstractions;
using KoboErrorFinder.Models;
using NPOI.SS.Formula.Functions;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public abstract class AbstractAgeValueOperator : AbstractOperator
    {
        public void CheckMonthsCount(List<IMyRow> rows, List<IError> errors)
        {
            List<IAgeRow> basicRows = rows.Cast<IAgeRow>().ToList();

            var monthsPatients = basicRows
                .Where(r => r.AgeUnit == "Months")
                .ToList();

            for (int i = 0; i < monthsPatients.Count(); i++)
            {
                if (Convert.ToInt32(monthsPatients[i].AgeValue) >= 12)
                {
                    IAgeError error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = (IAgeError)errors.FirstOrDefault(e => e.UniqueEntityId == monthsPatients[i].UniqueEntityId);
                    }

                    if (error == null)
                    {
                        error = (IAgeError)GetError();

                        error.UniqueEntityId = monthsPatients[i].UniqueEntityId;
                        error.AgeMoreThan11MonthError = true;
                    }
                    else
                    {
                        error.AgeMoreThan11MonthError = true;
                    }

                    errors.Add(error);
                }
            }
        }

    }
}
