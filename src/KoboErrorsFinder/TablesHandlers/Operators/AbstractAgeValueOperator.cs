using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities.Rows.Abstractions;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public abstract class AbstractAgeValueOperator : AbstractOperator
    {
        public void CheckMonthsCount(List<IMyRow> rows, List<IError> errors)
        {
            List<IAgeRow> basicRows = rows.Cast<IAgeRow>().ToList();

            var monthsErrorsRows = basicRows
                .Where(r => r.AgeUnit == "Months")
                .ToList();

            if (monthsErrorsRows != null && monthsErrorsRows.Count > 0)
            {
                for (int i = 0; i < monthsErrorsRows.Count(); i++)
                {
                    if (monthsErrorsRows.Any(e => int.TryParse(e.AgeValue, out int age) && age >= 12))
                    {
                        IAgeError error = null;

                        if (errors != null && errors.Count != 0)
                        {
                            error = (IAgeError)errors.FirstOrDefault(e => e.UniqueEntityId == monthsErrorsRows.First().UniqueEntityId);
                        }

                        if (error == null)
                        {
                            error = (IAgeError)GetError();

                            error.UniqueEntityId = monthsErrorsRows.First().UniqueEntityId;
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
