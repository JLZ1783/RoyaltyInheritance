using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using Microsoft.Dexterity.Applications;

namespace DynamicsGPAddin1.Models
{
    public class BomModel : TrxModel
    {
        public short TrxType { get; set; }
        public string Trx_ID { get ; set; }
        //BM30400.TRX_ID
        public int Component_ID { get; set; }
      
        public string ITEMNMBR { get; set; }

        public string SERLTNUM { get; set; }

        public int NumberOfOrganisms { get; set; }






    }
}
