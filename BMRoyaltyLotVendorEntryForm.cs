using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Shell;
using DynamicsGPAddin1.Models;
using System.Globalization;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Collections;
using Microsoft.Dexterity.Applications.SsgDictionary;

namespace DynamicsGPAddin1
{
    public partial class BMRoyaltyLotVendorEntryForm : DexUIForm, IRoyaltyLotVendorEntryForm 
    {
        List<VendorModel> availableVendors = new List<VendorModel>();
       
        public static bool isOpen;

        private bool GetIsOpen()
        {
            return isOpen;
        }

        private void SetIsOpen(bool value)
        {
            isOpen = value;
        }

        public BMRoyaltyLotVendorEntryForm()
        {
            InitializeComponent();
        }
        private void BMRoyaltyLotVendorEntryForm_Load(object sender, EventArgs e)
        {
            
            GetItemLotNumber();

            PopulateNumberOfOrgs();

            ActionButtonStatus();

            WireUpLists();

            PopTable();

            PopVendorList();

            SetIsOpen(true);
        }
        private void PopVendorList()
        {
            selectVendorsDropDown.DataSource = null;
            availableVendors = SqlCrud.GetAllRoyaltyVendors();
            selectVendorsDropDown.DataSource = availableVendors;
            selectVendorsDropDown.DisplayMember = "VendorID";

        }
        private void PopulateNumberOfOrgs()
        {
            int numberOfOrgs = SqlCrud.GetNumberOfOrgs(ItemNumberValue.Text);
            if (!string.IsNullOrEmpty(numberOfOrgs.ToString()))
            {
                NumberOfOrgsValue.Text = numberOfOrgs.ToString();
            }
            else
            {
                NumberOfOrgsValue.Text = "1";
            }
            
        }
        public void SelectedVendorPercentage()
        {
            royaltyPercentageValue.Text = "";
            VendorModel vendor = new VendorModel();
            vendor.VendorID = selectVendorsDropDown.Text;
            var vendorId = vendor.VendorID;

            decimal percent = SqlCrud.GetVendorPercentage(vendorId);
            royaltyPercentageValue.Text = ConvertDecimalToString(percent);


            if (selectVendorsDropDown.Text == null)
            {
                MessageBox.Show("Please select a vendor id from the list");
                royaltyPercentageValue.Text = "0.000000000000";

            }

        }

        private LotRoyaltyEntryModel BuildRoyaltyEntryModel()
        {
            LotRoyaltyEntryModel entryModel = new LotRoyaltyEntryModel();

            entryModel.ItemNumber = ItemNumberValue.Text;
            entryModel.LotNumber = LotValue.Text;
            entryModel.VendorID = selectVendorsDropDown.GetItemText(selectVendorsDropDown.SelectedItem);
            entryModel.Percentage = decimal.Parse(royaltyPercentageValue.Text);
            entryModel.OGItem = ItemNumberValue.Text;
            entryModel.OGLot = LotValue.Text;
            entryModel.NumberOfOrgs = Int32.Parse(NumberOfOrgsValue.Text);
            entryModel.Description = DescriptionValue.Text;
            switch (Controller.Instance.Model.WindowType)
            {
                case 1:
                    entryModel.CurrentTransactionNumber = Dynamics.Forms.BmTrxEntry.BmTrxEntry.TrxId.Value.ToString();
                    break;

            }

            return entryModel;

        }

        private string ConvertDecimalToString(decimal percent)
        {

            {
                NumberFormatInfo setPrecision = new NumberFormatInfo
                {
                    NumberDecimalDigits = 10
                };

                return percent.ToString("N", setPrecision);

            }
        }
        public void GetItemLotNumber()
        {
            ItemNumberValue.Text = Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.ItemNumber.Value.ToString();
            LotValue.Text = Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.LotNumber.Value.ToString(); 
        }
        public void PopTable()
        {
            
            if (Controller.Instance.Model.WindowType == 7)
            {
                var trxId = "";
                if (!String.IsNullOrEmpty(Controller.Instance.Model.TrxID) )
                {
                    trxId = Controller.Instance.Model.TrxID;
                }

                dataGridView1.DataSource = SqlCrud.GetRoyaltySalesHistoryByLot(ItemNumberValue.Text, LotValue.Text, trxId );

            }
            else
            {
                dataGridView1.DataSource = SqlCrud.GetAllLotRoyaltyEntries(ItemNumberValue.Text, LotValue.Text);

            }
        }
        public void WireUpLists()
        {
            royaltyPercentageValue.Text = "0.0000000000";
        }
        private void ActionButtonStatus()
        {
            switch (Controller.Instance.Model.WindowType)
            {
                case 1:
                    if (IsParentComponent(Controller.Instance.Model.TrxID, Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.ItemNumber.Value.ToString()))
                    {
                        insertButton.Enabled = true;
                        removeAllButton.Enabled = true;
                        removeButton.Enabled = true;

                    }
                    else
                    {
                        insertButton.Enabled = false;
                        removeAllButton.Enabled = false;
                        removeButton.Enabled = false;
                    }
                    break;

                case 5 :
                    insertButton.Enabled = false;
                    removeAllButton.Enabled = false;
                    removeButton.Enabled = false;
                    break;
                case 6:
                    insertButton.Enabled = false;
                    removeAllButton.Enabled = false;
                    removeButton.Enabled = false;
                    break;
                case 7:
                    insertButton.Enabled = false;
                    removeAllButton.Enabled = false;
                    removeButton.Enabled = false;
                    break;

            }
    
        }
        private bool IsParentComponent(string bmTrxId , string itemNumber)
        {
           int compId = SqlCrud.GetCompID(itemNumber, bmTrxId);

            if (compId == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidateForm()
        {
            if (royaltyPercentageValue.Text.Length == 0)
            {
                return false;
            }
            if (selectVendorsDropDown.SelectedItem == null)
            {
                return false;
            }

            return true;
        }

   
  
        private void selectVendorsDropDown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (selectVendorsDropDown.SelectedIndex == -1 && selectVendorsDropDown.Text != null)
            {
                for (int i = 0; i < selectVendorsDropDown.Items.Count; i++)
                {
                    if (selectVendorsDropDown.Text.Equals(selectVendorsDropDown.Items[i].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        selectVendorsDropDown.SelectedIndex = i;
                        break;
                    }
                }
                selectVendorsDropDown.Text = null;
            }

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            switch (Controller.Instance.Model.WindowType)
            {
               case 1:
                    if (IsParentComponent(Controller.Instance.Model.TrxID, Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.ItemNumber.Value.ToString()))
                    {

                        DialogResult answer = MessageBox.Show($"Are the following royalty entries correct for lot number: {LotValue.Text} ?", "Confirm Royalty Lot Entries", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (answer == DialogResult.Yes)
                        {
                            MessageBox.Show("Royalty Entry Confirmed");

                            this.Close();
                        }
                        if (answer == DialogResult.No)
                        {
                            MessageBox.Show($"Make corrections to royalty entries for {LotValue.Text}");
                        }
                    }
                    else
                    {
                       this.Close();
                    }
                    break;
                case 5:
                    this.Close();
                    break;
            }
           
            
        }

        private void selectVendorsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedVendorPercentage();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SqlCrud.AddLotRoyaltyEntry(BuildRoyaltyEntryModel());

                PopTable();

                royaltyPercentageValue.Clear();
                royaltyPercentageValue.Text = "0.0000000000";
                selectVendorsDropDown.SelectedItem = "";
                      

            }

            else
            {
                MessageBox.Show("Fill in all fields");
            }
        }

        private void BMRoyaltyLotVendorEntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetIsOpen(false);
        }

 

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            // remove entries from JLz_RoyaltyLotAtt where lot number = LotValue.Text

            using (SqlConnection sqlCon = SqlCrud.ConnectionGP())
            {

                SqlCommand cmd = new SqlCommand("JLz_spRoyaltyLotEntry_RemoveAll", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LotNumber", LotValue.Text);

                //sqlCon.Open();
                cmd.ExecuteNonQuery();

                PopTable();

                //sqlCon.Close();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = SqlCrud.ConnectionGP())
            {
                if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].Value.ToString().Length > 0 || dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[1].Value.ToString().Length > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    string v = Convert.ToString(selectedRow.Cells["VendorID"].Value);
                    string p = Convert.ToString(selectedRow.Cells["Percentage"].Value);
                    string trxId = Convert.ToString(selectedRow.Cells["CurrentTransactionNumbeR"].Value);
                    string ogLot = Convert.ToString(selectedRow.Cells["OGLot"].Value);
                    int rltSeqNum = int.Parse(selectedRow.Cells["RLTSEQNUM"].Value.ToString());

                    SqlCommand cmd = new SqlCommand("JLz_spRoyaltyLotEntry_RemoveRow", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LotNumber", LotValue.Text);
                    cmd.Parameters.AddWithValue("@VendorID", v);
                    cmd.Parameters.AddWithValue("@Percentage", p);
                    cmd.Parameters.AddWithValue("@CurrentTransactionNumber", trxId);
                    cmd.Parameters.AddWithValue("@OGLot", ogLot);
                    cmd.Parameters.AddWithValue("@RLTSEQNUM", rltSeqNum);

                    //sqlCon.Open();
                    cmd.ExecuteNonQuery();

                    PopTable();

                    //GetRoyaltyLotEntries();
                    //sqlCon.Close();
                }
                else
                {
                    MessageBox.Show("No selection made");
                }
            }
        }
    }
}