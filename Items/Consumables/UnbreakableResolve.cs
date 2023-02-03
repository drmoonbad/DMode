using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Items.Consumables
{
	public class UnbreakableResolve : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unbreakable Resolve");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Style permanently accumulates 20% faster");
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
            if (dModePlayer.styleSpeedBonus < 1.2f) 
            {
                dModePlayer.styleSpeedBonus = 1.2f;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.KingSlimeMasterTrophy);
            recipe.AddIngredient(ItemID.EyeofCthulhuMasterTrophy);
            recipe.AddIngredient(ItemID.EaterofWorldsMasterTrophy);
            recipe.AddIngredient(ItemID.QueenBeeMasterTrophy);
            recipe.AddIngredient(ItemID.SkeletronMasterTrophy);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.KingSlimeMasterTrophy);
            recipe.AddIngredient(ItemID.EyeofCthulhuMasterTrophy);
            recipe.AddIngredient(ItemID.BrainofCthulhuMasterTrophy);
            recipe.AddIngredient(ItemID.QueenBeeMasterTrophy);
            recipe.AddIngredient(ItemID.SkeletronMasterTrophy);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}
