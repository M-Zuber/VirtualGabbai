using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using DataCache;
using System.Data;
using DataCache.Models;

namespace DataAccess
{
    public static class PrivilegeGroupAccess
    {
        #region Read Methods

        #region Local type return

        public static List<PrivilegesGroup> GetAllPrivilegesGroups() => ConvertMultipleDbPrivilegesGroupsToLocalType(LookupAllPrivilegesGroups());

        public static PrivilegesGroup GetPrivilegesGroupById(int id) => ConvertSingleDbPrivilegesGroupToLocalType(LookupPrivilegesGroupById(id));

        public static PrivilegesGroup GetPrivilegesGroupByGroupName(string groupName) => ConvertSingleDbPrivilegesGroupToLocalType(LookupPrivilegesGroupByGroupName(groupName));

        #endregion

        #region Db type return

        private static List<DataCache.Models.PrivilegesGroup> LookupAllPrivilegesGroups()
        {
            try
            {
                return (from privilegeGroup in Cache.CacheData.t_privilege_groups
                        select privilegeGroup).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static PrivilegesGroup LookupPrivilegesGroupById(int id)
        {
            try
            {
                return (from privilegeGroup in Cache.CacheData.t_privilege_groups
                        where privilegeGroup.ID == id
                        select privilegeGroup).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static PrivilegesGroup LookupPrivilegesGroupByGroupName(string groupName)
        {
            try
            {
                return (from privilegeGroup in Cache.CacheData.t_privilege_groups
                        where privilegeGroup.GroupName == groupName
                        select privilegeGroup).First();
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
                PrivilegeAccess.UpsertMultiplePrivileges(newPrivilegesGroup.Privileges);
                DataCache.Models.PrivilegesGroup newDbPrivilegeGroup = DataCache.Models.PrivilegesGroup.Createt_privilege_groups(newPrivilegesGroup.ID);
                newDbPrivilegeGroup.GroupName = newPrivilegesGroup.GroupName;

                foreach (Privilege CurrPrivilege in newPrivilegesGroup.Privileges)
                {
                    newDbPrivilegeGroup.Privileges.Add(PrivilegeAccess.LookupPrivilegeById(CurrPrivilege.ID));
                }

                Cache.CacheData.t_privilege_groups.Add(newDbPrivilegeGroup);
                
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
                PrivilegeAccess.UpsertMultiplePrivileges(updatedPrivilegesGroup.Privileges);

                DataCache.Models.PrivilegesGroup privilegeGroupUpdating = LookupPrivilegesGroupById(updatedPrivilegesGroup.ID);
                privilegeGroupUpdating.ID = updatedPrivilegesGroup.ID;
                privilegeGroupUpdating.GroupName = updatedPrivilegesGroup.GroupName;
                privilegeGroupUpdating.Privileges.Clear();

                foreach (Privilege CurrPrivilege in updatedPrivilegesGroup.Privileges)
                {
                    privilegeGroupUpdating.Privileges.Add(PrivilegeAccess.LookupPrivilegeById(CurrPrivilege.ID));
                }

                Cache.CacheData.t_privilege_groups.Attach(privilegeGroupUpdating);
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
                DataCache.Models.PrivilegesGroup privilegeGroupDeleting = LookupPrivilegesGroupById(deletedPrivilegesGroup.ID);
                Cache.CacheData.t_privilege_groups.Remove(privilegeGroupDeleting);
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
            PrivilegesGroup currentPrivilegesGroup = GetPrivilegesGroupById(upsertedPrivilegesGroup.ID);

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

        internal static List<DataCache.Models.PrivilegesGroup> ConvertMultipleLocalPrivilegesGroupsToDbType(List<PrivilegesGroup> localTypePrivilegesGroupList)
        {
            List<DataCache.Models.PrivilegesGroup> dbTypePrivilegesGroupList = new List<DataCache.Models.PrivilegesGroup>();
            
            foreach (PrivilegesGroup CurrPrivilegesGroup in localTypePrivilegesGroupList)
            {
                dbTypePrivilegesGroupList.Add((DataCache.Models.PrivilegesGroup)ConvertSingleLocalPrivilegesGroupToDbType(CurrPrivilegesGroup));
            }

            return dbTypePrivilegesGroupList;
        }

        internal static PrivilegesGroup ConvertSingleLocalPrivilegesGroupToDbType(PrivilegesGroup localTypePrivilegesGroup)
        {
            DataCache.Models.PrivilegesGroup convertedPrivilegesGroup = DataCache.Models.PrivilegesGroup.Createt_privilege_groups(localTypePrivilegesGroup.ID);
            convertedPrivilegesGroup.GroupName = localTypePrivilegesGroup.GroupName;

            foreach (Privilege CurrPrivilege in localTypePrivilegesGroup.Privileges)
            {
                convertedPrivilegesGroup.Privileges.Add(PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(CurrPrivilege));
            }

            return convertedPrivilegesGroup;
        }

        internal static List<PrivilegesGroup> ConvertMultipleDbPrivilegesGroupsToLocalType(List<DataCache.Models.PrivilegesGroup> dbTypePrivilegesGroupList)
        {
            if (dbTypePrivilegesGroupList == null)
            {
                //LOG
                return null;
            }
            List<PrivilegesGroup> localTypePhoneTypeList = new List<PrivilegesGroup>();

            foreach (DataCache.Models.PrivilegesGroup CurrPrivilegesGroup in dbTypePrivilegesGroupList)
            {
                localTypePhoneTypeList.Add((PrivilegesGroup)ConvertSingleDbPrivilegesGroupToLocalType(CurrPrivilegesGroup));
            }

            return localTypePhoneTypeList;
        }

        internal static PrivilegesGroup ConvertSingleDbPrivilegesGroupToLocalType(DataCache.Models.PrivilegesGroup dbTypePrivilegesGroup)
        {
            if (dbTypePrivilegesGroup == null)
            {
                //LOG
                return null;
            }
            List<Privilege> convertedPrivilegesOfGroup = 
                PrivilegeAccess.ConvertMultipleDbPrivilegesToLocalType(
                                                    dbTypePrivilegesGroup.Privileges.ToList());
            PrivilegesGroup convertedPrivilegeGroup = 
                new PrivilegesGroup();
            return convertedPrivilegeGroup;
        }

        #endregion
    }
}
