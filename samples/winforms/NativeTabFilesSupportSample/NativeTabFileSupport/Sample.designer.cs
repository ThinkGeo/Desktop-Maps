using System;
using System.IO;
using System.Management;
using DisplayASimpleMap.Properties;
using ThinkGeo.MapSuite.WinForms;
using System.Windows.Forms;
using System.Drawing;

partial class Sample
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sample));
        this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
        this.backgroundPanel = new System.Windows.Forms.Panel();
        this.functionPanel = new System.Windows.Forms.Panel();
        this.Save_Btn = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.Delete_Btn = new System.Windows.Forms.Button();
        this.Edit_Btn = new System.Windows.Forms.Button();
        this.Ellipse_Btn = new System.Windows.Forms.Button();
        this.Circle_Btn = new System.Windows.Forms.Button();
        this.Polygon_Btn = new System.Windows.Forms.Button();
        this.Square_Btn = new System.Windows.Forms.Button();
        this.Rectangle_Btn = new System.Windows.Forms.Button();
        this.Line_Btn = new System.Windows.Forms.Button();
        this.Point_Btn = new System.Windows.Forms.Button();
        this.Mouse_Btn = new System.Windows.Forms.Button();
        this.InstructionLabel = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.InstructionsPanellabel = new System.Windows.Forms.Label();
        this.sampleFooter = new Footer();
        this.sampleBanner = new Banner();
        this.backgroundPanel.SuspendLayout();
        this.functionPanel.SuspendLayout();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // winformsMap1
        // 
        this.winformsMap1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.winformsMap1.BackColor = System.Drawing.Color.White;
        this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
        this.winformsMap1.CurrentScale = 590591790D;
        this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
        this.winformsMap1.Location = new System.Drawing.Point(4, 4);
        this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
        this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
        this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
        this.winformsMap1.MaximumScale = 80000000000000D;
        this.winformsMap1.MinimumScale = 200D;
        this.winformsMap1.Name = "winformsMap1";
        this.winformsMap1.Size = new System.Drawing.Size(774, 595);
        this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        this.winformsMap1.TabIndex = 11;
        this.winformsMap1.Text = "winformsMap1";
        this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
        this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
        this.winformsMap1.MapClick += new System.EventHandler<ThinkGeo.MapSuite.WinForms.MapClickWinformsMapEventArgs>(this.winformsMap1_MapClick);
        // 
        // backgroundPanel
        // 
        this.backgroundPanel.BackColor = System.Drawing.Color.White;
        this.backgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.backgroundPanel.Controls.Add(this.functionPanel);
        this.backgroundPanel.Controls.Add(this.winformsMap1);
        this.backgroundPanel.Location = new System.Drawing.Point(12, 87);
        this.backgroundPanel.Name = "backgroundPanel";
        this.backgroundPanel.Size = new System.Drawing.Size(990, 605);
        this.backgroundPanel.TabIndex = 19;
        // 
        // functionPanel
        // 
        this.functionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.functionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
        this.functionPanel.Controls.Add(this.Save_Btn);
        this.functionPanel.Controls.Add(this.panel1);
        this.functionPanel.Controls.Add(this.InstructionLabel);
        this.functionPanel.Controls.Add(this.label1);
        this.functionPanel.Controls.Add(this.InstructionsPanellabel);
        this.functionPanel.Location = new System.Drawing.Point(784, 4);
        this.functionPanel.Name = "functionPanel";
        this.functionPanel.Size = new System.Drawing.Size(200, 595);
        this.functionPanel.TabIndex = 14;
        this.functionPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.functionPanel_Paint);
        // 
        // Save_Btn
        // 
        this.Save_Btn.Location = new System.Drawing.Point(65, 351);
        this.Save_Btn.Name = "Save_Btn";
        this.Save_Btn.Size = new System.Drawing.Size(75, 23);
        this.Save_Btn.TabIndex = 3;
        this.Save_Btn.Text = "Save";
        this.Save_Btn.UseVisualStyleBackColor = true;
        this.Save_Btn.Click += new System.EventHandler(this.Save_Btn_Click);
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.Delete_Btn);
        this.panel1.Controls.Add(this.Edit_Btn);
        this.panel1.Controls.Add(this.Ellipse_Btn);
        this.panel1.Controls.Add(this.Circle_Btn);
        this.panel1.Controls.Add(this.Polygon_Btn);
        this.panel1.Controls.Add(this.Square_Btn);
        this.panel1.Controls.Add(this.Rectangle_Btn);
        this.panel1.Controls.Add(this.Line_Btn);
        this.panel1.Controls.Add(this.Point_Btn);
        this.panel1.Controls.Add(this.Mouse_Btn);
        this.panel1.Location = new System.Drawing.Point(7, 185);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(190, 140);
        this.panel1.TabIndex = 2;
        // 
        // Delete_Btn
        // 
        this.Delete_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Delete;
        this.Delete_Btn.Location = new System.Drawing.Point(109, 103);
        this.Delete_Btn.Name = "Delete_Btn";
        this.Delete_Btn.Size = new System.Drawing.Size(27, 27);
        this.Delete_Btn.TabIndex = 5;
        this.Delete_Btn.UseVisualStyleBackColor = true;
        this.Delete_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Edit_Btn
        // 
        this.Edit_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Edit;
        this.Edit_Btn.Location = new System.Drawing.Point(54, 103);
        this.Edit_Btn.Name = "Edit_Btn";
        this.Edit_Btn.Size = new System.Drawing.Size(27, 27);
        this.Edit_Btn.TabIndex = 9;
        this.Edit_Btn.UseVisualStyleBackColor = true;
        this.Edit_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Ellipse_Btn
        // 
        this.Ellipse_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Ellipse;
        this.Ellipse_Btn.Location = new System.Drawing.Point(117, 51);
        this.Ellipse_Btn.Name = "Ellipse_Btn";
        this.Ellipse_Btn.Size = new System.Drawing.Size(27, 27);
        this.Ellipse_Btn.TabIndex = 8;
        this.Ellipse_Btn.UseVisualStyleBackColor = true;
        this.Ellipse_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Circle_Btn
        // 
        this.Circle_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Circle;
        this.Circle_Btn.Location = new System.Drawing.Point(82, 51);
        this.Circle_Btn.Name = "Circle_Btn";
        this.Circle_Btn.Size = new System.Drawing.Size(27, 27);
        this.Circle_Btn.TabIndex = 11;
        this.Circle_Btn.UseVisualStyleBackColor = true;
        this.Circle_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Polygon_Btn
        // 
        this.Polygon_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Polygon;
        this.Polygon_Btn.Location = new System.Drawing.Point(47, 51);
        this.Polygon_Btn.Name = "Polygon_Btn";
        this.Polygon_Btn.Size = new System.Drawing.Size(27, 27);
        this.Polygon_Btn.TabIndex = 10;
        this.Polygon_Btn.UseVisualStyleBackColor = true;
        this.Polygon_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Square_Btn
        // 
        this.Square_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Square;
        this.Square_Btn.Location = new System.Drawing.Point(152, 11);
        this.Square_Btn.Name = "Square_Btn";
        this.Square_Btn.Size = new System.Drawing.Size(27, 27);
        this.Square_Btn.TabIndex = 7;
        this.Square_Btn.UseVisualStyleBackColor = true;
        this.Square_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Rectangle_Btn
        // 
        this.Rectangle_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Rectangle;
        this.Rectangle_Btn.Location = new System.Drawing.Point(117, 11);
        this.Rectangle_Btn.Name = "Rectangle_Btn";
        this.Rectangle_Btn.Size = new System.Drawing.Size(27, 27);
        this.Rectangle_Btn.TabIndex = 4;
        this.Rectangle_Btn.UseVisualStyleBackColor = true;
        this.Rectangle_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Line_Btn
        // 
        this.Line_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Line;
        this.Line_Btn.Location = new System.Drawing.Point(82, 11);
        this.Line_Btn.Name = "Line_Btn";
        this.Line_Btn.Size = new System.Drawing.Size(27, 27);
        this.Line_Btn.TabIndex = 3;
        this.Line_Btn.UseVisualStyleBackColor = true;
        this.Line_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Point_Btn
        // 
        this.Point_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Point;
        this.Point_Btn.Location = new System.Drawing.Point(47, 11);
        this.Point_Btn.Name = "Point_Btn";
        this.Point_Btn.Size = new System.Drawing.Size(27, 27);
        this.Point_Btn.TabIndex = 6;
        this.Point_Btn.UseVisualStyleBackColor = true;
        this.Point_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // Mouse_Btn
        // 
        this.Mouse_Btn.Image = global::DisplayASimpleMap.Properties.Resources.Normal;
        this.Mouse_Btn.Location = new System.Drawing.Point(12, 11);
        this.Mouse_Btn.Name = "Mouse_Btn";
        this.Mouse_Btn.Size = new System.Drawing.Size(27, 27);
        this.Mouse_Btn.TabIndex = 2;
        this.Mouse_Btn.UseVisualStyleBackColor = true;
        this.Mouse_Btn.Click += new System.EventHandler(this.BtnClick_Handle);
        // 
        // InstructionLabel
        // 
        this.InstructionLabel.AutoSize = true;
        this.InstructionLabel.Location = new System.Drawing.Point(4, 23);
        this.InstructionLabel.Name = "InstructionLabel";
        this.InstructionLabel.Size = new System.Drawing.Size(192, 130);
        this.InstructionLabel.TabIndex = 1;
        this.InstructionLabel.Text = resources.GetString("InstructionLabel.Text");
        // 
        // label1
        // 
        this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(1, 160);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(198, 22);
        this.label1.TabIndex = 0;
        this.label1.Text = "Editing Controls";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // InstructionsPanellabel
        // 
        this.InstructionsPanellabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
        this.InstructionsPanellabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.InstructionsPanellabel.Location = new System.Drawing.Point(1, 1);
        this.InstructionsPanellabel.Name = "InstructionsPanellabel";
        this.InstructionsPanellabel.Size = new System.Drawing.Size(198, 22);
        this.InstructionsPanellabel.TabIndex = 0;
        this.InstructionsPanellabel.Text = "Instructions";
        this.InstructionsPanellabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // sampleFooter
        // 
        this.sampleFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.sampleFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
        this.sampleFooter.Location = new System.Drawing.Point(0, 699);
        this.sampleFooter.Name = "sampleFooter";
        this.sampleFooter.Size = new System.Drawing.Size(1014, 26);
        this.sampleFooter.TabIndex = 21;
        // 
        // sampleBanner
        // 
        this.sampleBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.sampleBanner.Location = new System.Drawing.Point(4, 0);
        this.sampleBanner.Name = "sampleBanner";
        this.sampleBanner.Size = new System.Drawing.Size(1008, 81);
        this.sampleBanner.TabIndex = 20;
        // 
        // Sample
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
        this.ClientSize = new System.Drawing.Size(1014, 732);
        this.Controls.Add(this.sampleFooter);
        this.Controls.Add(this.sampleBanner);
        this.Controls.Add(this.backgroundPanel);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "Sample";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Native TAB File Support - Map Suite sample application";
        this.Load += new System.EventHandler(this.Sample_Load);
        this.backgroundPanel.ResumeLayout(false);
        this.functionPanel.ResumeLayout(false);
        this.functionPanel.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);

    }


    private void functionPanel_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(124, 171, 197)),
        e.ClipRectangle.Left,
        e.ClipRectangle.Top,
        e.ClipRectangle.Width - 1,
        e.ClipRectangle.Height - 1);
        base.OnPaint(e);
    }

    #endregion

    private WinformsMap winformsMap1;
    private System.Windows.Forms.Panel backgroundPanel;
    private System.Windows.Forms.Panel functionPanel;
    private Banner sampleBanner;
    private Footer sampleFooter;
    private System.Windows.Forms.Label InstructionsPanellabel;
    private System.Windows.Forms.Label InstructionLabel;
    private Panel panel1;
    private Label label1;
    private Button Save_Btn;
    private Button Delete_Btn;
    private Button Edit_Btn;
    private Button Ellipse_Btn;
    private Button Circle_Btn;
    private Button Polygon_Btn;
    private Button Square_Btn;
    private Button Rectangle_Btn;
    private Button Line_Btn;
    private Button Point_Btn;
    private Button Mouse_Btn;
}
