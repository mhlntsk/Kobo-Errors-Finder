using KoboErrorFinder.TablesExtensions.Executors;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public interface IConductorService
    {
        public void RegisterTable(string nameOfTable, IExecutor executor);
        public IExecutor GetExecutor(string sheetName);
    }
}
