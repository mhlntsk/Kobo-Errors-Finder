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
        public string UniqueEntityId { get; } = Guid.NewGuid().ToString();
        public DateOnly Date { get; set; }

        public int TotalNumberOfParticipants { get; set; }

        public int Female { get; set; }
        public int Male { get; set; }

        public int Y0_4 { get; set; }
        public int Y5_9 { get; set; }
        public int Y10_14 { get; set; }
        public int Y15_19 { get; set; }
        public int Y20_44 { get; set; }
        public int Y45_64 { get; set; }
        public int Y65_Plus { get; set; }

        public int IDPCount { get; set; }
        public int HostCount { get; set; }
        public int ReturneeCount { get; set; }

        public string ProviderCode { get; set; }
    }
}
