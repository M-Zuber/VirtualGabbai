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
            User localTypeUser = new User(1,"user:1","pass:1^^^1","1blah@doit.nike", new PrivilegesGroup(1, "admin", new List<Privilege>()));
            t_users expected = t_users.Createt_users(1,"user:1","pass:1^^^1","1blah@doit.nike",1);
            t_users actual;
            actual = UserAccess.ConvertSingleLocalUserToDbType(localTypeUser);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.email, actual.email);
            Assert.AreEqual(expected.name, actual.name);
            Assert.AreEqual(expected.password, actual.password);
            Assert.AreEqual(expected.privileges_group, actual.privileges_group);
        }

        /// <summary>
        ///A test for ConvertSingleDbUserToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleDbUserToLocalTypeTest()
        {
            t_users dbTypeUser = t_users.Createt_users(1, "user:1", "pass:1^^^1", "1blah@doit.nike", 201);
            dbTypeUser.t_privilege_groups = 
                PrivilegeGroupAccess.ConvertSingleLocalPrivilegesGroupToDbType(
                                                        PrivilegeGroupAccess.GetPrivilegesGroupById(201));
            User expected = new User(1,"user:1","pass:1^^^1","1blah@doit.nike",PrivilegeGroupAccess.GetPrivilegesGroupById(201));
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
            List<User> expected = 
                UserAccess.ConvertMultipleDbUsersToLocalType(UserAccess_Accessor.LookupAllUsers());
            List<User> actual;
            actual = UserAccess.GetAllUsers();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByUserId
        ///</summary>
        [TestMethod()]
        public void GetByUserIdTest()
        {
            int id = 1;
            User expected = 
                UserAccess.ConvertSingleDbUserToLocalType(UserAccess_Accessor.LookupByUserId(id));
            User actual;
            actual = UserAccess.GetByUserId(id);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for GetByUserId
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentUserIdTest()
        {
            int id = 0;
            User expected = null;
            User actual;
            actual = UserAccess.GetByUserId(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByEmailTest()
        {
            MailAddress email = new MailAddress("1blah@doit.nike");
            User expected = UserAccess.GetByUserId(1);
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
            MailAddress email = new MailAddress("blah@blah.blah");
            User expected = null;
            User actual;
            actual = UserAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegeTest()
        {
            Privilege privilege = PrivilegeAccess.GetPrivilegeById(301);
            List<User> expected = 
                UserAccess.ConvertMultipleDbUsersToLocalType(
                            UserAccess_Accessor.LookupByPrivilege(
                                    PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(privilege)));
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilege);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilege
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegeTest()
        {
            Privilege privilegeId = new Privilege(9999, "fooBar");
            List<User> expected = new List<User>();
            List<User> actual;
            actual = UserAccess.GetByPrivilege(privilegeId);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByPrivilegesGroupTest()
        {
            PrivilegesGroup privilegeGroup = PrivilegeGroupAccess.GetPrivilegesGroupById(201);
            List<User> expected = 
                UserAccess.ConvertMultipleDbUsersToLocalType(
                        UserAccess_Accessor.LookupByPrivilegesGroup(
                        PrivilegeGroupAccess.ConvertSingleLocalPrivilegesGroupToDbType(privilegeGroup)));
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroup);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPrivilegesGroupTest()
        {
            PrivilegesGroup privilegeGroupName = new PrivilegesGroup(9999, "blah", new List<Privilege>());
            List<User> expected = new List<User>();
            List<User> actual;
            actual = UserAccess.GetByPrivilegesGroup(privilegeGroupName);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByUserName
        ///</summary>
        [TestMethod()]
        public void GetByUserNameTest()
        {
            string userName = "name:1";
            User expected = UserAccess.GetByUserId(1);
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
            string userName = "blah";
            User expected = null;
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
            List<t_users> expected = (from user in Cache.CacheData.t_users
                                      select user).ToList();
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupAllUsers();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByEmailTest()
        {
            string email = "1blah@doit.nike";
            t_users expected = t_users.Createt_users(1,"name:1","pass:1^^^1","1blah@doit.nike",201);
            t_users actual;
            actual = UserAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.email, actual.email);
            Assert.AreEqual(expected.name, actual.name);
            Assert.AreEqual(expected.password, actual.password);
            Assert.AreEqual(expected.privileges_group, actual.privileges_group);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentEmailTest()
        {
            string email = "bad@mail.com";
            t_users expected = null;
            t_users actual;
            actual = UserAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegeTest()
        {
            t_privileges privilege = PrivilegeAccess.LookupPrivilegeById(301);

            List<t_users> expected = (from user in Cache.CacheData.t_users
                                      where user.t_privilege_groups.t_privileges.Any(lPrivilege => lPrivilege.C_id == privilege.C_id)
                                      select user).ToList();
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilege);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilege
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPrivilegeTest()
        {
            t_privileges privilege = t_privileges.Createt_privileges(9999);
            List<t_users> expected = new List<t_users>();
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilege(privilege);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPrivilegesGroupTest()
        {
            t_privilege_groups privilegeGroup = 
                PrivilegeGroupAccess.ConvertSingleLocalPrivilegesGroupToDbType(
                                            PrivilegeGroupAccess.GetPrivilegesGroupById(201));
            List<t_users> expected = (from user in Cache.CacheData.t_users
                                      where user.privileges_group == privilegeGroup.C_id
                                      select user).ToList();
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroup);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPrivilegesGroup
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPrivilegesGroupTest()
        {
            t_privilege_groups privilegeGroupId = t_privilege_groups.Createt_privilege_groups(9999); 
            List<t_users> expected = new List<t_users>(); 
            List<t_users> actual;
            actual = UserAccess_Accessor.LookupByPrivilegesGroup(privilegeGroupId);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByUserName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByUserNameTest()
        {
            string userName = "name:1";
            t_users expected = (from user in Cache.CacheData.t_users
                                where user.name == userName
                                select user).First();
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
            string userName = "blah";
            t_users expected = null;
            t_users actual;
            actual = UserAccess_Accessor.LookupByUserName(userName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByUserId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByUserIdTest()
        {
            int id = 1;
            t_users expected = (from user in Cache.CacheData.t_users
                                where user.C_id == id
                                select user).First();
            t_users actual;
            actual = UserAccess_Accessor.LookupByUserId(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByUserId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentUserIdTest()
        {
            int id = 0; 
            t_users expected = null;
            t_users actual;
            actual = UserAccess_Accessor.LookupByUserId(id);
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
