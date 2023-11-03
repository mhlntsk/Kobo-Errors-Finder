﻿using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Moduls.Printers
{
    public class AmbulancePrinter : AbstractPrinter
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
                    Console.WriteLine("\tВік в місяцях не може бути більши за 11!");
                }

                if (errorsById.Any(error => error.Error.OtherPatientLocationSpecifyIsEmpty == true))
                {
                    Console.WriteLine("\tУ пацієнта з локацією \"Other location\" має бути заповнене поле \"Other Patient location: specify\"");
                }

                if (errorsById.Any(error => error.Error.DateError == true))
                {
                    Console.WriteLine("\tДата не зазначена!");
                }


                foreach (var error in errorsById)
                {
                    Console.WriteLine($"\t\t{error.Row.AgeValue} | {error.Row.Date}");
                }

                counter++;
            }
        }
    }
}
