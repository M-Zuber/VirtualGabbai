using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class AccountAccess
    {
        #region Read Methods

        #region Local type return

        public static Account GetAccountById(int accountId) => ConvertSingleDbAccountToLocalType(LookupByAccountId(accountId));

        public static Account GetByPersonId(int personId) => ConvertSingleDbAccountToLocalType(LookupByPersonId(personId));

        public static List<Account> GetAllAccounts() => ConvertMultipleDbAccountsToLocalType(LookupAllAccounts());

        public static List<Account> GetByMonthlyPaymentTotal(int monthlyTotal) => ConvertMultipleDbAccountsToLocalType(LookupByMonthlyPaymentTotal(monthlyTotal));

        public static List<Account> GetByLastMonthlyPaymentDate(DateTime lastPayment) => ConvertMultipleDbAccountsToLocalType(LookupByLastMonthlyPaymentDate(lastPayment));

        public static Account GetByDonation(int donationId) => ConvertSingleDbAccountToLocalType(LookupByDonation(donationId));

        public static Account GetByDonation(Donation donationToLookBy) => ConvertSingleDbAccountToLocalType(LookupByDonation(donationToLookBy));

        #endregion

        #region Db type return

        private static Account LookupByAccountId(int accountId)
        {
            try
            {
                return Cache.CacheData.t_accounts.First(currAccount => currAccount.ID == accountId);
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Account LookupByPersonId(int personId)
        {
            try
            {
                return Cache.CacheData.t_accounts.First(WantedAccount => WantedAccount.ID == personId);
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Account> LookupAllAccounts()
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        select CurrAccount).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Account> LookupByMonthlyPaymentTotal(int monthlyTotal)
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.MonthlyPaymentTotal == monthlyTotal
                        select CurrAccount).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Account> LookupByLastMonthlyPaymentDate(DateTime lastPayment)
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.LastMonthlyPaymentDate == lastPayment
                        select CurrAccount).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Account LookupByDonation(int donationId)
        {
            try
            {
                return (from WantedAccount in Cache.CacheData.t_accounts
                        where WantedAccount.Donations.Any(WantedDonation => WantedDonation.ID == donationId)
                        select WantedAccount).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Account LookupByDonation(Donation donationToLookBy)
        {
            try
            {
                return LookupByDonation(donationToLookBy.ID);
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
                DataCache.Models.Account newDbAccount = ConvertSingleLocalAccountToDbType(newAccount, personId);
                Cache.CacheData.t_accounts.Add(newDbAccount);
                DonationAccess.UpsertMultipleDonations(newAccount.UnpaidDonations, newAccount.ID);
                DonationAccess.UpsertMultipleDonations(
                    new List<Donation>(newAccount.PaidDonations), newAccount.ID);
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
                DonationAccess.UpsertMultipleDonations(updatedAccount.UnpaidDonations, updatedAccount.ID);
                DonationAccess.UpsertMultipleDonations(new List<Donation>(updatedAccount.PaidDonations), updatedAccount.ID);
                DataCache.Models.Account accountUpdating = LookupByAccountId(updatedAccount.ID);
                accountUpdating = ConvertSingleLocalAccountToDbType(updatedAccount, personId);
                Cache.CacheData.t_accounts.Attach(accountUpdating);
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
                DataCache.Models.Account accountDeleting =
                    Cache.CacheData.t_accounts.First(account => account.ID == deletedAccount.ID);
                Cache.CacheData.t_accounts.Remove(accountDeleting);
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

        #region Upsert

        public static Enums.CRUDResults UpsertSingleAccount(Account upsertedAccount, int personId)
        {
            Account currentAccount = GetAccountById(upsertedAccount.ID);

            if (currentAccount == null)
            {
                return AddNewAccount(upsertedAccount, personId);
            }
            else
            {
                return UpdateSingleAccount(upsertedAccount, personId);
            }
        }

        public static void UpsertMultipleAccounts(List<Account> upsertedList, int personId)
        {
            foreach (Account CurrAccount in upsertedList)
            {
                UpsertSingleAccount(CurrAccount, personId);
            }
        }

        #endregion

        #endregion
        
        #region Private Methods

        internal static List<DataCache.Models.Account> ConvertMultipleLocalAccountsToDbType(List<Account> localTypeAccountList, int personId)
        {
            List<DataCache.Models.Account> dbTypeAccountList = new List<DataCache.Models.Account>();

            foreach (Account CurrAccount in localTypeAccountList)
            {
                dbTypeAccountList.Add((DataCache.Models.Account)ConvertSingleLocalAccountToDbType(CurrAccount, personId));
            }

            return dbTypeAccountList;
        }

        internal static Account ConvertSingleLocalAccountToDbType(Account localTypeAccount, int personId)
        {
            DataCache.Models.Account convertedAccount = new DataCache.Models.Account { ID = localTypeAccount.ID, PersonID = personId };
            convertedAccount.LastMonthlyPaymentDate = localTypeAccount.LastMonthlyPaymentDate;
            return convertedAccount;
        }

        internal static List<Account> ConvertMultipleDbAccountsToLocalType(List<DataCache.Models.Account> dbTypeAccountList)
        {
            if (dbTypeAccountList == null)
            {
                //LOG
                return null;
            }
            List<Account> localTypePhoneTypeList = new List<Account>();

            foreach (DataCache.Models.Account CurrAccount in dbTypeAccountList)
            {
                localTypePhoneTypeList.Add((Account)ConvertSingleDbAccountToLocalType(CurrAccount));
            }
        
            return localTypePhoneTypeList;
        }

        internal static Account ConvertSingleDbAccountToLocalType(DataCache.Models.Account dbTypeAccount)
        {
            if (dbTypeAccount == null)
            {
                //LOG
                return null;
            }

            List<Donation> accountDonations =
                DonationAccess.ConvertMultipleDbDonationsToLocalType(dbTypeAccount.Donations.ToList());

            DateTime lastMonthlyPaymentDate = DateTime.Today;
            if (dbTypeAccount.LastMonthlyPaymentDate.HasValue)
            {
                lastMonthlyPaymentDate = dbTypeAccount.LastMonthlyPaymentDate.Value;
            }

            int monthlyTotal = 0;
                monthlyTotal = dbTypeAccount.MonthlyPaymentTotal;
            //TODO fix
            return null;
            //return new Account(dbTypeAccount.ID, monthlyTotal, lastMonthlyPaymentDate, accountDonations);
        }
        
        #endregion
    }
}
