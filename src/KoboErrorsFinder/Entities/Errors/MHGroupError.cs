using KoboErrorFinder.Entities.Rows;

namespace KoboErrorFinder.Entities.Errors.Abstractions
{
    public class MHGroupError : IError
    {
        public string UniqueEntityId { get; set; }
        public bool DateError { get; set; }

        public bool ParticipantsBySexError { get; set; }
        public bool ParticipantsByAgeError { get; set; }
        public bool ParticipantsByPatientStatusError { get; set; }
    }
}
