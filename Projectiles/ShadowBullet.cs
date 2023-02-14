using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DModeRemastered.Projectiles
{
	public class ShadowBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
			Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.aiStyle = 0;
            Projectile.arrow = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.usesLocalNPCImmunity = false;
        }

        public override void AI()
        {
            Vector2 center = new Vector2(Projectile.position.X + Projectile.width / 2, 
                Projectile.position.Y + Projectile.height / 2);

            Dust.NewDust(center, 2, 2, Mod.Find<ModDust>("shadowDust").Type);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        { crit = false; }

        public override bool PreDraw(ref Color lightColor)
        {
            /*
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }*/
            return true;

        }
    }
}