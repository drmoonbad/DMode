using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs.Style
{
    public class StyleD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Style - D");
            Description.SetDefault("Can't touch this...");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.05f;
            player.jumpSpeedBoost += 0.20f;
            player.GetDamage<GenericDamageClass>() += 0.05f;
        }
    }
}
