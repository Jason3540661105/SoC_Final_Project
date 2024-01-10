Imports System.IO.Ports

Public Class Form1
    Dim buffer As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myport As Array
        myport = IO.Ports.SerialPort.GetPortNames
        ComboBox1.Items.AddRange(myport) '抓取電腦COM_Port
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SerialPort1.PortName = ComboBox1.Text '設定COM
        SerialPort1.BaudRate = 115200 '設定鮑率
        SerialPort1.ReceivedBytesThreshold = 14 '固定接收4個bytes
        SerialPort1.Open() '開啟通訊
        Timer1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SerialPort1.Close() '關閉通訊
        Timer1.Enabled = False
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        buffer = SerialPort1.ReadExisting.ToString '
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox1.Text = Format(buffer)
        'TextBox1.Text = Format(Val(buffer) / 1000, "00")
        'TextBox2.Text = Format((Val(buffer) Mod 1000) / 10, "00")
        'TextBox3.Text = Format((Val(buffer) Mod 1000) Mod 10, "0")
        Me.Chart1.Series("溫度").Points.AddY(TextBox1.Text)
        Me.Chart1.Series("濕度").Points.AddY(TextBox2.Text)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class

