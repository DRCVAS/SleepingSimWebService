Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports Newtonsoft.Json

Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class SleepingSim
    Inherits System.Web.Services.WebService
    Dim connectionStringVas22 As String = "Data Source=10.100.11.22;Initial Catalog=Alepo;Persist Security Info=True;User ID=sa;Password=VasP@ssw0rd"
    <WebMethod()>
    Public Sub ReservedNumber()
        Dim msisdn As String = ""
        Dim result As New clsResult
        result = GetReservedNumber()

        Context.Response.Clear()
        Context.Response.ContentType = "application/json"
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(result))

    End Sub

    Private Function GetReservedNumber() As clsResult

        Dim cn As New SqlConnection(connectionStringVas22)
        Dim cmd As New SqlCommand
        Dim Result As Integer = 0
        Dim reserved As New clsResult

        Try

            cmd.Connection = cn
            cn.Open()
            Dim strSql As String
            Dim dtresult As New DataTable

            strSql = "exec [simSwapweb].[dbo].[GetReservedNumber] "
            cmd = New SqlCommand(strSql, cn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = Integer.MaxValue
            dtresult.Load(cmd.ExecuteReader())

            For Each dt In dtresult.Rows


                If dtresult.Rows.Count > 0 Then
                    reserved.ReservedMsisdn = dt.item("msisdn")
                    reserved.Code = 0
                    reserved.Description = "Successful"
                Else

                    reserved.Code = 103
                    reserved.Description = "Failed to fetch"
                End If
            Next
            cn.Dispose()
            SqlConnection.ClearPool(cn)

            cn.Close()
            cn = Nothing

        Catch ex As Exception
            cn.Dispose()
            SqlConnection.ClearPool(cn)
            cn.Close()
            cn = Nothing
            reserved.Code = 103
            reserved.Description = "SQL Error"

        End Try
        Return reserved
    End Function
End Class