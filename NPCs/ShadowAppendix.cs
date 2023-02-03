using DMode.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.NPCs
{
    public class ShadowAppendix : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Appendage");
        }

        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 38;

            NPC.lifeMax = 68;
            NPC.damage = 24;
            NPC.defense = 8;

            NPC.knockBackResist = 0;

            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 5, 30);

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Snatcher];
            AnimationType = NPCID.Snatcher;

            NPC.scale = 0.875f;
            NPC.noTileCollide = true;
        }

        int attackTimer = 0;
        int glowTimer = 0;
        bool glowingEye = false;

        int myHost = -1;
        bool hasHost = false;

        int myTarget = -1;
        bool hasTarget = false;

        bool hadHost = false;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.life = 85;
            NPC.damage = 34;
            NPC.defense = 14;
        }

        public override void AI()
        { 
            //Friction:
            NPC.velocity *= 1 - 0.01f;

            //Finding a nearby host on spawn:
            if (myHost == -1 && !hadHost)
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC host = Main.npc[k];
                    if (host.active && host.netID != NPC.netID && Util.CanHaveLevel(host))
                    {
                        float dX = host.Center.X - NPC.Center.X;
                        float dY = host.Center.Y - NPC.Center.Y;
                        double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                        double range = 0.5 * Math.Sqrt(Math.Pow(host.height, 2) + Math.Pow(host.width, 2)) + 200;

                        if (distance <= range)
                        {
                            myHost = k;
                            hasHost = true;
                        }
                    }
                }
            }

            //While having a host:
            if (myHost != -1 && hasHost && !hadHost)
            {
                NPC host = Main.npc[myHost];
                if (host.active && host.netID != NPC.netID)
                {
                    NPC.AddBuff(Mod.Find<ModBuff>("ShadowAppendixBuff").Type, 20);
                    host.AddBuff(Mod.Find<ModBuff>("ShadowAppendixDebuff").Type, 20);

                    float dX = host.Center.X - NPC.Center.X;
                    float dY = host.Center.Y - NPC.Center.Y;
                    double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                    double minRange = Math.Sqrt(Math.Pow(host.height, 2) + Math.Pow(host.width, 2)) - 25;
                    double maxRange = Math.Sqrt(Math.Pow(host.height, 2) + Math.Pow(host.width, 2)) + 25;

                    if (distance > maxRange)
                    {
                        NPC.velocity.X += dX / (15 * 60);
                        NPC.velocity.Y += dY / (15 * 60);
                    }
                    else if (distance < minRange)
                    {
                        NPC.velocity.X -= dX / (5 * 60);
                        NPC.velocity.Y -= dY / (5 * 60);
                    }
                }
                else if (!host.active || host.life <= 0)
                {
                    myHost = -1;
                    hasHost = false;
                    hadHost = true;
                }
            }

            //Finding a nearby player to target:
            Player target = Main.LocalPlayer;
            if (target.active && target.statLife >= 1)
            {
                float dX = target.Center.X - NPC.Center.X;
                float dY = target.Center.Y - NPC.Center.Y;
                double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                if (distance <= 1000)
                {
                    myTarget = target.whoAmI;
                    hasTarget = true;
                }
            }

            //Looking at the player:
            if (myTarget != -1)
            {
                if (target.active && target.statLife >= 1)
                {
                    float dX = target.Center.X - NPC.Center.X;
                    float dY = target.Center.Y - NPC.Center.Y;
                    double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                    double angle = 0;
                    //Downwards
                    if (dY > 0)
                    {
                        angle = Math.PI * 3 / 2 - Math.Asin(dX / distance);
                    }
                    //Upwards
                    else if (dY < 0)
                    {
                        angle = Math.PI / 2 + Math.Asin(dX / distance);
                    }

                    NPC.rotation = (float)(angle);   
                }
                else
                {
                    myTarget = -1;
                    hasTarget = false;
                }
            }

            //Charging at the player:
            if (myTarget != -1 && myHost == -1 || hasTarget && !hasHost || hadHost)
            {
                if (target.active && target.statLife >= 1)
                {
                    float dX = target.Center.X - NPC.Center.X;
                    float dY = target.Center.Y - NPC.Center.Y;
                    double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                    attackTimer++;
                    if (attackTimer >= 60)
                    {
                        attackTimer = 0;

                        glowingEye = true;

                        NPC.velocity.X = dX / 27;
                        NPC.velocity.Y = dY / 27;
                    }
                }
            }

            if (glowingEye)
            {
                glowTimer++;
                for (int k = 0; k < 3; k++)
                {
                    int d = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), 2, 2, 235, 0, 0);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0;
                }

                if (glowTimer >= 30)
                {
                    glowTimer = 0;
                    glowingEye = false;
                }
            }

            if (myTarget != -1 && myHost != -1 || hasTarget && hasHost)
            {
                if (target.active && target.statLife >= 1)
                {
                    float dX = target.Center.X - NPC.Center.X;
                    float dY = target.Center.Y - NPC.Center.Y;
                    double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                    if (Util.GetRandomInt(0, 60) == 0)
                    {
                        NPC.velocity.X += dX / (60 * 60);
                        NPC.velocity.Y += dY / (60 * 60);
                    }
                }
            }

            //Velocity range:
            if (Math.Abs(NPC.velocity.X) > 8)
            {
                NPC.velocity.X *= 0.8f;
            }
            if (Math.Abs(NPC.velocity.Y) > 8)
            {
                NPC.velocity.Y *= 0.8f;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            int duration = 30 + 3 * Util.GetWorldLevel();
            if (duration > 120) { duration = 120; }
            target.AddBuff(Mod.Find<ModBuff>("DeathsGrasp").Type, duration);

            if (!target.active || target.statLife <= 0)
            {
                NPC.active = false;
            }
        }

        public override void OnKill()
        {
            //Death Effects:
            for (int k = 0; k < 2; k++) { Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("shadowDust").Type, 0, 2); }

            Item.NewItem(NPC.GetSource_Loot(), NPC.position, ModContent.ItemType<ConcentratedEvil>(), 1);
        }

        public override void DrawEffects(ref Color drawColor)
        {
            drawColor = new Color(0, 0, 0, 204);
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(NPC.position, NPC.width + 4, NPC.height + 4, Mod.Find<ModDust>("shadowDust").Type);
            }
        }
    }
}
