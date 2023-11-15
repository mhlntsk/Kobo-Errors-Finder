using NPOI.SS.UserModel;

namespace KoboErrorFinder.Services
{
    public interface IHeadersService
    {
        public Dictionary<string, int> GetHeadersDictionary(IRow firstRowWithHeaders);
    }
}
