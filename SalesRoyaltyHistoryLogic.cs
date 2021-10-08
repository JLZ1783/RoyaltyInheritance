using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicsGPAddin1.Models;

namespace DynamicsGPAddin1
{
    public static class SalesRoyaltyHistoryLogic
    {
        //TODO Get batch info from batches containing one sales with one or more royalties  
        //loop through filtered batch 
        //OR create the list of trxModel by querying inner join sop10201 , sop30300, sop30200 (for date), JLz_RoyaltyLotAtt (for most current ) where SOPNUMBE NOT in sop30300.
 
            

        public static List<LotRoyaltyEntryModel> RoyaltiesOnSopTrx(string trxId)
        {
             
            List<LotRoyaltyEntryModel> soldRoyaltyItems = new List<LotRoyaltyEntryModel>();
            List<TrxModel> sopTrx = SqlCrud.GetSopInfo(trxId);
            foreach (TrxModel row in sopTrx)
            {
                

                    List<LotRoyaltyEntryModel> entryModelsUsed = SqlCrud.GetAllLotRoyaltyEntries(row.SERLTNUM);

                    foreach (LotRoyaltyEntryModel royalty in entryModelsUsed)
                    {

                        var t = SqlCrud.GetParentInfo(row.Trx_ID);
                        soldRoyaltyItems.Add(new LotRoyaltyEntryModel { ItemNumber = t.ITEMNMBR, LotNumber = t.SERLTNUM, VendorID = royalty.VendorID, Percentage = royalty.Percentage, CurrentTransactionNumber = t.Trx_ID, OGItem = royalty.OGItem, OGLot = royalty.OGLot });

                        //Replaces InheritRoyalties(royalty, t);
                    }
                
            }

            return soldRoyaltyItems;
        }
    }
}
