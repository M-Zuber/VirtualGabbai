using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PhoneNumber
    {
        public int person_id { get; set; }
        public string number { get; set; }
        public int number_type { get; set; }
        public int C_id { get; set; }
        public virtual Person t_people { get; set; }
        public virtual t_phone_types t_phone_types { get; set; }

        public static PhoneNumber Createt_phone_numbers(int personId, string number, int _Id1, int _Id2) => new PhoneNumber { person_id = personId, number = number, number_type = _Id1, C_id = _Id2 };
    }
}
