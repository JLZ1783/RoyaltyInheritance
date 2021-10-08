using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1
{
    class ContractModel
    {
        public DateTime EffectiveDate { get; set; }

        public decimal StandardPercentage { get; set; }

        public List<string> ItemNumber { get; set; }

        public string Strain { get; set; }
    }
}
