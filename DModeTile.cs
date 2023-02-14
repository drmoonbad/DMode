<<<<<<< Updated upstream
﻿using Terraria.ModLoader;
using Terraria;
=======
﻿using Terraria;
using Terraria.ModLoader;
>>>>>>> Stashed changes

namespace DModeRemastered
{
    public class DModeTile: GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player player = Main.LocalPlayer;
            if (player.active && !player.dead)
            {
                if (!player.HeldItem.IsAir && Util.IsCommonTool(player.HeldItem))
                {
                    Skill.EarnExp(player.HeldItem);
                }
            }
        }
    }
}
