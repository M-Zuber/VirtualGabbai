using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;
using DataCache;

namespace DataAccess
{
    public class YarthzietAccess
    {
        public Yarthzieht GetYarthzieht(int personId, DateTime yarthziehtId, string personName)
        {
            t_yarthziehts RequestedYarhtzieht = (from test in Cache.CacheData.t_yarthziehts
                                                where test.person_id == personId && test.date == yarthziehtId && test.deceaseds_name == personName
                                                select test).First();
            return null; 
        }

        public List<Yarthzieht> GetYarthziehts(int personId) 
        {
            t_people test = (t_people)Cache.CacheData.t_people.Take(1);
            
            //List<t_yarthziehts> AllYarhtziets = (from CurrentYarhtziet  in Cache.CacheData.t_yarthziehts
            //                                     where CurrentYarhtziet.person_id == personId
            //                                     select CurrentYarhtziet).ToList<t_yarthziehts>();

            return this.ConvertListToLocalType(test.t_yarthziehts.ToList<t_yarthziehts>()); 
        }

        //public List<Yarthzieht> GetYarthziehts(string s) { return null; }

        private List<Yarthzieht> ConvertListToLocalType(List<t_yarthziehts> ya) 
        {
            List<Yarthzieht> myList = new List<Yarthzieht>();

            foreach (var item in ya)
            {
               myList.Add(this.ConvertToLocalType(item));
            }

            return myList;
        }

        private Yarthzieht ConvertToLocalType(t_yarthziehts ya)
        {
            Yarthzieht instance = new Yarthzieht();
            instance._Id = ya.C_id;
            instance.Date = ya.date;
            instance.Relation = ya.relation;
            instance.Name = ya.deceaseds_name;
            return instance;
        }
    }
}
