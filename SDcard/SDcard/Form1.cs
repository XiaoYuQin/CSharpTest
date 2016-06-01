using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace SDcard
{
    public partial class Form1 : Form
    {
        SDUtils SDutils;
        byte[] SectorBytes;
        private System.Timers.Timer timer = new System.Timers.Timer();
        long time;
        long index = 1;
        public Form1()
        {
            InitializeComponent();
            SectorBytes = new byte[512];

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  File type: {0}", d.DriveType);
            }


            Console.WriteLine("---------------------");

            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    Console.WriteLine("Drive {0}", d.Name);
                    comboBox1.Items.Add(d.Name);
                    comboBox1.SelectedIndex = 0;
                }
            }


         
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

        private void button3_Click(object sender, EventArgs e)
        {
            SDutils = new SDUtils("E:");
            Console.WriteLine("Form1");
            Console.WriteLine("SectorLength = " + SDutils.SectorLength);

           /* for (int i = 0; i < 512; i++)
            {
                SectorBytes[i] = (byte)('1' + i);
            }
            SDutils.WriteSector(SectorBytes, 1);*/

            byte[] readSectorBytes = new byte[512];
            readSectorBytes = SDutils.ReadSector(1);
            string str1 = System.Text.Encoding.Default.GetString(readSectorBytes);
            string s1 = str1.Insert(41, "\r\n");
            string s2 = s1.Insert(84, "\r\n");
            textBox1.Text = s2;
            Console.WriteLine(s2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            index--;
            if (index > 0)
            {
                Console.WriteLine("index = " + index);
                byte[] readSectorBytes = new byte[512];
                readSectorBytes = SDutils.ReadSector(index);
                string str1 = System.Text.Encoding.Default.GetString(readSectorBytes);
                string s1 = str1.Insert(41, "\r\n");
                string s2 = s1.Insert(84, "\r\n");
                textBox1.Text = s2;
                Console.WriteLine(s2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index++;
            if (index < 99999)
            {
                Console.WriteLine("index = " + index);
                byte[] readSectorBytes = new byte[512];
                readSectorBytes = SDutils.ReadSector(index);
                string str1 = System.Text.Encoding.Default.GetString(readSectorBytes);
                string s1 = str1.Insert(41, "\r\n");
                string s2 = s1.Insert(84, "\r\n");
                textBox1.Text = s2;
                Console.WriteLine(s2);
            }
        }



    }
}
