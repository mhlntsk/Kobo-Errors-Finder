using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Services
{
    public class PathReaderService : IPathReaderService
    {
        public string GetPath()
        {
            string filePath;

            while (true)
            {
                Console.Write("\nEnter the path to the file with the \".xlsx\" format: ");
                filePath = Console.ReadLine();

                if (filePath.EndsWith(".xlsx"))
                {
                    return filePath;
                }

                Console.WriteLine("Invalid format or file not found, please try again.\n");

            }
        }
    }
}
