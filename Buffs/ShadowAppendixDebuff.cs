using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered.Buffs
{
    public class ShadowAppendixDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Appendix Debuff");
            Description.SetDefault("Being a host for the Shadow Appendix.");

            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DModeNPC>().shadowHost = true;
        }
    }
}
