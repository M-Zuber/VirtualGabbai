using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCache;
using LocalTypes;
using Framework;

namespace DataAccess
{
    public static class PrivilegeAccess
    {
        #region Read Methods

        #region Local type return

        public static List<Privilege> GetAllPrivileges()
        {
            return null;
        }

        public static Privilege GetPrivilegeById(int id)
        {
            return null;
        }

        public static Privilege GetPrivilegeByName(string privilegeName)
        {
            return null;
        }

        #endregion

        #region Db type return

        private static List<t_privileges> LookupAllPrivileges()
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_privileges LookupPrivilegeById(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_privileges LookupPrivilegeByName(string privilegeName)
        {
            try
            {
                return null;
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
            Privilege currentPrivilege = GetPrivilegeById(upsertedPrivilege._Id);

            if (currentPrivilege == null)
            {
                return AddNewPrivilege(upsertedPrivilege);
            }
            else
            {
                return UpdateSinglePrivilege(upsertedPrivilege);
            }
        }

        public static void UpsertMultiplePrivileges(List<Privilege> upsertedList)
        {
            foreach (Privilege CurrPrivilege in upsertedList)
            {
                UpsertSinglePrivilege(CurrPrivilege);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_privileges> ConvertMultipleLocalPrivilegesToDbType(List<Privilege> localTypePrivilegeList)
        {
            List<t_privileges> dbTypePrivilegeList = new List<t_privileges>();
            
            foreach (Privilege CurrPrivilege in localTypePrivilegeList)
            {
                dbTypePrivilegeList.Add(ConvertSingleLocalPrivilegeToDbType(CurrPrivilege));
            }

            return dbTypePrivilegeList;
        }

        internal static t_privileges ConvertSingleLocalPrivilegeToDbType(Privilege localTypePrivilege)
        {
            return null;
        }

        internal static List<Privilege> ConvertMultipleDbPrivilegesToLocalType(List<t_privileges> dbTypePrivilegeList)
        {
            if (dbTypePrivilegeList == null)
            {
                //LOG
                return null;
            }
            List<Privilege> localTypePhoneTypeList = new List<Privilege>();

            foreach (t_privileges CurrPrivilege in dbTypePrivilegeList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbPrivilegeToLocalType(CurrPrivilege));
            }

            return localTypePhoneTypeList;
        }

        internal static Privilege ConvertSingleDbPrivilegeToLocalType(t_privileges dbTypePrivilege)
        {
            if (dbTypePrivilege == null)
            {
                //LOG
                return null;
            }

            return null;
        }

        #endregion
    }
}
