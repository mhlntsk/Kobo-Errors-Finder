using KoboErrorFinder.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util.Collections;
using System.Text.RegularExpressions;

namespace KoboErrorFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var PathService = new PathReader();

            while (true)
            {
                string filePath = PathService.GetPath();

                if (File.Exists(filePath))
                {
                    CheckerXlsx.CheckFile(filePath);
                }
                else
                {
                    Console.WriteLine("Файл не знайдено.");
                }
            }
        }
    }
}