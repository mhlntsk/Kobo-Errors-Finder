﻿using KoboErrorFinder.Entities;
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
    public class BasicPrinter : AbstractPrinter, IBasicPrinter
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            var castedErrors = errors.Cast<BasicError>().ToList();
            var castedRows = rows.Cast<BasicRow>().ToList();

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

                var errorsToPrint = errorsById.Where(e => e.Error.AgeError || e.Error.SexError);

                foreach (var error in errorsToPrint)
                {
                    Console.WriteLine($"\t\t {error.Row.AgeValue} | {error.Row.Sex} | {error.Row.Date}");
                }

                counter++;
            }
        }
    }
}
