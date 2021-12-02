using System;
using System.Collections.Generic;
using System.Linq;
using Feli.OpenMod.AdvancedCosmetics.API.Services;
using Feli.OpenMod.AdvancedCosmetics.Models;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Persistence;
using OpenMod.Core.Helpers;

namespace Feli.OpenMod.AdvancedCosmetics.Storage
{
    [PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton)]
    public class PlayerCosmeticsStore : IPlayerCosmeticsStore, IDisposable
    {
        public List<PlayerCosmetics> PlayersCosmetics { get; set; }

        private readonly IDataStore _dataStore;
        
        public PlayerCosmeticsStore(IDataStore dataStore)
        {
            _dataStore = dataStore;

            AsyncHelper.RunSync(async () =>
            {
                if (!await _dataStore.ExistsAsync("cosmetics"))
                {
                    await _dataStore.SaveAsync("cosmetics", new List<PlayerCosmetics>());
                }

                PlayersCosmetics = await _dataStore.LoadAsync<List<PlayerCosmetics>>("cosmetics");
            });
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

        public PlayerCosmetics GetCosmetics(ulong playerId)
        {
            return PlayersCosmetics.FirstOrDefault(x => x.PlayerId == playerId);
        }
        
        public void Dispose()
        {
            AsyncHelper.RunSync(async () =>
            {
                await _dataStore.SaveAsync("cosmetics", PlayersCosmetics);
            });
        }
    }
}