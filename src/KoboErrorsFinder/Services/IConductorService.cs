using KoboErrorFinder.TablesExtensions.Executors;

namespace KoboErrorFinder.Services
{
    public interface IConductorService
    {
        public void RegisterTable(string nameOfTable, IExecutor executor);
        public IExecutor GetExecutor(string sheetName);
    }
}
