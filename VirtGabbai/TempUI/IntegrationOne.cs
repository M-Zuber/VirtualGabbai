//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataAccess;
//using LocalTypes;
//using Framework;
//using DataCache.Models;

//namespace TempUI
//{
//    public class IntegrationOne
//    {
//        public static int donationId = 1;
//        public static PhoneType cellPhone = new PhoneType(1, "cell phone");
//        public static PhoneType housePhone = new PhoneType(2, "house phone");
//        public static PhoneType workPhone = new PhoneType(3, "work phone");
//        public static List<PhoneNumber> jackNumbers = new List<PhoneNumber>() 
//        {
//            new PhoneNumber(1, "0527652367", cellPhone),
//            new PhoneNumber(2, "0547453695", cellPhone),
//            new PhoneNumber(3, "027413652", housePhone),
//            new PhoneNumber(4, "035842569", workPhone)
//        };
//        public static List<Yahrtzieht> jackYahrtzieht = new List<Yahrtzieht>()
//        {
//            new Yahrtzieht(1, new DateTime(1945,6,12), "hadad ben hadad", "first kill"),
//            new Yahrtzieht(2, new DateTime(1993,1,25), "Teal'c", "best friend")
//        };
//        public static List<Donation> jackDonations = new List<Donation>() 
//        {
//            //new Donation(donationId++, "for a seat", 653, new DateTime(2012, 12,12), "did not get the seat he wanted"),
//            //new PaidDonation(donationId++, "in honor of his birthday", 1250, new DateTime(2013, 7, 1), "", new DateTime(2013,7,1))
//        };
//        public static Account jackAccount = new Account();
//        public LocalTypes.Person Jack = new LocalTypes.Person(1, "jacko'neill@sg1.sgc", "Jack", "O'niell", true,
//            "12;76;Cheyeene mountain;Denver;CO;usa;524398", jackAccount,
//            jackNumbers, jackYahrtzieht);
//    }
//}
