using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;
using Framework;

namespace DataAccess
{
    public static class AccountAccess
    {
        #region Read Methods
        
        #region Local type return

        public static Account GetByAccountId(int accountId)
        {
            return null;
        }

        public static Account GetByPersonId(int personId)
        {
            return null;
        }

        public static List<Account> GetAllAccounts()
        {
            return null;
        }

        public static List<Account> GetByMonthlyPaymentTotal(double monthlyTotal)
        {
            return null;
        }

        public static List<Account> GetByLastMonthlyPaymentDate(DateTime lastPayment)
        {
            return null;
        }

        public static List<Account> GetByDonation(int donationId)
        {
            return null;
        }

        public static List<Account> GetByDonation(Donation donationToLookBy)
        {
            return null;
        }

        #endregion

        #region Db type return

        private static t_accounts LookupByAccountId(int accountId)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_accounts LookupByPersonId(int personId)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupAllAccounts()
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupByMonthlyPaymentTotal(double monthlyTotal)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupByLastMonthlyPaymentDate(DateTime lastPayment)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupByDonation(int donationId)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupByDonation(Donation donationToLookBy)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        #endregion
        
        #endregion
        
        #region Write Methods
        
        #region Create

        public static Enums.CRUDResults AddNewAccount(Account newAccount, int personId)
        {
            try
            {
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewAccounts(List<Account> newAccountList, int personId)
        {
            Enums.CRUDResults result;
            foreach (Account newAccount in newAccountList)
            {
                result = AddNewAccount(newAccount, personId);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }
        
        #endregion
        
        #region Update

        public static Enums.CRUDResults UpdateSingleAccount(Account updatedAccount, int personId)
        {
            try
            {
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultipleAccounts(List<Account> updatedAccountList, int personId)
        {
            Enums.CRUDResults result;
            foreach (Account updatedAccount in updatedAccountList)
            {
                result = UpdateSingleAccount(updatedAccount, personId);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }
        
        #endregion
        
        #region Delete

        public static Enums.CRUDResults DeleteSingleAccount(Account deletedAccount, int personId)
        {
            try
            {
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultipleAccounts(List<Account> deletedAccountList, int personId)
        {
            Enums.CRUDResults result;
            foreach (Account deletedAccount in deletedAccountList)
            {
                result = DeleteSingleAccount(deletedAccount, personId);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }
        
        #endregion
        
        #endregion
        
        #region Private Methods

        internal static List<t_accounts> ConvertMultipleLocalAccountsToDbType(List<Account> localTypeAccountList, int personId)
        {
            List<t_accounts> dbTypeAccountList = new List<t_accounts>();

            foreach (Account CurrAccount in localTypeAccountList)
            {
                dbTypeAccountList.Add(ConvertSingleLocalAccountToDbType(CurrAccount, personId));
            }

            return dbTypeAccountList;
        }

        internal static t_accounts ConvertSingleLocalAccountToDbType(Account localTypeAccount, int personId)
        {
            t_accounts convertedAccount = t_accounts.Createt_accounts(localTypeAccount._Id, personId);
            convertedAccount.last_month_paid = localTypeAccount.LastMonthlyPaymentDate;
            convertedAccount.monthly_total = localTypeAccount.MonthlyPaymentTotal;
            return convertedAccount;
        }

        internal static List<Account> ConvertMultipleDbAccountsToLocalType(List<t_accounts> dbTypeAccountList)
        {
            if (dbTypeAccountList == null)
            {
                //LOG
                return null;
            }
            List<Account> localTypePhoneTypeList = new List<Account>();

            foreach (t_accounts CurrAccount in dbTypeAccountList)
            {
                localTypePhoneTypeList.Add(ConvertSingleDbAccountToLocalType(CurrAccount));
            }
        
            return localTypePhoneTypeList;
        }

        internal static Account ConvertSingleDbAccountToLocalType(t_accounts dbTypeAccount)
        {
            if (dbTypeAccount == null)
            {
                //LOG
                return null;
            }
            //maybe change this to use the conversion method
            List<Donation> accountDonations = 
                DonationAccess.ConvertMultipleDbDonationsToLocalType(dbTypeAccount.t_donations.ToList<t_donations>());

            DateTime lastMonthlyPaymentDate = DateTime.Today;
            if (dbTypeAccount.last_month_paid.HasValue)
            {
                lastMonthlyPaymentDate = dbTypeAccount.last_month_paid.Value;
            }

            int monthlyTotal = 0;
            if (dbTypeAccount.monthly_total.HasValue)
            {
                monthlyTotal = dbTypeAccount.monthly_total.Value;
            }

            return new Account(dbTypeAccount.C_id, monthlyTotal, lastMonthlyPaymentDate, accountDonations);
        }
        
        #endregion
    }
}
