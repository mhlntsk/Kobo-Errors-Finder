namespace KoboErrorFinder.Entities.Errors
{
    public interface IAgeError : IError
    {
        public bool AgeError { get; set; }
        public bool AgeMoreThan11MonthError { get; set; }
    }
}
