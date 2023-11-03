using KoboErrorFinder.Services;

namespace KoboErrorFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var pathReaderService = new PathReaderService();

            while (true)
            {
                var sheetsService = new SheetsService();
                string filePath = pathReaderService.GetPath();

                if (File.Exists(filePath))
                {
                    sheetsService.CheckFile(filePath);
                }
                else
                {
                    Console.WriteLine("Файл не знайдено.");
                }
            }
        }
    }
}