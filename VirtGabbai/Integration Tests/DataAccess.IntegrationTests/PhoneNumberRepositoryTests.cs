using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using GenFu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IntegrationTests
{
    [TestClass()]
    public class PhoneNumberRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        PhoneNumberRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            repository = new PhoneNumberRepository(_ctx);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _ctx.Database.Delete();
        }

        [TestMethod]
        public void Exists_Item_Null_Item_Returns_False()
        {
            Assert.IsFalse(repository.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var item = A.New<PhoneNumber>();
            Assert.IsFalse(repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(phoneNumber));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(phoneNumber.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var phoneNumbers = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(phoneNumbers, repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var phoneNumber = Helper.SetupData(_ctx);

            Assert.IsNull(repository.GetByID(phoneNumber.ID + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        class Helper
        {
            public static void GenFuSetup()
            {
                var generatedPhoneTypes = A.ListOf<PhoneType>();
                var phoneTypes = new List<PhoneType>();
                foreach (var gPT in generatedPhoneTypes)
                {
                    if (phoneTypes.FirstOrDefault(pt => pt.Name.Equals(gPT.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        phoneTypes.Add(gPT);
                    }
                }      

                A.Configure<PhoneNumber>()
                    .Fill(pn => pn.Number)
                    .AsPhoneNumber()
                    .Fill(pn => pn.Type)
                    .WithRandom(phoneTypes);
            }

            public static PhoneNumber SetupData(VGTestContext ctx)
            {
                GenFuSetup();
                var person = A.New<Person>();
                person.PhoneNumbers = A.ListOf<PhoneNumber>(1);
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person.PhoneNumbers.First();
            }

            public static List<PhoneNumber> SetupData(VGTestContext ctx, int count)
            {
                GenFuSetup();
                var people = A.ListOf<Person>(count);
                List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

                foreach (var person in people)
                {
                    var phoneNumber = A.ListOf<PhoneNumber>();
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
