using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Commands
{
    public class CustomCosmeticCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "customcosmetic";

        public string Help => "A command to get a custom cosmetic";

        public string Syntax => "/customcosmetic <cosmeticId> | /customcosmetic <cosmeticName>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;
            if(command.Length < 1)
            {
                UnturnedChat.Say(player, "Correct Usage: " + Syntax, AdvancedCosmetics.Instance.MessageColor);
            }
            else if(int.TryParse(command[0], out int SkinId))
            {
                if(AdvancedCosmetics.Instance.EconInfos.FirstOrDefault(x => x.itemdefid == SkinId) != null)
                {
                    AddCosmetic(player, AdvancedCosmetics.Instance.EconInfos.FirstOrDefault(x => x.itemdefid == SkinId));
                }
                else
                {
                    UnturnedChat.Say(player, AdvancedCosmetics.Instance.Translate("NotFound", SkinId), AdvancedCosmetics.Instance.MessageColor);
                }
            }
            else 
            {
                var skin = command[0];
                if(AdvancedCosmetics.Instance.EconInfos.FirstOrDefault(x => x.name.Contains(skin)) != null)
                {
                    AddCosmetic(player, AdvancedCosmetics.Instance.EconInfos.FirstOrDefault(x => x.name.Contains(skin)));
                }
                else
                {
                    UnturnedChat.Say(player, AdvancedCosmetics.Instance.Translate("NotFound", command[0]), AdvancedCosmetics.Instance.MessageColor);
                }
            }
        }

        public void AddCosmetic(UnturnedPlayer player, UnturnedEconInfo info)
        {
            if (!AdvancedCosmetics.Instance.Cosmetics.ContainsKey(player.CSteamID))
            {
                AdvancedCosmetics.Instance.Cosmetics.Add(player.CSteamID, new Models.Cosmetic());
            }

            var cosmetics = AdvancedCosmetics.Instance.Cosmetics[player.CSteamID];
            var tipo = info.type.ToLower();
            if (tipo.Contains("backpack"))
            {
                cosmetics.Backpack = info.itemdefid;
            }
            else if (tipo.Contains("glasses"))
            {
                cosmetics.Glass = info.itemdefid;
            }
            else if (tipo.Contains("hat"))
            {
                cosmetics.Hat = info.itemdefid;
            }
            else if (tipo.Contains("mask"))
            {
                cosmetics.Mask = info.itemdefid;
            }
            else if (tipo.Contains("pants"))
            {
                cosmetics.Pants = info.itemdefid;
            }
            else if (tipo.Contains("shirt"))
            {
                cosmetics.Shirt = info.itemdefid;
            }
            else if (tipo.Contains("vest"))
            {
                cosmetics.Vest = info.itemdefid;
            }
            else if(tipo.Contains("skin"))
            {
                if(cosmetics.Skins == null)
                {
                    cosmetics.Skins = new List<int>();
                }
                cosmetics.Skins.Add(info.itemdefid);
            }
            UnturnedChat.Say(player, AdvancedCosmetics.Instance.Translate("AddCos", info.name), AdvancedCosmetics.Instance.MessageColor);
        }
    }
}
