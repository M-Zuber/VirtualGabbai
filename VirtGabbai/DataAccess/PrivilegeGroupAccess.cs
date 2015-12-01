using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using Framework;
using DataCache;
using System.Data;

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

        private static List<t_privilege_groups> LookupAllPrivilegesGroups()
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

        private static t_privilege_groups LookupPrivilegesGroupById(int id)
        {
            try
            {
                return (from privilegeGroup in Cache.CacheData.t_privilege_groups
                        where privilegeGroup.C_id == id
                        select privilegeGroup).First();
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
                return (from privilegeGroup in Cache.CacheData.t_privilege_groups
                        where privilegeGroup.group_name == groupName
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
                t_privilege_groups newDbPrivilegeGroup = t_privilege_groups.Createt_privilege_groups(newPrivilegesGroup._Id);
                newDbPrivilegeGroup.group_name = newPrivilegesGroup.GroupName;

                foreach (Privilege CurrPrivilege in newPrivilegesGroup.Privileges)
                {
                    newDbPrivilegeGroup.t_privileges.Add(PrivilegeAccess.LookupPrivilegeById(CurrPrivilege._Id));
                }

                Cache.CacheData.t_privilege_groups.AddObject(newDbPrivilegeGroup);
                
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
                
                t_privilege_groups privilegeGroupUpdating = LookupPrivilegesGroupById(updatedPrivilegesGroup._Id);
                privilegeGroupUpdating.C_id = updatedPrivilegesGroup._Id;
                privilegeGroupUpdating.group_name = updatedPrivilegesGroup.GroupName;
                privilegeGroupUpdating.t_privileges.Clear();

                foreach (Privilege CurrPrivilege in updatedPrivilegesGroup.Privileges)
                {
                    privilegeGroupUpdating.t_privileges.Add(PrivilegeAccess.LookupPrivilegeById(CurrPrivilege._Id));
                }

                Cache.CacheData.t_privilege_groups.ApplyCurrentValues(privilegeGroupUpdating);
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
                t_privilege_groups privilegeGroupDeleting = LookupPrivilegesGroupById(deletedPrivilegesGroup._Id);
                Cache.CacheData.t_privilege_groups.DeleteObject(privilegeGroupDeleting);
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

        internal static List<t_privilege_groups> ConvertMultipleLocalPrivilegesGroupsToDbType(List<PrivilegesGroup> localTypePrivilegesGroupList)
        {
            List<t_privilege_groups> dbTypePrivilegesGroupList = new List<t_privilege_groups>();
            
            foreach (PrivilegesGroup CurrPrivilegesGroup in localTypePrivilegesGroupList)
            {
                dbTypePrivilegesGroupList.Add(ConvertSingleLocalPrivilegesGroupToDbType(CurrPrivilegesGroup));
            }

            return dbTypePrivilegesGroupList;
        }

        internal static t_privilege_groups ConvertSingleLocalPrivilegesGroupToDbType(PrivilegesGroup localTypePrivilegesGroup)
        {
            t_privilege_groups convertedPrivilegesGroup = t_privilege_groups.Createt_privilege_groups(localTypePrivilegesGroup._Id);
            convertedPrivilegesGroup.group_name = localTypePrivilegesGroup.GroupName;

            foreach (Privilege CurrPrivilege in localTypePrivilegesGroup.Privileges)
            {
                convertedPrivilegesGroup.t_privileges.Add(PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(CurrPrivilege));
            }

            return convertedPrivilegesGroup;
        }

        internal static List<PrivilegesGroup> ConvertMultipleDbPrivilegesGroupsToLocalType(List<t_privilege_groups> dbTypePrivilegesGroupList)
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

        internal static PrivilegesGroup ConvertSingleDbPrivilegesGroupToLocalType(t_privilege_groups dbTypePrivilegesGroup)
        {
            if (dbTypePrivilegesGroup == null)
            {
                //LOG
                return null;
            }
            List<Privilege> convertedPrivilegesOfGroup = 
                PrivilegeAccess.ConvertMultipleDbPrivilegesToLocalType(
                                                    dbTypePrivilegesGroup.t_privileges.ToList());
            PrivilegesGroup convertedPrivilegeGroup = 
                new PrivilegesGroup(dbTypePrivilegesGroup.C_id, dbTypePrivilegesGroup.group_name,
                                            convertedPrivilegesOfGroup);
            return convertedPrivilegeGroup;
        }

        #endregion
    }
}
