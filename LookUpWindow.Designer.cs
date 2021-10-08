
using System;

namespace DynamicsGPAddin1
{
    partial class LookUpWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lookupDataGridView = new System.Windows.Forms.DataGridView();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lookupDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lookupDataGridView
            // 
            this.lookupDataGridView.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.lookupDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.lookupDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.lookupDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lookupDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.lookupDataGridView.CausesValidation = false;
            this.lookupDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lookupDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.lookupDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lookupDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.lookupDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.lookupDataGridView.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lookupDataGridView.Location = new System.Drawing.Point(12, 92);
            this.lookupDataGridView.Name = "lookupDataGridView";
            this.lookupDataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lookupDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.lookupDataGridView.RowHeadersVisible = false;
            this.lookupDataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.lookupDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.lookupDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.lookupDataGridView.RowTemplate.ReadOnly = true;
            this.lookupDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.lookupDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lookupDataGridView.ShowCellErrors = false;
            this.lookupDataGridView.ShowCellToolTips = false;
            this.lookupDataGridView.ShowEditingIcon = false;
            this.lookupDataGridView.ShowRowErrors = false;
            this.lookupDataGridView.Size = new System.Drawing.Size(886, 232);
            this.lookupDataGridView.TabIndex = 48;
            
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.Control;
            this.okButton.Location = new System.Drawing.Point(414, 330);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 49;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click_1);
            // 
            // LookUpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 413);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.lookupDataGridView);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "LookUpWindow";
            this.Text = "Royalty Organism Lookup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LookUpWindow_FormClosed);
            this.Load += new System.EventHandler(this.LookUpWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

  

 

        #endregion
        public System.Windows.Forms.DataGridView lookupDataGridView;
        public System.Windows.Forms.Button okButton;
    }
}


