using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseOperations.Models
{
    public class Shipment
    {
        public int ID { get; set; }
        public string Recipient { get; set; }
        public string RecipientZip { get; set; }
        public string Sender { get; set; }
        public string SenderZip { get; set; }
        public string PONumber { get; set; }
        public string PaymentID { get; set; }
        public DateTime DateReceived { get; set; }
        public string BillOfLading { get; set; }
        public string PDFUrl { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
