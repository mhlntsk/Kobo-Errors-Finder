using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Models
{
    public class Patient
    {
        public string PatientId { get; set; }
        public DateOnly DateOfConsultation { get; set; }
        public string Sex { get; set; }
        public string AgeUnit { get; set; }
        public string Age { get; set; }

    }
}
