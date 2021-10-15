Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Web.Script.Serialization
Imports System.Net.ServicePointManager




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
    <WebMethod()>
    Public Sub SleepingSwapInsert()
        Dim result As New clsResult
        Dim simInfo As New clssiminfo

        Dim JsonInput = New StreamReader(HttpContext.Current.Request.InputStream)
        simInfo = JsonConvert.DeserializeObject(Of clsSimInfo)(JsonInput.ReadToEnd)

        If (simInfo.OldMsisdn <> "" And simInfo.IMSI <> "") Then
            result.Code = "0"
            result.Description = "Successful"
            result.stats = SleepingSwapInsert(simInfo.OldMsisdn, simInfo.IMSI)
        Else
            result.Code = "103"
            result.Description = "Failed"

        End If


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
    Private Function SleepingSwapInsert(Msisdn As String, IMSI As String) As clsSimInfo
        Dim stats As New clsSimInfo
        Dim StrSQL As String = "exec [simSwapweb].[dbo].[sleepingSwapInsert_20211012] '" & Msisdn & "','" & IMSI & "'"
        Dim cn As New SqlConnection(connectionStringVas22)
        Dim cmd As New SqlCommand
        Try
            Dim Dtstats As New DataTable
            cmd.Connection = cn
            cn.Open()
            cmd = New SqlCommand(StrSQL, cn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = Integer.MaxValue
            Dtstats.Load(cmd.ExecuteReader())
            If Dtstats.Rows.Count > 0 Then
                For Each Dt In Dtstats.Rows



                    stats.OldMsisdn = Dt.Item("OldMsisdn").ToString
                    stats.Newmsisdn = Dt.Item("NewMsisdn").ToString
                    stats.IMSI = Dt.Item("IMSI").ToString

                Next

            End If
            cn.Dispose()
            SqlConnection.ClearPool(cn)

            cn.Close()
            cn = Nothing

        Catch ex As Exception

        End Try
        Return stats
    End Function
End Class