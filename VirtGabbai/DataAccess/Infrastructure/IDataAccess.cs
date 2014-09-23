using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace DataAccess.Infrastructure
{
    interface IDataAccess<T, K>
    {
        #region Read Methods

        #region Local Type Return

        T GetAll();

        IEnumerable<T> GetByID();

        #endregion

        #region Db Type Return

        K LookupAll();

        IEnumerable<K> LookupByID(int id);

        #endregion

        #endregion

        #region Write Methods

        #region Create Methods

        Enums.CRUDResults AddSingle(T objectToAdd);

        void AddMultiple(IEnumerable<T> objectsToAdd);

        #endregion

        #region Update Methods

        Enums.CRUDResults UpdateSingle(T objectToUpdate);

        void UpdateMultiple(IEnumerable<T> objecstToUpsert);

        #endregion

        #region Upsert Methods

        Enums.CRUDResults UpsertSingle(T objectToUpsert);

        void UpsertMultiple(IEnumerable<T> objectsToUpsert);
        
        #endregion

        #region Delete Methods

        Enums.CRUDResults DeleteSingle(T objectToDelete);

        void DeleteMultiple(IEnumerable<T> objectsToDelete);

        #endregion

        #endregion

        #region Convert Methods

        #region To Local Type

        T ConvertMultipleToLocalType(IEnumerable<K> dbTypeObjects);

        T ConvertSingleToLocalType(K dbTypeObject);
        
        #endregion

        #region To Db Type

        K ConvertMultipleToDBType(IEnumerable<T> localTypeObjects);

        K ConvertSingleToDBType(T localTypeObject);

        #endregion

        #endregion

        #region Helper Methods

        int GetMaxID();

        #endregion
    }
}
