using Microsoft.Extensions.Localization;
using OpenMod.API.Commands;
using OpenMod.Core.Commands;
using SDG.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Commands
{
    [Command("customcosmetic", Priority = OpenMod.API.Prioritization.Priority.High)]
    [CommandDescription("A command to get a custom cosmetic")]
    [CommandSyntax("/customcosmetic <cosmeticId> | /customcosmetic <cosmeticName>")]
    public class CustomCosmeticCommand : Command
    {
        private readonly AdvancedCosmetics m_plugin;
        private readonly IStringLocalizer m_stringLocalizer;

        public CustomCosmeticCommand(
            AdvancedCosmetics advancedCosmetics, 
            IStringLocalizer stringLocalizer, 
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_plugin = advancedCosmetics;
            m_stringLocalizer = stringLocalizer;
        }

        protected override async Task OnExecuteAsync()
        {
            if(Context.Parameters.Length < 1)
            {
                await Context.Actor.PrintMessageAsync("Correct Usage: /customcosmetic <cosmeticId> | /customcosmetic <cosmeticName>");
            }
            else if(int.TryParse(Context.Parameters[0], out int SkinId))
            {
                var find = m_plugin.EconInfos.FirstOrDefault(x => x.itemdefid == SkinId);
                if (find != null)
                {
                    await AddCosmetics(Context.Actor, find);
                }
                else
                {
                    await Context.Actor.PrintMessageAsync(m_stringLocalizer["plugin_translations:NotFound", new { Name = SkinId }]);
                }
            }
            else
            {
                var skin = Context.Parameters[0];
                var find = m_plugin.EconInfos.FirstOrDefault(x => x.itemdefid == SkinId);
                if (find != null)
                {
                    await AddCosmetics(Context.Actor, find);
                }
                else
                {
                    await Context.Actor.PrintMessageAsync(m_stringLocalizer["plugin_translations:NotFound", new { Name = skin }]);
                }
            }
        }

        private Task AddCosmetics(ICommandActor actor, UnturnedEconInfo info)
        {
            if (!m_plugin.Cosmetics.ContainsKey(actor.Id))
            {
                m_plugin.Cosmetics.Add(actor.Id, new Models.Cosmetic());
            }

            var cosmetics = m_plugin.Cosmetics[actor.Id];
            var tipo = info.type.ToLower();
            if (tipo.Contains("backpack"))
            {
                cosmetics.Backpack = info.itemdefid;
            }
            else if (tipo.Contains("glasses"))
            {
                cosmetics.Glass = info.itemdefid;
            }
            else if (tipo.Contains("hat"))
            {
                cosmetics.Hat = info.itemdefid;
            }
            else if (tipo.Contains("mask"))
            {
                cosmetics.Mask = info.itemdefid;
            }
            else if (tipo.Contains("pants"))
            {
                cosmetics.Pants = info.itemdefid;
            }
            else if (tipo.Contains("shirt"))
            {
                cosmetics.Shirt = info.itemdefid;
            }
            else if (tipo.Contains("vest"))
            {
                cosmetics.Vest = info.itemdefid;
            }
            else if (tipo.Contains("skin"))
            {
                if (cosmetics.Skins == null)
                {
                    cosmetics.Skins = new List<int>();
                }
                cosmetics.Skins.Add(info.itemdefid);
            }

            actor.PrintMessageAsync(m_stringLocalizer["plugin_translations:AddCos", new { Name = info.name }]);
            return Task.CompletedTask;
        }
    }
}
