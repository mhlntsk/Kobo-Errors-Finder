using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinderTests.FakeData
{
    public class FakeIDsGenerator
    {
        public string GetValidIds()
        {
            string[] ids =
            {
                "DE-AND-OT-080923-002",
                "DE-ANV-MH-131023-010",
                "DE-KHR-ED-091023-004",
                "DE-MAL-OPD-260923-022",
                "DE-KHR-SRH-210823-008",
                "DE-SEL-ICU-210823-001",
            };

            Random rnd = new Random();

            return ids[rnd.Next(0, ids.Length)];
        }

        public string GetInvalidIds()
        {
            string[] ids =
            {
                "DE-LYM-ED-301023-052", // Cyrillic "-МН-" group 
                " DE-DNY-МН-301023-001",
                "DE-ZAP-ICU-160323-100 ",
                "DE-KHR -OT-301023-012",
                "DE-KHR-OPD-301023-0012",
                "DE-RHI-SRH-301023",
                "InvalidId",
            };

            Random rnd = new Random();

            return ids[rnd.Next(0, ids.Length)];
        }
    }
}
