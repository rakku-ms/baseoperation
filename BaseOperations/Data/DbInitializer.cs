using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BaseOperations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BaseOperations.Data
{
    public static class DbInitializer
    {
        private static Random rand = new Random();
        //static string storageConnectionString = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONN_STRING");
        //static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

        public static void Initialize(BaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Shipments.Any() && context.Events.Any())
            {
                return;
            }

            var senderZip = RandomNumber(5);
            DateTime start = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            int minsWeek = 10080;

            string urlPrefix = "https://baseopsstorage.blob.core.windows.net";  //storageAccount.BlobEndpoint.ToString() +" ";
            string truckimages = "truckimages";
            string nottruckimages = "nottruckimages";

            List<string> truckimagelist = new List<string> { "car_gate1_0_20191203-151318.jpg", "car_gate1_10_20191203-151321.jpg", "car_gate1_10_20191203-152609.jpg", "car_gate1_11_20191203-151321.jpg", "car_gate1_11_20191203-152609.jpg", "car_gate1_12_20191203-151322.jpg", "car_gate1_12_20191203-152609.jpg", "car_gate1_13_20191203-152610.jpg", "car_gate1_14_20191203-151322.jpg", "car_gate1_14_20191203-152610.jpg", "car_gate1_17_20191203-151322.jpg", "car_gate1_1_20191203-151318.jpg", "car_gate1_2_20191203-151318.jpg", "car_gate1_3_20191203-151319.jpg", "car_gate1_4_20191203-151319.jpg", "car_gate1_7_20191203-151320.jpg", "car_gate1_7_20191203-152607.jpg", "car_gate1_8_20191203-151320.jpg", "car_gate1_8_20191203-152608.jpg", "car_gate1_9_20191203-152608.jpg" };

            List<string> nottruckimagelist = new List<string> { "car_108_20191203-112632.jpg", "car_109_20191203-112632.jpg", "car_10_20191203-112601.jpg", "car_111_20191203-112633.jpg", "car_11_20191203-112602.jpg", "car_13_20191203-112602.jpg", "car_148_20191203-112646.jpg", "car_149_20191203-112646.jpg", "car_150_20191203-112647.jpg", "car_158_20191203-112649.jpg", "car_159_20191203-112650.jpg", "car_15_20191203-112606.jpg", "car_161_20191203-112651.jpg", "car_162_20191203-112651.jpg", "car_16_20191203-112606.jpg", "car_202_20191203-112705.jpg", "car_203_20191203-112705.jpg", "car_209_20191203-112707.jpg", "car_217_20191203-112710.jpg", "car_218_20191203-112711.jpg", "car_219_20191203-112711.jpg", "car_245_20191203-112719.jpg", "car_246_20191203-112719.jpg", "car_251_20191203-112720.jpg", "car_252_20191203-112721.jpg", "car_259_20191203-112724.jpg", "car_260_20191203-112724.jpg", "car_261_20191203-112724.jpg", "car_273_20191203-112728.jpg", "car_274_20191203-112728.jpg", "car_286_20191203-112732.jpg", "car_288_20191203-112733.jpg", "car_304_20191203-112737.jpg", "car_305_20191203-112738.jpg", "car_307_20191203-112739.jpg", "car_383_20191203-112755.jpg", "car_384_20191203-112756.jpg", "car_411_20191203-112802.jpg", "car_413_20191203-112803.jpg", "car_477_20191203-112815.jpg", "car_478_20191203-112816.jpg", "car_479_20191203-112816.jpg", "car_557_20191203-112836.jpg", "car_576_20191203-112842.jpg", "car_577_20191203-112842.jpg", "car_578_20191203-112843.jpg", "car_580_20191203-112844.jpg", "car_582_20191203-112844.jpg", "car_583_20191203-112845.jpg", "car_584_20191203-112846.jpg", "car_585_20191203-112846.jpg", "car_593_20191203-112849.jpg", "car_604_20191203-112852.jpg", "car_63_20191203-112618.jpg", "car_707_20191203-112918.jpg", "car_708_20191203-112918.jpg", "car_718_20191203-112921.jpg", "car_719_20191203-112921.jpg", "car_720_20191203-112922.jpg", "car_722_20191203-112923.jpg", "car_731_20191203-112925.jpg", "car_76_20191203-112622.jpg", "car_77_20191203-112622.jpg", "car_78_20191203-112623.jpg", "car_81_20191203-112623.jpg", "car_94_20191203-112627.jpg", "car_95_20191203-112628.jpg" };

            var shipments = new Shipment[]
            {
                new Shipment{Recipient="Jamie Evans", RecipientZip=RandomNumber(5), Sender="Central Mailing", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,6), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Markus Long", RecipientZip=RandomNumber(5), Sender="Central Mailing", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,6), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Graham Barnes", RecipientZip=RandomNumber(5), Sender="Catering", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,5), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Avery Howard", RecipientZip=RandomNumber(5), Sender="Procurement Office", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,5), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Gabriel Diaz", RecipientZip=RandomNumber(5), Sender="Catering", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,5), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Caleb Foster", RecipientZip=RandomNumber(5), Sender="Central Mailing", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,4), PaymentID = RandomString(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Wesley Brooks", RecipientZip=RandomNumber(5), Sender="Personal", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,3), PaymentID = RandomNumber(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Ellis Turner", RecipientZip=RandomNumber(5), Sender="Central Mailing", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,3), PaymentID = RandomNumber(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now},
                new Shipment{Recipient="Jesse Irwin", RecipientZip=RandomNumber(5), Sender="Personal", SenderZip=senderZip, PONumber= RandomNumber(6), DateReceived = new DateTime(2019,12,3), PaymentID = RandomNumber(10), BillOfLading = RandomString(10), PDFUrl = "#", TimeAdded = DateTime.Now}
            };

            foreach (Shipment s in shipments)
            {
                context.Shipments.Add(s);
            }
            context.SaveChanges();

            string truckurlprefix = urlPrefix + "/truckimages/";
            string nottruckurlprefix = urlPrefix + "/nottruckimages/";

            var truckEvents = new Event[]
            {
                new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = truckurlprefix + truckimagelist.ElementAt(rand.Next(truckimagelist.Count)), ContainsTruck = true },
                new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = truckurlprefix + truckimagelist.ElementAt(rand.Next(truckimagelist.Count)), ContainsTruck = true },
                new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = truckurlprefix + truckimagelist.ElementAt(rand.Next(truckimagelist.Count)), ContainsTruck = true },
                new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = truckurlprefix + truckimagelist.ElementAt(rand.Next(truckimagelist.Count)), ContainsTruck = true },
                new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = truckurlprefix + truckimagelist.ElementAt(rand.Next(truckimagelist.Count)), ContainsTruck = true}
            };

            var nottruckevents = new Event[]
            {
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false },
                 new Event{Timestamp = start.AddMinutes(rand.Next(minsWeek)), Location = "Gate" + rand.Next(1,4), ImageURL = nottruckurlprefix + nottruckimagelist.ElementAt(rand.Next(nottruckimagelist.Count)), ContainsTruck = false }
            };

            foreach (Event e in truckEvents)
            {
                context.Events.Add(e);
            }

            foreach (Event e in nottruckevents)
            {
                context.Events.Add(e);
            }

            context.SaveChanges();
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
