using Terraria;
using Terraria.ModLoader;

namespace DModeRemastered
{
    [CloneByReference]
    public class DModeProjectile : GlobalProjectile
    {
        //Soul Projectile:
        public int SoulOwner = -1;
        public int SoulValue = -11;

        //Bonuses:
        public bool receivedBonuses = false;
        public bool canBounce = true;
        public bool can_crit = true;

        //Skill
        public int sourceInvID;

        public override bool InstancePerEntity
        {
            get { return true; }
        }
        /*public override bool CloneNewInstances
        {
            get
            {
                return false;
            }
        }*/

        public override void AI(Projectile projectile)
        {
            //Unowned Souls:
            if (projectile.type == Mod.Find<ModProjectile>("Soul").Type && SoulOwner == -1)
            {
                SoulOwner = Main.LocalPlayer.whoAmI;
            }
        }

        public override void SetDefaults(Projectile projectile)
        {
            sourceInvID = -1;

            /*
             * When a frendly projectile does not have a damage type when generated, it's will receive a damage type.
             * The damage type to be set is the same as the item's damage type of the currently item being held by the projectile's owner.
             * Exception 1: If the determined weapon does not have a damage type, then a random damage type may be set.
             * Exception 2: If the projectile does not have an active owner or a valid item in hand, a random damage type may be set. 
             */
            bool hasNoDamageType = false;
            if (projectile.DamageType == null)
            {
                hasNoDamageType = true;
            }
            else
            {
                hasNoDamageType = false;
            }

            if (hasNoDamageType)
            {
                Player player = Main.LocalPlayer;
                if (player.active && player.HeldItem.netID != 0)
                {
                    Item item = player.HeldItem;

                    bool hasNoDamageType2;
                    if (item.DamageType == null)
                    {
                        hasNoDamageType2 = true;
                    }
                    else
                    {
                        hasNoDamageType2 = false;
                    }

                    if (hasNoDamageType2)
                    {
                        //Exception 1:
                        switch (Util.GetRandomInt(0, 4))
                        {
                            case 0:
                                projectile.DamageType = DamageClass.Melee;
                                break;
                            case 1:
                                projectile.DamageType = DamageClass.Magic;
                                break;
                            case 2:
                                projectile.DamageType = DamageClass.Ranged;
                                break;
                            case 3:
                                projectile.DamageType = DamageClass.Summon;
                                break;
                            case 4:
                                projectile.DamageType = DamageClass.Throwing;
                                break;
                        }
                    }
                    else if (!hasNoDamageType2)
                    {
                        projectile.DamageType = item.DamageType;
                    }
                }
                else if (!player.active || player.HeldItem.netID == 0)
                {
                    //Exception 2:
                    switch (Util.GetRandomInt(0, 4))
                    {
                        case 0:
                            projectile.DamageType = DamageClass.Melee;
                            break;
                        case 1:
                            projectile.DamageType = DamageClass.Magic;
                            break;
                        case 2:
                            projectile.DamageType = DamageClass.Ranged;
                            break;
                        case 3:
                            projectile.DamageType = DamageClass.Summon;
                            break;
                        case 4:
                            projectile.DamageType = DamageClass.Throwing;
                            break;
                    }
                }
            }

            //Reduced Lifespawn of unowned souls:
            if (projectile.type == Mod.Find<ModProjectile>("Soul").Type && SoulOwner == -1)
            {
                projectile.timeLeft = projectile.timeLeft / 2;
            }
        }

        private void EarnExp(Player player, NPC target, int dmg)
        {
            if (sourceInvID < 0)
            {
                /*
                * For the cases where a source weapon inventory position can't be defined,
                * the experience is given the item in the player's hand.
                */
                if (Util.IsCommonWeapon(player.HeldItem) && Util.IsValidSkillSystemTarget(target))
                {
                    Skill.EarnExp(player.HeldItem, dmg);
                }
            }
            else
            {
                /*
                * For the cases where a source weapon is defined,
                * the experience is given to the item in the respective ID in the player's inventory.
                * There is no need to check for weapon because shooting a damaging projectile is sufficient to know that it is a weapon.
                */
                Item item = player.inventory[sourceInvID];
                if (Util.IsValidSkillSystemTarget(target))
                {
                    Skill.EarnExp(item, dmg);
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.friendly)
            {
                Player player = Main.LocalPlayer;

                if (!player.HeldItem.IsAir && !projectile.npcProj)
                {
                    EarnExp(player, target, damage);
                }
            }
        }
    }
}