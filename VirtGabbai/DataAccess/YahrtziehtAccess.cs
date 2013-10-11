using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

namespace DataAccess
{
    public class YahrtziehtAccess
    {

        #region Read Methods

        #region Local type return

        public Yahrtzieht GetYahrtziehtById(int yahrId)
        {
            return (this.ConvertSingleYahrtziehtToLocalType(this.LookupYahrtziehtById(yahrId)));
        }

        public Yahrtzieht GetSpecificYahrtzieht(int personId, DateTime yahrDate, string personName)
        {
            return this.ConvertSingleYahrtziehtToLocalType(this.LookupSpecificYahrtzieht(personId, yahrDate,personName));
        }

        public List<Yahrtzieht> GetYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            return this.ConvertMultipleYahrtziehtsToLocalType(this.LookupYahrtziehtsByDate(personId, yahrDate));
        }

        public List<Yahrtzieht> GetAllYahrtziehts(int personId)
        {
            return this.ConvertMultipleYahrtziehtsToLocalType(this.LookupAllYahrtziehts(personId));
        } 

        #endregion

        #region Db type return
        
        private t_yahrtziehts LookupSpecificYahrtzieht(int personId, DateTime yahrDate, string personName)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.person_id == personId &&
                          CurrYahr.date == yahrDate &&
                          CurrYahr.deceaseds_name == personName
                    select CurrYahr).First();
        }

        private List<t_yahrtziehts> LookupYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.date == yahrDate
                    select CurrYahr).ToList<t_yahrtziehts>();
        }

        private List<t_yahrtziehts> LookupAllYahrtziehts(int personId)
        {
            return (from CurrPerson in Cache.CacheData.t_people
                    where CurrPerson.C_id == personId
                    select CurrPerson).First().t_yahrtziehts.ToList<t_yahrtziehts>();
        }

        private t_yahrtziehts LookupYahrtziehtById(int id)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.C_id == id
                    select CurrYahr).First();
        } 

        #endregion

        #endregion

        #region Write

        #region Create

        public void AddNewYahrtzieht(Yahrtzieht newYahr)
        {
            t_yahrtziehts yahrToAdd = this.ConvertSingleYahrtziehtToDbType(newYahr);
            Cache.CacheData.t_yahrtziehts.AddObject(yahrToAdd);
            Cache.CacheData.SaveChanges();
        }

        public void AddMultipleNewYahrtzieht(List<Yahrtzieht> newYahrList)
        {
            foreach (Yahrtzieht newYahr in newYahrList)
            {
                this.AddNewYahrtzieht(newYahr);
            }
        }

        #endregion

        #region Update

        public void UpdateSingleYahrtzieht(Yahrtzieht updatedYahr)
        {
            t_yahrtziehts yahrUpdating = this.LookupYahrtziehtById(updatedYahr._Id);
            yahrUpdating = this.ConvertSingleYahrtziehtToDbType(updatedYahr);
            Cache.CacheData.t_yahrtziehts.ApplyCurrentValues(yahrUpdating);
            Cache.CacheData.SaveChanges();
        }

        public void UpdateMultipleYahrtziehts(List<Yahrtzieht> updatedYahrList)
        {
            foreach (Yahrtzieht updatedYahr in updatedYahrList)
            {
                this.UpdateSingleYahrtzieht(updatedYahr);
            }
        }

        #endregion

        #region Delete

        public void DeleteSingleYahrtzieht(Yahrtzieht deletedYahr)
        {
            t_yahrtziehts yahrDeleting = Cache.CacheData.t_yahrtziehts.First(yahr => yahr.C_id == deletedYahr._Id);
            Cache.CacheData.t_yahrtziehts.DeleteObject(yahrDeleting);
            Cache.CacheData.SaveChanges();
        }

        public void DeleteMultipleYahrtziehts(List<Yahrtzieht> deletedYahrList)
        {
            foreach (Yahrtzieht deletedYahr in deletedYahrList)
            {
                this.DeleteSingleYahrtzieht(deletedYahr);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private List<t_yahrtziehts> ConvertMultipleYahrtziehtsToDbType(List<Yahrtzieht> localTypeYahrList)
        {
            List<t_yahrtziehts> dbTypeYahrList = new List<t_yahrtziehts>();

            foreach (Yahrtzieht CurrYahr in localTypeYahrList)
            {
                dbTypeYahrList.Add(this.ConvertSingleYahrtziehtToDbType(CurrYahr));
            }

            return dbTypeYahrList;
        }

        private t_yahrtziehts ConvertSingleYahrtziehtToDbType(Yahrtzieht localTypeYahr)
        {
            var dbTypeYahr = t_yahrtziehts.Createt_yahrtziehts(localTypeYahr._Id, localTypeYahr.PersonId,
                                                         localTypeYahr.Date, localTypeYahr.Name);
            dbTypeYahr.relation = localTypeYahr.Relation;
            return dbTypeYahr;
        }

        private List<Yahrtzieht> ConvertMultipleYahrtziehtsToLocalType(List<t_yahrtziehts> dbTypeYahrList)
        {
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();

            foreach (t_yahrtziehts Curryahr in dbTypeYahrList)
            {
                localTypeYahrList.Add(this.ConvertSingleYahrtziehtToLocalType(Curryahr));
            }

            return localTypeYahrList;
        }

        private Yahrtzieht ConvertSingleYahrtziehtToLocalType(t_yahrtziehts dbTypeYahr)
        {
            Yahrtzieht localTypeYahr = new Yahrtzieht(dbTypeYahr.C_id, dbTypeYahr.date, dbTypeYahr.deceaseds_name, dbTypeYahr.relation, dbTypeYahr.person_id);
            return localTypeYahr;
        } 

        #endregion
    }
}
