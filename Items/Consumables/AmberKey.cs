using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Items.Consumables
{
    public class AmberKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Amber Key");
            Tooltip.SetDefault("Opens 50 Destiny Boxes over a period of 10 seconds. This effect does not stack.");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 10;

            Item.width = 28;
            Item.height = 30;
        }

        public override bool CanRightClick()
        {
            Player player = Main.LocalPlayer;
            int boxes = 0;
            for (int i = 0; i < 58; i++)
            {
                Item box = player.inventory[i];
                if (box.netID == Mod.Find<ModItem>("DestinyBox").Type)
                {
                    boxes += box.stack;
                }
            }

            if (boxes >= 200 && !player.GetModPlayer<DModePlayer>().AmberKeyEffect) { return true; }
            else { return false; }
        }

        public override void RightClick(Player player)
        {
            player.GetModPlayer<DModePlayer>().AmberKeyEffect = true;

            int remaining = 50;
            for (int i = 0; i < 58; i++)
            {
                Item box = player.inventory[i];
                if (box.netID == Mod.Find<ModItem>("DestinyBox").Type)
                {
                    if (remaining > 0)
                    {
                        if (box.stack >= remaining)
                        {
                            box.stack -= remaining;
                            remaining = 0;
                        }
                        else if (box.stack < remaining)
                        {
                            box.stack = 0;
                            remaining -= box.stack;
                        }
                    }
                    else if (remaining == 0)
                    {
                        break;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Amber, 5);
            recipe.AddIngredient(ItemID.Obsidian, 30);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}
