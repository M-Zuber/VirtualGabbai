using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{
    [TestClass()]
    public class YahrtziehtTest
    {
        private Yahrtzieht target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
        }
       
        [TestCleanup()]
        public void MyTestCleanup()
        {
            target = null;
        }

        #region Equals

        /// <summary>
        ///Everything is the same
        ///</summary>
        [TestMethod()]
        public void AllIsEqualEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }

        /// <summary>
        ///The id is different
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(14, DateTime.Today, "rufus", "cats");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///The date is different
        ///</summary>
        [TestMethod()]
        public void DiffDateEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(1, DateTime.MinValue, "rufus", "cats");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///The name is different
        ///</summary>
        [TestMethod()]
        public void DiffNameEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(1, DateTime.Today, "fido", "cats");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///The relation is different
        ///</summary>
        [TestMethod()]
        public void DiffRelationEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(1, DateTime.Today, "rufus", "dogs");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///Everything is different
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(12, DateTime.MinValue, "fido", "dogs");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///Multiple properties are different
        ///</summary>
        [TestMethod()]
        public void MultiDiffEqualsTest()
        {
            Yahrtzieht other = new Yahrtzieht(14, DateTime.Today, "fido", "cats");

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        [TestMethod]
        public void Yarhtzieht_Equals_Null_Returns_False()
        {
            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void Yarhtzieht_Equals_Non_Yarhtzieht_Returns_False()
        {
            Assert.IsFalse(target.Equals(0));
        }

        [TestMethod]
        public void Yahrtzieht_Equals_Same_Ref_Returns_True()
        {
            var other = target;

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }

        #endregion

        #region ToString

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string expected = "Deceased's Name:\"rufus\" " +
                              "Date:\"" + DateTime.Today.Date.ToString("dd/MM/yyyy") + "\" " +
                              "Relation:\"cats\"";
            
            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
    }
}
