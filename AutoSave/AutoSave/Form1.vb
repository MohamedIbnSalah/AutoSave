Imports System.Runtime.InteropServices
Imports System.Text

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListBox1.Focus()
        ListBox1.Items.Clear()
        For Each p As Process In Process.GetProcesses
            If p.MainWindowTitle <> "" Then
                ListBox1.Items.Add(p.MainWindowTitle)
            End If
        Next
        ListBox1.SelectedIndex = -1
        ListBox1.Focus()
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            Timer1.Interval = 30000
        ElseIf ComboBox1.SelectedIndex = 1 Then
            Timer1.Interval = 60000
        ElseIf ComboBox1.SelectedIndex = 2 Then
            Timer1.Interval = 120000
        ElseIf ComboBox1.SelectedIndex = 3 Then
            Timer1.Interval = 180000
        End If
    End Sub

    <DllImport("user32.dll", EntryPoint:="GetWindowThreadProcessId")> _
    Private Shared Function GetWindowThreadProcessId(<InAttribute()> ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="GetForegroundWindow")> Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowTextW")> _
    Private Shared Function GetWindowTextW(<InAttribute()> ByVal hWnd As IntPtr, <OutAttribute(), MarshalAs(UnmanagedType.LPWStr)> ByVal lpString As StringBuilder, ByVal nMaxCount As Integer) As Integer
    End Function

    Private Function GetForgroundWindowInfo() As String
        Dim hWnd As IntPtr = GetForegroundWindow()
        If Not hWnd.Equals(IntPtr.Zero) Then

            Dim lgth As Integer = GetWindowTextLength(hWnd)
            Dim wTitle As New System.Text.StringBuilder("", lgth + 1)
            If lgth > 0 Then
                GetWindowTextW(hWnd, wTitle, wTitle.Capacity)
            End If


            Return wTitle.ToString
        End If
        Return ""
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ListBox1.SelectedIndex > -1 Then
            If ListBox1.Text = GetForgroundWindowInfo() Then
                Label2.Text = "أخر عملية حفظ تمت بتاريخ : " & Now
                SendKeys.SendWait("^{s}")
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ListBox1.Focus()
        ListBox1.Items.Clear()
        For Each p As Process In Process.GetProcesses
            If p.MainWindowTitle <> "" Then
                ListBox1.Items.Add(p.MainWindowTitle)
            End If
        Next
        ListBox1.SelectedIndex = -1
        ListBox1.Focus()
    End Sub
End Class
