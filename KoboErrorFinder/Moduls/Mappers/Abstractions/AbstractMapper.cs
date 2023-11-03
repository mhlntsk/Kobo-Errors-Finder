using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.Mappers
{
    public abstract class AbstractMapper
    {
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
            if (headersOfSheet.ContainsKey("Date of consultation"))
            {
                int columnIndex = headersOfSheet["Date of consultation"];
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
            else if (headersOfSheet.ContainsKey("Date of visit"))
            {
                int columnIndex = headersOfSheet["Date of visit"];
                string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();
                if (dateAsString != null)
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
            else if (headersOfSheet.ContainsKey("Date of arrival"))
            {
                int columnIndex = headersOfSheet["Date of arrival"];
                string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();

                if (dateAsString != null)
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
            else if (headersOfSheet.ContainsKey("Date of hospitalization"))
            {
                int columnIndex = headersOfSheet["Date of hospitalization"];
                string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();

                if (DateOnly.TryParse(dateAsString, out DateOnly date))
                {
                    myRow.Date = date;
                }
                else
                {
                    throw new Exception("Не вдається розпарсити дату");
                }
            }
            else if (headersOfSheet.ContainsKey("Date of group session"))
            {
                int columnIndex = headersOfSheet["Date of group session"];
                string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();

                if (dateAsString != null)
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
            else if (headersOfSheet.ContainsKey("Date of request"))
            {
                int columnIndex = headersOfSheet["Date of request"];
                string dateAsString = rowFromTable.GetCell(columnIndex)?.ToString();

                if (dateAsString != null)
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
