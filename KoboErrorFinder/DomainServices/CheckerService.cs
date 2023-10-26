using KoboErrorFinder.Models;
using NPOI.POIFS.Properties;
using System.Text.RegularExpressions;

namespace KoboErrorFinder.DomainServices
{
    public class CheckerService
    {
        private readonly string pattern;
        private readonly string secondGroup;
        private readonly string thirdGroup;
        public List<Error> errors { get; private set; }
        public CheckerService()
        {
            var secondValidOptions = new List<string> {
                "LYM", "LSH", "SYD", "SVY", "NOM", "DON", "VAS", "YAR", "DRO", "KAR",
                "PRY", "KHR", "MIK", "SEL", "RAI", "YAM", "TET", "CHE", "OLE", "NOS",
                "KRA", "MAL", "KOY", "SIV", "SSH", "AND", "SER", "ANV", "KUN", "VIR",
                "STU", "SPI", "OSK", "IZY", "CHI", "LIP", "KOM", "NOV", "KAL", "KLU",
                "OZE", "ZAK", "BOG", "KUR", "SLD", "KOS", "LZK", "PRE" };
            secondGroup = string.Join("|", secondValidOptions);

            var thirdValidOptions = new List<string> { "OPD", "OT", "SRH", "ED", "MH" };
            thirdGroup = string.Join("|", thirdValidOptions);

            pattern = $@"^DE-{secondGroup}-{thirdGroup}-\d{{6}}-\d{{3}}$";
        }

        public void Check(List<Patient> patients)
        {
            var groupedByIdPatients = patients
                .OrderBy(p => p.PatientId)
                .GroupBy(p => p.PatientId)
                .ToList();

            foreach (var group in groupedByIdPatients)
            {
                List<Patient> patientsInGroup = group.ToList();

                CheckIds(patientsInGroup);
                CheckAge(patientsInGroup);
                CheckSex(patientsInGroup);
                CheckMonthsCount(patientsInGroup);
            }
        }

        private void CheckIds(List<Patient> patientsInGroup)
        {
            for (int i = 0; i < patientsInGroup.Count(); i++)
            {
                if (!Regex.IsMatch(patientsInGroup[i].PatientId, pattern))
                {
                    var error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);

                    if (error == null)
                    {
                        error = new Error()
                        {
                            Patient = patientsInGroup[i],
                            IdError = true
                        };
                    }
                    else
                    {
                        error.IdError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        private void CheckAge(List<Patient> patientsInGroup)
        {
            for (int i = 0; i < patientsInGroup.Count() - 1; i++)
            {
                if (Convert.ToInt32(patientsInGroup[i + 1].Age) < Convert.ToInt32(patientsInGroup[i].Age) ||
                   (Convert.ToInt32(patientsInGroup[i + 1].Age) - Convert.ToInt32(patientsInGroup[i].Age) > 1))
                {
                    var error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);

                    if (error == null)
                    {
                        error = new Error()
                        {
                            Patient = patientsInGroup[i],
                            AgeError = true
                        };
                    }
                    else
                    {
                        error.AgeError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        private void CheckSex(List<Patient> patientsInGroup)
        {
            for (int i = 0; i < patientsInGroup.Count(); i++)
            {
                if (patientsInGroup.Any(p => p.Sex != patientsInGroup.First().Sex))
                {
                    var error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);

                    if (error == null)
                    {
                        error = new Error()
                        {
                            Patient = patientsInGroup[i],
                            SexError = true
                        };
                    }
                    else
                    {
                        error.SexError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        private void CheckMonthsCount(List<Patient> patientsInGroup)
        {
            var monthsPatientsInGroup = patientsInGroup
                .Where(patient => patient.AgeUnit == "Months")
                .OrderBy(p => p.PatientId)
                .ToList();

            for (int i = 0; i < monthsPatientsInGroup.Count(); i++)
            {
                if (Convert.ToInt32(monthsPatientsInGroup[0].Age) >= 12)
                {
                    var error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == monthsPatientsInGroup[0].UniqueEntityId);

                    if (error == null)
                    {
                        error = new Error()
                        {
                            Patient = monthsPatientsInGroup[i],
                            AgeMoreThan11MonthError = true
                        };
                    }
                    else
                    {
                        error.AgeMoreThan11MonthError = true;
                    }

                    errors.Add(error);
                }
            }
        }





        public void PrintIdError(List<Patient> patients)
        {
            if (patients.Count != 0)
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"-{patient.PatientId} - некоректний формат ID");
                }
            }
        }
        public void PrintAgeError(List<Patient> patients)
        {
            Console.WriteLine($"\n-{patients.First().PatientId} - не всі значення \"Age\" заповнені коректно:");

            foreach (var item in patients)
            {
                Console.WriteLine($"\t\t{item.Age} | {item.Sex} | {item.DateOfConsultation}");
            }
        }
        public void PrintSexError(List<Patient> patients)
        {
            Console.WriteLine($"\n-{patients.First().PatientId} - значення \"Sex\" не співпадають");

            foreach (var item in patients)
            {
                Console.WriteLine($"\t\t{item.Age} | {item.Sex} | {item.DateOfConsultation}");
            }
        }
        public void PrintMonthsCountError(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                Console.WriteLine($"-{patient.PatientId} - Вік в місяцях більше за 11");
                Console.WriteLine($"\t\t{patient.Age} | {patient.Sex} | {patient.DateOfConsultation}");
            }
        }
        
    }
}
