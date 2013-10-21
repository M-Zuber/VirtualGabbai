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
            return null;
        }

        private static List<Donation> GetByReason(string reason)
        {
            return null;
        }

        private static List<Donation> GetByDonationDate(DateTime donationDate)
        {
            return null;
        }

        private static List<Donation> GetByPaymentDate(DateTime paymentDate)
        {
            return null;
        }

        private static Donation GetSpecificDonation(string reason, double amount, DateTime donationDate)
        {
            return null;
        }

        private static List<Donation> GetAllDonations(int accountId)
        {
            return null;
        }

        #endregion

        #region Db type return

        private static t_donations LookupDonationById(int id)
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

        private static List<t_donations> LookupByReason(string reason)
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

        private static List<t_donations> LookupByDonationDate(DateTime donationDate)
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

        private static List<t_donations> LookupByPaymentDate(DateTime paymentDate)
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

        private static t_donations LookupSpecificDonation(string reason, double amount, DateTime donationDate)
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

        private static List<t_donations> LookupAllDonations(int accountId)
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
            bool wasPaid = localTypeDonation.DonationPaid;
            t_donations convertedDonation = t_donations.Createt_donations(
                localTypeDonation._Id, accountNumber, localTypeDonation.Reason, localTypeDonation.Amount,
                localTypeDonation.DonationDate, wasPaid);
            if (wasPaid)
            {
                convertedDonation.date_paid = localTypeDonation.PaymentDate; 
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
                convertedDonation = new Donation(dbTypeDonations.C_id, dbTypeDonations.reason, dbTypeDonations.amount,
                    dbTypeDonations.date_donated, dbTypeDonations.date_paid.Value, dbTypeDonations.comments);
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
