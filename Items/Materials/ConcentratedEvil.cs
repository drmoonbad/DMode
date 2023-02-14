using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Materials
{
    public class ConcentratedEvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concentrated Evil");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1000;

            Item.width = 24;
            Item.height = 26;
        }
    }
}
