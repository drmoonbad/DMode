using DMode.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Items.Materials
{
    public class MonsterCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monster Core");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.maxStack = 2000;

            Item.width = 14;
            Item.height = 14;

            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            //Tier 7 Conversion:
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 60);
            recipe.Register();
            

            //Tier 1 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 25);
            recipe.Register();


            //Tier 2 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 30);
            recipe.Register();


            //Tier 3 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 35);
            recipe.Register();


            //Tier 4 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 40);
            recipe.Register();


            //Tier 5 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 45);
            recipe.Register();


            //Tier 6 Conversion:
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonstrousExperience>(), 100);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 50);
            recipe.Register();

        }
    }
}
