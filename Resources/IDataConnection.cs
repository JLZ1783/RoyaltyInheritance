using RoyaltyVendorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoyaltyVendorLibrary.DataAccess
{
    public interface IDataConnection
    {
        
        List<VendorModel> AssignRoyaltyVendors();
        
    }
}
