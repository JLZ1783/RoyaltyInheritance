using DynamicsGPAddin1;
using RoyaltyVendorLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RoyaltyVendorLibrary.DataAccess
{
    //TODO verify IDataConnection isn't necessary then delete if true;
    public interface IDataConnection
    {
        
        void InsertRoyaltyLotVendor();

        void InsertRoyaltyLotEntry(LotRoyaltyEntryModel model);

        void RunInherit(string trxID);

       






    }
}
