using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{
    [TestClass]
    public class YahrtziehtTest
    {
        private Yahrtzieht _target;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _target = null;
        }

        #region Equals

        /// <summary>
        ///Everything is the same
        ///</summary>
        [TestMethod]
        public void AllIsEqualEqualsTest()
        {
            var other = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");

            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        /// <summary>
        ///The id is different
        ///</summary>
        [TestMethod]
        public void DiffIdEqualsTest()
        {
            var other = new Yahrtzieht(14, DateTime.Today, "rufus", "cats");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///The date is different
        ///</summary>
        [TestMethod]
        public void DiffDateEqualsTest()
        {
            var other = new Yahrtzieht(1, DateTime.MinValue, "rufus", "cats");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///The name is different
        ///</summary>
        [TestMethod]
        public void DiffNameEqualsTest()
        {
            var other = new Yahrtzieht(1, DateTime.Today, "fido", "cats");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///The relation is different
        ///</summary>
        [TestMethod]
        public void DiffRelationEqualsTest()
        {
            var other = new Yahrtzieht(1, DateTime.Today, "rufus", "dogs");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///Everything is different
        ///</summary>
        [TestMethod]
        public void AllDiffEqualsTest()
        {
            var other = new Yahrtzieht(12, DateTime.MinValue, "fido", "dogs");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///Multiple properties are different
        ///</summary>
        [TestMethod]
        public void MultiDiffEqualsTest()
        {
            var other = new Yahrtzieht(14, DateTime.Today, "fido", "cats");

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        [TestMethod]
        public void Yahrtzieht_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_target.Equals(null));
        }

        [TestMethod]
        public void Yahrtzieht_Equals_Non_Yahrtzieht_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_target.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void Yahrtzieht_Equals_Same_Ref_Returns_True()
        {
            var other = _target;

            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        #endregion

        #region ToString

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            var expected = "Deceased's Name:\"rufus\" " +
                              "Date:\"" + DateTime.Today.Date.ToString("dd/MM/yyyy") + "\" " +
                              "Relation:\"cats\"";

            Assert.AreEqual(expected, _target.ToString());
        }

        #endregion
    }
}
