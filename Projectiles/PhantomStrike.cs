using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Projectiles
{
	public class PhantomStrike : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantom Strike");
            Main.projFrames[Projectile.type] = 31;
        }

        public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 31 + 2;
			Projectile.friendly = false;
            Projectile.hostile = false;
			Projectile.aiStyle = 0;
            Projectile.arrow = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 1;
            Projectile.light = 0.4f;

            Projectile.usesLocalNPCImmunity = false;
            Projectile.localNPCHitCooldown = -1;

        }

        public override void AI()
        {
            //Animation:
            Projectile.frame++;

            //Damage Instance:
            if (Projectile.timeLeft == 16) { Projectile.friendly = true; }
            if (Projectile.timeLeft == 19) { Projectile.friendly = false; }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
    }
}