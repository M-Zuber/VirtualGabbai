using System;

namespace DataCache.Models
{
    public partial class Yahrtzieht
    {
        #region Object Methods

        public override bool Equals(object yahrComparingObj)
        {
            Yahrtzieht yahrComparing = (Yahrtzieht)yahrComparingObj;
            return ((this.ID == yahrComparing.ID) && 
                    (this.Date.Date == yahrComparing.Date.Date) &&
                    (this.Name == yahrComparing.Name) &&
                    (this.Relation == yahrComparing.Relation));
        }

        public override string ToString() => "Deceased's Name:\"" + this.Name + "\" " +
       "Date:\"" + this.Date.Date.ToString("dd/MM/yyyy") + "\" " +
       "Relation:\"" + this.Relation + "\"";

        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
