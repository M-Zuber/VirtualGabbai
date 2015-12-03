using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using System.Net.Mail;
using DataCache.Models;

namespace DataAccess
{
    public static class PersonAccess
    {
        #region Read Methods

        #region Local type return

        public static List<Person> GetAllPeople() => ConvertMultipleDbPersonsToLocalType(LookupAllPeople());

        public static List<Person> GetPeopleByMembership(bool membershipStatus) => ConvertMultipleDbPersonsToLocalType(LookupByMembership(membershipStatus));

        public static List<Person> GetByYahrtzieht(Yahrtzieht yahrtziehtSearchedBy) => ConvertMultipleDbPersonsToLocalType(
            LookupByYahrtzieht(yahrtziehtSearchedBy.Name, yahrtziehtSearchedBy.Relation));

        public static Person GetById(int id) => ConvertSingleDbPersonToLocalType(LookupById(id));

        public static Person GetByEmail(MailAddress email) => ConvertSingleDbPersonToLocalType(LookupByEmail(email.Address));

        public static List<Person> GetByName(string firstName, string lastName) => ConvertMultipleDbPersonsToLocalType(LookupByName(firstName, lastName));

        public static List<Person> GetByAddress(StreetAddress addressSearchedBy) => ConvertMultipleDbPersonsToLocalType(LookupByAddress(addressSearchedBy.ToDbString()));

        public static Person GetByAccount(Account accountSearchedBy) => ConvertSingleDbPersonToLocalType(LookupByAccount(accountSearchedBy.ID));

        public static Person GetByPhoneNumber(PhoneNumber numberSearchedBy) => ConvertSingleDbPersonToLocalType(LookupByPhoneNumber(numberSearchedBy.Number));

        #endregion

        #region Db type return

        private static List<DataCache.Models.Person> LookupAllPeople()
        {
            try
            {
                return (from person in Cache.CacheData.t_people
                        select person).ToList();
            }
            catch (Exception)
            {
            	//LOG
                return null;
            }
        }

        private static List<DataCache.Models.Person> LookupByMembership(bool membershipStatus)
        {
            try
            {
                return (from person in Cache.CacheData.t_people
                        where person.Member == membershipStatus
                        select person).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Person> LookupByYahrtzieht(string nameOfDeceased, string relationToDeceased)
        {
            try
            {
                return (from currYahrtzieht in Cache.CacheData.t_yahrtziehts
                        where currYahrtzieht.deceaseds_name == nameOfDeceased &&
                              currYahrtzieht.relation == relationToDeceased
                        select currYahrtzieht.t_people).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Person LookupById(int id)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.ID == id
                        select currPerson).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Person LookupByEmail(string email)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.Email == email
                        select currPerson).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Person> LookupByName(string firstName, string lastName)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.GivenName == firstName &&
                              currPerson.FamilyName == lastName
                        select currPerson).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Person> LookupByAddress(string address)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.Address == address
                        select currPerson).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Person LookupByAccount(int accountId)
        {
            try
            {
                return (from currAccount in Cache.CacheData.t_accounts
                        where currAccount.ID == accountId
                        select currAccount.Person).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Person LookupByPhoneNumber(string numberSearchedBy)
        {
            try
            {
                return (from currNumber in Cache.CacheData.t_phone_numbers
                        where currNumber.number == numberSearchedBy
                        select currNumber.t_people).First();
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

        public static Enums.CRUDResults AddNewPerson(Person newPerson)
        {
        	try
        	{
                DataCache.Models.Person newDbPerson = ConvertSingleLocalPersonToDbType(newPerson);
                Cache.CacheData.t_people.Add(newDbPerson);
                Cache.CacheData.SaveChanges();
                YahrtziehtAccess.UpsertMultipleYahrtziehts(newPerson.Yahrtziehts, newPerson.ID);
                AccountAccess.UpsertSingleAccount(newPerson.Account, newPerson.ID);
                PhoneNumberAccess.UpsertMultiplePhoneNumbers(newPerson.PhoneNumbers, newPerson.ID);
                Cache.CacheData.SaveChanges();        		
                return Enums.CRUDResults.CREATE_SUCCESS;
        	}
        	catch (Exception)
        	{
        		//LOG
        		return Enums.CRUDResults.CREATE_FAIL;
        	}
        }

        public static void AddMultipleNewPersons(List<Person> newPersonList)
        {
        	Enums.CRUDResults result;
            foreach (Person newPerson in newPersonList)
        	{
                result = AddNewPerson(newPerson);
        		if (result == Enums.CRUDResults.CREATE_FAIL)
        		{
        			//LOG
        		}
        	}
        }
        
        #endregion
        
        #region Update

        public static Enums.CRUDResults UpdateSinglePerson(Person updatedPerson)
        {
        	try
        	{
                DataCache.Models.Person personUpdating = LookupById(updatedPerson.ID);
                personUpdating = ConvertSingleLocalPersonToDbType(updatedPerson);
                Cache.CacheData.t_people.Attach(personUpdating);
                YahrtziehtAccess.UpsertMultipleYahrtziehts(updatedPerson.Yahrtziehts, updatedPerson.ID);
                AccountAccess.UpsertSingleAccount(updatedPerson.Account, updatedPerson.ID);
                PhoneNumberAccess.UpsertMultiplePhoneNumbers(updatedPerson.PhoneNumbers, updatedPerson.ID);
                Cache.CacheData.SaveChanges();
        		return Enums.CRUDResults.UPDATE_SUCCESS;
        	}
        	catch (Exception)
        	{
        		//LOG
        		return Enums.CRUDResults.UPDATE_FAIL;
        	}
        }

        public static void UpdateMultiplePersons(List<Person> updatedPersonList)
        {
        	Enums.CRUDResults result;
            foreach (Person updatedPerson in updatedPersonList)
        	{
                result = UpdateSinglePerson(updatedPerson);
        		if (result == Enums.CRUDResults.UPDATE_FAIL)
        		{
        			//LOG
        		}
        	}
        }
        
        #endregion
        
        #region Delete

        public static Enums.CRUDResults DeleteSinglePerson(Person deletedPerson)
        {
        	try
        	{
                DataCache.Models.Person personDeleting =
                    Cache.CacheData.t_people.First(person => person.ID == deletedPerson.ID);
                Cache.CacheData.t_people.Remove(personDeleting);
        		Cache.CacheData.SaveChanges();
        		return Enums.CRUDResults.DELETE_SUCCESS;
        	}
        	catch (Exception)
        	{
        		//LOG
        		return Enums.CRUDResults.DELETE_FAIL;
        	}
        }

        public static void DeleteMultiplePersons(List<Person> deletedPersonList)
        {
        	Enums.CRUDResults result;
            foreach (Person deletedPerson in deletedPersonList)
        	{
                result = DeleteSinglePerson(deletedPerson);
        		if (result == Enums.CRUDResults.DELETE_FAIL)
        		{
        			//LOG
        		}
        	}
        }
        
        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSinglePerson(Person upsertedPerson)
        {
            Person currentPerson = GetById(upsertedPerson.ID);

            if (currentPerson == null)
        	{
                return AddNewPerson(upsertedPerson);
        	}
        	else
        	{
                return UpdateSinglePerson(upsertedPerson);
        	}
        }

        public static void UpsertMultiplePersons(List<Person> upsertedList)
        {
            foreach (Person CurrPerson in upsertedList)
        	{
                UpsertSinglePerson(CurrPerson);
        	}
        }
        
        #endregion
        
        #endregion
        
        #region Private Methods

        internal static List<DataCache.Models.Person> ConvertMultipleLocalPersonsToDbType(List<Person> localTypePersonList)
        {
            List<DataCache.Models.Person> dbTypePersonList = new List<DataCache.Models.Person>();

            foreach (Person CurrPerson in localTypePersonList)
        	{
                dbTypePersonList.Add((DataCache.Models.Person)ConvertSingleLocalPersonToDbType(CurrPerson));
        	}

            return dbTypePersonList;
        }

        internal static Person ConvertSingleLocalPersonToDbType(Person localTypePerson)
        {
            DataCache.Models.Person convertedPerson = DataCache.Models.Person.Createt_people(localTypePerson.ID);
            convertedPerson.Address = localTypePerson.FullAddress.ToDbString();
            convertedPerson.Email = localTypePerson.Email;
            convertedPerson.FamilyName = localTypePerson.FamilyName;
            convertedPerson.GivenName = localTypePerson.GivenName;
            convertedPerson.Member = localTypePerson.Member;
        	return convertedPerson;
        }

        internal static List<Person> ConvertMultipleDbPersonsToLocalType(List<DataCache.Models.Person> dbTypePersonList)
        {
            if (dbTypePersonList == null)
        	{
        		//LOG
        		return null;
        	}
            List<Person> localTypePhoneTypeList = new List<Person>();

            foreach (DataCache.Models.Person CurrPerson in dbTypePersonList)
        	{
                localTypePhoneTypeList.Add((Person)ConvertSingleDbPersonToLocalType(CurrPerson));
        	}
        
        	return localTypePhoneTypeList;
        }

        internal static Person ConvertSingleDbPersonToLocalType(DataCache.Models.Person dbTypePerson)
        {
            if (dbTypePerson == null)
        	{
        		//LOG
        		return null;
        	}
            Account personalAccount = null;

            try
            {
                personalAccount = AccountAccess.ConvertSingleDbAccountToLocalType(dbTypePerson.Account);
            }
            catch {/*LOG*/ }
            List<Yahrtzieht> personalYahrtziehts =
                YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(
                                                        dbTypePerson.Yahrtziehts.ToList());
            List<PhoneNumber> personalNumbers =
                PhoneNumberAccess.ConvertMultipleDbPhoneNumbersToLocalType(
                                                        dbTypePerson.PhoneNumbers.ToList());
            return new Person();
        }
        
        #endregion
    }
}
