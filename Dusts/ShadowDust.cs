using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DMode.Dusts
{
	public class ShadowDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
            dust.velocity.Y -= 0.08f;
			dust.scale *= 1.075f;

            dust.noGravity = true;
            dust.noLight = true;
		}

		public override bool MidUpdate(Dust dust)
		{
            dust.velocity.Y -= 0.08f;
            if (dust.velocity.Y > 0) { dust.velocity.Y = -0.08f; }
            if (dust.velocity.X != 0) { dust.velocity.X = 0; }

            dust.color = new Color(dust.color.R, dust.color.G, dust.color.B, dust.scale / 1.075f);

			return true;
		}
	}
}