using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.ApplicationServices.Xlsx
{
    public class PathReaderXlsx
    {
        public string GetPath()
        {
            string filePath = null;

            while (true)
            {
                do
                {
                    Console.Write("Введіть шлях до файлу з розширенням \".xlsx\": ");
                    filePath = Console.ReadLine();


                } while (filePath == "");

                if (filePath.EndsWith(".xlsx"))
                {
                    return filePath;
                }

                Console.WriteLine("Невірний формат або файл не знайдено, спробуйте ще раз.\n");
            }

        }
    }
}
