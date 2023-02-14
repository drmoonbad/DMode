using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs.Style
{
    public class StyleC : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Style - C");
            Description.SetDefault("Can't touch this...");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 0.40f;
            player.GetDamage<GenericDamageClass>() += 0.10f;
        }
    }
}
