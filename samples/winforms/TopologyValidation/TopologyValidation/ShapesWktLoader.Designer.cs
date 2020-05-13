namespace TopologyValidation
{
    partial class ShapesWktLoader
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
            this.tbWkt = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fromShapeFileRadio = new System.Windows.Forms.RadioButton();
            this.fromWktRadio = new System.Windows.Forms.RadioButton();
            this.shapeFilePathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.secondLayerWktTextBox = new System.Windows.Forms.TextBox();
            this.secondLayerShapeFileTextBox = new System.Windows.Forms.TextBox();
            this.secondBrowseButton = new System.Windows.Forms.Button();
            this.secondFromShapeRadio = new System.Windows.Forms.RadioButton();
            this.secondFromWktRadio = new System.Windows.Forms.RadioButton();
            this.secondGroupBox = new System.Windows.Forms.GroupBox();
            this.firstGroupBox = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.secondGroupBox.SuspendLayout();
            this.firstGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbWkt
            // 
            this.tbWkt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWkt.Location = new System.Drawing.Point(6, 91);
            this.tbWkt.Multiline = true;
            this.tbWkt.Name = "tbWkt";
            this.tbWkt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbWkt.Size = new System.Drawing.Size(500, 235);
            this.tbWkt.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(479, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(560, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fromShapeFileRadio
            // 
            this.fromShapeFileRadio.AutoSize = true;
            this.fromShapeFileRadio.Location = new System.Drawing.Point(6, 19);
            this.fromShapeFileRadio.Name = "fromShapeFileRadio";
            this.fromShapeFileRadio.Size = new System.Drawing.Size(101, 17);
            this.fromShapeFileRadio.TabIndex = 2;
            this.fromShapeFileRadio.Text = "From Shape File";
            this.fromShapeFileRadio.UseVisualStyleBackColor = true;
            // 
            // fromWktRadio
            // 
            this.fromWktRadio.AutoSize = true;
            this.fromWktRadio.Location = new System.Drawing.Point(6, 68);
            this.fromWktRadio.Name = "fromWktRadio";
            this.fromWktRadio.Size = new System.Drawing.Size(71, 17);
            this.fromWktRadio.TabIndex = 5;
            this.fromWktRadio.Text = "From Wkt";
            this.fromWktRadio.UseVisualStyleBackColor = true;
            this.fromWktRadio.CheckedChanged += new System.EventHandler(this.fromWktRadio_CheckedChanged);
            // 
            // shapeFilePathTextBox
            // 
            this.shapeFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shapeFilePathTextBox.Location = new System.Drawing.Point(6, 42);
            this.shapeFilePathTextBox.Name = "shapeFilePathTextBox";
            this.shapeFilePathTextBox.Size = new System.Drawing.Size(465, 20);
            this.shapeFilePathTextBox.TabIndex = 3;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(477, 40);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(28, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // secondLayerWktTextBox
            // 
            this.secondLayerWktTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondLayerWktTextBox.Location = new System.Drawing.Point(6, 91);
            this.secondLayerWktTextBox.Multiline = true;
            this.secondLayerWktTextBox.Name = "secondLayerWktTextBox";
            this.secondLayerWktTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.secondLayerWktTextBox.Size = new System.Drawing.Size(500, 235);
            this.secondLayerWktTextBox.TabIndex = 11;
            // 
            // secondLayerShapeFileTextBox
            // 
            this.secondLayerShapeFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondLayerShapeFileTextBox.Location = new System.Drawing.Point(6, 42);
            this.secondLayerShapeFileTextBox.Name = "secondLayerShapeFileTextBox";
            this.secondLayerShapeFileTextBox.Size = new System.Drawing.Size(466, 20);
            this.secondLayerShapeFileTextBox.TabIndex = 8;
            // 
            // secondBrowseButton
            // 
            this.secondBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secondBrowseButton.Location = new System.Drawing.Point(478, 40);
            this.secondBrowseButton.Name = "secondBrowseButton";
            this.secondBrowseButton.Size = new System.Drawing.Size(28, 23);
            this.secondBrowseButton.TabIndex = 9;
            this.secondBrowseButton.Text = "...";
            this.secondBrowseButton.UseVisualStyleBackColor = true;
            this.secondBrowseButton.Click += new System.EventHandler(this.secondBrowseButton_Click);
            // 
            // secondFromShapeRadio
            // 
            this.secondFromShapeRadio.AutoSize = true;
            this.secondFromShapeRadio.Location = new System.Drawing.Point(6, 19);
            this.secondFromShapeRadio.Name = "secondFromShapeRadio";
            this.secondFromShapeRadio.Size = new System.Drawing.Size(101, 17);
            this.secondFromShapeRadio.TabIndex = 7;
            this.secondFromShapeRadio.TabStop = true;
            this.secondFromShapeRadio.Text = "From Shape File";
            this.secondFromShapeRadio.UseVisualStyleBackColor = true;
            // 
            // secondFromWktRadio
            // 
            this.secondFromWktRadio.AutoSize = true;
            this.secondFromWktRadio.Location = new System.Drawing.Point(6, 68);
            this.secondFromWktRadio.Name = "secondFromWktRadio";
            this.secondFromWktRadio.Size = new System.Drawing.Size(71, 17);
            this.secondFromWktRadio.TabIndex = 10;
            this.secondFromWktRadio.TabStop = true;
            this.secondFromWktRadio.Text = "From Wkt";
            this.secondFromWktRadio.UseVisualStyleBackColor = true;
            this.secondFromWktRadio.CheckedChanged += new System.EventHandler(this.secondFromWktRadio_CheckedChanged);
            // 
            // secondGroupBox
            // 
            this.secondGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondGroupBox.Controls.Add(this.secondLayerShapeFileTextBox);
            this.secondGroupBox.Controls.Add(this.secondLayerWktTextBox);
            this.secondGroupBox.Controls.Add(this.secondFromShapeRadio);
            this.secondGroupBox.Controls.Add(this.secondFromWktRadio);
            this.secondGroupBox.Controls.Add(this.secondBrowseButton);
            this.secondGroupBox.Location = new System.Drawing.Point(125, 12);
            this.secondGroupBox.Name = "secondGroupBox";
            this.secondGroupBox.Size = new System.Drawing.Size(512, 332);
            this.secondGroupBox.TabIndex = 0;
            this.secondGroupBox.TabStop = false;
            // 
            // firstGroupBox
            // 
            this.firstGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstGroupBox.Controls.Add(this.fromShapeFileRadio);
            this.firstGroupBox.Controls.Add(this.fromWktRadio);
            this.firstGroupBox.Controls.Add(this.shapeFilePathTextBox);
            this.firstGroupBox.Controls.Add(this.tbWkt);
            this.firstGroupBox.Controls.Add(this.browseButton);
            this.firstGroupBox.Location = new System.Drawing.Point(125, 12);
            this.firstGroupBox.Name = "firstGroupBox";
            this.firstGroupBox.Size = new System.Drawing.Size(512, 332);
            this.firstGroupBox.TabIndex = 0;
            this.firstGroupBox.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(107, 329);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ShapesWktLoader
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(647, 389);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.firstGroupBox);
            this.Controls.Add(this.secondGroupBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "ShapesWktLoader";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import/Export WKT";
            this.Load += new System.EventHandler(this.WktImporter_Load);
            this.secondGroupBox.ResumeLayout(false);
            this.secondGroupBox.PerformLayout();
            this.firstGroupBox.ResumeLayout(false);
            this.firstGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbWkt;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton fromShapeFileRadio;
        private System.Windows.Forms.RadioButton fromWktRadio;
        private System.Windows.Forms.TextBox shapeFilePathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox secondLayerWktTextBox;
        private System.Windows.Forms.TextBox secondLayerShapeFileTextBox;
        private System.Windows.Forms.Button secondBrowseButton;
        private System.Windows.Forms.RadioButton secondFromShapeRadio;
        private System.Windows.Forms.RadioButton secondFromWktRadio;
        private System.Windows.Forms.GroupBox secondGroupBox;
        private System.Windows.Forms.GroupBox firstGroupBox;
        private System.Windows.Forms.ListBox listBox1;
    }
}