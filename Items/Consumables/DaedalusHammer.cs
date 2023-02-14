using System;
using System.Collections.Generic;
using DModeRemastered.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Consumables
{
    public class DaedalusHammer : ModItem
    {
        private int level = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daedalus' Hammer");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt1 = new TooltipLine(Mod, "DMode-Use-1", "Right click to consume");
            TooltipLine tt2 = new TooltipLine(Mod, "DMode-Use-2",
                $"Upgrades the first item in the hotbar by {Math.Pow(10, level - 1)} Quality Level(s)");
            tooltips.Add(tt1);
            tooltips.Add(tt2);
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 100;
            Item.width = 32;
            Item.height = 32;
        }

        public override bool CanRightClick()
        {
            Item target = Main.LocalPlayer.inventory[0];
            return Util.CanHaveAnySystem(target);
        }

        public override void RightClick(Player player)
        {
            Item target = Main.LocalPlayer.inventory[0];
            target.GetGlobalItem<Quality>().Level++;

            Util.NewCombatText(player, new Color(255, 255, 0), "Quality Increased!", false, false, 1.25f, 90);
            Util.DefaultDustEffect(player.Center, 159, 5f);
            Util.NewSoundFX(SoundID.Item4, 0.3f, player.Center);
        }

        public override void AddRecipes()
        {
            //Multiple recipes with different cost efficiencies:
            /*
             * The efficiency depends on the items used.
             */

            //Tier 1:
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();

            //Tier 2:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 2);
            recipe.Register();

            //Tier 3:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 2);
            recipe.Register();

            //Tier 4:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 3);
            recipe.Register();

            //Tier 5:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 4);
            recipe.Register();

            //Tier 6:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 5);
            recipe.Register();

            //Tier 7:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 6);
            recipe.Register();

            //Tier 8:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 25);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 7);
            recipe.Register();
        }
    }
}