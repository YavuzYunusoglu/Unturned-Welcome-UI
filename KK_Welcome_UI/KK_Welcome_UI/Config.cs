using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK_Welcome_UI
{
    public class Config : IRocketPluginConfiguration
    {
        public string SunucuIsmi { get; set; }
        public string DiscordAciklama { get; set; }
        public string DiscordLink { get; set; }
        public string LogoURL { get; set; }
        public void LoadDefaults()
        {
            DiscordAciklama = "Discorda Katil";
            SunucuIsmi = "PDW RolePlay";
            DiscordLink = "https://discord.gg/Z7aKCPZeSj";
            LogoURL = "https://i.hizliresim.com/ltrqzq4.png";
        }
    }
}
