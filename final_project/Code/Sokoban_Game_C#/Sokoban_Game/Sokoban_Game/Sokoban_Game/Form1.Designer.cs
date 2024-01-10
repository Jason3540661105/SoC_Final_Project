namespace Sokoban_Game
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SerialPortConnect = new System.Windows.Forms.Button();
            this.SerialPortDisconnect = new System.Windows.Forms.Button();
            this.comport_label = new System.Windows.Forms.Label();
            this.SerialPortSelect = new System.Windows.Forms.ComboBox();
            this.uart_richTextBox_test = new System.Windows.Forms.RichTextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SerialPortConnect
            // 
            this.SerialPortConnect.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SerialPortConnect.Location = new System.Drawing.Point(712, 144);
            this.SerialPortConnect.Name = "SerialPortConnect";
            this.SerialPortConnect.Size = new System.Drawing.Size(153, 72);
            this.SerialPortConnect.TabIndex = 0;
            this.SerialPortConnect.Text = "Connect";
            this.SerialPortConnect.UseVisualStyleBackColor = true;
            this.SerialPortConnect.Click += new System.EventHandler(this.SerialPortConnect_Click);
            // 
            // SerialPortDisconnect
            // 
            this.SerialPortDisconnect.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SerialPortDisconnect.Location = new System.Drawing.Point(712, 255);
            this.SerialPortDisconnect.Name = "SerialPortDisconnect";
            this.SerialPortDisconnect.Size = new System.Drawing.Size(153, 72);
            this.SerialPortDisconnect.TabIndex = 1;
            this.SerialPortDisconnect.Text = "Disconnect";
            this.SerialPortDisconnect.UseVisualStyleBackColor = true;
            this.SerialPortDisconnect.Click += new System.EventHandler(this.SerialPortDisconnect_Click);
            // 
            // comport_label
            // 
            this.comport_label.AutoSize = true;
            this.comport_label.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comport_label.Location = new System.Drawing.Point(720, 32);
            this.comport_label.Name = "comport_label";
            this.comport_label.Size = new System.Drawing.Size(128, 23);
            this.comport_label.TabIndex = 2;
            this.comport_label.Text = "COM PORT";
            // 
            // SerialPortSelect
            // 
            this.SerialPortSelect.FormattingEnabled = true;
            this.SerialPortSelect.Location = new System.Drawing.Point(727, 79);
            this.SerialPortSelect.Name = "SerialPortSelect";
            this.SerialPortSelect.Size = new System.Drawing.Size(121, 23);
            this.SerialPortSelect.TabIndex = 3;
            this.SerialPortSelect.SelectedIndexChanged += new System.EventHandler(this.SerialPortSelect_SelectedIndexChanged);
            // 
            // uart_richTextBox_test
            // 
            this.uart_richTextBox_test.Location = new System.Drawing.Point(78, 416);
            this.uart_richTextBox_test.Name = "uart_richTextBox_test";
            this.uart_richTextBox_test.Size = new System.Drawing.Size(704, 48);
            this.uart_richTextBox_test.TabIndex = 4;
            this.uart_richTextBox_test.Text = "";
            this.uart_richTextBox_test.TextChanged += new System.EventHandler(this.uart_richTextBox_test_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(52, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "人物座標";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.YLabel.Location = new System.Drawing.Point(64, 100);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(49, 20);
            this.YLabel.TabIndex = 9;
            this.YLabel.Text = "Y : ";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.XLabel.Location = new System.Drawing.Point(64, 69);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(49, 20);
            this.XLabel.TabIndex = 8;
            this.XLabel.Text = "X : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 516);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.uart_richTextBox_test);
            this.Controls.Add(this.SerialPortSelect);
            this.Controls.Add(this.comport_label);
            this.Controls.Add(this.SerialPortDisconnect);
            this.Controls.Add(this.SerialPortConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SerialPortConnect;
        private System.Windows.Forms.Button SerialPortDisconnect;
        private System.Windows.Forms.Label comport_label;
        private System.Windows.Forms.ComboBox SerialPortSelect;
        private System.Windows.Forms.RichTextBox uart_richTextBox_test;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label XLabel;
    }
}

