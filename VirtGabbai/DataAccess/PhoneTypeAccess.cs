using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;
using Framework;

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
            try
            {
                return (from CurrPhoneType in Cache.CacheData.t_phone_types
                        where CurrPhoneType.type_name == typeName
                        select CurrPhoneType).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_phone_types> LookupAllPhoneTypes()
        {
            try
            {
                return (from CurrPhoneType in Cache.CacheData.t_phone_types
                        select CurrPhoneType).ToList<t_phone_types>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_phone_types LookupPhoneTypeById(int id)
        {
            try
            {
                return (from CurrPhoneType in Cache.CacheData.t_phone_types
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

        public static Enums.CRUDResults AddNewPhoneType(PhoneType newPhoneType)
        {
            try
            {
                t_phone_types phonrTypeToAdd = PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(newPhoneType);
                Cache.CacheData.t_phone_types.AddObject(phonrTypeToAdd);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewPhoneTypes(List<PhoneType> newPhoneTypeList)
        {
            Enums.CRUDResults result;
            foreach (PhoneType newPhoneType in newPhoneTypeList)
            {
                result = PhoneTypeAccess.AddNewPhoneType(newPhoneType);

                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSinglePhoneType(PhoneType updatedPhoneType)
        {
            try
            {
                t_phone_types phoneTypeUpdating = PhoneTypeAccess.LookupPhoneTypeById(updatedPhoneType._Id);
                phoneTypeUpdating = PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(updatedPhoneType);
                Cache.CacheData.t_phone_types.ApplyCurrentValues(phoneTypeUpdating);
                Cache.CacheData.SaveChanges();

                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultiplePhoneTypes(List<PhoneType> updatedPhoneTypeList)
        {
            Enums.CRUDResults result;
            foreach (PhoneType updatedPhoneType in updatedPhoneTypeList)
            {
                result = PhoneTypeAccess.UpdateSinglePhoneType(updatedPhoneType);

                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSinglePhoneType(PhoneType deletedPhoneType)
        {
            try
            {
                t_phone_types phoneTypeDeleting =
            Cache.CacheData.t_phone_types.First(phoneType => phoneType.C_id == deletedPhoneType._Id);
                Cache.CacheData.t_phone_types.DeleteObject(phoneTypeDeleting);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultiplePhoneTypes(List<PhoneType> deletedPhoneTypeList)
        {
            Enums.CRUDResults result;
            foreach (PhoneType deletedPhoneType in deletedPhoneTypeList)
            {
                result = PhoneTypeAccess.DeleteSinglePhoneType(deletedPhoneType);

                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSinglePhoneType(PhoneType upsertedPhoneType) 
        {
            PhoneType currentPhoneType = GetPhoneTypeById(upsertedPhoneType._Id);

            if (currentPhoneType == null)
            {
                return AddNewPhoneType(upsertedPhoneType);
            }
            else
            {
                return UpdateSinglePhoneType(upsertedPhoneType);
            }
        }

        public static void UpsertMultiplePhoneTypes(List<PhoneType> upsertedList) 
        {
            foreach (PhoneType CurrPhoneType in upsertedList)
            {
                UpsertSinglePhoneType(CurrPhoneType);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_phone_types> ConvertMultipleLocalPhoneTypesToDbType(List<PhoneType> localTypePhoneTypeList)
        {
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();

            foreach (PhoneType CurrPhoneType in localTypePhoneTypeList)
            {
                dbTypePhoneTypeList.Add(PhoneTypeAccess.ConvertSingleLocalPhoneTypeToDbType(CurrPhoneType));
            }

            return dbTypePhoneTypeList;
        }

        internal static t_phone_types ConvertSingleLocalPhoneTypeToDbType(PhoneType localTypePhoneType)
        {
            return t_phone_types.Createt_phone_types(localTypePhoneType._Id, localTypePhoneType.PhoneTypeName);
        }

        internal static List<PhoneType> ConvertMultipleDbPhoneTypesToLocalType(List<t_phone_types> dbTypePhoneTypeList)
        {
            if (dbTypePhoneTypeList == null)
            {
                //LOG 
                return null;
            }

            List<PhoneType> localTypePhoneTypeList = new List<PhoneType>();

            foreach (t_phone_types CurrPhoneType in dbTypePhoneTypeList)
            {
                localTypePhoneTypeList.Add(PhoneTypeAccess.ConvertSingleDbPhoneTypeToLocalType(CurrPhoneType));
            }

            return localTypePhoneTypeList;
        }

        internal static PhoneType ConvertSingleDbPhoneTypeToLocalType(t_phone_types dbTypePhoneType)
        {
            if (dbTypePhoneType == null) 
            { 
                //LOG 
                return null; 
            }
            return new PhoneType(dbTypePhoneType.C_id, dbTypePhoneType.type_name);
        }

        #endregion
    }
}
