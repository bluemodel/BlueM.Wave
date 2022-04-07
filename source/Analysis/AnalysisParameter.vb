Friend Class AnalysisParameter

    Public Enum ParameterTypeEnum
        Timeseries
        [Integer]
        [Date]
    End Enum

    Public Enum ParameterAmountEnum
        [Single]
        Multiple
    End Enum

    Public ParameterType As ParameterTypeEnum
    Public ParameterAmount As ParameterAmountEnum
    Public Description As String
    Public DefaultValue As Object
    Public MinValue As Object
    Public MaxValue As Object
    Private _Value As Object

    Public Property Value As Object
        Get
            Return _Value
        End Get
        Set
            _Value = Value
        End Set
    End Property

    Public Sub New(description As String, type As ParameterTypeEnum, amount As ParameterAmountEnum, Optional def As Object = Nothing, Optional min As Object = Nothing, Optional max As Object = Nothing)

        Me.Description = description
        Me.ParameterType = type
        Me.ParameterAmount = amount
        Me.DefaultValue = def
        Me.MinValue = min
        Me.MaxValue = max

        Me.Value = Nothing

    End Sub

End Class
