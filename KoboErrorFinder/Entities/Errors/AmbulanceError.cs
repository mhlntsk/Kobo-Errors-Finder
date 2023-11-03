using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Errors
{
    public class AmbulanceError : IError, IAgeError
    {
        public string UniqueEntityId { get; set; }
        public bool DateError { get; set; }

        public bool AgeError { get; set; }
        public bool AgeMoreThan11MonthError { get; set; }

        public bool OtherPatientLocationSpecifyIsEmpty { get; set; }

    }
}
