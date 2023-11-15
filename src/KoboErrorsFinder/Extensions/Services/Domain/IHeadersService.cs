using NPOI.SS.UserModel;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public interface IHeadersService
    {
        public Dictionary<string, int> GetHeadersDictionary(IRow firstRowWithHeaders);
    }
}
