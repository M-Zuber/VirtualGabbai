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
        public virtual Person t_people { get; set; }

        public static t_yahrtziehts Createt_yahrtziehts(int _Id, int personId, DateTime date, string name) => new t_yahrtziehts { C_id = _Id, person_id = personId, date = date, deceaseds_name = name };
    }
}
