using KoboErrorFinder.Entities.Rows.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinder.Entities.Rows
{
    public class MHGroupRow : IMyRow, IProviderCodeRow
    {
        public int TotalNumberOfarticipants { get; set; }
        public int Female { get; set; }
        public int Male { get; set; }
        public int y0_4 { get; set; }
        public int y5_9 { get; set; }
        public int y10_14 { get; set; }
        public int y15_19 { get; set; }
        public int y20_44 { get; set; }
        public int y45_64 { get; set; }
        public int y65_Plus { get; set; }
        public string ProviderCode { get; set; }
        public string UniqueEntityId { get; } = Guid.NewGuid().ToString();
        public DateOnly Date { get; set; }
    }
}
