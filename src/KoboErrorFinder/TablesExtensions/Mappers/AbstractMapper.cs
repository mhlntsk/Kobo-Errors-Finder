using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;
using System.Diagnostics;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public abstract class AbstractMapper
    {
        private readonly string[] _dateCells = new string[]
        {
            "Date of consultation",
            "Date of visit",
            "Date of arrival",
            "Date of hospitalization",
            "Date of group session",
            "Date of request"
        };
        public List<IMyRow> Map(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            Console.WriteLine($"\n\n------------- Початок сканування файлу {sheet.SheetName} -------------");

            List<IMyRow> rows = new List<IMyRow>();

            for (int rowIdx = 1; rowIdx <= sheet.LastRowNum - 1; rowIdx++) // Починаємо з 1, щоб уникнути заголовку стовпця
            {
                IRow rowFromTable = sheet.GetRow(rowIdx);

                if (rowFromTable != null)
                {
                    var myRow = MakeSpecificMapping(headersOfSheet, rowFromTable);

                    rows.Add(myRow);
                }
            }

            return rows;
        }
        public abstract IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable);
        public void MapDate(Dictionary<string, int> headersOfSheet, IMyRow myRow, IRow rowFromTable)
        {
            for (int i = 0; i < _dateCells.Length; i++)
            {
                if (headersOfSheet.ContainsKey(_dateCells[i]))
                {
                    int columnIndex = headersOfSheet[_dateCells[i]];

                    string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();

                    if (!string.IsNullOrEmpty(dateAsString))
                    {
                        if (DateOnly.TryParse(dateAsString, out DateOnly date))
                        {
                            myRow.Date = date;
                        }
                        else
                        {
                            throw new Exception("Не вдається розпарсити дату");
                        }
                    }
                }
            }
        }
    }
}
