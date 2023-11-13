using NPOI.POIFS.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboErrorFinderTests.FakeData
{
    public class FakeDataGenerator
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

            var Random = new Random();

            return ids[Random.Next(0, ids.Length)];
        }
        public string GetInvalidIds()
        {
            string[] ids =
            {
                "DE-LYM-МН-301023-052", // Cyrillic "-МН-" group 
                " DE-DNY-МН-301023-001",
                "DE-ZAP-ICU-160323-100 ",
                "DE-KHR -OT-301023-012",
                "DE-KHR-OPD-301023-0012",
                "DE-RHI-SRH-301023",
                "InvalidId",
                " ",
                "",
            };

            var Random = new Random();

            return ids[Random.Next(0, ids.Length)];
        }


        public (string key, string value) GetValidAgePair()
        {
            var dictionary = new (string key, string value)[]
            {
                (key: "10", value: "10"),
                (key: "32", value: "33"),
                (key: "12", value: "13"),
                (key: "41", value: "41"),
                (key: "54", value: "55"),
                (key: "16", value: "16"),
                (key: "124", value: "125"),
            };

            var Random = new Random();

            return dictionary[Random.Next(0, dictionary.Length)];
        }
        public (string key, string value) GetInvalidAgePair()
        {
            var dictionary = new (string key, string value)[]
            {
                (key: "10", value: "9"),
                (key: "32", value: "30"),
                (key: "12", value: "1"),
                (key: "41", value: "99"),
                (key: "54", value: "57"),
                (key: "1", value: "15"),
                (key: "124", value: "100"),
                (key: "54", value: null),
            };

            var Random = new Random();

            return dictionary[Random.Next(0, dictionary.Length)];
        }
        public (string key, string value) GetEmptyAgePair()
        {
            var dictionary = new (string key, string value)[]
            {
                (key: "41", value: " "),
                (key: "", value: "15"),
            };

            var Random = new Random();

            return dictionary[Random.Next(0, dictionary.Length)];
        }


        public (string key, string value1, string value2) GetValidSexSequence()
        {
            var dictionary = new (string key, string value1, string value2)[]
            {
                (key: "Male", value1: "Male", value2: "Male"),
                (key: "Female", value1: "Female", value2: "Female"),
                (key: "SexExample", value1: "SexExample", value2: "SexExample"),
            };

            var Random = new Random();

            return dictionary[Random.Next(0, dictionary.Length)];
        }
        public (string key, string value1, string value2) GetInvalidSexSequence()
        {
            var dictionary = new (string key, string value1, string value2)[]
            {
                (key: "Male", value1: "Female", value2: "Male"),
                (key: "female", value1: "Female", value2: "Female"),
                (key: " ", value1: "SexExample", value2: "SexExample"),
                (key: "", value1: "SexExample", value2: "SexExample"),
                (key: null, value1: "SexExample", value2: "SexExample"),
            };

            var Random = new Random();

            return dictionary[Random.Next(0, dictionary.Length)];
        }
    }
}
