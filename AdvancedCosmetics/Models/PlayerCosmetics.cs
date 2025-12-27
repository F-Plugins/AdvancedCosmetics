using System.Collections.Generic;
using System.Linq;
using SDG.Provider;
using SDG.Unturned;

namespace RestoreMonarchy.AdvancedCosmetics.Models
{
    public class PlayerCosmetics
    {
        public ulong PlayerId { get; set; }
        public int Hat { get; set; }
        public int Mask { get; set; }
        public int Glasses { get; set; }
        public int Backpack { get; set; }
        public int Shirt { get; set; }
        public int Pants { get; set; }
        public int Vest { get; set; }
        public List<int> SkinItems { get; }

        public PlayerCosmetics()
        {
            SkinItems = [];
        }

        public PlayerCosmetics(ulong playerId)
        {
            PlayerId = playerId;
            SkinItems = [];
        }

        public void ApplyCosmetics(SteamPending pending)
        {
            pending.hatItem = Hat == 0 ? pending.hatItem : Hat;
            pending.maskItem = Mask == 0 ? pending.maskItem : Mask;
            pending.glassesItem = Glasses == 0 ? pending.glassesItem : Glasses;
            pending.backpackItem = Backpack == 0 ? pending.backpackItem : Backpack;
            pending.shirtItem = Shirt == 0 ? pending.shirtItem : Shirt;
            pending.pantsItem = Pants == 0 ? pending.pantsItem : Pants;
            pending.vestItem = Vest == 0 ? pending.vestItem : Vest;
            pending.skinItems = pending.skinItems
                .Concat(SkinItems)
                .Reverse()
                .ToArray();
        }

        public void AddCosmetic(UnturnedEconInfo info)
        {
            var type = info.display_type.ToLower();
            if (type.Contains("backpack"))
            {
                Backpack = info.itemdefid;
            }
            else if (type.Contains("glasses"))
            {
                Glasses = info.itemdefid;
            }
            else if (type.Contains("hat"))
            {
                Hat = info.itemdefid;
            }
            else if (type.Contains("mask"))
            {
                Mask = info.itemdefid;
            }
            else if (type.Contains("pants"))
            {
                Pants = info.itemdefid;
            }
            else if (type.Contains("shirt"))
            {
                Shirt = info.itemdefid;
            }
            else if (type.Contains("vest"))
            {
                Vest = info.itemdefid;
            }
            else if(type.Contains("skin"))
            {
                SkinItems.Add(info.itemdefid);
            }
        }
    }
}
