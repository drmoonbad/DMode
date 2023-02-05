using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Items.Tools
{
    public class DHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Heart of Destiny");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;

            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;

            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;

            Item.consumable = false;

            Item.UseSound = SoundID.Item4;
        }

        double gotDestinyPoints = 0;

        public override bool AltFunctionUse(Player player)
        {
            var dModePlayer = player.GetModPlayer<DModePlayer>();

            int Strength = player.GetModPlayer<DModePlayer>().Strength;
            int Mind = player.GetModPlayer<DModePlayer>().Mind;
            int Dexterity = player.GetModPlayer<DModePlayer>().Dexterity;
            int Spirit = player.GetModPlayer<DModePlayer>().Spirit;
            int GeneralLevel = player.GetModPlayer<DModePlayer>().GeneralLevel;

            int totalCrystalUpgrades = Strength + Mind + Dexterity + Spirit;
            if (GeneralLevel >= dModePlayer.maxGeneralLevel && Main.expertMode)
            {
                player.GetModPlayer<DModePlayer>().maxGeneralLevel += 4;
                player.GetModPlayer<DModePlayer>().maxCrystalUpgrades += 4;

                gotDestinyPoints += 0.075 * player.GetModPlayer<DModePlayer>().GeneralLevel;
                gotDestinyPoints += 0.05 * totalCrystalUpgrades;

                Say(player.name + " got " + (int)gotDestinyPoints + " destiny points!");

                player.GetModPlayer<DModePlayer>().DestinyPoints += (int)gotDestinyPoints;
                gotDestinyPoints = 0;

                player.GetModPlayer<DModePlayer>().GeneralLevel = 0;
                player.GetModPlayer<DModePlayer>().SoulPoints = 0;
                player.GetModPlayer<DModePlayer>().MaxSoulPoints = 80;

                player.GetModPlayer<DModePlayer>().Strength = 0;
                player.GetModPlayer<DModePlayer>().strengthExp = 0;
                player.GetModPlayer<DModePlayer>().strengthMaxExp = 800;

                player.GetModPlayer<DModePlayer>().Dexterity = 0;
                player.GetModPlayer<DModePlayer>().dexterityExp = 0;
                player.GetModPlayer<DModePlayer>().dexterityMaxExp = 800;

                player.GetModPlayer<DModePlayer>().Mind = 0;
                player.GetModPlayer<DModePlayer>().mindExp = 0;
                player.GetModPlayer<DModePlayer>().mindMaxExp = 800;

                player.GetModPlayer<DModePlayer>().Spirit = 0;
                player.GetModPlayer<DModePlayer>().spiritExp = 0;
                player.GetModPlayer<DModePlayer>().spiritMaxExp = 800;
            }
            else
            {
                if (Main.expertMode)
                {
                    Say("You can't reset yet!", 255, 0, 0);
                }
                else if (!Main.expertMode)
                {
                    Say("You can't prestige in casual mode!", 255, 0, 0);
                }
            }

            return true;
        }

        private static void Say(string message, byte R = 255, byte B = 255, byte G = 255)
        {
            Main.NewText(message, R, B, G);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            DModePlayer player = Main.LocalPlayer.GetModPlayer<DModePlayer>();

            int strengthProgress = (player.strengthExp * 100) / player.strengthMaxExp;
            int MindProgress = (player.mindExp * 100) / player.mindMaxExp;
            int DexterityProgress = (player.dexterityExp * 100) / player.dexterityMaxExp;
            int SpiritProgress = (player.spiritExp * 100) / player.spiritMaxExp;

            int GeneralLevel = player.GeneralLevel;
            int MaxGeneralLevel = player.maxGeneralLevel;

            int SoulPoints = player.SoulPoints;
            int MaxSoulPoints = player.MaxSoulPoints;

            TooltipLine description = new TooltipLine(Mod, "description", "Check out your stats!");
            TooltipLine name = new TooltipLine(Mod, "name", "-#- [ " + Main.LocalPlayer.name + " ] -#-");

            TooltipLine level = new TooltipLine(Mod, "level", "[Current Level]: " + GeneralLevel);

            double progress = SoulPoints * 100 / MaxSoulPoints;
            string progressString = progress + "%";
            TooltipLine soulPoints = new TooltipLine(Mod, "soulPoints", "[Leveling Progress]: " + progressString);

            float DestinyBonus = ((player.DestinyPoints / 2) * 0.01f);
            TooltipLine destinyBonus = new TooltipLine(Mod, "destinyBonus",
                "[Current Destiny Bonus]: " + Math.Round(DestinyBonus * 100, 2) + "%");

            TooltipLine strength = new TooltipLine(Mod, "strength",
                "[Strength]: " + player.Strength + " (" + strengthProgress + "%)");
            strength.OverrideColor = new Color(230, 80, 90);

            TooltipLine mind = new TooltipLine(Mod, "mind", "[Mind]: " + player.Mind + " (" + MindProgress + "%)");
            mind.OverrideColor = new Color(80, 80, 230);

            TooltipLine dexterity = new TooltipLine(Mod, "dexterity",
                "[Dexterity]: " + player.Dexterity + " (" + DexterityProgress + "%)");
            dexterity.OverrideColor = new Color(135, 230, 80);

            TooltipLine spirit =
                new TooltipLine(Mod, "spirit", "[Spirit]: " + player.Spirit + " (" + SpiritProgress + "%)");
            spirit.OverrideColor = new Color(80, 230, 200);

            if (Main.expertMode)
            {
                level.Text = $"[Current Level :: Max Level]: [  {GeneralLevel}  :: {MaxGeneralLevel} ]";
            }

            tooltips.Add(description);
            tooltips.Add(name);
            tooltips.Add(level);
            tooltips.Add(soulPoints);
            tooltips.Add(destinyBonus);

            tooltips.Add(strength);
            tooltips.Add(mind);
            tooltips.Add(dexterity);
            tooltips.Add(spirit);
            if (Main.expertMode)
                tooltips.Add(new TooltipLine(Mod, "prestige",
                    $"To advance to next prestige, use this item when having max level"));
        }
    }
}