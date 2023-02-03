using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Buffs
{
    public class BrokenBones : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Bones");
            Description.SetDefault("Be careful...");

            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed -= 0.18f;
            player.noKnockback = false;

            if (Util.GetRandomInt(0, 160 - 1) == 0)
            {
                player.AddBuff(Mod.Find<ModBuff>("Pain").Type, 30);
            }

            if (Util.GetRandomInt(0, 80) == 0)
            {
                player.AddBuff(BuffID.Bleeding, 90);
            }
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.AddBuff(BuffID.Bleeding, (int)(60 * 2.5));
            player.AddBuff(Mod.Find<ModBuff>("Pain").Type, 120);
            return true;
        }
    }
}
