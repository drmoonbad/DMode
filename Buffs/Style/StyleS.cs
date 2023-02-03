using Terraria;
using Terraria.ModLoader;

namespace DMode.Buffs.Style
{
    public class StyleS : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Style - S");
            Description.SetDefault("Can't touch this...");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.25f;
            player.jumpSpeedBoost += 1.00f;
            player.GetDamage<GenericDamageClass>() += 0.25f;
        }
    }
}
