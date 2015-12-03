using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class PrivilegeAccess
    {
        #region Read Methods

        #region Local type return

        public static List<Privilege> GetAllPrivileges() => ConvertMultipleDbPrivilegesToLocalType(LookupAllPrivileges());

        public static Privilege GetPrivilegeById(int id) => ConvertSingleDbPrivilegeToLocalType(LookupPrivilegeById(id));

        public static Privilege GetPrivilegeByName(string privilegeName) => ConvertSingleDbPrivilegeToLocalType(LookupPrivilegeByName(privilegeName));

        #endregion

        #region Db type return

        private static List<DataCache.Models.Privilege> LookupAllPrivileges()
        {
            try
            {
                return (from privilege in Cache.CacheData.t_privileges
                        select privilege).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        internal static Privilege LookupPrivilegeById(int id)
        {
            try
            {
                return (from privilege in Cache.CacheData.t_privileges
                        where privilege.ID == id
                        select privilege).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Privilege LookupPrivilegeByName(string privilegeName)
        {
            try
            {
                return (from privilege in Cache.CacheData.t_privileges
                        where privilege.Name == privilegeName
                        select privilege).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        #endregion

        #endregion

        #region Write Methods

        #region Create

        public static Enums.CRUDResults AddNewPrivilege(Privilege newPrivilege)
        {
            try
            {
                DataCache.Models.Privilege newDbPrivilege = ConvertSingleLocalPrivilegeToDbType(newPrivilege);
                Cache.CacheData.t_privileges.Add(newDbPrivilege);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewPrivileges(List<Privilege> newPrivilegeList)
        {
            Enums.CRUDResults result;
            foreach (Privilege newPrivilege in newPrivilegeList)
            {
                result = AddNewPrivilege(newPrivilege);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSinglePrivilege(Privilege updatedPrivilege)
        {
            try
            {
                DataCache.Models.Privilege privilegeUpdating = LookupPrivilegeById(updatedPrivilege.ID);
                privilegeUpdating = ConvertSingleLocalPrivilegeToDbType(updatedPrivilege);
                Cache.CacheData.t_privileges.Attach(privilegeUpdating);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultiplePrivileges(List<Privilege> updatedPrivilegeList)
        {
            Enums.CRUDResults result;
            foreach (Privilege updatedPrivilege in updatedPrivilegeList)
            {
                result = UpdateSinglePrivilege(updatedPrivilege);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSinglePrivilege(Privilege deletedPrivilege)
        {
            try
            {
                DataCache.Models.Privilege deletedDbPrivilege = Cache.CacheData.t_privileges.First(
                    privilege => privilege.ID == deletedPrivilege.ID);
                Cache.CacheData.t_privileges.Remove(deletedDbPrivilege);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultiplePrivileges(List<Privilege> deletedPrivilegeList)
        {
            Enums.CRUDResults result;
            foreach (Privilege deletedPrivilege in deletedPrivilegeList)
            {
                result = DeleteSinglePrivilege(deletedPrivilege);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSinglePrivilege(Privilege upsertedPrivilege)
        {
            Privilege currentPrivilege = GetPrivilegeById(upsertedPrivilege.ID);

            if (currentPrivilege == null)
            {
                return AddNewPrivilege(upsertedPrivilege);
            }
            else
            {
                return UpdateSinglePrivilege(upsertedPrivilege);
            }
        }

        public static void UpsertMultiplePrivileges(IEnumerable<Privilege> upsertedList)
        {
            foreach (Privilege CurrPrivilege in upsertedList)
            {
                UpsertSinglePrivilege(CurrPrivilege);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<DataCache.Models.Privilege> ConvertMultipleLocalPrivilegesToDbType(List<Privilege> localTypePrivilegeList)
        {
            List<DataCache.Models.Privilege> dbTypePrivilegeList = new List<DataCache.Models.Privilege>();
            
            foreach (Privilege CurrPrivilege in localTypePrivilegeList)
            {
                dbTypePrivilegeList.Add((DataCache.Models.Privilege)ConvertSingleLocalPrivilegeToDbType(CurrPrivilege));
            }

            return dbTypePrivilegeList;
        }

        internal static Privilege ConvertSingleLocalPrivilegeToDbType(Privilege localTypePrivilege)
        {
            DataCache.Models.Privilege convertedPrivilege = DataCache.Models.Privilege.Createt_privileges(localTypePrivilege.ID);
            convertedPrivilege.Name = localTypePrivilege.Name;
            return convertedPrivilege;
        }

        internal static List<Privilege> ConvertMultipleDbPrivilegesToLocalType(List<DataCache.Models.Privilege> dbTypePrivilegeList)
        {
            if (dbTypePrivilegeList == null)
            {
                //LOG
                return null;
            }
            List<Privilege> localTypePhoneTypeList = new List<Privilege>();

            foreach (DataCache.Models.Privilege CurrPrivilege in dbTypePrivilegeList)
            {
                localTypePhoneTypeList.Add((Privilege)ConvertSingleDbPrivilegeToLocalType(CurrPrivilege));
            }

            return localTypePhoneTypeList;
        }

        internal static Privilege ConvertSingleDbPrivilegeToLocalType(DataCache.Models.Privilege dbTypePrivilege)
        {
            if (dbTypePrivilege == null)
            {
                //LOG
                return null;
            }

            return new Privilege();
        }

        #endregion
    }
}
