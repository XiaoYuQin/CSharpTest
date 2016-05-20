using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mAppwidgetCoordinates
{
    class MapPointFactory
    {
        private static void debug(String str){Console.WriteLine(str);}
        public static String produceMapPoint(Point s,Point e,int count,String cardIndex,int beginCardNumber)
        {
            Console.WriteLine("produceMapPoint");
            MapPointList list = new MapPointList();

            double sss = ((double)2 / (double)100);
            debug("sss = " + sss);


            debug("e.x = "+e.X+"e.y"+e.Y);
            debug("s.x = " + s.X + "s.y" + s.Y);
            double xstep = (((double)e.X - (double)s.X) / count);
            debug("xstep = " + xstep);
            double ystep = (((double)e.Y - (double)s.Y) / count);
            debug("ystep = " + ystep);
            for (int i = 0; i < count;i++)
            {
                MapPoints mapPoint = new MapPoints();
                //debug("xstep * count = " + xstep * i);

                mapPoint.point.X = (int)(s.X + xstep * i);
                mapPoint.point.Y = (int)(s.Y + ystep * i);
                mapPoint.id = cardIndex + "" + (beginCardNumber + i);

                debug("i = "+i+"  x = " + mapPoint.point.X + " y = " + mapPoint.point.Y);
                list.add(mapPoint);
                //setProgressBar1();
                
            }
            string output = JsonConvert.SerializeObject(list);
            Console.WriteLine(output);

            return output;
        }

        public class MapPointList
        {
            public List<MapPoints> list;
            public MapPointList()
            {
                list = new List<MapPoints>();
            }
            public void add(MapPoints mapPoints)
            {
                list.Add(mapPoints);
            }
        }
    }
}
