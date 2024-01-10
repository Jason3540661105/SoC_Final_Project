using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Uart_test1
{
    public partial class Form1 : Form
    {
        StringBuilder port = new StringBuilder();
        SerialPort SerialPort = new SerialPort();

        public Task ReceiveSerial;

        public Form1()
        {
            InitializeComponent();
            SerialPortSelect.Items.AddRange(SerialPort.GetPortNames());
            SerialPortDisconnect.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SerialPortConnect_Click (object sender, EventArgs e)
        {
            try
            {
                SerialPort.PortName = port.ToString();
                SerialPort.BaudRate = 115200;
                SerialPort.Parity = Parity.None;
                SerialPort.DataBits = 8;
                SerialPort.StopBits = StopBits.One;
                SerialPort.Open();
                SerialPortConnect.Enabled = false;
                SerialPortDisconnect.Enabled = true;

                if (SerialPort.IsOpen)
                {
                    Receive_Data();         
                }
            }
            catch (Exception exex)
            {
                MessageBox.Show(exex.Message);
                SerialPort.Close();

            }
        }

        private void SerialPortSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.Append(SerialPortSelect.Text);
        }

        private void SerialPortDisConnect_Click (object sender, EventArgs e)
        {
            SerialPortSelect.Enabled = true;
            SerialPortConnect.Enabled = false;
            SerialPort.Close();
        }

        private void Receive_Data()
        {
            try
            {
                ReceiveSerial = new Task(Receive_Data_avilable);

                ReceiveSerial.Start();
            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message);
            }
        }

        private void Receive_Data_avilable()
        {
            int uart_test_data = 24;

            int row = 0, col = 0, i = 0;
            //int number = 256;
            int number = 256;
            //byte[] ReceiveData = new byte[number];
            byte[] ReceiveData = new byte[uart_test_data];

            while (SerialPort.IsOpen)
            {
                SerialPort.DiscardInBuffer();
                //MessageBox.Show("check serialportselect again");
                //Task.Delay(1).Wait();
                //while (SerialPort.BytesToRead != (256)) ;
                while (SerialPort.BytesToRead != (24)) ;
                Task.Delay(5);

                SerialPort.Read(ReceiveData, 0, SerialPort.BytesToRead);
                textBox1.Text = ReceiveData.ToString();

                /*for (i = 0; i < number; i++)
                {

                    switch (ReceiveData[i])
                    {
                        case 0:
                            GridsColor[row, col] = EMPTY;
                            break;
                        case 1:
                            GridsColor[row, col] = BORDER;
                            break;
                        case 2:
                            GridsColor[row, col] = CONTINUE;
                            break;
                        case 3:
                            GridsColor[row, col] = GFOOD;
                            break;
                        case 4:
                            GridsColor[row, col] = BFOOD;
                            break;
                        default:
                            break;
                    }
                    Console.Write($"{ReceiveData[i]}");
                    col = col + 1;
                    if (col == COL)
                    {
                        row = row + 1;
                        col = 0;
                        Console.WriteLine();
                    }
                    if (row == ROW)
                    {
                        row = 0;
                        col = 0;

                    }
                }*/
                //Console.WriteLine($"{ Convert.ToString(ReceiveData[256],16)}");
                //SerialPort.DiscardInBuffer();
                //UpadteScreen();

                //SerialPort.DiscardInBuffer();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
