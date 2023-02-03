using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using DMode.Items.Materials;

namespace DMode.Items
{
    public class SpiritPowerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Power Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.scale = 1;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;

            ///Consumable related stuff
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useTime = 15;
            Item.useAnimation = 15;
        }

        public override bool CanUseItem(Player player)
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            if (Main.expertMode)
            {
                if (Strength + Mind + Dexterity + Spirit < player.GetModPlayer<DModePlayer>().maxCrystalUpgrades)
                {
                    return true;
                }
                else { return false; }
            }
            else { return true; }
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            int crystal_upgrades = Strength + Mind + Dexterity + Spirit;

            Util.NewCombatText(player, new Color(210, 45, 230), "Spirit Power increased!");

            player.GetModPlayer<DModePlayer>().Spirit ++;
            player.GetModPlayer<DModePlayer>().spiritMaxExp = (int)(80 * (10 + crystal_upgrades - Spirit) * (1 + 0.025 * Spirit));
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "description", "Increases your minion damage, capacity and knock-back");
            TooltipLine tooltip2 = new TooltipLine(Mod, "descrition_2", "Also decreases your mana cost");

            tooltip.OverrideColor = Color.White;
            tooltip2.OverrideColor = Color.White;

            tooltips.Add(tooltip);
            tooltips.Add(tooltip2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 80);
            recipe.Register();
        }
    }

    public class StrengthPowerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strength Power Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.scale = 1;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;

            ///Consumable related stuff
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useTime = 15;
            Item.useAnimation = 15;
        }

        public override bool CanUseItem(Player player)
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            if (Main.expertMode)
            {
                if (Strength + Mind + Dexterity + Spirit < player.GetModPlayer<DModePlayer>().maxCrystalUpgrades)
                {
                    return true;
                }
                else { return false; }
            }
            else { return true; }
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            int crystal_upgrades = Strength + Mind + Dexterity + Spirit;
            Util.NewCombatText(player, new Color(230, 70, 80), "Strength Power increased!");

            player.GetModPlayer<DModePlayer>().Strength++;
            player.GetModPlayer<DModePlayer>().strengthMaxExp = (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.025 * Strength));
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "description", "Increases your melee damage and critical strike chance");
            TooltipLine tooltip2 = new TooltipLine(Mod, "descrition_2", "Also increases your thrown damage and critical strike chance");

            tooltip.OverrideColor = Color.White;
            tooltip2.OverrideColor = Color.White;

            tooltips.Add(tooltip);
            tooltips.Add(tooltip2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 80);
            recipe.Register();
        }
    }

    public class MindPowerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Power Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.scale = 1;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;

            ///Consumable related stuff
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useTime = 15;
            Item.useAnimation = 15;
        }

        public override bool CanUseItem(Player player)
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            if (Main.expertMode)
            {
                if (Strength + Mind + Dexterity + Spirit < player.GetModPlayer<DModePlayer>().maxCrystalUpgrades)
                {
                    return true;
                }
                else { return false; }
            }
            else { return true; }
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            int crystal_upgrades = Strength + Mind + Dexterity + Spirit;

            Util.NewCombatText(player, new Color(80, 110, 230), "Mind Power increased!");

            player.GetModPlayer<DModePlayer>().Mind++;
            player.GetModPlayer<DModePlayer>().mindMaxExp = (int)(80 * (10 + crystal_upgrades - Mind) * (1 + 0.025 * Mind));
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "description", "Increases your magic damage and critical strike chance");
            TooltipLine tooltip2 = new TooltipLine(Mod, "descrition_2", "Also increases your minion damage and knock-back");

            tooltip.OverrideColor = Color.White;
            tooltip2.OverrideColor = Color.White;

            tooltips.Add(tooltip);
            tooltips.Add(tooltip2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 80);
            recipe.Register();
        }
    }

    public class DexterityPowerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dexterity Power Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.scale = 1;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;

            ///Consumable related stuff
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useTime = 15;
            Item.useAnimation = 15;
        }

        public override bool CanUseItem(Player player)
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            if (Main.expertMode)
            {
                if (Strength + Mind + Dexterity + Spirit < player.GetModPlayer<DModePlayer>().maxCrystalUpgrades)
                {
                    return true;
                }
                else { return false; }
            }
            else { return true; }
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;

            int crystal_upgrades = Strength + Mind + Dexterity + Spirit;

            Util.NewCombatText(player, new Color(110, 230, 80), "Dexterity Power increased!");

            player.GetModPlayer<DModePlayer>().Dexterity++;
            player.GetModPlayer<DModePlayer>().dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.025 * Dexterity));
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "description", "Increases your ranged damage and critical strike chance");
            TooltipLine tooltip2 = new TooltipLine(Mod, "descrition_2", "Increases your thrown damage and critical strike chance");

            tooltip.OverrideColor = Color.White;
            tooltip2.OverrideColor = Color.White;

            tooltips.Add(tooltip);
            tooltips.Add(tooltip2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MonsterCore>(), 80);
            recipe.Register();
        }
    }
}