using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Extensions.Services.Domain
{
    public interface ISheetsService
    {
        public void CheckAllSheets(string filePath);
    }
}
