namespace KFM_Utility
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.KFMBox = new System.Windows.Forms.TextBox();
            this.KFM_Open = new System.Windows.Forms.Button();
            this.KFMFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.EncodeButton = new System.Windows.Forms.Button();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.XML_Open = new System.Windows.Forms.Button();
            this.XMLBox = new System.Windows.Forms.TextBox();
            this.Label_XML = new System.Windows.Forms.Label();
            this.Label_KFM = new System.Windows.Forms.Label();
            this.XMLFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KFMBox
            // 
            this.KFMBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.KFMBox.Location = new System.Drawing.Point(47, 6);
            this.KFMBox.Name = "KFMBox";
            this.KFMBox.Size = new System.Drawing.Size(452, 20);
            this.KFMBox.TabIndex = 2;
            // 
            // KFM_Open
            // 
            this.KFM_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.KFM_Open.Location = new System.Drawing.Point(505, 4);
            this.KFM_Open.Name = "KFM_Open";
            this.KFM_Open.Size = new System.Drawing.Size(46, 23);
            this.KFM_Open.TabIndex = 3;
            this.KFM_Open.Text = "Open";
            this.KFM_Open.UseVisualStyleBackColor = true;
            this.KFM_Open.Click += new System.EventHandler(this.KFM_Open_Click);
            // 
            // KFMFileDialog
            // 
            this.KFMFileDialog.CheckFileExists = false;
            this.KFMFileDialog.DefaultExt = "kfm";
            this.KFMFileDialog.Filter = "KFM Files (*.kfm)|*.kfm|All Files (*.*)|*.*";
            this.KFMFileDialog.Title = "KFM File";
            // 
            // EncodeButton
            // 
            this.EncodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EncodeButton.Location = new System.Drawing.Point(382, 61);
            this.EncodeButton.Name = "EncodeButton";
            this.EncodeButton.Size = new System.Drawing.Size(82, 23);
            this.EncodeButton.TabIndex = 6;
            this.EncodeButton.Text = "Encode";
            this.EncodeButton.UseVisualStyleBackColor = true;
            this.EncodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
            // 
            // DecodeButton
            // 
            this.DecodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DecodeButton.Location = new System.Drawing.Point(470, 61);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(81, 23);
            this.DecodeButton.TabIndex = 7;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // XML_Open
            // 
            this.XML_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XML_Open.Location = new System.Drawing.Point(505, 30);
            this.XML_Open.Name = "XML_Open";
            this.XML_Open.Size = new System.Drawing.Size(46, 23);
            this.XML_Open.TabIndex = 5;
            this.XML_Open.Text = "Open";
            this.XML_Open.UseVisualStyleBackColor = true;
            this.XML_Open.Click += new System.EventHandler(this.XML_Open_Click);
            // 
            // XMLBox
            // 
            this.XMLBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLBox.Location = new System.Drawing.Point(47, 32);
            this.XMLBox.Name = "XMLBox";
            this.XMLBox.Size = new System.Drawing.Size(452, 20);
            this.XMLBox.TabIndex = 4;
            // 
            // Label_XML
            // 
            this.Label_XML.AutoSize = true;
            this.Label_XML.Location = new System.Drawing.Point(12, 35);
            this.Label_XML.Name = "Label_XML";
            this.Label_XML.Size = new System.Drawing.Size(29, 13);
            this.Label_XML.TabIndex = 1;
            this.Label_XML.Text = "XML";
            // 
            // Label_KFM
            // 
            this.Label_KFM.AutoSize = true;
            this.Label_KFM.Location = new System.Drawing.Point(12, 9);
            this.Label_KFM.Name = "Label_KFM";
            this.Label_KFM.Size = new System.Drawing.Size(29, 13);
            this.Label_KFM.TabIndex = 0;
            this.Label_KFM.Text = "KFM";
            // 
            // XMLFileDialog
            // 
            this.XMLFileDialog.CheckFileExists = false;
            this.XMLFileDialog.CheckPathExists = false;
            this.XMLFileDialog.DefaultExt = "xml";
            this.XMLFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.XMLFileDialog.Title = "XML File";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(12, 66);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(0, 13);
            this.Status.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 89);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.DecodeButton);
            this.Controls.Add(this.EncodeButton);
            this.Controls.Add(this.XML_Open);
            this.Controls.Add(this.XMLBox);
            this.Controls.Add(this.KFM_Open);
            this.Controls.Add(this.KFMBox);
            this.Controls.Add(this.Label_XML);
            this.Controls.Add(this.Label_KFM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "KonverterFM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KFMBox;
        private System.Windows.Forms.Button KFM_Open;
        private System.Windows.Forms.OpenFileDialog KFMFileDialog;
        private System.Windows.Forms.Button EncodeButton;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.Button XML_Open;
        private System.Windows.Forms.TextBox XMLBox;
        private System.Windows.Forms.Label Label_XML;
        private System.Windows.Forms.Label Label_KFM;
        private System.Windows.Forms.OpenFileDialog XMLFileDialog;
        private System.Windows.Forms.Label Status;
    }
}

