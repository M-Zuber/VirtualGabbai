using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;
using Framework;

namespace DataAccess
{
    public static class PhoneNumberAccess
    {
        #region Read Methods

        #region Local type return

        public static List<PhoneNumber> GetPhoneNumberByType(PhoneType searchedType)
        {
            return ConvertMultipleDbPhoneNumbersToLocalType(LookupPhoneNumberByType(searchedType));
        }

        public static PhoneNumber GetPhoneNumberById(int id)
        {
            return ConvertSingleDbPhoneNumberToLocalType(LookupPhoneNumberById(id));
        }

        public static List<PhoneNumber> GetAllPhoneNumbers(int personId)
        {
            return ConvertMultipleDbPhoneNumbersToLocalType(LookupAllPhoneNumbers(personId));
        }

        public static PhoneNumber GetSpecificPhoneNumber(string phoneNumber, PhoneType numberType)
        {
            return ConvertSingleDbPhoneNumberToLocalType(LookupSpecificPhoneNumber(phoneNumber, numberType._Id));
        }

        #endregion

        #region Db type return

        private static List<t_phone_numbers> LookupPhoneNumberByType(PhoneType searchedType)
        {
            try
            {
                return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                        where CurrPhoneNumber.number_type == searchedType._Id
                        select CurrPhoneNumber).ToList<t_phone_numbers>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_phone_numbers LookupSpecificPhoneNumber(string phoneNumber, int numberType)
        {
            try
            {
                return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                        where CurrPhoneNumber.number == phoneNumber &&
                              CurrPhoneNumber.number_type == numberType
                        select CurrPhoneNumber).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_phone_numbers> LookupAllPhoneNumbers(int personId)
        {
            try
            {
                return (from CurrPerson in Cache.CacheData.t_people
                        where CurrPerson.C_id == personId
                        select CurrPerson).First().t_phone_numbers.ToList<t_phone_numbers>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_phone_numbers LookupPhoneNumberById(int id)
        {
            try
            {
                return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
                        where CurrPhoneType.C_id == id
                        select CurrPhoneType).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        #endregion

        #endregion

        #region Write

        #region Create

        public static Enums.CRUDResults AddNewPhoneNumber(PhoneNumber newPhoneNumber, int personId)
        {
            try
            {
                PhoneTypeAccess.UpsertSinglePhoneType(newPhoneNumber.NumberType);
                t_phone_numbers phoneNumberToAdd = ConvertSingleLocalPhoneNumberToDbType(newPhoneNumber, personId);
                Cache.CacheData.t_phone_numbers.AddObject(phoneNumberToAdd);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewPhoneTypes(List<PhoneNumber> newPhoneNumberList, int personId)
        {
            Enums.CRUDResults result;
            foreach (PhoneNumber newPhoneNumber in newPhoneNumberList)
            {
                result = AddNewPhoneNumber(newPhoneNumber, personId);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSinglePhoneNumber(PhoneNumber updatedPhoneNumber, int personId)
        {
            try
            {
                PhoneTypeAccess.UpsertSinglePhoneType(updatedPhoneNumber.NumberType);
                t_phone_numbers phoneNumberUpdating = LookupPhoneNumberById(updatedPhoneNumber._Id);
                phoneNumberUpdating = ConvertSingleLocalPhoneNumberToDbType(updatedPhoneNumber, personId);
                Cache.CacheData.t_phone_numbers.ApplyCurrentValues(phoneNumberUpdating);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultiplePhoneNumbers(List<PhoneNumber> updatedPhoneNumberList, int personId)
        {
            Enums.CRUDResults result;
            foreach (PhoneNumber updatedPhoneNumber in updatedPhoneNumberList)
            {
                result = UpdateSinglePhoneNumber(updatedPhoneNumber, personId);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSinglePhoneNumber(PhoneNumber deletedPhoneNumber)
        {
            try
            {
                t_phone_numbers phoneTypeDeleting =
                        Cache.CacheData.t_phone_numbers.First(number => number.C_id == deletedPhoneNumber._Id);
                Cache.CacheData.t_phone_numbers.DeleteObject(phoneTypeDeleting);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultiplePhoneNumbers(List<PhoneNumber> deletedPhoneNumberList)
        {
            Enums.CRUDResults result;
            foreach (PhoneNumber deletedPhoneNumber in deletedPhoneNumberList)
            {
                result = DeleteSinglePhoneNumber(deletedPhoneNumber);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSinglePhoneNumber(PhoneNumber upsertedPhoneNumber, int personId)
        {
            PhoneNumber currentPhoneNumber = GetPhoneNumberById(upsertedPhoneNumber._Id);

            if (currentPhoneNumber == null)
            {
                return AddNewPhoneNumber(upsertedPhoneNumber, personId);
            }
            else
            {
                return UpdateSinglePhoneNumber(upsertedPhoneNumber, personId);
            }
        }

        public static void UpsertMultiplePhoneNumbers(List<PhoneNumber> upsertedList, int personId)
        {
            foreach (PhoneNumber CurrPhoneNumber in upsertedList)
            {
                UpsertSinglePhoneNumber(CurrPhoneNumber, personId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_phone_numbers> ConvertMultipleLocalPhoneNumbersToDbType(List<PhoneNumber> localTypePhoneNumberList, int personId)
        {
            List<t_phone_numbers> dbTypePhoneNumberList = new List<t_phone_numbers>();

            foreach (PhoneNumber CurrPhoneNumber in localTypePhoneNumberList)
            {
                dbTypePhoneNumberList.Add(ConvertSingleLocalPhoneNumberToDbType(CurrPhoneNumber, personId));
            }

            return dbTypePhoneNumberList;
        }

        internal static t_phone_numbers ConvertSingleLocalPhoneNumberToDbType(PhoneNumber localTypePhoneNumber, int personId)
        {
            return t_phone_numbers.Createt_phone_numbers(personId, localTypePhoneNumber.Number, 
                                            localTypePhoneNumber.NumberType._Id, localTypePhoneNumber._Id);
        }

        internal static List<PhoneNumber> ConvertMultipleDbPhoneNumbersToLocalType(List<t_phone_numbers> dbTypePhoneNumberList)
        {
            if (dbTypePhoneNumberList == null)
            {
                //LOG
                return null;
            }
            List<PhoneNumber> localTypePhoneTypeList = new List<PhoneNumber>();

            foreach (t_phone_numbers CurrPhoneNumber in dbTypePhoneNumberList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbPhoneNumberToLocalType(CurrPhoneNumber));
            }

            return localTypePhoneTypeList;
        }

        internal static PhoneNumber ConvertSingleDbPhoneNumberToLocalType(t_phone_numbers dbTypePhoneNumber)
        {
            if (dbTypePhoneNumber == null)
            {
                //LOG
                return null;
            }

            PhoneType numberType = PhoneTypeAccess.GetPhoneTypeById(dbTypePhoneNumber.number_type);
            return new PhoneNumber(dbTypePhoneNumber.C_id, dbTypePhoneNumber.number, numberType);
        }

        #endregion
    }
}
