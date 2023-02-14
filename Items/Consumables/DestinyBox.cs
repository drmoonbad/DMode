using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Consumables
{
    public class DestinyBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destiny Box");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1000;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 28;
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine howToUse = new TooltipLine(Mod, "howToUse", "Right click to open");
            tooltips.Add(howToUse);
        }

        public override void RightClick(Player player)
        {
            var stackSize = this.Item.stack;
            for (int i = 0; i < stackSize; i++)
                Util.OpenDestinyBox(player);
            this.Item.stack = 0;
        }

        public override bool CanRightClick()
        {
            return true;
        }
    }
}