using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Application
{
    public class PathReaderService : IPathReaderService
    {
        public string GetPath()
        {
            string filePath;

            while (true)
            {
                Console.Write("Введіть шлях до файлу з розширенням \".xlsx\": ");
                filePath = Console.ReadLine();

                if (filePath.EndsWith(".xlsx"))
                {
                    return filePath;
                }

                Console.WriteLine("Невірний формат або файл не знайдено, спробуйте ще раз.\n");

            }
        }
    }
}
