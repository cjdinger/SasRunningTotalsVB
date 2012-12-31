Imports SAS.EG.Controls.SASVariableSelector
Imports SAS.Tasks.Toolkit.Data
Imports SAS.Shared.AddIns
Imports SAS.Tasks.Toolkit
Imports System.IO

Public Class Helpers

    ''' <summary>
    ''' In the special case where we have a local SAS data set file (sas7bdat),
    ''' and a local SAS server, we have to make sure that there is a library assigned
    ''' </summary>
    ''' <param name="consumer"></param>
    ''' <remarks></remarks>
    Friend Shared Sub AssignLocalLibraryIfNeeded(ByVal consumer As ISASTaskConsumer3)
        Dim data As New SasData(TryCast(consumer.ActiveData, ISASTaskData2))
        Dim server As New SasServer(data.Server)
        If (server.IsLocal _
            AndAlso ((Not String.IsNullOrEmpty(consumer.ActiveData.File) _
            AndAlso (consumer.ActiveData.Source = SourceType.SasDataset)) _
            AndAlso consumer.ActiveData.File.Contains("\"))) Then
            Dim str2 As String
            str2 = ""
            Dim directoryName As String = Path.GetDirectoryName(consumer.ActiveData.File)
            Dim submitter As New SasSubmitter(data.Server)
            submitter.SubmitSasProgramAndWait(String.Format("libname {0} ""{1}"";" & ChrW(13) & ChrW(10), data.Libref, directoryName), (str2))
        End If
    End Sub

    ''' <summary>
    ''' Build a List of AddVariableParams that can be added
    ''' to the SAS Variable Selector control
    ''' </summary>
    ''' <param name="data">A SasData object for the input data used
    ''' in the variable selector</param>
    ''' <returns>A list of AddVariableParams objects that
    ''' can be added to the control</returns>
    Public Shared Function BuildVariableParamsList(ByVal data As SasData) As List(Of AddVariableParams)
        Dim list As New List(Of AddVariableParams)
        Dim column As SasColumn
        For Each column In data.GetColumns
            Dim item As New AddVariableParams
            item.Name = column.Name
            item.Label = column.Label
            item.Format = column.Format
            item.Informat = column.Informat

            ' map the column category
            ' to the variable selector
            ' version of this enumeration
            ' Ensures the correct "type" icon is
            ' shown in the variable selector
            Select Case column.Category
                Case SasColumn.eCategory.Character
                    item.Type = SAS.EG.Controls.VARTYPE.Character
                    Exit Select
                Case SasColumn.eCategory.Numeric
                    item.Type = SAS.EG.Controls.VARTYPE.Numeric
                    Exit Select
                Case SasColumn.eCategory.Currency
                    item.Type = SAS.EG.Controls.VARTYPE.Currency
                    Exit Select
                Case SasColumn.eCategory.Date
                    item.Type = SAS.EG.Controls.VARTYPE.Date
                    Exit Select
                Case SasColumn.eCategory.DateTime
                    item.Type = SAS.EG.Controls.VARTYPE.Time
                    Exit Select
                Case SasColumn.eCategory.Georef
                    item.Type = SAS.EG.Controls.VARTYPE.GeoRef
                    Exit Select
                Case Else
                    item.Type = SAS.EG.Controls.VARTYPE.Numeric
                    Exit Select
            End Select
            list.Add(item)
        Next
        Return list
    End Function


End Class
