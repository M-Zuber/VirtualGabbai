using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using LocalTypes;
using Framework;
using DataCache.Models;

namespace DataAccess
{
    public static class DonationAccess
    {
        #region Read Methods

        #region Local type return

        public static Donation GetDonationById(int id) => ConvertSingleDbDonationToLocalType(LookupDonationById(id));

        public static List<Donation> GetByReason(string reason) => ConvertMultipleDbDonationsToLocalType(LookupByReason(reason));

        public static List<Donation> GetByDonationDate(DateTime donationDate) => ConvertMultipleDbDonationsToLocalType(LookupByDonationDate(donationDate));

        public static List<Donation> GetByPaymentDate(DateTime paymentDate) => ConvertMultipleDbDonationsToLocalType(LookupByPaymentDate(paymentDate));

        public static Donation GetSpecificDonation(string reason, double amount, DateTime donationDate) => ConvertSingleDbDonationToLocalType(LookupSpecificDonation(reason, amount, donationDate));

        public static List<Donation> GetAllDonations(int accountId) => ConvertMultipleDbDonationsToLocalType(LookupAllDonations(accountId));

        #endregion

        #region Db type return

        private static t_donations LookupDonationById(int id)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.C_id == id
                        select CurrDonation).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_donations> LookupByReason(string reason)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.reason == reason
                        select CurrDonation).ToList<t_donations>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_donations> LookupByDonationDate(DateTime donationDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.date_donated == donationDate
                        select CurrDonation).ToList<t_donations>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_donations> LookupByPaymentDate(DateTime paymentDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.date_paid == paymentDate
                        select CurrDonation).ToList<t_donations>();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static t_donations LookupSpecificDonation(string reason, double amount, DateTime donationDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.reason == reason &&
                              CurrDonation.amount == amount &&
                              CurrDonation.date_donated == donationDate
                        select CurrDonation).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<t_donations> LookupAllDonations(int accountId)
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.C_id == accountId
                        select CurrAccount).First().t_donations.ToList<t_donations>();
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

        public static Enums.CRUDResults AddNewDonation(Donation newDonation, int accountId)
        {
            try
            {
                t_donations newDbDonation = ConvertSingleLocalDonationToDbType(newDonation, accountId);
                Cache.CacheData.t_donations.Add(newDbDonation);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.CREATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.CREATE_FAIL;
            }
        }

        public static void AddMultipleNewDonations(List<Donation> newDonationList, int accountId)
        {
            Enums.CRUDResults result;
            foreach (Donation newDonation in newDonationList)
            {
                result = AddNewDonation(newDonation, accountId);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSingleDonation(Donation updatedDonation, int accountId)
        {
            try
            {
                t_donations donationUpdating = LookupDonationById(updatedDonation._Id);
                donationUpdating = ConvertSingleLocalDonationToDbType(updatedDonation, accountId);
                Cache.CacheData.t_donations.Attach(donationUpdating);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.UPDATE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.UPDATE_FAIL;
            }
        }

        public static void UpdateMultipleDonations(List<Donation> updatedDonationList, int accountId)
        {
            Enums.CRUDResults result;
            foreach (Donation updatedDonation in updatedDonationList)
            {
                result = UpdateSingleDonation(updatedDonation, accountId);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSingleDonation(Donation deleteDonation, int accountId)
        {
            try
            {
                t_donations donationDeleting =
                    Cache.CacheData.t_donations.First(donation => donation.C_id == deleteDonation._Id);
                Cache.CacheData.t_donations.Remove(donationDeleting);
                Cache.CacheData.SaveChanges();
                return Enums.CRUDResults.DELETE_SUCCESS;
            }
            catch (Exception)
            {
                //LOG
                return Enums.CRUDResults.DELETE_FAIL;
            }
        }

        public static void DeleteMultipleDonations(List<Donation> deletedDonationList, int accountId)
        {
            Enums.CRUDResults result;
            foreach (Donation deletedDonation in deletedDonationList)
            {
                result = DeleteSingleDonation(deletedDonation, accountId);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Upsert

        public static Enums.CRUDResults UpsertSingleDonation(Donation upsertedDonation, int accountId)
        {
            Donation currentDonation = GetDonationById(upsertedDonation._Id);

            if (currentDonation == null)
            {
                return AddNewDonation(upsertedDonation, accountId);
            }
            else
            {
                return UpdateSingleDonation(upsertedDonation, accountId);
            }
        }

        public static void UpsertMultipleDonations(List<Donation> upsertedList, int accountId)
        {
            foreach (Donation CurrDonation in upsertedList)
            {
                UpsertSingleDonation(CurrDonation, accountId);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        internal static List<t_donations> ConvertMultipleLocalDonationssToDbType(List<Donation> localTypeDonationList, int accountNumber)
        {
            List<t_donations> dbTypeDonationList = new List<t_donations>();

            foreach (Donation CurrDonation in localTypeDonationList)
            {
                dbTypeDonationList.Add(ConvertSingleLocalDonationToDbType(CurrDonation, accountNumber));
            }

            return dbTypeDonationList;
        }

        internal static t_donations ConvertSingleLocalDonationToDbType(Donation localTypeDonation, int accountNumber)
        {
            t_donations convertedDonation = t_donations.Createt_donations(
                localTypeDonation._Id, accountNumber, localTypeDonation.Reason, localTypeDonation.Amount,
                localTypeDonation.DonationDate, false);
            convertedDonation.comments = localTypeDonation.Comments;
            if (localTypeDonation is PaidDonation)
            {
                convertedDonation.paid = true;
                convertedDonation.date_paid = (localTypeDonation as PaidDonation).PaymentDate; 
            }
            else
            {
                convertedDonation.date_paid = null;
            }
            return convertedDonation;
        }

        internal static List<Donation> ConvertMultipleDbDonationsToLocalType(List<t_donations> dbTypeDonationList)
        {
            if (dbTypeDonationList == null)
            {
                //LOG
                return null;
            }
            List<Donation> localTypeDonationList = new List<Donation>();

            foreach (t_donations CurrDonation in dbTypeDonationList)
            {
                localTypeDonationList.Add(ConvertSingleDbDonationToLocalType(CurrDonation));
            }

            return localTypeDonationList;
        }

        internal static Donation ConvertSingleDbDonationToLocalType(t_donations dbTypeDonations)
        {
            if (dbTypeDonations == null)
            {
                //LOG
                return null;
            }
            if (dbTypeDonations.comments == null)
            {
                dbTypeDonations.comments = "";
            }
            Donation convertedDonation;
            if (dbTypeDonations.paid)
            {
                DateTime paymentDate = DateTime.Today;
                if (dbTypeDonations.date_paid.HasValue)
                {
                    paymentDate = dbTypeDonations.date_paid.Value;
                }
                convertedDonation = new PaidDonation(dbTypeDonations.C_id, dbTypeDonations.reason, dbTypeDonations.amount,
                    dbTypeDonations.date_donated, dbTypeDonations.comments, paymentDate);
            }
            else
            {
                convertedDonation = new Donation(dbTypeDonations.C_id, dbTypeDonations.reason, dbTypeDonations.amount,
                                    dbTypeDonations.date_donated, dbTypeDonations.comments);
            }

            return convertedDonation;
        }

        #endregion
    }
}
