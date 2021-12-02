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
using SDG.Provider;
using SDG.Unturned;

namespace Feli.OpenMod.AdvancedCosmetics.Commands
{
    [Command("customcosmetic")]
    [CommandDescription("Sets your custom cosmetics")]
    [CommandSyntax("<cosmeticId> | <cosmeticName> [--force (reconnects you to the server so the changes get applied)]")]
    [CommandActor(typeof(UnturnedUser))]
    public class CustomCosmetic : UnturnedCommand
    {
        private readonly IPlayerCosmeticsStore _playerCosmeticsStore;
        private readonly IStringLocalizer _stringLocalizer;
        
        public CustomCosmetic(
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
            
            if (Context.Parameters.Length < 1)
                throw new CommandWrongUsageException(Context);

            var search = await Context.Parameters.GetAsync<string>(0);

            var econInfos = TempSteamworksEconomy.econInfo;
            
            var cosmetic = int.TryParse(search, out int searchId) ? econInfos.FirstOrDefault(x => x.itemdefid == searchId) : econInfos.FirstOrDefault(x => x.name.ToLower().Contains(search.ToLower()));

            if (cosmetic == null)
                throw new UserFriendlyException(_stringLocalizer["CustomCosmetic:NotFound", new
                {
                    Name = search
                }]);

            var cosmetics = _playerCosmeticsStore.GetOrAddCosmetics(user.SteamId.m_SteamID);
            
            cosmetics.AddCosmetic(cosmetic);

            if (Context.Parameters.Any(x => x == "--force"))
            {
                await UniTask.SwitchToMainThread();
                user.Player.Player.sendRelayToServer(Provider.ip, Provider.port, Provider.serverPassword, false);
            }
            else
            {
                await user.PrintMessageAsync(_stringLocalizer["CustomCosmetic:Success", new
                {
                    Name = cosmetic.name
                }], Color.White, true, string.Empty);
            }
        }
    }
}