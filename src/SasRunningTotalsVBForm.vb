Imports SAS.Tasks.Toolkit
Imports SAS.Tasks.Toolkit.Helpers
Imports SAS.EG.Controls
Imports System.IO
Imports SAS.Tasks.Toolkit.Data
Imports SAS.Shared.AddIns
Imports System.ComponentModel

Public Class SasRunningTotalsVBForm : Inherits SAS.Tasks.Toolkit.Controls.TaskForm

    Private _settings As SasRunningTotalsVBSettings
    Private varSelectorValid As Boolean = False


    Sub New(ByVal iSASTaskConsumer3 As SAS.Shared.AddIns.ISASTaskConsumer3)
        Consumer = iSASTaskConsumer3
        InitializeComponent()

        ' add event handlers for the data filter. input data, and output data controls
        AddHandler Me.selDataCtl.DataSelectionChanged, New EventHandler(AddressOf Me.OnDataSelectionChanged)
        AddHandler Me.txtOutput.TextChanged, New EventHandler(AddressOf Me.OnOutputDataName_Changed)
        AddHandler Me.txtTotalsCol.TextChanged, New EventHandler(AddressOf Me.OnTotalsVariable_Changed)

    End Sub

    Public Property Settings() As SasRunningTotalsVBSettings
        Get
            Return _settings
        End Get
        Set(ByVal value As SasRunningTotalsVBSettings)
            _settings = value
        End Set
    End Property

    ' validate the variable name for running total result
    Private Function isValidVarName(ByVal name As String) As Boolean
        Dim regex As New System.Text.RegularExpressions.Regex("^(?=.{1,32}$)([a-zA-Z_][a-zA-Z0-9_]*)$", _
                         System.Text.RegularExpressions.RegexOptions.Compiled)
        Return regex.IsMatch(Me.txtTotalsCol.Text)
    End Function

    ' validate the output data set name
    Private Function isValidOutput(ByVal output As String) As Boolean
        Dim strArray As String() = output.Trim.Split(New Char() {"."c})
        If (((output.Trim.Contains(" ") OrElse (strArray.Length <> 2)) OrElse (strArray(0).IndexOfAny(Path.GetInvalidFileNameChars) > -1)) OrElse (strArray(1).IndexOfAny(Path.GetInvalidFileNameChars) > -1)) Then
            Return False
        End If
        Return True
    End Function

    ' change whether the OK button is enabled, based on whether
    ' all settings are valid
    Private Sub UpdateStatus()
        Dim flag As Boolean = (((Me.varSelCtl.GetNumberOfAssignedVariables(My.Resources.Translate.MeasureValueRole) > 0) AndAlso Me.isValidVarName(Me.txtTotalsCol.Text)) AndAlso Me.isValidOutput(Me.txtOutput.Text))
        Me.btnOK.Enabled = flag
    End Sub

    ' if variable is assigned, take these actions
    Private Sub varSelCtl_VariableAssigned(ByVal sender As Object, ByVal ea As VariableAssignedEventArgs)
        Me.varSelectorValid = ea.MeetsRequirements
        If (ea.RoleName = My.Resources.Translate.MeasureValueRole) Then
            Me.txtTotalsCol.Text = String.Format("totals_{0}", UtilityFunctions.GetValidSasName(ea.VarName, &H18))
        End If
        Me.UpdateStatus()
    End Sub

    ' when a variable is unassigned, take these actions
    Private Sub varSelCtl_VariableDeassigned(ByVal sender As Object, ByVal ea As VariableDeassignedEventArgs)
        Me.varSelectorValid = ea.MeetsRequirements
        Me.UpdateStatus()
    End Sub

    ' if variable name for output column is changed, validate
    Private Sub OnTotalsVariable_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Me.UpdateStatus()
    End Sub

    ' initialize the variable selector with the input data
    Private Sub SetupDataForVarSelector()
        If (MyBase.Consumer.InputData.Length > 0) Then
            Dim data As New SasData(TryCast(MyBase.Consumer.InputData(0), ISASTaskData2))
            Helpers.AssignLocalLibraryIfNeeded(MyBase.Consumer)
            Dim paramList As List(Of SASVariableSelector.AddVariableParams) = Helpers.BuildVariableParamsList(data)
            Me.varSelCtl.AddVariables(paramList)
            If (Not String.IsNullOrEmpty(Me.Settings.VariableMeasure) AndAlso data.ContainsColumn(Me.Settings.VariableMeasure)) Then
                Me.varSelCtl.AssignVariable(My.Resources.Translate.MeasureValueRole, Me.Settings.VariableMeasure)
            End If
            Dim str As String
            For Each str In Me.Settings.VariableGroups
                If data.ContainsColumn(str) Then
                    Me.varSelCtl.AssignVariable(My.Resources.Translate.GroupingRole, str)
                End If
            Next
            AddHandler Me.varSelCtl.VariableAssigned, New SASVariableSelector.VariableAssignedEventHandler(AddressOf Me.varSelCtl_VariableAssigned)
            AddHandler Me.varSelCtl.VariableDeassigned, New SASVariableSelector.VariableDeassignedEventHandler(AddressOf Me.varSelCtl_VariableDeassigned)
        End If
    End Sub

    ' if input data selection is changed, update settings
    Private Sub OnDataSelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim filterSettings As SAS.Tasks.Toolkit.Controls.FilterSettings = Me.selDataCtl.FilterSettings
        Me.Settings.FilterSettings = filterSettings
    End Sub

    ' if output data name is changed, validate
    Private Sub OnOutputDataName_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Me.UpdateStatus()
    End Sub

    ' Initialize the variable roles
    Private Sub SetupVariableRoles()
        Dim arp As New SASVariableSelector.AddRoleParams
        arp.Name = My.Resources.Translate.MeasureValueRole
        arp.MinNumVars = 1
        arp.MaxNumVars = 1
        arp.AcceptsMacroVars = False
        arp.Type = SASVariableSelector.ROLETYPE.Numeric
        Me.varSelCtl.AddRole(arp)
        Dim params2 As New SASVariableSelector.AddRoleParams
        params2.Name = My.Resources.Translate.GroupingRole
        params2.MinNumVars = 0
        params2.AcceptsMacroVars = False
        params2.Type = SASVariableSelector.ROLETYPE.All
        Me.varSelCtl.AddRole(params2)
    End Sub

    ' When form is loaded, initialize all of the controls
    ' with active settings and data
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        Me.selDataCtl.Consumer = MyBase.Consumer
        Me.selDataCtl.TaskData = TryCast(MyBase.Consumer.InputData(0), ISASTaskData2)
        Me.selDataCtl.FilterSettings = Me.Settings.FilterSettings
        Me.varSelCtl.ColumnsToAssignTitle = My.Resources.Translate.ColumnsTitle
        Me.varSelCtl.ColumnRolesTitle = My.Resources.Translate.RolesTitleBar
        Me.SetupVariableRoles()
        Me.SetupDataForVarSelector()
        Me.txtOutput.Text = Me.Settings.DataOut
        Me.txtTotalsCol.Text = Me.Settings.VariableTotal
        Me.UpdateStatus()
    End Sub

    ' when form is closing with OK status, save all of the settings
    Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
        If (MyBase.DialogResult = Windows.Forms.DialogResult.OK) Then
            Me.Settings.DataOut = Me.txtOutput.Text
            Me.Settings.VariableMeasure = Me.varSelCtl.GetAssignedVariable(My.Resources.Translate.MeasureValueRole, 0)
            Me.Settings.VariableGroups.Clear()
            Dim numberOfAssignedVariables As Integer = Me.varSelCtl.GetNumberOfAssignedVariables(My.Resources.Translate.GroupingRole)
            If (numberOfAssignedVariables > 0) Then
                Dim i As Integer
                For i = 0 To numberOfAssignedVariables - 1
                    Me.Settings.VariableGroups.Add(Me.varSelCtl.GetAssignedVariable(My.Resources.Translate.GroupingRole, i))
                Next i
            End If
            Me.Settings.VariableTotal = Me.txtTotalsCol.Text
        End If
        MyBase.OnClosing(e)
    End Sub

End Class