﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportFromDatabaseDialog
    Inherits System.Windows.Forms.Form

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
Me.components = New System.ComponentModel.Container
Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
Me.OK_Button = New System.Windows.Forms.Button
Me.Cancel_Button = New System.Windows.Forms.Button
Me.DataGridView_Auswahl = New System.Windows.Forms.DataGridView
Me.BlueMOrbDataSet = New IHWB.Wave.BlueMOrbDataSet
Me.AllInfosAboutSimResultsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
Me.AllInfosAboutSimResultsTableAdapter = New IHWB.Wave.BlueMOrbDataSetTableAdapters.AllInfosAboutSimResultsTableAdapter
Me.SIMRESULTIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.SIMULATIONNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.SIMULATIONDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.MODELSOFTWAREDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.MODELDATASETDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.ELEMENTSETNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.DIMENSIONNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.DIMENSIONEINHEITDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
Me.TableLayoutPanel1.SuspendLayout()
CType(Me.DataGridView_Auswahl, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.BlueMOrbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.AllInfosAboutSimResultsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'TableLayoutPanel1
'
Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.TableLayoutPanel1.ColumnCount = 2
Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
Me.TableLayoutPanel1.Location = New System.Drawing.Point(722, 572)
Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
Me.TableLayoutPanel1.RowCount = 1
Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
Me.TableLayoutPanel1.TabIndex = 0
'
'OK_Button
'
Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
Me.OK_Button.Location = New System.Drawing.Point(3, 3)
Me.OK_Button.Name = "OK_Button"
Me.OK_Button.Size = New System.Drawing.Size(67, 23)
Me.OK_Button.TabIndex = 0
Me.OK_Button.Text = "OK"
'
'Cancel_Button
'
Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
Me.Cancel_Button.Name = "Cancel_Button"
Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
Me.Cancel_Button.TabIndex = 1
Me.Cancel_Button.Text = "Cancel"
'
'DataGridView_Auswahl
'
Me.DataGridView_Auswahl.AllowUserToAddRows = False
Me.DataGridView_Auswahl.AllowUserToDeleteRows = False
Me.DataGridView_Auswahl.AllowUserToOrderColumns = True
Me.DataGridView_Auswahl.AutoGenerateColumns = False
Me.DataGridView_Auswahl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
Me.DataGridView_Auswahl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SIMRESULTIDDataGridViewTextBoxColumn, Me.SIMULATIONNAMEDataGridViewTextBoxColumn, Me.SIMULATIONDATEDataGridViewTextBoxColumn, Me.MODELSOFTWAREDataGridViewTextBoxColumn, Me.MODELDATASETDataGridViewTextBoxColumn, Me.ELEMENTSETNAMEDataGridViewTextBoxColumn, Me.DIMENSIONNAMEDataGridViewTextBoxColumn, Me.DIMENSIONEINHEITDataGridViewTextBoxColumn})
Me.DataGridView_Auswahl.DataSource = Me.AllInfosAboutSimResultsBindingSource
Me.DataGridView_Auswahl.Location = New System.Drawing.Point(18, 25)
Me.DataGridView_Auswahl.Name = "DataGridView_Auswahl"
Me.DataGridView_Auswahl.ReadOnly = True
Me.DataGridView_Auswahl.Size = New System.Drawing.Size(846, 534)
Me.DataGridView_Auswahl.TabIndex = 1
'
'BlueMOrbDataSet
'
Me.BlueMOrbDataSet.DataSetName = "BlueMOrbDataSet"
Me.BlueMOrbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
'
'AllInfosAboutSimResultsBindingSource
'
Me.AllInfosAboutSimResultsBindingSource.DataMember = "AllInfosAboutSimResults"
Me.AllInfosAboutSimResultsBindingSource.DataSource = Me.BlueMOrbDataSet
'
'AllInfosAboutSimResultsTableAdapter
'
Me.AllInfosAboutSimResultsTableAdapter.ClearBeforeFill = True
'
'SIMRESULTIDDataGridViewTextBoxColumn
'
Me.SIMRESULTIDDataGridViewTextBoxColumn.DataPropertyName = "SIMRESULT_ID"
Me.SIMRESULTIDDataGridViewTextBoxColumn.HeaderText = "SIMRESULT_ID"
Me.SIMRESULTIDDataGridViewTextBoxColumn.Name = "SIMRESULTIDDataGridViewTextBoxColumn"
Me.SIMRESULTIDDataGridViewTextBoxColumn.ReadOnly = True
Me.SIMRESULTIDDataGridViewTextBoxColumn.Visible = False
'
'SIMULATIONNAMEDataGridViewTextBoxColumn
'
Me.SIMULATIONNAMEDataGridViewTextBoxColumn.DataPropertyName = "SIMULATION_NAME"
Me.SIMULATIONNAMEDataGridViewTextBoxColumn.HeaderText = "SIMULATION_NAME"
Me.SIMULATIONNAMEDataGridViewTextBoxColumn.Name = "SIMULATIONNAMEDataGridViewTextBoxColumn"
Me.SIMULATIONNAMEDataGridViewTextBoxColumn.ReadOnly = True
'
'SIMULATIONDATEDataGridViewTextBoxColumn
'
Me.SIMULATIONDATEDataGridViewTextBoxColumn.DataPropertyName = "SIMULATION_DATE"
Me.SIMULATIONDATEDataGridViewTextBoxColumn.HeaderText = "SIMULATION_DATE"
Me.SIMULATIONDATEDataGridViewTextBoxColumn.Name = "SIMULATIONDATEDataGridViewTextBoxColumn"
Me.SIMULATIONDATEDataGridViewTextBoxColumn.ReadOnly = True
'
'MODELSOFTWAREDataGridViewTextBoxColumn
'
Me.MODELSOFTWAREDataGridViewTextBoxColumn.DataPropertyName = "MODEL_SOFTWARE"
Me.MODELSOFTWAREDataGridViewTextBoxColumn.HeaderText = "MODEL_SOFTWARE"
Me.MODELSOFTWAREDataGridViewTextBoxColumn.Name = "MODELSOFTWAREDataGridViewTextBoxColumn"
Me.MODELSOFTWAREDataGridViewTextBoxColumn.ReadOnly = True
'
'MODELDATASETDataGridViewTextBoxColumn
'
Me.MODELDATASETDataGridViewTextBoxColumn.DataPropertyName = "MODEL_DATASET"
Me.MODELDATASETDataGridViewTextBoxColumn.HeaderText = "MODEL_DATASET"
Me.MODELDATASETDataGridViewTextBoxColumn.Name = "MODELDATASETDataGridViewTextBoxColumn"
Me.MODELDATASETDataGridViewTextBoxColumn.ReadOnly = True
'
'ELEMENTSETNAMEDataGridViewTextBoxColumn
'
Me.ELEMENTSETNAMEDataGridViewTextBoxColumn.DataPropertyName = "ELEMENTSET_NAME"
Me.ELEMENTSETNAMEDataGridViewTextBoxColumn.HeaderText = "ELEMENTSET_NAME"
Me.ELEMENTSETNAMEDataGridViewTextBoxColumn.Name = "ELEMENTSETNAMEDataGridViewTextBoxColumn"
Me.ELEMENTSETNAMEDataGridViewTextBoxColumn.ReadOnly = True
'
'DIMENSIONNAMEDataGridViewTextBoxColumn
'
Me.DIMENSIONNAMEDataGridViewTextBoxColumn.DataPropertyName = "DIMENSION_NAME"
Me.DIMENSIONNAMEDataGridViewTextBoxColumn.HeaderText = "DIMENSION_NAME"
Me.DIMENSIONNAMEDataGridViewTextBoxColumn.Name = "DIMENSIONNAMEDataGridViewTextBoxColumn"
Me.DIMENSIONNAMEDataGridViewTextBoxColumn.ReadOnly = True
'
'DIMENSIONEINHEITDataGridViewTextBoxColumn
'
Me.DIMENSIONEINHEITDataGridViewTextBoxColumn.DataPropertyName = "DIMENSION_EINHEIT"
Me.DIMENSIONEINHEITDataGridViewTextBoxColumn.HeaderText = "DIMENSION_EINHEIT"
Me.DIMENSIONEINHEITDataGridViewTextBoxColumn.Name = "DIMENSIONEINHEITDataGridViewTextBoxColumn"
Me.DIMENSIONEINHEITDataGridViewTextBoxColumn.ReadOnly = True
'
'ImportFromDatabaseDialog
'
Me.AcceptButton = Me.OK_Button
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.CancelButton = Me.Cancel_Button
Me.ClientSize = New System.Drawing.Size(880, 613)
Me.Controls.Add(Me.DataGridView_Auswahl)
Me.Controls.Add(Me.TableLayoutPanel1)
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "ImportFromDatabaseDialog"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "ImportFromDatabaseDialog"
Me.TableLayoutPanel1.ResumeLayout(False)
CType(Me.DataGridView_Auswahl, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.BlueMOrbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.AllInfosAboutSimResultsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView_Auswahl As System.Windows.Forms.DataGridView
    Friend WithEvents BlueMOrbDataSet As IHWB.Wave.BlueMOrbDataSet
    Friend WithEvents AllInfosAboutSimResultsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AllInfosAboutSimResultsTableAdapter As IHWB.Wave.BlueMOrbDataSetTableAdapters.AllInfosAboutSimResultsTableAdapter
    Friend WithEvents SIMRESULTIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SIMULATIONNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SIMULATIONDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MODELSOFTWAREDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MODELDATASETDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ELEMENTSETNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIMENSIONNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIMENSIONEINHEITDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
