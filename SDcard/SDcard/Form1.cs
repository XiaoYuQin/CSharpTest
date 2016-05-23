using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDcard
{
    public partial class Form1 : Form
    {
        SDUtils SDutils = new SDUtils("N:");
        byte[] SectorBytes;
        private System.Timers.Timer timer = new System.Timers.Timer();
        long time;
        public Form1()
        {
            InitializeComponent();
            SectorBytes = new byte[512];

            Console.WriteLine("Form1");
            Console.WriteLine("SectorLength = " + SDutils.SectorLength);

            for (int i = 0; i < 512; i++)
            {
                SectorBytes[i] = (byte)('1'+i);            
            }
            SDutils.WriteSector(SectorBytes, 1);

            byte[] readSectorBytes = new byte[512];
            readSectorBytes = SDutils.ReadSector(1);
            string str1 = System.Text.Encoding.Default.GetString(readSectorBytes);
            textBox1.Text = str1;
            Console.WriteLine(str1);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;


            
            //1000000*12.3#16.8!\r\n

            /*for (int i = 0; i < 512; i++)
            {
                Console.Write(readSectorBytes[i]);
                Console.Write(' ');
            }
            Console.WriteLine("");*/

            /*readSectorBytes = SDutils.ReadSector(2);
            string str2 = System.Text.Encoding.Default.GetString(readSectorBytes);
            Console.WriteLine(str2);*/
            /*for (int i = 0; i < 512; i++)
            {
                Console.Write(readSectorBytes[i]);
                Console.Write(' ');
            }
            Console.WriteLine("");*/




        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //模拟的做一些耗时的操作
            System.Threading.Thread.Sleep(2000);
            time++;

        }


    }
}
