using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

namespace DataAccess
{
    public static class YahrtziehtAccess
    {

        #region Read Methods

        #region Local type return

        public static Yahrtzieht GetYahrtziehtById(int yahrId)
        {
            return (YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(YahrtziehtAccess.LookupYahrtziehtById(yahrId)));
        }

        public static Yahrtzieht GetSpecificYahrtzieht(int personId, DateTime yahrDate, string personName)
        {
            return YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(YahrtziehtAccess.LookupSpecificYahrtzieht(personId, yahrDate,personName));
        }

        public static List<Yahrtzieht> GetYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            return YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(YahrtziehtAccess.LookupYahrtziehtsByDate(personId, yahrDate));
        }

        public static List<Yahrtzieht> GetAllYahrtziehts(int personId)
        {
            return YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(YahrtziehtAccess.LookupAllYahrtziehts(personId));
        } 

        #endregion

        #region Db type return

        private static t_yahrtziehts LookupSpecificYahrtzieht(int personId, DateTime yahrDate, string personName)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.person_id == personId &&
                          CurrYahr.date == yahrDate &&
                          CurrYahr.deceaseds_name == personName
                    select CurrYahr).First();
        }

        private static List<t_yahrtziehts> LookupYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.date == yahrDate
                    select CurrYahr).ToList<t_yahrtziehts>();
        }

        private static List<t_yahrtziehts> LookupAllYahrtziehts(int personId)
        {
            return (from CurrPerson in Cache.CacheData.t_people
                    where CurrPerson.C_id == personId
                    select CurrPerson).First().t_yahrtziehts.ToList<t_yahrtziehts>();
        }

        private static t_yahrtziehts LookupYahrtziehtById(int id)
        {
            return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                    where CurrYahr.C_id == id
                    select CurrYahr).First();
        } 

        #endregion

        #endregion

        #region Write

        #region Create

        public static void AddNewYahrtzieht(Yahrtzieht newYahr)
        {
            t_yahrtziehts yahrToAdd = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(newYahr);
            Cache.CacheData.t_yahrtziehts.AddObject(yahrToAdd);
            Cache.CacheData.SaveChanges();
        }

        public static void AddMultipleNewYahrtzieht(List<Yahrtzieht> newYahrList)
        {
            foreach (Yahrtzieht newYahr in newYahrList)
            {
                YahrtziehtAccess.AddNewYahrtzieht(newYahr);
            }
        }

        #endregion

        #region Update

        public static void UpdateSingleYahrtzieht(Yahrtzieht updatedYahr)
        {
            t_yahrtziehts yahrUpdating = YahrtziehtAccess.LookupYahrtziehtById(updatedYahr._Id);
            yahrUpdating = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(updatedYahr);
            Cache.CacheData.t_yahrtziehts.ApplyCurrentValues(yahrUpdating);
            Cache.CacheData.SaveChanges();
        }

        public static void UpdateMultipleYahrtziehts(List<Yahrtzieht> updatedYahrList)
        {
            foreach (Yahrtzieht updatedYahr in updatedYahrList)
            {
                YahrtziehtAccess.UpdateSingleYahrtzieht(updatedYahr);
            }
        }

        #endregion

        #region Delete

        public static void DeleteSingleYahrtzieht(Yahrtzieht deletedYahr)
        {
            t_yahrtziehts yahrDeleting = Cache.CacheData.t_yahrtziehts.First(yahr => yahr.C_id == deletedYahr._Id);
            Cache.CacheData.t_yahrtziehts.DeleteObject(yahrDeleting);
            Cache.CacheData.SaveChanges();
        }

        public static void DeleteMultipleYahrtziehts(List<Yahrtzieht> deletedYahrList)
        {
            foreach (Yahrtzieht deletedYahr in deletedYahrList)
            {
                YahrtziehtAccess.DeleteSingleYahrtzieht(deletedYahr);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_yahrtziehts> ConvertMultipleYahrtziehtsToDbType(List<Yahrtzieht> localTypeYahrList)
        {
            List<t_yahrtziehts> dbTypeYahrList = new List<t_yahrtziehts>();

            foreach (Yahrtzieht CurrYahr in localTypeYahrList)
            {
                dbTypeYahrList.Add(YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(CurrYahr));
            }

            return dbTypeYahrList;
        }

        private static t_yahrtziehts ConvertSingleYahrtziehtToDbType(Yahrtzieht localTypeYahr)
        {
            var dbTypeYahr = t_yahrtziehts.Createt_yahrtziehts(localTypeYahr._Id, 1,
                                                         localTypeYahr.Date, localTypeYahr.Name);
            dbTypeYahr.relation = localTypeYahr.Relation;
            return dbTypeYahr;
        }

        private static List<Yahrtzieht> ConvertMultipleYahrtziehtsToLocalType(List<t_yahrtziehts> dbTypeYahrList)
        {
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();

            foreach (t_yahrtziehts Curryahr in dbTypeYahrList)
            {
                localTypeYahrList.Add(YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(Curryahr));
            }

            return localTypeYahrList;
        }

        private static Yahrtzieht ConvertSingleYahrtziehtToLocalType(t_yahrtziehts dbTypeYahr)
        {
            Yahrtzieht localTypeYahr = new Yahrtzieht(dbTypeYahr.C_id, dbTypeYahr.date, dbTypeYahr.deceaseds_name, dbTypeYahr.relation);
            return localTypeYahr;
        } 

        #endregion
    }
}
