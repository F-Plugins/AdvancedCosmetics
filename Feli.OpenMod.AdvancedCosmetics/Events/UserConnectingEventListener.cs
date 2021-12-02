using System.Linq;
using System.Threading.Tasks;
using Feli.OpenMod.AdvancedCosmetics.API.Services;
using OpenMod.API.Eventing;
using OpenMod.Core.Users.Events;
using SDG.Unturned;

namespace Feli.OpenMod.AdvancedCosmetics.Events
{
    public class UserConnectingEventListener : IEventListener<IUserConnectingEvent>
    {
        private readonly IPlayerCosmeticsStore _playerCosmeticsStore;
        
        public UserConnectingEventListener(IPlayerCosmeticsStore playerCosmeticsStore)
        {
            _playerCosmeticsStore = playerCosmeticsStore;
        }
        
        public Task HandleEventAsync(object sender, IUserConnectingEvent @event)
        {
            var id = ulong.Parse(@event.User.Id);

            var cosmetics = _playerCosmeticsStore.GetCosmetics(id);
            
            if(cosmetics == null)
                return Task.CompletedTask;
            
            var pending = Provider.pending.FirstOrDefault(x => x.playerID.steamID.m_SteamID == id);
            
            cosmetics.ApplyCosmetics(pending);

            return Task.CompletedTask;
        }
    }
}