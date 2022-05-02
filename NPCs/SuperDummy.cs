using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SuperDummy.NPCs
{
    class SuperDummy : ModNPC
    {
        private bool recentlyHit = false;
        private bool onSpawn = true;
        private int hitDirection = 0;
        private int amountOfFrames = 10;
        private int height = 50;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Dummy");
            NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TargetDummy);
            NPC.lifeMax = int.MaxValue;
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = this.height;
            NPC.immortal = false;
            NPC.npcSlots = 0;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit15;
            Main.npcFrameCount[NPC.type] = this.amountOfFrames;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
            NPC.life = NPC.lifeMax;

            if (onSpawn)
            {
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];

                if (NPC.position.X - target.position.X > 0)
                {
                    //Facing Right
                    NPC.spriteDirection = -1;
                }
                else
                {
                    //Facing Left
                    NPC.spriteDirection = 1;
                }

                onSpawn = false;
            }

            HitAnimation();
        }

        public void HitAnimation()
        {
            if (recentlyHit == true)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 6)
                {
                    //Facing Left
                    if (NPC.spriteDirection == 1)
                    {
                        //Damage from Left
                        if (hitDirection == 1)
                        {
                            if (NPC.frame.Y < height * 1)
                            {
                                NPC.frame.Y = height * 1;
                            }
                            NPC.frame.Y += height;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y == height * 5)
                            {
                                NPC.frame.Y = 0;
                                recentlyHit = false;
                            }
                        }
                        //Damage from Right
                        else if (hitDirection == -1)
                        {
                            if (NPC.frame.Y < height * 6)
                            {
                                NPC.frame.Y = height * 6;
                            }
                            NPC.frame.Y += height;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y == height * 10)
                            {
                                NPC.frame.Y = 0;
                                recentlyHit = false;
                            }
                        }
                    }
                    //Facing Right
                    else
                    {
                        //Damage from Left
                        if (hitDirection == 1)
                        {
                            if (NPC.frame.Y < height * 6)
                            {
                                NPC.frame.Y = height * 6;
                            }
                            NPC.frame.Y += height;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y == height * 10)
                            {
                                NPC.frame.Y = 0;
                                recentlyHit = false;
                            }
                        }
                        //Damage from Right
                        else if (hitDirection == -1)
                        {
                            if (NPC.frame.Y < height * 1)
                            {
                                NPC.frame.Y = height * 1;
                            }
                            NPC.frame.Y += height;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y == height * 5)
                            {
                                NPC.frame.Y = 0;
                                recentlyHit = false;
                            }
                        }
                    }
                }
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            recentlyHit = true;
            NPC.frame.Y = 0;
            //Facing Left
            if (NPC.spriteDirection == 1)
            {
                //Damage from Left
                if (hitDirection == 1)
                {
                    NPC.frame.Y = height * 1;
                }
                //Damage from Right
                else if (hitDirection == -1)
                {
                    NPC.frame.Y = height * 5;
                }
            }
            //Facing Right
            else
            {
                //Damage from Left
                if (hitDirection == 1)
                {
                    NPC.frame.Y = height * 5;
                }
                //Damage from Right
                else if (hitDirection == -1)
                {
                    NPC.frame.Y = height * 1;
                }
            }
            NPC.frameCounter = 0;
            this.hitDirection = hitDirection;

            base.HitEffect(hitDirection, damage);
        }

        public override bool CheckDead()
        {
            return false;
        }

        public override bool CheckActive()
        {
            return false;
        }    
    }
}
