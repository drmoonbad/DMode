using System.Collections.Generic;
using DModeRemastered.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Accessories
{
    public class ChainsOfAbaddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chains of Abaddon");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Receive 25% more damage from enemies");
            TooltipLine tt2 = new TooltipLine(Mod, "DMode-Use-2", "Deal 25% more damage to enemies");
            tooltips.Add(tt1);
            tooltips.Add(tt2);
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1;

            Item.width = 32;
            Item.height = 32;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<GenericDamageClass>() += 0.25f;
            player.endurance -= 0.25f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle, 2);
            recipe.AddIngredient(ModContent.ItemType<ConcentratedEvil>(), 25);
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle, 2);
            recipe.AddIngredient(ModContent.ItemType<ConcentratedEvil>(), 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
