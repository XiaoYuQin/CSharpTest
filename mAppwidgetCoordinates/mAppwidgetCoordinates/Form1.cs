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


        }
        private void reciveUdpData()
        {
            Console.WriteLine("reciveUdpData");
            client = new UdpClient(11000);
            while (isReciveUdpData)
            {
                receiveData = client.Receive(ref remotePoint);//接收数据 
                receiveString = Encoding.Default.GetString(receiveData);
                Console.WriteLine(receiveString);                



            }
            Console.WriteLine("reciveUdpData -over");
            client.Close();//关闭连接 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Form1_FormClosing");
            isReciveUdpData = false;
        }

        private void buildMaps(Point start,Point end)
        {
            int Alen = Math.Abs(start.X - end.X);
            int Blen = Math.Abs(start.X - end.X);
            double tanAB = Alen / Blen;
            //y=kx+a
            //k = (y - a) / k;
            
        }
    }
    public class MapPoints {
        public Point point;
        public String id;
        
        public MapPoints() {
            point = new Point();
        }
    }
    public class MapPointList
    {
        public List<MapPoints> list;
        public MapPointList(){
            list = new List<MapPoints>();
        }
    }



}
