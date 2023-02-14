using System;
using DModeRemastered.Items.Consumables;
using DModeRemastered.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DModeRemastered
{
    public class Util
    {

        public static void ChatMessage(string message, Color color) 
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
        }

        public static bool CanHaveLevel(NPC npc) 
        {
            return !npc.CountsAsACritter && npc.damage > 0;
        }

        public static bool CanBeVariant(NPC npc, Mod mod) 
        {
            return CanHaveLevel(npc) && !npc.friendly && !npc.boss && npc.type != NPCID.TargetDummy && npc.type != mod.Find<ModNPC>("ShadowAppendix").Type;
        }

        public static bool Chance(float chance) 
        {
            return Main.rand.NextFloat(0, 1f) < chance * 0.01f;
        }

        public static float DistanceFromNPC(Player player, NPC npc) 
        {
            return (npc.position - player.position).Length();
        }

        public static void NewCombatText(Player player, Color color, string text, bool crit = false, bool buff = false, float scale = 1, int timeleft = 60, float speed = 1) 
        {
            int i = CombatText.NewText(player.getRect(), color, text, crit, buff);
            Main.combatText[i].scale = scale;
            Main.combatText[i].lifeTime = timeleft;
            Main.combatText[i].velocity *= speed;
        }

        public static void DefaultDustEffect(Vector2 pos, int type, float speed) 
        {
            int precision = 36;
            for (int i = 0; i < precision; i++)
            {
                float angle = i * 360 / precision;
                float deg2Rad = MathF.PI / 180;
                float velx = speed * MathF.Cos(deg2Rad * angle);
                float vely = speed * MathF.Sin(deg2Rad * angle);
                Dust.NewDust(pos, 0, 0, type, velx, vely);
            }
        }

        public static void NewSoundFX(SoundStyle style, float volume, Vector2 pos) 
        {
            style.Volume = volume;
            SoundEngine.PlaySound(style, pos);
        }

        public static float GetSoulValue(NPC npc) 
        {
            return MathF.Floor(3 + 100 * (1 - (float)Math.Exp(-1.7e-4 * npc.lifeMax)));
        }

        public static int GetWorldLevel() 
        {
            return DModeWorld.LevelBonus;
        }

        public static bool IsCommonWeapon(Item item) 
        {
            return item.maxStack == 1 && item.damage > 0 && item.defense == 0 && !item.consumable && !item.accessory && !item.IsAir && !item.vanity && !IsCommonTool(item);
        }

        public static bool IsCommonArmor(Item item) 
        {
            return item.maxStack == 1 && item.defense > 0 && !item.consumable && !item.accessory && !item.IsAir && !item.vanity;
        }

        public static bool IsCommonTool(Item item) 
        {
            return item.pick > 0;
        }

        public static bool IsValidSkillSystemTarget(NPC target) 
        {
            return !target.friendly && !target.SpawnedFromStatue && target.type != NPCID.TargetDummy;
        }

        public static bool CanHaveAnySystem(Item item) 
        {
            return IsCommonWeapon(item) || IsCommonArmor(item) || IsCommonTool(item);
        }
    
        public static void GiveItem(Player player, int item, int amount) 
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(ModContent.ItemType<DestinyBox>()), item, amount);
        }

        public static void OpenDestinyBox(Player player) 
        {
            bool gotAnyTier = false;
            int amberPoints = 0;

            if (Chance(12.5f)) 
            {
                GiveItem(player, ItemID.Obsidian, 5);
                GiveItem(player, ItemID.Amethyst, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 5;
                gotAnyTier = true;
            }

            if(Chance(6.25f)) 
            {
                GiveItem(player, ItemID.Obsidian, 10);
                GiveItem(player, ItemID.Topaz, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 10;
                gotAnyTier = true;
            }

            if(Chance(3.13f)) 
            {
                GiveItem(player, ItemID.Obsidian, 15);
                GiveItem(player, ItemID.Sapphire, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 15;
                gotAnyTier = true;
            }

            if (Chance(1.56f))
            {
                GiveItem(player, ItemID.Obsidian, 20);
                GiveItem(player, ItemID.Emerald, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 20;
                gotAnyTier = true;
            }

            if (Chance(0.78f))
            {
                GiveItem(player, ItemID.Obsidian, 25);
                GiveItem(player, ItemID.Ruby, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 25;
                gotAnyTier = true;
            }

            if (Chance(0.39f))
            {
                GiveItem(player, ItemID.Obsidian, 30);
                GiveItem(player, ItemID.Diamond, 1);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 25);
                amberPoints += 30;
                gotAnyTier = true;
            }

            if (!gotAnyTier)
            {
                GiveItem(player, ItemID.SilverCoin, 5);
                GiveItem(player, ItemID.Obsidian, 5);
                GiveItem(player, ModContent.ItemType<MonstrousExperience>(), 5);
                amberPoints += 1;
            }

            NewCombatText(player, new Color(255, 180, 0), "You got " + amberPoints + " Amber Points!", false, false, 1.5f, 90, -1);
            player.GetModPlayer<DModePlayer>().AmberPoints += amberPoints;
        }

        public static int GetRandomInt(int min, int max) 
        {
            return Main.rand.Next(min, max + 1);
        }

        public static bool GreaterThanAll(int value, int[] others) 
        {
            for (int i = 0; i < others.Length; i++) 
            {
                if(value < others[i]) 
                {
                    return false;
                }
            }

            return true;
        }

        public static int FindInvID(Item item, Player player) 
        {
            int invID = -1;
            for(int i = 0; i < player.inventory.Length; i++) 
            {
                if(player.inventory[i].type == item.type) 
                {
                    invID = i;
                    break;
                }
            }
            return invID;
        }

        public static int FindInvIDwithShoot(int type, Player player) 
        {
            int id = -1;
            for(int i = 0; i < player.inventory.Length; i++) 
            {
                if(type == player.inventory[i].shoot) 
                {
                    id = i;
                    break;
                }
            }
            return id;
        }

        public static int GetArmorBonus(Player player, Item item, int quality)
        {
            int rarity = item.rare;
            if (rarity >= 0)
            {
                double max = 0.75 * rarity + 8;
                double armor = 1 + (1 - Math.Exp(-0.06 * quality)) * max;
                return (int)armor;
            }

            return 0;
        }
    }
}
