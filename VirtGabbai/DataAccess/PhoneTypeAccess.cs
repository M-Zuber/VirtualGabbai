using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

namespace DataAccess
{
    public static class PhoneTypeAccess
    {
        #region Read Methods

        #region Local type return

        public static PhoneType GetPhoneTypeByTypeName(string typeName)
        {
            return PhoneTypeAccess.ConvertSingleDbPhoneTypeToLocalType(PhoneTypeAccess.LookupPhoneTypeByTypeName(typeName));
        }

        public static PhoneType GetPhoneTypeById(int id)
        {
            return PhoneTypeAccess.ConvertSingleDbPhoneTypeToLocalType(PhoneTypeAccess.LookupPhoneTypeById(id));
        }

        public static List<PhoneType> GetAllPhoneTypes()
        {
            return PhoneTypeAccess.ConvertMultipleDbPhoneTypesToLocalType(PhoneTypeAccess.LookupAllPhoneTypes());
        }

        #endregion

        #region Db type return

        private static t_phone_types LookupPhoneTypeByTypeName(string typeName)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    where CurrPhoneType.type_name == typeName
                    select CurrPhoneType).First();
        }

        private static List<t_phone_types> LookupAllPhoneTypes()
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    select CurrPhoneType).ToList<t_phone_types>();
        }

        private static t_phone_types LookupPhoneTypeById(int id)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    where CurrPhoneType.C_id == id
                    select CurrPhoneType).First();
        }

        #endregion

        #endregion

        #region Write

        #region Create

        public static void AddNewPhoneType(PhoneType newPhoneType)
        {
            t_phone_types phonrTypeToAdd = PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(newPhoneType);
            Cache.CacheData.t_phone_types.AddObject(phonrTypeToAdd);
            Cache.CacheData.SaveChanges();
        }

        public static void AddMultipleNewPhoneTypes(List<PhoneType> newPhoneTypeList)
        {
            foreach (PhoneType newPhoneType in newPhoneTypeList)
            {
                PhoneTypeAccess.AddNewPhoneType(newPhoneType);
            }
        }

        #endregion

        #region Update

        public static void UpdateSinglePhoneType(PhoneType updatedPhoneType)
        {
            t_phone_types phoneTypeUpdating = PhoneTypeAccess.LookupPhoneTypeById(updatedPhoneType._Id);
            phoneTypeUpdating = PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(updatedPhoneType);
            Cache.CacheData.t_phone_types.ApplyCurrentValues(phoneTypeUpdating);
            Cache.CacheData.SaveChanges();
        }

        public static void UpdateMultiplePhoneTypes(List<PhoneType> updatedPhoneTypeList)
        {
            foreach (PhoneType updatedPhoneType in updatedPhoneTypeList)
            {
                PhoneTypeAccess.UpdateSinglePhoneType(updatedPhoneType);
            }
        }

        #endregion

        #region Delete

        public static void DeleteSinglePhoneType(PhoneType deletedPhoneType)
        {
            t_phone_types phoneTypeDeleting = 
                Cache.CacheData.t_phone_types.First(phoneType => phoneType.C_id == deletedPhoneType._Id);
            Cache.CacheData.t_phone_types.DeleteObject(phoneTypeDeleting);
            Cache.CacheData.SaveChanges();
        }

        public static void DeleteMultiplePhoneTypes(List<PhoneType> deletedPhoneTypeList)
        {
            foreach (PhoneType deletedPhoneType in deletedPhoneTypeList)
            {
                PhoneTypeAccess.DeleteSinglePhoneType(deletedPhoneType);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_phone_types> ConvertMultipleLocalPhoneTypesToDbType(List<PhoneType> localTypePhoneTypeList)
        {
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();

            foreach (PhoneType CurrPhoneType in localTypePhoneTypeList)
            {
                dbTypePhoneTypeList.Add(PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(CurrPhoneType));
            }

            return dbTypePhoneTypeList;
        }

        private static t_phone_types ConvertSingleLocalPhoneTypeToDbType(PhoneType localTypePhoneType)
        {
            return t_phone_types.Createt_phone_types(localTypePhoneType._Id, localTypePhoneType.PhoneTypeName);
        }

        private static List<PhoneType> ConvertMultipleDbPhoneTypesToLocalType(List<t_phone_types> dbTypePhoneTypeList)
        {
            List<PhoneType> localTypePhoneTypeList = new List<PhoneType>();

            foreach (t_phone_types CurrPhoneType in dbTypePhoneTypeList)
            {
                localTypePhoneTypeList.Add(PhoneTypeAccess.ConvertSingleDbPhoneTypeToLocalType(CurrPhoneType));
            }

            return localTypePhoneTypeList;
        }

        private static PhoneType ConvertSingleDbPhoneTypeToLocalType(t_phone_types dbTypePhoneType)
        {
            return new PhoneType(dbTypePhoneType.C_id, dbTypePhoneType.type_name);
        }

        #endregion
    }
}
