Public Class clsSimInfo
    Private strNewmsisdn As String
    Public Property Newmsisdn As String
        Get
            Return strNewmsisdn
        End Get
        Set(value As String)
            strNewmsisdn = value
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

    Private strIMSI As String
    Public Property IMSI As String
        Get
            Return strIMSI
        End Get
        Set(value As String)
            strIMSI = value
        End Set
    End Property
End Class
