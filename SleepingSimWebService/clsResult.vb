Public Class clsResult
    Private strCode As String
    Public Property Code As String
        Get
            Return strCode
        End Get
        Set(value As String)
            strCode = value
        End Set
    End Property

    Private strDescription As String
    Public Property Description As String
        Get
            Return strDescription
        End Get
        Set(value As String)
            strDescription = value
        End Set
    End Property

    Private strReservedMsisdn As String
    Public Property ReservedMsisdn As String
        Get
            Return strReservedMsisdn
        End Get
        Set(value As String)
            strReservedMsisdn = value
        End Set
    End Property

    Private strstats As clsSimInfo
    Public Property stats As clsSimInfo
        Get
            Return strstats
        End Get
        Set(value As clsSimInfo)
            strstats = value
        End Set
    End Property
End Class
