using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class YahrtziehtAccess
    {

        #region Read Methods

        #region Local type return

        public static Yahrtzieht GetYahrtziehtById(int yahrId) => (YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(YahrtziehtAccess.LookupYahrtziehtById(yahrId)));

        public static Yahrtzieht GetSpecificYahrtzieht(int personId, DateTime yahrDate, string personName) => YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(YahrtziehtAccess.LookupSpecificYahrtzieht(personId, yahrDate, personName));

        public static List<Yahrtzieht> GetYahrtziehtsByDate(int personId, DateTime yahrDate) => YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(YahrtziehtAccess.LookupYahrtziehtsByDate(personId, yahrDate));

        public static List<Yahrtzieht> GetAllYahrtziehts(int personId) => YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(YahrtziehtAccess.LookupAllYahrtziehts(personId));

        #endregion

        #region Db type return

        private static Yahrtzieht LookupSpecificYahrtzieht(int personId, DateTime yahrDate, string personName)
        {
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.PersonID == personId &&
                              CurrYahr.Date == yahrDate &&
                              CurrYahr.Name == personName
                        select CurrYahr).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Yahrtzieht> LookupYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.Date == yahrDate
                        select CurrYahr).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Yahrtzieht> LookupAllYahrtziehts(int personId)
        {
            try
            {
                return (from CurrPerson in Cache.CacheData.t_people
                        where CurrPerson.ID == personId
                        select CurrPerson).First().Yahrtziehts.ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Yahrtzieht LookupYahrtziehtById(int id)
        {
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.ID == id
                        select CurrYahr).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        } 

        #endregion

        #endregion

        #region Write

        #region Create

        public static Enums.CRUDResults AddNewYahrtzieht(Yahrtzieht newYahr, int personId)
        {
            try
            {
                DataCache.Models.Yahrtzieht yahrToAdd = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(newYahr, personId);
                Cache.CacheData.t_yahrtziehts.Add(yahrToAdd);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewYahrtzieht(List<Yahrtzieht> newYahrList, int personId)
        {
            Enums.CRUDResults result;
            foreach (Yahrtzieht newYahr in newYahrList)
            {
                result = YahrtziehtAccess.AddNewYahrtzieht(newYahr, personId);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSingleYahrtzieht(Yahrtzieht updatedYahr, int personId)
        {
            try
            {
                DataCache.Models.Yahrtzieht yahrUpdating = YahrtziehtAccess.LookupYahrtziehtById(updatedYahr.ID);
                yahrUpdating = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(updatedYahr, personId);
                Cache.CacheData.t_yahrtziehts.Attach(yahrUpdating);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultipleYahrtziehts(List<Yahrtzieht> updatedYahrList, int personId)
        {
            Enums.CRUDResults result;
            foreach (Yahrtzieht updatedYahr in updatedYahrList)
            {
                result = YahrtziehtAccess.UpdateSingleYahrtzieht(updatedYahr, personId);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSingleYahrtzieht(Yahrtzieht deletedYahr)
        {
            try
            {
                DataCache.Models.Yahrtzieht yahrDeleting = Cache.CacheData.t_yahrtziehts.First(yahr => yahr.ID == deletedYahr.ID);
                Cache.CacheData.t_yahrtziehts.Remove(yahrDeleting);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultipleYahrtziehts(List<Yahrtzieht> deletedYahrList)
        {
            Enums.CRUDResults result;
            foreach (Yahrtzieht deletedYahr in deletedYahrList)
            {
                result = YahrtziehtAccess.DeleteSingleYahrtzieht(deletedYahr);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSingleYahrtzieht(Yahrtzieht upsertedYahrtzieht, int personId)
        {
            Yahrtzieht currentYahrtzieht = GetYahrtziehtById(upsertedYahrtzieht.ID);

            if (currentYahrtzieht == null)
            {
                return AddNewYahrtzieht(upsertedYahrtzieht, personId);
            }
            else
            {
                return UpdateSingleYahrtzieht(upsertedYahrtzieht, personId);
            }
        }

        public static void UpsertMultipleYahrtziehts(IEnumerable<Yahrtzieht> upsertedList, int personId)
        {
            foreach (Yahrtzieht CurrYahrtzieht in upsertedList)
            {
                UpsertSingleYahrtzieht(CurrYahrtzieht, personId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<DataCache.Models.Yahrtzieht> ConvertMultipleYahrtziehtsToDbType(List<Yahrtzieht> localTypeYahrList, int personId)
        {
            List<DataCache.Models.Yahrtzieht> dbTypeYahrList = new List<DataCache.Models.Yahrtzieht>();

            foreach (Yahrtzieht CurrYahr in localTypeYahrList)
            {
                dbTypeYahrList.Add((DataCache.Models.Yahrtzieht)YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(CurrYahr, personId));
            }

            return dbTypeYahrList;
        }

        internal static Yahrtzieht ConvertSingleYahrtziehtToDbType(Yahrtzieht localTypeYahr, int personId)
        {
            var dbTypeYahr = DataCache.Models.Yahrtzieht.Createt_yahrtziehts(localTypeYahr.ID, personId,
                                                         localTypeYahr.Date, localTypeYahr.Name);
            dbTypeYahr.Relation = localTypeYahr.Relation;
            return dbTypeYahr;
        }

        internal static List<Yahrtzieht> ConvertMultipleYahrtziehtsToLocalType(List<DataCache.Models.Yahrtzieht> dbTypeYahrList)
        {
            if (dbTypeYahrList ==  null)
            {
                //LOG
                return null;
            }
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();

            foreach (DataCache.Models.Yahrtzieht Curryahr in dbTypeYahrList)
            {
                localTypeYahrList.Add((Yahrtzieht)YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(Curryahr));
            }

            return localTypeYahrList;
        }

        internal static Yahrtzieht ConvertSingleYahrtziehtToLocalType(DataCache.Models.Yahrtzieht dbTypeYahr)
        {
            if (dbTypeYahr == null)
            {
                //LOG
                return null;
            }
            Yahrtzieht localTypeYahr = new Yahrtzieht();
            return localTypeYahr;
        } 

        #endregion
    }
}
