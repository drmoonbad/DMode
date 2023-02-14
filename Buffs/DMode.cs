using System;
using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs
{
    public class DMode : ModBuff
    {
        public override void SetStaticDefaults()
        {
            string description = null;

            if (Main.expertMode)
            {
                description = "Prepare to die, Terrarian!";
            }
            else if (!Main.expertMode)
            {
                description = "Unlimited Power!";
            }

            DisplayName.SetDefault("D-Mode");
            Description.SetDefault(description);

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        private static void AddDamageModifier<T>(Player player, float modifier) where T : DamageClass
        {
            player.GetDamage<T>() *= 1 + modifier;
        }

        private static void AddCritModifier<T>(Player player, float modifier) where T : DamageClass
        {
            player.GetCritChance<T>() += modifier;
        }

        private static void AddKnockbackModifier<T>(Player player, float modifier) where T : DamageClass
        {
            player.GetKnockback<T>() += modifier;
        }

        private static void AddAttackSpeedModifier<T>(Player player, float modifier) where T : DamageClass
        {
            player.GetAttackSpeed<T>() *=  1 + modifier;
        }

        public override void Update(Player player, ref int buffInDexterity)
        {
            DModePlayer dModePlayer = player.GetModPlayer<DModePlayer>();

            int GeneralLevel = dModePlayer.GeneralLevel;
            int Strength = dModePlayer.Strength;
            int Mind = dModePlayer.Mind;
            int Dexterity = dModePlayer.Dexterity;
            int Spirit = dModePlayer.Spirit;

            float DestinyFactor = 1 + dModePlayer.DestinyPoints * 0.005f;
            float MinionBonusFactor = 2 / player.maxMinions;

            //General Damage [0.4% - 0.8% BPP]
            AddDamageModifier<MeleeDamageClass>(player, Strength * 0.002f * DestinyFactor);
            AddDamageModifier<MagicDamageClass>(player, Mind * 0.002f * DestinyFactor);
            AddDamageModifier<RangedDamageClass>(player, Dexterity * 0.002f * DestinyFactor);
            AddDamageModifier<ThrowingDamageClass>(player, (Strength + Dexterity) * 0.002f * DestinyFactor);

            //General Critical Strike Chance [2.5% - 5% at level 100]
            AddCritModifier<MeleeDamageClass>(player, (int)(Strength * 0.05));
            AddCritModifier<MeleeDamageClass>(player, (int)(Mind * 0.05));
            AddCritModifier<MeleeDamageClass>(player, (int)(Dexterity * 0.05));
            AddCritModifier<MeleeDamageClass>(player, (int)((Strength + Dexterity) * 0.025));

            //Minion Damage and Related
            player.maxMinions += (int)Math.Floor(0.04 * Spirit);

            AddKnockbackModifier<SummonDamageClass>(player,
                (Mind + Spirit) * 0.0075f * MinionBonusFactor * DestinyFactor);
            AddDamageModifier<SummonDamageClass>(player, (Mind + Spirit) * 0.008f * MinionBonusFactor * DestinyFactor);

            //Mana Cost and Melee Speed
            double bonusManaCost = Spirit * 0.0004 > 0.1 ? 0.1 : Spirit * 0.0004;
            player.manaCost -= (float)bonusManaCost;

            AddAttackSpeedModifier<GenericDamageClass>(player, (Strength + Dexterity) * 0.0001f * DestinyFactor);

            //General Levels
            double bonusLifeMax = GeneralLevel * 2 * DestinyFactor;
            if (bonusLifeMax > 100)
            {
                bonusLifeMax = 100;
            }

            player.statLifeMax2 += (int)(bonusLifeMax);

            double bonusManaMax = GeneralLevel * 1 * DestinyFactor;
            if (bonusManaMax > 100)
            {
                bonusManaMax = 100;
            }

            player.statManaMax2 += (int)(bonusManaMax);

            double bonusMoveSpeed = GeneralLevel * 0.001 * DestinyFactor;
            if (bonusMoveSpeed > 0.25)
            {
                bonusMoveSpeed = 0.25;
            }

            player.moveSpeed += (float)(bonusMoveSpeed);

            //Defense Bonuses
            double StrengthBonusDefense = 0.15 * Strength;
            double MindBonusDefense = 0.1 * Mind;
            double DexterityBonusDefense = 0.125 * Dexterity;
            double SpiritBonusDefense = 0.0875 * Spirit;

            int BonusDefense =
                (int)(StrengthBonusDefense + MindBonusDefense + DexterityBonusDefense + SpiritBonusDefense);
            player.statDefense = BonusDefense;

            if (!player.HeldItem.IsAir && Util.IsCommonTool(player.HeldItem))
            {
                Skill skill = player.HeldItem.GetGlobalItem<Skill>();
                Quality quality = player.HeldItem.GetGlobalItem<Quality>();

                player.pickSpeed -= 0.01f * (quality.Level - 1);
                player.pickSpeed *= 1 - 0.01f * (skill.Level - 1);
            }
        }
    }
}