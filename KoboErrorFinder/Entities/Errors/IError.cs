using KoboErrorFinder.Entities.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Errors
{
    public interface IError
    {
        public string UniqueEntityId { get; set; }
        public bool DateError { get; set; }
    }
}
