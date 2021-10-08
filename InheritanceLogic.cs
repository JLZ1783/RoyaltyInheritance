using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using Microsoft.Dexterity.Applications;
using Dapper;
using DynamicsGPAddin1.DataAccess;

using System.Data.SqlClient;
using System.Data;

namespace DynamicsGPAddin1.Models
{
    public static class InheritanceLogic 
    {
        
        public static List<LotRoyaltyEntryModel>RoyaltiesUsed(string trxId)
        {
            List<LotRoyaltyEntryModel> inheritedRoyalties = new List<LotRoyaltyEntryModel>();
            List<BomModel> bmTrx = SqlCrud.GetBomInfo(trxId);
            foreach (BomModel row in bmTrx)
            {
                if (row.Component_ID > 0)
                {

                    List<LotRoyaltyEntryModel> entryModelsUsed = SqlCrud.GetAllLotRoyaltyEntries(row.SERLTNUM);

                    foreach (LotRoyaltyEntryModel royalty in entryModelsUsed)
                    {

                        var t = SqlCrud.GetParentInfo(row.Trx_ID);
                        inheritedRoyalties.Add(new LotRoyaltyEntryModel { ItemNumber = t.ITEMNMBR, LotNumber = t.SERLTNUM, VendorID = royalty.VendorID, Percentage = royalty.Percentage, CurrentTransactionNumber = t.Trx_ID, OGItem = royalty.OGItem, OGLot = royalty.OGLot, NumberOfOrgs = t.NumberOfOrganisms, Description = royalty.Description });

                        //Replaces InheritRoyalties(royalty, t);
                    }
                }
            }

            return inheritedRoyalties;
        }
        public static void ComponentCollection(string trxId)
        {
            List<BomModel> bomComps = new List<BomModel>();
            bomComps = SqlCrud.GetBomInfo(trxId);
        }
    }  
}

