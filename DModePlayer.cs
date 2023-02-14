using System;
using System.Collections.Generic;
using DModeRemastered.Buffs.Style;
using DModeRemastered.Dusts;
using DModeRemastered.Items.Tools;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DModeRemastered
{
    public class DModePlayer : ModPlayer
    {
        //Simple Buffs and Debuffs:
        public bool DeathsGrasp = false;

        //Accessories
        public bool MadnessSigil = false;

        //Permanent Upgrades
        public float styleSpeedBonus;
        public float soulValueBonus;
        public bool ArtifialElderichSoul = false;
        public bool MaterialAncientFear = false;

        //Style
        public bool activeStyle;
        public float style;
        public int styleClock;
        public const int styleClockMax = 120;

        //General Level | Souls Points | MaxSoulPoints:
        public int GeneralLevel;
        public int SoulPoints;
        public int MaxSoulPoints;

        //Destiny Points | Expert Mode | Master Mode:
        public int DestinyPoints;
        public int maxGeneralLevel;
        public int maxCrystalUpgrades;

        //Amber Points:
        public int AmberPoints;

        //Attributes:
        public int Strength;
        public int Mind;
        public int Dexterity;
        public int Spirit;

        //Attributes' Experience:
        public int strengthExp;
        public int mindExp;
        public int spiritExp;
        public int dexterityExp;

        public int strengthMaxExp;
        public int mindMaxExp;
        public int dexterityMaxExp;
        public int spiritMaxExp;

        //Amber Key Rewards: 
        public bool AmberKeyEffect = false;
        int OppenedBoxes = 0;
        int AmberTimer = -12;

        //Projectile Source Updates
        /*
         * Executing for loops is very taxing.
         * Also, there is no need for real time updating.
         * This allows for multiple weapons to earn experience at the same time.
         * Also allows summoner players to play the game lul.
         */
        int sourceUpdateClock = 30;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("GeneralLevel", GeneralLevel);
            tag.Add("SoulPoints", SoulPoints);
            tag.Add("MaxSoulPoints", MaxSoulPoints);

            tag.Add("Strength", Strength);
            tag.Add("Mind", Mind);
            tag.Add("Dexterity", Dexterity);
            tag.Add("Spirit", Spirit);

            tag.Add("strengthExp", strengthExp);
            tag.Add("strengthMaxExp", strengthMaxExp);

            tag.Add("mindExp", mindExp);
            tag.Add("mindMaxExp", mindMaxExp);

            tag.Add("dexterityExp", dexterityExp);
            tag.Add("dexterityMaxExp", dexterityMaxExp);

            tag.Add("spiritExp", spiritExp);
            tag.Add("spiritMaxExp", spiritMaxExp);

            tag.Add("AmberPoints", AmberPoints);

            tag.Add("DestinyPoints", DestinyPoints);
            tag.Add("maxGeneralLevel", maxGeneralLevel);
            tag.Add("maxCrystalUpgrades", maxCrystalUpgrades);

            tag.Add("styleSpeedBonus", styleSpeedBonus);
            tag.Add("soulValueBonus", soulValueBonus);
        }

        public override void LoadData(TagCompound tag)
        {
            GeneralLevel = tag.GetInt("GeneralLevel");
            SoulPoints = tag.GetInt("SoulPoints");
            MaxSoulPoints = tag.GetInt("MaxSoulPoints");

            Strength = tag.GetInt("Strength");
            Mind = tag.GetInt("Mind");
            Dexterity = tag.GetInt("Dexterity");
            Spirit = tag.GetInt("Spirit");

            strengthExp = tag.GetInt("strengthExp");
            strengthMaxExp = tag.GetInt("strengthMaxExp");

            mindExp = tag.GetInt("mindExp");
            mindMaxExp = tag.GetInt("mindMaxExp");

            dexterityExp = tag.GetInt("dexterityExp");
            dexterityMaxExp = tag.GetInt("dexterityMaxExp");

            spiritExp = tag.GetInt("spiritExp");
            spiritMaxExp = tag.GetInt("spiritMaxExp");

            AmberPoints = tag.GetInt("AmberPoints");

            DestinyPoints = tag.GetInt("DestinyPoints");
            maxGeneralLevel = tag.GetInt("maxGeneralLevel");
            maxCrystalUpgrades = tag.GetInt("maxCrystalUpgrades");

            styleSpeedBonus = tag.GetFloat("styleSpeedBonus");
            soulValueBonus = tag.GetFloat("soulValueBonus");
        }

        public void FirstSpawn()
        {
            activeStyle = false;
            style = 0;
            styleClock = 0;
            styleSpeedBonus = 0;
            soulValueBonus = 0;

            maxGeneralLevel = 8;
            maxCrystalUpgrades = 8;

            if (Main.masterMode)
            {
                MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0.2);
            }
            else if (Main.expertMode)
            {
                MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0.1);
            }
            else
            {
                MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0);
            }

            strengthMaxExp = 800;
            mindMaxExp = 800;
            dexterityMaxExp = 800;
            spiritMaxExp = 800;
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            FirstSpawn();

            return new[]
            {
                new Item(ModContent.ItemType<DHeart>())
            };
        }

        public static int CalculateMaxSoulPoints(int level, double factor)
        {
            return (int)Math.Floor(8 * (1 - factor) * (12 + 0.75 * level) * (1 + 0.0075 * level));
        }

        public override void OnRespawn(Player player)
        {
            //Value reseting to prevent bugs
            DeathsGrasp = false;

            activeStyle = false;
            style = 0;
            styleClock = 0;

            AmberKeyEffect = false;
            OppenedBoxes = 0;
            AmberTimer = -12;
        }

        public override void PostUpdate()
        {
            //Projectile Sources Updates
            if (sourceUpdateClock < 60)
            {
                sourceUpdateClock++;
            }
            else
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.active && !proj.npcProj)
                    {
                        proj.GetGlobalProjectile<DModeProjectile>().sourceInvID =
                            Util.FindInvIDwithShoot(proj.type, Main.LocalPlayer);
                    }
                }

                sourceUpdateClock = 0;
            }
        }

        public override void PreUpdateBuffs()
        {
            //Style
            if (Main.masterMode)
            {
                if (activeStyle)
                {
                    if (styleClock < styleClockMax)
                    {
                        styleClock++;
                        if (style < 100)
                        {
                            style += 5.0f * (1 + styleSpeedBonus) / 60.0f;
                        }
                    }
                    else
                    {
                        styleClock = 0;
                        activeStyle = false;
                    }
                }
                else
                {
                    if (style > 0)
                    {
                        style -= 5.0f / 60.0f;
                    }
                }

                switch ((int)Math.Floor(style / 20f))
                {
                    case 0:
                        break;
                    case 1:
                        Player.AddBuff(ModContent.BuffType<StyleD>(), 5);
                        break;
                    case 2:
                        Player.AddBuff(ModContent.BuffType<StyleC>(), 5);
                        break;
                    case 3:
                        Player.AddBuff(ModContent.BuffType<StyleB>(), 5);
                        break;
                    case 4:
                        Player.AddBuff(ModContent.BuffType<StyleA>(), 5);
                        break;
                    case 5:
                        Player.AddBuff(ModContent.BuffType<StyleS>(), 5);
                        break;
                }
            }

            //Pain Debuff:
            double HP = Player.statLife * Math.Pow(Player.statLifeMax, -1);
            if (HP <= 0.12 && HP > 0.05)
            {
                if (Util.Chance(1))
                {
                    double duration = 45 * (2 - HP);
                    Player.AddBuff(Mod.Find<ModBuff>("Pain").Type, (int)duration);
                }
            }
            else if (HP <= 0.05)
            {
                Player.AddBuff(Mod.Find<ModBuff>("Pain").Type, 15);
            }

            //Max Experience decreases by 10% in Expert, 20% in Master.
            if (Main.masterMode)
            {
                if (SoulPoints >= MaxSoulPoints && GeneralLevel < maxGeneralLevel)
                {
                    GeneralLevel++;
                    SoulPoints -= MaxSoulPoints;
                    MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0.2);

                    Util.NewCombatText(Player, new Color(255, 255, 255), "Level Increased!", true, false, 1.25f, 120,
                        1.1f);
                    Util.DefaultDustEffect(Player.Center, ModContent.DustType<WhiteDust>(), 8f);
                    Util.NewSoundFX(SoundID.Item4, 0.3f, Player.Center);
                }
            }
            else if (Main.expertMode)
            {
                if (SoulPoints >= MaxSoulPoints && GeneralLevel < maxGeneralLevel)
                {
                    GeneralLevel++;
                    SoulPoints -= MaxSoulPoints;
                    MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0.1);

                    Util.NewCombatText(Player, new Color(255, 255, 255), "Level Increased!", true, false, 1.25f, 120,
                        1.1f);
                    Util.DefaultDustEffect(Player.Center, ModContent.DustType<WhiteDust>(), 8f);
                    Util.NewSoundFX(SoundID.Item4, 0.3f, Player.Center);
                }
            }
            else
            {
                if (SoulPoints >= MaxSoulPoints)
                {
                    GeneralLevel++;
                    SoulPoints -= MaxSoulPoints;
                    MaxSoulPoints = CalculateMaxSoulPoints(GeneralLevel, 0);

                    Util.NewCombatText(Player, new Color(255, 255, 255), "Level Increased!", true, false, 1.25f, 120,
                        1.1f);
                    Util.DefaultDustEffect(Player.Center, ModContent.DustType<WhiteDust>(), 8f);
                    Util.NewSoundFX(SoundID.Item4, 0.3f, Player.Center);
                }
            }

            //Destiny Box Ambers
            if (AmberPoints >= 100)
            {
                AmberPoints = AmberPoints - 100;

                Item.NewItem(Player.GetSource_Misc("DMODE Amber Reward"), Player.position, ItemID.Amber, 1);
                Util.NewCombatText(Player, new Color(255, 180, 0), "Amber Bonus", false, true, 1, 50);
            }

            //Amber Key Effect
            if (AmberKeyEffect)
            {
                AmberTimer++;
                if (AmberTimer == 12 && OppenedBoxes < 50)
                {
                    AmberTimer = 0;
                    OppenedBoxes += 1;
                    Util.OpenDestinyBox(Player);
                }

                if (OppenedBoxes == 50)
                {
                    AmberKeyEffect = false;
                }
            }
            else if (!AmberKeyEffect)
            {
                AmberTimer = -12;
                OppenedBoxes = 0;
            }

            //DMode Buff
            if (Player.active)
            {
                Player.AddBuff(Mod.Find<ModBuff>("DMode").Type, 15);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            //Death's Grasp damage:
            if (DeathsGrasp)
            {
                double dot = (Player.statLife * 0.05 + 2) * 2;
                Player.lifeRegen -= (int)dot;
            }

            //Broken Bones's Bleeding damage:
            if (Player.HasBuff(Mod.Find<ModBuff>("BrokenBones").Type) && Player.HasBuff(BuffID.Bleeding))
            {
                double dot = 2 * 4 * Player.moveSpeed;
                Player.lifeRegen -= (int)(dot);
            }
        }

        public override void ResetEffects()
        {
            //Buffs
            DeathsGrasp = false;

            //Accessories
            MadnessSigil = false;
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            activeStyle = false;
            style = 0;

            //Skill System for Armors:
            Item armor_head = Player.armor[0];
            Item armor_body = Player.armor[1];
            Item armor_legs = Player.armor[2];

            //Head:
            if (Util.CanHaveAnySystem(armor_head))
            {
                Skill.EarnExp(armor_head, damage);
            }

            //Body:
            if (Util.CanHaveAnySystem(armor_body))
            {
                Skill.EarnExp(armor_body, damage);
            }

            //Legs:
            if (Util.CanHaveAnySystem(armor_legs))
            {
                Skill.EarnExp(armor_legs, damage);
            }
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            activeStyle = false;
            style = 0;
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            //Pain Damage Reduction:
            if (Player.HasBuff(Mod.Find<ModBuff>("Pain").Type))
            {
                damage = (int)(damage * 0.75);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
            ref bool crit, ref int hitDirection)
        {
            //Pain Damage Reduction:
            if (Player.HasBuff(Mod.Find<ModBuff>("Pain").Type))
            {
                damage = (int)(damage * 0.75);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            //Style
            activeStyle = true;

            //Attribute Experience System:
            if (Util.CanHaveLevel(target))
            {
                double generalLevelFactor = 1 - (0.005 * GeneralLevel);
                if (generalLevelFactor < 0.32)
                {
                    generalLevelFactor = 0.32;
                }

                double maxYieldperAttack = 2.5 + 3 * Math.Log(1 + GeneralLevel);
                if (maxYieldperAttack > 100)
                {
                    maxYieldperAttack = 100;
                }

                double minYieldperAttack = maxYieldperAttack * 0.25;

                double critFactor = 0.5;
                if (crit)
                {
                    generalLevelFactor *= critFactor;
                }

                double expYield = 0;
                if (damage * generalLevelFactor >= minYieldperAttack &&
                    damage * generalLevelFactor <= maxYieldperAttack)
                {
                    expYield = damage * generalLevelFactor;
                }
                else if (damage * generalLevelFactor > maxYieldperAttack)
                {
                    expYield = maxYieldperAttack;
                }

                int crystal_upgrades = Strength + Mind + Dexterity + Spirit;

                //Non-Expert Mode:
                if (!Main.expertMode)
                {
                    //Melee:
                    if (item.DamageType == DamageClass.Melee)
                    {
                        strengthExp += (int)(expYield);
                        if (strengthExp >= strengthMaxExp)
                        {
                            strengthExp = 0;
                            Strength += 1;
                            strengthMaxExp = (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                        }
                    }

                    //Magic:
                    if (item.DamageType == DamageClass.Magic)
                    {
                        mindExp += (int)(expYield);
                        if (mindExp >= mindMaxExp)
                        {
                            mindExp = 0;
                            Mind += 1;
                            mindMaxExp = (int)(80 * (10 + crystal_upgrades - Mind) * (1 + 0.075 * Mind));
                        }
                    }

                    //Ranged:
                    if (item.DamageType == DamageClass.Ranged)
                    {
                        dexterityExp += (int)(expYield);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            dexterityExp = 0;
                            Dexterity += 1;
                            dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                        }
                    }

                    //Summon:
                    if (item.DamageType == DamageClass.Summon)
                    {
                        spiritExp += (int)(expYield);
                        if (spiritExp >= dexterityMaxExp)
                        {
                            spiritExp = 0;
                            Spirit += 1;
                            spiritMaxExp = (int)(80 * (10 + crystal_upgrades - Spirit) * (1 + 0.075 * Spirit));
                        }
                    }

                    //Trown:
                    if (item.DamageType == DamageClass.Throwing)
                    {
                        strengthExp += (int)(expYield * 0.5);
                        if (strengthExp >= strengthMaxExp)
                        {
                            strengthExp = 0;
                            Strength += 1;
                            strengthMaxExp = (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                        }

                        dexterityExp += (int)(expYield * 0.5);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            dexterityExp = 0;
                            Dexterity += 1;
                            dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                        }
                    }
                }

                else if (Main.expertMode)
                {
                    //Melee:
                    if (item.DamageType == DamageClass.Melee)
                    {
                        strengthExp += (int)(expYield);
                        if (strengthExp >= strengthMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                strengthExp = 0;
                                Strength += 1;
                                strengthMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                            }
                            else
                            {
                                strengthExp = strengthMaxExp - 1;
                            }
                        }
                    }

                    //Magic:
                    if (item.DamageType == DamageClass.Magic)
                    {
                        mindExp += (int)(expYield);
                        if (mindExp >= mindMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                mindExp = 0;
                                Mind += 1;
                                mindMaxExp = (int)(80 * (10 + crystal_upgrades - Mind) * (1 + 0.075 * Mind));
                            }
                            else
                            {
                                mindExp = mindMaxExp - 1;
                            }
                        }
                    }

                    //Ranged:
                    if (item.DamageType == DamageClass.Ranged)
                    {
                        dexterityExp += (int)(expYield);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                dexterityExp = 0;
                                Dexterity += 1;
                                dexterityMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                            }
                            else
                            {
                                dexterityExp = dexterityMaxExp - 1;
                            }
                        }
                    }

                    //Summon:
                    if (item.DamageType == DamageClass.Summon)
                    {
                        spiritExp += (int)(expYield);
                        if (spiritExp >= dexterityMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                spiritExp = 0;
                                Spirit += 1;
                                spiritMaxExp = (int)(80 * (10 + crystal_upgrades - Spirit) * (1 + 0.075 * Spirit));
                            }
                            else
                            {
                                spiritExp = spiritMaxExp - 1;
                            }
                        }
                    }

                    //Trown:
                    if (item.DamageType == DamageClass.Throwing)
                    {
                        strengthExp += (int)(expYield) / 2;
                        if (strengthExp >= strengthMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                strengthExp = 0;
                                Strength += 1;
                                strengthMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                            }
                            else
                            {
                                strengthExp = strengthMaxExp - 1;
                            }
                        }

                        dexterityExp += (int)(expYield) / 2;
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                dexterityExp = 0;
                                Dexterity += 1;
                                dexterityMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                            }
                            else
                            {
                                dexterityExp = dexterityMaxExp - 1;
                            }
                        }
                    }
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            //Style
            activeStyle = true;

            //Attribute Experience System:
            if (Util.CanHaveLevel(target))
            {
                double generalLevelFactor = 1 - (0.0075 * GeneralLevel);

                double maxYieldperAttack = 2.5 + 3 * Math.Log(1 + GeneralLevel);
                if (maxYieldperAttack > 100)
                {
                    maxYieldperAttack = 100;
                }

                double minYieldperAttack = maxYieldperAttack * 0.25;

                if (crit)
                {
                    generalLevelFactor *= 0.5;
                }

                if (proj.penetrate > 1 && proj.penetrate != 0)
                {
                    generalLevelFactor *= (1 / proj.penetrate);
                }

                if (generalLevelFactor < 0.25)
                {
                    generalLevelFactor = 0.25;
                }

                double expYield = 0;
                if (damage * generalLevelFactor >= minYieldperAttack &&
                    damage * generalLevelFactor <= maxYieldperAttack)
                {
                    expYield = damage * generalLevelFactor;
                }
                else if (damage * generalLevelFactor > maxYieldperAttack)
                {
                    expYield = maxYieldperAttack;
                }

                int crystal_upgrades = Strength + Mind + Dexterity + Spirit;

                //Non-Expert Mode:
                if (!Main.expertMode)
                {
                    //Melee:
                    if (proj.DamageType == DamageClass.Melee)
                    {
                        strengthExp += (int)(expYield);
                        if (strengthExp >= strengthMaxExp)
                        {
                            strengthExp = 0;
                            Strength += 1;
                            strengthMaxExp = (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                        }
                    }

                    //Magic:
                    if (proj.DamageType == DamageClass.Magic)
                    {
                        mindExp += (int)(expYield);
                        if (mindExp >= mindMaxExp)
                        {
                            mindExp = 0;
                            Mind += 1;
                            mindMaxExp = (int)(80 * (10 + crystal_upgrades - Mind) * (1 + 0.075 * Mind));
                        }
                    }

                    //Ranged:
                    if (proj.DamageType == DamageClass.Ranged)
                    {
                        dexterityExp += (int)(expYield);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            dexterityExp = 0;
                            Dexterity += 1;
                            dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                        }
                    }

                    //Summon:
                    if (proj.DamageType == DamageClass.Summon)
                    {
                        spiritExp += (int)(expYield);
                        if (spiritExp >= dexterityMaxExp)
                        {
                            spiritExp = 0;
                            Spirit += 1;
                            spiritMaxExp = (int)(80 * (10 + crystal_upgrades - Spirit) * (1 + 0.075 * Spirit));
                            ;
                        }
                    }

                    //Trown:
                    if (proj.DamageType == DamageClass.Throwing)
                    {
                        strengthExp += (int)(expYield * 0.5);
                        if (strengthExp >= strengthMaxExp)
                        {
                            strengthExp = 0;
                            Strength += 1;
                            strengthMaxExp = (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                        }

                        dexterityExp += (int)(expYield * 0.5);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            dexterityExp = 0;
                            Dexterity += 1;
                            dexterityMaxExp = (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                        }
                    }
                }

                if (Main.expertMode)
                {
                    //Melee:
                    if (proj.DamageType == DamageClass.Melee)
                    {
                        strengthExp += (int)(expYield);
                        if (strengthExp >= strengthMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                strengthExp = 0;
                                Strength += 1;
                                strengthMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                strengthExp = strengthMaxExp - 1;
                            }
                        }
                    }

                    //Magic:
                    if (proj.DamageType == DamageClass.Magic)
                    {
                        mindExp += (int)(expYield);
                        if (mindExp >= mindMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                mindExp = 0;
                                Mind += 1;
                                mindMaxExp = (int)(80 * (10 + crystal_upgrades - Mind) * (1 + 0.075 * Mind));
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                mindExp = mindMaxExp - 1;
                            }
                        }
                    }

                    //Ranged:
                    if (proj.DamageType == DamageClass.Ranged)
                    {
                        dexterityExp += (int)(expYield);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                dexterityExp = 0;
                                Dexterity += 1;
                                dexterityMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                dexterityExp = dexterityMaxExp - 1;
                            }
                        }
                    }

                    //Summon:
                    if (proj.DamageType == DamageClass.Summon)
                    {
                        spiritExp += (int)(expYield);
                        if (spiritExp >= spiritMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                spiritExp = 0;
                                Spirit += 1;
                                spiritMaxExp = (int)(80 * (10 + crystal_upgrades - Spirit) * (1 + 0.075 * Spirit));
                                ;
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                spiritExp = spiritMaxExp - 1;
                            }
                        }
                    }

                    //Trown:
                    if (proj.DamageType == DamageClass.Throwing)
                    {
                        strengthExp += (int)(expYield * 0.5);
                        if (strengthExp >= strengthMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                strengthExp = 0;
                                Strength += 1;
                                strengthMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Strength) * (1 + 0.075 * Strength));
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                strengthExp = strengthMaxExp - 1;
                            }
                        }

                        dexterityExp += (int)(expYield * 0.5);
                        if (dexterityExp >= dexterityMaxExp)
                        {
                            if (crystal_upgrades < maxCrystalUpgrades)
                            {
                                dexterityExp = 0;
                                Dexterity += 1;
                                dexterityMaxExp =
                                    (int)(80 * (10 + crystal_upgrades - Dexterity) * (1 + 0.075 * Dexterity));
                            }
                            else if (crystal_upgrades >= maxCrystalUpgrades)
                            {
                                dexterityExp = dexterityMaxExp - 1;
                            }
                        }
                    }
                }
            }
        }
    }
}