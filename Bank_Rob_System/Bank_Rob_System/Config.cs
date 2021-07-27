using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Rob_System
{
    public class Config : IRocketPluginConfiguration
    {
        public int robTime { get; set; }
        public string bankaperm { get; set; }
        public string soyguncuperm { get; set; }
        public string polisPerm { get; set; }
        public string sivilPerm { get; set; }
        public long cooldown { get; set; }
        public uint ucret { get; set; }
        public int minPolisSayi { get; set; }
        public void LoadDefaults()
        {
            polisPerm = "flame.polis";
            sivilPerm = "flame.sivil";
            bankaperm = "banka.soyabilir";
            soyguncuperm = "banka.soyguncu";
            cooldown = 899;
            robTime = 300;
            minPolisSayi = 5;
            ucret = 5000;
        }
    }
}
