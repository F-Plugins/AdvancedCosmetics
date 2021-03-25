using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCosmetics.Models
{
    public class Cosmetic
    {
        public int Backpack { get; set; }
        public int Glass { get; set; }
        public int Hat { get; set; }
        public int Mask { get; set; }
        public int Pants { get; set; }
        public int Shirt { get; set; }
        public int Vest { get; set; }
        public List<int> Skins { get; set; }
    }
}
