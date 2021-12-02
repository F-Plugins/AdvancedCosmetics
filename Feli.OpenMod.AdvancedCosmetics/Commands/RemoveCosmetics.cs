using System;
using System.Drawing;
using System.Linq;
using Cysharp.Threading.Tasks;
using Feli.OpenMod.AdvancedCosmetics.API.Services;
using Microsoft.Extensions.Localization;
using OpenMod.API.Commands;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;

namespace Feli.OpenMod.AdvancedCosmetics.Commands
{
    [Command("removecosmetics")]
    [CommandDescription("Removes all your custom cosmetics")]
    [CommandSyntax("[--force (reconnects you to the server so the changes get applied)]")]
    [CommandActor(typeof(UnturnedUser))]
    public class RemoveCosmetics : UnturnedCommand
    {
        private readonly IPlayerCosmeticsStore _playerCosmeticsStore;
        private readonly IStringLocalizer _stringLocalizer;
        
        public RemoveCosmetics(
            IStringLocalizer stringLocalizer,
            IPlayerCosmeticsStore playerCosmeticsStore,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _playerCosmeticsStore = playerCosmeticsStore;
            _stringLocalizer = stringLocalizer;
        }

        protected override async UniTask OnExecuteAsync()
        {
            var user = (UnturnedUser) Context.Actor;

            var cosmetics = _playerCosmeticsStore.GetCosmetics(user.SteamId.m_SteamID);

            if (cosmetics == null)
            {
                throw new UserFriendlyException(_stringLocalizer["RemoveCosmetics:Fail"]);
            }

            _playerCosmeticsStore.PlayersCosmetics.Remove(cosmetics);

            if (Context.Parameters.Any(x => x == "--force"))
            {
                await UniTask.SwitchToMainThread();
                user.Player.Player.sendRelayToServer(Provider.ip, Provider.port, Provider.serverPassword, false);
            }
            else
            {
                await user.PrintMessageAsync(_stringLocalizer["RemoveCosmetics:Success"], Color.White, true,
                    string.Empty);
            }
        }
    }
}