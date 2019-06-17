Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.Utils
Imports System.Drawing
Imports DXSample

Namespace docShowSeriesPointTooltip
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private toolTipController1 As New ToolTipController()

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' TODO: This line of code loads data into the 'nwindDataSet.Products' table. You can move, or remove it, as needed.
			Me.productsTableAdapter.Fill(Me.nwindDataSet.Products)
			toolTipController1.ReshowDelay = 300
			toolTipController1.InitialDelay = 300
			toolTipController1.AddClientControl(chartControl1, New ChartControlToolTipHelper(chartControl1))
			AddHandler toolTipController1.BeforeShow, AddressOf OnToolTipControllerBeforeShow
		End Sub

		Private Sub OnToolTipControllerBeforeShow(ByVal sender As Object, ByVal e As ToolTipControllerShowEventArgs)
			Dim point As SeriesPoint = TryCast(e.SelectedObject, SeriesPoint)
			If point Is Nothing Then
				Return
			End If
			Dim rowView As DataRowView = TryCast(point.Tag, DataRowView)
			If rowView Is Nothing Then
				Return
			End If
			If Convert.ToDouble(rowView("UnitPrice")) > 25 Then
				e.Appearance.BackColor = Color.Blue
			Else
				e.Appearance.BackColor = Color.Green
			End If
		End Sub
	End Class
End Namespace

