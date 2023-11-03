using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Rows.Abstractions
{
    public interface IAgeRow : IMyRow
    {
        public string AgeUnit { get; set; }
        public string AgeValue { get; set; }
    }
}
