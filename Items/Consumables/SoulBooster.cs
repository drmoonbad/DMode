using DModeRemastered.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Consumables
{
    public class SoulBooster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Booster");
            Tooltip.SetDefault("Right-Click to release an unknown Soul.");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 100;

            Item.width = 10;
            Item.height = 18;

            Item.scale = 0.5f;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OnCraft(Recipe recipe)
        {
            Player player = Main.LocalPlayer;
            if (player.active && player.HeldItem.netID == Mod.Find<ModItem>("SoulBooster").Type)
            {
                string pronoum = "";
                if (player.Male) { pronoum = "his"; }
                else if (!player.Male) { pronoum = "her"; }

                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " has shattered " + pronoum + " own Soul."), 30 + (int)(player.statDefense * 0.75), -player.direction, false, false, false, 60);
            }
        }

        public override void RightClick(Player player)
        {
            int Soul = Projectile.NewProjectile(player.GetSource_Misc("DMODE Soul Booster"), player.position, new Vector2(0, 0), Mod.Find<ModProjectile>("Soul").Type, 0, 0, Item.playerIndexTheItemIsReservedFor);
            Main.projectile[Soul].ai[1] += 20 + 0.05f * player.statLifeMax2 + 5 * Util.GetWorldLevel();
            Main.projectile[Soul].GetGlobalProjectile<DModeProjectile>().SoulOwner = player.whoAmI;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
