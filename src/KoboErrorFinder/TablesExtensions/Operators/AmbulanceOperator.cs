using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder;
using KoboErrorFinder.TablesExtensions.Printers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public class AmbulanceOperator : AbstractAgeValueOperator, IAmbulanceOperator
    {
        List<IError> errors = new List<IError>();
        public override List<IError> Check(List<IMyRow> rows)
        {
            CheckRegionOfPatient(rows, errors);
            CheckMonthsCount(rows, errors);
            CheckDate(rows, errors);

            return errors;
        }

        public void CheckRegionOfPatient(List<IMyRow> rows, List<IError> errors)
        {
            List<AmbulanceRow> ambulanceRows = rows.Cast<AmbulanceRow>().ToList();

            var rowsWithoutSpecifyLocation = ambulanceRows
                .Where(p =>
                    p.RegionOfPatient == "Other location" &&
                    p.OtherPatientLocationSpecify == null);

            if (rowsWithoutSpecifyLocation != null)
            {
                foreach (var row in rowsWithoutSpecifyLocation)
                {
                    AmbulanceError error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = (AmbulanceError)errors.FirstOrDefault(e => e.UniqueEntityId == row.UniqueEntityId);
                    }

                    if (error == null)
                    {
                        error = new AmbulanceError()
                        {
                            UniqueEntityId = row.UniqueEntityId,
                            OtherPatientLocationSpecifyIsEmpty = true
                        };
                    }
                    else
                    {
                        error.OtherPatientLocationSpecifyIsEmpty = true;
                    }

                    errors.Add(error);
                }
            }

        }
        public override IAgeError GetError()
        {
            return new AmbulanceError();
        }

    }
}
