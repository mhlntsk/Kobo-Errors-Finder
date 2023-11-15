using KoboErrorFinder.Entities.Errors;

namespace KoboErrorFinder.Models
{
    public class BasicError : IError, IAgeError
    {
        public string UniqueEntityId { get; set; }
        public bool DateError { get; set; }

        public bool AgeError { get; set; }
        public bool AgeMoreThan11MonthError { get; set; }

        public bool IdError { get; set; }
        public bool SexError { get; set; }
    }
}
