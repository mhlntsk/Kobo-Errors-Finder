using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public abstract class AbstractOperator
    {
        public abstract IError GetError();

        public void CheckDate(List<IMyRow> rows, List<IError> errors)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i].Date == default || rows[i].Date > DateOnly.FromDateTime(DateTime.Now))
                {
                    IError error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = errors.FirstOrDefault(e => e.UniqueEntityId == rows[i].UniqueEntityId);
                    }

                    if (error == null)
                    {
                        error = GetError();

                        error.UniqueEntityId = rows[i].UniqueEntityId;
                        error.DateError = true;
                    }
                    else
                    {
                        error.DateError = true;
                    }

                    errors.Add(error);
                }
            }
        }

    }
}
