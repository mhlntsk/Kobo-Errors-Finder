using KoboErrorFinder.Models;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.DomainServices
{
    public class PrinterService
    {
        public void PrintResult(List<Error> errors, List<Patient> patients)
        {
            var groupedByIdErrors = errors
                .OrderBy(e => e.Patient.PatientId)
                .GroupBy(e => e.Patient.PatientId)
                .ToList();

            int counter = 1;

            foreach (var groupOfErrors in groupedByIdErrors)
            {
                var errorsById = groupOfErrors.ToList();

                var patientsById = patients
                    .Where(p => p.PatientId == groupOfErrors.First().Patient.PatientId)
                    .ToList();

                Console.WriteLine($"\n{counter++}) {errorsById.First().Patient.PatientId}");

                if (errorsById.Any(error => error.IdError == true))
                {
                    Console.WriteLine("\tIdError");
                }

                if (errorsById.Any(error => error.SexError == true))
                {
                    Console.WriteLine("\tSexError");
                }

                if (errorsById.Any(error => error.AgeError == true))
                {
                    Console.WriteLine("\tAgeError");
                }

                if (errorsById.Any(error => error.AgeMoreThan11MonthError == true))
                {
                    Console.WriteLine("\tAgeMoreThan11MonthError");
                }

                foreach (var patientById in patientsById)
                {
                    Console.WriteLine($"\t\t{patientById.DateOfConsultation} | {patientById.Sex} | {patientById.Age}");
                }
            }
        }
    }
}
