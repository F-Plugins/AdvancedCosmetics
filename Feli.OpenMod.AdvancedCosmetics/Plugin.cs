using System;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

[assembly: PluginMetadata("Feli.AdvancedCosmetics", 
    DisplayName = "Advanced Cosmetics",
    Author = "Feli",
    Description = "Allows your users to setup their cosmetics with commands",
    Website = "https://discord.gg/4FF2548"
)]

namespace Feli.OpenMod.AdvancedCosmetics
{
    public class Plugin : OpenModUnturnedPlugin
    {
        public Plugin(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
