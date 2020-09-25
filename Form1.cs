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

namespace ServoUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string data;

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            timer1.Enabled = true;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
             data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displayData_event));

        }
        private void displayData_event(object sender, EventArgs e)
        {
            label5.Text = progressBar1.Value.ToString();
                                          
            progressBar1.Value = Convert.ToInt32(data);   
            if (Convert.ToInt32(label5.Text) <= 150)
            {
                label6.Text = "Social Distance violation Detected!!!";
            }
            else
            {
                label6.Text = "No Violation";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x = comboBox1.SelectedItem.ToString();
            serialPort1.PortName = x;
            serialPort1.BaudRate = 9600;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    label7.ForeColor = Color.Green;
                    label7.Text = "CONNECTED";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Select serial port!!");
                label7.Text = "Disconnected";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToLongTimeString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen==true)
            {
                int pwm = trackBar1.Value;
                textBox2.Text = trackBar1.Value.ToString();
                byte[] b = BitConverter.GetBytes(pwm);
                serialPort1.Write(b, 0, 4);

            }
            else
            {
                MessageBox.Show("ERROR!!");
            }
        }

        
    }
}
