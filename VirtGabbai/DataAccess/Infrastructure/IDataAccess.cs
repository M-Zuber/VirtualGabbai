using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace DataAccess.Infrastructure
{
    public interface IDataAccess<TLocalType, TDBType>
    {
        #region Read Methods

        #region Local Type Return

        TLocalType GetByID(int id);

        IEnumerable<TLocalType> GetAll();

        #endregion

        #endregion

        #region Write Methods

        #region Create Methods

        Enums.CRUDResults AddSingle(TLocalType objectToAdd);

        void AddMultiple(IEnumerable<TLocalType> objectsToAdd);

        #endregion

        #region Update Methods

        Enums.CRUDResults UpdateSingle(TLocalType objectToUpdate);

        void UpdateMultiple(IEnumerable<TLocalType> objecstToUpsert);

        #endregion

        #region Upsert Methods

        Enums.CRUDResults UpsertSingle(TLocalType objectToUpsert);

        void UpsertMultiple(IEnumerable<TLocalType> objectsToUpsert);
        
        #endregion

        #region Delete Methods

        Enums.CRUDResults DeleteSingle(TLocalType objectToDelete);

        void DeleteMultiple(IEnumerable<TLocalType> objectsToDelete);

        #endregion

        #endregion

        #region Helper Methods

        int GetMaxID();

        #endregion
    }
}
