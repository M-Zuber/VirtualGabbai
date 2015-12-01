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

        public PhoneType GetByPhoneTypeName(string typeName) => this.ConvertSingleToLocalType(this.LookupByPhoneTypeName(typeName));

        public override PhoneType GetByID(int id) => this.ConvertSingleToLocalType(this.LookupByID(id));

        public override IEnumerable<PhoneType> GetAll() => this.ConvertMultipleToLocalType(this.LookupAll().ToList());

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

        public override Enums.CRUDResults DeleteSingle(PhoneType objectToDelete)
        {
            try
            {
                t_phone_types phoneTypeDeleting =
                    Cache.CacheData.t_phone_types.FirstOrDefault(phoneType => phoneType.C_id == objectToDelete._Id);
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
        public override void DeleteMultiple(IEnumerable<PhoneType> objectsToDelete)
        {
            Enums.CRUDResults result;
            foreach (PhoneType deletedPhoneType in objectsToDelete)
            {
                result = this.DeleteSingle(deletedPhoneType);

                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public override Enums.CRUDResults UpsertSingle(PhoneType objectToUpsert)
        {
            PhoneType currentPhoneType = new PhoneTypeAccess().GetByID(objectToUpsert._Id);

            if (currentPhoneType == null)
            {
                return this.AddSingle(objectToUpsert);
            }
            else
            {
                return this.UpdateSingle(objectToUpsert);
            }
        }
        public override void UpsertMultiple(IEnumerable<PhoneType> objectsToUpsert)
        {
            foreach (PhoneType CurrPhoneType in objectsToUpsert)
            {
                this.UpsertSingle(CurrPhoneType);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        protected override IEnumerable<t_phone_types> ConvertMultipleToDBType(IEnumerable<PhoneType> localTypeObjects)
        {
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();

            foreach (PhoneType CurrPhoneType in localTypeObjects)
            {
                dbTypePhoneTypeList.Add(this.ConvertSingleToDBType(CurrPhoneType));
            }

            return dbTypePhoneTypeList;
        }

        protected override t_phone_types ConvertSingleToDBType(PhoneType localTypeObject) => t_phone_types.Createt_phone_types(localTypeObject._Id, localTypeObject.PhoneTypeName);

        protected override IEnumerable<PhoneType> ConvertMultipleToLocalType(IEnumerable<t_phone_types> dbTypeObjects)
        {
            if (dbTypeObjects == null)
            {
                //LOG 
                return null;
            }

            List<PhoneType> localTypePhoneTypeList = new List<PhoneType>();

            foreach (t_phone_types CurrPhoneType in dbTypeObjects)
            {
                localTypePhoneTypeList.Add(this.ConvertSingleToLocalType(CurrPhoneType));
            }

            return localTypePhoneTypeList;
        }

        protected override PhoneType ConvertSingleToLocalType(t_phone_types dbTypeObject)
        {
            if (dbTypeObject == null)
            {
                //LOG 
                return null;
            }
            return new PhoneType(dbTypeObject.C_id, dbTypeObject.type_name);
        }

        #endregion

        #region HelperMethods

        public override int GetMaxID() => Cache.CacheData.t_phone_types.Max(pt => pt.C_id);

        #endregion
    }
}
