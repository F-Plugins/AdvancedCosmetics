using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Commands
{
    public class RemoveCustomCosmeticsCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "removecustomcosmetics";

        public string Help => "A command to remove the custom cosmetics";

        public string Syntax => "/removecustomcosmetics";

        public List<string> Aliases => new List<string>
        {
            "removecosmetics",
        };

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;
            if (AdvancedCosmetics.Instance.Cosmetics.ContainsKey(player.CSteamID))
            {
                AdvancedCosmetics.Instance.Cosmetics.Remove(player.CSteamID);
                UnturnedChat.Say(player, AdvancedCosmetics.Instance.Translate("RemovedCos"), AdvancedCosmetics.Instance.MessageColor);
            }
            else
            {
                UnturnedChat.Say(player, AdvancedCosmetics.Instance.Translate("NotSetUp"), AdvancedCosmetics.Instance.MessageColor);
            }
        }
    }
}
