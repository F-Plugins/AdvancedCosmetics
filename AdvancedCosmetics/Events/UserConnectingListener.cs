using OpenMod.API.Eventing;
using OpenMod.Core.Eventing;
using OpenMod.Core.Users.Events;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Events
{
    public class UserConnectingListener : IEventListener<IUserConnectingEvent>
    {
        private readonly AdvancedCosmetics m_plugin;
        public UserConnectingListener(AdvancedCosmetics advancedCosmetics, IServiceProvider serviceProvider)
        {
            m_plugin = advancedCosmetics;
        }

        public Task HandleEventAsync(object? sender, IUserConnectingEvent @event)
        {
            var pending = Provider.pending.FirstOrDefault(x => x.playerID.steamID.ToString() == @event.User.Id);

            if(pending != null && m_plugin.Cosmetics.ContainsKey(@event.User.Id))
            {
                var cosmetics = m_plugin.Cosmetics[@event.User.Id];
                if (cosmetics != null)
                {
                    pending.backpackItem = cosmetics.Backpack;
                    pending.glassesItem = cosmetics.Glass;
                    pending.hatItem = cosmetics.Hat;
                    pending.maskItem = cosmetics.Mask;
                    pending.pantsItem = cosmetics.Pants;
                    pending.shirtItem = cosmetics.Shirt;
                    pending.vestItem = cosmetics.Vest;
                    if (cosmetics.Skins != null)
                    {
                        pending.skinItems = cosmetics.Skins.ToArray();
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
