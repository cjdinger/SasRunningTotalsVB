Imports SAS.Shared.AddIns
Imports SAS.Tasks.Toolkit
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Reflection

<ClassId("2c9e827d-9ba2-4b62-be41-5028b15c28de")> _
<IconLocation("RunningTotalsVB.task.ico")> _
<InputRequired(InputResourceType.Data)> _
Public Class SasRunningTotalsVB : Inherits SAS.Tasks.Toolkit.SasTask

#Region "Initialization"

    Sub New()
        InitializeComponent()
    End Sub

    Sub InitializeComponent()
        '
        'SasRunningTotalsVB
        '
        Me.ProcsUsed = "DATA step"
        Me.ProductsRequired = "BASE"
        Me.TaskCategory = "SAS Press Examples"
        Me.TaskDescription = "Calculate running totals for a column of data"
        Me.TaskName = "Calculate Running Totals (VB)"

    End Sub


#End Region

#Region "Internal fields"
    Private settings As New SasRunningTotalsVBSettings
#End Region

#Region "Overrides"

    ' This function is called when it's time to generate a SAS program
    ' from the task settings so far.
    Public Overrides Function GetSasCode() As String
        Return Me.settings.GetSasProgram(MyBase.Consumer)
    End Function

    Public Overrides Function GetXmlState() As String
        Return Me.settings.ToXml
    End Function

    Public Overrides Function Initialize() As Boolean
        Me.settings = New SasRunningTotalsVBSettings
        Return True
    End Function

    ' expect one output data set
    Public Overrides ReadOnly Property OutputDataCount As Integer
        Get
            Return 1
        End Get
    End Property

    ' name and label for the output data set
    Public Overrides ReadOnly Property OutputDataDescriptorList As List(Of ISASTaskDataDescriptor)
        Get
            Dim list As New List(Of ISASTaskDataDescriptor)
            Dim strArray As String() = Me.settings.DataOut.Split(New Char() {"."c})
            If (strArray.Length = 2) Then
                list.Add(SASTaskDataDescriptor.CreateLibrefDataDescriptor(MyBase.Consumer.AssignedServer, strArray(0), strArray(1), ""))
            End If
            Return list
        End Get
    End Property


    ' This function is called when it's time to show the task window
    Public Overrides Function Show(ByVal Owner As IWin32Window) As ShowResult
        Dim form As New SasRunningTotalsVBForm(MyBase.Consumer)
        form.Icon = New Icon(Assembly.GetExecutingAssembly.GetManifestResourceStream(MyBase.IconName))
        form.Text = Me.Label
        form.Settings = Me.settings
        If (DialogResult.OK = form.ShowDialog(Owner)) Then
            Me.settings = form.Settings
            Return ShowResult.RunNow
        End If
        Return ShowResult.Canceled
    End Function


#Region "Serialization - saving/restored task state"
    Public Overrides Sub RestoreStateFromXml(ByVal xmlState As String)
        Me.settings = New SasRunningTotalsVBSettings
        Me.settings.FromXml(xmlState)
    End Sub
#End Region

#End Region

End Class
