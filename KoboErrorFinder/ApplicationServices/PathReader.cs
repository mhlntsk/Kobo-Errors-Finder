using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Services
{
    public class PathReader
    {
        public string GetPath()
        {
            string filePath = null;

            while (string.IsNullOrEmpty(filePath))
            {
                Console.Write("Введіть шлях до файлу з розширенням \".xlsx\": ");
                filePath = Console.ReadLine();

                if (string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine("Нажаль, файл не знайдено, спробуйте ще раз.\n");
                }
            }

            return filePath;
        }
    }
}
