using System.Collections.Generic;
using System.Linq;
using Feli.RocketMod.AdvancedCosmetics.Models;
using Rocket.API;

namespace Feli.RocketMod.AdvancedCosmetics.Storage
{
    public class PlayersCosmeticsStore : IDefaultable
    {
        public List<PlayerCosmetics> PlayersCosmetics { get; set; }

        public void LoadDefaults()
        {
            PlayersCosmetics = [];
        }

        public PlayerCosmetics GetOrAddCosmetics(ulong playerId)
        {
            var cosmetics = PlayersCosmetics.FirstOrDefault(x => x.PlayerId == playerId);

            if (cosmetics == null)
            {
                PlayersCosmetics.Add(new PlayerCosmetics(playerId));
                cosmetics = PlayersCosmetics.FirstOrDefault(x => x.PlayerId == playerId);
            }
            
            return cosmetics;
        }
    }
}