using DevExpress.Utils;
using System.Drawing;
using DevExpress.XtraCharts;
using System;
using DevExpress.XtraCharts.Native;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DXSample {
    public class ChartControlToolTipHelperBase :IToolTipControlClient {
        public ChartControlToolTipHelperBase(ChartControl chart, bool useInitialDelay, ToolTipIconType iconType, DefaultBoolean allowHtmlText) {
            if (null == chart) throw new ArgumentNullException("chart");
            fChart = chart;
            fUseInitialDelay = useInitialDelay;
            fIconType = iconType;
            fAllowHtmlText = allowHtmlText;
        }

        public ChartControlToolTipHelperBase(ChartControl chart, bool useInitialDelay, ToolTipIconType iconType) 
            : this(chart, useInitialDelay, iconType, DefaultBoolean.Default) { }

        public ChartControlToolTipHelperBase(ChartControl chart, bool useInitialDelay) : this(chart, useInitialDelay, ToolTipIconType.None) { }

        public ChartControlToolTipHelperBase(ChartControl chart) : this(chart, true) { }

        private ChartControl fChart;
        protected ChartControl Chart { get { return fChart; } }

        private bool fUseInitialDelay;
        public bool UseInitialDelay {
            get { return fUseInitialDelay; }
            set { UseInitialDelay = value; }
        }

        private ToolTipIconType fIconType;
        public ToolTipIconType IconType {
            get { return fIconType; }
            set { fIconType = value; }
        }

        private DefaultBoolean fAllowHtmlText;
        public DefaultBoolean AllowHtmlText {
            get { return fAllowHtmlText; }
            set { fAllowHtmlText = value; }
        }

        protected virtual ToolTipControlInfo GetObjectInfo(Point point) {
            ChartHitInfo hitInfo = Chart.CalcHitInfo(point);
            if (!hitInfo.InSeries) return null;
            return new ToolTipControlInfo(hitInfo.SeriesPoint, GetToolTipText(hitInfo.SeriesPoint),
                GetToolTipCaptionForSeriesPoint(hitInfo.SeriesPoint), !UseInitialDelay, IconType, AllowHtmlText);
        }

        protected virtual string GetToolTipText(SeriesPoint point) {
            return "Base tooltip text";
        }

        protected virtual string GetToolTipCaptionForSeriesPoint(SeriesPoint point) {
            return string.Empty;
        }

        ToolTipControlInfo IToolTipControlClient.GetObjectInfo(Point point) {
            return GetObjectInfo(point);
        }

        bool IToolTipControlClient.ShowToolTips { get { return true; } }
    }

    public class ChartControlToolTipHelper :ChartControlToolTipHelperBase {
        public ChartControlToolTipHelper(ChartControl chart, bool useInitialDelay, ToolTipIconType iconType, DefaultBoolean allowHtmlText) 
            :base(chart, useInitialDelay, iconType, allowHtmlText) { }

        public ChartControlToolTipHelper(ChartControl chart, bool useInitialDelay, ToolTipIconType iconType) 
            : base(chart, useInitialDelay, iconType) { }

        public ChartControlToolTipHelper(ChartControl chart, bool useInitialDelay) : base(chart, useInitialDelay) { }

        public ChartControlToolTipHelper(ChartControl chart) : base(chart) { }

        protected override string GetToolTipText(SeriesPoint point) {
            DataRowView rowView = (DataRowView)point.Tag;
            return string.Format("Unit price = {0}\r\nUnits in stock = {1}\r\nQuantity per unit = {2}", rowView["UnitPrice"], 
                rowView["UnitsInStock"], rowView["QuantityPerUnit"]);
        }
    }
}