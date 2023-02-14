using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DModeRemastered
{
    [CloneByReference]
    public class Skill : GlobalItem
    {
        public int Level;
        public int Exp;
        public int ExpMax;

        public override bool InstancePerEntity => true;

        public static int RecalculateMaxExp(Item item)
        {
            var increaseModifier = Util.IsCommonArmor(item) ? 1.3 : 2;
            var baseModifier = Util.IsCommonArmor(item) ? 2 : 10;

            Skill skill = item.GetGlobalItem<Skill>();

            int rarity = item.rare < ItemRarityID.White ? 0 : item.rare;
            return baseModifier * (rarity + 1) * ((int)Math.Pow(skill.Level, increaseModifier) + baseModifier) *
                   skill.Level;
        }


        public static void EarnExp(Item item, int amount = 1)
        {
            Skill skill = item.GetGlobalItem<Skill>();
            if (skill == null)
                return;
            Player player = Main.LocalPlayer;

            if (skill.Exp + amount < skill.ExpMax)
            {
                skill.Exp += amount;
            }
            else
            {
                var expLeft = amount - (skill.ExpMax - skill.Exp);

                skill.Level++;
                skill.Exp = 0;
                skill.ExpMax = RecalculateMaxExp(item);


                Util.NewCombatText(player, new Color(88, 188, 35), "Skill level up!", false, false, 1.25f, 90);
                Util.DefaultDustEffect(player.Center, 74, 5f);
                Util.NewSoundFX(SoundID.Item4, 0.3f, player.Center);
                EarnExp(item, expLeft);
            }
        }

        public override void SetDefaults(Item item)
        {
            Level = 1;
            Exp = 0;
            ExpMax = RecalculateMaxExp(item);
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("Level", Level);
            tag.Add("Exp", Exp);
            tag.Add("ExpMax", ExpMax);
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            Level = tag.GetInt("Level");
            Exp = tag.GetInt("Exp");
            ExpMax = RecalculateMaxExp(item);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Util.CanHaveAnySystem(item))
            {
                //Experience Tracking
                string expText = "[Skill Exp]: " + Exp + " / " + ExpMax;
                TooltipLine expTooltipLine = new TooltipLine(Mod, "DMode-Skill-Exp", expText);
                expTooltipLine.OverrideColor = Color.LightGreen;
                tooltips.Add(expTooltipLine);

                //Item Name Modification
                TooltipLine itemName = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
                itemName.Text = itemName.Text + " [c/8CE78C:(+" + Level + ")]";
            }

            if (Util.IsCommonArmor(item))
            {
                //Defense Tooptip Modification
                TooltipLine itemDefense = tooltips.Find(x => x.Name == "Defense" && x.Mod == "Terraria");
                if (itemDefense != null)
                {
                    itemDefense.Text = itemDefense.Text + " [c/8CE78C:(+" + CalculateLifeBonus(item) + " HP)]";
                    tooltips.Add(new TooltipLine(Mod, "playerSpeed",
                        $"[c/8CE78C:+{Math.Floor(0.35f * Level)}% Move speed]"));
                }
            }

            if (Util.IsCommonWeapon(item))
            {
                //Crit Tooptip Modification
                TooltipLine itemCrit = tooltips.Find(x => x.Name == "CritChance" && x.Mod == "Terraria");
                if (itemCrit != null)
                {
                    itemCrit.Text = itemCrit.Text + " [c/8CE78C:(+" + (Level) + "%)]";
                }
            }
        }

        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            if (Util.IsCommonWeapon(item))
            {
                crit += Level;
            }
        }

        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Util.IsCommonWeapon(item) && Util.IsValidSkillSystemTarget(target))
            {
                EarnExp(item, damage);
            }
        }

        public static int CalculateLifeBonus(Item item)
        {
            if (Util.IsCommonArmor(item))
            {
                int Level = item.GetGlobalItem<Skill>().Level;
                return Level;
            }

            return 0;
        }

        public override void UpdateEquip(Item item, Player player)
        {
            player.statLifeMax2 += CalculateLifeBonus(item);

            if (Util.IsCommonArmor(item))
            {
                player.moveSpeed += 0.0035f * Level;
            }
        }
    }
}