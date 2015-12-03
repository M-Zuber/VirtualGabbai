using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Yahrtzieht
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string Relation { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public virtual Person Person { get; set; }

        public static Yahrtzieht Createt_yahrtziehts(int _Id, int personId, DateTime date, string name) => new Yahrtzieht { ID = _Id, PersonID = personId, Date = date, Name = name };
    }
}
