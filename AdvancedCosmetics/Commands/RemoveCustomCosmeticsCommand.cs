using Microsoft.Extensions.Localization;
using OpenMod.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Commands
{
    [Command("removecustomcosmetics", Priority = OpenMod.API.Prioritization.Priority.High)]
    [CommandDescription("A command to remove the custom cosmetics")]
    [CommandSyntax("/removecustomcosmetics")]
    public class RemoveCustomCosmeticsCommand : Command
    {
        private readonly AdvancedCosmetics m_plugin;
        private readonly IStringLocalizer m_stringLocalizer;

        public RemoveCustomCosmeticsCommand(
            AdvancedCosmetics advancedCosmetics,
            IStringLocalizer stringLocalizer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_plugin = advancedCosmetics;
            m_stringLocalizer = stringLocalizer;
        }

        protected override async Task OnExecuteAsync()
        {
            if (m_plugin.Cosmetics.ContainsKey(Context.Actor.Id))
            {
                m_plugin.Cosmetics.Remove(Context.Actor.Id);
                await Context.Actor.PrintMessageAsync(m_stringLocalizer["plugin_translations:RemovedCos"]);
            }
            else
            {
                await Context.Actor.PrintMessageAsync(m_stringLocalizer["plugin_translations:NotSetUp"]);
            }
        }
    }
}
