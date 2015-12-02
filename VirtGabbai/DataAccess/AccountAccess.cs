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

        private static t_accounts LookupByAccountId(int accountId)
        {
            try
            {
                return Cache.CacheData.t_accounts.First(currAccount => currAccount.C_id == accountId);
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
                return Cache.CacheData.t_accounts.First(WantedAccount => WantedAccount.C_id == personId);
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
                return (from CurrAccount in Cache.CacheData.t_accounts
                        select CurrAccount).ToList<t_accounts>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_accounts> LookupByMonthlyPaymentTotal(int monthlyTotal)
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.monthly_total == monthlyTotal
                        select CurrAccount).ToList<t_accounts>();
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
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.last_month_paid == lastPayment
                        select CurrAccount).ToList<t_accounts>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_accounts LookupByDonation(int donationId)
        {
            try
            {
                return (from WantedAccount in Cache.CacheData.t_accounts
                        where WantedAccount.t_donations.Any(WantedDonation => WantedDonation.C_id == donationId)
                        select WantedAccount).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_accounts LookupByDonation(Donation donationToLookBy)
        {
            try
            {
                return LookupByDonation(donationToLookBy._Id);
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
                t_accounts newDbAccount = ConvertSingleLocalAccountToDbType(newAccount, personId);
                Cache.CacheData.t_accounts.Add(newDbAccount);
                DonationAccess.UpsertMultipleDonations(newAccount.UnpaidDonations, newAccount._Id);
                DonationAccess.UpsertMultipleDonations(
                    new List<Donation>(newAccount.PaidDonations), newAccount._Id);
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
                DonationAccess.UpsertMultipleDonations(updatedAccount.UnpaidDonations, updatedAccount._Id);
                DonationAccess.UpsertMultipleDonations(new List<Donation>(updatedAccount.PaidDonations), updatedAccount._Id);
                t_accounts accountUpdating = LookupByAccountId(updatedAccount._Id);
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
                t_accounts accountDeleting =
                    Cache.CacheData.t_accounts.First(account => account.C_id == deletedAccount._Id);
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
            Account currentAccount = GetAccountById(upsertedAccount._Id);

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
            t_accounts convertedAccount = new t_accounts { C_id = localTypeAccount._Id,person_id = personId };
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
