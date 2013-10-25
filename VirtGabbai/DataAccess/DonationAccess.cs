using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;
using Framework;

namespace DataAccess
{
    public static class DonationAccess
    {
        #region Read Methods

        #region Local type return

        private static Donation GetDonationById(int id)
        {
            return ConvertSingleDbDonationToLocalType(LookupDonationById(id));
        }

        private static List<Donation> GetByReason(string reason)
        {
            return ConvertMultipleDbDonationsToLocalType(LookupByReason(reason));
        }

        private static List<Donation> GetByDonationDate(DateTime donationDate)
        {
            return ConvertMultipleDbDonationsToLocalType(LookupByDonationDate(donationDate));
        }

        private static List<Donation> GetByPaymentDate(DateTime paymentDate)
        {
            return ConvertMultipleDbDonationsToLocalType(LookupByPaymentDate(paymentDate));
        }

        private static Donation GetSpecificDonation(string reason, double amount, DateTime donationDate)
        {
            return ConvertSingleDbDonationToLocalType(LookupSpecificDonation(reason, amount, donationDate));
        }

        private static List<Donation> GetAllDonations(int accountId)
        {
            return ConvertMultipleDbDonationsToLocalType(LookupAllDonations(accountId));
        }

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

        public static Enums.CRUDResults AddNewDonation(Donation newDonation)
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

        public static void AddMultipleNewDonations(List<Donation> newDonationList)
        {
            Enums.CRUDResults result;
            foreach (Donation newDonation in newDonationList)
            {
                result = AddNewDonation(newDonation);
                if (result == Enums.CRUDResults.CREATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Update

        public static Enums.CRUDResults UpdateSingleDonation(Donation updatedDonation)
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

        public static void UpdateMultipleDonations(List<Donation> updatedDonationList)
        {
            Enums.CRUDResults result;
            foreach (Donation updatedDonation in updatedDonationList)
            {
                result = UpdateSingleDonation(updatedDonation);
                if (result == Enums.CRUDResults.UPDATE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #region Delete

        public static Enums.CRUDResults DeleteSingleDonation(Donation deleteDonation)
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

        public static void DeleteMultipleDonations(List<Donation> deletedDonationList)
        {
            Enums.CRUDResults result;
            foreach (Donation deletedDonation in deletedDonationList)
            {
                result = DeleteSingleDonation(deletedDonation);
                if (result == Enums.CRUDResults.DELETE_FAIL)
                {
                    //LOG
                }
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_donations> ConvertMultipleLocalDonationssToDbType(List<Donation> localTypeDonationList, int accountNumber)
        {
            List<t_donations> dbTypeDonationList = new List<t_donations>();

            foreach (Donation CurrDonation in localTypeDonationList)
            {
                dbTypeDonationList.Add(ConvertSingleLocalDonationToDbType(CurrDonation, accountNumber));
            }

            return dbTypeDonationList;
        }

        private static t_donations ConvertSingleLocalDonationToDbType(Donation localTypeDonation, int accountNumber)
        {
            t_donations convertedDonation = t_donations.Createt_donations(
                localTypeDonation._Id, accountNumber, localTypeDonation.Reason, localTypeDonation.Amount,
                localTypeDonation.DonationDate, false);
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

        private static List<Donation> ConvertMultipleDbDonationsToLocalType(List<t_donations> dbTypeDonationList)
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

        private static Donation ConvertSingleDbDonationToLocalType(t_donations dbTypeDonations)
        {
            if (dbTypeDonations == null)
            {
                //LOG
                return null;
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
