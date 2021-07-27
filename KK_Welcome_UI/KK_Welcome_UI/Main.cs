using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK_Welcome_UI
{
    public class Main : RocketPlugin<Config>
    {
        public Main Instance { get; private set; }
        public override void LoadPlugin()
        {
            Instance = this;
            U.Events.OnPlayerConnected += Connected;
            EffectManager.onEffectButtonClicked += OnButtonClicked;
            Logger.Log($"{Name} Eklentisi Yüklendi");
            Logger.Log($"Discord: Y Λ V U Z#8317");
        }


        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            Instance = null;
            Logger.Log($"{Name} Eklentisi Yüklendi");
            U.Events.OnPlayerConnected -= Connected;
            Logger.Log($"Discord: Y Λ V U Z#8317");
        }
        private void Connected(UnturnedPlayer player)
        {
            var config = Configuration.Instance;
            EffectManager.sendUIEffect(31353, 31353, player.SteamPlayer().transportConnection, false, config.SunucuIsmi);
            EffectManager.sendUIEffectImageURL(31353, player.SteamPlayer().transportConnection, true, "Logo", config.LogoURL);
            player.Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, true);
        }
        public static void Discord(UnturnedPlayer player,string Aciklama,string Link)
        {
            player.Player.sendBrowserRequest(Aciklama, Link);
        }
        public void OnButtonClicked(Player caller, string buttonName)
        {
            var config = Configuration.Instance;
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(caller);
            if (buttonName == "oyna")
            {
                player.Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, false);
                EffectManager.askEffectClearByID(31353, player.SteamPlayer().transportConnection);
            }
            if (buttonName == "discord")
            {
                Discord(player, config.DiscordAciklama, config.DiscordLink);
            }
        }
    }
}
