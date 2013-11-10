using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;

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
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.person_id == personId &&
                              CurrYahr.date == yahrDate &&
                              CurrYahr.deceaseds_name == personName
                        select CurrYahr).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_yahrtziehts> LookupYahrtziehtsByDate(int personId, DateTime yahrDate)
        {
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.date == yahrDate
                        select CurrYahr).ToList<t_yahrtziehts>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_yahrtziehts> LookupAllYahrtziehts(int personId)
        {
            try
            {
                return (from CurrPerson in Cache.CacheData.t_people
                        where CurrPerson.C_id == personId
                        select CurrPerson).First().t_yahrtziehts.ToList<t_yahrtziehts>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_yahrtziehts LookupYahrtziehtById(int id)
        {
            try
            {
                return (from CurrYahr in Cache.CacheData.t_yahrtziehts
                        where CurrYahr.C_id == id
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
                t_yahrtziehts yahrToAdd = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(newYahr, personId);
                Cache.CacheData.t_yahrtziehts.AddObject(yahrToAdd);
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
                t_yahrtziehts yahrUpdating = YahrtziehtAccess.LookupYahrtziehtById(updatedYahr._Id);
                yahrUpdating = YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(updatedYahr, personId);
                Cache.CacheData.t_yahrtziehts.ApplyCurrentValues(yahrUpdating);
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
                t_yahrtziehts yahrDeleting = Cache.CacheData.t_yahrtziehts.First(yahr => yahr.C_id == deletedYahr._Id);
                Cache.CacheData.t_yahrtziehts.DeleteObject(yahrDeleting);
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
            Yahrtzieht currentYahrtzieht = GetYahrtziehtById(upsertedYahrtzieht._Id);

            if (currentYahrtzieht == null)
            {
                return AddNewYahrtzieht(upsertedYahrtzieht, personId);
            }
            else
            {
                return UpdateSingleYahrtzieht(upsertedYahrtzieht, personId);
            }
        }

        public static void UpsertMultipleYahrtziehts(List<Yahrtzieht> upsertedList, int personId)
        {
            foreach (Yahrtzieht CurrYahrtzieht in upsertedList)
            {
                UpsertSingleYahrtzieht(CurrYahrtzieht, personId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_yahrtziehts> ConvertMultipleYahrtziehtsToDbType(List<Yahrtzieht> localTypeYahrList, int personId)
        {
            List<t_yahrtziehts> dbTypeYahrList = new List<t_yahrtziehts>();

            foreach (Yahrtzieht CurrYahr in localTypeYahrList)
            {
                dbTypeYahrList.Add(YahrtziehtAccess.ConvertSingleYahrtziehtToDbType(CurrYahr, personId));
            }

            return dbTypeYahrList;
        }

        internal static t_yahrtziehts ConvertSingleYahrtziehtToDbType(Yahrtzieht localTypeYahr, int personId)
        {
            var dbTypeYahr = t_yahrtziehts.Createt_yahrtziehts(localTypeYahr._Id, personId,
                                                         localTypeYahr.Date, localTypeYahr.Name);
            dbTypeYahr.relation = localTypeYahr.Relation;
            return dbTypeYahr;
        }

        internal static List<Yahrtzieht> ConvertMultipleYahrtziehtsToLocalType(List<t_yahrtziehts> dbTypeYahrList)
        {
            if (dbTypeYahrList ==  null)
            {
                //LOG
                return null;
            }
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();

            foreach (t_yahrtziehts Curryahr in dbTypeYahrList)
            {
                localTypeYahrList.Add(YahrtziehtAccess.ConvertSingleYahrtziehtToLocalType(Curryahr));
            }

            return localTypeYahrList;
        }

        internal static Yahrtzieht ConvertSingleYahrtziehtToLocalType(t_yahrtziehts dbTypeYahr)
        {
            if (dbTypeYahr == null)
            {
                //LOG
                return null;
            }
            Yahrtzieht localTypeYahr = new Yahrtzieht(dbTypeYahr.C_id, dbTypeYahr.date, dbTypeYahr.deceaseds_name, dbTypeYahr.relation);
            return localTypeYahr;
        } 

        #endregion
    }
}
