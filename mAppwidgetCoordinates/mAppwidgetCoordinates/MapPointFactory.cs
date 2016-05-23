using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace mAppwidgetCoordinates
{
    class MapPointFactory
    {
        private static void debug(String str){Console.WriteLine(str);}
        public static String produceMapPoint(Point s,Point e,int count,String cardIndex,int beginCardNumber)
        {
            Console.WriteLine("produceMapPoint");
            MapPointList mapPointss = new MapPointList();

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

                mapPoint.x = (int)(s.X + xstep * i);
                mapPoint.y = (int)(s.Y + ystep * i);
                mapPointss.add(cardIndex + "" + (beginCardNumber + i), mapPoint);

                /*mapPoint.id = cardIndex + "" + (beginCardNumber + i);

                debug("i = "+i+"  x = " + mapPoint.point.X + " y = " + mapPoint.point.Y);
                list.add(mapPoint);*/
                //setProgressBar1();
                
            }
            string output = ConvertJsonString(JsonConvert.SerializeObject(mapPointss));

            Console.WriteLine(output);

            return output;
        }

         private static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar =' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }          
        }


        public class MapPointList
        {
            public Dictionary<String, MapPoints> mapPoints;
            public MapPointList()
            {
                mapPoints = new Dictionary<String, MapPoints>();
            }
            public void add(String id ,MapPoints mapPoints)
            {
                this.mapPoints.Add(id, mapPoints);
            }
        }
    }
}
