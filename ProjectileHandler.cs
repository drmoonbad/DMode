using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using System;

namespace DMode
{
    [CloneByReference]
    public class ProjectileHandler : GlobalProjectile
    {
        public override void OnHitNPC(Terraria.Projectile projectile, NPC target, int damage, float knockback,
            bool crit)
        {
            if (projectile.owner != Main.myPlayer)
                return;
            Player player = Main.LocalPlayer;

            var itemInHand = player.inventory[player.selectedItem];
            Skill.EarnExp(itemInHand, damage);
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
    }
}