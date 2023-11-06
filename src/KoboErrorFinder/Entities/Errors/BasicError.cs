using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
