using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Rob_System
{
    public class Ekle : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "ekle";

        public string Help => "";

        public string Syntax => "<name>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            string message = string.Join(" ", command);
            if (message == "ekle")
            {
                Main.Instance.polis++;
                UnturnedChat.Say(caller, "Sunucudaki Polis Sayısı: " + Main.Instance.polis);
                if (Main.Instance.polis == Main.Instance.Configuration.Instance.minPolisSayi)
                {
                    foreach (var steamPlayer in Provider.clients)
                    {
                        UnturnedPlayer players = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                        UnturnedChat.Say(players, "Sunucuda Yeterli Sayıda Polis Var Banka Soygunu Yapılabilir");
                    }

                    var backnumber = Main.Instance.Configuration.Instance.minPolisSayi -= 1;
                    if (Main.Instance.polis == backnumber)
                    {
                        foreach (var steamPlayer in Provider.clients)
                        {
                            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                            UnturnedPlayer players = unturnedPlayer;
                            UnturnedChat.Say(players, "Sunucuda Yeterli Sayıda Polis Yok Banka Soygunu Yapılamaz");
                        }
                    }
                }
            }
            if (message == "sil")
            {
                Main.Instance.polis--;
                UnturnedChat.Say(caller, "Sunucudaki Polis Sayısı: " + Main.Instance.polis);
                if (Main.Instance.polis == Main.Instance.Configuration.Instance.minPolisSayi)
                {
                    foreach (var steamPlayer in Provider.clients)
                    {
                        UnturnedPlayer players = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                        UnturnedChat.Say(players, "Sunucuda Yeterli Sayıda Polis Var Banka Soygunu Yapılabilir");
                    }

                    var backnumber = Main.Instance.Configuration.Instance.minPolisSayi -= 1;
                    if (Main.Instance.polis == backnumber)
                    {
                        foreach (var steamPlayer in Provider.clients)
                        {
                            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                            UnturnedPlayer players = unturnedPlayer;
                            UnturnedChat.Say(players, "Sunucuda Yeterli Sayıda Polis Yok Banka Soygunu Yapılamaz");
                        }
                    }
                }
            }
        }
    }
}
