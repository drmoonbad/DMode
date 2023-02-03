using Terraria;
using Terraria.ModLoader;

namespace DMode.Buffs
{
    public class ShadowAppendixBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Appendix Buff");
            Description.SetDefault("The Appendix has received some buffs.");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DModeNPC>().shadowParasite = true;
        }
    }
}
