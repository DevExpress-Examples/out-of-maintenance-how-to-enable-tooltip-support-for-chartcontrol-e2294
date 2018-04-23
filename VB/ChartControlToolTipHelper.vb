Imports Microsoft.VisualBasic
Imports DevExpress.Utils
Imports System.Drawing
Imports DevExpress.XtraCharts
Imports System
Imports DevExpress.XtraCharts.Native
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace DXSample
	Public Class ChartControlToolTipHelperBase
		Implements IToolTipControlClient
		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean, ByVal iconType As ToolTipIconType, ByVal allowHtmlText As DefaultBoolean)
			If Nothing Is chart Then
				Throw New ArgumentNullException("chart")
			End If
			fChart = chart
			fUseInitialDelay = useInitialDelay
			fIconType = iconType
			fAllowHtmlText = allowHtmlText
		End Sub

		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean, ByVal iconType As ToolTipIconType)
			Me.New(chart, useInitialDelay, iconType, DefaultBoolean.Default)
		End Sub

		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean)
			Me.New(chart, useInitialDelay, ToolTipIconType.None)
		End Sub

		Public Sub New(ByVal chart As ChartControl)
			Me.New(chart, True)
		End Sub

		Private fChart As ChartControl
		Protected ReadOnly Property Chart() As ChartControl
			Get
				Return fChart
			End Get
		End Property

		Private fUseInitialDelay As Boolean
		Public Property UseInitialDelay() As Boolean
			Get
				Return fUseInitialDelay
			End Get
			Set(ByVal value As Boolean)
				UseInitialDelay = value
			End Set
		End Property

		Private fIconType As ToolTipIconType
		Public Property IconType() As ToolTipIconType
			Get
				Return fIconType
			End Get
			Set(ByVal value As ToolTipIconType)
				fIconType = value
			End Set
		End Property

		Private fAllowHtmlText As DefaultBoolean
		Public Property AllowHtmlText() As DefaultBoolean
			Get
				Return fAllowHtmlText
			End Get
			Set(ByVal value As DefaultBoolean)
				fAllowHtmlText = value
			End Set
		End Property

		Protected Overridable Function GetObjectInfo(ByVal point As Point) As ToolTipControlInfo
			Dim hitInfo As ChartHitInfo = Chart.CalcHitInfo(point)
			If (Not hitInfo.InSeries) Then
				Return Nothing
			End If
			Return New ToolTipControlInfo(hitInfo.SeriesPoint, GetToolTipText(hitInfo.SeriesPoint), GetToolTipCaptionForSeriesPoint(hitInfo.SeriesPoint), (Not UseInitialDelay), IconType, AllowHtmlText)
		End Function

		Protected Overridable Function GetToolTipText(ByVal point As SeriesPoint) As String
			Return "Base tooltip text"
		End Function

		Protected Overridable Function GetToolTipCaptionForSeriesPoint(ByVal point As SeriesPoint) As String
			Return String.Empty
		End Function

		Private Function IToolTipControlClient_GetObjectInfo(ByVal point As Point) As ToolTipControlInfo Implements IToolTipControlClient.GetObjectInfo
			Return GetObjectInfo(point)
		End Function

		Private ReadOnly Property ShowToolTips() As Boolean Implements IToolTipControlClient.ShowToolTips
			Get
				Return True
			End Get
		End Property
	End Class

	Public Class ChartControlToolTipHelper
		Inherits ChartControlToolTipHelperBase
		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean, ByVal iconType As ToolTipIconType, ByVal allowHtmlText As DefaultBoolean)
			MyBase.New(chart, useInitialDelay, iconType, allowHtmlText)
		End Sub

		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean, ByVal iconType As ToolTipIconType)
			MyBase.New(chart, useInitialDelay, iconType)
		End Sub

		Public Sub New(ByVal chart As ChartControl, ByVal useInitialDelay As Boolean)
			MyBase.New(chart, useInitialDelay)
		End Sub

		Public Sub New(ByVal chart As ChartControl)
			MyBase.New(chart)
		End Sub

		Protected Overrides Function GetToolTipText(ByVal point As SeriesPoint) As String
			Dim rowView As DataRowView = CType(point.Tag, DataRowView)
            Return String.Format("Unit price = {0}{3}Units in stock = {1}{3}Quantity per unit = {2}", rowView("UnitPrice"), rowView("UnitsInStock"), rowView("QuantityPerUnit"), Microsoft.VisualBasic.Constants.vbCrLf)
		End Function
	End Class
End Namespace