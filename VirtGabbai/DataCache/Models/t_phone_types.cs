using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_phone_types
    {
        public t_phone_types()
        {
            this.t_phone_numbers = new List<t_phone_numbers>();
        }

        public int C_id { get; set; }
        public string type_name { get; set; }
        public virtual ICollection<t_phone_numbers> t_phone_numbers { get; set; }

        public static t_phone_types Createt_phone_types(int _Id, string phoneTypeName) => new t_phone_types { C_id = _Id, type_name = phoneTypeName };
    }
}
