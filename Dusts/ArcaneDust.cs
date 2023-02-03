using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DMode.Dusts
{
	public class ArcaneDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
            dust.velocity.Y -= 0.08f;
			dust.scale *= 1.5f;

            dust.noGravity = true;
            dust.noLight = true;
		}

		public override bool MidUpdate(Dust dust)
		{
            dust.velocity.Y -= 0.04f;
            if (dust.velocity.Y > 0) { dust.velocity.Y = -0.04f; }
            if (System.Math.Abs(dust.velocity.X) > 0.5f) { dust.velocity.X = Main.rand.NextFloat(-5, 5) / 10; }

            float strength = 0.005f;
            Lighting.AddLight(dust.position, new Vector3(0, 90, 255) * strength);

			return true;
		}
	}
}