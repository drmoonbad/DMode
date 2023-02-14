using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs.Style
{
    public class StyleA : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Style - A");
            Description.SetDefault("Can't touch this...");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.20f;
            player.jumpSpeedBoost += 0.80f;
            player.GetDamage<GenericDamageClass>() += 0.20f;
        }
    }
}
