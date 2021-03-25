using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string MessageColor;
        public void LoadDefaults()
        {
            MessageColor = "magenta";
        }
    }
}
