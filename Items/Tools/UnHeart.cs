using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DMode.Items.Tools
{
	public class UnHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("UnHeart");
            Tooltip.SetDefault("You should not have this.");
        }

        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 26;
			Item.maxStack = 1;
			Item.rare = -12;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = 4;
            Item.useTurn = true;
            
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            int k = Projectile.NewProjectile(player.GetSource_Misc("DMODE UnHeart"), player.position, player.velocity*0, Mod.Find<ModProjectile>("Soul").Type, 0, 0);
            Main.projectile[k].ai[1] = 100;
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item4, player.position);

            DModePlayer modplayer = player.GetModPlayer<DModePlayer>();

            modplayer.GeneralLevel = 0;
            modplayer.maxGeneralLevel = 10;
            modplayer.maxCrystalUpgrades = 10;
            modplayer.SoulPoints = 0;
            modplayer.MaxSoulPoints = 80;

            modplayer.Strength = 0;
            modplayer.Mind = 0;
            modplayer.Dexterity = 0;
            modplayer.Spirit = 0;

            modplayer.strengthExp = 0;
            modplayer.mindExp = 0;
            modplayer.dexterityExp = 0;
            modplayer.spiritExp = 0;

            modplayer.strengthMaxExp = 0;
            modplayer.mindMaxExp = 0;
            modplayer.dexterityMaxExp = 0;
            modplayer.spiritMaxExp = 0;

            modplayer.DestinyPoints = 0;
            modplayer.AmberPoints = 0;

            return true;
        }
	}
}
