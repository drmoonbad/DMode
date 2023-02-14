using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Consumables
{
	public class DivineHammer : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Divine Hammer");
        }

        public override void SetDefaults()
		{
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 100;
            Item.width = 32;
            Item.height = 32;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Right click to consume");
            TooltipLine tt2 = new TooltipLine(Mod, "DMode-Use-2", "Upgrades the first item in the hotbar to the next 10th Quality Level");
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
            Item target = Main.LocalPlayer.inventory[0];
            target.GetGlobalItem<Quality>().Level+= 10;

            Util.NewCombatText(player, new Color(255, 255, 0), "Quality Increased!", false, false, 1.25f, 90);
            Util.DefaultDustEffect(player.Center, 159, 10f);
            Util.NewSoundFX(SoundID.Item4, 0.3f, player.Center);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaedalusHammer>(), 10);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}
