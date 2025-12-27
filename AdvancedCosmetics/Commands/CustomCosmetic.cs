using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Provider;
using SDG.Unturned;

namespace RestoreMonarchy.AdvancedCosmetics.Commands
{
    public class CustomCosmetic : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var plugin = AdvancedCosmeticsPlugin.Instance;
            var player = (UnturnedPlayer) caller;

            if (command.Length < 1)
            {
                UnturnedChat.Say(player, plugin.Translate("CustomCosmetic:Usage"), true);
                return;
            }

            var search = command[0];

            if (!int.TryParse(search, out int searchId) || !AdvancedCosmeticsPlugin.Instance.EconInfos.TryGetValue(searchId, out UnturnedEconInfo cosmetic))
            {
                cosmetic = AdvancedCosmeticsPlugin.Instance.EconInfos.Values.FirstOrDefault(x => x.name.ToLower().Contains(search.ToLower()));
            }

            if (cosmetic == null)
            {
                UnturnedChat.Say(player, plugin.Translate("CustomCosmetic:NotFound", search), true);
                return;
            }

            var cosmetics = plugin.CosmeticsStore.Instance.GetOrAddCosmetics(player.CSteamID.m_SteamID);

            cosmetics.AddCosmetic(cosmetic);

            if (command.Any(x => x == "--force"))
            {
                player.Player.sendRelayToServer(Provider.ip, Provider.port, Provider.serverPassword, false);
            }
            else
            {
                UnturnedChat.Say(player, plugin.Translate("CustomCosmetic:Success", cosmetic.name), true);
            }
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "customcosmetic";
        public string Help => "Sets your custom cosmetics";
        public string Syntax => "/customcosmetics <cosmeticId> | /customcosmetics <cosmeticName> [--force (reconnects you to the server so the changes get applied)]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>();
    }
}
