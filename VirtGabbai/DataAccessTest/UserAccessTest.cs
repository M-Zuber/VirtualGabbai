using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LocalTypes;
using System.Collections.Generic;
using DataCache;
using Framework;
using System.Net.Mail;
using System.Linq;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for UserAccessTest and is intended
    ///to contain all UserAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserAccessTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            for (int privilegeIndex = 301; privilegeIndex < 306; privilegeIndex++)
            {
                if (!Cache.CacheData.t_privileges.Any(privilege => privilege.C_id == privilegeIndex))
                {
                    t_privileges newPrivilege = t_privileges.Createt_privileges(privilegeIndex);
                    newPrivilege.privilege_name = "privilege:" + privilegeIndex;
                    Cache.CacheData.t_privileges.AddObject(newPrivilege);
                }
            }
            Cache.CacheData.SaveChanges();

            if (!Cache.CacheData.t_privilege_groups.Any(group => group.C_id == 201))
            {
                t_privilege_groups newGroup = t_privilege_groups.Createt_privilege_groups(201);
                newGroup.group_name = "group:" + 201;

                List<t_privileges> allPrivileges = (from privilege in Cache.CacheData.t_privileges
                                                    where privilege.C_id == 301 ||
                                                            privilege.C_id == 302 ||
                                                            privilege.C_id == 303 ||
                                                            privilege.C_id == 304 ||
                                                            privilege.C_id == 305
                                                    select privilege).ToList();
                foreach (t_privileges CurrPrivilege in allPrivileges)
                {
                    newGroup.t_privileges.Add(CurrPrivilege);
                }
                Cache.CacheData.t_privilege_groups.AddObject(newGroup);
            }

            for (int userIndex = 1; userIndex < 11; userIndex++)
            {
                if (!Cache.CacheData.t_users.Any(user => user.C_id == userIndex))
                {
                    t_users newUser = 
                        t_users.Createt_users(userIndex, "name:" + userIndex,
                                        "pass:" + userIndex + "^^^" + userIndex,
                                        userIndex + "blah@doit.nike", 201);
                    Cache.CacheData.t_users.AddObject(newUser);
                }
                
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            Cache.CacheData.clear_database();
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Convert Tests

        /// <summary>
        ///A test for ConvertSingleLocalUserToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleLocalUserToDbTypeTest()
        {
            User localTypeUser = null; // TODO: Initialize to an appropriate value
            t_users expected = null; // TODO: Initialize to an appropriate value
            t_users actual;
            actual = UserAccess.ConvertSingleLocalUserToDbType(localTypeUser);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbUserToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleDbUserToLocalTypeTest()
        {
            t_users dbTypeUser = null; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = UserAccess.ConvertSingleDbUserToLocalType(dbTypeUser);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Add Tests

        /// <summary>
        ///A test for AddNewUser
        ///</summary>
        [TestMethod()]
        public void AddNewUserTest()
        {
            User newUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.AddNewUser(newUser);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddMultipleNewUsers
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewUsersTest()
        {
            List<User> newUserList = null; // TODO: Initialize to an appropriate value
            UserAccess.AddMultipleNewUsers(newUserList);
        }
        
        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultipleUsers
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleUsersTest()
        {
            List<User> deletedUserList = null; // TODO: Initialize to an appropriate value
            UserAccess.DeleteMultipleUsers(deletedUserList);
        }

        /// <summary>
        ///A test for DeleteSingleUser
        ///</summary>
        [TestMethod()]
        public void DeleteSingleUserTest()
        {
            User deletedUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.DeleteSingleUser(deletedUser);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteSingleUser
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExsistentUserTest()
        {
            User deletedUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.DeleteSingleUser(deletedUser);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllUsers
        ///</summary>
        [TestMethod()]
        public void GetAllUsersTest()
        {
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetAllUsers();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByEmailTest()
        {
            MailAddress email = null; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = UserAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByNonExsitentEmailTest()
        {
            MailAddress email = null; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = UserAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegeIdTest()
        {
            int privilegeId = 0; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilegeId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegeIdTest()
        {
            int privilegeId = 0; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilegeId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegeNameTest()
        {
            string privilegeName = string.Empty; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilegeName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegeNameTest()
        {
            string privilegeName = string.Empty; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilegeName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegesGroupNameTest()
        {
            string privilegeGroupName = string.Empty; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegesGroupNameTest()
        {
            string privilegeGroupName = string.Empty; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegesGroupIdTest()
        {
            int privilegeGroupId = 0; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroupId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegesGroupIdTest()
        {
            int privilegeGroupId = 0; // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroupId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByUserName
        ///</summary>
        [TestMethod()]
        public void GetByUserNameTest()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = UserAccess.GetByUserName(userName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByUserName
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentUserNameTest()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = UserAccess.GetByUserName(userName);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllUsers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllUsersTest()
        {
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupAllUsers();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByEmailTest()
        {
            string email = string.Empty; // TODO: Initialize to an appropriate value
            t_users expected = null; // TODO: Initialize to an appropriate value
            t_users actual;
            actual = UserAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentEmailTest()
        {
            string email = string.Empty; // TODO: Initialize to an appropriate value
            t_users expected = null; // TODO: Initialize to an appropriate value
            t_users actual;
            actual = UserAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegeNameTest()
        {
            string privilegeName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilegeName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPrivilegeNameTest()
        {
            string privilegeName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilegeName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegeTestId()
        {
            int privilegeId = 0; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilegeId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPrivilegeTestId()
        {
            int privilegeId = 0; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilegeId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegesGroupIdTest()
        {
            int privilegeGroupId = 0; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroupId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupBNonExsistentyPrivilegesGroupIdTest()
        {
            int privilegeGroupId = 0; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroupId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegesGroupNameTest()
        {
            string privilegeGroupName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPrivilegesGroupNameTest()
        {
            string privilegeGroupName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_users> expected = null; // TODO: Initialize to an appropriate value
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByUserName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByUserNameTest()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            t_users expected = null; // TODO: Initialize to an appropriate value
            t_users actual;
            actual = UserAccess_Accessor.LookupByUserName(userName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByUserName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentUserNameTest()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            t_users expected = null; // TODO: Initialize to an appropriate value
            t_users actual;
            actual = UserAccess_Accessor.LookupByUserName(userName);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultipleUsers
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleUsersTest()
        {
            List<User> updatedUserList = null; // TODO: Initialize to an appropriate value
            UserAccess.UpdateMultipleUsers(updatedUserList);
        }

        /// <summary>
        ///A test for UpdateSingleUser
        ///</summary>
        [TestMethod()]
        public void UpdateSingleUserTest()
        {
            User updatedUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.UpdateSingleUser(updatedUser);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSingleUser
        ///</summary>
        [TestMethod()]
        public void UpsertAddSingleUserTest()
        {
            User upsertedUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.UpsertSingleUser(upsertedUser);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpsertSingleUser
        ///</summary>
        [TestMethod()]
        public void UpsertMultipleSingleUserTest()
        {
            User upsertedUser = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = UserAccess.UpsertSingleUser(upsertedUser);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
