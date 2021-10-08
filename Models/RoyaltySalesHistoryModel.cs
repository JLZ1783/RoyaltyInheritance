using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1.Models
{
    public class RoyaltySalesHistoryModel
    {
        public string SOPNUMBE { get; set; }
              
        public decimal Percentage { get; set; }

        public string VendorID { get; set; }

        public string ItemNumber { get; set; }
        
        public string LotNumber { get; set; }
               
        public string OGLot { get; set; }

        public decimal MinComboPercentage { get; set; }
        
        public decimal ComboPercentage { get; set; }
        
        public DateTime PostDate { get; set; }
                
        public int NumberOfOrgs { get; set; }


    }
}
