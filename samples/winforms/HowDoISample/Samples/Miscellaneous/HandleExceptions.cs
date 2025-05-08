using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class HandleExceptions : UserControl
    {
        public HandleExceptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // This code sets up the sample to use the overlay versus the layer.
            UseLayer(DrawingExceptionMode.DrawException, false);

            // Set the current extent to a local area.
            mapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private async void rbDrawingExceptionMode_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (button.Text == null || !button.Checked) return;            
            switch (button.Text)
            {
                case "Throw Exception":
                    UseLayer(DrawingExceptionMode.ThrowException, false);
                    break;
                case "Customize Drawing Exception":
                    UseLayer(DrawingExceptionMode.DrawException, true);
                    break;
                case "Draw Exception":
                default:
                    UseLayer(DrawingExceptionMode.DrawException, false);
                    break;
            }
            await mapView.RefreshAsync();
        }

        private void UseLayer(DrawingExceptionMode drawingExceptionMode, bool drawCustomException)
        {
            // Clear out the overlays so we start fresh
            mapView.Overlays.Clear();
            txtException.Text = "";

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay
            {
                ThrowingExceptionMode = ThrowingExceptionMode.ThrowException
            };
            if (drawingExceptionMode == DrawingExceptionMode.ThrowException)
            {
                staticOverlay.ThrowingException += (sender, e) =>
                {
                    txtException.Text = e.Exception?.InnerException.Message;
                    e.Handled = true;
                };
            }

            mapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            var wmsImageLayer = new CustomWmsLayer(new Uri("http://not_exist.com/services/service"), drawCustomException)
            {
                DrawingExceptionMode = drawingExceptionMode
            };

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;
        private Label label2;
        private TextBox txtException;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            txtException = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(1000, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton3);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtException);
            panel1.Location = new System.Drawing.Point(1000, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 1;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(20, 47);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(161, 24);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "Draw Exception";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(rbDrawingExceptionMode_CheckedChanged);
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(20, 84);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(196, 24);
            radioButton2.TabIndex = 3;
            radioButton2.Text = "Customize Drawing Exception";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(rbDrawingExceptionMode_CheckedChanged);
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton3.ForeColor = System.Drawing.Color.White;
            radioButton3.Location = new System.Drawing.Point(20, 121);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new System.Drawing.Size(231, 24);
            radioButton3.TabIndex = 4;
            radioButton3.Text = "Throw Exception";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new EventHandler(rbDrawingExceptionMode_CheckedChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(15, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(162, 25);
            label1.TabIndex = 5;
            label1.Text = "Overlay Exception:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(15, 158);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(162, 25);
            label2.TabIndex = 6;
            label2.Text = "The exception message:";
            // 
            // txtException
            // 
            this.txtException.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtException.Location = new System.Drawing.Point(15, 195);
            this.txtException.Name = "txtCurrentAngle";
            this.txtException.ReadOnly = true;
            this.txtException.Size = new System.Drawing.Size(210, 80);
            this.txtException.Multiline = true;
            this.txtException.TabIndex = 7;
            // 
            // HandleExceptions
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "HandleExceptions";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }

    public class CustomWmsLayer : Core.WmsAsyncLayer
    {
        private readonly bool _drawCustomException;
        public CustomWmsLayer(Uri uri, bool drawCustomException)
            : base(uri)
        {
            this._drawCustomException = drawCustomException;
        }

        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (!_drawCustomException)
            {
                base.DrawExceptionCore(canvas, e);
            }
            else
            {
                // customize the drawing exception. Here below we draw the error in red on orange canvas.
                canvas.DrawArea(canvas.CurrentWorldExtent, GeoBrushes.LightOrange, DrawingLevel.LevelOne);
                canvas.DrawText("Customized Exception Message", new GeoFont("Arial", 10), GeoBrushes.Red, new[] { new ScreenPointF(canvas.Width / 2, canvas.Height / 2) }, DrawingLevel.LabelLevel);
            }
        }
    }
}
