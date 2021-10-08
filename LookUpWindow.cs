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
    public partial class LookUpWindow : DexUIForm
    {
        public bool IsOpen;
        
        public LookUpWindow()
        {
            InitializeComponent();
        }
   

        private void LookUpWindow_Load(object sender, EventArgs e)
        {
            
            PopTable();
            IsOpen = true;
            

        }

        public void PopTable()
        {
            lookupDataGridView.DataSource = SqlCrud.GetAllRawRoidNumbers();

        }

     

        private void okButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LookUpWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsOpen)
            {
                IsOpen = false;
                
            }
            
        }

        

     
    }
}