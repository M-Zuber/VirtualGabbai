using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using GenFu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class PhoneNumberRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private PhoneNumberRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new PhoneNumberRepository(_ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ctx.Database.Delete();
        }

        [TestMethod]
        public void Exists_Item_Null_Item_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var item = GenFu.GenFu.New<PhoneNumber>();
            Assert.IsFalse(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(phoneNumber));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(phoneNumber.Id));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var phoneNumbers = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(phoneNumbers, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetById(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsNull(_repository.GetById(phoneNumber.Id + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetById(expected.Id));
        }

        private static class Helper
        {
            private static void GenFuSetup()
            {
                var generatedPhoneTypes = GenFu.GenFu.ListOf<PhoneType>();
                var phoneTypes = new List<PhoneType>();
                foreach (var gPt in generatedPhoneTypes)
                {
                    if (phoneTypes.Find(pt => pt.Name.Equals(gPt.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        phoneTypes.Add(gPt);
                    }
                }

                GenFu.GenFu.Configure<PhoneNumber>()
                    .Fill(pn => pn.Number)
                    .AsPhoneNumber()
                    .Fill(pn => pn.Type)
                    .WithRandom(phoneTypes);
            }

            public static PhoneNumber SetupData(ZeraLeviContext ctx)
            {
                GenFuSetup();
                var person = GenFu.GenFu.New<Person>();
                person.PhoneNumbers = GenFu.GenFu.ListOf<PhoneNumber>(1);
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person.PhoneNumbers.First();
            }

            public static List<PhoneNumber> SetupData(ZeraLeviContext ctx, int count)
            {
                GenFuSetup();
                var people = GenFu.GenFu.ListOf<Person>(count);
                var phoneNumbers = new List<PhoneNumber>();

                foreach (var person in people)
                {
                    var phoneNumber = GenFu.GenFu.ListOf<PhoneNumber>();
                    person.PhoneNumbers = phoneNumber;
                    phoneNumbers.AddRange(phoneNumber);
                }

                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return phoneNumbers;
            }
        }
    }
}
