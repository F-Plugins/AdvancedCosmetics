using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace Feli.RocketMod.AdvancedCosmetics.Commands
{
    public class RemoveCosmetics : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var plugin = Plugin.Instance;
            var player = (UnturnedPlayer) caller;

            var cosmetics =
                plugin.CosmeticsStore.Instance.PlayersCosmetics.FirstOrDefault(x =>
                    x.PlayerId == player.CSteamID.m_SteamID);

            if (cosmetics == null)
            {
                UnturnedChat.Say(player, plugin.Translate("RemoveCosmetics:Fail"), true);
                return;
            }

            plugin.CosmeticsStore.Instance.PlayersCosmetics.Remove(cosmetics);

            if (command.Any(x => x == "--force"))
            {
                player.Player.sendRelayToServer(Provider.ip, Provider.port, Provider.serverPassword, false);
            }
            else
            {
                UnturnedChat.Say(player, plugin.Translate("RemoveCosmetics:Success"), true);
            }
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "removecosmetics";
        public string Help => "Removes all your custom cosmetics";
        public string Syntax => "/removecosmetics [--force (reconnects you to the server so the changes get applied)]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>();
    }
}