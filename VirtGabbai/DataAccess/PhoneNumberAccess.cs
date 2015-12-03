using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class PhoneNumberAccess
    {
        #region Read Methods

        #region Local type return

        public static List<LocalTypes.PhoneNumber> GetPhoneNumberByType(PhoneType searchedType) => ConvertMultipleDbPhoneNumbersToLocalType(LookupPhoneNumberByType(searchedType));

        public static PhoneNumber GetPhoneNumberById(int id) => ConvertSingleDbPhoneNumberToLocalType(LookupPhoneNumberById(id));

        public static List<LocalTypes.PhoneNumber> GetAllPhoneNumbers(int personId) => ConvertMultipleDbPhoneNumbersToLocalType(LookupAllPhoneNumbers(personId));

        public static PhoneNumber GetSpecificPhoneNumber(string phoneNumber, PhoneType numberType) => ConvertSingleDbPhoneNumberToLocalType(LookupSpecificPhoneNumber(phoneNumber, numberType._Id));

        #endregion

        #region Db type return

        private static List<DataCache.Models.PhoneNumber> LookupPhoneNumberByType(PhoneType searchedType)
        {
            try
            {
                return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                        where CurrPhoneNumber.number_type == searchedType._Id
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

        public static Enums.CRUDResults AddNewPhoneNumber(LocalTypes.PhoneNumber newPhoneNumber, int personId)
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

        public static void AddMultipleNewPhoneTypes(List<LocalTypes.PhoneNumber> newPhoneNumberList, int personId)
        {
            Enums.CRUDResults result;
            foreach (LocalTypes.PhoneNumber newPhoneNumber in newPhoneNumberList)
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

        public static Enums.CRUDResults UpdateSinglePhoneNumber(LocalTypes.PhoneNumber updatedPhoneNumber, int personId)
        {
            try
            {
                new PhoneTypeAccess().UpsertSingle(updatedPhoneNumber.NumberType);
                DataCache.Models.PhoneNumber phoneNumberUpdating = LookupPhoneNumberById(updatedPhoneNumber._Id);
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

        public static void UpdateMultiplePhoneNumbers(List<LocalTypes.PhoneNumber> updatedPhoneNumberList, int personId)
        {
            Enums.CRUDResults result;
            foreach (LocalTypes.PhoneNumber updatedPhoneNumber in updatedPhoneNumberList)
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

        public static Enums.CRUDResults DeleteSinglePhoneNumber(LocalTypes.PhoneNumber deletedPhoneNumber)
        {
            try
            {
                DataCache.Models.PhoneNumber phoneTypeDeleting =
                        Cache.CacheData.t_phone_numbers.First(number => number.C_id == deletedPhoneNumber._Id);
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

        public static void DeleteMultiplePhoneNumbers(List<LocalTypes.PhoneNumber> deletedPhoneNumberList)
        {
            Enums.CRUDResults result;
            foreach (LocalTypes.PhoneNumber deletedPhoneNumber in deletedPhoneNumberList)
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

        public static Enums.CRUDResults UpsertSinglePhoneNumber(LocalTypes.PhoneNumber upsertedPhoneNumber, int personId)
        {
            LocalTypes.PhoneNumber currentPhoneNumber = GetPhoneNumberById(upsertedPhoneNumber._Id);

            if (currentPhoneNumber == null)
            {
                return AddNewPhoneNumber(upsertedPhoneNumber, personId);
            }
            else
            {
                return UpdateSinglePhoneNumber(upsertedPhoneNumber, personId);
            }
        }

        public static void UpsertMultiplePhoneNumbers(List<LocalTypes.PhoneNumber> upsertedList, int personId)
        {
            foreach (LocalTypes.PhoneNumber CurrPhoneNumber in upsertedList)
            {
                UpsertSinglePhoneNumber(CurrPhoneNumber, personId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<DataCache.Models.PhoneNumber> ConvertMultipleLocalPhoneNumbersToDbType(List<LocalTypes.PhoneNumber> localTypePhoneNumberList, int personId)
        {
            List<DataCache.Models.PhoneNumber> dbTypePhoneNumberList = new List<DataCache.Models.PhoneNumber>();

            foreach (LocalTypes.PhoneNumber CurrPhoneNumber in localTypePhoneNumberList)
            {
                dbTypePhoneNumberList.Add((DataCache.Models.PhoneNumber)ConvertSingleLocalPhoneNumberToDbType(CurrPhoneNumber, personId));
            }

            return dbTypePhoneNumberList;
        }

        internal static PhoneNumber ConvertSingleLocalPhoneNumberToDbType(LocalTypes.PhoneNumber localTypePhoneNumber, int personId) => DataCache.Models.PhoneNumber.Createt_phone_numbers(personId, localTypePhoneNumber.Number,
                                localTypePhoneNumber.NumberType._Id, localTypePhoneNumber._Id);

        internal static List<LocalTypes.PhoneNumber> ConvertMultipleDbPhoneNumbersToLocalType(List<DataCache.Models.PhoneNumber> dbTypePhoneNumberList)
        {
            if (dbTypePhoneNumberList == null)
            {
                //LOG
                return null;
            }
            List<LocalTypes.PhoneNumber> localTypePhoneTypeList = new List<LocalTypes.PhoneNumber>();

            foreach (DataCache.Models.PhoneNumber CurrPhoneNumber in dbTypePhoneNumberList)
            {
                localTypePhoneTypeList.Add((LocalTypes.PhoneNumber)ConvertSingleDbPhoneNumberToLocalType(CurrPhoneNumber));
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

            PhoneType numberType = new PhoneTypeAccess().GetByID(dbTypePhoneNumber.number_type);
            return new LocalTypes.PhoneNumber(dbTypePhoneNumber.C_id, dbTypePhoneNumber.number, numberType);
        }

        #endregion
    }
}
