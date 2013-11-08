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
            return null;
        }

        public static List<Person> GetByYahrtzieht(Yahrtzieht yahrtziehtSearchedBy)
        {
            return null;
        }

        public static Person GetById(int id)
        {
            return null;
        }

        public static Person GetByEmail(MailAddress email)
        {
            return null;
        }

        public static List<Person> GetByName(string fullName)
        {
            return null;
        }

        public static List<Person> GetByAddress(StreetAddress addressSearchedBy)
        {
            return null;
        }

        public static Person GetByAccount(Account accountSearchedBy)
        {
            return null;
        }

        public static Person GetByPhoneNumber(PhoneNumber numberSearchedBy)
        {
            return null;
        }

        #endregion

        #region Db type return

        private static List<t_people> LookupAllPeople()
        {
            try
            {
                return null;
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
                return null;
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
                return null;
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
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_people> LookupByName(string fullName)
        {
            try
            {
                return null;
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
                return null;
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
                return null;
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
                return null;
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
            convertedPerson.email = localTypePerson.Email.ToString();
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
