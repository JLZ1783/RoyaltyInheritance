using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DynamicsGPAddin1.DataAccess;
using System.IO;

namespace RoyaltyVendorLibrary.DataAccess
{
    //TODO Verify this isn't needed then delete GlobaConfig if true;
    public static class GlobalConfig
    {
       public static IDataConnection Connection { get; set; }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;

        }

       
     }
    
}
