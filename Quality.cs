using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DMode
{
    [CloneByReference]
    public class Quality : GlobalItem
    {
        public int Level;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            Level = 1;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("Level", Level);
        }
       
        public override void LoadData(Item item, TagCompound tag)
        {
            Level = tag.GetInt("Level");
        }

        public static int CalculateDamageBonus(Item item) 
        {
            if (Util.IsCommonWeapon(item)) 
            {
                int Level = item.GetGlobalItem<Quality>().Level;
                return (int)Math.Ceiling(0.01 * (Level - 1) * item.damage);
            }
            else 
            { 
                return 0; 
            }
        }

        public static int CalculateDefenseBonus(Item item)
        {
            if (Util.IsCommonArmor(item))
            {
                int Level = item.GetGlobalItem<Quality>().Level;
                return (int)Math.Ceiling(0.25 * (Level - 1));
            }
            else
            {
                return 0;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Util.CanHaveAnySystem(item)) 
            {
                //Item Name Modification
                TooltipLine itemName = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
                itemName.Text = itemName.Text + " [c/D6D600:(+" + Level + ")]";
            }

            if (Util.IsCommonWeapon(item)) 
            {
                TooltipLine itemDamage = tooltips.Find(x => x.Name == "Damage" && x.Mod == "Terraria");
                itemDamage.Text = itemDamage.Text + " [c/D6D600:(+" + CalculateDamageBonus(item) + ")]";
            }

            if (Util.IsCommonArmor(item))
            {
                //Defense Tooptip Modification
                TooltipLine itemDefense = tooltips.Find(x => x.Name == "Defense" && x.Mod == "Terraria");
                if (itemDefense != null)
                {
                    itemDefense.Text = itemDefense.Text + " [c/D6D600:(+" + CalculateDefenseBonus(item) + ")]";
                }
            }
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (Util.IsCommonWeapon(item))
            {
                damage = damage.CombineWith(new StatModifier(1, 1, CalculateDamageBonus(item)));
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            //Quality Bonus:
            if (Util.IsCommonArmor(item))
            {
                player.statDefense += CalculateDefenseBonus(item);
            }
        }
    }
}
