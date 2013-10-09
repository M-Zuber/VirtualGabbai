using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

namespace DataAccess
{
    public class PhoneTypeAccess
    {
        #region Read Methods

        #region Local type return

        public PhoneType GetPhoneTypeByTypeName(string typeName)
        {
            return null;//this.ConverSingleYahrtziehtToLocalType(this.LookupSpecificYahrtzieht(personId, yahr_date, personName));
        }

        public PhoneType GetPhoneTypeById(int id)
        {
            return null;//this.ConverSingleYahrtziehtToLocalType(this.LookupSpecificYahrtzieht(personId, yahr_date, personName));
        }

        public List<PhoneType> GetAllPhoneTypes()
        {
            return null;
            //return this.ConvertMultipleYahrtziehtsToLocalType(this.LookupAllYahrtziehts(personId));
        }

        #endregion

        #region Db type return

        private t_phone_types LookupPhoneTypeByTypeName(string typeName)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    where CurrPhoneType.type_name == typeName
                    select CurrPhoneType).First();
        }

        private List<t_phone_types> LookupAllPhoneTypes()
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    select CurrPhoneType).ToList<t_phone_types>();
        }

        private t_phone_types LookupPhoneTypById(int ID)
        {
            return (from CurrPhoneType in Cache.CacheData.t_phone_types
                    where CurrPhoneType.C_id == ID
                    select CurrPhoneType).First();
        }

        #endregion

        #endregion

        #region Write

        #region Create

        public void AddNewPhoneType(PhoneType newPhoneType)
        {
            t_phone_types phonrTypeToAdd = this.ConvertSingleLocalPhoneTypeToDbType(newPhoneType);
            Cache.CacheData.t_phone_types.AddObject(phonrTypeToAdd);
            Cache.CacheData.SaveChanges();
        }

        public void AddMultipleNewPhoneTypes(List<PhoneType> newPhoneTypeList)
        {
            foreach (PhoneType newPhoneType in newPhoneTypeList)
            {
                this.AddNewPhoneType(newPhoneType);
            }
        }

        #endregion

        #region Update

        public void UpdateSinglePhoneType(PhoneType updatedPhoneType)
        {
            t_phone_types phoneTypeUpdating = this.LookupPhoneTypById(updatedPhoneType._Id);
            phoneTypeUpdating = this.ConvertSingleLocalPhoneTypeToDbType(updatedPhoneType);
            Cache.CacheData.t_phone_types.ApplyCurrentValues(phoneTypeUpdating);
            Cache.CacheData.SaveChanges();
        }

        public void UpdateMultiplePhoneTypes(List<PhoneType> updatedPhoneTypeList)
        {
            foreach (PhoneType updatedPhoneType in updatedPhoneTypeList)
            {
                this.UpdateSinglePhoneType(updatedPhoneType);
            }
        }

        #endregion

        #region Delete

        public void DeleteSinglePhoneType(PhoneType deletedPhoneType)
        {
            t_phone_types phoneTypeDeleting = 
                Cache.CacheData.t_phone_types.First(phoneType => phoneType.C_id == deletedPhoneType._Id);
            Cache.CacheData.t_phone_types.DeleteObject(phoneTypeDeleting);
            Cache.CacheData.SaveChanges();
        }

        public void DeleteMultiplePhoneTypes(List<PhoneType> deletedPhoneTypeList)
        {
            foreach (PhoneType deletedPhoneType in deletedPhoneTypeList)
            {
                this.DeleteSinglePhoneType(deletedPhoneType);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private List<t_phone_types> ConvertMultipleLocalPhoneTypesToDbType(List<PhoneType> localTypePhoneTypeList)
        {
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();

            foreach (PhoneType CurrPhoneType in localTypePhoneTypeList)
            {
                dbTypePhoneTypeList.Add(this.ConvertSingleLocalPhoneTypeToDbType(CurrPhoneType));
            }

            return dbTypePhoneTypeList;
        }

        private t_phone_types ConvertSingleLocalPhoneTypeToDbType(PhoneType localTypePhoneType)
        {
            return t_phone_types.Createt_phone_types(localTypePhoneType._Id, localTypePhoneType.PhoneTypeName);
        }

        private List<PhoneType> ConvertMultipleDbPhoneTypesToLocalType(List<t_phone_types> dbTypePhoneTypeList)
        {
            List<PhoneType> localTypePhoneTypeList = new List<PhoneType>();

            foreach (t_phone_types CurrPhoneType in dbTypePhoneTypeList)
            {
                localTypePhoneTypeList.Add(this.ConvertSingleDbPhoneTypeToLocalType(CurrPhoneType));
            }

            return localTypePhoneTypeList;
        }

        private PhoneType ConvertSingleDbPhoneTypeToLocalType(t_phone_types dbTypePhoneType)
        {
            return new PhoneType(dbTypePhoneType.C_id, dbTypePhoneType.type_name);
        }

        #endregion
    }
}
