using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Buffs
{
    public class DeathsGrasp : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death's Grasp");
            Description.SetDefault("Your health is your enemy");

            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DModePlayer>().DeathsGrasp = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.AddBuff(BuffID.Blackout, 75);
            return true;
        }
    }
}
