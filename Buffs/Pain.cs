using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs
{
    public class Pain : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pain");
            Description.SetDefault("It hurts so much...");

            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.moveSpeed > 1) { player.moveSpeed = 1; }
            else if (player.moveSpeed < 1) { player.moveSpeed *= 0.75f; }
        }
    }
}
