namespace DataCache.Models
{
    public partial class Privilege
    {
        #region Object Methods

        public override bool Equals(object obj)
        {
            Privilege comparedPrivilege = (Privilege)obj;
            return ((ID == comparedPrivilege.ID) &&
                    (Name == comparedPrivilege.Name));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Name;

        #endregion
    }
}
