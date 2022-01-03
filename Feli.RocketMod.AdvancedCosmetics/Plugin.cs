using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Feli.RocketMod.AdvancedCosmetics.Storage;
using Rocket.API.Collections;
using Rocket.Core.Assets;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Permissions;
using SDG.Provider;
using SDG.Unturned;
using Steamworks;

namespace Feli.RocketMod.AdvancedCosmetics
{
    public class Plugin : RocketPlugin
    {
        public static Plugin Instance { get; set; }
        public XMLFileAsset<PlayersCosmeticsStore> CosmeticsStore { get; set; }

        public List<UnturnedEconInfo> EconInfos => TempSteamworksEconomy.econInfo;
        
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"RemoveCosmetics:Fail", "You haven't set up any custom cosmetics yet"},
            {"RemoveCosmetics:Success", "Successfully removed all your cosmetics. Reconnect to the server to see the changes"},
            {"CustomCosmetic:Usage", "Correct command usage: /customcosmetics <cosmeticId> | /customcosmetics <cosmeticName> [--force (reconnects you to the server so the changes get applied)]"},
            {"CustomCosmetic:NotFound", "Cosmetic with id or name {0} was not found"},
            {"CustomCosmetic:Success", "Successfully added the cosmetic {0}"}
        };

        protected override void Load()
        {
            Instance = this;
            CosmeticsStore = new XMLFileAsset<PlayersCosmeticsStore>(Path.Combine(Directory, $"{Name}.cosmetics.xml"));
            CosmeticsStore.Load();
            UnturnedPermissions.OnJoinRequested += OnJoinRequested;
            SaveManager.onPreSave += OnPreSave;
            Logger.Log($"Advanced Cosmetics v{Assembly.GetName().Version} has been loaded");
            Logger.Log("Do you want more cool plugins? Join now: https://discord.gg/4FF2548 !");
        }

        private void OnPreSave()
        {
            CosmeticsStore.Save();
        }

        private void OnJoinRequested(CSteamID player, ref ESteamRejection? rejectionreason)
        {
            var cosmetics = CosmeticsStore.Instance.PlayersCosmetics.FirstOrDefault(x => x.PlayerId == player.m_SteamID);
            
            if(cosmetics == null)
                return;
            
            var pending = Provider.pending.FirstOrDefault(x => x.playerID.steamID == player);
            
            cosmetics.ApplyCosmetics(pending);
        }

        protected override void Unload()
        {
            Instance = null;
            CosmeticsStore.Save();
            UnturnedPermissions.OnJoinRequested -= OnJoinRequested;
            SaveManager.onPreSave -= OnPreSave;
            Logger.Log($"Advanced Cosmetics v{Assembly.GetName().Version} has been unloaded");
        }
    }
}