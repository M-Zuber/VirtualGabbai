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

        #region Read Methods

        public Yarthzieht GetSpecificYarthzieht(long personId, DateTime date, string personName)
        {
            t_yarthziehts requestedYarhtzieht = (from test in Cache.CacheData.t_yarthziehts
                                                 where test.person_id == personId &&
                                                       test.date == date &&
                                                       test.deceaseds_name == personName
                                                 select test).First();

            return this.ConverSingleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yarthzieht> GetYarhtzietsByName(long personId, string personName)
        {
           List<t_yarthziehts> requestedYarhtzieht = (from test in Cache.CacheData.t_yarthziehts
                                                      where test.deceaseds_name == personName
                                                      select test).ToList<t_yarthziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yarthzieht> GetYarhtzietsByDate(long personId, DateTime date)
        {
            List<t_yarthziehts> requestedYarhtzieht = (from test in Cache.CacheData.t_yarthziehts
                                                       where test.date == date
                                                       select test).ToList<t_yarthziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yarthzieht> GetYarhtzietsByRelation(long personId, string relation)
        {
            List<t_yarthziehts> requestedYarhtzieht = (from test in Cache.CacheData.t_yarthziehts
                                                       where test.relation == relation
                                                       select test).ToList<t_yarthziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(requestedYarhtzieht);
        }

        public List<Yarthzieht> GetAllYarthziehts(long personId)
        {
            t_people test = (t_people)Cache.CacheData.t_people.Take(1);

            //List<t_yarthziehts> AllYarhtziets = (from CurrentYarhtziet  in Cache.CacheData.t_yarthziehts
            //                                     where CurrentYarhtziet.person_id == personId
            //                                     select CurrentYarhtziet).ToList<t_yarthziehts>();

            return this.ConvertMultipleYarhtzietToLocalType(test.t_yarthziehts.ToList<t_yarthziehts>());
        }

        private t_yarthziehts LookupSpecificYarthzieht(long personId, DateTime date, string personName)
        {
            return (from test in Cache.CacheData.t_yarthziehts
                    where test.person_id == personId &&
                        test.date == date &&
                        test.deceaseds_name == personName
                    select test).First();
        }

        private List<t_yarthziehts> LookupYarhtzietsByName(long personId, string personName)
        {
            return (from test in Cache.CacheData.t_yarthziehts
                    where test.deceaseds_name == personName
                    select test).ToList<t_yarthziehts>();
        }

        private List<t_yarthziehts> LookupYarhtzietsByDate(long personId, DateTime date)
        {
            return (from test in Cache.CacheData.t_yarthziehts
                    where test.date == date
                    select test).ToList<t_yarthziehts>();
        }

        private List<t_yarthziehts> LookupYarhtzietsByRelation(long personId, string relation)
        {
            return (from test in Cache.CacheData.t_yarthziehts
                    where test.relation == relation
                    select test).ToList<t_yarthziehts>();
        }

        private List<t_yarthziehts> LookupAllYarthziehts(long personId)
        {
            return ((t_people)Cache.CacheData.t_people.Take(1)).t_yarthziehts.ToList<t_yarthziehts>();
        }

        private t_yarthziehts LookupYarhtzietById(long ID)
        {
            return (from test in Cache.CacheData.t_yarthziehts
                    where test.C_id == ID
                    select test).First();
        }

        #endregion

        #region Write

        #region Create

        public void AddNewYartzieht(Yarthzieht ya)
        {
            t_yarthziehts yaToAdd = this.ConvertSingleYarhtzietToDbType(ya);
            Cache.CacheData.t_yarthziehts.AddObject(yaToAdd);
        }

        public void AddMultipleNewYartzieht(List<Yarthzieht> myYaList)
        {
            foreach (var item in myYaList)
            {
                this.AddNewYartzieht(item);
            }
        }

        #endregion

        #region Update

        public void UpdateSingleYarhtzieht(Yarthzieht ya)
        {
            t_yarthziehts instanceUpdating = this.LookupYarhtzietById(ya._Id);
            //TODO lookup how to do updating when using entity framework
        }

        #endregion

        #region Delete

        public void DeleteSingleYarhtzieht(Yarthzieht ya)
        {
            Cache.CacheData.t_yarthziehts.DeleteObject(this.ConvertSingleYarhtzietToDbType(ya));
        }

        public void DeleteMultipleYarhtziet(List<Yarthzieht> myYaList)
        {
            foreach (var item in myYaList)
            {
                this.DeleteSingleYarhtzieht(item);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private List<t_yarthziehts> ConvertMultipleYarhtzietToDbType(List<Yarthzieht> myYaList)
        {
            List<t_yarthziehts> myList = new List<t_yarthziehts>();

            foreach (var item in myYaList)
            {
                myList.Add(this.ConvertSingleYarhtzietToDbType(item));
            }

            return myList;
        }

        private t_yarthziehts ConvertSingleYarhtzietToDbType(Yarthzieht ya)
        {
            return t_yarthziehts.Createt_yarthziehts(ya._Id, ya.PersonId, ya.Date, ya.Name);
        }

        private List<Yarthzieht> ConvertMultipleYarhtzietToLocalType(List<t_yarthziehts> ya)
        {
            List<Yarthzieht> myList = new List<Yarthzieht>();

            foreach (var item in ya)
            {
                myList.Add(this.ConverSingleYarhtzietToLocalType(item));
            }

            return myList;
        }

        private Yarthzieht ConverSingleYarhtzietToLocalType(t_yarthziehts ya)
        {
            Yarthzieht instance = new Yarthzieht();
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
