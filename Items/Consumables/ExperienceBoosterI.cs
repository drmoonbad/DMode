using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using DMode.Items.Materials;

namespace DMode.Items.Consumables
{
	public class ExperienceBoosterI : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Experience Booster I");
        }

        public override void SetDefaults()
		{
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 20;
            Item.width = 10;
            Item.height = 18;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Right Click to consume");
            TooltipLine tt2 = new TooltipLine(Mod, "DMode-Use-2", "Increases the Skill Experience of the first item in the hotbar by 100");
            tooltips.Add(tt1);
            tooltips.Add(tt2);
        }

        public override bool CanRightClick()
        {
            Item target = Main.LocalPlayer.inventory[0];
            return Util.CanHaveAnySystem(target);
        }

        public override void RightClick(Player player)
        {
            Skill.EarnExp(player.inventory[0], 100);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}
