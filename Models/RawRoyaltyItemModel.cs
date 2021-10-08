using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1.Models
{
    public class RawRoyaltyItemModel
    {
        //Get from Extender Tables 
        //**in theory each item should have unique vendor attached** Can we allow for multiple vendors with this class?
        //B
        
        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

        public string Description { get; set; }


        public string VendorID { get; set; }

        public decimal Percentage { get; set; }

        
    }
}
