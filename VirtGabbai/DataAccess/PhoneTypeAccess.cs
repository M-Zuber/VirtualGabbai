using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using DataAccess.Infrastructure;
using DataCache.Models;

namespace DataAccess
{
    public class PhoneTypeAccess : BaseDataAccess<PhoneType, DataCache.Models.PhoneType>
    {
        #region Read Methods

        #region Local type return

        public PhoneType GetByPhoneTypeName(string typeName) => this.ConvertSingleToLocalType(this.LookupByPhoneTypeName(typeName));

        public override PhoneType GetByID(int id) => this.ConvertSingleToLocalType(this.LookupByID(id));

        public override IEnumerable<PhoneType> GetAll() => this.ConvertMultipleToLocalType(this.LookupAll().ToList());

        #endregion

        #region Db type return

        protected override IEnumerable<DataCache.Models.PhoneType> LookupAll()
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

        protected override PhoneType LookupByID(int id)
        {
            try
            {
                return Cache.CacheData.t_phone_types.FirstOrDefault(pt => pt.ID == id);
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        protected PhoneType LookupByPhoneTypeName(string typeName)
        {
            try
            {
                return Cache.CacheData.t_phone_types.FirstOrDefault(pt => pt.Name.Equals(typeName, StringComparison.CurrentCultureIgnoreCase));
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
                DataCache.Models.PhoneType phoneTypeToAdd = this.ConvertSingleToDBType(objectToAdd);
                Cache.CacheData.t_phone_types.Add(phoneTypeToAdd);
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
                DataCache.Models.PhoneType phoneTypeUpdating = this.LookupByID(objectToUpdate.ID);
                phoneTypeUpdating = this.ConvertSingleToDBType(objectToUpdate);
                Cache.CacheData.t_phone_types.Attach(phoneTypeUpdating);
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
                DataCache.Models.PhoneType phoneTypeDeleting =
                    Cache.CacheData.t_phone_types.FirstOrDefault(phoneType => phoneType.ID == objectToDelete.ID);
                Cache.CacheData.t_phone_types.Remove(phoneTypeDeleting);
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
            PhoneType currentPhoneType = new PhoneTypeAccess().GetByID(objectToUpsert.ID);

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

        protected override IEnumerable<DataCache.Models.PhoneType> ConvertMultipleToDBType(IEnumerable<PhoneType> localTypeObjects)
        {
            List<DataCache.Models.PhoneType> dbTypePhoneTypeList = new List<DataCache.Models.PhoneType>();

            foreach (PhoneType CurrPhoneType in localTypeObjects)
            {
                dbTypePhoneTypeList.Add((DataCache.Models.PhoneType)this.ConvertSingleToDBType(CurrPhoneType));
            }

            return dbTypePhoneTypeList;
        }

        protected override PhoneType ConvertSingleToDBType(PhoneType localTypeObject) => DataCache.Models.PhoneType.Createt_phone_types(localTypeObject.ID, localTypeObject.Name);

        protected override IEnumerable<PhoneType> ConvertMultipleToLocalType(IEnumerable<DataCache.Models.PhoneType> dbTypeObjects)
        {
            if (dbTypeObjects == null)
            {
                //LOG 
                return null;
            }

            List<PhoneType> localTypePhoneTypeList = new List<PhoneType>();

            foreach (DataCache.Models.PhoneType CurrPhoneType in dbTypeObjects)
            {
                localTypePhoneTypeList.Add((PhoneType)this.ConvertSingleToLocalType(CurrPhoneType));
            }

            return localTypePhoneTypeList;
        }

        protected override PhoneType ConvertSingleToLocalType(DataCache.Models.PhoneType dbTypeObject)
        {
            if (dbTypeObject == null)
            {
                //LOG 
                return null;
            }
            return new PhoneType();
        }

        #endregion

        #region HelperMethods

        public override int GetMaxID() => Cache.CacheData.t_phone_types.Max(pt => pt.ID);

        #endregion
    }
}
