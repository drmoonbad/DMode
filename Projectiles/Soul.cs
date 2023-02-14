using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Projectiles
{
	public class Soul : ModProjectile
	{
        //public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul");
        }

        public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
			Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.aiStyle = 0;
            Projectile.arrow = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
		}

        bool followTarget = false;
        bool inKillRange = false;
        float pickRange = 1200;
        public override void AI()
        { 
            int A = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234, Projectile.oldVelocity.X * 0.025f, Projectile.oldVelocity.Y * 0.025f);
		    int B = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 187, Projectile.oldVelocity.X * 0.025f, Projectile.oldVelocity.Y * 0.025f);
		    int C = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, Projectile.oldVelocity.X * 0.025f, Projectile.oldVelocity.Y * 0.025f);

            Main.dust[A].noGravity = true;
            Main.dust[B].noGravity = true;
            Main.dust[C].noGravity = true;

            Player target = Main.LocalPlayer;
            if (target.active && Projectile.GetGlobalProjectile<DModeProjectile>().SoulOwner == target.whoAmI)
            {
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));

                if (distance > pickRange) { followTarget = false; inKillRange = false; }
                else if (distance >= 10 && distance <= pickRange) { followTarget = true; inKillRange = false; }
                else if (distance < 10) { followTarget = true; inKillRange = true; }

                if (followTarget)
                {
                    Projectile.owner = target.whoAmI;

                    Main.dust[A].velocity *= 0;
                    float vel = 12.5f;
                    distance = vel / distance;
                    shootToX *= distance;
                    shootToY *= distance;
                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                }

                if (inKillRange)
                {
                    int value = (int)Projectile.ai[1];
                    Util.NewCombatText(target, Color.White, "+" + value + " SP");
                    target.GetModPlayer<DModePlayer>().SoulPoints += value;
                    Projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            //Dying effect
			for (int k = 0; k < 3; k++)
			{
                int A = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234, Projectile.velocity.X * -1, Projectile.velocity.Y * -1);
                int B = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 187, Projectile.velocity.X * -1, Projectile.velocity.Y * -1);
                int C = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, Projectile.velocity.X * -1, Projectile.velocity.Y * -1);

                Main.dust[A].noGravity = true;
                Main.dust[B].noGravity = true;
                Main.dust[C].noGravity = true;
            }
            Util.NewSoundFX(SoundID.NPCDeath6, 0.2f, Projectile.position);
		}
    }
}