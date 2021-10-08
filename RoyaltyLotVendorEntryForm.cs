using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Shell;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using System.Drawing;

namespace DynamicsGPAddin1
{
    //TODO:  Only allow numbers and 1 decimal point in percentage value
    //TODO:  Only allow existing ROID# to be selected on IV transaction entry window type 2 
    //Textbox.Visible propert does not work for DexUIForm textboxes.  To hide text boxes we must remove completely.
    


    public partial class RoyaltyLotVendorEntryForm : DexUIForm, IRoyaltyLotVendorEntryForm
    {
        //TODO Create a list of SOURCE Lot numbers
        //TODO Wire Source Lot list to vendor drop down 
        //**This will involve a retroactive adjustment.**  Most Source Lots will be an infinite supply consisting of 1 lot ever recieved.  Once a Comp/Reag is assembled with SourceLot, that source will be passed from RawMat to component\Reag to FG carrying RoyaltyEntryModel with it. This allows for a raw material item number to hold multiple lots consisting of different strains.


        //TODO Get Selected Vendor Percentage *** this may need to be adjusted if % is linked to lot: ***Could allow for vendors to hold multiple %*** ***

        //SqlCrud sql = new SqlCrud();

        private TextBox _ogItemBox;
       
        private TextBox _ogLotBox;

        private LookUpWindow _lookUpWindow = new LookUpWindow();


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
        



        // LotRoyaltyEntryModel lotRoyaltyEntryModel;
        // select * from Vendor Master Where Vendor Class ID = Royalty To List
        // Foreach row in Data Set map model.  

        public RoyaltyLotVendorEntryForm()
        {

            InitializeComponent();
        // GetRoyaltyLotEntries();

        }   
        private void RoyaltyLotVendorEntryFrom_Load(object sender, EventArgs e)
        {

            GetItemLotNumber();

            PopulateNumberOfOrgs();

            WireUpLists();

            PopTable();

            PopVendorList();

            SetIsOpen(true);

            SetAsEditWindow();

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            _lookUpWindow.lookupDataGridView.SelectionChanged += LookupDataGridView_SelectionChanged;


            selectVendorsDropDown.Leave += SelectVendorsDropDown_Leave;

            dataGridView1.ClearSelection();

        }

        private void SelectVendorsDropDown_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(selectVendorsDropDown.Text))
            {
                ChangeSelectedItemDropDown(); 
            }
        }

    

        private void LookupDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowIndex;
            if (_lookUpWindow.lookupDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            else
            {

                rowIndex = _lookUpWindow.lookupDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = _lookUpWindow.lookupDataGridView.Rows[rowIndex];
                selectVendorsDropDown.Text = Convert.ToString(selectedRow.Cells["VendorID"].Value);
                
                royaltyPercentageValue.Text = Convert.ToString(selectedRow.Cells["Percentage"].Value);
                _ogItemBox.Text = Convert.ToString(selectedRow.Cells["ItemNumber"].Value);
                _ogLotBox.Text = Convert.ToString(selectedRow.Cells["LotNumber"].Value);
                DescriptionValue.Text = Convert.ToString(selectedRow.Cells["Description"].Value);
            }
        }

        public void ChangeSelectedItemDropDown()
        {
            if (selectVendorsDropDown.Items.Contains(selectVendorsDropDown.Text.Trim()))
            {

                selectVendorsDropDown.SelectedIndex = selectVendorsDropDown.Items.IndexOf(selectVendorsDropDown.Text.Trim());
                 
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.WindowType != 3)
            {
                int rowIndex;
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    return;
                }
                else
                {

                    rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    selectVendorsDropDown.Text = Convert.ToString(selectedRow.Cells["VendorID"].Value);
                    royaltyPercentageValue.Text = Convert.ToString(selectedRow.Cells["Percentage"].Value);
                    _ogItemBox.Text = Convert.ToString(selectedRow.Cells["OGItem"].Value);
                    _ogLotBox.Text = Convert.ToString(selectedRow.Cells["OGLot"].Value);
                    DescriptionValue.Text = Convert.ToString(selectedRow.Cells["Description"].Value);
                } 
            }
            else
            {
                int rowIndex;
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    return;
                }
                else
                {

                    rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    selectVendorsDropDown.Text = Convert.ToString(selectedRow.Cells["VendorID"].Value);
                    royaltyPercentageValue.Text = Convert.ToString(selectedRow.Cells["Percentage"].Value);
                    //_ogItemBox.Text = Convert.ToString(selectedRow.Cells["OGItem"].Value);
                    //_ogLotBox.Text = Convert.ToString(selectedRow.Cells["OGLot"].Value);
                    DescriptionValue.Text = Convert.ToString(selectedRow.Cells["Description"].Value);
                }

            }

        }

        private void PopVendorList()
        {
            
            availableVendors = SqlCrud.GetAllRoyaltyVendors();
            selectVendorsDropDown.DataSource = availableVendors;
            selectVendorsDropDown.DisplayMember = "VendorID";

        }

        private void MakeOGTextBoxes()
        {
            TextBox ogItemTextBox = new TextBox();
            TextBox ogLotTextBox = new TextBox();

            ogItemTextBox.Location = new System.Drawing.Point(606, 68);
            ogItemTextBox.Size = new System.Drawing.Size(100, 13);
            ogItemTextBox.Name = "OGItemTextBox";
            ogItemTextBox.Visible = true;
            ogItemTextBox.Enabled = false;
            ogItemTextBox.BorderStyle = BorderStyle.None;

            ogLotTextBox.Location = new System.Drawing.Point(606, 101);
            ogLotTextBox.Size = new System.Drawing.Size(100, 13);
            ogLotTextBox.Name = "OGLotTextBox";
            ogLotTextBox.Visible = true;
            ogLotTextBox.Enabled = false;
            ogLotTextBox.BorderStyle = BorderStyle.None;

            _ogItemBox = ogItemTextBox;
            _ogLotBox = ogLotTextBox;
            this.Controls.Add(_ogItemBox);
            this.Controls.Add(_ogLotBox);
        }
        private void RemoveOGTextBoxes()
        {
            if (this.Controls.Find("OGItemTextBox", true).Length > 0)
            {

                this.Controls.RemoveByKey("OGItemTextBox");
               
                this._ogItemBox.Visible = false;

            }
            if (this.Controls.Find("OGLotTextBox",true).Length > 0)
            {
                this.Controls.RemoveByKey("OGLotTextBox");

                this._ogLotBox.Visible = false;
            }
        }
        private void SetAsEditWindow()
        {


            if (Controller.Instance.Model.WindowType != 3)
            {

                MakeOGTextBoxes();
                OGItemLookup.Visible = true;
                OGItemLabel.Visible = true;
                EditOGItemCB.Visible = true;

                OGLotLookup.Visible = true;
                OGLotLabel.Visible = true;
                EditOGLotCB.Visible = true;
            }
            else
            {
                RemoveOGTextBoxes();
                OGItemLookup.Visible = false;
                OGItemLabel.Visible = false;
                EditOGItemCB.Visible = false;

                OGLotLookup.Visible = false;
                OGLotLabel.Visible = false;
                EditOGLotCB.Visible = false;
            }

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
            //VendorModel vendor = new VendorModel();
            var vendorId = selectVendorsDropDown.Text;
            

            if (!string.IsNullOrEmpty(vendorId))
            {
                decimal percent = SqlCrud.GetVendorPercentage(vendorId);
                royaltyPercentageValue.Text = ConvertDecimalToString(percent);


                if (selectVendorsDropDown.Text == null)
                {
                    MessageBox.Show("Please select a vendor id from the list");
                    royaltyPercentageValue.Text = "0.000000000000";

                } 
            }

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

        public void PopTable()
        {

            dataGridView1.DataSource = SqlCrud.GetAllLotRoyaltyEntries(ItemNumberValue.Text, LotValue.Text);

        }

        public void GetItemLotNumber()
        {
            if (Controller.Instance.Model.WindowType != 0 )
            {
                switch (Controller.Instance.Model.WindowType)
                {
                    default: LotValue.Text = Ssg.Forms.IvLotAttributes.IvLotAttributes.LotNumber.Value.ToString();
                        ItemNumberValue.Text = Ssg.Forms.IvLotAttributes.IvLotAttributes.ItemNumber.Value.ToString();   
                        break;
                    case 2:
                        LotValue.Text = Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.LotNumber.Value.ToString();
                        ItemNumberValue.Text = Ssg.Forms.IvLotAttributesInquiry.IvLotAttributesInquiry.ItemNumber.Value.ToString();
                        break;

                    case 3:
                        LotValue.Text = Ssg.Forms.IvLotAttributes.IvLotAttributes.LotNumber.Value.ToString();
                        ItemNumberValue.Text = Ssg.Forms.IvLotAttributes.IvLotAttributes.ItemNumber.Value.ToString();
                        break;

                    case 4:

                        if (!string.IsNullOrEmpty(Ssg.Forms.IvLotAttributesEdit.IvLotAttributesEdit.LotNumber.Value))
                        {
                            LotValue.Text = Ssg.Forms.IvLotAttributesEdit.IvLotAttributesEdit.LotNumber.Value.ToString();
                            ItemNumberValue.Text = Ssg.Forms.IvLotAttributesEdit.IvLotAttributesEdit.ItemNumber.Value.ToString();
                            
                        }
                        else
                        {
                            Dynamics.Forms.SyVisualStudioHelper.Functions.DexError.Invoke("Please specify a lot number.");
                            this.Close();
                        }
                        break;

                } 
            }
            
        }

        public void WireUpLists()
        {
            royaltyPercentageValue.Text = "0.0000000000";

            //selectVendorsDropDown.DataSource = availableVendors;
            //selectVendorsDropDown.DisplayMember = "VendorID";

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
            if (string.IsNullOrEmpty(DescriptionValue.Text))
            {
                return false;
            }
            if (Controller.Instance.Model.WindowType != 3)
            {
                if (string.IsNullOrEmpty(_ogItemBox.Text) || string.IsNullOrEmpty(_ogLotBox.Text))
                {
                    return false;
                }
                if (SqlCrud.RoidExists(_ogLotBox.Text, _ogItemBox.Text, selectVendorsDropDown.Text) == 0)
                {
                    return false;
                }
            }

            return true;
        }
        private LotRoyaltyEntryModel BuildRoyaltyEntryModel()
        {
            LotRoyaltyEntryModel entryModel = new LotRoyaltyEntryModel();

            entryModel.ItemNumber = ItemNumberValue.Text;
            entryModel.LotNumber = LotValue.Text;
            //entryModel.VendorID = selectVendorsDropDown.GetItemText(selectVendorsDropDown.SelectedItem);
            entryModel.VendorID = selectVendorsDropDown.Text;
            entryModel.Percentage = decimal.Parse(royaltyPercentageValue.Text);
            entryModel.Description = DescriptionValue.Text;
            entryModel.NumberOfOrgs = Int32.Parse(NumberOfOrgsValue.Text);
            switch (Controller.Instance.Model.WindowType)
            {
                case 2:
                   entryModel.CurrentTransactionNumber = Dynamics.Forms.IvTransactionEntry.IvTransactionEntry.IvDocumentNumber.Value.ToString();
                    entryModel.OGItem = _ogItemBox.Text;
                    entryModel.OGLot = _ogLotBox.Text;
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int rowIndex;
                        rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                        entryModel.RLTSEQNUM = int.Parse(selectedRow.Cells["RLTSEQNUM"].Value.ToString());


                    }
                    break;

                case 3:
                  
                    entryModel.CurrentTransactionNumber = Dynamics.Forms.PopReceivingsEntry.PopReceivingsEntry.PopReceiptNumber.Value.ToString();
                    entryModel.OGItem = ItemNumberValue.Text;
                    entryModel.OGLot = LotValue.Text;
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int rowIndex;
                        rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                        entryModel.RLTSEQNUM = int.Parse(selectedRow.Cells["RLTSEQNUM"].Value.ToString());


                    }
                    break;

                case 4:
                 
                    entryModel.CurrentTransactionNumber = $"Edited {Dynamics.Globals.UserDate.Value.ToShortDateString()} ";
                    entryModel.OGItem = _ogItemBox.Text;
                    entryModel.OGLot = _ogLotBox.Text;
                    entryModel.Description = DescriptionValue.Text;
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int rowIndex;
                        rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                        entryModel.RLTSEQNUM = int.Parse(selectedRow.Cells["RLTSEQNUM"].Value.ToString());
                         

                    }
                    break;
            }

            return entryModel;

        }

        private void insertButton_Click_1(object sender, EventArgs e)
        {
            //TODO Change this so that insert adds to List then OK button will add to SQL Table
            //12:04:33  'IV_Transaction_Entry_Lot_Number_POST on form IV_Transaction_Entry'
            //12:04:33  'IV_Transaction_Lot_Numbers_Expansion_Button1_CHG on form IV_Transaction_Entry'
            if (ValidateForm())
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var answer = Dynamics.Forms.SyVisualStudioHelper.Functions.DexAsk.Invoke("The selected row will be updated.  Are you sure you want to update the current selection?", SButton1: "Yes", SButton2: "No", SButton3: "");

                    if (answer == 2)
                    {
                        Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke("Please click outside of the grid to unselect rows. ");
                    }
                    else
                    {
                        if (Controller.Instance.Model.WindowType != 3)
                        {

                            LotRoyaltyEntryModel model = BuildRoyaltyEntryModel();
                            var entryExists = SqlCrud.LookForDuplicateRoids(model);
                            if (entryExists > 0)
                            {
                                Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke($"Duplicate organism ID {model.OGLot}. ");

                            }
                            else
                            {
                                SqlCrud.AddOrEditLotRoyaltyEntry(model);

                            }
                        }
                        else
                        {
                            SqlCrud.AddLotRoyaltyEntry(BuildRoyaltyEntryModel());

                        }
                        PopTable();

                        royaltyPercentageValue.Clear();
                        royaltyPercentageValue.Text = "0.0000000000";
                        selectVendorsDropDown.SelectedItem = "";


                    }

                }
                else
                {
                    if (Controller.Instance.Model.WindowType != 3)
                    {

                        LotRoyaltyEntryModel model = BuildRoyaltyEntryModel();
                        var entryExists = SqlCrud.LookForDuplicateRoids(model);
                        if (entryExists > 0)
                        {
                            Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke($"Duplicate organism ID {model.OGLot}. ");

                        }
                        else
                        {
                            SqlCrud.AddOrEditLotRoyaltyEntry(model);

                        }
                    }
                    else
                    {
                        SqlCrud.AddLotRoyaltyEntry(BuildRoyaltyEntryModel());

                    }
                    PopTable();

                    royaltyPercentageValue.Clear();
                    royaltyPercentageValue.Text = "0.0000000000";
                    selectVendorsDropDown.SelectedItem = "";
                }
            }

            else
            {
                MessageBox.Show("Fill in all fields");
            }
        }

        private void removeButton_Click_1(object sender, EventArgs e)
        {
            //TODO Add line sequence or use dgv row index as a parameter
            using (SqlConnection sqlCon = SqlCrud.ConnectionGP())
            {
                if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].Value.ToString().Length > 0 || dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[1].Value.ToString().Length > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    string v = Convert.ToString(selectedRow.Cells["VendorID"].Value);
                    string p = Convert.ToString(selectedRow.Cells["Percentage"].Value);
                    string trxId = Convert.ToString(selectedRow.Cells["CurrentTransactionNumber"].Value);
                    string ogLot = Convert.ToString(selectedRow.Cells["OGLot"].Value);
                    int rltSeqNum = Int32.Parse(selectedRow.Cells["RLTSEQNUM"].Value.ToString());
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


        private void removeAllButton_Click_1(object sender, EventArgs e)
        {
            //remove entries from JLz_RoyaltyLotAtt where lot number = LotValue.Text

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

        private void okButton_Click_1(object sender, EventArgs e)
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



        private void selectVendorsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedVendorPercentage();
        }

        private void RoyaltyLotVendorEntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetIsOpen(false);
        }



        private void RoyaltyLotVendorEntryForm_Click(object sender, EventArgs e)
        {
            ClearDataGridViewSelection();
        }          
      

        private void ClearDataGridViewSelection()
        {
            
                dataGridView1.ClearSelection();
                DescriptionValue.Text = "";
            if (Controller.Instance.Model.WindowType != 3)
            {
                //Case 4
                _ogItemBox.Text = "";
                _ogLotBox.Text = "";

            }
        }

        public LookUpWindow MakeLookUpWindow()
        {
            if (_lookUpWindow is null || _lookUpWindow.IsDisposed)
            {
                _lookUpWindow = new LookUpWindow();
                _lookUpWindow.lookupDataGridView.SelectionChanged += LookupDataGridView_SelectionChanged;
            }
            return _lookUpWindow;
        }

        private void OGItemLookup_Click(object sender, EventArgs e)
        {
            ClearDataGridViewSelection();
            MakeLookUpWindow();
            
            _lookUpWindow.Show();
        }
        private void OGLotLookup_Click(object sender, EventArgs e)
        {
            ClearDataGridViewSelection();
            MakeLookUpWindow();
            
            _lookUpWindow.Show();
        }

        private void EditOGItemCB_CheckedChanged_1(object sender, EventArgs e)
        {
            if (EditOGItemCB.Checked)
            {
                _ogItemBox.Enabled = true;
                OGItemLabel.Enabled = true;


            }
            else
            {
                _ogItemBox.Enabled = false;
                OGItemLabel.Enabled = false;
            }

        }

        private void EditOGLotCB_CheckedChanged_1(object sender, EventArgs e)
        {
            if (EditOGLotCB.Checked)
            {
                _ogLotBox.Enabled = true;
                OGLotLabel.Enabled = true;
            }
            else
            {
                _ogLotBox.Enabled = false;
                OGLotLabel.Enabled = false;
            }

        }

        private void EditDescriptonCB_CheckedChanged(object sender, EventArgs e)
        {
            if (EditDescriptonCB.Checked)
            {
                DescriptionValue.Enabled = true;
            }
            else
            {
                DescriptionValue.Enabled = false;
            }
        }

        private void royaltyPercentageValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void royaltyPercentageValue_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(royaltyPercentageValue.Text))
            {
                string text = royaltyPercentageValue.Text.Trim();
                if (!text.Contains('.'))
                {
                    royaltyPercentageValue.Text = text.Insert(1, ".");
                    text = royaltyPercentageValue.Text.Trim();
                }
                royaltyPercentageValue.Text = text.PadRight(royaltyPercentageValue.MaxLength, '0');
                royaltyPercentageValue.Select(royaltyPercentageValue.Text.Length, 0); 
            }
           
            
        }

        private void insertButton_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Insert a new row or update selected row", this, 10 + insertButton.Location.X   , 10 + insertButton.Location.Y, 3650 );
        }

        private void insertButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this);
        }
    }
}

                  


        


    