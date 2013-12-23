using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LocalTypes;
using System.Collections.Generic;
using Framework;
using DataCache;
using System.Linq;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PrivilegeGroupAccessTest and is intended
    ///to contain all PrivilegeGroupAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrivilegeGroupAccessTest
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
            for (int privilegeIndex = 201; privilegeIndex < 206; privilegeIndex++)
            {
                if (!Cache.CacheData.t_privileges.Any(privilege => privilege.C_id == privilegeIndex))
                {
                    t_privileges newPrivilege = t_privileges.Createt_privileges(privilegeIndex);
                    newPrivilege.privilege_name = privilegeIndex + ":privilege";
                    Cache.CacheData.t_privileges.AddObject(newPrivilege);
                }
            }
            Cache.CacheData.SaveChanges();

            for (int groupIndex = 1; groupIndex < 11; groupIndex++)
            {
                if (!Cache.CacheData.t_privilege_groups.Any(group => group.C_id == groupIndex))
                {
                    t_privilege_groups newGroup = t_privilege_groups.Createt_privilege_groups(groupIndex);
                    newGroup.group_name = "group:" + groupIndex;

                    List<t_privileges> allPrivileges = (from privilege in Cache.CacheData.t_privileges
                                                        where privilege.C_id == 201 ||
                                                              privilege.C_id == 202 ||
                                                              privilege.C_id == 203 ||
                                                              privilege.C_id == 204 ||
                                                              privilege.C_id == 205
                                                        select privilege).ToList();
                    foreach (t_privileges CurrPrivilege in allPrivileges)
                    {
                        newGroup.t_privileges.Add(CurrPrivilege);
                    }
                    Cache.CacheData.t_privilege_groups.AddObject(newGroup);
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

        #region Add Tests

        /// <summary>
        ///A test for AddMultipleNewPrivilegesGroups
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPrivilegesGroupsTest()
        {
            List<PrivilegesGroup> newPrivilegesGroupList = new List<PrivilegesGroup>()
            {
                new PrivilegesGroup(11, "group:11", new List<Privilege>(){new Privilege(1894, "privilege:1894")}),
                new PrivilegesGroup(12, "group:12", new List<Privilege>(){new Privilege(1894, "privilege:1894")})
            };
            PrivilegeGroupAccess.AddMultipleNewPrivilegesGroups(newPrivilegesGroupList);
            List<PrivilegesGroup> afterAddition = PrivilegeGroupAccess.GetAllPrivilegesGroups();
            Assert.IsTrue(afterAddition.Contains(newPrivilegesGroupList));
        }

        /// <summary>
        ///A test for AddNewPrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void AddNewPrivilegesGroupTest()
        {
            PrivilegesGroup newPrivilegesGroup = new PrivilegesGroup(13, "group:13",
                new List<Privilege>()
                {
                    new Privilege(1710, "privilege:1710"),
                    new Privilege(1894, "privilege:1894")
                });
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.AddNewPrivilegesGroup(newPrivilegesGroup);
            Assert.AreEqual(expected, actual);
            List<PrivilegesGroup> afterAdd = PrivilegeGroupAccess.GetAllPrivilegesGroups();
            Assert.IsTrue(afterAdd.Contains(newPrivilegesGroup));
        }
        
        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertSingleDbPrivilegesGroupToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbPrivilegesGroupToLocalTypeTest()
        {
            t_privilege_groups dbTypePrivilegesGroup = t_privilege_groups.Createt_privilege_groups(1);
            dbTypePrivilegesGroup.group_name = "group:1";
            
            t_privileges firstPrivilege = t_privileges.Createt_privileges(1);
            firstPrivilege.privilege_name = "privilege:1";
            t_privileges secondPrivilege = t_privileges.Createt_privileges(2);
            secondPrivilege.privilege_name = "privilege:2";
            dbTypePrivilegesGroup.t_privileges.Add(firstPrivilege);
            dbTypePrivilegesGroup.t_privileges.Add(secondPrivilege);

            PrivilegesGroup expected = new PrivilegesGroup(1, "group:1",
                new List<Privilege>(){new Privilege(1, "privilege:1"),
                                      new Privilege(2, "privilege:2")});
            PrivilegesGroup actual;
            actual = PrivilegeGroupAccess_Accessor.ConvertSingleDbPrivilegesGroupToLocalType(dbTypePrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPrivilegesGroupToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalPrivilegesGroupToDbTypeTest()
        {
            PrivilegesGroup localTypePrivilegesGroup = new PrivilegesGroup(1, "group:1",
                new List<Privilege>(){new Privilege(1, "privilege:1"),
                                      new Privilege(2, "privilege:2")});
            t_privilege_groups expected = t_privilege_groups.Createt_privilege_groups(1);
            expected.group_name = "group:1";
            
            t_privileges firstPrivilege = t_privileges.Createt_privileges(1);
            firstPrivilege.privilege_name = "privilege:1";
            t_privileges secondPrivilege = t_privileges.Createt_privileges(2);
            secondPrivilege.privilege_name = "privilege:2";
            expected.t_privileges.Add(firstPrivilege);
            expected.t_privileges.Add(secondPrivilege);
           
            t_privilege_groups actual;
            actual = PrivilegeGroupAccess_Accessor.ConvertSingleLocalPrivilegesGroupToDbType(localTypePrivilegesGroup);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.group_name, actual.group_name);
            Assert.IsTrue(expected.t_privileges.Count == actual.t_privileges.Count);
            
            for (int i = 0; i < expected.t_privileges.Count; i++)
            {
                Assert.AreEqual(expected.t_privileges.ElementAt(i).C_id,
                                        actual.t_privileges.ElementAt(i).C_id);
                Assert.AreEqual(expected.t_privileges.ElementAt(i).privilege_name,
                                        actual.t_privileges.ElementAt(i).privilege_name);
            }
        }
        
        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePrivilegesGroups
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePrivilegesGroupsTest()
        {
            List<PrivilegesGroup> deletedPrivilegesGroupList = null; // TODO: Initialize to an appropriate value
            PrivilegeGroupAccess.DeleteMultiplePrivilegesGroups(deletedPrivilegesGroupList);
        }

        /// <summary>
        ///A test for DeleteSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePrivilegesGroupTest()
        {
            PrivilegesGroup deletedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.DeleteSinglePrivilegesGroup(deletedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExsistentPrivilegesGroupTest()
        {
            PrivilegesGroup deletedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.DeleteSinglePrivilegesGroup(deletedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllPrivilegesGroups
        ///</summary>
        [TestMethod()]
        public void GetAllPrivilegesGroupsTest()
        {
            List<PrivilegesGroup> expected = PrivilegeGroupAccess.ConvertMultipleDbPrivilegesGroupsToLocalType(
                PrivilegeGroupAccess_Accessor.LookupAllPrivilegesGroups());
            List<PrivilegesGroup> actual;
            actual = PrivilegeGroupAccess.GetAllPrivilegesGroups();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegesGroupByGroupName
        ///</summary>
        [TestMethod()]
        public void GetPrivilegesGroupByGroupNameTest()
        {
            string groupName = "group:1";
            PrivilegesGroup expected = PrivilegeGroupAccess.ConvertSingleDbPrivilegesGroupToLocalType(
                PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupByGroupName(groupName));
            PrivilegesGroup actual;
            actual = PrivilegeGroupAccess.GetPrivilegesGroupByGroupName(groupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegesGroupByGroupName
        ///</summary>
        [TestMethod()]
        public void GetPrivilegesGroupByNonExsistintGroupNameTest()
        {
            string groupName = "blah";
            PrivilegesGroup expected = null;
            PrivilegesGroup actual;
            actual = PrivilegeGroupAccess.GetPrivilegesGroupByGroupName(groupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegesGroupById
        ///</summary>
        [TestMethod()]
        public void GetPrivilegesGroupByIdTest()
        {
            int id = 1;
            PrivilegesGroup expected = PrivilegeGroupAccess.ConvertSingleDbPrivilegesGroupToLocalType(
                PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupById(id));
            PrivilegesGroup actual;
            actual = PrivilegeGroupAccess.GetPrivilegesGroupById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegesGroupById
        ///</summary>
        [TestMethod()]
        public void GetPrivilegesGroupByNonExsistintIdTest()
        {
            int id = 0;
            PrivilegesGroup expected = null;
            PrivilegesGroup actual;
            actual = PrivilegeGroupAccess.GetPrivilegesGroupById(id);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPrivilegesGroups
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPrivilegesGroupsTest()
        {
            List<t_privilege_groups> expected = (from privilegeGroup in Cache.CacheData.t_privilege_groups
                                                 select privilegeGroup).ToList();
            List<t_privilege_groups> actual;
            actual = PrivilegeGroupAccess_Accessor.LookupAllPrivilegesGroups();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPrivilegesGroupByGroupName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegesGroupByGroupNameTest()
        {
            string groupName = "group:1";
            t_privilege_groups expected = (from privilegeGroup in Cache.CacheData.t_privilege_groups
                                           where privilegeGroup.group_name == groupName
                                           select privilegeGroup).First();
            t_privilege_groups actual;
            actual = PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupByGroupName(groupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPrivilegesGroupByGroupName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegesGroupByNonExistentGroupNameTest()
        {
            string groupName = "blah";
            t_privilege_groups expected = null;
            t_privilege_groups actual;
            actual = PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupByGroupName(groupName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPrivilegesGroupById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegesGroupByIdTest()
        {
            int id = 1;
            t_privilege_groups expected = (from privilegeGroup in Cache.CacheData.t_privilege_groups
                                           where privilegeGroup.C_id == id
                                           select privilegeGroup).First();
            t_privilege_groups actual;
            actual = PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPrivilegesGroupById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegesGroupByNonExsistentIdTest()
        {
            int id = 0;
            t_privilege_groups expected = null;
            t_privilege_groups actual;
            actual = PrivilegeGroupAccess_Accessor.LookupPrivilegesGroupById(id);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePrivilegesGroups
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePrivilegesGroupsTest()
        {
            List<PrivilegesGroup> updatedPrivilegesGroupList = null; // TODO: Initialize to an appropriate value
            PrivilegeGroupAccess.UpdateMultiplePrivilegesGroups(updatedPrivilegesGroupList);
        }

        /// <summary>
        ///A test for UpdateSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePrivilegesGroupTest()
        {
            PrivilegesGroup updatedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.UpdateSinglePrivilegesGroup(updatedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExsistentPrivilegesGroupTest()
        {
            PrivilegesGroup updatedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.UpdateSinglePrivilegesGroup(updatedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void UpsertAddSinglePrivilegesGroupTest()
        {
            PrivilegesGroup upsertedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.UpsertSinglePrivilegesGroup(upsertedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for UpsertSinglePrivilegesGroup
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateSinglePrivilegesGroupTest()
        {
            PrivilegesGroup upsertedPrivilegesGroup = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeGroupAccess.UpsertSinglePrivilegesGroup(upsertedPrivilegesGroup);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
