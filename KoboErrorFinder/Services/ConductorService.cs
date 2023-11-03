using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Extensions.Checkers;
using KoboErrorFinder.Mappers;
using KoboErrorFinder.Models;
using KoboErrorFinder.Moduls.Checkers;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.Extensions.Services
{
    public class ConductorService
    {
        public void SetChekingRegardToTypeOfTheSheet(ISheet sheet, Dictionary<string, int> headersOfSheet)
        {
            List<IMyRow> rows;

            if (sheet.SheetName.StartsWith(TypesOfTables.Ambulance.ToString()))
            {
                var ambulanceDataMapper = new AmbulanceMapper();
                rows = ambulanceDataMapper.Map(sheet, headersOfSheet);

                var ambulanceChecker = new AmbulanceOperator();
                ambulanceChecker.Check(rows);
            }
            else if (sheet.SheetName.StartsWith(TypesOfTables.MH_Group.ToString()))
            {
                var mHGroupDataMapper = new MHGroupMapper();
                rows = mHGroupDataMapper.Map(sheet, headersOfSheet);

                var mhGroupChecker = new MHGroupOperator();
                mhGroupChecker.Check(rows);
            }
            else
            {
                var dataMapperService = new BasicMapper();
                rows = dataMapperService.Map(sheet, headersOfSheet);

                var mainCheckerService = new BasicOperator();
                mainCheckerService.Check(rows);
            }
        }
    }
}
