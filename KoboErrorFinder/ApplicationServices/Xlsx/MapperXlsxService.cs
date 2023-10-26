using KoboErrorFinder.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.ApplicationServices.Xlsx
{
    public class MapperXlsxService
    {
        public List<Patient> ScanSheet(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            List<Patient> patients = new List<Patient>();

            for (int rowIdx = 1; rowIdx <= sheet.LastRowNum; rowIdx++) // Починаємо з 1, щоб уникнути заголовку стовпця
            {
                IRow row = sheet.GetRow(rowIdx);

                if (row != null)
                {
                    Patient patient = new Patient();

                    if (headersOfSheet.ContainsKey("MSF Patient ID"))
                    {
                        int columnIndex = headersOfSheet["MSF Patient ID"];
                        patient.PatientId = row.GetCell(columnIndex)?.ToString();
                    }

                    if (headersOfSheet.ContainsKey("_submission_time"))
                    {
                        int columnIndex = headersOfSheet["_submission_time"];
                        string dateAsString = row.GetCell(columnIndex)?.ToString();

                        if (DateOnly.TryParse(dateAsString, out DateOnly date))
                        {
                            patient.DateOfConsultation = date;
                        }
                        else
                        {
                            throw new Exception("Не вдається розпарсити дату");
                        }
                    }

                    if (headersOfSheet.ContainsKey("Age unit"))
                    {
                        int columnIndex = headersOfSheet["Age unit"];
                        patient.AgeUnit = row.GetCell(columnIndex)?.ToString();
                    }

                    if (headersOfSheet.ContainsKey("Age value"))
                    {
                        int columnIndex = headersOfSheet["Age value"];
                        patient.Age = row.GetCell(columnIndex)?.ToString();
                    }

                    if (headersOfSheet.ContainsKey("Sex"))
                    {
                        int columnIndex = headersOfSheet["Sex"];
                        patient.Sex = row.GetCell(columnIndex)?.ToString();
                    }

                    patients.Add(patient);
                }
            }

            return patients;
        }
    }
}
