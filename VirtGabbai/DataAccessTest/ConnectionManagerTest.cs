//using DataAccess;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace DataAccessTest
//{
    
    
//    /// <summary>
//    ///This is a test class for ConnectionManagerTest and is intended
//    ///to contain all ConnectionManagerTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class ConnectionManagerTest
//    {


//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion

//        /// <summary>
//        ///Tests that the calling ConnectionManager.Instance recieves the connection
//        ///string correctly
//        ///</summary>
//        [TestMethod()]
//        public void InstanceConnectionStringTest()
//        {
//            ConnectionManager actual = ConnectionManager.Instance;
//            Assert.AreEqual("database=zera_levi;server=127.0.0.10;" +
//                            "User Id=root;password=7BAC61zuber", actual.Connection.ConnectionString);
//        }

//        /// <summary>
//        ///Tests that the connection can be opened
//        ///</summary>
//        [TestMethod()]
//        public void InstanceConnectTest()
//        {
//            ConnectionManager Connecter = ConnectionManager.Instance;
//            bool expected = true;
//            bool actual = false;
//            try
//            {
//                Connecter.Connection.Open();
//                actual = true;
//            }
//            catch
//            {
//                actual = false;
//            }
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        /// Tests that the singelton implentation works
//        /// </summary>
//        [TestMethod()]
//        public void InstanceSingeltonTest()
//        {
//            ConnectionManager actual = ConnectionManager.Instance;
//            var expected = ConnectionManager.Instance;
//            Assert.AreEqual(expected, actual); 
//        }
//    }
//}
