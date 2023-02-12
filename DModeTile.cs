using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace DMode
{
    public class DModeTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player player = Main.LocalPlayer;
            if (!(player.active && !player.dead))
                return;
            if (!(!player.HeldItem.IsAir && Util.IsCommonTool(player.HeldItem)))
                return;


            if (fail || !player.InInteractionRange(i, j))
                return;
            Skill.EarnExp(player.HeldItem);
        }
    }
}