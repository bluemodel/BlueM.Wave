<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportDiag
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim SplitContainer1 As System.Windows.Forms.SplitContainer
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportDiag))
        Me.GroupBox_Selection = New System.Windows.Forms.GroupBox
        Me.Label_Search = New System.Windows.Forms.Label
        Me.TextBox_Search = New System.Windows.Forms.TextBox
        Me.Label_Series = New System.Windows.Forms.Label
        Me.ListBox_Series = New System.Windows.Forms.ListBox
        Me.Button_SelectAll = New System.Windows.Forms.Button
        Me.GroupBox_Preview = New System.Windows.Forms.GroupBox
        Me.Label_File = New System.Windows.Forms.Label
        Me.TextBox_Preview = New System.Windows.Forms.RichTextBox
        Me.ComboBox_Separator = New System.Windows.Forms.ComboBox
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Label_ColumnDateTime = New System.Windows.Forms.Label
        Me.RadioButton_FixedWidth = New System.Windows.Forms.RadioButton
        Me.RadioButton_CharSeparated = New System.Windows.Forms.RadioButton
        Me.GroupBox_Columns = New System.Windows.Forms.GroupBox
        Me.TextBox_ColumnWidth = New System.Windows.Forms.TextBox
        Me.NumericUpDown_LineTitles = New System.Windows.Forms.NumericUpDown
        Me.Label_LineTitles = New System.Windows.Forms.Label
        Me.ComboBox_DecimalMark = New System.Windows.Forms.ComboBox
        Me.GroupBox_Settings = New System.Windows.Forms.GroupBox
        Me.CheckBox_Units = New System.Windows.Forms.CheckBox
        Me.NumericUpDown_LineUnits = New System.Windows.Forms.NumericUpDown
        Me.Label_LineData = New System.Windows.Forms.Label
        Me.NumericUpDown_LineData = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDown_ColumnDateTime = New System.Windows.Forms.NumericUpDown
        Me.GroupBox_DecimalMark = New System.Windows.Forms.GroupBox
        Me.Label_DecimalMark = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusImage = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox_Dateformat = New System.Windows.Forms.GroupBox
        Me.PictureBox_DateFormatHelp = New System.Windows.Forms.PictureBox
        Me.Label_Dateformat = New System.Windows.Forms.Label
        Me.ComboBox_Dateformat = New System.Windows.Forms.ComboBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        SplitContainer1 = New System.Windows.Forms.SplitContainer
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        Me.GroupBox_Selection.SuspendLayout()
        Me.GroupBox_Preview.SuspendLayout()
        Me.GroupBox_Columns.SuspendLayout()
        CType(Me.NumericUpDown_LineTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Settings.SuspendLayout()
        CType(Me.NumericUpDown_LineUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_LineData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_ColumnDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_DecimalMark.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox_Dateformat.SuspendLayout()
        CType(Me.PictureBox_DateFormatHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SplitContainer1.Location = New System.Drawing.Point(12, 188)
        SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        SplitContainer1.Panel1.Controls.Add(Me.GroupBox_Selection)
        '
        'SplitContainer1.Panel2
        '
        SplitContainer1.Panel2.Controls.Add(Me.GroupBox_Preview)
        SplitContainer1.Size = New System.Drawing.Size(530, 220)
        SplitContainer1.SplitterDistance = 176
        SplitContainer1.TabIndex = 33
        '
        'GroupBox_Selection
        '
        Me.GroupBox_Selection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Selection.Controls.Add(Me.Label_Search)
        Me.GroupBox_Selection.Controls.Add(Me.TextBox_Search)
        Me.GroupBox_Selection.Controls.Add(Me.Label_Series)
        Me.GroupBox_Selection.Controls.Add(Me.ListBox_Series)
        Me.GroupBox_Selection.Controls.Add(Me.Button_SelectAll)
        Me.GroupBox_Selection.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_Selection.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox_Selection.Name = "GroupBox_Selection"
        Me.GroupBox_Selection.Size = New System.Drawing.Size(176, 220)
        Me.GroupBox_Selection.TabIndex = 0
        Me.GroupBox_Selection.TabStop = False
        Me.GroupBox_Selection.Text = "Series selection"
        '
        'Label_Search
        '
        Me.Label_Search.AutoSize = True
        Me.Label_Search.Location = New System.Drawing.Point(12, 21)
        Me.Label_Search.Name = "Label_Search"
        Me.Label_Search.Size = New System.Drawing.Size(44, 13)
        Me.Label_Search.TabIndex = 0
        Me.Label_Search.Text = "Search:"
        '
        'TextBox_Search
        '
        Me.TextBox_Search.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Search.Location = New System.Drawing.Point(59, 18)
        Me.TextBox_Search.Name = "TextBox_Search"
        Me.TextBox_Search.Size = New System.Drawing.Size(111, 20)
        Me.TextBox_Search.TabIndex = 1
        '
        'Label_Series
        '
        Me.Label_Series.AutoSize = True
        Me.Label_Series.Location = New System.Drawing.Point(12, 46)
        Me.Label_Series.Name = "Label_Series"
        Me.Label_Series.Size = New System.Drawing.Size(83, 13)
        Me.Label_Series.TabIndex = 2
        Me.Label_Series.Text = "Available series:"
        '
        'ListBox_Series
        '
        Me.ListBox_Series.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Series.FormattingEnabled = True
        Me.ListBox_Series.Location = New System.Drawing.Point(15, 64)
        Me.ListBox_Series.Name = "ListBox_Series"
        Me.ListBox_Series.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Series.Size = New System.Drawing.Size(155, 121)
        Me.ListBox_Series.TabIndex = 3
        '
        'Button_SelectAll
        '
        Me.Button_SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAll.Location = New System.Drawing.Point(15, 191)
        Me.Button_SelectAll.Name = "Button_SelectAll"
        Me.Button_SelectAll.Size = New System.Drawing.Size(88, 23)
        Me.Button_SelectAll.TabIndex = 4
        Me.Button_SelectAll.Text = "Select all"
        Me.Button_SelectAll.UseVisualStyleBackColor = True
        '
        'GroupBox_Preview
        '
        Me.GroupBox_Preview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Preview.Controls.Add(Me.Label_File)
        Me.GroupBox_Preview.Controls.Add(Me.TextBox_Preview)
        Me.GroupBox_Preview.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_Preview.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox_Preview.Name = "GroupBox_Preview"
        Me.GroupBox_Preview.Size = New System.Drawing.Size(350, 220)
        Me.GroupBox_Preview.TabIndex = 0
        Me.GroupBox_Preview.TabStop = False
        Me.GroupBox_Preview.Text = "File preview"
        '
        'Label_File
        '
        Me.Label_File.AutoSize = True
        Me.Label_File.Location = New System.Drawing.Point(9, 21)
        Me.Label_File.Name = "Label_File"
        Me.Label_File.Size = New System.Drawing.Size(26, 13)
        Me.Label_File.TabIndex = 0
        Me.Label_File.Text = "File:"
        '
        'TextBox_Preview
        '
        Me.TextBox_Preview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Preview.BackColor = System.Drawing.Color.White
        Me.TextBox_Preview.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Preview.Location = New System.Drawing.Point(12, 43)
        Me.TextBox_Preview.Name = "TextBox_Preview"
        Me.TextBox_Preview.ReadOnly = True
        Me.TextBox_Preview.Size = New System.Drawing.Size(332, 166)
        Me.TextBox_Preview.TabIndex = 1
        Me.TextBox_Preview.Text = ""
        Me.TextBox_Preview.WordWrap = False
        '
        'ComboBox_Separator
        '
        Me.ComboBox_Separator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Separator.DropDownWidth = 100
        Me.ComboBox_Separator.FormattingEnabled = True
        Me.ComboBox_Separator.Location = New System.Drawing.Point(131, 18)
        Me.ComboBox_Separator.Name = "ComboBox_Separator"
        Me.ComboBox_Separator.Size = New System.Drawing.Size(90, 21)
        Me.ComboBox_Separator.TabIndex = 1
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(386, 414)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 4
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(467, 414)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 5
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label_ColumnDateTime
        '
        Me.Label_ColumnDateTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_ColumnDateTime.AutoSize = True
        Me.Label_ColumnDateTime.Location = New System.Drawing.Point(409, 24)
        Me.Label_ColumnDateTime.Name = "Label_ColumnDateTime"
        Me.Label_ColumnDateTime.Size = New System.Drawing.Size(115, 13)
        Me.Label_ColumnDateTime.TabIndex = 6
        Me.Label_ColumnDateTime.Text = "Column with date/time:"
        '
        'RadioButton_FixedWidth
        '
        Me.RadioButton_FixedWidth.AutoSize = True
        Me.RadioButton_FixedWidth.Location = New System.Drawing.Point(15, 53)
        Me.RadioButton_FixedWidth.Name = "RadioButton_FixedWidth"
        Me.RadioButton_FixedWidth.Size = New System.Drawing.Size(78, 17)
        Me.RadioButton_FixedWidth.TabIndex = 2
        Me.RadioButton_FixedWidth.TabStop = True
        Me.RadioButton_FixedWidth.Text = "Fixed width"
        Me.RadioButton_FixedWidth.UseVisualStyleBackColor = True
        '
        'RadioButton_CharSeparated
        '
        Me.RadioButton_CharSeparated.AutoSize = True
        Me.RadioButton_CharSeparated.Location = New System.Drawing.Point(15, 19)
        Me.RadioButton_CharSeparated.Name = "RadioButton_CharSeparated"
        Me.RadioButton_CharSeparated.Size = New System.Drawing.Size(112, 17)
        Me.RadioButton_CharSeparated.TabIndex = 0
        Me.RadioButton_CharSeparated.TabStop = True
        Me.RadioButton_CharSeparated.Text = "Separated by char"
        Me.RadioButton_CharSeparated.UseVisualStyleBackColor = True
        '
        'GroupBox_Columns
        '
        Me.GroupBox_Columns.Controls.Add(Me.RadioButton_CharSeparated)
        Me.GroupBox_Columns.Controls.Add(Me.ComboBox_Separator)
        Me.GroupBox_Columns.Controls.Add(Me.RadioButton_FixedWidth)
        Me.GroupBox_Columns.Controls.Add(Me.TextBox_ColumnWidth)
        Me.GroupBox_Columns.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Columns.Name = "GroupBox_Columns"
        Me.GroupBox_Columns.Size = New System.Drawing.Size(239, 84)
        Me.GroupBox_Columns.TabIndex = 0
        Me.GroupBox_Columns.TabStop = False
        Me.GroupBox_Columns.Text = "Columns"
        '
        'TextBox_ColumnWidth
        '
        Me.TextBox_ColumnWidth.Location = New System.Drawing.Point(131, 52)
        Me.TextBox_ColumnWidth.Name = "TextBox_ColumnWidth"
        Me.TextBox_ColumnWidth.Size = New System.Drawing.Size(90, 20)
        Me.TextBox_ColumnWidth.TabIndex = 3
        '
        'NumericUpDown_LineTitles
        '
        Me.NumericUpDown_LineTitles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_LineTitles.Location = New System.Drawing.Point(15, 46)
        Me.NumericUpDown_LineTitles.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_LineTitles.Name = "NumericUpDown_LineTitles"
        Me.NumericUpDown_LineTitles.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDown_LineTitles.TabIndex = 1
        Me.NumericUpDown_LineTitles.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_LineTitles
        '
        Me.Label_LineTitles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_LineTitles.AutoSize = True
        Me.Label_LineTitles.Location = New System.Drawing.Point(12, 25)
        Me.Label_LineTitles.Name = "Label_LineTitles"
        Me.Label_LineTitles.Size = New System.Drawing.Size(76, 13)
        Me.Label_LineTitles.TabIndex = 0
        Me.Label_LineTitles.Text = "Line with titles:"
        '
        'ComboBox_DecimalMark
        '
        Me.ComboBox_DecimalMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_DecimalMark.DropDownWidth = 50
        Me.ComboBox_DecimalMark.FormattingEnabled = True
        Me.ComboBox_DecimalMark.Location = New System.Drawing.Point(9, 51)
        Me.ComboBox_DecimalMark.Name = "ComboBox_DecimalMark"
        Me.ComboBox_DecimalMark.Size = New System.Drawing.Size(66, 21)
        Me.ComboBox_DecimalMark.TabIndex = 1
        '
        'GroupBox_Settings
        '
        Me.GroupBox_Settings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Settings.Controls.Add(Me.Label_LineTitles)
        Me.GroupBox_Settings.Controls.Add(Me.NumericUpDown_LineTitles)
        Me.GroupBox_Settings.Controls.Add(Me.CheckBox_Units)
        Me.GroupBox_Settings.Controls.Add(Me.NumericUpDown_LineUnits)
        Me.GroupBox_Settings.Controls.Add(Me.Label_LineData)
        Me.GroupBox_Settings.Controls.Add(Me.NumericUpDown_LineData)
        Me.GroupBox_Settings.Controls.Add(Me.Label_ColumnDateTime)
        Me.GroupBox_Settings.Controls.Add(Me.NumericUpDown_ColumnDateTime)
        Me.GroupBox_Settings.Location = New System.Drawing.Point(12, 102)
        Me.GroupBox_Settings.Name = "GroupBox_Settings"
        Me.GroupBox_Settings.Size = New System.Drawing.Size(530, 79)
        Me.GroupBox_Settings.TabIndex = 3
        Me.GroupBox_Settings.TabStop = False
        Me.GroupBox_Settings.Text = "Settings"
        '
        'CheckBox_Units
        '
        Me.CheckBox_Units.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_Units.AutoSize = True
        Me.CheckBox_Units.Location = New System.Drawing.Point(149, 24)
        Me.CheckBox_Units.Name = "CheckBox_Units"
        Me.CheckBox_Units.Size = New System.Drawing.Size(96, 17)
        Me.CheckBox_Units.TabIndex = 2
        Me.CheckBox_Units.Text = "Line with units:"
        Me.CheckBox_Units.UseVisualStyleBackColor = True
        '
        'NumericUpDown_LineUnits
        '
        Me.NumericUpDown_LineUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_LineUnits.Location = New System.Drawing.Point(149, 46)
        Me.NumericUpDown_LineUnits.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_LineUnits.Name = "NumericUpDown_LineUnits"
        Me.NumericUpDown_LineUnits.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDown_LineUnits.TabIndex = 3
        Me.NumericUpDown_LineUnits.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_LineData
        '
        Me.Label_LineData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_LineData.AutoSize = True
        Me.Label_LineData.Location = New System.Drawing.Point(283, 25)
        Me.Label_LineData.Name = "Label_LineData"
        Me.Label_LineData.Size = New System.Drawing.Size(72, 13)
        Me.Label_LineData.TabIndex = 4
        Me.Label_LineData.Text = "First data line:"
        '
        'NumericUpDown_LineData
        '
        Me.NumericUpDown_LineData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_LineData.Location = New System.Drawing.Point(286, 46)
        Me.NumericUpDown_LineData.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_LineData.Name = "NumericUpDown_LineData"
        Me.NumericUpDown_LineData.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDown_LineData.TabIndex = 5
        Me.NumericUpDown_LineData.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NumericUpDown_ColumnDateTime
        '
        Me.NumericUpDown_ColumnDateTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_ColumnDateTime.Location = New System.Drawing.Point(474, 46)
        Me.NumericUpDown_ColumnDateTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_ColumnDateTime.Name = "NumericUpDown_ColumnDateTime"
        Me.NumericUpDown_ColumnDateTime.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDown_ColumnDateTime.TabIndex = 8
        Me.NumericUpDown_ColumnDateTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'GroupBox_DecimalMark
        '
        Me.GroupBox_DecimalMark.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_DecimalMark.Controls.Add(Me.Label_DecimalMark)
        Me.GroupBox_DecimalMark.Controls.Add(Me.ComboBox_DecimalMark)
        Me.GroupBox_DecimalMark.Location = New System.Drawing.Point(416, 12)
        Me.GroupBox_DecimalMark.Name = "GroupBox_DecimalMark"
        Me.GroupBox_DecimalMark.Size = New System.Drawing.Size(126, 84)
        Me.GroupBox_DecimalMark.TabIndex = 2
        Me.GroupBox_DecimalMark.TabStop = False
        Me.GroupBox_DecimalMark.Text = "Decimal mark"
        '
        'Label_DecimalMark
        '
        Me.Label_DecimalMark.AutoSize = True
        Me.Label_DecimalMark.Location = New System.Drawing.Point(6, 26)
        Me.Label_DecimalMark.Name = "Label_DecimalMark"
        Me.Label_DecimalMark.Size = New System.Drawing.Size(74, 13)
        Me.Label_DecimalMark.TabIndex = 0
        Me.Label_DecimalMark.Text = "Decimal mark:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusImage})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 440)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(549, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusImage
        '
        Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.tick
        Me.StatusImage.Name = "StatusImage"
        Me.StatusImage.Size = New System.Drawing.Size(39, 17)
        Me.StatusImage.Text = "OK"
        '
        'GroupBox_Dateformat
        '
        Me.GroupBox_Dateformat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Dateformat.Controls.Add(Me.PictureBox_DateFormatHelp)
        Me.GroupBox_Dateformat.Controls.Add(Me.Label_Dateformat)
        Me.GroupBox_Dateformat.Controls.Add(Me.ComboBox_Dateformat)
        Me.GroupBox_Dateformat.Location = New System.Drawing.Point(258, 12)
        Me.GroupBox_Dateformat.Name = "GroupBox_Dateformat"
        Me.GroupBox_Dateformat.Size = New System.Drawing.Size(152, 84)
        Me.GroupBox_Dateformat.TabIndex = 1
        Me.GroupBox_Dateformat.TabStop = False
        Me.GroupBox_Dateformat.Text = "Date format"
        '
        'PictureBox_DateFormatHelp
        '
        Me.PictureBox_DateFormatHelp.Image = Global.BlueM.Wave.My.Resources.Resources.help
        Me.PictureBox_DateFormatHelp.Location = New System.Drawing.Point(75, 25)
        Me.PictureBox_DateFormatHelp.Name = "PictureBox_DateFormatHelp"
        Me.PictureBox_DateFormatHelp.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox_DateFormatHelp.TabIndex = 3
        Me.PictureBox_DateFormatHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox_DateFormatHelp, "https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-" & _
                "format-strings")
        '
        'Label_Dateformat
        '
        Me.Label_Dateformat.AutoSize = True
        Me.Label_Dateformat.Location = New System.Drawing.Point(6, 26)
        Me.Label_Dateformat.Name = "Label_Dateformat"
        Me.Label_Dateformat.Size = New System.Drawing.Size(65, 13)
        Me.Label_Dateformat.TabIndex = 0
        Me.Label_Dateformat.Text = "Date format:"
        '
        'ComboBox_Dateformat
        '
        Me.ComboBox_Dateformat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Dateformat.FormattingEnabled = True
        Me.ComboBox_Dateformat.Location = New System.Drawing.Point(7, 53)
        Me.ComboBox_Dateformat.Name = "ComboBox_Dateformat"
        Me.ComboBox_Dateformat.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox_Dateformat.TabIndex = 1
        '
        'ImportDiag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(549, 462)
        Me.Controls.Add(SplitContainer1)
        Me.Controls.Add(Me.GroupBox_Columns)
        Me.Controls.Add(Me.GroupBox_Dateformat)
        Me.Controls.Add(Me.GroupBox_DecimalMark)
        Me.Controls.Add(Me.GroupBox_Settings)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(565, 500)
        Me.Name = "ImportDiag"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import series from file"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        SplitContainer1.ResumeLayout(False)
        Me.GroupBox_Selection.ResumeLayout(False)
        Me.GroupBox_Selection.PerformLayout()
        Me.GroupBox_Preview.ResumeLayout(False)
        Me.GroupBox_Preview.PerformLayout()
        Me.GroupBox_Columns.ResumeLayout(False)
        Me.GroupBox_Columns.PerformLayout()
        CType(Me.NumericUpDown_LineTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Settings.ResumeLayout(False)
        Me.GroupBox_Settings.PerformLayout()
        CType(Me.NumericUpDown_LineUnits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_LineData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_ColumnDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_DecimalMark.ResumeLayout(False)
        Me.GroupBox_DecimalMark.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox_Dateformat.ResumeLayout(False)
        Me.GroupBox_Dateformat.PerformLayout()
        CType(Me.PictureBox_DateFormatHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents GroupBox_Settings As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox_Columns As System.Windows.Forms.GroupBox
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents GroupBox_Preview As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox_DecimalMark As System.Windows.Forms.GroupBox
    Private WithEvents TextBox_Preview As System.Windows.Forms.RichTextBox
    Private WithEvents ComboBox_Separator As System.Windows.Forms.ComboBox
    Private WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Private WithEvents Label_Search As System.Windows.Forms.Label
    Private WithEvents Label_ColumnDateTime As System.Windows.Forms.Label
    Private WithEvents RadioButton_FixedWidth As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton_CharSeparated As System.Windows.Forms.RadioButton
    Private WithEvents TextBox_ColumnWidth As System.Windows.Forms.TextBox
    Private WithEvents NumericUpDown_LineTitles As System.Windows.Forms.NumericUpDown
    Private WithEvents Label_LineTitles As System.Windows.Forms.Label
    Private WithEvents ComboBox_DecimalMark As System.Windows.Forms.ComboBox
    Private WithEvents NumericUpDown_LineUnits As System.Windows.Forms.NumericUpDown
    Private WithEvents NumericUpDown_LineData As System.Windows.Forms.NumericUpDown
    Private WithEvents Label_LineData As System.Windows.Forms.Label
    Private WithEvents CheckBox_Units As System.Windows.Forms.CheckBox
    Private WithEvents Label_DecimalMark As System.Windows.Forms.Label
    Private WithEvents TextBox_Search As System.Windows.Forms.TextBox
    Private WithEvents Label_Series As System.Windows.Forms.Label
    Private WithEvents NumericUpDown_ColumnDateTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusImage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Button_SelectAll As System.Windows.Forms.Button
    Friend WithEvents GroupBox_Selection As System.Windows.Forms.GroupBox
    Friend WithEvents Label_File As System.Windows.Forms.Label
    Friend WithEvents GroupBox_Dateformat As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox_Dateformat As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Dateformat As System.Windows.Forms.Label
    Friend WithEvents PictureBox_DateFormatHelp As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
