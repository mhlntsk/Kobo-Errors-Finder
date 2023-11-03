using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.Moduls.Operators.Abstractions;
using KoboErrorFinder.Moduls.Printers;
using NPOI.SS.Formula.Functions;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoboErrorFinder.Moduls.Checkers
{
    /// <summary>
    /// Scans basic tables such as "OPD-NCD", "SRH"
    /// </summary>
    public class BasicOperator : AgeValueAbstractOperator
    {
        //private const string pattern = @"^DE-[A-ZА-Я]{2,3}-[A-ZА-Я]{2,3}-\d{6}-\d{3}$";
        private readonly string pattern;
        private readonly string secondGroup;
        private readonly string thirdGroup;
        public BasicOperator()
        {
            var secondValidOptions = new List<string> {
                "LYM", "LSH", "SYD", "SVY", "NOM", "DON", "VAS", "YAR", "DRO", "KAR",
                "PRY", "KHR", "MIK", "SEL", "RAI", "YAM", "TET", "CHE", "OLE", "NOS",
                "KRA", "MAL", "KOY", "SIV", "SSH", "AND", "SER", "ANV", "KUN", "VIR",
                "STU", "SPI", "OSK", "IZY", "CHI", "LIP", "KOM", "NOV", "KAL", "KLU",
                "OZE", "ZAK", "BOG", "KUR", "SLD", "KOS", "LZK", "PRE" };
            secondGroup = string.Join("|", secondValidOptions);

            var thirdValidOptions = new List<string> { "OPD", "OT", "SRH", "ED", "MH", "ICU" };
            thirdGroup = string.Join("|", thirdValidOptions);

            pattern = $@"^DE-({secondGroup})-({thirdGroup})-\d{{6}}-\d{{3}}$";

        }


        List<IError> errors = new List<IError>();
        public override void Check(List<IMyRow> rows)
        {
            CheckIds(rows, errors);
            CheckAge(rows, errors);
            CheckSex(rows, errors);

            CheckDate(rows, errors);
            CheckMonthsCount(rows, errors);

            var printer = new BasicPrinter();
            printer.Print(errors, rows);
        }

        public void CheckIds(List<IMyRow> rows, List<IError> errors)
        {
            List<BasicRow> basicRows = rows.Cast<BasicRow>().ToList();

            var groupedByIdRows = basicRows
                .OrderBy(p => p.MSFPatientID)
                .GroupBy(p => p.MSFPatientID)
                .ToList();

            foreach (var group in groupedByIdRows)
            {
                List<BasicRow> rowsInGroup = group.ToList();

                for (int i = 0; i < rowsInGroup.Count(); i++)
                {
                    if (!Regex.IsMatch(rowsInGroup[i].MSFPatientID, pattern))
                    {
                        BasicError error = null;

                        if (errors != null && errors.Count != 0)
                        {
                            error = (BasicError)errors.FirstOrDefault(e => e.UniqueEntityId == rowsInGroup[i].UniqueEntityId);
                        }

                        if (error == null)
                        {
                            error = new BasicError()
                            {
                                UniqueEntityId = rowsInGroup[i].UniqueEntityId,
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
        }
        public void CheckAge(List<IMyRow> rows, List<IError> errors)
        {
            List<BasicRow> basicRows = rows.Cast<BasicRow>().ToList();

            var groupedByIdRows = basicRows
                .OrderBy(p => p.MSFPatientID)
                .GroupBy(p => p.MSFPatientID)
                .ToList();

            foreach (var group in groupedByIdRows)
            {
                List<BasicRow> rowsInGroup = group.ToList();

                for (int i = 0; i < rowsInGroup.Count() - 1; i++)
                {
                    if (Convert.ToInt32(rowsInGroup[i + 1].AgeValue) < Convert.ToInt32(rowsInGroup[i].AgeValue) ||
                        Convert.ToInt32(rowsInGroup[i + 1].AgeValue) - Convert.ToInt32(rowsInGroup[i].AgeValue) > 1)
                    {
                        BasicError error = null;

                        if (errors != null && errors.Count != 0)
                        {
                            error = (BasicError)errors.FirstOrDefault(e => e.UniqueEntityId == rowsInGroup[i].UniqueEntityId);
                        }

                        if (error == null)
                        {
                            error = new BasicError()
                            {
                                UniqueEntityId = rowsInGroup[i].UniqueEntityId,
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
        }
        public void CheckSex(List<IMyRow> rows, List<IError> errors)
        {
            List<BasicRow> basicRows = rows.Cast<BasicRow>().ToList();

            var groupedByIdRows = basicRows
                .OrderBy(p => p.MSFPatientID)
                .GroupBy(p => p.MSFPatientID)
                .ToList();

            foreach (var group in groupedByIdRows)
            {
                List<BasicRow> rowsInGroup = group.ToList();

                for (int i = 0; i < rowsInGroup.Count(); i++)
                {
                    if (rowsInGroup.Any(p => p.Sex != rowsInGroup.First().Sex))
                    {
                        BasicError error = null;

                        if (errors != null && errors.Count != 0)
                        {
                            error = (BasicError)errors.FirstOrDefault(e => e.UniqueEntityId == rowsInGroup[i].UniqueEntityId);
                        }

                        if (error == null)
                        {
                            error = new BasicError()
                            {
                                UniqueEntityId = rowsInGroup[i].UniqueEntityId,
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
        }
        public override IAgeError GetError()
        {
            return new BasicError();
        }
    }
}
