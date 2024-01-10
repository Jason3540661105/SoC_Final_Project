using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Sokoban_Game
{
    public partial class Form1 : Form
    {
        int X = 0, Y = 0;
        const int ROW = 9, COL = 8;                     //ROW是向下生長(高)    //COL是向右生長(寬)
        const int GRID_WIDTH = 36, GRID_HEIGHT = 36;
        const int GAME_POS_X = 200, GAME_POS_Y = 20;
        
        public Task ReceiveSerial;

        StringBuilder port = new StringBuilder();
        SerialPort SerialPort = new SerialPort();
        string text = "Is Working!";

        //Color WIN = Color.Yellow;
        //Color CONTINUE = Color.Yellow;
        //Color LOSE = Color.Green;
        Color BG_COLOR = Color.Black;

        //Game Item Color
        Color EMPTY         = Color.Black;
        Color WALL          = Color.Gray;
        Color Box_Desti     = Color.Red;
        Color Box_Pos       = Color.FromArgb(151, 62, 4);
        Color Person_1      = Color.Beige;
        Color Person_2      = Color.Beige;
        Color Box_On_Target = Color.Green;

        int[,] array = new int[ROW, COL];
        Color[,] GridsColor = new Color[ROW, COL];
        Label[,] grids = new Label[ROW, COL];

        public Form1()
        {
            InitializeComponent();
            SerialPortSelect.Items.AddRange(SerialPort.GetPortNames());

            /*for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    /*grids[i, j] = new Label();
                    grids[i, j].Width = GRID_WIDTH;
                    grids[i, j].Height = GRID_HEIGHT;

                    grids[i, j].BackColor = BG_COLOR;
                    grids[i, j].Left = j * GRID_WIDTH + GAME_POS_X;
                    grids[i, j].Top = i * GRID_HEIGHT + GAME_POS_Y;
                    grids[i, j].Visible = true;

                    this.Controls.Add(grids[i, j]); 
                    switch (array[i, j])
                    {
                        case 0:
                            this.Controls.Add(new space(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 1:
                            this.Controls.Add(new wall(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 3:
                            this.Controls.Add(new target(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 4:
                            this.Controls.Add(new box(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 5:
                            this.Controls.Add(new player(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 8:
                            this.Controls.Add(new player(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                        case 7:
                            this.Controls.Add(new box_on_target(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                            break;
                    }
                }
            }*/
            SerialPortDisconnect.Enabled = false;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SerialPortSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.Append(SerialPortSelect.Text);
        }

        private void SerialPortConnect_Click(object sender, EventArgs e)
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

                SerialPort.DiscardInBuffer();       //可加可不加

                if (SerialPort.IsOpen)
                {
                    Console.WriteLine(text + "Port is : " + SerialPort.PortName);
                    Receive_Data();
                }
            }
            catch (Exception exex)
            {
                MessageBox.Show(exex.Message);
                SerialPort.Close();

            }
        }

        private void SerialPortDisconnect_Click(object sender, EventArgs e)
        {
            SerialPortSelect.Enabled = true;
            SerialPortConnect.Enabled = false;
            SerialPort.Close();
        }

        private void uart_richTextBox_test_TextChanged(object sender, EventArgs e)
        {

        }

        private void Receive_Data()
        {
            try
            {
                ReceiveSerial = new Task(Receive_Data_available);
                ReceiveSerial.Start();
            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message);
            }
        }

        private void Receive_Data_available()
        {
            int row = 0, col = 0, i;
            
            int number = 73;                                   // 設定接收所有資料的位元組數量
            int Map_number = 72;                               // 設定接收地圖資料的位元組數量
            int Game_State_number = 72;                        // 遊戲狀態的位元組位置
            int Sokoban_Game_State = 0;

            byte[] ReceiveData = new byte[number];             //接收UART資料 : {地圖資料[72 bits]} + {遊戲遊玩狀態[1bit]}
            
            while (SerialPort.IsOpen)
            {
                if (SerialPort.BytesToRead == number)
                {
                    SerialPort.Read(ReceiveData, 0, number);                        // 讀取接收的資料到位元組陣列中 || 0是陣列索引值
                    Sokoban_Game_State = ReceiveData[Game_State_number];            // 將UART的第[72]資料儲存到Sokoban_Game_State中

                    string receivedText = Encoding.ASCII.GetString(ReceiveData);    // 將位元組陣列轉換為字串-之後會刪除
                    UpdateRichTextBox(receivedText);                                // 更新 RichTextBox中的文字-之後會刪除

                    //for (i=0; i<number; i++)
                    //{
                    //    Console.Write($"{ReceiveData[i]}");
                    //}
                    //MessageBox.Show("Game_State_number is"+ Sokoban_Game_State);
                    
                    for (i = 0; i < Map_number; i++)
                    {
                        switch (ReceiveData[i])
                        {
                            case 0:
                                {
                                    //GridsColor[row, col] = EMPTY;
                                    //this.Controls.Add(new space(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 0;
                                    break;
                                }
                            case 1:
                                {
                                    //GridsColor[row, col] = WALL;
                                    //this.Controls.Add(new wall(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 1;
                                    break;
                                }
                            case 3:
                                {
                                    //GridsColor[row, col] = Box_Desti;
                                    //this.Controls.Add(new target(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 3;
                                    break;
                                }
                            case 4:
                                {
                                    //GridsColor[row, col] = Box_Pos;
                                    //this.Controls.Add(new box(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 4;
                                    break;
                                }
                            case 5:
                                {
                                    //GridsColor[row, col] = Person_1;
                                    //this.Controls.Add(new player(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 5;
                                    X = col;
                                    Y = row;
                                    break;
                                }
                            case 8:
                                {
                                    //GridsColor[row, col] = Person_2;
                                    //this.Controls.Add(new player(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 8;
                                    X = col;
                                    Y = row;
                                    break;
                                }
                            case 7:
                                {
                                    //GridsColor[row, col] = Box_On_Target;
                                    //this.Controls.Add(new box_on_target(col * GRID_WIDTH + GAME_POS_X, row * GRID_HEIGHT + GAME_POS_Y));
                                    array[row, col] = 7;
                                    break;
                                }
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
                    }
                        /*for (int y = 0; y < ROW; y++)
                        {
                            for (int x = 0; x < COL; x++)
                            {
                                switch (array[x, y])
                                {
                                    case 0:
                                        this.Controls.Add(new space(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 1:
                                        this.Controls.Add(new wall(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 3:
                                        this.Controls.Add(new target(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 4:
                                        this.Controls.Add(new box(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 5:
                                        this.Controls.Add(new player(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 8:
                                        this.Controls.Add(new player(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    case 7:
                                        this.Controls.Add(new box_on_target(x * GRID_WIDTH + GAME_POS_X, y * GRID_HEIGHT + GAME_POS_Y));
                                        break;
                                    }
                                }
                             }*/
                        UpadteScreen();

                    if (Sokoban_Game_State == 1)
                    {
                        MessageBox.Show("遊戲通關！\n" + "1.重新遊玩遊戲請按「上下左右」的按鈕\n" + "2.想離開遊戲請按「重置」按鈕");
                        Task.Delay(20);
                    }
                    //this.Refresh();
                    //System.Threading.Thread.Sleep(100);
                }
                else
                {
                    // 若資料長度不足，等待一段時間再嘗試讀取
                    Task.Delay(20).Wait();
                }
            }
        }

        private void UpadteScreen()
        {
            //if(grids.InvokeRequired)
            this.Invoke(new EventHandler(delegate
            {
                // 清除现有的控件
                foreach (Control control in this.Controls.OfType<space>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                foreach (Control control in this.Controls.OfType<wall>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                foreach (Control control in this.Controls.OfType<box>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                foreach (Control control in this.Controls.OfType<target>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                foreach (Control control in this.Controls.OfType<player>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                foreach (Control control in this.Controls.OfType<box_on_target>().ToList())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }

                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COL; j++)
                    {
                        //grids[i, j].BackColor = GridsColor[i, j];
                        XLabel.Text = "X : " + X;
                        YLabel.Text = "Y : " + Y;
                        switch (array[i, j])
                        {
                            case 0:
                                {
                                    this.Controls.Add(new space(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            case 1:
                                {
                                    this.Controls.Add(new wall(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            case 3:
                                {
                                    this.Controls.Add(new target(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            case 4:
                                {
                                    this.Controls.Add(new box(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            case 5:
                            case 8:
                                {
                                    this.Controls.Add(new player(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            /*case 8:
                                {
                                    this.Controls.Add(new player(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }*/
                            case 7:
                                {
                                    this.Controls.Add(new box_on_target(j * GRID_WIDTH + GAME_POS_X, i * GRID_HEIGHT + GAME_POS_Y));
                                    //this.Refresh();
                                    //System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
            ));
        }

        //之後會刪除
        private void UpdateRichTextBox(string text)
        {
            if (uart_richTextBox_test.InvokeRequired)
            {
                uart_richTextBox_test.Invoke(new Action<string>(UpdateRichTextBox), text); // 在 UI 線程中更新 RichTextBox
            }
            else
            {
                uart_richTextBox_test.AppendText(text + Environment.NewLine); // 將接收到的資料顯示在 RichTextBox 中並換行
            }
        }
    }
}
