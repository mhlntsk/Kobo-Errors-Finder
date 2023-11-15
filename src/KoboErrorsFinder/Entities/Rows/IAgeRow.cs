namespace KoboErrorFinder.Entities.Rows.Abstractions
{
    public interface IAgeRow : IMyRow
    {
        public string AgeUnit { get; set; }
        public string AgeValue { get; set; }
    }
}
