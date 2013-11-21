using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using Framework;
using DataCache;

namespace DataAccess
{
    public static class PrivilegeGroupAccess
    {
        #region Read Methods

        public static List<PrivilegesGroup> GetAllPrivilegesGroups()
        {
            return null;
        }

        public static PrivilegesGroup GetPrivilegesGroupById(int id)
        {
            return null;
        }

        public static PrivilegesGroup GetPrivilegesGroupByGroupName(string groupName)
        {
            return null;
        }

        #region Local type return

        #endregion

        #region Db type return

        private static List<t_privilege_groups> LookupAllPrivilegesGroups()
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

        private static t_privilege_groups LookupPrivilegesGroupById(int id)
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

        private static t_privilege_groups LookupPrivilegesGroupByGroupName(string groupName)
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

        public static Enums.CRUDResults AddNewPrivilegesGroup(PrivilegesGroup newPrivilegesGroup)
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

        public static void AddMultipleNewPrivilegesGroups(List<PrivilegesGroup> newPrivilegesGroupList)
        {
            Enums.CRUDResults result;
            foreach (PrivilegesGroup newPrivilegesGroup in newPrivilegesGroupList)
            {
                result = AddNewPrivilegesGroup(newPrivilegesGroup);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSinglePrivilegesGroup(PrivilegesGroup updatedPrivilegesGroup)
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

        public static void UpdateMultiplePrivilegesGroups(List<PrivilegesGroup> updatedPrivilegesGroupList)
        {
            Enums.CRUDResults result;
            foreach (PrivilegesGroup updatedPrivilegesGroup in updatedPrivilegesGroupList)
            {
                result = UpdateSinglePrivilegesGroup(updatedPrivilegesGroup);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSinglePrivilegesGroup(PrivilegesGroup deletedPrivilegesGroup)
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

        public static void DeleteMultiplePrivilegesGroups(List<PrivilegesGroup> deletedPrivilegesGroupList)
        {
            Enums.CRUDResults result;
            foreach (PrivilegesGroup deletedPrivilegesGroup in deletedPrivilegesGroupList)
            {
                result = DeleteSinglePrivilegesGroup(deletedPrivilegesGroup);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSinglePrivilegesGroup(PrivilegesGroup upsertedPrivilegesGroup)
        {
            PrivilegesGroup currentPrivilegesGroup = GetPrivilegesGroupById(upsertedPrivilegesGroup._Id);

            if (currentPrivilegesGroup == null)
            {
                return AddNewPrivilegesGroup(upsertedPrivilegesGroup);
            }
            else
            {
                return UpdateSinglePrivilegesGroup(upsertedPrivilegesGroup);
            }
        }

        public static void UpsertMultiplePrivilegesGroups(List<PrivilegesGroup> upsertedList)
        {
            foreach (PrivilegesGroup CurrPrivilegesGroup in upsertedList)
            {
                UpsertSinglePrivilegesGroup(CurrPrivilegesGroup);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_privilege_groups> ConvertMultipleLocalPrivilegesGroupsToDbType(List<PrivilegesGroup> localTypePrivilegesGroupList)
        {
            List<t_privilege_groups> dbTypePrivilegesGroupList = new List<t_privilege_groups>();
            
            foreach (PrivilegesGroup CurrPrivilegesGroup in localTypePrivilegesGroupList)
            {
                dbTypePrivilegesGroupList.Add(ConvertSingleLocalPrivilegesGroupToDbType(CurrPrivilegesGroup));
            }

            return dbTypePrivilegesGroupList;
        }

        private static t_privilege_groups ConvertSingleLocalPrivilegesGroupToDbType(PrivilegesGroup localTypePrivilegesGroup)
        {
            return null;
        }

        private static List<PrivilegesGroup> ConvertMultipleDbPrivilegesGroupsToLocalType(List<t_privilege_groups> dbTypePrivilegesGroupList)
        {
            if (dbTypePrivilegesGroupList == null)
            {
                //LOG
                return null;
            }
            List<PrivilegesGroup> localTypePhoneTypeList = new List<PrivilegesGroup>();

            foreach (t_privilege_groups CurrPrivilegesGroup in dbTypePrivilegesGroupList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbPrivilegesGroupToLocalType(CurrPrivilegesGroup));
            }

            return localTypePhoneTypeList;
        }

        private static PrivilegesGroup ConvertSingleDbPrivilegesGroupToLocalType(t_privilege_groups dbTypePrivilegesGroup)
        {
            if (dbTypePrivilegesGroup == null)
            {
                //LOG
                return null;
            }

            return null;
        }

        #endregion
    }
}
