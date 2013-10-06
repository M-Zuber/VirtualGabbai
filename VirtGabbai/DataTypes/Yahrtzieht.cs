using System;

namespace DataTypes
{
    public class Yahrtzieht
    {
        #region Properties

        public int _Id { get; set; }

        public DateTime Date { get; set; }

        public string Relation { get; set; }

        public string Name { get; set; }

        public int PersonId { get; set; }
        
        #endregion

        #region C'tor

        public Yahrtzieht() { }

        public Yahrtzieht(int _id, DateTime date, string relation, string name, int personId)
        {
            this._Id = _id;
            this.Date = date;
            this.Relation = relation;
            this.Name = name;
            this.PersonId = personId;
        }
        
        #endregion

        #region Helper Methods

        public bool Equals(Yahrtzieht yahrComparing)
        {
            return ((this._Id == yahrComparing._Id) || 
                    (this.Date == yahrComparing.Date) || 
                    (this.Name == yahrComparing.Name) || 
                    (this.PersonId == yahrComparing.PersonId) ||
                    (this.Relation == this.Relation));
        }

        #endregion
    }
}
