using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace mAppwidgetCoordinates
{
    public partial class Form1 : Form
    {
        private bool isReciveUdpData = true;
        UdpClient client = null;
        string receiveString = null;
        byte[] receiveData = null;
        IPEndPoint remotePoint;
        Thread _readThread;
        Point startPoint = new Point();
        Point endPoint = new Point();

        enum POINT_STEP
        {
            START_POINT_STEP,
            END_POINT_STEP,
            THIRD_POINT_STEP
        };
        POINT_STEP pointStep = POINT_STEP.START_POINT_STEP;

        private static void debug(String str) { Console.WriteLine(str); }
        public Form1()
        {
            Console.WriteLine("Form1");
            InitializeComponent();
            remotePoint = new IPEndPoint(IPAddress.Any, 0);
            _readThread = new Thread(reciveUdpData);
            _readThread.Start();

            /*MapPoints mapPoints = new MapPoints();
            mapPoints.point.X = 12;
            mapPoints.point.Y = 11;
            mapPoints.id = "b123";
            string output = JsonConvert.SerializeObject(mapPoints);
            Console.WriteLine(output);


            MapPoints p1 = new MapPoints();
            p1.point.X = 111;
            p1.point.Y = 222;
            p1.id = "b111";


            MapPointList mapPointList = new MapPointList();
            mapPointList.list.Add(mapPoints);
            mapPointList.list.Add(p1);
            Console.WriteLine(JsonConvert.SerializeObject(mapPointList));

            MapPoints m1;
            m1 = JsonConvert.DeserializeObject<MapPoints>(output);
            Console.WriteLine(m1.id);
            Console.WriteLine("x = "+m1.point.X+"y = "+m1.point.Y);*/


            String pointxxx = "123,333";
            int x = pointxxx.IndexOf(',');
            String y = pointxxx.Substring(0,x);
            Console.WriteLine("y = " + y);
            String z = pointxxx.Substring(x + 1, pointxxx.Length-x-1);
            Console.WriteLine("z = " + z);
            
            int intx = int.Parse(y);
            int inty = int.Parse(z);
            Console.WriteLine("intx = " + intx + " inty = " + inty);

        }


        private void reciveUdpData()
        {
            Console.WriteLine("reciveUdpData");
            client = new UdpClient(8000);
            while (isReciveUdpData)
            {
                receiveData = client.Receive(ref remotePoint);//接收数据 
                receiveString = Encoding.Default.GetString(receiveData);
                Console.WriteLine(receiveString);
                //receiveString.IndexOf(',');               

                Point tmpPoint = new Point();
                int indexof = receiveString.IndexOf(',');
                tmpPoint.X = int.Parse(receiveString.Substring(0, indexof));
                tmpPoint.Y = int.Parse(receiveString.Substring(indexof + 1, receiveString.Length - indexof - 1));

                switch(pointStep)
                {
                    case POINT_STEP.START_POINT_STEP:
                    {
                        Console.WriteLine("start point");
                        startPoint.X = tmpPoint.X;
                        startPoint.Y = tmpPoint.Y;
                        setStartPoint(startPoint);
                        pointStep = POINT_STEP.END_POINT_STEP;
                    }
                    break;
                    case POINT_STEP.END_POINT_STEP:
                    {
                        Console.WriteLine("end point");
                        endPoint.X = tmpPoint.X;
                        endPoint.Y = tmpPoint.Y;
                        setEndPoint(endPoint);
                        pointStep = POINT_STEP.THIRD_POINT_STEP;
                    }
                    break;
                    case POINT_STEP.THIRD_POINT_STEP:
                    {
                        Point tmp = new Point();
                        tmp.X = endPoint.X;
                        tmp.Y = endPoint.Y;
                        startPoint.X = tmp.X;
                        startPoint.Y = tmp.Y;
                        endPoint.X = tmpPoint.X;
                        endPoint.Y = tmpPoint.Y;

                        setStartPoint(startPoint);
                        setEndPoint(endPoint);
                    }
                    break;
                }

                /*if (pointStep == POINT_STEP.START_POINT_STEP)
                {
                    Console.WriteLine("start point");
                    startPoint.X = tmpPoint.X;
                    startPoint.Y = tmpPoint.Y;
                    setStartPoint(startPoint);
                    pointStep = POINT_STEP.END_POINT_STEP;
                }
                else
                {
                    Console.WriteLine("end point");
                    endPoint.X = tmpPoint.X;
                    endPoint.Y = tmpPoint.Y;
                    setEndPoint(endPoint);
                    pointStep = POINT_STEP.START_POINT_STEP;
                }
                else if(THIRD_POINT_STEP)
                {
                
                }*/
            }
            Console.WriteLine("reciveUdpData thread stop");
            client.Close();//关闭连接 
        }



        private delegate void startPointDelegate(Point point); //代理 // 声明delegate对象  
        // 欲传递的方法，它与CompareDelegate具有相同的参数和返回值类型  
        private void setStartPoint(Point point)
        {
            if (this.textBox1.InvokeRequired)//等待异步
            {
                startPointDelegate fc = new startPointDelegate(setStartPoint);
                this.Invoke(fc,point); //通过代理调用刷新方法
            }
            else
            {
                textBox1.Text = point.X + "," + point.Y;
            }
        }

        /// <summary>
        /// 设置终点点位的委托
        /// </summary>
        /// <param name="point"></param>
        private delegate void endPointDelegate(Point point); //代理 // 声明delegate对象  
                 
        /// <summary>
        ///  设置终点,欲传递的方法，它与CompareDelegate具有相同的参数和返回值类型  
        /// </summary>
        /// <param name="point"></param>
        private void setEndPoint(Point point)
        {
            if (this.textBox2.InvokeRequired)//等待异步
            {
                endPointDelegate fc = new endPointDelegate(setEndPoint);
                this.Invoke(fc, point); //通过代理调用刷新方法
            }
            else
            {
                textBox2.Text = point.X + "," + point.Y;
            }
        }
        private delegate void progressBar1Delegate(); //代理 // 声明delegate对象  
        public void setProgressBar1()
        {
            if (this.progressBar1.InvokeRequired)//等待异步
            {
                progressBar1Delegate fc = new progressBar1Delegate(setProgressBar1);
                this.Invoke(fc); //通过代理调用刷新方法
            }
            else
            {
                debug("textBox6.Text = " + textBox3.Text);
                debug("progressBar1.Value = " + (int.Parse(textBox3.Text)));
                progressBar1.Value = int.Parse(textBox3.Text) ;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Form1_FormClosing");
            isReciveUdpData = false;
        }

        private void buildMaps(Point start,Point end)
        {
            int Alen = Math.Abs(start.X - end.X);
            int Blen = Math.Abs(start.Y - end.Y);
            Console.WriteLine("A = "+Alen+"  B = "+Blen);
            double tanAB = Alen / Blen;
            Console.WriteLine("tanab = " + tanAB);

            //y=kx+a
            //k = (y - a) / k;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox3.Text == null)
            {
                MessageBox.Show("","");
                return;
            }
            if (textBox5.Text == "" || textBox5.Text == null)
            {
                MessageBox.Show("","");
                return;
            }
            if (textBox6.Text == "" || textBox6.Text == null)
            {
                MessageBox.Show("", "");
                return;
            }

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = int.Parse(textBox3.Text);
            
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync("");
            }     
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作
            //Thread.Sleep(5000);
            String json_return = MapPointFactory.produceMapPoint(startPoint, endPoint, int.Parse(textBox3.Text), textBox5.Text,int.Parse(textBox6.Text));
            setProgressBar1();
            e.Result = e.Argument + json_return;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
            this.textBox4.Text = e.Result.ToString();
            startPoint.X = endPoint.X;
            startPoint.Y = endPoint.Y;
        }
    }
    public class MapPoints {

        public int x;
        public int y;

        public MapPoints() {
            /*this.x = x;
            this.y = y;*/
        }
    }




}
