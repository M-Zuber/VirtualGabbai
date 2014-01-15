using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using System.Net.Mail;

namespace DataAccess
{
    public static class UserAccess
    {
        #region Read Methods

        #region Local type return

        public static List<User> GetAllUsers()
        {
            return ConvertMultipleDbUsersToLocalType(LookupAllUsers());
        }

        public static List<User> GetByPrivilegesGroup(PrivilegesGroup privilegeGroup)
        {
            return ConvertMultipleDbUsersToLocalType(LookupByPrivilegesGroup(
                        PrivilegeGroupAccess.ConvertSingleLocalPrivilegesGroupToDbType(privilegeGroup)));
        }

        public static List<User> GetByPrivilege(Privilege privilege)
        {
            return ConvertMultipleDbUsersToLocalType(LookupByPrivilege(
                                    PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(privilege)));
        }

        public static User GetByUserName(string userName)
        {
            return ConvertSingleDbUserToLocalType(LookupByUserName(userName));
        }

        public static User GetByEmail(MailAddress email)
        {
            return ConvertSingleDbUserToLocalType(LookupByEmail(email.Address));
        }

        public static User GetByUserId(int id)
        {
            return ConvertSingleDbUserToLocalType(LookupByUserId(id));
        }

        #endregion

        #region Db type return

        private static List<t_users> LookupAllUsers()
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        select user).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_users> LookupByPrivilegesGroup(t_privilege_groups privilegeGroup)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.privileges_group == privilegeGroup.C_id
                        select user).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_users> LookupByPrivilege(t_privileges privilege)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.t_privilege_groups.t_privileges.Any(lPrivilege => lPrivilege.C_id == privilege.C_id)
                        select user).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_users LookupByUserName(string userName)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.name == userName
                        select user).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_users LookupByEmail(string email)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.email == email
                        select user).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_users LookupByUserId(int id)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.C_id == id
                        select user).First();
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

        public static Enums.CRUDResults AddNewUser(User newUser)
        {
            try
            {
                PrivilegeGroupAccess.UpsertSinglePrivilegesGroup(newUser.UserGroup);

                t_users newDbUser = ConvertSingleLocalUserToDbType(newUser);
                Cache.CacheData.t_users.AddObject(newDbUser);
                Cache.CacheData.SaveChanges();

                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewUsers(List<User> newUserList)
        {
            Enums.CRUDResults result;
            foreach (User newUser in newUserList)
            {
                result = AddNewUser(newUser);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSingleUser(User updatedUser)
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

        public static void UpdateMultipleUsers(List<User> updatedUserList)
        {
            Enums.CRUDResults result;
            foreach (User updatedUser in updatedUserList)
            {
                result = UpdateSingleUser(updatedUser);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSingleUser(User deletedUser)
        {
            try
            {
                t_users userDeleting =
                    Cache.CacheData.t_users.First(user => user.C_id == deletedUser._Id);
                Cache.CacheData.t_users.DeleteObject(userDeleting);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultipleUsers(List<User> deletedUserList)
        {
            Enums.CRUDResults result;
            foreach (User deletedUser in deletedUserList)
            {
                result = DeleteSingleUser(deletedUser);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSingleUser(User upsertedUser)
        {
            User currentUser = null;//GetUserById(upsertedUser._Id);

            if (currentUser == null)
            {
                return AddNewUser(upsertedUser);
            }
            else
            {
                return UpdateSingleUser(upsertedUser);
            }
        }

        public static void UpsertMultipleUsers(List<User> upsertedList)
        {
            foreach (User CurrUser in upsertedList)
            {
                UpsertSingleUser(CurrUser);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_users> ConvertMultipleLocalUsersToDbType(List<User> localTypeUserList)
        {
            List<t_users> dbTypeUserList = new List<t_users>();

            foreach (User CurrUser in localTypeUserList)
            {
                dbTypeUserList.Add(ConvertSingleLocalUserToDbType(CurrUser));
            }

            return dbTypeUserList;
        }

        internal static t_users ConvertSingleLocalUserToDbType(User localTypeUser)
        {
            return t_users.Createt_users(localTypeUser._Id, localTypeUser.UserName,
                        localTypeUser.Password, localTypeUser.Email.Address, localTypeUser.UserGroup._Id);
        }

        internal static List<User> ConvertMultipleDbUsersToLocalType(List<t_users> dbTypeUserList)
        {
            if (dbTypeUserList == null)
            {
                //LOG
                return null;
            }
            List<User> localTypePhoneTypeList = new List<User>();

            foreach (t_users CurrUser in dbTypeUserList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbUserToLocalType(CurrUser));
            }

            return localTypePhoneTypeList;
        }

        internal static User ConvertSingleDbUserToLocalType(t_users dbTypeUser)
        {
            if (dbTypeUser == null)
            {
                //LOG
                return null;
            }
            
            PrivilegesGroup userGroup = null;

            try
            {
                userGroup = PrivilegeGroupAccess.ConvertSingleDbPrivilegesGroupToLocalType(dbTypeUser.t_privilege_groups);
            }
            catch{/*LOG*/}

            return new User(dbTypeUser.C_id, dbTypeUser.name, dbTypeUser.password,
                                                    dbTypeUser.email, userGroup);
        }

        #endregion
    }
}
