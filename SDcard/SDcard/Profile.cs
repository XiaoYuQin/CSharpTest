using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SDcard
{
    class Profile
    {
        public static void LoadProfile()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory;
            _file = new IniFile(strPath + "Cfg.ini");
            G_PictureNumbers = _file.ReadString("CONFIG", "PictureNumbers", "0");
            G_StartSector = _file.ReadString("CONFIG", "StartSector", "0");
            G_SectorNumbers = _file.ReadString("CONFIG", "SectorNumbers", "0");
            G_PictureHeight = _file.ReadString("CONFIG", "PictureHeight", "0");
            G_PictureWidth = _file.ReadString("CONFIG",  "PictureWidth", "0");
        }
        public static void SaveProfile()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory;
            _file.WriteString("CONFIG", "PictureNumbers", G_PictureNumbers);            //写数据，下同
            _file.WriteString("CONFIG", "StartSector",    G_StartSector);
            _file.WriteString("CONFIG", "SectorNumbers",  G_SectorNumbers);
            _file.WriteString("CONFIG", "PictureHeight",  G_PictureHeight);
            _file.WriteString("CONFIG", "PictureWidth",   G_PictureWidth);
        }
	
        private static IniFile _file;//内置了一个对象
        public static string G_PictureNumbers = "0";
        public static string G_StartSector = "0";
        public static string G_SectorNumbers = "0";
        public static string G_PictureHeight = "0";
        public static string G_PictureWidth = "0";
    }
}
