using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public interface IHeadersService
    {
        public Dictionary<string, int> GetHeadersDictionary(IRow firstRowWithHeaders);
    }
}
