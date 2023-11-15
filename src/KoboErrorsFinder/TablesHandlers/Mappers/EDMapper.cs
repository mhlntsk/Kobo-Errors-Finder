using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.TablesExtensions.Mappers
{
    public class EDMapper : BasicMapper, IMapper<EDMapper>
    {
        protected override string nameOfDateCell { get => "Date of arrival"; }

        public override IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
        {
            BasicRow myRow = new BasicRow();

            MapMsfPatientId(headersOfSheet, myRow, rowFromTable);

            MapSex(headersOfSheet, myRow, rowFromTable);

            MapAgeUnit(headersOfSheet, myRow, rowFromTable);

            MapAgeValue(headersOfSheet, myRow, rowFromTable);

            MapDate(headersOfSheet, myRow, rowFromTable);

            return myRow;
        }
    }
}
