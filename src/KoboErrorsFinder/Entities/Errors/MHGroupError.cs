using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Errors.Abstractions
{
    public class MHGroupError : IError
    {
        public string UniqueEntityId { get; set; }
        public bool DateError { get; set; }
        public bool ParticipantBySexError { get; set; }
        public bool ParticipantByAgeError { get; set; }
    }
}
