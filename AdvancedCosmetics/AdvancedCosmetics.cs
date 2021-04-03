using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using System.Collections.Generic;
using Steamworks;
using AdvancedCosmetics.Models;
using SDG.Provider;
using System.IO;
using System.Drawing;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("Feli.AdvancedCosmetics", DisplayName = "AdvancedCosmetics", Website = "unturnedstore.com", Author = "Feli")]
namespace AdvancedCosmetics
{
    public class AdvancedCosmetics : OpenModUnturnedPlugin
    {
        public Dictionary<string, Cosmetic> Cosmetics = new Dictionary<string, Cosmetic>();
        public List<UnturnedEconInfo> EconInfos = new List<UnturnedEconInfo>();


        private readonly ILogger<AdvancedCosmetics> m_Logger;

        public AdvancedCosmetics(
            ILogger<AdvancedCosmetics> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Logger = logger;
        }

        protected override UniTask OnLoadAsync()
        {
            var dir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(WorkingDirectory).FullName).FullName).FullName).FullName).FullName + @"\EconInfo.json";
            EconInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UnturnedEconInfo>>(File.ReadAllText(dir));
            m_Logger.LogInformation("Advanced Cosmetics 1.0.0 has been loaded");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation("Advanced Cosmetics 1.0.0 has been unloaded");
            return UniTask.CompletedTask;
        }
    }
}
