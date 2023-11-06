using KoboErrorFinder.Entities.Rows;
using NPOI.SS.UserModel;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public class AmbulanceMapper : AbstractAgeValueMapper, IAmbulanceMapper
    {
        public override IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
        {
            var myRow = new AmbulanceRow();

            MapDate(headersOfSheet, myRow, rowFromTable);

            MapAgeUnit(headersOfSheet, myRow, rowFromTable);

            MapAgeValue(headersOfSheet, myRow, rowFromTable);

            MapAutoFormId(headersOfSheet, myRow, rowFromTable);

            MapRegionOfPatient(headersOfSheet, myRow, rowFromTable);

            MapOtherPatientLocationSpecify(headersOfSheet, myRow, rowFromTable);

            return myRow;
        }
        public void MapAutoFormId(Dictionary<string, int> headersOfSheet, AmbulanceRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("auto_form_id"))
            {
                int columnIndex = headersOfSheet["auto_form_id"];
                myRow.Auto_form_id = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }
        public void MapRegionOfPatient(Dictionary<string, int> headersOfSheet, AmbulanceRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Region of patient"))
            {
                int columnIndex = headersOfSheet["Region of patient"];
                myRow.RegionOfPatient = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }
        public void MapOtherPatientLocationSpecify(Dictionary<string, int> headersOfSheet, AmbulanceRow myRow, IRow rowFromTable)
        {
            if (headersOfSheet.ContainsKey("Other Patient location: specify"))
            {
                int columnIndex = headersOfSheet["Other Patient location: specify"];
                myRow.OtherPatientLocationSpecify = rowFromTable.GetCell(columnIndex)?.ToString();
            }
        }
    }
}
