using KoboErrorFinder.Models;
using NPOI.POIFS.Properties;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KoboErrorFinder.DomainServices
{
    public class CheckerService
    {
        private readonly string pattern;
        private readonly string secondGroup;
        private readonly string thirdGroup;
        public List<Error> errors { get; private set; } = new List<Error>();
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

            var printer = new PrinterService();
            printer.PrintResult(errors, patients);
        }

        private void CheckIds(List<Patient> patientsInGroup)
        {
            for (int i = 0; i < patientsInGroup.Count(); i++)
            {
                if (!Regex.IsMatch(patientsInGroup[i].PatientId, pattern))
                {
                    Error error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);
                    }

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
                    Error error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);
                    }

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
                    Error error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);
                    }

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
                    Error error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = errors.FirstOrDefault(e => e.Patient.UniqueEntityId == patientsInGroup[0].UniqueEntityId);
                    }

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
    }
}
