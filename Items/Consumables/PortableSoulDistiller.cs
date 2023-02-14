using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Consumables
{
	public class PortableSoulDistiller : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portable Soul Distiller");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Souls permanently have 20% more value");
            tooltips.Add(tt1);
        }

        public override void SetDefaults()
		{
            Item.rare = ItemRarityID.Orange;

            Item.maxStack = 1;

            Item.width = 24;
            Item.height = 24;

            Item.consumable = true;

            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
        }

        public override bool? UseItem(Player player)
        {
            DModePlayer dModePlayer = player.GetModPlayer<DModePlayer>();
            if (dModePlayer.soulValueBonus < 1.2f && Main.masterMode) 
            {
                dModePlayer.soulValueBonus = 1.2f;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 4);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddIngredient(ModContent.ItemType<SoulBooster>(), 5);
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bottle, 4);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddIngredient(ModContent.ItemType<SoulBooster>(), 5);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
