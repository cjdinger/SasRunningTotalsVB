Imports System.Xml
Imports SAS.Tasks.Toolkit.Helpers
Imports System.Text
Imports SAS.Shared.AddIns

Public Class SasRunningTotalsVBSettings

    Public DataOut As String
    Public FilterSettings As SAS.Tasks.Toolkit.Controls.FilterSettings
    Public VariableGroups As List(Of String)
    Public VariableMeasure As String
    Public VariableTotal As String
    Private version As Integer = 1


    Public Sub New()
        Me.VariableGroups = New List(Of String)
        Me.FilterSettings = New SAS.Tasks.Toolkit.Controls.FilterSettings
        Me.DataOut = "WORK.OUT_TOTALS"
        Me.VariableMeasure = ""
        Me.VariableTotal = ""
    End Sub

    ' initialize task from saved XML
    Public Sub FromXml(ByVal xml As String)
        Try
            Dim document As XDocument = XDocument.Parse(xml)
            Dim element As XElement = document.Element("RunningTotalsTask").Element("FilterSettings")
            Me.FilterSettings = New SAS.Tasks.Toolkit.Controls.FilterSettings(element.Value)
            Dim element2 As XElement = document.Element("RunningTotalsTask").Element("DataOut")
            Me.DataOut = element2.Value
            Dim element3 As XElement = document.Element("RunningTotalsTask").Element("MeasureVar")
            Me.VariableMeasure = element3.Value
            Dim element4 As XElement = document.Element("RunningTotalsTask").Element("TotalsVar")
            Me.VariableTotal = element4.Value
            Dim enumerable As IEnumerable(Of XElement) = document.Element("RunningTotalsTask").Element("GroupVariables").Elements("GroupVariable")
            Dim element6 As XElement
            For Each element6 In enumerable
                Me.VariableGroups.Add(element6.Value)
            Next
        Catch exception1 As XmlException
        End Try
    End Sub

    ' Save XML state of the task
    Public Function ToXml() As String
        Dim element As New XElement("GroupVariables")
        Dim str As String
        For Each str In Me.VariableGroups
            element.Add(New XElement("GroupVariable", str))
        Next
        Dim document As New XDocument( _
            New XDeclaration("1.0", "utf-8", String.Empty), _
            New Object() { _
                New XElement("RunningTotalsTask", _
                    New Object() { _
                        New XElement("Version", Me.version), _
                        New XElement("FilterSettings", Me.FilterSettings.ToXml), _
                        New XElement("DataOut", Me.DataOut), _
                        New XElement("MeasureVar", Me.VariableMeasure), _
                        New XElement("TotalsVar", Me.VariableTotal), element} _
                         ) _
                    } _
            )
        Return document.ToString
    End Function

    Public Function GetSasProgram(ByVal consumer As ISASTaskConsumer3) As String
        Dim builder As New StringBuilder
        Dim str As String = ""
        If Not String.IsNullOrEmpty(Me.FilterSettings.FilterValue) Then
            str = String.Format("(where=({0}))", Me.FilterSettings.FilterValue)
        End If
        ' use a utility function to generate a SAS code header with comments
        builder.Append(UtilityFunctions.BuildSasTaskCodeHeader("Calculate Running Totals (VB)", String.Format("{0}.{1}", consumer.ActiveData.Library, consumer.ActiveData.Member), consumer.AssignedServer))
        If (Me.VariableGroups.Count = 0) Then
            builder.AppendFormat("data {0};" & ChrW(10), Me.DataOut)
            builder.AppendFormat("  set {0}.{1}{2};" & ChrW(10), consumer.ActiveData.Library, consumer.ActiveData.Member, str)

            ' using a utility function to "wrap" variable names in SAS literal syntax
            ' if necessary
            builder.AppendFormat("  {0} + {1}; " & ChrW(10), UtilityFunctions.SASValidLiteral(Me.VariableTotal), UtilityFunctions.SASValidLiteral(Me.VariableMeasure))
            builder.AppendLine("run;")
        Else
            builder.AppendFormat("data {0};" & ChrW(10), Me.DataOut)
            builder.AppendFormat("  set {0}.{1}{2};" & ChrW(10), consumer.ActiveData.Library, consumer.ActiveData.Member, str)
            builder.AppendLine("  by ")
            Dim builder2 As New StringBuilder
            Dim i As Integer
            For i = 0 To Me.VariableGroups.Count - 1
                builder.AppendLine(String.Format("   {0}", UtilityFunctions.SASValidLiteral(Me.VariableGroups.Item(i))))
                If (i = 0) Then
                    builder2.AppendFormat("FIRST.{0} ", UtilityFunctions.SASValidLiteral(Me.VariableGroups.Item(i)))
                Else
                    builder2.AppendFormat("or FIRST.{0} ", UtilityFunctions.SASValidLiteral(Me.VariableGroups.Item(i)))
                End If
            Next i
            builder.AppendLine("  ;")
            builder.AppendFormat("  if {0} then " & ChrW(10) & "   {1}={2};" & ChrW(10), builder2, UtilityFunctions.SASValidLiteral(Me.VariableTotal), UtilityFunctions.SASValidLiteral(Me.VariableMeasure))
            builder.AppendFormat("  else {0} + {1}; " & ChrW(10), UtilityFunctions.SASValidLiteral(Me.VariableTotal), UtilityFunctions.SASValidLiteral(Me.VariableMeasure))
            builder.AppendLine("run;")
        End If
        Return builder.ToString
    End Function

End Class
