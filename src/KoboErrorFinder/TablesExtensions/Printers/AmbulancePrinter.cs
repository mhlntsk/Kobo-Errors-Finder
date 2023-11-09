using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public class AmbulancePrinter : AbstractPrinter, IAmbulancePrinter
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            var castedErrors = errors.Cast<AmbulanceError>().ToList();
            var castedRows = rows.Cast<AmbulanceRow>().ToList();

            var joinedErrorsAndRows = from error in castedErrors
                                      join row in castedRows on error.UniqueEntityId equals row.UniqueEntityId
                                      select new { Error = error, Row = row };

            var groupedByIdErrorsAndRows = joinedErrorsAndRows
                .OrderBy(item => item.Row.Auto_form_id)
                .GroupBy(item => item.Row.Auto_form_id)
                .ToList();

            int counter = 1;

            foreach (var groupOfErrorsAndRows in groupedByIdErrorsAndRows)
            {
                var errorsById = groupOfErrorsAndRows.ToList();


                Console.WriteLine($"\n{counter}) {errorsById.First().Row.Auto_form_id}");

                if (errorsById.Any(error => error.Error.AgeMoreThan11MonthError == true))
                {
                    Console.WriteLine("\tAge in months cannot be more than 11!");
                }

                if (errorsById.Any(error => error.Error.OtherPatientLocationSpecifyIsEmpty == true))
                {
                    Console.WriteLine("\tThe patient with the location \"Other location\" must have the field \"Other Patient location: specify\"");
                }

                if (errorsById.Any(error => error.Error.DateError == true))
                {
                    Console.WriteLine("\tThe date is not specified!");
                }

                counter++;
            }
        }
    }
}
