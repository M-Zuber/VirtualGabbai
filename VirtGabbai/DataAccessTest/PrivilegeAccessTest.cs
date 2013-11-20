using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LocalTypes;
using System.Collections.Generic;
using Framework;
using DataCache;

namespace DataAccessTest
{


    /// <summary>
    ///This is a test class for PrivilegeAccessTest and is intended
    ///to contain all PrivilegeAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrivilegeAccessTest
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
            for (int i = 1; i < 11; i++)
            {
                if (!Cache.CacheData.t_privileges.Any(privilege => privilege.C_id == i))
                {
                    t_privileges newPrivilege = t_privileges.Createt_privileges(i);
                    newPrivilege.privilege_name = "privilege:" + i;
                    Cache.CacheData.t_privileges.AddObject(newPrivilege);
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
        ///A test for AddMultipleNewPrivileges
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPrivilegesTest()
        {
            List<Privilege> newPrivilegeList = new List<Privilege>()
            {
                new Privilege(11, "privilege:11"),
                new Privilege(12, "privilege:12")
            };
            PrivilegeAccess.AddMultipleNewPrivileges(newPrivilegeList);
            List<Privilege> afterAdd = PrivilegeAccess.GetAllPrivileges();
            Assert.IsTrue(afterAdd.Contains(newPrivilegeList));
        }

        /// <summary>
        ///A test for AddNewPrivilege
        ///</summary>
        [TestMethod()]
        public void AddNewPrivilegeTest()
        {
            Privilege newPrivilege = new Privilege(13, "privilege:13");
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.AddNewPrivilege(newPrivilege);
            Assert.AreEqual(expected, actual);
            List<Privilege> afterAdd = PrivilegeAccess.GetAllPrivileges();
            Assert.IsTrue(afterAdd.Contains(newPrivilege));
        }

        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertSingleDbPrivilegeToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleDbPrivilegeToLocalTypeTest()
        {
            t_privileges dbTypePrivilege = t_privileges.Createt_privileges(1);
            dbTypePrivilege.privilege_name = "privilege:1";
            Privilege expected = new Privilege(1, "privilege:1");
            Privilege actual;
            actual = PrivilegeAccess.ConvertSingleDbPrivilegeToLocalType(dbTypePrivilege);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPrivilegeToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleLocalPrivilegeToDbTypeTest()
        {
            Privilege localTypePrivilege = new Privilege(1, "privilege:1");
            t_privileges expected = t_privileges.Createt_privileges(1);
            expected.privilege_name = "privilege:1";
            t_privileges actual;
            actual = PrivilegeAccess.ConvertSingleLocalPrivilegeToDbType(localTypePrivilege);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.privilege_name, actual.privilege_name);
        }

        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePrivileges
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePrivilegesTest()
        {
            List<Privilege> deletedPrivilegeList = new List<Privilege>()
            {
                new Privilege(3, "privilege:3"),
                new Privilege(4, "privilege:4")
            };
            PrivilegeAccess.DeleteMultiplePrivileges(deletedPrivilegeList);
            List<Privilege> afterDelete = PrivilegeAccess.GetAllPrivileges();
            Assert.IsFalse(afterDelete.Contains(deletedPrivilegeList));
        }

        /// <summary>
        ///A test for DeleteSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePrivilegeTest()
        {
            Privilege deletedPrivilege = new Privilege(2, "privilege:2");
            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.DeleteSinglePrivilege(deletedPrivilege);
            Assert.AreEqual(expected, actual);
            List<Privilege> afterDelete = PrivilegeAccess.GetAllPrivileges();
            Assert.IsFalse(afterDelete.Contains(deletedPrivilege));
        }

        /// <summary>
        ///A test for DeleteSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExistentPrivilegeTest()
        {
            Privilege deletedPrivilege = new Privilege(2040, "privilege:2040");
            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_FAIL;
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.DeleteSinglePrivilege(deletedPrivilege);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllPrivileges
        ///</summary>
        [TestMethod()]
        public void GetAllPrivilegesTest()
        {
            List<Privilege> expected = PrivilegeAccess.ConvertMultipleDbPrivilegesToLocalType(
                PrivilegeAccess_Accessor.LookupAllPrivileges());
            List<Privilege> actual;
            actual = PrivilegeAccess.GetAllPrivileges();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegeById
        ///</summary>
        [TestMethod()]
        public void GetPrivilegeByIdTest()
        {
            int id = 1;
            Privilege expected = new Privilege(1, "privilege:1");
            Privilege actual;
            actual = PrivilegeAccess.GetPrivilegeById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPrivilegeByName
        ///</summary>
        [TestMethod()]
        public void GetPrivilegeByNameTest()
        {
            string privilegeName = "privilege:1";
            Privilege expected = new Privilege(1, "privilege:1");
            Privilege actual;
            actual = PrivilegeAccess.GetPrivilegeByName(privilegeName);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPrivileges
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPrivilegesTest()
        {
            List<t_privileges> expected = (from privilege in Cache.CacheData.t_privileges
                                           select privilege).ToList();
            List<t_privileges> actual;
            actual = PrivilegeAccess_Accessor.LookupAllPrivileges();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPrivilegeById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegeByIdTest()
        {
            int id = 1;
            t_privileges expected = t_privileges.Createt_privileges(1);
            expected.privilege_name = "privilege:1";
            t_privileges actual;
            actual = PrivilegeAccess_Accessor.LookupPrivilegeById(id);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.privilege_name, actual.privilege_name);
        }

        /// <summary>
        ///A test for LookupPrivilegeByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPrivilegeByNameTest()
        {
            string privilegeName = "privilege:1";
            t_privileges expected = t_privileges.Createt_privileges(1);
            expected.privilege_name = "privilege:1";
            t_privileges actual;
            actual = PrivilegeAccess_Accessor.LookupPrivilegeByName(privilegeName);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.privilege_name, actual.privilege_name);
        }

        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePrivileges
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePrivilegesTest()
        {
            List<Privilege> updatedPrivilegeList = new List<Privilege>()
            {
                PrivilegeAccess.GetPrivilegeById(5),
                PrivilegeAccess.GetPrivilegeById(6)
            };
            updatedPrivilegeList[0].PrivilegeName = "update:mult1";
            updatedPrivilegeList[1].PrivilegeName = "update:mult2";
            PrivilegeAccess.UpdateMultiplePrivileges(updatedPrivilegeList);
            List<Privilege> afterUpdate = new List<Privilege>()
            {
                PrivilegeAccess.GetPrivilegeById(5),
                PrivilegeAccess.GetPrivilegeById(6)
            };
            CollectionAssert.AreEqual(updatedPrivilegeList, afterUpdate);
        }

        /// <summary>
        ///A test for UpdateSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePrivilegeTest()
        {
            Privilege updatedPrivilege = PrivilegeAccess.GetPrivilegeById(7);
            updatedPrivilege.PrivilegeName = "update:single";
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.UpdateSinglePrivilege(updatedPrivilege);
            Assert.AreEqual(expected, actual);
            Privilege afterUpdate = PrivilegeAccess.GetPrivilegeById(7);
            Assert.AreEqual(updatedPrivilege, afterUpdate);
        }

        /// <summary>
        ///A test for UpdateSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExsistentPrivilegeTest()
        {
            Privilege updatedPrivilege = new Privilege(2040, "boo-yah");
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_FAIL;
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.UpdateSinglePrivilege(updatedPrivilege);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void UpsertSingleAddPrivilegeTest()
        {
            Privilege upsertedPrivilege = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.UpsertSinglePrivilege(upsertedPrivilege);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpsertSinglePrivilege
        ///</summary>
        [TestMethod()]
        public void UpsertSingleUpdatePrivilegeTest()
        {
            Privilege upsertedPrivilege = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PrivilegeAccess.UpsertSinglePrivilege(upsertedPrivilege);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
