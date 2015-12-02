using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_phone_numbers
    {
        public int person_id { get; set; }
        public string number { get; set; }
        public int number_type { get; set; }
        public int C_id { get; set; }
        public virtual t_people t_people { get; set; }
        public virtual t_phone_types t_phone_types { get; set; }

        public static t_phone_numbers Createt_phone_numbers(int personId, string number, int _Id1, int _Id2) => new t_phone_numbers { person_id = personId, number = number, number_type = _Id1, C_id = _Id2 };
    }
}
