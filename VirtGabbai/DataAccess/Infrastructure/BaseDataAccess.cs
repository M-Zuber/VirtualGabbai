using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Infrastructure
{
    public abstract class BaseDataAccess<TLocalType, TDBType>: IDataAccess<TLocalType, TDBType>
    {
        #region Db Type Return

        protected abstract TDBType LookupByID(int id);

        protected abstract IEnumerable<TDBType> LookupAll();

        #endregion

        #region Convert Methods

        #region To Local Type

        protected abstract IEnumerable<TLocalType> ConvertMultipleToLocalType(IEnumerable<TDBType> dbTypeObjects);

        protected abstract TLocalType ConvertSingleToLocalType(TDBType dbTypeObject);

        #endregion

        #region To Db Type

        protected abstract IEnumerable<TDBType> ConvertMultipleToDBType(IEnumerable<TLocalType> localTypeObjects);

        protected abstract TDBType ConvertSingleToDBType(TLocalType localTypeObject);

        #endregion

        #endregion

        #region IDataAccessRegion

        public abstract TLocalType GetByID(int id);

        public abstract IEnumerable<TLocalType> GetAll();

        public abstract Enums.CRUDResults AddSingle(TLocalType objectToAdd);

        public abstract void AddMultiple(IEnumerable<TLocalType> objectsToAdd);

        public abstract Enums.CRUDResults UpdateSingle(TLocalType objectToUpdate);

        public abstract void UpdateMultiple(IEnumerable<TLocalType> objecstToUpsert);

        public abstract Enums.CRUDResults UpsertSingle(TLocalType objectToUpsert);

        public abstract void UpsertMultiple(IEnumerable<TLocalType> objectsToUpsert);

        public abstract Enums.CRUDResults DeleteSingle(TLocalType objectToDelete);

        public abstract void DeleteMultiple(IEnumerable<TLocalType> objectsToDelete);

        public abstract int GetMaxID();

        #endregion
    }
}
