using System;

namespace DataTypes
{
    public class Yahrtzieht
    {
        public int _Id { get; set; }

        public DateTime Date { get; set; }

        public string Relation { get; set; }

        public string Name { get; set; }

        public int PersonId { get; set; }

        public Yahrtzieht(){}

        public Yahrtzieht(int _id, DateTime date, string relation, string name, int personId)
        {
            this._Id = _id;
            this.Date = date;
            this.Relation = relation;
            this.Name = name;
            this.PersonId = personId;
        }
    }
}
