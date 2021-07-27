using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Bank_Rob_System
{
    public class Command : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "soy";

        public string Help => "Banka Soymanızı Sağlar";

        public string Syntax => "<name>";

        public List<string> Aliases => new List<string> {"rob","bankasoy","bankrob"};

        public List<string> Permissions => new List<string> {Main.Instance.Configuration.Instance.bankaperm};

        [Obsolete]
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var ana = Main.Instance;
            var config = ana.Configuration.Instance;
            UnturnedPlayer player = caller as UnturnedPlayer;
            var soyguncu = ana.soyguncu;
            if (player.HasPermission(config.soyguncuperm))
            {
                if(ana.polis >= config.minPolisSayi)
                {
                    if (ana.cooldown <= 0)
                    {
                        if (ana.bankState == false)
                        {
                            soyguncu.Add(player.CSteamID);
                            ana.bankState = true;
                            ana.timer(player);
                            ana.bildirim(player);
                        }
                        else
                        {
                            UnturnedChat.Say(player, ana.Translate("StateTrue"));
                        }
                    }
                    else
                    {
                        UnturnedChat.Say(player, ana.Translate("Cooldown") + ana.cooldown + " Saniye", Color.magenta);
                    }
                }
                else
                {
                    UnturnedChat.Say(player,ana.Translate("MinPolis"),Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(player, ana.Translate("Permission"),Color.red);
            }
        }
    }
}
