namespace DynamicsGPAddin1
{
    partial class RoyaltyLotVendorEntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.okButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.insertButton = new System.Windows.Forms.Button();
            this.royaltyPercentageLabel = new System.Windows.Forms.Label();
            this.vendorLabel = new System.Windows.Forms.Label();
            this.selectVendorsDropDown = new System.Windows.Forms.ComboBox();
            this.royaltyPercentageValue = new System.Windows.Forms.TextBox();
            this.vendorModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.VENDOR_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Royalty_Multiplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorPercentageSequenceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.LotValue = new System.Windows.Forms.Label();
            this.ItemNumberValue = new System.Windows.Forms.Label();
            this.ItemNumberLabel = new System.Windows.Forms.Label();
            this.OGLotLabel = new System.Windows.Forms.Label();
            this.OGItemLabel = new System.Windows.Forms.Label();
            this.EditOGItemCB = new System.Windows.Forms.CheckBox();
            this.EditOGLotCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NumberOfOrgsValue = new System.Windows.Forms.TextBox();
            this.OGItemLookup = new System.Windows.Forms.Button();
            this.OGLotLookup = new System.Windows.Forms.Button();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionValue = new System.Windows.Forms.TextBox();
            this.EditDescriptonCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.vendorModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.Control;
            this.okButton.Location = new System.Drawing.Point(449, 298);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 26;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click_1);
            // 
            // removeAllButton
            // 
            this.removeAllButton.BackColor = System.Drawing.SystemColors.Control;
            this.removeAllButton.Location = new System.Drawing.Point(53, 208);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(75, 23);
            this.removeAllButton.TabIndex = 24;
            this.removeAllButton.Text = "Remove All";
            this.removeAllButton.UseVisualStyleBackColor = false;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click_1);
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.SystemColors.Control;
            this.removeButton.Location = new System.Drawing.Point(53, 179);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 23;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click_1);
            // 
            // insertButton
            // 
            this.insertButton.BackColor = System.Drawing.SystemColors.Control;
            this.insertButton.Location = new System.Drawing.Point(53, 150);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(75, 23);
            this.insertButton.TabIndex = 22;
            this.insertButton.Text = "Insert >>";
            this.toolTip.SetToolTip(this.insertButton, "Insert a new row or update the selected row");
            this.insertButton.UseVisualStyleBackColor = false;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click_1);
            this.insertButton.MouseLeave += new System.EventHandler(this.insertButton_MouseLeave);
            this.insertButton.MouseHover += new System.EventHandler(this.insertButton_MouseHover);
            // 
            // royaltyPercentageLabel
            // 
            this.royaltyPercentageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.royaltyPercentageLabel.Location = new System.Drawing.Point(50, 101);
            this.royaltyPercentageLabel.Name = "royaltyPercentageLabel";
            this.royaltyPercentageLabel.Size = new System.Drawing.Size(78, 13);
            this.royaltyPercentageLabel.TabIndex = 20;
            this.royaltyPercentageLabel.Text = "Percentage";
            this.royaltyPercentageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vendorLabel
            // 
            this.vendorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vendorLabel.Location = new System.Drawing.Point(33, 75);
            this.vendorLabel.Name = "vendorLabel";
            this.vendorLabel.Size = new System.Drawing.Size(95, 13);
            this.vendorLabel.TabIndex = 21;
            this.vendorLabel.Text = "Royalty Vendor";
            this.vendorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selectVendorsDropDown
            // 
            this.selectVendorsDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.selectVendorsDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.selectVendorsDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectVendorsDropDown.FormattingEnabled = true;
            this.selectVendorsDropDown.Location = new System.Drawing.Point(137, 67);
            this.selectVendorsDropDown.Name = "selectVendorsDropDown";
            this.selectVendorsDropDown.Size = new System.Drawing.Size(291, 21);
            this.selectVendorsDropDown.TabIndex = 19;
            this.selectVendorsDropDown.SelectedIndexChanged += new System.EventHandler(this.selectVendorsDropDown_SelectedIndexChanged);
            this.selectVendorsDropDown.SelectionChangeCommitted += new System.EventHandler(this.selectVendorsDropDown_SelectionChangeCommitted);
            // 
            // royaltyPercentageValue
            // 
            this.royaltyPercentageValue.AcceptsReturn = true;
            this.royaltyPercentageValue.AcceptsTab = true;
            this.royaltyPercentageValue.BackColor = System.Drawing.SystemColors.Window;
            this.royaltyPercentageValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.royaltyPercentageValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.royaltyPercentageValue.Location = new System.Drawing.Point(137, 101);
            this.royaltyPercentageValue.MaxLength = 13;
            this.royaltyPercentageValue.Name = "royaltyPercentageValue";
            this.royaltyPercentageValue.Size = new System.Drawing.Size(290, 13);
            this.royaltyPercentageValue.TabIndex = 20;
            this.royaltyPercentageValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.royaltyPercentageValue_KeyPress);
            this.royaltyPercentageValue.Leave += new System.EventHandler(this.royaltyPercentageValue_Leave);
            // 
            // vendorModelBindingSource
            // 
            this.vendorModelBindingSource.DataSource = typeof(DynamicsGPAddin1.VendorModel);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(137, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(720, 138);
            this.dataGridView1.TabIndex = 17;
            // 
            // VENDOR_ID
            // 
            this.VENDOR_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.VENDOR_ID.DataPropertyName = "VENDOR_ID";
            this.VENDOR_ID.HeaderText = "Vendor";
            this.VENDOR_ID.Name = "VENDOR_ID";
            this.VENDOR_ID.ReadOnly = true;
            // 
            // Royalty_Multiplier
            // 
            this.Royalty_Multiplier.DataPropertyName = "Royalty_Multiplier";
            this.Royalty_Multiplier.HeaderText = "Perecentage";
            this.Royalty_Multiplier.Name = "Royalty_Multiplier";
            this.Royalty_Multiplier.ReadOnly = true;
            this.Royalty_Multiplier.Width = 93;
            // 
            // VendorPercentageSequenceNumber
            // 
            this.VendorPercentageSequenceNumber.HeaderText = "Sequence Number";
            this.VendorPercentageSequenceNumber.Name = "VendorPercentageSequenceNumber";
            this.VendorPercentageSequenceNumber.ReadOnly = true;
            this.VendorPercentageSequenceNumber.Visible = false;
            this.VendorPercentageSequenceNumber.Width = 121;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Lot";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LotValue
            // 
            this.LotValue.Location = new System.Drawing.Point(138, 48);
            this.LotValue.Name = "LotValue";
            this.LotValue.Size = new System.Drawing.Size(290, 16);
            this.LotValue.TabIndex = 29;
            this.LotValue.Text = "<lotValue>";
            // 
            // ItemNumberValue
            // 
            this.ItemNumberValue.Location = new System.Drawing.Point(138, 32);
            this.ItemNumberValue.Name = "ItemNumberValue";
            this.ItemNumberValue.Size = new System.Drawing.Size(290, 16);
            this.ItemNumberValue.TabIndex = 45;
            this.ItemNumberValue.Text = "<ItemNumberValue>";
            // 
            // ItemNumberLabel
            // 
            this.ItemNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumberLabel.Location = new System.Drawing.Point(36, 32);
            this.ItemNumberLabel.Name = "ItemNumberLabel";
            this.ItemNumberLabel.Size = new System.Drawing.Size(92, 13);
            this.ItemNumberLabel.TabIndex = 44;
            this.ItemNumberLabel.Text = "Item Number";
            this.ItemNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OGLotLabel
            // 
            this.OGLotLabel.Enabled = false;
            this.OGLotLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OGLotLabel.Location = new System.Drawing.Point(461, 99);
            this.OGLotLabel.Name = "OGLotLabel";
            this.OGLotLabel.Size = new System.Drawing.Size(138, 15);
            this.OGLotLabel.TabIndex = 51;
            this.OGLotLabel.Text = "Original ROID Number";
            this.OGLotLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.OGLotLabel.Visible = false;
            // 
            // OGItemLabel
            // 
            this.OGItemLabel.Enabled = false;
            this.OGItemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OGItemLabel.Location = new System.Drawing.Point(461, 69);
            this.OGItemLabel.Name = "OGItemLabel";
            this.OGItemLabel.Size = new System.Drawing.Size(138, 15);
            this.OGItemLabel.TabIndex = 52;
            this.OGItemLabel.Text = "Original Item Number";
            this.OGItemLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.OGItemLabel.Visible = false;
            // 
            // EditOGItemCB
            // 
            this.EditOGItemCB.AutoSize = true;
            this.EditOGItemCB.Location = new System.Drawing.Point(733, 66);
            this.EditOGItemCB.Name = "EditOGItemCB";
            this.EditOGItemCB.Size = new System.Drawing.Size(44, 17);
            this.EditOGItemCB.TabIndex = 53;
            this.EditOGItemCB.Text = "Edit";
            this.EditOGItemCB.UseVisualStyleBackColor = true;
            this.EditOGItemCB.CheckedChanged += new System.EventHandler(this.EditOGItemCB_CheckedChanged_1);
            // 
            // EditOGLotCB
            // 
            this.EditOGLotCB.AutoSize = true;
            this.EditOGLotCB.Location = new System.Drawing.Point(733, 99);
            this.EditOGLotCB.Name = "EditOGLotCB";
            this.EditOGLotCB.Size = new System.Drawing.Size(44, 17);
            this.EditOGLotCB.TabIndex = 54;
            this.EditOGLotCB.Text = "Edit";
            this.EditOGLotCB.UseVisualStyleBackColor = true;
            this.EditOGLotCB.CheckedChanged += new System.EventHandler(this.EditOGLotCB_CheckedChanged_1);
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(459, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 15);
            this.label2.TabIndex = 55;
            this.label2.Text = "Number of Organisms";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // NumberOfOrgsValue
            // 
            this.NumberOfOrgsValue.BackColor = System.Drawing.SystemColors.Window;
            this.NumberOfOrgsValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumberOfOrgsValue.Enabled = false;
            this.NumberOfOrgsValue.Location = new System.Drawing.Point(605, 32);
            this.NumberOfOrgsValue.Name = "NumberOfOrgsValue";
            this.NumberOfOrgsValue.Size = new System.Drawing.Size(51, 13);
            this.NumberOfOrgsValue.TabIndex = 56;
            // 
            // OGItemLookup
            // 
            this.OGItemLookup.BackColor = System.Drawing.Color.Transparent;
            this.OGItemLookup.BackgroundImage = global::DynamicsGPAddin1.Properties.Resources.Field_Lookup;
            this.OGItemLookup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dexButtonProvider.SetButtonType(this.OGItemLookup, Microsoft.Dexterity.Shell.DexButtonType.Field);
            this.OGItemLookup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
            this.OGItemLookup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OGItemLookup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.OGItemLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OGItemLookup.Location = new System.Drawing.Point(707, 64);
            this.OGItemLookup.Name = "OGItemLookup";
            this.OGItemLookup.Size = new System.Drawing.Size(20, 20);
            this.OGItemLookup.TabIndex = 57;
            this.OGItemLookup.TabStop = false;
            this.OGItemLookup.UseVisualStyleBackColor = false;
            this.OGItemLookup.Click += new System.EventHandler(this.OGItemLookup_Click);
            // 
            // OGLotLookup
            // 
            this.OGLotLookup.BackColor = System.Drawing.Color.Transparent;
            this.OGLotLookup.BackgroundImage = global::DynamicsGPAddin1.Properties.Resources.Field_Lookup;
            this.OGLotLookup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dexButtonProvider.SetButtonType(this.OGLotLookup, Microsoft.Dexterity.Shell.DexButtonType.Field);
            this.OGLotLookup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
            this.OGLotLookup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OGLotLookup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.OGLotLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OGLotLookup.Location = new System.Drawing.Point(707, 97);
            this.OGLotLookup.Name = "OGLotLookup";
            this.OGLotLookup.Size = new System.Drawing.Size(20, 20);
            this.OGLotLookup.TabIndex = 58;
            this.OGLotLookup.TabStop = false;
            this.OGLotLookup.UseVisualStyleBackColor = false;
            this.OGLotLookup.Click += new System.EventHandler(this.OGLotLookup_Click);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.Location = new System.Drawing.Point(50, 128);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(78, 13);
            this.DescriptionLabel.TabIndex = 59;
            this.DescriptionLabel.Text = "Description";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DescriptionValue
            // 
            this.DescriptionValue.BackColor = System.Drawing.SystemColors.Window;
            this.DescriptionValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionValue.Cursor = System.Windows.Forms.Cursors.Help;
            this.DescriptionValue.Enabled = false;
            this.DescriptionValue.Location = new System.Drawing.Point(138, 128);
            this.DescriptionValue.Name = "DescriptionValue";
            this.DescriptionValue.Size = new System.Drawing.Size(290, 13);
            this.DescriptionValue.TabIndex = 21;
            // 
            // EditDescriptonCB
            // 
            this.EditDescriptonCB.AutoSize = true;
            this.EditDescriptonCB.Location = new System.Drawing.Point(434, 128);
            this.EditDescriptonCB.Name = "EditDescriptonCB";
            this.EditDescriptonCB.Size = new System.Drawing.Size(44, 17);
            this.EditDescriptonCB.TabIndex = 61;
            this.EditDescriptonCB.Text = "Edit";
            this.EditDescriptonCB.UseVisualStyleBackColor = true;
            this.EditDescriptonCB.CheckedChanged += new System.EventHandler(this.EditDescriptonCB_CheckedChanged);
            // 
            // RoyaltyLotVendorEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(908, 357);
            this.Controls.Add(this.EditDescriptonCB);
            this.Controls.Add(this.DescriptionValue);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.OGLotLookup);
            this.Controls.Add(this.OGItemLookup);
            this.Controls.Add(this.NumberOfOrgsValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EditOGLotCB);
            this.Controls.Add(this.EditOGItemCB);
            this.Controls.Add(this.OGItemLabel);
            this.Controls.Add(this.OGLotLabel);
            this.Controls.Add(this.ItemNumberValue);
            this.Controls.Add(this.ItemNumberLabel);
            this.Controls.Add(this.LotValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.removeAllButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.royaltyPercentageLabel);
            this.Controls.Add(this.vendorLabel);
            this.Controls.Add(this.selectVendorsDropDown);
            this.Controls.Add(this.royaltyPercentageValue);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "RoyaltyLotVendorEntryForm";
            this.Text = "Royalty Lot Vendor Entry Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RoyaltyLotVendorEntryForm_FormClosed);
            this.Load += new System.EventHandler(this.RoyaltyLotVendorEntryFrom_Load);
            this.Click += new System.EventHandler(this.RoyaltyLotVendorEntryForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.vendorModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Label royaltyPercentageLabel;
        private System.Windows.Forms.Label vendorLabel;
        private System.Windows.Forms.ComboBox selectVendorsDropDown;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VENDOR_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Royalty_Multiplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorPercentageSequenceNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LotValue;
        private System.Windows.Forms.BindingSource vendorModelBindingSource;
        private System.Windows.Forms.TextBox royaltyPercentageValue;
        private System.Windows.Forms.Label ItemNumberValue;
        private System.Windows.Forms.Label ItemNumberLabel;
        private System.Windows.Forms.Label OGLotLabel;
        private System.Windows.Forms.Label OGItemLabel;
        private System.Windows.Forms.CheckBox EditOGItemCB;
        private System.Windows.Forms.CheckBox EditOGLotCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NumberOfOrgsValue;
        private System.Windows.Forms.Button OGItemLookup;
        private System.Windows.Forms.Button OGLotLookup;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.TextBox DescriptionValue;
        private System.Windows.Forms.CheckBox EditDescriptonCB;
    }
}

