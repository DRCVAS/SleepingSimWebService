Public Class clsSimInfo
    Private strMsisdn As String
    Public Property Msisdn As String
        Get
            Return strMsisdn
        End Get
        Set(value As String)
            strMsisdn = value
        End Set
    End Property
    Private strImsi As String
    Public Property Imsi As String
        Get
            Return strImsi
        End Get
        Set(value As String)
            strImsi = value
        End Set
    End Property
    Private strOldMsisdn As String
    Public Property OldMsisdn As String
        Get
            Return strOldMsisdn
        End Get
        Set(value As String)
            strOldMsisdn = value
        End Set
    End Property
End Class
