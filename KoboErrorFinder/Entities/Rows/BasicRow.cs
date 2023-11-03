using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities.Rows.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities
{
    public class BasicRow : IMyRow, IAgeRow
    {
        public string MSFPatientID { get; set; }
        public string Sex { get; set; }
        public string AgeUnit { get; set; }
        public string AgeValue { get; set; }
        public string UniqueEntityId { get; } = Guid.NewGuid().ToString();
        public DateOnly Date { get; set; }
    }
}
