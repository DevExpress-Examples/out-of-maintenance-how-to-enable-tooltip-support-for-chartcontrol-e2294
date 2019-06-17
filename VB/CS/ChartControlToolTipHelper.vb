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

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable iconType was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable allowHtmlText was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean, ByVal iconType_Renamed As ToolTipIconType, ByVal allowHtmlText_Renamed As DefaultBoolean)
			If Nothing Is chart_Renamed Then
				Throw New ArgumentNullException("chart")
			End If
			fChart = chart_Renamed
			fUseInitialDelay = useInitialDelay_Renamed
			fIconType = iconType_Renamed
			fAllowHtmlText = allowHtmlText_Renamed
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable iconType was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean, ByVal iconType_Renamed As ToolTipIconType)
			Me.New(chart_Renamed, useInitialDelay_Renamed, iconType_Renamed, DefaultBoolean.Default)
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean)
			Me.New(chart_Renamed, useInitialDelay_Renamed, ToolTipIconType.None)
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl)
			Me.New(chart_Renamed, True)
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
			If Not hitInfo.InSeries Then
				Return Nothing
			End If
			Return New ToolTipControlInfo(hitInfo.SeriesPoint, GetToolTipText(hitInfo.SeriesPoint), GetToolTipCaptionForSeriesPoint(hitInfo.SeriesPoint), Not UseInitialDelay, IconType, AllowHtmlText)
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

		Private ReadOnly Property IToolTipControlClient_ShowToolTips() As Boolean Implements IToolTipControlClient.ShowToolTips
			Get
				Return True
			End Get
		End Property
	End Class

	Public Class ChartControlToolTipHelper
		Inherits ChartControlToolTipHelperBase

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable iconType was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable allowHtmlText was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean, ByVal iconType_Renamed As ToolTipIconType, ByVal allowHtmlText_Renamed As DefaultBoolean)
			MyBase.New(chart_Renamed, useInitialDelay_Renamed, iconType_Renamed, allowHtmlText_Renamed)
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable iconType was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean, ByVal iconType_Renamed As ToolTipIconType)
			MyBase.New(chart_Renamed, useInitialDelay_Renamed, iconType_Renamed)
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable useInitialDelay was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl, ByVal useInitialDelay_Renamed As Boolean)
			MyBase.New(chart_Renamed, useInitialDelay_Renamed)
		End Sub

'INSTANT VB NOTE: The variable chart was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal chart_Renamed As ChartControl)
			MyBase.New(chart_Renamed)
		End Sub

		Protected Overrides Function GetToolTipText(ByVal point As SeriesPoint) As String
			Dim rowView As DataRowView = CType(point.Tag, DataRowView)
			Return String.Format("Unit price = {0}" & vbCrLf & "Units in stock = {1}" & vbCrLf & "Quantity per unit = {2}", rowView("UnitPrice"), rowView("UnitsInStock"), rowView("QuantityPerUnit"))
		End Function
	End Class
End Namespace