<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ImportCSVDialog
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim SplitContainer1 As SplitContainer
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportCSVDialog))
        GroupBox_Selection = New GroupBox()
        Label_Selected = New Label()
        Label_Search = New Label()
        TextBox_Search = New TextBox()
        Label_Series = New Label()
        ListBox_Series = New ListBox()
        Button_SelectAll = New Button()
        GroupBox_Preview = New GroupBox()
        Button_EncodingAutodetect = New Button()
        ComboBox_Encoding = New ComboBox()
        Label_File = New Label()
        Label_Encoding = New Label()
        TextBox_Preview = New RichTextBox()
        ComboBox_Separator = New ComboBox()
        Button_OK = New Button()
        Button_Cancel = New Button()
        Label_ColumnDateTime = New Label()
        RadioButton_FixedWidth = New RadioButton()
        RadioButton_CharSeparated = New RadioButton()
        GroupBox_Columns = New GroupBox()
        TextBox_ColumnWidth = New TextBox()
        NumericUpDown_LineTitles = New NumericUpDown()
        Label_LineTitles = New Label()
        ComboBox_DecimalSeparator = New ComboBox()
        GroupBox_Settings = New GroupBox()
        CheckBox_Units = New CheckBox()
        NumericUpDown_LineUnits = New NumericUpDown()
        Label_LineData = New Label()
        NumericUpDown_LineData = New NumericUpDown()
        NumericUpDown_ColumnDateTime = New NumericUpDown()
        GroupBox_DecimalMark = New GroupBox()
        Label_DecimalMark = New Label()
        StatusStrip1 = New StatusStrip()
        StatusImage = New ToolStripStatusLabel()
        GroupBox_Dateformat = New GroupBox()
        PictureBox_DateFormatHelp = New PictureBox()
        Label_Dateformat = New Label()
        ComboBox_Dateformat = New ComboBox()
        ToolTip1 = New ToolTip(components)
        PictureBox_TitleSuffixHelp = New PictureBox()
        Label_TitleSuffix = New Label()
        TextBox_TitleSuffix = New TextBox()
        SplitContainer1 = New SplitContainer()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        GroupBox_Selection.SuspendLayout()
        GroupBox_Preview.SuspendLayout()
        GroupBox_Columns.SuspendLayout()
        CType(NumericUpDown_LineTitles, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_Settings.SuspendLayout()
        CType(NumericUpDown_LineUnits, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown_LineData, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown_ColumnDateTime, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_DecimalMark.SuspendLayout()
        StatusStrip1.SuspendLayout()
        GroupBox_Dateformat.SuspendLayout()
        CType(PictureBox_DateFormatHelp, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SplitContainer1.Location = New Point(14, 217)
        SplitContainer1.Margin = New Padding(0)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox_Selection)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox_Preview)
        SplitContainer1.Size = New Size(618, 254)
        SplitContainer1.SplitterDistance = 205
        SplitContainer1.SplitterWidth = 5
        SplitContainer1.TabIndex = 33
        ' 
        ' GroupBox_Selection
        ' 
        GroupBox_Selection.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Selection.Controls.Add(Label_Selected)
        GroupBox_Selection.Controls.Add(Label_Search)
        GroupBox_Selection.Controls.Add(TextBox_Search)
        GroupBox_Selection.Controls.Add(Label_Series)
        GroupBox_Selection.Controls.Add(ListBox_Series)
        GroupBox_Selection.Controls.Add(Button_SelectAll)
        GroupBox_Selection.Location = New Point(0, 0)
        GroupBox_Selection.Name = "GroupBox_Selection"
        GroupBox_Selection.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Selection.Size = New Size(205, 254)
        GroupBox_Selection.TabIndex = 0
        GroupBox_Selection.TabStop = False
        GroupBox_Selection.Text = "Series selection"
        ' 
        ' Label_Selected
        ' 
        Label_Selected.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label_Selected.Location = New Point(111, 226)
        Label_Selected.Name = "Label_Selected"
        Label_Selected.Size = New Size(88, 15)
        Label_Selected.TabIndex = 34
        Label_Selected.Text = "0 selected"
        Label_Selected.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label_Search
        ' 
        Label_Search.AutoSize = True
        Label_Search.Location = New Point(14, 24)
        Label_Search.Name = "Label_Search"
        Label_Search.Size = New Size(45, 15)
        Label_Search.TabIndex = 0
        Label_Search.Text = "Search:"
        ' 
        ' TextBox_Search
        ' 
        TextBox_Search.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Search.Location = New Point(69, 21)
        TextBox_Search.Name = "TextBox_Search"
        TextBox_Search.Size = New Size(129, 23)
        TextBox_Search.TabIndex = 1
        ' 
        ' Label_Series
        ' 
        Label_Series.AutoSize = True
        Label_Series.Location = New Point(14, 53)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New Size(90, 15)
        Label_Series.TabIndex = 2
        Label_Series.Text = "Available series:"
        ' 
        ' ListBox_Series
        ' 
        ListBox_Series.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Series.FormattingEnabled = True
        ListBox_Series.Location = New Point(18, 74)
        ListBox_Series.Name = "ListBox_Series"
        ListBox_Series.SelectionMode = SelectionMode.MultiExtended
        ListBox_Series.Size = New Size(180, 139)
        ListBox_Series.TabIndex = 3
        ' 
        ' Button_SelectAll
        ' 
        Button_SelectAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAll.Location = New Point(18, 220)
        Button_SelectAll.Name = "Button_SelectAll"
        Button_SelectAll.Size = New Size(80, 27)
        Button_SelectAll.TabIndex = 4
        Button_SelectAll.Text = "Select all"
        Button_SelectAll.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_Preview
        ' 
        GroupBox_Preview.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Preview.Controls.Add(Button_EncodingAutodetect)
        GroupBox_Preview.Controls.Add(ComboBox_Encoding)
        GroupBox_Preview.Controls.Add(Label_File)
        GroupBox_Preview.Controls.Add(Label_Encoding)
        GroupBox_Preview.Controls.Add(TextBox_Preview)
        GroupBox_Preview.Location = New Point(0, 0)
        GroupBox_Preview.Name = "GroupBox_Preview"
        GroupBox_Preview.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Preview.Size = New Size(408, 254)
        GroupBox_Preview.TabIndex = 0
        GroupBox_Preview.TabStop = False
        GroupBox_Preview.Text = "File preview"
        ' 
        ' Button_EncodingAutodetect
        ' 
        Button_EncodingAutodetect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_EncodingAutodetect.Location = New Point(226, 220)
        Button_EncodingAutodetect.Name = "Button_EncodingAutodetect"
        Button_EncodingAutodetect.Size = New Size(80, 27)
        Button_EncodingAutodetect.TabIndex = 3
        Button_EncodingAutodetect.Text = "Autodetect"
        Button_EncodingAutodetect.UseVisualStyleBackColor = True
        ' 
        ' ComboBox_Encoding
        ' 
        ComboBox_Encoding.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox_Encoding.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Encoding.FormattingEnabled = True
        ComboBox_Encoding.Location = New Point(82, 222)
        ComboBox_Encoding.Name = "ComboBox_Encoding"
        ComboBox_Encoding.Size = New Size(137, 23)
        ComboBox_Encoding.TabIndex = 2
        ' 
        ' Label_File
        ' 
        Label_File.AutoSize = True
        Label_File.Location = New Point(10, 24)
        Label_File.Name = "Label_File"
        Label_File.Size = New Size(28, 15)
        Label_File.TabIndex = 0
        Label_File.Text = "File:"
        ' 
        ' Label_Encoding
        ' 
        Label_Encoding.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label_Encoding.AutoSize = True
        Label_Encoding.Location = New Point(10, 226)
        Label_Encoding.Name = "Label_Encoding"
        Label_Encoding.Size = New Size(60, 15)
        Label_Encoding.TabIndex = 1
        Label_Encoding.Text = "Encoding:"
        ' 
        ' TextBox_Preview
        ' 
        TextBox_Preview.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Preview.BackColor = Color.White
        TextBox_Preview.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TextBox_Preview.Location = New Point(14, 50)
        TextBox_Preview.Name = "TextBox_Preview"
        TextBox_Preview.ReadOnly = True
        TextBox_Preview.Size = New Size(387, 163)
        TextBox_Preview.TabIndex = 1
        TextBox_Preview.Text = ""
        TextBox_Preview.WordWrap = False
        ' 
        ' ComboBox_Separator
        ' 
        ComboBox_Separator.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Separator.DropDownWidth = 100
        ComboBox_Separator.FormattingEnabled = True
        ComboBox_Separator.Location = New Point(153, 21)
        ComboBox_Separator.Name = "ComboBox_Separator"
        ComboBox_Separator.Size = New Size(104, 23)
        ComboBox_Separator.TabIndex = 1
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(450, 478)
        Button_OK.Margin = New Padding(4, 3, 4, 3)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(88, 27)
        Button_OK.TabIndex = 4
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(545, 478)
        Button_Cancel.Margin = New Padding(4, 3, 4, 3)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(88, 27)
        Button_Cancel.TabIndex = 5
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' Label_ColumnDateTime
        ' 
        Label_ColumnDateTime.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label_ColumnDateTime.AutoSize = True
        Label_ColumnDateTime.Location = New Point(477, 28)
        Label_ColumnDateTime.Name = "Label_ColumnDateTime"
        Label_ColumnDateTime.Size = New Size(134, 15)
        Label_ColumnDateTime.TabIndex = 6
        Label_ColumnDateTime.Text = "Column with date/time:"
        ' 
        ' RadioButton_FixedWidth
        ' 
        RadioButton_FixedWidth.AutoSize = True
        RadioButton_FixedWidth.Location = New Point(18, 61)
        RadioButton_FixedWidth.Name = "RadioButton_FixedWidth"
        RadioButton_FixedWidth.Size = New Size(86, 19)
        RadioButton_FixedWidth.TabIndex = 2
        RadioButton_FixedWidth.TabStop = True
        RadioButton_FixedWidth.Text = "Fixed width"
        RadioButton_FixedWidth.UseVisualStyleBackColor = True
        ' 
        ' RadioButton_CharSeparated
        ' 
        RadioButton_CharSeparated.AutoSize = True
        RadioButton_CharSeparated.Location = New Point(18, 22)
        RadioButton_CharSeparated.Name = "RadioButton_CharSeparated"
        RadioButton_CharSeparated.Size = New Size(119, 19)
        RadioButton_CharSeparated.TabIndex = 0
        RadioButton_CharSeparated.TabStop = True
        RadioButton_CharSeparated.Text = "Separated by char"
        RadioButton_CharSeparated.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_Columns
        ' 
        GroupBox_Columns.Controls.Add(RadioButton_CharSeparated)
        GroupBox_Columns.Controls.Add(ComboBox_Separator)
        GroupBox_Columns.Controls.Add(RadioButton_FixedWidth)
        GroupBox_Columns.Controls.Add(TextBox_ColumnWidth)
        GroupBox_Columns.Location = New Point(14, 14)
        GroupBox_Columns.Name = "GroupBox_Columns"
        GroupBox_Columns.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Columns.Size = New Size(279, 97)
        GroupBox_Columns.TabIndex = 0
        GroupBox_Columns.TabStop = False
        GroupBox_Columns.Text = "Columns"
        ' 
        ' TextBox_ColumnWidth
        ' 
        TextBox_ColumnWidth.Location = New Point(153, 60)
        TextBox_ColumnWidth.Name = "TextBox_ColumnWidth"
        TextBox_ColumnWidth.Size = New Size(104, 23)
        TextBox_ColumnWidth.TabIndex = 3
        ' 
        ' NumericUpDown_LineTitles
        ' 
        NumericUpDown_LineTitles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown_LineTitles.Location = New Point(18, 53)
        NumericUpDown_LineTitles.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_LineTitles.Name = "NumericUpDown_LineTitles"
        NumericUpDown_LineTitles.Size = New Size(58, 23)
        NumericUpDown_LineTitles.TabIndex = 1
        NumericUpDown_LineTitles.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label_LineTitles
        ' 
        Label_LineTitles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label_LineTitles.AutoSize = True
        Label_LineTitles.Location = New Point(14, 29)
        Label_LineTitles.Name = "Label_LineTitles"
        Label_LineTitles.Size = New Size(86, 15)
        Label_LineTitles.TabIndex = 0
        Label_LineTitles.Text = "Line with titles:"
        ' 
        ' ComboBox_DecimalSeparator
        ' 
        ComboBox_DecimalSeparator.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_DecimalSeparator.DropDownWidth = 50
        ComboBox_DecimalSeparator.FormattingEnabled = True
        ComboBox_DecimalSeparator.Location = New Point(10, 59)
        ComboBox_DecimalSeparator.Name = "ComboBox_DecimalSeparator"
        ComboBox_DecimalSeparator.Size = New Size(76, 23)
        ComboBox_DecimalSeparator.TabIndex = 1
        ' 
        ' GroupBox_Settings
        ' 
        GroupBox_Settings.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Settings.Controls.Add(Label_LineTitles)
        GroupBox_Settings.Controls.Add(NumericUpDown_LineTitles)
        GroupBox_Settings.Controls.Add(CheckBox_Units)
        GroupBox_Settings.Controls.Add(NumericUpDown_LineUnits)
        GroupBox_Settings.Controls.Add(Label_LineData)
        GroupBox_Settings.Controls.Add(NumericUpDown_LineData)
        GroupBox_Settings.Controls.Add(Label_ColumnDateTime)
        GroupBox_Settings.Controls.Add(NumericUpDown_ColumnDateTime)
        GroupBox_Settings.Location = New Point(14, 118)
        GroupBox_Settings.Name = "GroupBox_Settings"
        GroupBox_Settings.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Settings.Size = New Size(618, 91)
        GroupBox_Settings.TabIndex = 3
        GroupBox_Settings.TabStop = False
        GroupBox_Settings.Text = "Settings"
        ' 
        ' CheckBox_Units
        ' 
        CheckBox_Units.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        CheckBox_Units.AutoSize = True
        CheckBox_Units.Location = New Point(174, 28)
        CheckBox_Units.Margin = New Padding(4, 3, 4, 3)
        CheckBox_Units.Name = "CheckBox_Units"
        CheckBox_Units.Size = New Size(106, 19)
        CheckBox_Units.TabIndex = 2
        CheckBox_Units.Text = "Line with units:"
        CheckBox_Units.UseVisualStyleBackColor = True
        ' 
        ' NumericUpDown_LineUnits
        ' 
        NumericUpDown_LineUnits.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown_LineUnits.Location = New Point(174, 53)
        NumericUpDown_LineUnits.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_LineUnits.Name = "NumericUpDown_LineUnits"
        NumericUpDown_LineUnits.Size = New Size(58, 23)
        NumericUpDown_LineUnits.TabIndex = 3
        NumericUpDown_LineUnits.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label_LineData
        ' 
        Label_LineData.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label_LineData.AutoSize = True
        Label_LineData.Location = New Point(330, 29)
        Label_LineData.Name = "Label_LineData"
        Label_LineData.Size = New Size(80, 15)
        Label_LineData.TabIndex = 4
        Label_LineData.Text = "First data line:"
        ' 
        ' NumericUpDown_LineData
        ' 
        NumericUpDown_LineData.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown_LineData.Location = New Point(334, 53)
        NumericUpDown_LineData.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown_LineData.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_LineData.Name = "NumericUpDown_LineData"
        NumericUpDown_LineData.Size = New Size(70, 23)
        NumericUpDown_LineData.TabIndex = 5
        NumericUpDown_LineData.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' NumericUpDown_ColumnDateTime
        ' 
        NumericUpDown_ColumnDateTime.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown_ColumnDateTime.Location = New Point(553, 53)
        NumericUpDown_ColumnDateTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_ColumnDateTime.Name = "NumericUpDown_ColumnDateTime"
        NumericUpDown_ColumnDateTime.Size = New Size(58, 23)
        NumericUpDown_ColumnDateTime.TabIndex = 8
        NumericUpDown_ColumnDateTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' GroupBox_DecimalMark
        ' 
        GroupBox_DecimalMark.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        GroupBox_DecimalMark.Controls.Add(Label_DecimalMark)
        GroupBox_DecimalMark.Controls.Add(ComboBox_DecimalSeparator)
        GroupBox_DecimalMark.Location = New Point(485, 14)
        GroupBox_DecimalMark.Name = "GroupBox_DecimalMark"
        GroupBox_DecimalMark.Padding = New Padding(4, 3, 4, 3)
        GroupBox_DecimalMark.Size = New Size(147, 97)
        GroupBox_DecimalMark.TabIndex = 2
        GroupBox_DecimalMark.TabStop = False
        GroupBox_DecimalMark.Text = "Decimal separator"
        ' 
        ' Label_DecimalMark
        ' 
        Label_DecimalMark.AutoSize = True
        Label_DecimalMark.Location = New Point(7, 30)
        Label_DecimalMark.Name = "Label_DecimalMark"
        Label_DecimalMark.Size = New Size(105, 15)
        Label_DecimalMark.TabIndex = 0
        Label_DecimalMark.Text = "Decimal separator:"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {StatusImage})
        StatusStrip1.Location = New Point(0, 511)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 16, 0)
        StatusStrip1.Size = New Size(640, 22)
        StatusStrip1.TabIndex = 6
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' StatusImage
        ' 
        StatusImage.Image = CType(resources.GetObject("StatusImage.Image"), Image)
        StatusImage.Name = "StatusImage"
        StatusImage.Size = New Size(39, 17)
        StatusImage.Text = "OK"
        ' 
        ' GroupBox_Dateformat
        ' 
        GroupBox_Dateformat.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Dateformat.Controls.Add(PictureBox_DateFormatHelp)
        GroupBox_Dateformat.Controls.Add(Label_Dateformat)
        GroupBox_Dateformat.Controls.Add(ComboBox_Dateformat)
        GroupBox_Dateformat.Location = New Point(301, 14)
        GroupBox_Dateformat.Name = "GroupBox_Dateformat"
        GroupBox_Dateformat.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Dateformat.Size = New Size(177, 97)
        GroupBox_Dateformat.TabIndex = 1
        GroupBox_Dateformat.TabStop = False
        GroupBox_Dateformat.Text = "Date format"
        ' 
        ' PictureBox_DateFormatHelp
        ' 
        PictureBox_DateFormatHelp.Image = CType(resources.GetObject("PictureBox_DateFormatHelp.Image"), Image)
        PictureBox_DateFormatHelp.Location = New Point(88, 29)
        PictureBox_DateFormatHelp.Name = "PictureBox_DateFormatHelp"
        PictureBox_DateFormatHelp.Size = New Size(19, 18)
        PictureBox_DateFormatHelp.TabIndex = 3
        PictureBox_DateFormatHelp.TabStop = False
        ToolTip1.SetToolTip(PictureBox_DateFormatHelp, "https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings")
        ' 
        ' Label_Dateformat
        ' 
        Label_Dateformat.AutoSize = True
        Label_Dateformat.Location = New Point(7, 30)
        Label_Dateformat.Name = "Label_Dateformat"
        Label_Dateformat.Size = New Size(73, 15)
        Label_Dateformat.TabIndex = 0
        Label_Dateformat.Text = "Date format:"
        ' 
        ' ComboBox_Dateformat
        ' 
        ComboBox_Dateformat.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_Dateformat.FormattingEnabled = True
        ComboBox_Dateformat.Location = New Point(8, 61)
        ComboBox_Dateformat.Name = "ComboBox_Dateformat"
        ComboBox_Dateformat.Size = New Size(162, 23)
        ComboBox_Dateformat.TabIndex = 1
        ' 
        ' PictureBox_TitleSuffixHelp
        ' 
        PictureBox_TitleSuffixHelp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        PictureBox_TitleSuffixHelp.Image = CType(resources.GetObject("PictureBox_TitleSuffixHelp.Image"), Image)
        PictureBox_TitleSuffixHelp.Location = New Point(253, 483)
        PictureBox_TitleSuffixHelp.Margin = New Padding(4, 3, 4, 3)
        PictureBox_TitleSuffixHelp.Name = "PictureBox_TitleSuffixHelp"
        PictureBox_TitleSuffixHelp.Size = New Size(19, 18)
        PictureBox_TitleSuffixHelp.TabIndex = 36
        PictureBox_TitleSuffixHelp.TabStop = False
        ToolTip1.SetToolTip(PictureBox_TitleSuffixHelp, "The title suffix will be appended to each series' title during import")
        ' 
        ' Label_TitleSuffix
        ' 
        Label_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label_TitleSuffix.AutoSize = True
        Label_TitleSuffix.Location = New Point(16, 483)
        Label_TitleSuffix.Margin = New Padding(4, 0, 4, 0)
        Label_TitleSuffix.Name = "Label_TitleSuffix"
        Label_TitleSuffix.Size = New Size(64, 15)
        Label_TitleSuffix.TabIndex = 34
        Label_TitleSuffix.Text = "Title suffix:"
        ' 
        ' TextBox_TitleSuffix
        ' 
        TextBox_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_TitleSuffix.Location = New Point(90, 480)
        TextBox_TitleSuffix.Margin = New Padding(4, 3, 4, 3)
        TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        TextBox_TitleSuffix.Size = New Size(156, 23)
        TextBox_TitleSuffix.TabIndex = 35
        ' 
        ' ImportCSVDialog
        ' 
        AcceptButton = Button_OK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button_Cancel
        ClientSize = New Size(640, 533)
        Controls.Add(PictureBox_TitleSuffixHelp)
        Controls.Add(Label_TitleSuffix)
        Controls.Add(TextBox_TitleSuffix)
        Controls.Add(SplitContainer1)
        Controls.Add(GroupBox_Columns)
        Controls.Add(GroupBox_Dateformat)
        Controls.Add(GroupBox_DecimalMark)
        Controls.Add(GroupBox_Settings)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Controls.Add(StatusStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(656, 571)
        Name = "ImportCSVDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Import series from CSV file"
        TopMost = True
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        GroupBox_Selection.ResumeLayout(False)
        GroupBox_Selection.PerformLayout()
        GroupBox_Preview.ResumeLayout(False)
        GroupBox_Preview.PerformLayout()
        GroupBox_Columns.ResumeLayout(False)
        GroupBox_Columns.PerformLayout()
        CType(NumericUpDown_LineTitles, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_Settings.ResumeLayout(False)
        GroupBox_Settings.PerformLayout()
        CType(NumericUpDown_LineUnits, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown_LineData, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown_ColumnDateTime, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_DecimalMark.ResumeLayout(False)
        GroupBox_DecimalMark.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        GroupBox_Dateformat.ResumeLayout(False)
        GroupBox_Dateformat.PerformLayout()
        CType(PictureBox_DateFormatHelp, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
    Private WithEvents ComboBox_DecimalSeparator As System.Windows.Forms.ComboBox
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
    Friend WithEvents ComboBox_Encoding As ComboBox
    Friend WithEvents Label_Encoding As Label
    Friend WithEvents Button_EncodingAutodetect As Button
    Friend WithEvents Label_Selected As Label
    Friend WithEvents PictureBox_TitleSuffixHelp As PictureBox
    Friend WithEvents Label_TitleSuffix As Label
    Friend WithEvents TextBox_TitleSuffix As TextBox
End Class
