using System.Collections.Generic;
using DModeRemastered.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Accessories
{
    public class MadnessSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Sigil");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Monsters have an additional core");
            TooltipLine tt2 = new TooltipLine(Mod, "DMode-Use-2", "Monsters don't leave any experience behind");
            tooltips.Add(tt1);
            tooltips.Add(tt2);
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1;

            Item.width = 28;
            Item.height = 30;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DModePlayer>().MadnessSigil = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Marble, 25);
            recipe.AddIngredient(ModContent.ItemType<ConcentratedEvil>(), 25);
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Marble, 25);
            recipe.AddIngredient(ModContent.ItemType<ConcentratedEvil>(), 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

    }
}
