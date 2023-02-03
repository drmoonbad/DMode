using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using DMode.Items.Materials;

namespace DMode
{
    [CloneByReference]
    public class DModeNPC : GlobalNPC
    {
        //Level:
        public bool hasLevel = false;
        public int level = 0;

        //Monster Scaling Constants
        const float lifePerLevel_casual = 0.05f;
        const float defensePerLevel_casual = 0.05f;
        const float damagePerLevel_casual = 0.05f;
        const float bossFactor_casual = 0.5f;

        const float lifePerLevel_expert = 0.05f;
        const float defensePerLevel_expert = 0.075f;
        const float damagePerLevel_expert = 0.075f;
        const float bossFactor_expert = 0.55f;

        const float lifePerLevel_master = 0.05f;
        const float defensePerLevel_master = 0.09f;
        const float damagePerLevel_master = 0.09f;
        const float bossFactor_master = 0.6f;

        const float hardmodeFactor = 0.5f;
        const float moonlordFactor = 0.5f;

        //Souls:
        public int playerToLastHit = 0;

        //Special NPCs:
        private bool bossVariant = false;
        private bool shadowVariant = false;
        private bool hasSpawnedAppendix = false;

        //Buffs:
        public bool shadowHost = false;
        public bool shadowParasite = false;

        //Phantom Strike:
        public bool phantomStrike = false;
        public int phantomTimer = 150 - 16;
        public int phantomOwner = 0;
        public int phantomDamage = 0;

        //Arcane Aura:
        public bool arcaneAura = false;
        public int arcaneDamage = 0;

        private int combatNameType;
        private string combatName;

        public override bool InstancePerEntity => true;

        public void ApplyModifiers(NPC npc) 
        {
            hasLevel = false;
            level = 0;

            bossVariant = false;
            shadowVariant = false;
            hasSpawnedAppendix = false;

            shadowHost = false;
            shadowParasite = false;

            if (Util.CanHaveLevel(npc))
            {
                level = 1 + Util.GetWorldLevel() + Util.GetRandomInt(0, 4);
                hasLevel = true;

                if (Util.CanBeVariant(npc, Mod) && !npc.boss)
                {
                    if (Util.GetWorldLevel() >= 5 && Util.Chance(0.4f))
                    {
                        bossVariant = true;
                        npc.lifeMax *= 10;
                        npc.defense += (int)(1 + npc.defense * 0.2f);
                        npc.damage += (int)(1 + npc.damage * 0.2f);
                        npc.scale *= 1.4f;
                        npc.knockBackResist = 0;
                    }

                    if (Util.GetWorldLevel() >= 10 && Util.Chance(0.2f))
                    {
                        shadowVariant = true;
                        npc.lifeMax += (int)(1.5f * npc.lifeMax);
                        npc.defense *= 2;
                        npc.damage *= 2;
                        npc.knockBackResist *= 0.5f;
                    }
                }

                float bonusLife, bonusDefense, bonusDamage;
                if (Main.masterMode)
                {
                    bonusLife = lifePerLevel_master * level * npc.lifeMax;
                    bonusDefense = defensePerLevel_master * level * npc.defense;
                    bonusDamage = damagePerLevel_master * level * npc.damage;

                    if (npc.boss)
                    {
                        bonusLife *= bossFactor_master;
                        bonusDefense *= bossFactor_master;
                        bonusDamage *= bossFactor_master;
                    }
                }
                else if (Main.expertMode)
                {
                    bonusLife = lifePerLevel_expert * level * npc.lifeMax;
                    bonusDefense = defensePerLevel_expert * level * npc.defense;
                    bonusDamage = damagePerLevel_expert * level * npc.damage;

                    if (npc.boss)
                    {
                        bonusLife *= bossFactor_expert;
                        bonusDefense *= bossFactor_expert;
                        bonusDamage *= bossFactor_expert;
                    }
                }
                else
                {
                    bonusLife = lifePerLevel_casual * level * npc.lifeMax;
                    bonusDefense = defensePerLevel_casual * level * npc.defense;
                    bonusDamage = damagePerLevel_casual * level * npc.damage;

                    if (npc.boss)
                    {
                        bonusLife *= bossFactor_casual;
                        bonusDefense *= bossFactor_casual;
                        bonusDamage *= bossFactor_casual;
                    }
                }

                if (Main.hardMode)
                {
                    bonusLife *= hardmodeFactor;
                    bonusDefense *= hardmodeFactor;
                    bonusDamage *= hardmodeFactor;
                }

                if (NPC.downedMoonlord)
                {
                    bonusLife *= moonlordFactor;
                    bonusDefense *= moonlordFactor;
                    bonusDamage *= moonlordFactor;
                }

                npc.lifeMax += (int)MathF.Ceiling(bonusLife);
                npc.defense += (int)MathF.Ceiling(bonusDefense);
                npc.damage += (int)MathF.Ceiling(bonusDamage);
            }
        }

        public override void SetDefaults(NPC npc)
        {
            ApplyModifiers(npc);
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            //Name Title Spawn
            if (Util.CanHaveLevel(npc))
            {
                combatName = npc.FullName + " LV." + level;
                combatNameType = CombatText.NewText(npc.getRect(), Color.White, combatName);
            }
        }

        public override void AI(NPC npc)
        {
            if (hasLevel)
            {
                CombatText combatText = Main.combatText[combatNameType];

                //Name Title Update
                if (npc.active && combatName != null)
                {
                    combatText.position = npc.Center - new Vector2(combatName.Length * 5, npc.height + 20);
                    combatText.lifeTime = 5;
                    combatText.scale = 0.75f;
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            //Shadow Appendix Buffs and Debuffs:
            if (shadowHost)
            {
                shadowParasite = false;
                npc.lifeRegen -= 4;
                npc.lifeRegenExpectedLossPerSecond = 2;
            }
            if (shadowParasite)
            {
                shadowHost = false;     
                npc.lifeRegen += 4;
            }
        }

        public override void ResetEffects(NPC npc)
        {
            shadowHost = false;
            shadowParasite = false;
            arcaneAura = false;
        }

        public override bool CheckDead(NPC npc)
        {
            DModeWorld.BloodValueMax = 90 * Math.Exp(0.06 * DModeWorld.LevelBonus);
            if (Util.CanHaveLevel(npc))
            {
                double bloodValue;
                double bloodFactor = npc.boss ? 0.01 : 0.02;
                bloodValue = bloodFactor * Math.Pow(0.977, DModeWorld.LevelBonus);
                bloodValue = bloodValue < 0.5 ? 0.5 : bloodValue;
                DModeWorld.BloodValue += bloodValue;
                if (DModeWorld.BloodValue >= DModeWorld.BloodValueMax)
                {
                    DModeWorld.BloodValue = 0;
                    DModeWorld.LevelBonus += 1;
                    Util.ChatMessage("You feel evil thoughts creeping on you...", Color.LightSeaGreen);
                }
            }
            return true;
        }

        public override void OnKill(NPC npc)
        {
            //Madness Sigil
            Player player = Main.LocalPlayer;
            int redFactor, greenFactor;
            if (player.GetModPlayer<DModePlayer>().MadnessSigil) 
            {
                redFactor = 2;
                greenFactor = 0;
            }
            else 
            {
                redFactor = 1;
                greenFactor = 1;
            }

            //Monster Drops
            if (Util.CanHaveLevel(npc) && !npc.friendly)
            {
                //Red
                if (!npc.boss)
                {
                    if (npc.lifeMax >= 75)
                    {
                        Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ModContent.ItemType<MonsterCore>(), 1 * redFactor);
                    }
                }
                else
                {
                    Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ModContent.ItemType<MonsterCore>(), 5 * redFactor);
                }

                //Green
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ModContent.ItemType<MonstrousExperience>(), Util.GetRandomInt(1, 3) * greenFactor);

                //Soul
                int soul = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, Mod.Find<ModProjectile>("Soul").Type, 0, 0);
                Main.projectile[soul].GetGlobalProjectile<DModeProjectile>().SoulOwner = playerToLastHit;
                Main.projectile[soul].ai[1] = Util.GetSoulValue(npc) * (1 + player.GetModPlayer<DModePlayer>().soulValueBonus);
            }

            //Destiny Box Drops
            if (npc.boss || (Util.CanBeVariant(npc, Mod) && npc.lifeMax >= 100 && Util.Chance(1)))
            {
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, Mod.Find<ModItem>("DestinyBox").Type, 1);
            }

            //Variants
            int variantCount = 0;
            if (shadowVariant) { variantCount++; }
            if (bossVariant) { variantCount++; }
            if (variantCount > 0) 
            {
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, Mod.Find<ModItem>("DestinyBox").Type, variantCount);
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ModContent.ItemType<MonsterCore>(), 5 * variantCount * redFactor );
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ModContent.ItemType<ConcentratedEvil>(), variantCount);
                Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ItemID.Amber, variantCount);
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (shadowVariant)
            {
                if (npc.netID !=  Mod.Find<ModNPC>("ShadowAppendix").Type)
                {
                    drawColor = new Color(0, 0, 0, 204);
                }

                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(new Vector2(npc.position.X - 4, npc.position.Y - 4), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("shadowDust").Type);
                }
            }

            if (arcaneAura)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(new Vector2(npc.position.X - 4, npc.position.Y - 4), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("arcaneDust").Type);
                }
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            //Shadow Debuff:
            if (shadowVariant)
            {
                int duration = 30 + 3 * Util.GetWorldLevel();
                duration = duration > 90 ? 90 : duration;
                target.AddBuff(Mod.Find<ModBuff>("DeathsGrasp").Type, duration);
            }

            //Broken Bones Proc:
            float chance = 50 - 0.05f * Util.GetWorldLevel() + 0.5f * target.statDefense;
            chance = chance < 10 ? 10 : chance;
            if (Util.Chance(chance) && target.statLife < target.statLifeMax2 / 2)
            {
                double duration = 300 + 6 * Util.GetWorldLevel() - 12 * target.statDefense;
                duration = duration < 60 ? 60 : duration;
                duration = duration > 600 ? 600 : duration;
                target.AddBuff(Mod.Find<ModBuff>("BrokenBones").Type, (int)duration);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            playerToLastHit = player.whoAmI;

            //Shadow Appendix Spawn:
            if (shadowVariant && !hasSpawnedAppendix && npc.netID != Mod.Find<ModNPC>("ShadowAppendix").Type)
            {
                hasSpawnedAppendix = true;
                NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X), (int)(npc.position.Y), Mod.Find<ModNPC>("ShadowAppendix").Type);
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            playerToLastHit = projectile.owner;

            //Shadow Appendix Spawn:
            if (shadowVariant && !hasSpawnedAppendix && npc.netID != Mod.Find<ModNPC>("ShadowAppendix").Type)
            {
                hasSpawnedAppendix = true;
                NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X), (int)(npc.position.Y), Mod.Find<ModNPC>("ShadowAppendix").Type);
            }
        }
    }
}