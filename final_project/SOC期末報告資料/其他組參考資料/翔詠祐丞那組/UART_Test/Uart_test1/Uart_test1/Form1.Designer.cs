namespace Uart_test1
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SerialPortConnect = new System.Windows.Forms.Button();
            this.SerialPortSelect = new System.Windows.Forms.ComboBox();
            this.SerialPortDisconnect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SerialPortConnect
            // 
            this.SerialPortConnect.Location = new System.Drawing.Point(560, 138);
            this.SerialPortConnect.Name = "SerialPortConnect";
            this.SerialPortConnect.Size = new System.Drawing.Size(121, 36);
            this.SerialPortConnect.TabIndex = 0;
            this.SerialPortConnect.Text = "Connect";
            this.SerialPortConnect.UseVisualStyleBackColor = true;
            this.SerialPortConnect.Click += new System.EventHandler(this.SerialPortConnect_Click);
            // 
            // SerialPortSelect
            // 
            this.SerialPortSelect.FormattingEnabled = true;
            this.SerialPortSelect.Location = new System.Drawing.Point(560, 93);
            this.SerialPortSelect.Name = "SerialPortSelect";
            this.SerialPortSelect.Size = new System.Drawing.Size(121, 23);
            this.SerialPortSelect.TabIndex = 1;
            this.SerialPortSelect.SelectedIndexChanged += new System.EventHandler(this.SerialPortSelect_SelectedIndexChanged);
            // 
            // SerialPortDisconnect
            // 
            this.SerialPortDisconnect.Location = new System.Drawing.Point(560, 195);
            this.SerialPortDisconnect.Name = "SerialPortDisconnect";
            this.SerialPortDisconnect.Size = new System.Drawing.Size(121, 36);
            this.SerialPortDisconnect.TabIndex = 2;
            this.SerialPortDisconnect.Text = "Disconnect";
            this.SerialPortDisconnect.UseVisualStyleBackColor = true;
            this.SerialPortDisconnect.Click += new System.EventHandler(this.SerialPortDisConnect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(283, 25);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SerialPortDisconnect);
            this.Controls.Add(this.SerialPortSelect);
            this.Controls.Add(this.SerialPortConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button SerialPortConnect;
        private System.Windows.Forms.ComboBox SerialPortSelect;
        private System.Windows.Forms.Button SerialPortDisconnect;
        private System.Windows.Forms.TextBox textBox1;
    }
}

