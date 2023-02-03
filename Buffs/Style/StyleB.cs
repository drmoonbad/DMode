using Terraria;
using Terraria.ModLoader;

namespace DMode.Buffs.Style
{
    public class StyleB : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Style - B");
            Description.SetDefault("Can't touch this...");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.15f;
            player.jumpSpeedBoost += 0.60f;
            player.GetDamage<GenericDamageClass>() += 0.15f;
        }
    }
}
