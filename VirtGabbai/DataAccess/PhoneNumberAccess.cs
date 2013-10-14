using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

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
            return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                    where CurrPhoneNumber.number_type == searchedType._Id
                    select CurrPhoneNumber).ToList<t_phone_numbers>();
        }

        private static t_phone_numbers LookupSpecificPhoneNumber(string phoneNumber, int numberType)
        {
            return (from CurrPhoneNumber in Cache.CacheData.t_phone_numbers
                    where CurrPhoneNumber.number == phoneNumber &&
                          CurrPhoneNumber.number_type == numberType
                    select CurrPhoneNumber).First();
        }

        private static List<t_phone_numbers> LookupAllPhoneNumbers(int personId)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
                    where CurrPhoneType.person_id == personId
                    select CurrPhoneType).ToList<t_phone_numbers>();
        }

        private static t_phone_numbers LookupPhoneNumberById(int id)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
                    where CurrPhoneType.C_id == id
                    select CurrPhoneType).First();
        }

        #endregion

        #endregion

        #region Write

        #region Create

        public static void AddNewPhoneNumber(PhoneNumber newPhoneNumber)
        {
            //t_phone_types phonrTypeToAdd = this.ConvertSingleLocalPhoneNumberToDbType(newPhoneNumber);
            //Cache.CacheData.t_phone_types.AddObject(phonrTypeToAdd);
            //Cache.CacheData.SaveChanges();
        }

        public static void AddMultipleNewPhoneTypes(List<PhoneNumber> newPhoneNumberList)
        {
            foreach (PhoneNumber newPhoneNumber in newPhoneNumberList)
            {
                PhoneNumberAccess.AddNewPhoneNumber(newPhoneNumber);
            }
        }

        #endregion

        #region Update

        public static void UpdateSinglePhoneNumber(PhoneNumber updatedPhoneNumber)
        {
            //t_phone_types phoneTypeUpdating = this.LookupPhoneTypById(updatedPhoneNumber._Id);
            //phoneTypeUpdating = this.ConvertSingleLocalPhoneNumberToDbType(updatedPhoneNumber);
            //Cache.CacheData.t_phone_types.ApplyCurrentValues(phoneTypeUpdating);
            //Cache.CacheData.SaveChanges();
        }

        public static void UpdateMultiplePhoneNumbers(List<PhoneNumber> updatedPhoneNumberList)
        {
            foreach (PhoneNumber updatedPhoneNumber in updatedPhoneNumberList)
            {
                PhoneNumberAccess.UpdateSinglePhoneNumber(updatedPhoneNumber);
            }
        }

        #endregion

        #region Delete

        public static void DeleteSinglePhoneNumber(PhoneNumber deletedPhoneNumber)
        {
            //t_phone_types phoneTypeDeleting =
            //    Cache.CacheData.t_phone_types.First(phoneType => phoneType.C_id == deletedPhoneNumber._Id);
            //Cache.CacheData.t_phone_types.DeleteObject(phoneTypeDeleting);
            //Cache.CacheData.SaveChanges();
        }

        public static void DeleteMultiplePhoneNumbers(List<PhoneNumber> deletedPhoneNumberList)
        {
            foreach (PhoneNumber deletedPhoneNumber in deletedPhoneNumberList)
            {
                PhoneNumberAccess.DeleteSinglePhoneNumber(deletedPhoneNumber);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_phone_numbers> ConvertMultipleLocalPhoneNumbersToDbType(List<PhoneNumber> localTypePhoneNumberList)
        {
            List<t_phone_numbers> dbTypePhoneNumberList = new List<t_phone_numbers>();

            foreach (PhoneNumber CurrPhoneNumber in localTypePhoneNumberList)
            {
                dbTypePhoneNumberList.Add(ConvertSingleLocalPhoneNumberToDbType(CurrPhoneNumber));
            }

            return dbTypePhoneNumberList;
        }

        private static t_phone_numbers ConvertSingleLocalPhoneNumberToDbType(PhoneNumber localTypePhoneNumber)
        {
            // Due to the fact that the local type -PhoneNumber - does not keep track of the person id,
            // the id must be pulled out this way
            var personId = (from cpn in Cache.CacheData.t_phone_numbers
                            where cpn.number == localTypePhoneNumber.Number
                            select cpn).First().t_people.C_id;
            return t_phone_numbers.Createt_phone_numbers(personId, localTypePhoneNumber.Number, 
                                            localTypePhoneNumber.NumberType._Id, localTypePhoneNumber._Id);
        }

        private static List<PhoneNumber> ConvertMultipleDbPhoneNumbersToLocalType(List<t_phone_numbers> dbTypePhoneNumberList)
        {
            List<PhoneNumber> localTypePhoneTypeList = new List<PhoneNumber>();

            foreach (t_phone_numbers CurrPhoneNumber in dbTypePhoneNumberList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbPhoneNumberToLocalType(CurrPhoneNumber));
            }

            return localTypePhoneTypeList;
        }

        private static PhoneNumber ConvertSingleDbPhoneNumberToLocalType(t_phone_numbers dbTypePhoneNumber)
        {
            PhoneType numberType = PhoneTypeAccess.GetPhoneTypeById(dbTypePhoneNumber.number_type);
            return new PhoneNumber(dbTypePhoneNumber.C_id, dbTypePhoneNumber.number, numberType);
        }

        #endregion
    }
}
