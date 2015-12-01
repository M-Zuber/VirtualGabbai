using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_yahrtziehts
    {
        public int C_id { get; set; }
        public int person_id { get; set; }
        public string relation { get; set; }
        public System.DateTime date { get; set; }
        public string deceaseds_name { get; set; }
        public virtual t_people t_people { get; set; }
    }
}
