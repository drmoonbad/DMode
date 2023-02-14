using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Materials
{
    public class MonstrousExperience : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monstrous Experience");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.maxStack = 5000;

            Item.width = 14;
            Item.height = 14;

            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightGreen.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
