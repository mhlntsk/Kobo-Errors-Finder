using KoboErrorFinder.Models;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KoboErrorFinder.DomainServices
{
    public class CheckerService
    {
        //private const string pattern = @"^DE-[A-ZА-Я]{2,3}-[A-ZА-Я]{2,3}-\d{6}-\d{3}$";
        private readonly string pattern;
        private readonly string secondGroup;
        private readonly string thirdGroup;
        public CheckerService()
        {
            var secondValidOptions = new List<string> { 
                "LYM", "LSH", "SYD", "SVY", "NOM", "DON", "VAS", "YAR", "DRO", "KAR", 
                "PRY", "KHR", "MIK", "SEL", "RAI", "YAM", "TET", "CHE", "OLE", "NOS", 
                "KRA", "MAL", "KOY", "SIV", "SSH", "AND", "SER", "ANV", "KUN", "VIR", 
                "STU", "SPI", "OSK", "IZY", "CHI", "LIP", "KOM", "NOV", "KAL", "KLU", 
                "OZE", "ZAK", "BOG", "KUR", "SLD", "KOS", "LZK", "PRE" };
            secondGroup = string.Join("|", secondValidOptions);
            
            var thirdValidOptions = new List<string> { "OPD", "OT", "SRH", "ED", "MH"};
            thirdGroup = string.Join("|", thirdValidOptions);

            pattern = $@"^DE-{secondGroup}-{thirdGroup}-\d{{6}}-\d{{3}}$";
        }
        public void Check(List<Patient> patients)
        {
            CheckIds(patients);

            CheckSex(patients);

            CheckAge(patients);
        }

        private void CheckIds(List<Patient> patients)
        {
            Console.WriteLine("\n1. Перевірка полів \"MSF Patient Id\":");

            List<Patient> uniquePatientsIds = patients
               .OrderBy(p => p.PatientId)
               .GroupBy(p => p.PatientId)
               .Select(group => group.First())
               .ToList();

            var noCorrectIds = new List<string>();

            foreach (var patient in uniquePatientsIds)
            {
                if (!Regex.IsMatch(patient.PatientId, pattern))
                {
                    noCorrectIds.Add(patient.PatientId);
                }
            }

            if (noCorrectIds.Count != 0)
            {
                foreach (var item in noCorrectIds)
                {
                    Console.WriteLine($"\tID: {item} - некоректний формат ID");

                }
            }
            else
            {
                Console.WriteLine("\tЗначень \"MSF Patient Id\" некоректного формату не виявлено");
            }
        }
        private void CheckSex(List<Patient> patients)
        {
            Console.WriteLine("\n2. Перевірка полів \"Sex\" серед ідентичних Id:");

            var groupedPatients = patients
                .OrderBy(p => p.PatientId)
                .GroupBy(p => p.PatientId);

            var noCorrectSex = new List<string>();

            foreach (var group in groupedPatients)
            {
                if (!group.All(p => p.Sex == group.First().Sex))
                {
                    Console.WriteLine($"\tID: {group.Key} - не всі значення Sex співпадають:");
                }
            }

            if (noCorrectSex.Count != 0)
            {
                foreach (var item in noCorrectSex)
                {
                    Console.WriteLine($"\tID: {item} - значення \"Sex\" не співпадають");
                }
            }
            else
            {
                Console.WriteLine("\tНекоректних значень \"Sex\" не виявлено");
            }
        }
        private void CheckAge(List<Patient> patients)
        {
            Console.WriteLine("\n3. Перевірка полів \"Age\" серед ідентичних Id:");

            Console.WriteLine("\n\t3.1 Вік в місяцях:");
            CheckMonthsAge(patients);

            Console.WriteLine("\n\t3.2 Вік в роках:");
            CheckYearsAge(patients);
        }

        private void CheckMonthsAge(List<Patient> patients)
        {
            var groupedByIdMonthsAgePatients = patients
                .Where(patient => patient.AgeUnit == "Months")
                .OrderBy(p => p.PatientId)
                .GroupBy(p => p.PatientId)
                .ToList();

            var patientsWithErrors = new List<Patient>();

            foreach (var group in groupedByIdMonthsAgePatients)
            {
                Patient patientWithMinDate = group.OrderBy(p => p.DateOfConsultation).FirstOrDefault();

                if (patientWithMinDate != null)
                {
                    DateOnly minDate = patientWithMinDate.DateOfConsultation;

                    bool hasPatientWithGreaterDateAndLessAge = group.Any(p =>
                        p.DateOfConsultation > minDate && Convert.ToInt32(p.Age) < Convert.ToInt32(patientWithMinDate.Age));

                    if (hasPatientWithGreaterDateAndLessAge)
                    {
                        Console.WriteLine($"\tID: {group.Key} - не всі значення Age співпадають або знайдено пацієнта з більшою датою та меншим віком:");

                        foreach (var item in group)
                        {
                            Console.WriteLine($"\t\t - {item.Age}");
                            patientsWithErrors.Add(item);
                        }
                    }
                }
                else
                {
                    throw new Exception("Не вдалося знайти пацієнта з мінімальною датою");
                }
            }


            var monthsAgePatients = patients
                .Where(patient => patient.AgeUnit == "Months")
                .OrderBy(p => p.PatientId)
                .ToList();

            var patientsWithMoreThan11Months = new List<Patient>();

            foreach (var patient in monthsAgePatients)
            {
                if (Convert.ToInt32(patient.Age) >= 12)
                {
                    patientsWithMoreThan11Months.Add(patient);
                    patientsWithErrors.Add(patient);

                    Console.WriteLine($"ID: {patient.PatientId} - Вік в місяцях більше за 11");
                }
            }

            if (!patientsWithErrors.Any())
            {
                Console.WriteLine($"\t\tНекоректних даних в розділі \"вік в місяцях\" не знайдено.");
            }
        }

        private void CheckYearsAge(List<Patient> patients)
        {
            var groupedByIdYearsAgePatients = patients
                .Where(patient => patient.AgeUnit == "Years")
                .OrderBy(p => p.PatientId)
                .GroupBy(p => p.PatientId)
                .ToList();

            var patientsWithErrors = new List<Patient>();

            foreach (var group in groupedByIdYearsAgePatients)
            {
                Patient patientWithMinDate = group.OrderBy(p => p.DateOfConsultation).FirstOrDefault();

                if (patientWithMinDate != null)
                {
                    DateOnly minDate = patientWithMinDate.DateOfConsultation;

                    bool hasPatientWithGreaterDateAndLessAge = group.Any(p =>
                        p.DateOfConsultation > minDate && Convert.ToInt32(p.Age) < Convert.ToInt32(patientWithMinDate.Age));

                    if (hasPatientWithGreaterDateAndLessAge)
                    {
                        Console.WriteLine($"\tID: {group.Key} - не всі значення Age співпадають або знайдено пацієнта з більшою датою та меншим віком:");

                        foreach (var item in group)
                        {
                            Console.WriteLine($"\t\t - {item.Age}");
                            patientsWithErrors.Add(item);
                        }
                    }
                }
                else
                {
                    throw new Exception("Не вдалося знайти пацієнта з мінімальною датою");
                }
            }

            if (!patientsWithErrors.Any())
            {
                Console.WriteLine($"\t\tНекоректних даних в розділі \"вік в роках\" не знайдено.");
            }
        }
    }
}
