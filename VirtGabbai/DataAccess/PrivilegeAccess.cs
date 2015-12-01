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

        public static List<Privilege> GetAllPrivileges() => ConvertMultipleDbPrivilegesToLocalType(LookupAllPrivileges());

        public static Privilege GetPrivilegeById(int id) => ConvertSingleDbPrivilegeToLocalType(LookupPrivilegeById(id));

        public static Privilege GetPrivilegeByName(string privilegeName) => ConvertSingleDbPrivilegeToLocalType(LookupPrivilegeByName(privilegeName));

        #endregion

        #region Db type return

        private static List<t_privileges> LookupAllPrivileges()
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

        internal static t_privileges LookupPrivilegeById(int id)
        {
            try
            {
                return (from privilege in Cache.CacheData.t_privileges
                        where privilege.C_id == id
                        select privilege).First();
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
                return (from privilege in Cache.CacheData.t_privileges
                        where privilege.privilege_name == privilegeName
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
                t_privileges newDbPrivilege = ConvertSingleLocalPrivilegeToDbType(newPrivilege);
                Cache.CacheData.t_privileges.AddObject(newDbPrivilege);
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
                t_privileges privilegeUpdating = LookupPrivilegeById(updatedPrivilege._Id);
                privilegeUpdating = ConvertSingleLocalPrivilegeToDbType(updatedPrivilege);
                Cache.CacheData.t_privileges.ApplyCurrentValues(privilegeUpdating);
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
                t_privileges deletedDbPrivilege = Cache.CacheData.t_privileges.First(
                    privilege => privilege.C_id == deletedPrivilege._Id);
                Cache.CacheData.t_privileges.DeleteObject(deletedDbPrivilege);
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
            t_privileges convertedPrivilege = t_privileges.Createt_privileges(localTypePrivilege._Id);
            convertedPrivilege.privilege_name = localTypePrivilege.PrivilegeName;
            return convertedPrivilege;
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

            return new Privilege(dbTypePrivilege.C_id, dbTypePrivilege.privilege_name);
        }

        #endregion
    }
}
