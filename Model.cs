using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1
{
    public class Model
    {
        

        public string AssemblyVersion { get; set; }

        public string GPServer { get; set; }
        public string GPSystemDB { get; set; }
        public string GPCompanyDB { get; set; }
        public string GPUserID { get; set; }
        public string GPPassword { get; set; }

        public string TrxID { get; set; }

        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

        public int LineNumber { get; set; }

        public short WindowType { get; set; }

        internal short DeleteType;
    }
}
