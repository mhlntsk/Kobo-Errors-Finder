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

            var groupedByIdRows = basicRows
                .Where(r => r.AgeUnit == "Months")
                .OrderBy(p => p.MSFPatientID)
                .GroupBy(p => p.MSFPatientID)
                .ToList();

            foreach (var group in groupedByIdRows)
            {
                for (int i = 0; i < group.Count(); i++)
                {
                    if (group.Any(e => int.TryParse(e.AgeValue, out int age) && age >= 12))
                    {
                        IAgeError error = null;

                        if (errors != null && errors.Count != 0)
                        {
                            error = (IAgeError)errors.FirstOrDefault(e => e.UniqueEntityId == group.First().UniqueEntityId);
                        }

                        if (error == null)
                        {
                            error = (IAgeError)GetError();

                            error.UniqueEntityId = group.First().UniqueEntityId;
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
}
