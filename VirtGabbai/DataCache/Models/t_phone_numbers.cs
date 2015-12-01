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
    }
}
