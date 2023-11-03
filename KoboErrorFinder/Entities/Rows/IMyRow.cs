using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Rows
{
    public interface IMyRow
    {
        public DateOnly Date { get; set; }
        public string UniqueEntityId { get; }
    }
}
