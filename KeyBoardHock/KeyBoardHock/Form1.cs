using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KeyBoardHock
{
    public partial class Form1 : Form
    {
        BarCodeHook BarCode = new BarCodeHook();    
        public Form1()
        {
            InitializeComponent();
            System.Console.WriteLine("Form1");
            BarCode.BarCodeEvent += new BarCodeHook.BarCodeDelegate(BarCode_BarCodeEvent);    
        }
        private delegate void ShowInfoDelegate(BarCodeHook.BarCodes barCode);
        private void ShowInfo(BarCodeHook.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                System.Console.WriteLine(barCode.KeyName);
                textBox6.Text = textBox6.Text + barCode.KeyName;
            }
        }

        void BarCode_BarCodeEvent(BarCodeHook.BarCodes barCode)
        {
            ShowInfo(barCode);
        }



        private void Form1_Load_1(object sender, EventArgs e)
        {
            System.Console.WriteLine("Start");
            BarCode.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Console.WriteLine("Stop");
            BarCode.Stop();
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox6.Text.Length > 0)
            {
                //MessageBox.Show(textBox6.Text);
            }
        }  

    }
}
