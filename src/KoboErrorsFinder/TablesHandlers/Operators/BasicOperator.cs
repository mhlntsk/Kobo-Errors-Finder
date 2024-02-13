using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder;
using KoboErrorFinder.TablesExtensions.Printers;
using System.Text.RegularExpressions;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public class BasicOperator : AbstractAgeValueOperator, IOperator<BasicOperator>
    {
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
                "OZE", "ZAK", "BOG", "KUR", "SLD", "KOS", "LZK", "PRE", "SHA", "OLE", 
                "DIB", "DMY", "LZC", "MKP", "NVH", "PRK", "SEL" };
            secondGroup = string.Join("|", secondValidOptions);

            var thirdValidOptions = new List<string> { "OPD", "OT", "SRH", "ED", "MH", "ICU" };
            thirdGroup = string.Join("|", thirdValidOptions);

            pattern = $@"^DE-({secondGroup})-({thirdGroup})-\d{{6}}-\d{{3}}$";

        }


        public List<IError> errors { get; set; } = new List<IError>();
        public List<IError> Check(List<IMyRow> rows)
        {
            CheckIds(rows, errors);
            CheckAge(rows, errors);
            CheckSex(rows, errors);

            CheckDate(rows, errors);
            CheckMonthsCount(rows, errors);

            return errors;
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
                        for (int j = 0; j < rowsInGroup.Count; j++)
                        {
                            BasicError error = null;

                            if (errors != null && errors.Count != 0)
                            {
                                error = (BasicError)errors.FirstOrDefault(e => e.UniqueEntityId == rowsInGroup[j].UniqueEntityId);
                            }

                            if (error == null)
                            {
                                error = new BasicError()
                                {
                                    UniqueEntityId = rowsInGroup[j].UniqueEntityId,
                                    AgeError = true
                                };
                            }
                            else
                            {
                                error.AgeError = true;
                            }

                            errors.Add(error);
                        }

                        break;
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