using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.ApplicationServices.Xlsx
{
    public class SheetChoosingServiceXlsx
    {
        public ISheet GetSheet(int sheetCount, XSSFWorkbook workbook)
        {
            ISheet sheet;

        start:  // Мітка

            bool correctNumber = true;
            string input = null;
            do
            {
                Console.Write("Введіть номер листа: ");
                input = Console.ReadLine();
                int number;

                if (int.TryParse(input, out number))
                {
                    correctNumber = false;
                }
                
            } while (correctNumber);

            int sheetNumber = Convert.ToInt32(input) - 1; // Перший лист має індекс 0

            if (sheetNumber < 0)
            {
                Console.WriteLine("Номер листа має починатися з \"1\"!\n");
                goto start;  // Перехід на мітку
            }

            if (sheetNumber > sheetCount - 1)
            {
                Console.WriteLine($"Кількість листів у поточному файлі - {sheetCount}\n");
                goto start;  // Перехід на мітку
            }

            sheet = workbook.GetSheetAt(sheetNumber); // Виберіть аркуш, який вас цікавить

            string sheetName = sheet.SheetName;

            if (sheetName.StartsWith("Ambulance"))
            {
                Console.WriteLine($"Таблиця \"Ambulance\" не підтримується, спробуйте ще раз\n");
                goto start;  // Перехід на мітку
            }

            if (sheetName.StartsWith("MH_Group Session"))
            {
                Console.WriteLine($"Таблиця \"MH_Group Session\" не підтримується, спробуйте ще раз\n");
                goto start;  // Перехід на мітку
            }

            return sheet;
        }
    }
}
