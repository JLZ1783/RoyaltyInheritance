using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DynamicsGPAddin1
{
    public partial class SalesRoyaltyHistoryInquiry : DexUIForm
    {
        public static bool isOpen;
        private bool GetIsOpen()
        {
            return isOpen;
        }

        private void SetIsOpen(bool value)
        {
            isOpen = value;
        }
        public SalesRoyaltyHistoryInquiry()
        {
            InitializeComponent();
        }
        private void SalesRoyaltyHistoryInquiry_Load(object sender, EventArgs e)
        {
            GetItemLotNumber();
            PopTable();
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
                if (!String.IsNullOrEmpty(Controller.Instance.Model.TrxID))
                {
                    trxId = Controller.Instance.Model.TrxID;
                }

                dataGridView1.DataSource = SqlCrud.GetRoyaltySalesHistoryByLot(ItemNumberValue.Text, LotValue.Text, trxId);

            }
            else
            {
                dataGridView1.DataSource = SqlCrud.GetAllLotRoyaltyEntries(ItemNumberValue.Text, LotValue.Text);

            }
        }

            private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
    }
}