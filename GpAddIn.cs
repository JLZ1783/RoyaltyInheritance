using DynamicsGPAddin1.Models;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using Microsoft.Dexterity.Applications.MenusForVisualStudioToolsDictionary;
using Microsoft.Dexterity.Applications.SsgDictionary;
using Microsoft.Dexterity.Bridge;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using BmTrxDocDetailEntryForm = Microsoft.Dexterity.Applications.SsgDictionary.BmTrxDocDetailEntryForm;
using BmTrxSerialLotEntryForm = Microsoft.Dexterity.Applications.SsgDictionary.BmTrxSerialLotEntryForm;
using IvLotAttributesEditForm = Microsoft.Dexterity.Applications.SsgDictionary.IvLotAttributesEditForm;
using IvLotAttributesForm = Microsoft.Dexterity.Applications.DynamicsDictionary.IvLotAttributesForm;
using IvLotAttributesInquiryForm = Microsoft.Dexterity.Applications.SsgDictionary.IvLotAttributesInquiryForm;

namespace DynamicsGPAddin1
{
    public class GPAddIn : IDexterityAddIn
    {
        //HACK Remove min royalty % as parameter everywhere
        //HACK 
        //HACK create mechanism to consolidate vendor items used and get combination percentage totals for each sale. Could be done at the sql report level
        //TODO Configure temp tables so that datagridview shows all the current state of the list per transaction.  //FORNOW Save it and go back to view parent   
        //TODO Royalty Entry ID's :  Use ID to prevent duplicates at the parent level. Using Lot Number as ID and SELECT DISTINCT 
        //HACK Connect to SQL one time at start of application. However, Dapper closes the connection each call with using() call. 

        // IDexterityAddIn interface
        public static IvTransactionEntryForm ivTransactionEntryForm = Dynamics.Forms.IvTransactionEntry;
        public static IvLotAttributesForm ivLotAttributesForm = Dynamics.Forms.IvLotAttributes;
        public static IvLotAttributesEditForm IvLotAttributesEditForm = Ssg.Forms.IvLotAttributesEdit;
        public static Microsoft.Dexterity.Applications.DynamicsDictionary.IvLotAttributesEditForm IvLotAttributesEdit = Dynamics.Forms.IvLotAttributesEdit;
        public static BmTrxSerialLotEntryForm bmTrxSerialLotEntryForm = Ssg.Forms.BmTrxSerialLotEntry;
        public static IvLotAttributesForm ssgIvLotAttributes = Ssg.Forms.IvLotAttributes;
        public static IvLotAttributesInquiryForm ivLotAttributesInquiryForm = Ssg.Forms.IvLotAttributesInquiry;
        public static BmTrxEntryForm BmTrxEntryForm = Dynamics.Forms.BmTrxEntry;
        public static PopReceivingsEntryForm PopReceivingsEntryForm = Dynamics.Forms.PopReceivingsEntry;
        public static IvLotNumberInquiryForm IvLotNumberInquiryForm = Dynamics.Forms.IvLotNumberInquiry;
        public static SopBatchEntryForm SopBatchEntryForm = Dynamics.Forms.SopBatchEntry;
        public static PopInquirySerialLotForm PopInquirySerialLotForm = Dynamics.Forms.PopInquirySerialLot;
        public void Initialize()
        {

            Ssg.Forms.IvLotAttributes.AddMenuHandler(OpenRoyaltyEntryForm, "Royalty Entry/Inquiry");
            Ssg.Forms.IvLotAttributesInquiry.AddMenuHandler(OpenBMRoyaltyEntryForm, "Royalty Entry/Inquiry");
            Ssg.Forms.IvLotAttributesEdit.AddMenuHandler(OpenEditLotAttRoyaltyEntryForm, "Edit Lot Royalties");
            Ssg.Procedures.SsgBmSaveButton.InvokeAfterOriginal += SsgBmSaveButton_InvokeAfterOriginal;
            Ssg.Forms.IvLotAttributes.IvLotAttributes.CloseAfterOriginal += IvLotAttributes_CloseAfterOriginal;
            Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.CloseAfterOriginal += IvLotAttributesInquiry_CloseAfterOriginal;
            Dynamics.Forms.IvBatchEntry.IvBatchEntry.DeleteButton.ClickBeforeOriginal += IVBatchDeleteButton_ClickBeforeOriginal;
            Dynamics.Forms.IvBatchEntry.IvBatchEntry.Origin.ValidateAfterOriginal += Origin_ValidateAfterOriginal;

            //Receiving
            Dynamics.Forms.PopLotEntry.PopLotEntry.ExpansionButton1.ClickAfterOriginal += SetAsInquiryWinodwOnAttEntry;
            Dynamics.Forms.PopLotEntry.PopLotEntry.ExpansionButton2.ClickAfterOriginal += PopLotEntryExpansionButton2_ClickAfterOriginal;
            Dynamics.Forms.PopLotEntry.PopLotEntry.RemoveButtonR.ClickBeforeOriginal += PopLotEntryRemoveButtonR_ClickBeforeOriginal;
            Dynamics.Forms.PopLotEntry.PopLotEntry.RemoveAllButtonMnemonic.ClickBeforeOriginal += PopLotEntryRemoveAllButton_ClickBeforeOriginal;
            Ssg.Procedures.SsgIvLotAttributesDelete.InvokeAfterOriginal += SsgIvLotAttributesDelete_InvokeAfterOriginal;
            
            Dynamics.Forms.PopLotLine.Procedures.DelForReceiptLine.InvokeBeforeOriginal += DelForReceiptLine_InvokeBeforeOriginal;
            Dynamics.Forms.PopBatchEntry.PopBatchEntry.DeleteButton.ClickBeforeOriginal += PopBatchDeleteButton_ClickBeforeOriginal;

            Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.LineScroll.LineDeleteBeforeOriginal += PopLineScroll_LineDeleteBeforeOriginal;
            Dynamics.Forms.PopReceivingsItemDetail.PopReceivingsItemDetail.DeleteButton.ClickBeforeOriginal += PopLineDetailDeleteButton_ClickBeforeOriginal;
            Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.DeleteButton.ClickBeforeOriginal += PopDeleteButton_ClickBeforeOriginal;
            Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.VoidButtonI.ClickBeforeOriginal += PopVoidButtonI_ClickBeforeOriginal;

            //Handle Deleting or voiding POP records

            //Assembly Entry
            Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ExpansionButton3.ClickAfterOriginal += BmExpansionButton3_ClickAfterOriginal;
            Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ExpansionButton2.ClickAfterOriginal += SetAsInquiryWindow_BmLotAttEntry;
            Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ExpansionButton1.ClickAfterOriginal += SetAsInquiryWindow_BmLotAttEntry;

            Dynamics.Forms.BmTrxComp.Procedures.DelForTrxId.InvokeBeforeOriginal += BmDelForTrxId_InvokeBeforeOriginal;

            //Handle Deleting or voiding BM records

            //Edit Lot Attributes
            Ssg.Forms.IvLotAttributesEdit.IvLotAttributesEdit.OpenAfterOriginal += SsgIvLotAttributesEdit_OpenAfterOriginal;


            //Iv Transaction Entry
            Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.ExpansionButton1.ClickAfterOriginal += SetAsInquiryWinodwOnAttEntry;
            Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.ExpansionButton2.ClickAfterOriginal += IvLotEntryExpansionButton2_ClickAfterOriginal;
            Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.RemoveButtonR.ClickBeforeOriginal += IvRemoveButtonR_ClickBeforeOriginal;
            Dynamics.Procedures.IvRemoveLotNumbers.InvokeBeforeOriginal += IvRemoveLotNumbers_InvokeBeforeOriginal;
            Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.RemoveAllButton.ClickBeforeOriginal += IvRemoveAllButton_ClickBeforeOriginal;
            Dynamics.Procedures.IvDeleteDocument.InvokeBeforeOriginal += IvDeleteDocument_InvokeBeforeOriginal;
            Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.DeleteButton.ClickBeforeOriginal += IvDeleteButton_ClickBeforeOriginal;
            Dynamics.Procedures.IvDeleteLineItem.InvokeBeforeOriginal += IvDeleteLineItem_InvokeBeforeOriginal;
            Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvTransactionScroll.LineDeleteBeforeOriginal += IvTransactionScroll_LineDeleteBeforeOriginal;

            //Handle Deleting or voiding IV Transaction records

            

            //Inquiry windows
            Dynamics.Forms.IvLotNumberInquiry.IvLotNumberInquiry.ExpansionButton.ClickAfterOriginal += SetAsInquiryWindow;
            Dynamics.Forms.BmTrxSerialLotInquiryZoom.BmTrxSerialLotInquiryZoom.ExpansionButton.ClickAfterOriginal += SetAsInquiryWindow;
            Dynamics.Forms.SopLotEntry.SopLotEntry.ExpansionButton3.ClickAfterOriginal += SetAsInquiryWindow;
            Dynamics.Forms.PopInquirySerialLot.PopInquirySerialLot.ExpansionButton.ClickAfterOriginal += SetAsInquiryWindow;

            //TODO add inquiry windows on IV transfers

            //Sales Entry
            Ssg.Procedures.SsgSopLotEntryExpansion1.InvokeBeforeOriginal += SetAsInquiryWindow;
            Ssg.Procedures.SsgSopLotEntryExpansion2.InvokeBeforeOriginal += SetAsInquiryWindow;
            Dynamics.Forms.SopSerialLotInquiry.SopSerialLotInquiry.ExpansionButton.ClickBeforeOriginal += SetAsRoyaltyHistoryInquiry;
            Dynamics.Forms.SopSerialLotInquiry.AddMenuHandler(OpenSalesRoyaltyHistory, "Sales Royalty History");
            //BatchPosting
            //Dynamics.Forms.SopBatchEntry.SopBatchEntry.PostButton.ClickBeforeOriginal += PostButton_ClickBeforeOriginal;
            //Dynamics.Forms.SopBatchEntry.Procedures.PostBatch.InvokeBeforeOriginal += PostBatch_InvokeBeforeOriginal;
            //Dynamics.Procedures.SopPostCreateLineHistory.InvokeAfterOriginal += SopPostCreateLineHistory_InvokeAfterOriginal;
            Dynamics.Forms.SopBatchEntry.SopBatchEntry.BatchNumber.Change += BatchNumber_Change;
            Dynamics.Forms.SopBatchEntry.SopBatchEntry.CloseAfterOriginal += SopBatchEntry_CloseAfterOriginal;
            Dynamics.Procedures.SopPostDeleteWorkRecords.InvokeAfterOriginal += SopPostDeleteWorkRecords_InvokeAfterOriginal;
            SopBatchEntryForm.AddMenuHandler(RunSalesRoyaltyHistoryPush, "Royalty History Push");

            
        }

        private void PopLineDetailDeleteButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            SetAsDeleteLineDetail();
        }

        private void OpenSalesRoyaltyHistory(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Sales
        private void SopPostDeleteWorkRecords_InvokeAfterOriginal(object sender, SopPostDeleteWorkRecordsProcedure.InvokeEventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            string batchNumber = Controller.Instance.TrxModel.BatchNumber;
            if (!String.IsNullOrWhiteSpace(batchNumber))
            {
                SqlCrud.CaptureRoyaltySalesHistory(batchNumber);
                MessageBox.Show("SopPostDeleteWorkRecords Event Fired");
            }
        }

        private void RunSalesRoyaltyHistoryPush(object sender, EventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            if (!String.IsNullOrWhiteSpace(SopBatchEntryForm.SopBatchEntry.BatchNumber.Value))
            {
                SqlCrud.CaptureRoyaltySalesHistory(SopBatchEntryForm.SopBatchEntry.BatchNumber.Value);
            }
        }

        private void SopBatchEntry_CloseAfterOriginal(object sender, EventArgs e)
        {
            Controller.Instance.TrxModel.BatchNumber = "";
        }

        private void BatchNumber_Change(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(SopBatchEntryForm.SopBatchEntry.BatchNumber.Value))
            {
                var batchNumber = SopBatchEntryForm.SopBatchEntry.BatchNumber.Value;
                Controller.Instance.SetTrxBatchNumber(batchNumber);
            }

        }

        #endregion Sales

        #region Purchasing

        //TODO Add to RawRoyaltyMaster along with RoyaltyLotAtt(pool)

        private void PopVoidButtonI_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            SetAsDeleteTransaction();
        }

        private void PopBatchDeleteButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            Controller.Instance.SetDeleteType(6);
        }

        private void PopLineScroll_LineDeleteBeforeOriginal(object sender, CancelEventArgs e)
        {
            Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke("Deleting this record will delete all royalties referencing this receipt and line number.");
            SetAsDeleteLine();
        }

        private void PopDeleteButton_ClickBeforeOriginal(object sender, EventArgs e)
        {
            Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke("Deleting this record will delete all royalties referencing this receipt number.");
            SetAsDeleteTransaction();
        }

        private void DelForReceiptLine_InvokeBeforeOriginal(object sender, PopLotLineForm.DelForReceiptLineProcedure.InvokeEventArgs e)
        {
            //This fires for every line. 
            Controller.Instance.SetConnectionInfo();

            var deleteType = Controller.Instance.Model.DeleteType;

            switch (deleteType)
            {
                case 1:
                    var trxId = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.PopReceiptNumber.Value.ToString();
                    try
                    {

                        SqlCrud.RemoveAllPopLotRoyalties(trxId);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;

                case 2:
                    trxId = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.PopReceiptNumber.Value.ToString();
                    var itemNumber = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.LineScroll.ItemNumber.Value.ToString();
                    var rcptLine = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.LineScroll.ReceiptLineNumber.Value;
                    if (!string.IsNullOrEmpty(trxId) && !string.IsNullOrEmpty(itemNumber) && rcptLine > 0)
                    {

                        try
                        {

                            SqlCrud.RemovePopLotRoyalty(trxId, itemNumber, rcptLine);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    break;
                case 3:
                    trxId = Dynamics.Forms.PopReceivingsItemDetail.PopReceivingsItemDetail.PopReceiptNumber.Value.ToString();
                    itemNumber = Dynamics.Forms.PopReceivingsItemDetail.PopReceivingsItemDetail.ItemNumber.Value.ToString();
                    rcptLine = Dynamics.Forms.PopReceivingsItemDetail.PopReceivingsItemDetail.ReceiptLineNumber.Value;
                    if (!string.IsNullOrEmpty(trxId) && !string.IsNullOrEmpty(itemNumber) && rcptLine > 0)
                    {

                        try
                        {

                            SqlCrud.RemovePopLotRoyalty(trxId, itemNumber, rcptLine);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    break;
                case 6:

                    try
                    {
                        var batch = Dynamics.Forms.PopBatchEntry.PopBatchEntry.BatchNumber.Value.ToString();

                        SqlCrud.RemovePopRoyaltiesByBatch(batch);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
            }




        }

        private void PopLotEntryRemoveAllButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            int answer = Dynamics.Forms.SyVisualStudioHelper.Functions.DexAsk.Invoke("Any royalties associated with these lots have been removed. If you did not wish to remove these lots click cancel at the next prompt and add the appropriate royalties.", "Continue", "", "");
            switch (answer)
            {
                case 1:
                    Controller.Instance.SetConnectionInfo();
                    var trxId = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.PopReceiptNumber.Value.ToString();
                    var itemNumber = Dynamics.Forms.PopLotEntry.PopLotEntry.ItemNumber.Value.ToString();
                    var rcptLine = Dynamics.Forms.PopLotEntry.Tables.PopSerialLot.ReceiptLineNumber.Value;
                    SqlCrud.RemovePopLotRoyalty(trxId, itemNumber, rcptLine);
                    break;

                case 2:
                    //TODO gain access to the dialog box that pops after remove all. Once found add cancel button and listen for user response.  
                    e.Cancel = true;
                    break;

            }


        }

        private void PopLotEntryRemoveButtonR_ClickBeforeOriginal(object sender, EventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            var trxId = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.PopReceiptNumber.Value.ToString();
            var itemNumber = Dynamics.Forms.PopLotEntry.PopLotEntry.ItemNumber.Value.ToString();
            var lotNumber = Dynamics.Forms.PopLotEntry.PopLotEntry.SelectedScroll.SerialLotNumber.Value.ToString();
            SqlCrud.RemovePopLotRoyalty(trxId, itemNumber, lotNumber);
        }

        private void PopLotEntryExpansionButton2_ClickAfterOriginal(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(3);
        }

        #endregion Purchasing

        #region Inventory
        private void IvLotEntryExpansionButton2_ClickAfterOriginal(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(2);
        }
        private void Origin_ValidateAfterOriginal(object sender, EventArgs e)
        {
            var type = Dynamics.Forms.IvBatchEntry.IvBatchEntry.Origin.Value;
            MessageBox.Show($"{type}");
        }
        private void IVBatchDeleteButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {

            var type = Dynamics.Forms.IvBatchEntry.IvBatchEntry.Origin.Value;


            switch (type)
            {
                case 1:
                    Controller.Instance.SetDeleteType(4);

                    break;

                case 3:
                    Controller.Instance.SetDeleteType(5);

                    break;

            }
        }
        private void IvTransactionScroll_LineDeleteBeforeOriginal(object sender, CancelEventArgs e)
        {
            SetAsDeleteLine();
        }

        private void IvDeleteLineItem_InvokeBeforeOriginal(object sender, IvDeleteLineItemProcedure.InvokeEventArgs e)
        {
            IvDeleteRoyalties();
        }

        private void IvDeleteButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {

            SetAsDeleteTransaction();
        }

        private void IvDeleteDocument_InvokeBeforeOriginal(object sender, IvDeleteDocumentProcedure.InvokeEventArgs e)
        {
            IvDeleteRoyalties();

        }

        private void IvDeleteRoyalties()
        {
            Controller.Instance.SetConnectionInfo();

            var removeType = Controller.Instance.Model.DeleteType;

            switch (removeType)
            {
                case 1:
                    var trxId = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvDocumentNumber.Value.ToString();
                    SqlCrud.RemoveAllIvLotRoyalties(trxId);
                    break;

                case 2:
                    trxId = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvDocumentNumber.Value.ToString();
                    var itemNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvTransactionScroll.ItemNumber.Value.ToString();
                    var lineNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvTransactionScroll.LineSeqNumber.Value;
                    SqlCrud.RemoveIvLotRoyalty(trxId, itemNumber, lineNumber);
                    break;

                case 3:
                    trxId = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvDocumentNumber.Value.ToString();
                    itemNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvTransactionScroll.ItemNumber.Value.ToString();
                    lineNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvTransactionScroll.LineSeqNumber.Value;
                    SqlCrud.RemoveIvLotRoyalty(trxId, itemNumber, lineNumber);
                    break;
                case 4:
                    var batch = Dynamics.Forms.IvBatchEntry.IvBatchEntry.BatchNumber.Value.ToString();
                    SqlCrud.RemoveIvRoyaltiesByBatch(batch);
                    break;

            }
        }
        private void IvRemoveLotNumbers_InvokeBeforeOriginal(object sender, IvRemoveLotNumbersProcedure.InvokeEventArgs e)
        {
            IvDeleteRoyalties();

        }

        private void IvRemoveAllButton_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            Controller.Instance.SetDeleteType(3);
        }

        private void IvRemoveButtonR_ClickBeforeOriginal(object sender, CancelEventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            var trxId = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvDocumentNumber.Value.ToString();
            var itemNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.ItemNumber.Value.ToString();
            //var lineNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.LocalTempLineSeqNum.Value;
            var lotNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionLotNumbers.IvTransactionLotSelected.SerialLotNumber.Value.ToString();

            SqlCrud.RemoveIvLotRoyalty(trxId, itemNumber, lotNumber);

        }

        private void IvLotAttributesInquiry_CloseAfterOriginal(object sender, EventArgs e)
        {
            ResetWindowType();

        }

        private void IvLotAttributes_CloseAfterOriginal(object sender, EventArgs e)
        {
            ResetWindowType();
        }

        private void IvEntryExpansionButton1_ClickAfterOriginal(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(2);
        }

        #endregion Inventory

        #region Assembly BOM's
        private void BmDelForTrxId_InvokeBeforeOriginal(object sender, BmTrxCompForm.DelForTrxIdProcedure.InvokeEventArgs e)
        {
            BmDeleteRoyaties();
        }
        private void GetBmTrxId()
        {
            var trxId = BmTrxEntryForm.BmTrxEntry.TrxId.Value.ToString();
            Controller.Instance.SetTrxId(trxId);
        }
        private void PreviousButtonToolbar_ClickAfterOriginal(object sender, EventArgs e)
        {
            MessageBox.Show($"ItemNumber: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ItemNumber.Value } ,  IvItem: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.IvItem.Value }, ComponentItem: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ComponentItemNumber.Value} ");
        }

        private void BmExpansionButton1_ClickAfterOriginal(object sender, EventArgs e)
        {

            Controller.Instance.SetWindowType(0);

        }

        private void BmExpansionButton3_ClickAfterOriginal(object sender, EventArgs e)
        {

            Controller.Instance.SetWindowType(1);
            GetBmTrxId();


        }

        private void BmDeleteRoyaties()
        {
            Controller.Instance.SetConnectionInfo();


            switch (Controller.Instance.Model.DeleteType)
            {
                case 5:
                    var batch = Dynamics.Forms.IvBatchEntry.IvBatchEntry.BatchNumber.Value.ToString();
                    SqlCrud.RemoveBmLotRoyaltiesByBatch(batch);

                    break;
                default:
                    var trxId = Dynamics.Forms.BmTrxEntry.BmTrxEntry.TrxId.Value.ToString();
                    SqlCrud.DeleteInheritedLotRoyaltyEntry(trxId);
                    break;

            }


        }
        #endregion Assembly BOM's

        #region Controller Instructions
        private void SetAsRoyaltyHistoryInquiry(object sender, CancelEventArgs e)
        {
            //TODO Make history viewer window
            var trxId = Dynamics.Forms.SopSerialLotInquiry.SopSerialLotInquiry.NumberScroll.SopNumber.Value;
            Controller.Instance.SetTrxId(trxId);
            
            Controller.Instance.SetWindowType(7);            
        }
        private void SetAsDeleteTransaction()
        {
            Controller.Instance.SetDeleteType(1);
        }
        private void SetAsDeleteLine()
        {
            Controller.Instance.SetDeleteType(2);
        }
        private void SetAsDeleteLineDetail()
        {
            Controller.Instance.SetDeleteType(3);
        }
        private void SetAsInquiryWindow(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(5);
        }
        private void SetAsInquiryWinodwOnAttEntry(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(6);
        }
        private void SetAsInquiryWindow_BmLotAttEntry(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(0);
        }

        private void ResetWindowType()
        {
            if (BMRoyaltyLotVendorEntryForm.isOpen)
            {
                Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke("Close open Royalty Inquiry window");
                Form.ActiveForm.Activate();
            }
            else if (RoyaltyLotVendorEntryForm.isOpen)
            {
                Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke("Close open Royalty Entry window");
                Form.ActiveForm.Activate();
            }
            else
            {
                Controller.Instance.SetWindowType(0);
            }
        }

        #endregion Controller Instructions

        #region Lot Attributes
        private void SsgIvLotAttributesEdit_OpenAfterOriginal(object sender, EventArgs e)
        {
            Controller.Instance.SetWindowType(4);
            Controller.Instance.SetConnectionInfo();

        }
        private void OpenEditLotAttRoyaltyEntryForm(object sender, EventArgs e)
        {

            var window = RoyaltyEntryWindowFactory.WindowPicker();
            if (!string.IsNullOrWhiteSpace(IvLotAttributesEdit.IvLotAttributesEdit.ItemNumber.Value) && !string.IsNullOrWhiteSpace(IvLotAttributesEdit.IvLotAttributesEdit.LotNumber.Value))
            {
                window.Show();
            }
            else
            {
                Dynamics.Forms.SyVisualStudioHelper.Functions.DexError.Invoke("Please specify an item number and lot number to edit royalties.");
            }

        }

        #endregion Lot Attributes

        #region Royalty Entry Window
        private void OpenRoyaltyEntryForm(object sender, EventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            var window = RoyaltyEntryWindowFactory.WindowPicker();
            if (window is null)
            {
                MessageBox.Show("Cannot edit royalties from this window.  Insert a lot and view Lot Attribute Inquiry window.  Only parent component royalties can be edited");
            }
            else if (Controller.Instance.Model.WindowType == 6)
            {
                MessageBox.Show("Cannot edit royalties from this window.  First, insert a lot then use the blue arrow to open to Lot Attributes Entry or Inquiry window.");
            }
            else
            {
                window.Show();
            }
        }

        private void OpenBMRoyaltyEntryForm(object sender, EventArgs e)
        {
            Controller.Instance.SetConnectionInfo();
            if (Controller.Instance.Model.WindowType == 1)
            {
                GetBmTrxId();
            }


            var window = RoyaltyEntryWindowFactory.WindowPicker();
            if (window != null)
            {
                window.Show();
            }
            else
            {
                MessageBox.Show("Royalty Add-in has not been configured for this window. Notify your system admin");
            }

        }

        private void NextButtonToolbar_ClickAfterOriginal(object sender, EventArgs e)
        {
            MessageBox.Show($"ItemNumber: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ItemNumber.Value } ,  IvItem: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.IvItem.Value }, ComponentItem: {Ssg.Forms.BmTrxSerialLotEntry.BmTrxSerialLotEntry.ComponentItemNumber.Value} ");
        }

        private void SsgBmSaveButton_InvokeAfterOriginal(object sender, Microsoft.Dexterity.Applications.SsgDictionary.SsgBmSaveButtonProcedure.InvokeEventArgs e)
        {
           
            TestListInherit(Dynamics.Forms.BmTrxEntry.BmTrxEntry.TrxId.Value.ToString());
            int unwantedEntry = SqlCrud.CatchUnwantedRoyaltyEntries(Dynamics.Forms.BmTrxEntry.BmTrxEntry.TrxId.Value.ToString());
            if (unwantedEntry > 0 )
            {
                Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke($"There are more royalty entries on the top-level than on components.  If the top-level item does not require specialty royalties then reopen {Dynamics.Forms.BmTrxEntry.BmTrxEntry.TrxId.Value} and remove all royalties from top-level then click save to run inheritance process. ");
                
            }

        }


        #endregion Royalty Entry Window

        #region Execute Inheritance

        private static void TestListInherit(string trxId)
        {

            //Check if Current trx , OGItem ,OGLot match current parent on bmTrx if not delete all and refresh.  
            //SqlCrud.DeleteInheritedLotRoyaltyEntry(trxId);
            var lotRoyaltyEntries = InheritanceLogic.RoyaltiesUsed(trxId);
            foreach (var entry in lotRoyaltyEntries)
            {
                SqlCrud.AddInheritedLotRoyaltyEntry(entry);

            }

        }

        private static void RunInherit(string v)
        {

            using (SqlConnection sqlCon = SqlCrud.ConnectionGP())
            {


                SqlCommand cmd = new SqlCommand("JLz_spRoyaltyEntry_Inherit", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TrxID", v);

                sqlCon.Open();
                cmd.ExecuteNonQuery();

                sqlCon.Close();
            }
        }
        #endregion Execture inheritance 

        #region NOT USED
        private void SsgIvLotAttributesDelete_InvokeAfterOriginal(object sender, SsgIvLotAttributesDeleteProcedure.InvokeEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion NOT USED

    }
}
