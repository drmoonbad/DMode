using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DMode.Dusts
{
	public class WhiteDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 2.0f;
            dust.noGravity = true;
            dust.noLight = true;
		}

		public override bool MidUpdate(Dust dust)
		{
            float strength = 5e-3f;
			Lighting.AddLight(dust.position, new Vector3(255, 255, 255) * strength * dust.scale);

			return true;
		}
	}
}