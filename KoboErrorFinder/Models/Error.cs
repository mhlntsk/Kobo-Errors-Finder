using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Models
{
    public class Error
    {
        public Patient Patient { get; set; }
        public bool IdError { get; set; }
        public bool SexError { get; set; }
        public bool AgeError { get; set; }
        public bool AgeMoreThan11MonthError { get; set; }
    }
}
