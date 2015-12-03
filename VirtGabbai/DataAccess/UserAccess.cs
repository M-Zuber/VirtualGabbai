using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using System.Net.Mail;
using DataCache.Models;

namespace DataAccess
{
    public static class UserAccess
    {
        #region Read Methods

        #region Local type return

        public static List<User> GetAllUsers() => ConvertMultipleDbUsersToLocalType(LookupAllUsers());

        public static List<User> GetByPrivilegesGroup(PrivilegesGroup privilegeGroup) => ConvertMultipleDbUsersToLocalType(LookupByPrivilegesGroup(
            PrivilegeGroupAccess.ConvertSingleLocalPrivilegesGroupToDbType(privilegeGroup)));

        public static List<User> GetByPrivilege(Privilege privilege) => ConvertMultipleDbUsersToLocalType(LookupByPrivilege(
                        PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(privilege)));

        public static User GetByUserName(string userName) => ConvertSingleDbUserToLocalType(LookupByUserName(userName));

        public static User GetByEmail(MailAddress email) => ConvertSingleDbUserToLocalType(LookupByEmail(email.Address));

        public static User GetByUserId(int id) => ConvertSingleDbUserToLocalType(LookupByUserId(id));

        #endregion

        #region Db type return

        private static List<DataCache.Models.User> LookupAllUsers()
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

        private static List<DataCache.Models.User> LookupByPrivilegesGroup(DataCache.Models.PrivilegesGroup privilegeGroup)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.PrivilegesGroupID == privilegeGroup.ID
                        select user).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.User> LookupByPrivilege(DataCache.Models.Privilege privilege)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.PrivilegeGroup.Privileges.Any(lPrivilege => lPrivilege.ID == privilege.ID)
                        select user).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static User LookupByUserName(string userName)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.UserName == userName
                        select user).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static User LookupByEmail(string email)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.Email == email
                        select user).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static User LookupByUserId(int id)
        {
            try
            {
                return (from user in Cache.CacheData.t_users
                        where user.ID == id
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
                PrivilegeGroupAccess.UpsertSinglePrivilegesGroup(newUser.PrivilegeGroup);

                DataCache.Models.User newDbUser = ConvertSingleLocalUserToDbType(newUser);
                Cache.CacheData.t_users.Add(newDbUser);
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
                PrivilegeGroupAccess.UpsertSinglePrivilegesGroup(updatedUser.PrivilegeGroup);
                DataCache.Models.User userUpdating = LookupByUserId(updatedUser.ID);
                userUpdating = ConvertSingleLocalUserToDbType(updatedUser);
                Cache.CacheData.t_users.Attach(userUpdating);
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
                DataCache.Models.User userDeleting =
                    Cache.CacheData.t_users.First(user => user.ID == deletedUser.ID);
                Cache.CacheData.t_users.Remove(userDeleting);
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
            User currentUser = GetByUserId(upsertedUser.ID);

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

        internal static List<DataCache.Models.User> ConvertMultipleLocalUsersToDbType(List<User> localTypeUserList)
        {
            List<DataCache.Models.User> dbTypeUserList = new List<DataCache.Models.User>();

            foreach (User CurrUser in localTypeUserList)
            {
                dbTypeUserList.Add((DataCache.Models.User)ConvertSingleLocalUserToDbType(CurrUser));
            }

            return dbTypeUserList;
        }

        internal static User ConvertSingleLocalUserToDbType(User localTypeUser) => DataCache.Models.User.Createt_users(localTypeUser.ID, localTypeUser.UserName,
            localTypeUser.Password, localTypeUser.Email, localTypeUser.PrivilegeGroup.ID);

        internal static List<User> ConvertMultipleDbUsersToLocalType(List<DataCache.Models.User> dbTypeUserList)
        {
            if (dbTypeUserList == null)
            {
                //LOG
                return null;
            }
            List<User> localTypePhoneTypeList = new List<User>();

            foreach (DataCache.Models.User CurrUser in dbTypeUserList)
            {
                localTypePhoneTypeList.Add((User)ConvertSingleDbUserToLocalType(CurrUser));
            }

            return localTypePhoneTypeList;
        }

        internal static User ConvertSingleDbUserToLocalType(DataCache.Models.User dbTypeUser)
        {
            if (dbTypeUser == null)
            {
                //LOG
                return null;
            }

            PrivilegesGroup userGroup = null;

            try
            {
                userGroup = PrivilegeGroupAccess.ConvertSingleDbPrivilegesGroupToLocalType(dbTypeUser.PrivilegeGroup);
            }
            catch{/*LOG*/}

            return new User();
        }

        #endregion
    }
}
