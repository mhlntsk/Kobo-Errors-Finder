using KoboErrorFinder.Entities.Rows.Abstractions;

namespace KoboErrorFinder.Entities.Rows
{
    public class AmbulanceRow : IMyRow, IAgeRow
    {
        public string UniqueEntityId { get; } = Guid.NewGuid().ToString();
        public DateOnly Date { get; set; }

        public string AgeUnit { get; set; }
        public string AgeValue { get; set; }

        public string Auto_form_id { get; set; }
        public string RegionOfPatient { get; set; }
        public string OtherPatientLocationSpecify { get; set; }
        public string TypeOfRequester { get; set; }
    }
}
