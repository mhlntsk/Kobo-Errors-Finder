using KoboErrorFinder.ApplicationServices.Xlsx;

namespace KoboErrorFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var PathService = new PathReaderXlsx();

            while (true)
            {
                var checkerXlsx = new CheckerXlsx();
                string filePath = PathService.GetPath();

                if (File.Exists(filePath))
                {
                    checkerXlsx.CheckFile(filePath);
                }
                else
                {
                    Console.WriteLine("Файл не знайдено.");
                }
            }
        }
    }
}