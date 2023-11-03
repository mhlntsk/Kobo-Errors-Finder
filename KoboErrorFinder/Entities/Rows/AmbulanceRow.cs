using KoboErrorFinder.Entities.Rows.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Rows
{
    public class AmbulanceRow : IMyRow, IAgeRow
    {
        public string Auto_form_id { get; set; }
        public string RegionOfPatient { get; set; }
        public string OtherPatientLocationSpecify { get; set; }
        public string AgeUnit { get; set; }
        public string AgeValue { get; set; }
        public string UniqueEntityId { get; } = Guid.NewGuid().ToString();
        public DateOnly Date { get; set; }
    }
}
