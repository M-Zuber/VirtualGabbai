using System;

namespace DataCache.Models
{
    public class Yahrtzieht
    {
        public Yahrtzieht()
        {

        }

        public Yahrtzieht(int id, DateTime date, string name, string relation)
        {
            Id = id;
            Date = date;
            Name = name;
            Relation = relation;
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Relation { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public virtual Person Person { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Yahrtzieht other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && Date.Date == other.Date.Date
                   && Name == other.Name
                   && Relation == other.Relation);
        }

        public override string ToString() =>
            $"Deceased's Name:\"{Name}\" " +
            $"Date:\"{Date.Date.ToString("dd/MM/yyyy")}\" " +
            $"Relation:\"{Relation}\"";

        public override int GetHashCode() => Id.GetHashCode();
    }
}
