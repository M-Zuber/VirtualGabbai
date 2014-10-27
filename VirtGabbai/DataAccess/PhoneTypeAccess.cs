using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using DataAccess.Infrastructure;

namespace DataAccess
{
    public class PhoneTypeAccess : BaseDataAccess<PhoneType, t_phone_types>
    {
        #region Read Methods

        #region Local type return

        public PhoneType GeByPhoneTypeName(string typeName)
        {
            return this.ConvertSingleToLocalType(this.LookupByPhoneTypeName(typeName));
        }

        public override PhoneType GetByID(int id)
        {
            return this.ConvertSingleToLocalType(this.LookupByID(id));
        }

        public override IEnumerable<PhoneType> GetAll()
        {
            return this.ConvertMultipleToLocalType(this.LookupAll().ToList());
        }

        #endregion

        #region Db type return

        protected override IEnumerable<t_phone_types> LookupAll()
        {
            try
            {
                return Cache.CacheData.t_phone_types;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        protected override t_phone_types LookupByID(int id)
        {
            try
            {
                return Cache.CacheData.t_phone_types.FirstOrDefault(pt => pt.C_id == id);
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        protected t_phone_types LookupByPhoneTypeName(string typeName)
        {
            try
            {
                return Cache.CacheData.t_phone_types.FirstOrDefault(pt => pt.type_name.Equals(typeName, StringComparison.CurrentCultureIgnoreCase));
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

        public override Enums.CRUDResults AddSingle(PhoneType objectToAdd)
        {
            try
            {
                t_phone_types phoneTypeToAdd = this.ConvertSingleToDBType(objectToAdd);
                Cache.CacheData.t_phone_types.AddObject(phoneTypeToAdd);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public override void AddMultiple(IEnumerable<PhoneType> objectsToAdd)
        {
            Enums.CRUDResults result;
            foreach (PhoneType newPhoneType in objectsToAdd)
            {
                result = this.AddSingle(newPhoneType);

                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public override Enums.CRUDResults UpdateSingle(PhoneType objectToUpdate)
        {
            try
            {
                t_phone_types phoneTypeUpdating = this.LookupByID(objectToUpdate._Id);
                phoneTypeUpdating = this.ConvertSingleToDBType(objectToUpdate);
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

        public override void UpdateMultiple(IEnumerable<PhoneType> objecstToUpdate)
        {
            Enums.CRUDResults result;
            foreach (PhoneType updatedPhoneType in objecstToUpdate)
            {
                result = this.UpdateSingle(updatedPhoneType);

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
            PhoneType currentPhoneType = new PhoneTypeAccess().GetByID(upsertedPhoneType._Id);

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


        public override Enums.CRUDResults UpsertSingle(PhoneType objectToUpsert)
        {
            throw new NotImplementedException();
        }

        public override void UpsertMultiple(IEnumerable<PhoneType> objectsToUpsert)
        {
            throw new NotImplementedException();
        }

        public override Enums.CRUDResults DeleteSingle(PhoneType objectToDelete)
        {
            throw new NotImplementedException();
        }

        public override void DeleteMultiple(IEnumerable<PhoneType> objectsToDelete)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<PhoneType> ConvertMultipleToLocalType(IEnumerable<t_phone_types> dbTypeObjects)
        {
            throw new NotImplementedException();
        }

        protected override PhoneType ConvertSingleToLocalType(t_phone_types dbTypeObject)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<t_phone_types> ConvertMultipleToDBType(IEnumerable<PhoneType> localTypeObjects)
        {
            throw new NotImplementedException();
        }

        protected override t_phone_types ConvertSingleToDBType(PhoneType localTypeObject)
        {
            throw new NotImplementedException();
        }

        public override int GetMaxID()
        {
            throw new NotImplementedException();
        }
    }
}
