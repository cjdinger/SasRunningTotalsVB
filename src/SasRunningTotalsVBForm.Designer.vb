<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SasRunningTotalsVBForm
    Inherits SAS.Tasks.Toolkit.Controls.TaskForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim FilterSettings1 As SAS.Tasks.Toolkit.Controls.FilterSettings = New SAS.Tasks.Toolkit.Controls.FilterSettings()
        Me.lblOutCol = New System.Windows.Forms.Label()
        Me.txtTotalsCol = New System.Windows.Forms.TextBox()
        Me.lblOutData = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.btnBrowseOut = New System.Windows.Forms.Button()
        Me.varSelCtl = New SAS.EG.Controls.SASVariableSelector()
        Me.selDataCtl = New SAS.Tasks.Toolkit.Controls.TaskSelectedDataControl()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblOutCol
        '
        Me.lblOutCol.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOutCol.AutoSize = True
        Me.lblOutCol.Location = New System.Drawing.Point(12, 275)
        Me.lblOutCol.Name = "lblOutCol"
        Me.lblOutCol.Size = New System.Drawing.Size(69, 13)
        Me.lblOutCol.TabIndex = 11
        Me.lblOutCol.Text = "New column:"
        '
        'txtTotalsCol
        '
        Me.txtTotalsCol.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalsCol.Location = New System.Drawing.Point(93, 271)
        Me.txtTotalsCol.Name = "txtTotalsCol"
        Me.txtTotalsCol.Size = New System.Drawing.Size(239, 20)
        Me.txtTotalsCol.TabIndex = 12
        '
        'lblOutData
        '
        Me.lblOutData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOutData.AutoSize = True
        Me.lblOutData.Location = New System.Drawing.Point(12, 301)
        Me.lblOutData.Name = "lblOutData"
        Me.lblOutData.Size = New System.Drawing.Size(66, 13)
        Me.lblOutData.TabIndex = 13
        Me.lblOutData.Text = "Output data:"
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.Location = New System.Drawing.Point(93, 297)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.Size = New System.Drawing.Size(239, 20)
        Me.txtOutput.TabIndex = 14
        '
        'btnBrowseOut
        '
        Me.btnBrowseOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseOut.Location = New System.Drawing.Point(338, 297)
        Me.btnBrowseOut.Name = "btnBrowseOut"
        Me.btnBrowseOut.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseOut.TabIndex = 15
        Me.btnBrowseOut.Text = "Browse..."
        Me.btnBrowseOut.UseVisualStyleBackColor = True
        '
        'varSelCtl
        '
        Me.varSelCtl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.varSelCtl.CacheDirtyFlag = False
        Me.varSelCtl.CachePath = Nothing
        Me.varSelCtl.ColumnRolesTitle = ""
        Me.varSelCtl.ColumnsToAssignTitle = ""
        Me.varSelCtl.Location = New System.Drawing.Point(11, 64)
        Me.varSelCtl.Name = "varSelCtl"
        Me.varSelCtl.Size = New System.Drawing.Size(402, 195)
        Me.varSelCtl.TabIndex = 10
        '
        'selDataCtl
        '
        Me.selDataCtl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.selDataCtl.Consumer = Nothing
        Me.selDataCtl.FilterSettings = FilterSettings1
        Me.selDataCtl.HideEditButton = False
        Me.selDataCtl.Location = New System.Drawing.Point(11, 11)
        Me.selDataCtl.Name = "selDataCtl"
        Me.selDataCtl.Size = New System.Drawing.Size(402, 46)
        Me.selDataCtl.TabIndex = 9
        Me.selDataCtl.TaskData = Nothing
        Me.selDataCtl.UseLabelsForVarNames = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(346, 334)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 23)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(275, 334)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 16
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'SasRunningTotalsVBForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 368)
        Me.Controls.Add(Me.lblOutCol)
        Me.Controls.Add(Me.txtTotalsCol)
        Me.Controls.Add(Me.lblOutData)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.btnBrowseOut)
        Me.Controls.Add(Me.varSelCtl)
        Me.Controls.Add(Me.selDataCtl)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "SasRunningTotalsVBForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SasRunningTotalsVBForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lblOutCol As System.Windows.Forms.Label
    Private WithEvents txtTotalsCol As System.Windows.Forms.TextBox
    Private WithEvents lblOutData As System.Windows.Forms.Label
    Private WithEvents txtOutput As System.Windows.Forms.TextBox
    Private WithEvents btnBrowseOut As System.Windows.Forms.Button
    Private WithEvents varSelCtl As SAS.EG.Controls.SASVariableSelector
    Private WithEvents selDataCtl As SAS.Tasks.Toolkit.Controls.TaskSelectedDataControl
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnOK As System.Windows.Forms.Button
End Class
