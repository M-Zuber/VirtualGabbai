using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class PhoneNumberAccess
    {
        #region Read Methods

        #region Local type return

        public static List<PhoneNumber> GetPhoneNumberByType(PhoneType searchedType) => ConvertMultipleDbPhoneNumbersToLocalType(LookupPhoneNumberByType(searchedType));

        public static PhoneNumber GetPhoneNumberById(int id) => ConvertSingleDbPhoneNumberToLocalType(LookupPhoneNumberById(id));

        public static List<PhoneNumber> GetAllPhoneNumbers(int personId) => ConvertMultipleDbPhoneNumbersToLocalType(LookupAllPhoneNumbers(personId));

        public static PhoneNumber GetSpecificPhoneNumber(string phoneNumber, PhoneType numberType) => ConvertSingleDbPhoneNumberToLocalType(LookupSpecificPhoneNumber(phoneNumber, numberType.ID));

        #endregion

        #region Db type return

        private static List<DataCache.Models.PhoneNumber> LookupPhoneNumberByType(PhoneType searchedType)
        {
            try
            {
                return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                        where CurrPhoneNumber.NumberTypeID == searchedType.ID
                        select CurrPhoneNumber).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static PhoneNumber LookupSpecificPhoneNumber(string phoneNumber, int numberType)
        {
            try
            {
                return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                        where CurrPhoneNumber.Number == phoneNumber &&
                              CurrPhoneNumber.NumberTypeID == numberType
                        select CurrPhoneNumber).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.PhoneNumber> LookupAllPhoneNumbers(int personId)
        {
            try
            {
                return (from CurrPerson in Cache.CacheData.t_people
                        where CurrPerson.ID == personId
                        select CurrPerson).First().PhoneNumbers.ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static PhoneNumber LookupPhoneNumberById(int id)
        {
            try
            {
                return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
                        where CurrPhoneType.ID == id
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
                new PhoneTypeAccess().UpsertSingle(newPhoneNumber.NumberType);
                DataCache.Models.PhoneNumber phoneNumberToAdd = ConvertSingleLocalPhoneNumberToDbType(newPhoneNumber, personId);
                Cache.CacheData.t_phone_numbers.Add(phoneNumberToAdd);
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
                new PhoneTypeAccess().UpsertSingle(updatedPhoneNumber.NumberType);
                DataCache.Models.PhoneNumber phoneNumberUpdating = LookupPhoneNumberById(updatedPhoneNumber.ID);
                phoneNumberUpdating = ConvertSingleLocalPhoneNumberToDbType(updatedPhoneNumber, personId);
                Cache.CacheData.t_phone_numbers.Attach(phoneNumberUpdating);
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
                DataCache.Models.PhoneNumber phoneTypeDeleting =
                        Cache.CacheData.t_phone_numbers.First(number => number.ID == deletedPhoneNumber.ID);
                Cache.CacheData.t_phone_numbers.Remove(phoneTypeDeleting);
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
            PhoneNumber currentPhoneNumber = GetPhoneNumberById(upsertedPhoneNumber.ID);

            if (currentPhoneNumber == null)
            {
                return AddNewPhoneNumber(upsertedPhoneNumber, personId);
            }
            else
            {
                return UpdateSinglePhoneNumber(upsertedPhoneNumber, personId);
            }
        }

        public static void UpsertMultiplePhoneNumbers(IEnumerable<PhoneNumber> upsertedList, int personId)
        {
            foreach (PhoneNumber CurrPhoneNumber in upsertedList)
            {
                UpsertSinglePhoneNumber(CurrPhoneNumber, personId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<DataCache.Models.PhoneNumber> ConvertMultipleLocalPhoneNumbersToDbType(List<PhoneNumber> localTypePhoneNumberList, int personId)
        {
            List<DataCache.Models.PhoneNumber> dbTypePhoneNumberList = new List<DataCache.Models.PhoneNumber>();

            foreach (PhoneNumber CurrPhoneNumber in localTypePhoneNumberList)
            {
                dbTypePhoneNumberList.Add((DataCache.Models.PhoneNumber)ConvertSingleLocalPhoneNumberToDbType(CurrPhoneNumber, personId));
            }

            return dbTypePhoneNumberList;
        }

        internal static PhoneNumber ConvertSingleLocalPhoneNumberToDbType(PhoneNumber localTypePhoneNumber, int personId) => DataCache.Models.PhoneNumber.Createt_phone_numbers(personId, localTypePhoneNumber.Number,
                                localTypePhoneNumber.NumberTypeID, localTypePhoneNumber.ID);

        internal static List<PhoneNumber> ConvertMultipleDbPhoneNumbersToLocalType(List<DataCache.Models.PhoneNumber> dbTypePhoneNumberList)
        {
            if (dbTypePhoneNumberList == null)
            {
                //LOG
                return null;
            }
            List<PhoneNumber> localTypePhoneTypeList = new List<PhoneNumber>();

            foreach (DataCache.Models.PhoneNumber CurrPhoneNumber in dbTypePhoneNumberList)
            {
                localTypePhoneTypeList.Add((PhoneNumber)ConvertSingleDbPhoneNumberToLocalType(CurrPhoneNumber));
            }

            return localTypePhoneTypeList;
        }

        internal static PhoneNumber ConvertSingleDbPhoneNumberToLocalType(DataCache.Models.PhoneNumber dbTypePhoneNumber)
        {
            if (dbTypePhoneNumber == null)
            {
                //LOG
                return null;
            }

            PhoneType numberType = new PhoneTypeAccess().GetByID(dbTypePhoneNumber.NumberTypeID);
            return new PhoneNumber();
        }

        #endregion
    }
}
