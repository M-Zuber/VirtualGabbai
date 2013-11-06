using System;

namespace LocalTypes
{
    public class Yahrtzieht
    {
        #region Properties

        public int _Id { get; set; }

        public DateTime Date { get; set; }

        public string Relation { get; set; }

        public string Name { get; set; }
        
        #endregion

        #region C'tor

        public Yahrtzieht() { }

        public Yahrtzieht(int _id, DateTime date, string name, string relation)
        {
            this._Id = _id;
            this.Date = date;
            this.Relation = relation;
            this.Name = name;
        }
        
        #endregion

        #region Object Methods

        public override bool Equals(object yahrComparingObj)
        {
            Yahrtzieht yahrComparing = (Yahrtzieht)yahrComparingObj;
            return ((this._Id == yahrComparing._Id) && 
                    (this.Date.Date == yahrComparing.Date.Date) &&
                    (this.Name == yahrComparing.Name) &&
                    (this.Relation == yahrComparing.Relation));
        }

        public override string ToString()
        {
            return "Deceased's Name:\"" + this.Name + "\" " +
                   "Date:\""+ this.Date.Date.ToString("dd/MM/yyyy") + "\" " +
                   "Relation:\"" +this.Relation + "\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
