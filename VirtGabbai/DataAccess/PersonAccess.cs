using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using System.Net.Mail;

namespace DataAccess
{
    public static class PersonAccess
    {
        #region Read Methods
        
        #region Local type return

        public static List<Person> GetAllPeople()
        {
            return ConvertMultipleDbPersonsToLocalType(LookupAllPeople());
        }

        public static List<Person> GetByYahrtzieht(Yahrtzieht yahrtziehtSearchedBy)
        {
            return ConvertMultipleDbPersonsToLocalType(
                        LookupByYahrtzieht(yahrtziehtSearchedBy.Name, yahrtziehtSearchedBy.Relation));
        }

        public static Person GetById(int id)
        {
            return ConvertSingleDbPersonToLocalType(LookupById(id));
        }

        public static Person GetByEmail(MailAddress email)
        {
            return ConvertSingleDbPersonToLocalType(LookupByEmail(email.Address));
        }

        public static List<Person> GetByName(string firstName, string lastName)
        {
            return ConvertMultipleDbPersonsToLocalType(LookupByName(firstName, lastName));
        }

        public static List<Person> GetByAddress(StreetAddress addressSearchedBy)
        {
            return ConvertMultipleDbPersonsToLocalType(LookupByAddress(addressSearchedBy.ToDbString()));
        }

        public static Person GetByAccount(Account accountSearchedBy)
        {
            return ConvertSingleDbPersonToLocalType(LookupByAccount(accountSearchedBy._Id));
        }

        public static Person GetByPhoneNumber(PhoneNumber numberSearchedBy)
        {
            return ConvertSingleDbPersonToLocalType(LookupByPhoneNumber(numberSearchedBy.Number));
        }

        #endregion

        #region Db type return

        private static List<t_people> LookupAllPeople()
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

        private static List<t_people> LookupByYahrtzieht(string nameOfDeceased, string relationToDeceased)
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

        private static t_people LookupById(int id)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.C_id == id
                        select currPerson).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_people LookupByEmail(string email)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.email == email
                        select currPerson).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_people> LookupByName(string firstName, string lastName)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.given_name == firstName &&
                              currPerson.family_name == lastName
                        select currPerson).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_people> LookupByAddress(string address)
        {
            try
            {
                return (from currPerson in Cache.CacheData.t_people
                        where currPerson.address == address
                        select currPerson).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_people LookupByAccount(int accountId)
        {
            try
            {
                return (from currAccount in Cache.CacheData.t_accounts
                        where currAccount.C_id == accountId
                        select currAccount.t_people).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_people LookupByPhoneNumber(string numberSearchedBy)
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
                YahrtziehtAccess.UpsertMultipleYahrtziehts(updatedPerson.Yahrtziehts, updatedPerson._Id);
                AccountAccess.UpsertSingleAccount(updatedPerson.PersonalAccount, updatedPerson._Id);
                PhoneNumberAccess.UpsertMultiplePhoneNumbers(updatedPerson.PhoneNumbers, updatedPerson._Id);
                t_people personUpdating = LookupById(updatedPerson._Id);
                personUpdating = ConvertSingleLocalPersonToDbType(updatedPerson);
                Cache.CacheData.t_people.ApplyCurrentValues(personUpdating);
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
                t_people personDeleting =
                    Cache.CacheData.t_people.First(person => person.C_id == deletedPerson._Id);
                Cache.CacheData.t_people.DeleteObject(personDeleting);
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
            Person currentPerson = GetById(upsertedPerson._Id);

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

        internal static List<t_people> ConvertMultipleLocalPersonsToDbType(List<Person> localTypePersonList)
        {
            List<t_people> dbTypePersonList = new List<t_people>();

            foreach (Person CurrPerson in localTypePersonList)
        	{
                dbTypePersonList.Add(ConvertSingleLocalPersonToDbType(CurrPerson));
        	}

            return dbTypePersonList;
        }

        internal static t_people ConvertSingleLocalPersonToDbType(Person localTypePerson)
        {
            t_people convertedPerson = t_people.Createt_people(localTypePerson._Id);
            convertedPerson.address = localTypePerson.Address.ToDbString();
            convertedPerson.email = localTypePerson.Email.Address;
            convertedPerson.family_name = localTypePerson.LastName;
            convertedPerson.given_name = localTypePerson.FirstName;
        	return convertedPerson;
        }

        internal static List<Person> ConvertMultipleDbPersonsToLocalType(List<t_people> dbTypePersonList)
        {
            if (dbTypePersonList == null)
        	{
        		//LOG
        		return null;
        	}
            List<Person> localTypePhoneTypeList = new List<Person>();

            foreach (t_people CurrPerson in dbTypePersonList)
        	{
                localTypePhoneTypeList.Add(ConvertSingleDbPersonToLocalType(CurrPerson));
        	}
        
        	return localTypePhoneTypeList;
        }

        internal static Person ConvertSingleDbPersonToLocalType(t_people dbTypePerson)
        {
            if (dbTypePerson == null)
        	{
        		//LOG
        		return null;
        	}
            Account personalAccount = null;

            try
            {
                personalAccount = AccountAccess.ConvertSingleDbAccountToLocalType(dbTypePerson.t_accounts.First());
            }
            catch {/*LOG*/ }
            List<Yahrtzieht> personalYahrtziehts =
                YahrtziehtAccess.ConvertMultipleYahrtziehtsToLocalType(
                                                        dbTypePerson.t_yahrtziehts.ToList());
            List<PhoneNumber> personalNumbers =
                PhoneNumberAccess.ConvertMultipleDbPhoneNumbersToLocalType(
                                                        dbTypePerson.t_phone_numbers.ToList());
            return new Person(dbTypePerson.C_id, dbTypePerson.email, dbTypePerson.given_name,
                              dbTypePerson.family_name, dbTypePerson.address,
                              personalAccount, personalNumbers, personalYahrtziehts);
        }
        
        #endregion
    }
}
