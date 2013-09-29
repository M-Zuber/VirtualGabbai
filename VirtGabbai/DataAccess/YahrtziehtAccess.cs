using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;
using DataCache;

namespace DataAccess
{
    public class YahrtziehtAccess
    {

        #region Read Methods

        public Yahrtzieht GetSpecificYarthzieht(long personId, DateTime date, string personName)
        {
            t_yahrtziehts requestedYarhtzieht = (from test in Cache.CacheData.t_yahrtziehts
                                                 where test.person_id == personId &&
                                                       test.date == date &&
                                                       test.deceaseds_name == personName
                                                 select test).First();

            return this.ConverSingleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yahrtzieht> GetYarhtzietsByDate(long personId, DateTime date)
        {
            List<t_yahrtziehts> requestedYarhtzieht = (from test in Cache.CacheData.t_yahrtziehts
                                                       where test.date == date
                                                       select test).ToList<t_yahrtziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yahrtzieht> GetAllYarthziehts(long personId)
        {
            t_people test = (t_people)Cache.CacheData.t_people.Take(1);

            //List<t_yarthziehts> AllYarhtziets = (from CurrentYarhtziet  in Cache.CacheData.t_yarthziehts
            //                                     where CurrentYarhtziet.person_id == personId
            //                                     select CurrentYarhtziet).ToList<t_yarthziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(test.t_yahrtziehts.ToList<t_yahrtziehts>());
        }

        private t_yahrtziehts LookupSpecificYarthzieht(long personId, DateTime date, string personName)
        {
            return (from test in Cache.CacheData.t_yahrtziehts
                    where test.person_id == personId &&
                        test.date == date &&
                        test.deceaseds_name == personName
                    select test).First();
        }

        private List<t_yahrtziehts> LookupYarhtzietsByDate(long personId, DateTime date)
        {
            return (from test in Cache.CacheData.t_yahrtziehts
                    where test.date == date
                    select test).ToList<t_yahrtziehts>();
        }

        private List<t_yahrtziehts> LookupAllYarthziehts(long personId)
        {
            return ((t_people)Cache.CacheData.t_people.Take(1)).t_yahrtziehts.ToList<t_yahrtziehts>();
        }

        private t_yahrtziehts LookupYarhtzietById(long ID)
        {
            return (from test in Cache.CacheData.t_yahrtziehts
                    where test.C_id == ID
                    select test).First();
        }

        #endregion

        #region Write

        #region Create

        public void AddNewYartzieht(Yahrtzieht ya)
        {
            t_yahrtziehts yaToAdd = this.ConvertSingleYarhtzietToDbType(ya);
            Cache.CacheData.t_yahrtziehts.AddObject(yaToAdd);
            Cache.CacheData.SaveChanges();
        }

        public void AddMultipleNewYartzieht(List<Yahrtzieht> myYaList)
        {
            foreach (var item in myYaList)
            {
                this.AddNewYartzieht(item);
            }
        }

        #endregion

        #region Update

        public void UpdateSingleYarhtzieht(Yahrtzieht ya)
        {
            t_yahrtziehts instanceUpdating = this.LookupYarhtzietById(ya._Id);
            Cache.CacheData.t_yahrtziehts.Attach(instanceUpdating);
            Cache.CacheData.SaveChanges();
            //TODO lookup how to do updating when using entity framework
            //db.users.attach(updateduser)
            // db.savechanges()
            // db.Entry(original).currentvalues.setvalues(updatedUser) -after first loading origional
        }

        public void UpdateMultipleYarhtzieht(List<Yahrtzieht> myYaList)
        {
            foreach (var item in myYaList)
            {
                this.UpdateSingleYarhtzieht(item);
            }
        }

        #endregion

        #region Delete

        public void DeleteSingleYarhtzieht(Yahrtzieht ya)
        {
            t_yahrtziehts test = Cache.CacheData.t_yahrtziehts.First(person => person.C_id == ya._Id);
            Cache.CacheData.t_yahrtziehts.DeleteObject(test);
            Cache.CacheData.SaveChanges();
        }

        public void DeleteMultipleYarhtziet(List<Yahrtzieht> myYaList)
        {
            foreach (var item in myYaList)
            {
                this.DeleteSingleYarhtzieht(item);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private List<t_yahrtziehts> ConvertMultipleYarhtzietToDbType(List<Yahrtzieht> myYaList)
        {
            List<t_yahrtziehts> myList = new List<t_yahrtziehts>();

            foreach (var item in myYaList)
            {
                myList.Add(this.ConvertSingleYarhtzietToDbType(item));
            }

            return myList;
        }

        private t_yahrtziehts ConvertSingleYarhtzietToDbType(Yahrtzieht ya)
        {
            var test = t_yahrtziehts.Createt_yahrtziehts(ya._Id, ya.PersonId, ya.Date, ya.Name);
            test.relation = ya.Relation;
            return test;
        }

        private List<Yahrtzieht> ConvertMultipleYarhtzietToLocalType(List<t_yahrtziehts> ya)
        {
            List<Yahrtzieht> myList = new List<Yahrtzieht>();

            foreach (var item in ya)
            {
                myList.Add(this.ConverSingleYarhtzietToLocalType(item));
            }

            return myList;
        }

        private Yahrtzieht ConverSingleYarhtzietToLocalType(t_yahrtziehts ya)
        {
            Yahrtzieht instance = new Yahrtzieht();
            instance._Id = ya.C_id;
            instance.Date = ya.date;
            instance.Relation = ya.relation;
            instance.Name = ya.deceaseds_name;
            instance.PersonId = ya.person_id;

            return instance;
        } 

        #endregion
    }
}
