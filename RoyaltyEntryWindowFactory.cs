using Microsoft.Dexterity.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicsGPAddin1
{
    class RoyaltyEntryWindowFactory
    {
        public static DexUIForm WindowPicker()
        {
            if (Controller.Instance.Model.WindowType != 0)
            {

                switch (Controller.Instance.Model.WindowType)
                {
                    case 1:
                        BMRoyaltyLotVendorEntryForm bomRoyalty = new BMRoyaltyLotVendorEntryForm();
                        
                        return bomRoyalty;


                    case 2:
                        RoyaltyLotVendorEntryForm ivRoyaltyEntry = new RoyaltyLotVendorEntryForm();
                        
                        return ivRoyaltyEntry;

                    case 3:
                        RoyaltyLotVendorEntryForm popRoyaltyEntry = new RoyaltyLotVendorEntryForm();
                        return popRoyaltyEntry;

                    case 4:
                        RoyaltyLotVendorEntryForm editLotAttRoyaltyEntry = new RoyaltyLotVendorEntryForm();
                                                
                        return editLotAttRoyaltyEntry;

                    case 5:
                        BMRoyaltyLotVendorEntryForm bmLotRoyaltyInquiry = new BMRoyaltyLotVendorEntryForm();

                        return bmLotRoyaltyInquiry;

                    case 6: BMRoyaltyLotVendorEntryForm ivLotRoyaltyInquiry = new BMRoyaltyLotVendorEntryForm();
                        return ivLotRoyaltyInquiry;

                    case 7:
                        //TODO Create a Sales History View.  Bigger window More columns. 
                        SalesRoyaltyHistoryInquiry sopLotRoyaltyHistoryInquiry = new SalesRoyaltyHistoryInquiry();
                        return sopLotRoyaltyHistoryInquiry;



                }
               
            }
            
            return null;
            
        }
    }
}
