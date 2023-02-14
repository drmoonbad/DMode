using System.Collections.Generic;
using DModeRemastered.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Items.Tools
{
    public class RingOfConvergence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Convergence");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1;

            Item.width = 28;
            Item.height = 20;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;

            Item.mana = 100;

            Item.consumable = false;

            Item.UseSound = SoundID.Item4;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string text = "Exchanges some experience from weak attributes to the most stronger attribute.";
            TooltipLine description = new TooltipLine(Mod, "description", text);
            description.OverrideColor = Color.White;
            tooltips.Add(description);
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int Str = player.GetModPlayer<DModePlayer>().Strength;
            int Dex = player.GetModPlayer<DModePlayer>().Dexterity;
            int Min = player.GetModPlayer<DModePlayer>().Mind;
            int Spi = player.GetModPlayer<DModePlayer>().Spirit;

            int crystal_upgrades = Str + Dex + Min + Spi;

            int StrEXP = player.GetModPlayer<DModePlayer>().strengthExp;
            int DexEXP = player.GetModPlayer<DModePlayer>().dexterityExp;
            int MinEXP = player.GetModPlayer<DModePlayer>().mindExp;
            int SpiEXP = player.GetModPlayer<DModePlayer>().spiritExp;

            int[] atts = new int[] { Str, Dex, Min, Spi };
            int[] attsEXP = new int[] { StrEXP, DexEXP, MinEXP, SpiEXP};

            string major_att = "none";

            atts.SetValue(0, 0);
            if (Util.GreaterThanAll(Str, atts) ) { major_att = "Strength"; }
            atts.SetValue(Str, 0);

            atts.SetValue(0, 1);
            if (Util.GreaterThanAll(Dex, atts)) { major_att = "Dexterity"; }
            atts.SetValue(Dex, 1);

            atts.SetValue(0, 2);
            if (Util.GreaterThanAll(Min, atts)) { major_att = "Mind"; }
            atts.SetValue(Min, 2);

            atts.SetValue(0, 3);
            if (Util.GreaterThanAll(Spi, atts)) { major_att = "Spirit"; }
            atts.SetValue(Spi, 3);

            if (major_att == "none")
            {
                attsEXP.SetValue(0, 0);
                if (Util.GreaterThanAll(StrEXP, attsEXP)) { major_att = "Strength"; }
                attsEXP.SetValue(StrEXP, 0);

                attsEXP.SetValue(0, 1);
                if (Util.GreaterThanAll(DexEXP, attsEXP)) { major_att = "Dexterity"; }
                attsEXP.SetValue(DexEXP, 1);

                attsEXP.SetValue(0, 2);
                if (Util.GreaterThanAll(MinEXP, attsEXP)) { major_att = "Mind"; }
                attsEXP.SetValue(MinEXP, 2);

                attsEXP.SetValue(0, 3);
                if (Util.GreaterThanAll(SpiEXP, attsEXP)) { major_att = "Spirit"; }
                attsEXP.SetValue(SpiEXP, 3);
            }

            if (major_att == "none")
            {
                Main.NewText("You do not have a main attribute.", Color.Red);
            }
            else
            {
                switch (major_att)
                {
                    case "Strength":
                        player.GetModPlayer<DModePlayer>().strengthExp +=
                            (int)(0.8 * player.GetModPlayer<DModePlayer>().dexterityExp +
                            0.8 * player.GetModPlayer<DModePlayer>().mindExp +
                            0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        player.GetModPlayer<DModePlayer>().dexterityExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().dexterityExp);
                        player.GetModPlayer<DModePlayer>().mindExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().mindExp);
                        player.GetModPlayer<DModePlayer>().spiritExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        while (player.GetModPlayer<DModePlayer>().strengthExp >= player.GetModPlayer<DModePlayer>().strengthMaxExp)
                        {
                            player.GetModPlayer<DModePlayer>().strengthExp = player.GetModPlayer<DModePlayer>().strengthExp - player.GetModPlayer<DModePlayer>().strengthMaxExp;
                            player.GetModPlayer<DModePlayer>().Strength += 1;
                            player.GetModPlayer<DModePlayer>().strengthMaxExp = (int)(80 * (10 + crystal_upgrades - player.GetModPlayer<DModePlayer>().Strength) * (1 + 0.075 * player.GetModPlayer<DModePlayer>().Strength));
                        }

                        break;

                    case "Dexterity":
                        player.GetModPlayer<DModePlayer>().dexterityExp +=
                            (int)(0.8 * player.GetModPlayer<DModePlayer>().strengthExp +
                            0.8 * player.GetModPlayer<DModePlayer>().mindExp +
                            0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        player.GetModPlayer<DModePlayer>().strengthExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().strengthExp);
                        player.GetModPlayer<DModePlayer>().mindExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().mindExp);
                        player.GetModPlayer<DModePlayer>().spiritExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        while (player.GetModPlayer<DModePlayer>().dexterityExp >= player.GetModPlayer<DModePlayer>().dexterityMaxExp)
                        {
                            player.GetModPlayer<DModePlayer>().dexterityExp = player.GetModPlayer<DModePlayer>().dexterityExp - player.GetModPlayer<DModePlayer>().dexterityMaxExp;
                            player.GetModPlayer<DModePlayer>().Dexterity += 1;
                            player.GetModPlayer<DModePlayer>().dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - player.GetModPlayer<DModePlayer>().Dexterity) * (1 + 0.075 * player.GetModPlayer<DModePlayer>().Dexterity));
                        }

                        break;

                    case "Mind":
                        player.GetModPlayer<DModePlayer>().mindExp +=
                            (int)(0.8 * player.GetModPlayer<DModePlayer>().strengthExp +
                            0.8 * player.GetModPlayer<DModePlayer>().dexterityExp +
                            0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        player.GetModPlayer<DModePlayer>().dexterityExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().dexterityExp);
                        player.GetModPlayer<DModePlayer>().strengthExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().strengthExp);
                        player.GetModPlayer<DModePlayer>().spiritExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        while (player.GetModPlayer<DModePlayer>().mindExp >= player.GetModPlayer<DModePlayer>().mindMaxExp)
                        {
                            player.GetModPlayer<DModePlayer>().mindExp = player.GetModPlayer<DModePlayer>().mindExp - player.GetModPlayer<DModePlayer>().mindMaxExp; ;
                            player.GetModPlayer<DModePlayer>().Mind += 1;
                            player.GetModPlayer<DModePlayer>().mindMaxExp = (int)(80 * (10 + crystal_upgrades - player.GetModPlayer<DModePlayer>().Mind) * (1 + 0.075 * player.GetModPlayer<DModePlayer>().Mind));
                        }
                        break;

                    case "Spirit":
                        player.GetModPlayer<DModePlayer>().spiritExp +=
                            (int)(0.8 * player.GetModPlayer<DModePlayer>().strengthExp +
                            0.8 * player.GetModPlayer<DModePlayer>().dexterityExp +
                            0.8 * player.GetModPlayer<DModePlayer>().mindExp);

                        player.GetModPlayer<DModePlayer>().dexterityExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().dexterityExp);
                        player.GetModPlayer<DModePlayer>().mindExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().mindExp);
                        player.GetModPlayer<DModePlayer>().strengthExp -= (int)(0.8 * player.GetModPlayer<DModePlayer>().spiritExp);

                        while (player.GetModPlayer<DModePlayer>().spiritExp >= player.GetModPlayer<DModePlayer>().spiritMaxExp)
                        {
                            player.GetModPlayer<DModePlayer>().spiritExp = player.GetModPlayer<DModePlayer>().spiritExp - player.GetModPlayer<DModePlayer>().spiritMaxExp; ;
                            player.GetModPlayer<DModePlayer>().Spirit += 1;
                            player.GetModPlayer<DModePlayer>().spiritMaxExp = (int)(80 * (10 + crystal_upgrades - player.GetModPlayer<DModePlayer>().Spirit) * (1 + 0.075 * player.GetModPlayer<DModePlayer>().Spirit));
                        }

                        break;
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ConcentratedEvil>(), 12);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.SilverBar, 2);
            recipe.AddIngredient(ItemID.Amber, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
