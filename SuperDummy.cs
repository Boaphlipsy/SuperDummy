using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SuperDummy
{
	public class SuperDummy : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte messageType = reader.ReadByte();

            switch (messageType)
            {
                case 1:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.SuperDummy>())
                            {
                                NPC npc = Main.npc[i];
                                npc.life = 0;
                                npc.HitEffect();
                                npc.SimpleStrikeNPC(int.MaxValue, 0);

                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, i);
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}