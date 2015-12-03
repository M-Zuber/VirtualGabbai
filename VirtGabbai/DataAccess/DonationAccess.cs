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

        private static Donation LookupDonationById(int id)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.ID == id
                        select CurrDonation).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Donation> LookupByReason(string reason)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.Reason == reason
                        select CurrDonation).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Donation> LookupByDonationDate(DateTime donationDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.DonationDate == donationDate
                        select CurrDonation).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Donation> LookupByPaymentDate(DateTime paymentDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.DatePaid == paymentDate
                        select CurrDonation).ToList();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static Donation LookupSpecificDonation(string reason, double amount, DateTime donationDate)
        {
            try
            {
                return (from CurrDonation in Cache.CacheData.t_donations
                        where CurrDonation.Reason == reason &&
                              CurrDonation.Amount == amount &&
                              CurrDonation.DonationDate == donationDate
                        select CurrDonation).First();
            }
            catch (Exception)
            {
                //LOG
                return null;
            }
        }

        private static List<DataCache.Models.Donation> LookupAllDonations(int accountId)
        {
            try
            {
                return (from CurrAccount in Cache.CacheData.t_accounts
                        where CurrAccount.ID == accountId
                        select CurrAccount).First().Donations.ToList<DataCache.Models.Donation>();
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
                DataCache.Models.Donation newDbDonation = ConvertSingleLocalDonationToDbType(newDonation, accountId);
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
                DataCache.Models.Donation donationUpdating = LookupDonationById(updatedDonation.ID);
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
                DataCache.Models.Donation donationDeleting =
                    Cache.CacheData.t_donations.First(donation => donation.ID == deleteDonation.ID);
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
            Donation currentDonation = GetDonationById(upsertedDonation.ID);

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

        internal static List<DataCache.Models.Donation> ConvertMultipleLocalDonationssToDbType(List<Donation> localTypeDonationList, int accountNumber)
        {
            List<DataCache.Models.Donation> dbTypeDonationList = new List<DataCache.Models.Donation>();

            foreach (Donation CurrDonation in localTypeDonationList)
            {
                dbTypeDonationList.Add((DataCache.Models.Donation)ConvertSingleLocalDonationToDbType(CurrDonation, accountNumber));
            }

            return dbTypeDonationList;
        }

        internal static Donation ConvertSingleLocalDonationToDbType(Donation localTypeDonation, int accountNumber)
        {
            DataCache.Models.Donation convertedDonation = DataCache.Models.Donation.Createt_donations(
                localTypeDonation.ID, accountNumber, localTypeDonation.Reason, localTypeDonation.Amount,
                localTypeDonation.DonationDate, localTypeDonation.Paid);
            convertedDonation.Comments = localTypeDonation.Comments;
            convertedDonation.DatePaid = localTypeDonation.DatePaid;

            return convertedDonation;
        }

        internal static List<Donation> ConvertMultipleDbDonationsToLocalType(List<DataCache.Models.Donation> dbTypeDonationList)
        {
            if (dbTypeDonationList == null)
            {
                //LOG
                return null;
            }
            List<Donation> localTypeDonationList = new List<Donation>();

            foreach (DataCache.Models.Donation CurrDonation in dbTypeDonationList)
            {
                localTypeDonationList.Add((Donation)ConvertSingleDbDonationToLocalType(CurrDonation));
            }

            return localTypeDonationList;
        }

        internal static Donation ConvertSingleDbDonationToLocalType(DataCache.Models.Donation dbTypeDonations)
        {
            if (dbTypeDonations == null)
            {
                //LOG
                return null;
            }
            if (dbTypeDonations.Comments == null)
            {
                dbTypeDonations.Comments = "";
            }
            Donation convertedDonation = null;
            if (dbTypeDonations.Paid)
            {
                DateTime paymentDate = DateTime.Today;
                if (dbTypeDonations.DatePaid.HasValue)
                {
                    paymentDate = dbTypeDonations.DatePaid.Value;
                }
                //TODO fix this
                //convertedDonation = new PaidDonation(dbTypeDonations.ID, dbTypeDonations.Reason, dbTypeDonations.Amount,
                //    dbTypeDonations.DonationDate, dbTypeDonations.Comments, paymentDate);
            }
            else
            {
                //convertedDonation = new Donation(dbTypeDonations.ID, dbTypeDonations.Reason, dbTypeDonations.Amount,
                //                    dbTypeDonations.DonationDate, dbTypeDonations.Comments);
            }

            return convertedDonation;
        }

        #endregion
    }
}
