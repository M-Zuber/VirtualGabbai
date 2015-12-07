using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Yahrtzieht
    {
        public Yahrtzieht()
        {

        }
        public Yahrtzieht(int id, DateTime date, string name, string relation)
        {
            ID = id;
            Date = date;
            Name = name;
            Relation = relation;
        }

        public int ID { get; set; }
        public int PersonID { get; set; }
        public string Relation { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public virtual Person Person { get; set; }

        public static Yahrtzieht Createt_yahrtziehts(int _Id, int personId, DateTime date, string name) => new Yahrtzieht { ID = _Id, PersonID = personId, Date = date, Name = name };

        public override bool Equals(object yahrComparingObj)
        {
            Yahrtzieht yahrComparing = (Yahrtzieht)yahrComparingObj;
            return ((ID == yahrComparing.ID) &&
                    (Date.Date == yahrComparing.Date.Date) &&
                    (Name == yahrComparing.Name) &&
                    (Relation == yahrComparing.Relation));
        }

        public override string ToString() => 
            $"Deceased's Name:\"{Name}\" " +
            $"Date:\"{Date.Date.ToString("dd/MM/yyyy")}\" " +
            $"Relation:\"{Relation}\"";

        public override int GetHashCode() => base.GetHashCode();
    }
}
