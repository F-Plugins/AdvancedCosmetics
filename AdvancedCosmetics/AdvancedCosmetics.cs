using AdvancedCosmetics.Models;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Permissions;
using SDG.Provider;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace AdvancedCosmetics
{
    public class AdvancedCosmetics : RocketPlugin<Configuration>
    {
        public static AdvancedCosmetics Instance { get; private set; }
        public Color MessageColor { get; private set; }
        public List<UnturnedEconInfo> EconInfos { get; private set;}
        public Dictionary<CSteamID, Cosmetic> Cosmetics { get; set; }
        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.green);
            Cosmetics = new Dictionary<CSteamID, Cosmetic>();
            string dir = "";
            if (Configuration.Instance.FileInPluginFolder)
            {
                dir = $@"{System.Environment.CurrentDirectory}/Plugins/AdvancedCosmetics/EconInfo.json";
            }
            else
            {
                dir = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName).FullName + @"/EconInfo.json";
            }
            EconInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UnturnedEconInfo>>(File.ReadAllText(dir));
            UnturnedPermissions.OnJoinRequested += UnturnedPermissions_OnJoinRequested;
            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded");
        }

        protected override void Unload()
        {
            UnturnedPermissions.OnJoinRequested -= UnturnedPermissions_OnJoinRequested;
            Logger.Log($"{Name} {Assembly.GetName().Version} has been unloaded");
        }

        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "NotSetUp", "You didnt setup any custom cosmetic yet !" },
            { "RemovedCos", "Sucssesfully removed your custom cosmetics !"},
            { "AddCos", "Sucssesfully added a {0} to your cosmetics !"},
            { "NotFound", "Cosmetic with the id or name {0} not found !"}
        };

        private void UnturnedPermissions_OnJoinRequested(Steamworks.CSteamID player, ref SDG.Unturned.ESteamRejection? rejectionReason)
        {
            var pending = Provider.pending.FirstOrDefault(x => x.playerID.steamID == player);
            if (pending != null && Cosmetics.ContainsKey(player))
            {
                var cosmetics = Cosmetics[player];
                if(cosmetics != null)
                {
                    pending.backpackItem = cosmetics.Backpack;
                    pending.glassesItem = cosmetics.Glass;
                    pending.hatItem = cosmetics.Hat;
                    pending.maskItem = cosmetics.Mask;
                    pending.pantsItem = cosmetics.Pants;
                    pending.shirtItem = cosmetics.Shirt;
                    pending.vestItem = cosmetics.Vest;
                    if(cosmetics.Skins != null)
                    {
                        pending.skinItems = cosmetics.Skins.ToArray();
                    }
                }
            }   
        }
    }
}
