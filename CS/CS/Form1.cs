using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using System.Drawing;
using DXSample;

namespace docShowSeriesPointTooltip {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private ToolTipController toolTipController1 = new ToolTipController();

        private void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.nwindDataSet.Products);
            toolTipController1.ReshowDelay = 300;
            toolTipController1.InitialDelay = 300;
            toolTipController1.AddClientControl(chartControl1, new ChartControlToolTipHelper(chartControl1));
            toolTipController1.BeforeShow += new ToolTipControllerBeforeShowEventHandler(OnToolTipControllerBeforeShow);
        }

        private void OnToolTipControllerBeforeShow(object sender, ToolTipControllerShowEventArgs e) {
            SeriesPoint point = e.SelectedObject as SeriesPoint;
            if (point == null) return;
            DataRowView rowView = point.Tag as DataRowView;
            if (rowView == null) return;
            if (Convert.ToDouble(rowView["UnitPrice"]) > 25) e.Appearance.BackColor = Color.Blue;
            else e.Appearance.BackColor = Color.Green;
        }
    }
}

