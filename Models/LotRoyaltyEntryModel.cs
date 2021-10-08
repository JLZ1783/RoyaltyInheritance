using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1
{
   
    public class LotRoyaltyEntryModel
    {
        public string CurrentTransactionNumber { get; set; }

        //public string OrigTransactionNumber { get; set; }

        public decimal Percentage { get; set; }

        public string VendorID { get; set; }

        public string ItemNumber { get; set; }
        public string LotNumber { get; set; }

        public string OGItem { get; set; }

        public string OGLot { get; set; }

        public string Description { get; set; }
        public int NumberOfOrgs { get; set; }
        public int RLTSEQNUM { get; set; }



    }

}
