namespace ICDAR2PascalVoc
{
    partial class FormConvertICDARToPascalVoc
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
            this.buttonSaveAsPASCALVOCFormat = new System.Windows.Forms.Button();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // buttonSaveAsPASCALVOCFormat
            // 
            this.buttonSaveAsPASCALVOCFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAsPASCALVOCFormat.Location = new System.Drawing.Point(9, 441);
            this.buttonSaveAsPASCALVOCFormat.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveAsPASCALVOCFormat.Name = "buttonSaveAsPASCALVOCFormat";
            this.buttonSaveAsPASCALVOCFormat.Size = new System.Drawing.Size(511, 26);
            this.buttonSaveAsPASCALVOCFormat.TabIndex = 1;
            this.buttonSaveAsPASCALVOCFormat.Text = "Save As PASCAL VOC Format";
            this.buttonSaveAsPASCALVOCFormat.UseVisualStyleBackColor = true;
            this.buttonSaveAsPASCALVOCFormat.Click += new System.EventHandler(this.buttonSaveAsPASCALVOCFormat_Click);
            this.buttonSaveAsPASCALVOCFormat.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragDrop);
            this.buttonSaveAsPASCALVOCFormat.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragEnter);
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowDrop = true;
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Location = new System.Drawing.Point(9, 9);
            this.listViewFiles.Margin = new System.Windows.Forms.Padding(2);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(512, 423);
            this.listViewFiles.TabIndex = 2;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragDrop);
            this.listViewFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragEnter);
            this.listViewFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewFiles_KeyDown);
            // 
            // ConvertICDARToPascalVocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 475);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.buttonSaveAsPASCALVOCFormat);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "ConvertICDARToPascalVocForm";
            this.Text = "ConvertICDARToPascalVocForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSaveAsPASCALVOCFormat;
        private System.Windows.Forms.ListView listViewFiles;
    }
}