using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Bank_Rob_System
{
    public class Main : RocketPlugin<Config>
    {
        public List<object> soyguncu = new List<object>();
        public long cooldown { get; set; }
        public bool bankState { get; set; }
        public static Main Instance { get; private set; }
        public int polis;
        protected override void Load()
        {
            StartCoroutine(cooldownTimer());
            bankState = false;
            U.Events.OnPlayerConnected += connect;
            U.Events.OnPlayerDisconnected += disconnect;
            cooldown = Configuration.Instance.cooldown;
            Instance = this;
            Logger.Log($"{Name} Adlı Eklenti Yüklendi");
        }

        private void disconnect(UnturnedPlayer player)
        {
            var config = Configuration.Instance;
            if (player.HasPermission(config.polisPerm))
            {
                if (player.IsAdmin)
                {
                    return;
                }
                else
                {
                    if(polis != 0)
                    {
                        polis--;
                    }
                }
            }
        }

        private void connect(UnturnedPlayer player)
        {
            var config = Configuration.Instance;
            if (player.HasPermission(config.polisPerm))
            {
                if (player.IsAdmin)
                {
                    return;
                }
                else
                {
                    polis++;
                }
            }
        }

        protected override void Unload()
        {
            bankState = false;
            Instance = null;
            Logger.Log($"{Name} Adlı Eklenti Devre Dışı");
        }
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"Cooldown" , "Şu Anda Banka Soygunu Yapılamaz Banka Soygunu İçin Kalan Süre: "},
            {"Permission" , "Banka Soygunu Yapabilmek İçin Bir Çete Üyesi Olman Gerekiyor!"},
            {"PolisMessage" , "Birisi Banka Soygunu Yapıyor Konumu Haritanda İşaretlendi!" },
            {"BankName" , "Bankanın İsmi: "},
            {"Time" , "Banka Soygununun Bitmesine Kalan Süre: "},
            {"StateFalse" , "Şu Anda Banka Soygunu Yapılmıyor!"},
            {"StateTrue" , "Şu Anda Zaten Banka Soygunu Yapılıyor!"},
            {"RobEnd" , "Banka Soygunu Bitti Kazandığın Ücret: "},
            {"MinPolis" , "Sunucuda Yeteri Kadar Polis Bulunmamaktadır."},
            {"LeaveBank" , "Bankada Olmadığın İçin Ücret Verilmedi!"},
            {"BankState" , "Banka Soygunu: "},
            {"RobStart" , "Banka Soygunu Yapmaya Başladın /zaman Veya /time Yazarak Soygunun Bitmesine Ne Kadar Kaldığını Görebilirsin!"},
            {"NotARobber" , "Banka Soygununun Ne Zaman Biteceğini Görmek İçin Banka Soygunu Yapman Gerekir!"}
        };

        [Obsolete]
        public void bildirim(UnturnedPlayer player)
        {
            var config = Configuration.Instance;
            foreach (var steamPlayer in Provider.clients)
            {
                UnturnedPlayer polis = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                if (polis.HasPermission(config.polisPerm))
                {
                    var node = LevelNodes.nodes.OfType<LocationNode>().OrderBy(k => Vector3.Distance(k.point, player.Position)).FirstOrDefault();
                    polis.Player.quests.askSetMarker(player.CSteamID, true, player.Position);
                    UnturnedChat.Say(polis, Translate("PolisMessage"),Color.yellow);
                    UnturnedChat.Say(polis, Translate("BankName") + node.name, Color.cyan);
                }
            }
        }
        public void timer(UnturnedPlayer player)
        {
            StartCoroutine(RobTime(player));
        }
        public int kalanSure { get; set; }
        public IEnumerator<WaitForSeconds> RobTime(UnturnedPlayer player)
        {
            var config = Configuration.Instance;
            for (kalanSure = Configuration.Instance.robTime; kalanSure > 0; kalanSure--)
            {
                yield return new WaitForSeconds(1f);
            }
            if (player.HasPermission(config.bankaperm))
            {
                UnturnedChat.Say(player, Translate("RobEnd") + config.ucret, Color.cyan);
                player.Experience += config.ucret;
                StartCoroutine(cooldownTimer());
                bankState = false;
            }
            else
            {
                UnturnedChat.Say(player,Translate("LeaveBank"),Color.red);
            }
        }
        public IEnumerator<WaitForSeconds> cooldownTimer()
        {
            for (cooldown = cooldown; cooldown > 0; cooldown--)
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
