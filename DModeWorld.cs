using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DModeRemastered
{
    public class DModeWorld : ModSystem
    {
        public static int LevelBonus = 0;
        public static double BloodValue= 0;
        public static double BloodValueMax = 90;

        /*
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(LevelBonus);
            writer.Write(BloodValue);
            writer.Write(BloodValueMax);
        }

        public override void NetReceive(BinaryReader reader)
        {
            LevelBonus = reader.ReadInt32();
            BloodValue = reader.ReadDouble();
            BloodValueMax = reader.ReadDouble();
        }*/

        public override void SaveWorldData(TagCompound tag)/* Suggestion: Edit tag parameter rather than returning new TagCompound */
        {
            tag.Add("LevelBonus", LevelBonus);
            tag.Add("BloodValue", BloodValue);
            tag.Add("BloodValueMax", BloodValueMax);

        }
        public override void LoadWorldData(TagCompound tag)
        {
            LevelBonus = tag.GetInt("LevelBonus");
            BloodValue = tag.GetDouble("BloodValue");
            BloodValueMax = tag.GetDouble("BloodValueMax");
        }
    }
}
