using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public class OpdPrinter : AbstractPrinter, IPrinter<OpdPrinter>
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            var castedErrors = errors.Cast<BasicError>().ToList();
            var castedRows = rows.Cast<BasicRow>().ToList();

            var LYM_Errors = castedErrors
                .Join(castedRows.Where(r => r.MSFPatientID.StartsWith("DE-LYM")),
                      error => error.UniqueEntityId,
                      row => row.UniqueEntityId,
                      (error, row) => error)
                .ToList();

            var SVY_Errors = castedErrors
                .Join(castedRows.Where(r => r.MSFPatientID.StartsWith("DE-SVY")),
                      error => error.UniqueEntityId,
                      row => row.UniqueEntityId,
                      (error, row) => error)
                .ToList();

            var YAR_Errors = castedErrors
                .Join(castedRows.Where(r => r.MSFPatientID.StartsWith("DE-YAR")),
                      error => error.UniqueEntityId,
                      row => row.UniqueEntityId,
                      (error, row) => error)
                .ToList();


            var Other_Errors = castedErrors
                .Except(LYM_Errors)
                .Except(SVY_Errors)
                .Except(YAR_Errors)
                .ToList();

            MapGroups(LYM_Errors, castedRows, "LYM");
            MapGroups(SVY_Errors, castedRows, "SVY");
            MapGroups(YAR_Errors, castedRows, "YAR");
            MapGroups(Other_Errors, castedRows, "Other");
        }

        private void MapGroups(List<BasicError> castedErrors, List<BasicRow> castedRows, string nameOfTheGroup)
        {
            Console.WriteLine($"\n\t- - - - - {nameOfTheGroup}- - - - - ");

            var joinedErrorsAndRows = from error in castedErrors
                                      join row in castedRows on error.UniqueEntityId equals row.UniqueEntityId
                                      select new { Error = error, Row = row };

            var groupedByIdErrors = joinedErrorsAndRows
                .OrderBy(item => item.Row.MSFPatientID)
                .GroupBy(item => item.Row.MSFPatientID)
                .ToList();

            int counter = 1;

            foreach (var groupOfErrors in groupedByIdErrors)
            {
                var errorsById = groupOfErrors.ToList();

                Console.WriteLine($"\n{counter}) {errorsById.First().Row.MSFPatientID}");

                if (errorsById.Any(error => error.Error.IdError == true))
                {
                    Console.WriteLine("\tError in field \"MSF Patient Id\"");
                }

                if (errorsById.Any(error => error.Error.SexError == true))
                {
                    Console.WriteLine("\tError in synchronization of field \"Sex\"");
                }

                if (errorsById.Any(error => error.Error.AgeError == true))
                {
                    Console.WriteLine("\tError in synchronization of field \"Age\"");
                }

                if (errorsById.Any(error => error.Error.AgeMoreThan11MonthError == true))
                {
                    Console.WriteLine("\tAge in months cannot be more than 11!");
                }

                if (errorsById.Any(error => error.Error.DateError == true))
                {
                    Console.WriteLine("\tThe date is not specified!");
                }

                if (errorsById.Any(e => e.Error.AgeError == true || e.Error.SexError == true))
                {
                    var MSFPatientID = groupOfErrors.Key;

                    List<BasicRow> rowsInErrorGroup = castedRows
                        .Where(e => e.MSFPatientID == MSFPatientID)
                        .ToList();

                    foreach (var row in rowsInErrorGroup)
                    {
                        Console.WriteLine($"\t\t {row.AgeValue} | {row.Sex} | {row.Date}");
                    }
                }
                else
                {
                    Console.WriteLine($"\t\t {errorsById.First().Row.AgeValue} | {errorsById.First().Row.Sex} | {errorsById.First().Row.Date}");
                }


                counter++;
            }

        }
    }
}
