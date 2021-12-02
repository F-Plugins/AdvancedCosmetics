using System.Collections.Generic;
using Feli.OpenMod.AdvancedCosmetics.Models;
using OpenMod.API.Ioc;

namespace Feli.OpenMod.AdvancedCosmetics.API.Services
{
    [Service]
    public interface IPlayerCosmeticsStore
    {
        List<PlayerCosmetics> PlayersCosmetics { get; set; }

        PlayerCosmetics GetOrAddCosmetics(ulong playerId);

        PlayerCosmetics GetCosmetics(ulong playerId);
    }
}