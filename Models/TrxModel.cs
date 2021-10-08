using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1.Models
{
    public class TrxModel
     {
        public string BatchNumber { get; set; }
        public short TrxType { get; set; }
          
        public string Trx_ID { get; set; }
        //BM30400.TRX_ID
        //public int Component_ID { get; set; } Only for BOMTRX

        public string ITEMNMBR { get; set; }

        public string SERLTNUM { get; set; }

 
    }
}
