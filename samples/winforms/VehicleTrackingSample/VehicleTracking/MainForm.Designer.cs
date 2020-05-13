using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AutoRefreshStatusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editContainerPanel = new System.Windows.Forms.Panel();
            this.rbtnDeleteFence = new System.Windows.Forms.RadioButton();
            this.rbtnSaveFence = new System.Windows.Forms.RadioButton();
            this.CancelEditRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.RefreshRadioButton = new System.Windows.Forms.RadioButton();
            this.AutoRefreshRadioButton = new System.Windows.Forms.RadioButton();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.PanRadioButton = new System.Windows.Forms.RadioButton();
            this.rbtnEdit = new System.Windows.Forms.RadioButton();
            this.rbtnMeasure = new System.Windows.Forms.RadioButton();
            this.editPanel = new System.Windows.Forms.Panel();
            this.DrawNewFenceRadioButton = new System.Windows.Forms.RadioButton();
            this.EditFenceRadioButton = new System.Windows.Forms.RadioButton();
            this.measurePanel = new System.Windows.Forms.Panel();
            this.rbtnMeasureArea = new System.Windows.Forms.RadioButton();
            this.MeasureLineRadioButton = new System.Windows.Forms.RadioButton();
            this.measureContainerPanel = new System.Windows.Forms.Panel();
            this.rbtnCancelMeasure = new System.Windows.Forms.RadioButton();
            this.MeasureUnitComboBox = new System.Windows.Forms.ComboBox();
            this.measureUnitLabel = new System.Windows.Forms.Label();
            this.dataRepeater = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.lblDuration = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.LinkLabel();
            this.picGreenBall = new System.Windows.Forms.PictureBox();
            this.VehiclePictureBox = new System.Windows.Forms.PictureBox();
            this.VehiclesGroupPictureBox = new System.Windows.Forms.PictureBox();
            this.testPanel = new System.Windows.Forms.Panel();
            this.ControlGroupPictureBox = new System.Windows.Forms.PictureBox();
            this.RefreshGroupPictureBox = new System.Windows.Forms.PictureBox();
            this.CollapsPictureBox = new System.Windows.Forms.PictureBox();
            this.winformsMap = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.grayPanel = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LocationYToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LocationXToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.editContainerPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.editPanel.SuspendLayout();
            this.measurePanel.SuspendLayout();
            this.measureContainerPanel.SuspendLayout();
            this.dataRepeater.ItemTemplate.SuspendLayout();
            this.dataRepeater.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGreenBall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclesGroupPictureBox)).BeginInit();
            this.VehiclesGroupPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControlGroupPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshGroupPictureBox)).BeginInit();
            this.RefreshGroupPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CollapsPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayPanel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Click to refresh vehicles:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Auto Refresh:";
            // 
            // AutoRefreshStatusLabel
            // 
            this.AutoRefreshStatusLabel.AutoSize = true;
            this.AutoRefreshStatusLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoRefreshStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.AutoRefreshStatusLabel.Location = new System.Drawing.Point(151, 52);
            this.AutoRefreshStatusLabel.Name = "AutoRefreshStatusLabel";
            this.AutoRefreshStatusLabel.Size = new System.Drawing.Size(28, 19);
            this.AutoRefreshStatusLabel.TabIndex = 7;
            this.AutoRefreshStatusLabel.Text = "On";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(65, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Refresh Manually";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tracked Vehicles";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(218)))));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(7, 223);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 28);
            this.panel1.TabIndex = 8;
            // 
            // editContainerPanel
            // 
            this.editContainerPanel.BackColor = System.Drawing.Color.Transparent;
            this.editContainerPanel.Controls.Add(this.rbtnDeleteFence);
            this.editContainerPanel.Controls.Add(this.rbtnSaveFence);
            this.editContainerPanel.Controls.Add(this.CancelEditRadioButton);
            this.editContainerPanel.Location = new System.Drawing.Point(83, 3);
            this.editContainerPanel.Name = "editContainerPanel";
            this.editContainerPanel.Size = new System.Drawing.Size(87, 26);
            this.editContainerPanel.TabIndex = 8;
            // 
            // rbtnDeleteFence
            // 
            this.rbtnDeleteFence.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDeleteFence.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnDeleteFence.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnDeleteFence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDeleteFence.Image = ((System.Drawing.Image)(resources.GetObject("rbtnDeleteFence.Image")));
            this.rbtnDeleteFence.Location = new System.Drawing.Point(0, 0);
            this.rbtnDeleteFence.Name = "rbtnDeleteFence";
            this.rbtnDeleteFence.Size = new System.Drawing.Size(26, 26);
            this.rbtnDeleteFence.TabIndex = 12;
            this.rbtnDeleteFence.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnDeleteFence.Click += new System.EventHandler(this.DeleteFenceClick);
            // 
            // rbtnSaveFence
            // 
            this.rbtnSaveFence.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnSaveFence.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnSaveFence.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnSaveFence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnSaveFence.Image = ((System.Drawing.Image)(resources.GetObject("rbtnSaveFence.Image")));
            this.rbtnSaveFence.Location = new System.Drawing.Point(28, 0);
            this.rbtnSaveFence.Name = "rbtnSaveFence";
            this.rbtnSaveFence.Size = new System.Drawing.Size(26, 26);
            this.rbtnSaveFence.TabIndex = 12;
            this.rbtnSaveFence.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnSaveFence.Click += new System.EventHandler(this.SaveFenceClick);
            // 
            // CancelEditRadioButton
            // 
            this.CancelEditRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelEditRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.CancelEditRadioButton.BackgroundImage = global::ThinkGeo.MapSuite.VehicleTracking.Properties.Resources.clear;
            this.CancelEditRadioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelEditRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelEditRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.CancelEditRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelEditRadioButton.Location = new System.Drawing.Point(59, 0);
            this.CancelEditRadioButton.Name = "CancelEditRadioButton";
            this.CancelEditRadioButton.Size = new System.Drawing.Size(26, 26);
            this.CancelEditRadioButton.TabIndex = 12;
            this.CancelEditRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.CancelEditRadioButton.Click += new System.EventHandler(this.CancelEditClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 21);
            this.label6.TabIndex = 4;
            this.label6.Text = "Interact with the map using these tools:";
            // 
            // RefreshRadioButton
            // 
            this.RefreshRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.RefreshRadioButton.AutoSize = true;
            this.RefreshRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RefreshRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshRadioButton.Image")));
            this.RefreshRadioButton.Location = new System.Drawing.Point(10, 47);
            this.RefreshRadioButton.Name = "RefreshRadioButton";
            this.RefreshRadioButton.Size = new System.Drawing.Size(36, 36);
            this.RefreshRadioButton.TabIndex = 6;
            this.RefreshRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.RefreshRadioButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // AutoRefreshRadioButton
            // 
            this.AutoRefreshRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.AutoRefreshRadioButton.AutoSize = true;
            this.AutoRefreshRadioButton.Checked = true;
            this.AutoRefreshRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AutoRefreshRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.AutoRefreshRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoRefreshRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("AutoRefreshRadioButton.Image")));
            this.AutoRefreshRadioButton.Location = new System.Drawing.Point(10, 7);
            this.AutoRefreshRadioButton.Name = "AutoRefreshRadioButton";
            this.AutoRefreshRadioButton.Size = new System.Drawing.Size(36, 36);
            this.AutoRefreshRadioButton.TabIndex = 6;
            this.AutoRefreshRadioButton.TabStop = true;
            this.AutoRefreshRadioButton.UseVisualStyleBackColor = true;
            this.AutoRefreshRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.AutoRefreshRadioButton.Click += new System.EventHandler(this.AutoRefreshButton_Click);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.controlPanel.Controls.Add(this.PanRadioButton);
            this.controlPanel.Controls.Add(this.rbtnEdit);
            this.controlPanel.Controls.Add(this.rbtnMeasure);
            this.controlPanel.Controls.Add(this.editPanel);
            this.controlPanel.Controls.Add(this.measurePanel);
            //this.controlPanel.Controls.Add(this.dataRepeater);
            this.controlPanel.Controls.Add(this.VehiclesGroupPictureBox);
            this.controlPanel.Controls.Add(this.label1);
            this.controlPanel.Controls.Add(this.panel1);
            this.controlPanel.Controls.Add(this.label6);
            this.controlPanel.Controls.Add(this.AutoRefreshStatusLabel);
            this.controlPanel.Controls.Add(this.label4);
            this.controlPanel.Controls.Add(this.label2);
            this.controlPanel.Controls.Add(this.ControlGroupPictureBox);
            this.controlPanel.Controls.Add(this.RefreshGroupPictureBox);
            this.controlPanel.Location = new System.Drawing.Point(0, 45);
            this.controlPanel.MinimumSize = new System.Drawing.Size(0, 455);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(305, 566);
            this.controlPanel.TabIndex = 9;
            // 
            // PanRadioButton
            // 
            this.PanRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.PanRadioButton.AutoSize = true;
            this.PanRadioButton.Checked = true;
            this.PanRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PanRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.PanRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PanRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("PanRadioButton.Image")));
            this.PanRadioButton.Location = new System.Drawing.Point(14, 167);
            this.PanRadioButton.Name = "PanRadioButton";
            this.PanRadioButton.Size = new System.Drawing.Size(40, 40);
            this.PanRadioButton.TabIndex = 14;
            this.PanRadioButton.TabStop = true;
            this.PanRadioButton.UseVisualStyleBackColor = true;
            this.PanRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.PanRadioButton.Click += new System.EventHandler(this.ControlMap_Click);
            // 
            // rbtnEdit
            // 
            this.rbtnEdit.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnEdit.AutoSize = true;
            this.rbtnEdit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnEdit.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("rbtnEdit.Image")));
            this.rbtnEdit.Location = new System.Drawing.Point(60, 167);
            this.rbtnEdit.Name = "rbtnEdit";
            this.rbtnEdit.Size = new System.Drawing.Size(40, 40);
            this.rbtnEdit.TabIndex = 14;
            this.rbtnEdit.UseVisualStyleBackColor = true;
            this.rbtnEdit.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnEdit.Click += new System.EventHandler(this.DrawFence_Click);
            // 
            // rbtnMeasure
            // 
            this.rbtnMeasure.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMeasure.AutoSize = true;
            this.rbtnMeasure.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnMeasure.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnMeasure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMeasure.ForeColor = System.Drawing.Color.Transparent;
            this.rbtnMeasure.Image = ((System.Drawing.Image)(resources.GetObject("rbtnMeasure.Image")));
            this.rbtnMeasure.Location = new System.Drawing.Point(106, 167);
            this.rbtnMeasure.Name = "rbtnMeasure";
            this.rbtnMeasure.Size = new System.Drawing.Size(40, 40);
            this.rbtnMeasure.TabIndex = 14;
            this.rbtnMeasure.UseVisualStyleBackColor = true;
            this.rbtnMeasure.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnMeasure.Click += new System.EventHandler(this.MeasureDistance_Click);
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.DrawNewFenceRadioButton);
            this.editPanel.Controls.Add(this.EditFenceRadioButton);
            this.editPanel.Controls.Add(this.editContainerPanel);
            this.editPanel.Location = new System.Drawing.Point(7, 223);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(280, 35);
            this.editPanel.TabIndex = 11;
            this.editPanel.Visible = false;
            // 
            // DrawNewFenceRadioButton
            // 
            this.DrawNewFenceRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.DrawNewFenceRadioButton.Checked = true;
            this.DrawNewFenceRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.DrawNewFenceRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.DrawNewFenceRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawNewFenceRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("DrawNewFenceRadioButton.Image")));
            this.DrawNewFenceRadioButton.Location = new System.Drawing.Point(7, 3);
            this.DrawNewFenceRadioButton.Name = "DrawNewFenceRadioButton";
            this.DrawNewFenceRadioButton.Size = new System.Drawing.Size(26, 26);
            this.DrawNewFenceRadioButton.TabIndex = 12;
            this.DrawNewFenceRadioButton.TabStop = true;
            this.DrawNewFenceRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.DrawNewFenceRadioButton.Click += new System.EventHandler(this.DrawNewFenceClick);
            // 
            // EditFenceRadioButton
            // 
            this.EditFenceRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.EditFenceRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.EditFenceRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.EditFenceRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditFenceRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("EditFenceRadioButton.Image")));
            this.EditFenceRadioButton.Location = new System.Drawing.Point(38, 3);
            this.EditFenceRadioButton.Name = "EditFenceRadioButton";
            this.EditFenceRadioButton.Size = new System.Drawing.Size(26, 26);
            this.EditFenceRadioButton.TabIndex = 12;
            this.EditFenceRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.EditFenceRadioButton.Click += new System.EventHandler(this.EditFenceClick);
            // 
            // measurePanel
            // 
            this.measurePanel.Controls.Add(this.rbtnMeasureArea);
            this.measurePanel.Controls.Add(this.MeasureLineRadioButton);
            this.measurePanel.Controls.Add(this.measureContainerPanel);
            this.measurePanel.Controls.Add(this.MeasureUnitComboBox);
            this.measurePanel.Controls.Add(this.measureUnitLabel);
            this.measurePanel.Location = new System.Drawing.Point(9, 223);
            this.measurePanel.Name = "measurePanel";
            this.measurePanel.Size = new System.Drawing.Size(256, 35);
            this.measurePanel.TabIndex = 11;
            this.measurePanel.Visible = false;
            // 
            // rbtnMeasureArea
            // 
            this.rbtnMeasureArea.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMeasureArea.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnMeasureArea.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnMeasureArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMeasureArea.Image = ((System.Drawing.Image)(resources.GetObject("rbtnMeasureArea.Image")));
            this.rbtnMeasureArea.Location = new System.Drawing.Point(38, 3);
            this.rbtnMeasureArea.Name = "rbtnMeasureArea";
            this.rbtnMeasureArea.Size = new System.Drawing.Size(26, 26);
            this.rbtnMeasureArea.TabIndex = 12;
            this.rbtnMeasureArea.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnMeasureArea.Click += new System.EventHandler(this.MeasurePolygonClick);
            // 
            // MeasureLineRadioButton
            // 
            this.MeasureLineRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.MeasureLineRadioButton.Checked = true;
            this.MeasureLineRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MeasureLineRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.MeasureLineRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MeasureLineRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("MeasureLineRadioButton.Image")));
            this.MeasureLineRadioButton.Location = new System.Drawing.Point(7, 3);
            this.MeasureLineRadioButton.Name = "MeasureLineRadioButton";
            this.MeasureLineRadioButton.Size = new System.Drawing.Size(26, 26);
            this.MeasureLineRadioButton.TabIndex = 12;
            this.MeasureLineRadioButton.TabStop = true;
            this.MeasureLineRadioButton.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.MeasureLineRadioButton.Click += new System.EventHandler(this.MeasureLineClick);
            // 
            // measureContainerPanel
            // 
            this.measureContainerPanel.Controls.Add(this.rbtnCancelMeasure);
            this.measureContainerPanel.Location = new System.Drawing.Point(74, 3);
            this.measureContainerPanel.Name = "measureContainerPanel";
            this.measureContainerPanel.Size = new System.Drawing.Size(26, 26);
            this.measureContainerPanel.TabIndex = 11;
            // 
            // rbtnCancelMeasure
            // 
            this.rbtnCancelMeasure.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnCancelMeasure.BackgroundImage = global::ThinkGeo.MapSuite.VehicleTracking.Properties.Resources.clear;
            this.rbtnCancelMeasure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnCancelMeasure.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnCancelMeasure.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnCancelMeasure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnCancelMeasure.Location = new System.Drawing.Point(0, 0);
            this.rbtnCancelMeasure.Name = "rbtnCancelMeasure";
            this.rbtnCancelMeasure.Size = new System.Drawing.Size(26, 26);
            this.rbtnCancelMeasure.TabIndex = 12;
            this.rbtnCancelMeasure.CheckedChanged += new System.EventHandler(this.RefreshRadioButton_CheckedChanged);
            this.rbtnCancelMeasure.Click += new System.EventHandler(this.CancelMeasureClick);
            // 
            // MeasureUnitComboBox
            // 
            this.MeasureUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MeasureUnitComboBox.DropDownWidth = 70;
            this.MeasureUnitComboBox.Items.AddRange(new object[] {
            "Imperial",
            "Metric"});
            this.MeasureUnitComboBox.Location = new System.Drawing.Point(184, 5);
            this.MeasureUnitComboBox.Name = "MeasureUnitComboBox";
            this.MeasureUnitComboBox.Size = new System.Drawing.Size(70, 21);
            this.MeasureUnitComboBox.TabIndex = 12;
            this.MeasureUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.MeasureUnitComboBox_SelectedIndexChanged);
            // 
            // measureUnitLabel
            // 
            this.measureUnitLabel.AutoSize = true;
            this.measureUnitLabel.Location = new System.Drawing.Point(112, 9);
            this.measureUnitLabel.Name = "measureUnitLabel";
            this.measureUnitLabel.Size = new System.Drawing.Size(73, 13);
            this.measureUnitLabel.TabIndex = 12;
            this.measureUnitLabel.Text = "Measure Unit:";
            // 
            // dataRepeater
            // 
            this.dataRepeater.AllowUserToAddItems = false;
            this.dataRepeater.AllowUserToDeleteItems = false;
            this.dataRepeater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataRepeater.ItemHeaderVisible = false;
            // 
            // dataRepeater.ItemTemplate
            // 
            this.dataRepeater.ItemTemplate.Controls.Add(this.lblDuration);
            this.dataRepeater.ItemTemplate.Controls.Add(this.label9);
            this.dataRepeater.ItemTemplate.Controls.Add(this.lblSpeed);
            this.dataRepeater.ItemTemplate.Controls.Add(this.lblArea);
            this.dataRepeater.ItemTemplate.Controls.Add(this.label8);
            this.dataRepeater.ItemTemplate.Controls.Add(this.label7);
            this.dataRepeater.ItemTemplate.Controls.Add(this.lblStatus);
            this.dataRepeater.ItemTemplate.Controls.Add(this.lblName);
            this.dataRepeater.ItemTemplate.Controls.Add(this.picGreenBall);
            this.dataRepeater.ItemTemplate.Controls.Add(this.VehiclePictureBox);
            this.dataRepeater.ItemTemplate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataRepeater.ItemTemplate.Size = new System.Drawing.Size(262, 100);
            this.dataRepeater.Location = new System.Drawing.Point(12, 275);
            this.dataRepeater.Name = "dataRepeater";
            this.dataRepeater.Size = new System.Drawing.Size(270, 280);
            this.dataRepeater.TabIndex = 10;
            this.dataRepeater.Text = "dataRepeater1";
            this.dataRepeater.Visible = false;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(116, 75);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 1;
            this.lblDuration.Text = "Duration:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Duration:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(116, 62);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblSpeed.TabIndex = 1;
            this.lblSpeed.Text = "Speed:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(116, 49);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(32, 13);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Area:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Speed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Area:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblStatus.Location = new System.Drawing.Point(74, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "lblStatus";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.LinkColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(61, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 13);
            this.lblName.TabIndex = 1;
            this.lblName.TabStop = true;
            this.lblName.Text = "lblName";
            this.lblName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.VehicleNameLabel_LinkClicked);
            // 
            // picGreenBall
            // 
            this.picGreenBall.Image = ((System.Drawing.Image)(resources.GetObject("picGreenBall.Image")));
            this.picGreenBall.Location = new System.Drawing.Point(63, 26);
            this.picGreenBall.Name = "picGreenBall";
            this.picGreenBall.Size = new System.Drawing.Size(11, 11);
            this.picGreenBall.TabIndex = 0;
            this.picGreenBall.TabStop = false;
            // 
            // VehiclePictureBox
            // 
            this.VehiclePictureBox.Location = new System.Drawing.Point(10, 3);
            this.VehiclePictureBox.Name = "VehiclePictureBox";
            this.VehiclePictureBox.Size = new System.Drawing.Size(45, 33);
            this.VehiclePictureBox.TabIndex = 0;
            this.VehiclePictureBox.TabStop = false;
            this.VehiclePictureBox.Click += new System.EventHandler(this.VehiclePictureBox_Click);
            // 
            // VehiclesGroupPictureBox
            // 
            this.VehiclesGroupPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VehiclesGroupPictureBox.Controls.Add(this.testPanel);
            this.VehiclesGroupPictureBox.Location = new System.Drawing.Point(7, 264);
            this.VehiclesGroupPictureBox.Name = "VehiclesGroupPictureBox";
            this.VehiclesGroupPictureBox.Size = new System.Drawing.Size(282, 297);
            this.VehiclesGroupPictureBox.TabIndex = 9;
            this.VehiclesGroupPictureBox.TabStop = false;
            // 
            // testPanel
            // 
            this.testPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testPanel.AutoScroll = true;
            this.testPanel.Location = new System.Drawing.Point(5, 5);
            this.testPanel.Name = "testPanel";
            this.testPanel.Size = new System.Drawing.Size(272, 280);
            this.testPanel.TabIndex = 9;
            // 
            // ControlGroupPictureBox
            // 
            this.ControlGroupPictureBox.Location = new System.Drawing.Point(7, 160);
            this.ControlGroupPictureBox.Name = "ControlGroupPictureBox";
            this.ControlGroupPictureBox.Size = new System.Drawing.Size(280, 54);
            this.ControlGroupPictureBox.TabIndex = 9;
            this.ControlGroupPictureBox.TabStop = false;
            // 
            // RefreshGroupPictureBox
            // 
            this.RefreshGroupPictureBox.Controls.Add(this.AutoRefreshRadioButton);
            this.RefreshGroupPictureBox.Controls.Add(this.RefreshRadioButton);
            this.RefreshGroupPictureBox.Location = new System.Drawing.Point(9, 37);
            this.RefreshGroupPictureBox.Name = "RefreshGroupPictureBox";
            this.RefreshGroupPictureBox.Size = new System.Drawing.Size(280, 90);
            this.RefreshGroupPictureBox.TabIndex = 9;
            this.RefreshGroupPictureBox.TabStop = false;
            // 
            // CollapsPictureBox
            // 
            this.CollapsPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CollapsPictureBox.Image")));
            this.CollapsPictureBox.Location = new System.Drawing.Point(295, 239);
            this.CollapsPictureBox.Name = "CollapsPictureBox";
            this.CollapsPictureBox.Size = new System.Drawing.Size(10, 104);
            this.CollapsPictureBox.TabIndex = 10;
            this.CollapsPictureBox.TabStop = false;
            this.CollapsPictureBox.Click += new System.EventHandler(this.CollapsPictureBox_Click);
            this.CollapsPictureBox.MouseEnter += new System.EventHandler(this.CollapsPictureBox_MouseEnter);
            this.CollapsPictureBox.MouseLeave += new System.EventHandler(this.CollapsPictureBox_MouseLeave);
            // 
            // winformsMap
            // 
            this.winformsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winformsMap.BackColor = System.Drawing.Color.White;
            this.winformsMap.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap.CurrentScale = 590591790D;
            this.winformsMap.DrawingQuality = DrawingQuality.Default;
            this.winformsMap.Location = new System.Drawing.Point(315, 45);
            this.winformsMap.MapFocusMode = WinForms.MapFocusMode.Default;
            this.winformsMap.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap.MaximumScale = 80000000000000D;
            this.winformsMap.MinimumScale = 200D;
            this.winformsMap.Name = "winformsMap";
            this.winformsMap.Size = new System.Drawing.Size(824, 566);
            this.winformsMap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap.TabIndex = 11;
            this.winformsMap.Text = "winformsMap1";
            this.winformsMap.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap.ThreadingMode = MapThreadingMode.Default;
            this.winformsMap.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            this.winformsMap.MapClick += new System.EventHandler<MapClickWinformsMapEventArgs>(this.WinformsMap1_MapClick);
            this.winformsMap.CurrentExtentChanged += new System.EventHandler<CurrentExtentChangedWinformsMapEventArgs>(this.WinformsMap1_CurrentExtentChanged);
            this.winformsMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WinformsMap1_MouseMove);
            this.winformsMap.Resize += new System.EventHandler(this.WinformsMap1_Resize);
            // 
            // grayPanel
            // 
            this.grayPanel.Location = new System.Drawing.Point(305, 46);
            this.grayPanel.Name = "grayPanel";
            this.grayPanel.Size = new System.Drawing.Size(10, 566);
            this.grayPanel.TabIndex = 12;
            this.grayPanel.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LocationYToolStripStatusLabel,
            this.LocationXToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 615);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1139, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LocationYToolStripStatusLabel
            // 
            this.LocationYToolStripStatusLabel.AutoSize = false;
            this.LocationYToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.LocationYToolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LocationYToolStripStatusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.LocationYToolStripStatusLabel.Name = "LocationYToolStripStatusLabel";
            this.LocationYToolStripStatusLabel.Size = new System.Drawing.Size(95, 17);
            this.LocationYToolStripStatusLabel.Text = "Y:000000";
            this.LocationYToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LocationXToolStripStatusLabel
            // 
            this.LocationXToolStripStatusLabel.AutoSize = false;
            this.LocationXToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.LocationXToolStripStatusLabel.Name = "LocationXToolStripStatusLabel";
            this.LocationXToolStripStatusLabel.Size = new System.Drawing.Size(95, 17);
            this.LocationXToolStripStatusLabel.Text = "X:000000";
            this.LocationXToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // headerPanel
            // 
            this.headerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("headerPanel.BackgroundImage")));
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1139, 46);
            this.headerPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1139, 637);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grayPanel);
            this.Controls.Add(this.CollapsPictureBox);
            this.Controls.Add(this.winformsMap);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.headerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1155, 675);
            this.Name = "MainForm";
            this.Text = "Vehicle Tracking";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.editContainerPanel.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.editPanel.ResumeLayout(false);
            this.measurePanel.ResumeLayout(false);
            this.measurePanel.PerformLayout();
            this.measureContainerPanel.ResumeLayout(false);
            this.dataRepeater.ItemTemplate.ResumeLayout(false);
            this.dataRepeater.ItemTemplate.PerformLayout();
            this.dataRepeater.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGreenBall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclesGroupPictureBox)).EndInit();
            this.VehiclesGroupPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ControlGroupPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshGroupPictureBox)).EndInit();
            this.RefreshGroupPictureBox.ResumeLayout(false);
            this.RefreshGroupPictureBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CollapsPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayPanel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton AutoRefreshRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AutoRefreshStatusLabel;
        private System.Windows.Forms.RadioButton RefreshRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel editContainerPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.PictureBox CollapsPictureBox;
        private WinForms.WinformsMap winformsMap;
        private System.Windows.Forms.PictureBox grayPanel;
        private System.Windows.Forms.PictureBox VehiclesGroupPictureBox;
        private System.Windows.Forms.Panel testPanel;
        private System.Windows.Forms.PictureBox ControlGroupPictureBox;
        private System.Windows.Forms.PictureBox RefreshGroupPictureBox;
        private Microsoft.VisualBasic.PowerPacks.DataRepeater dataRepeater;
        private System.Windows.Forms.LinkLabel lblName;
        private System.Windows.Forms.PictureBox VehiclePictureBox;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.PictureBox picGreenBall;
        private System.Windows.Forms.Panel editPanel;
        private System.Windows.Forms.RadioButton DrawNewFenceRadioButton;
        private System.Windows.Forms.RadioButton CancelEditRadioButton;
        private System.Windows.Forms.RadioButton rbtnSaveFence;
        private System.Windows.Forms.RadioButton rbtnDeleteFence;
        private System.Windows.Forms.RadioButton EditFenceRadioButton;
        private System.Windows.Forms.Panel measurePanel;
        private System.Windows.Forms.Panel measureContainerPanel;
        private System.Windows.Forms.RadioButton rbtnCancelMeasure;
        private System.Windows.Forms.ComboBox MeasureUnitComboBox;
        private System.Windows.Forms.Label measureUnitLabel;
        private System.Windows.Forms.RadioButton rbtnMeasureArea;
        private System.Windows.Forms.RadioButton MeasureLineRadioButton;
        private System.Windows.Forms.RadioButton rbtnMeasure;
        private System.Windows.Forms.RadioButton rbtnEdit;
        private System.Windows.Forms.RadioButton PanRadioButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LocationYToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel LocationXToolStripStatusLabel;
    }
}

