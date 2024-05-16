using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using SuperDummy.Projectiles;

namespace SuperDummy.Items
{
    class SuperDummy : ModItem
    {
        public override void SetStaticDefaults()
        {
            /*string displayName = "Super Dummy";
            string tooltip =
                "Spawns a super dummy at your cursor\n" +
                "Can be detected by minions and homing projectiles\n" +
                "Right click to remove all spawned super dummies";

            DisplayName.SetDefault(displayName);
            Tooltip.SetDefault(tooltip);*/

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Blue;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == ItemAlternativeFunctionID.ActivatedAndUsed)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.SuperDummy>())
                            {
                                NPC npc = Main.npc[i];
                                npc.life = 0;
                                npc.HitEffect();
                                npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0, null, false, 0, true);
                                SoundEngine.PlaySound(SoundID.Dig, npc.position);
                            }
                        }
                    }
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.SuperDummy>())
                            {
                                SoundEngine.PlaySound(SoundID.Dig, Main.npc[i].position);
                            }
                        }
                        var net = Mod.GetPacket();
                        net.Write((byte)1);
                        net.Send();
                    }
                }
            }
            else if (NPC.CountNPCS(ModContent.NPCType<NPCs.SuperDummy>()) < 50)
            {
                Vector2 pos = new((int)Main.MouseWorld.X - 9, (int)Main.MouseWorld.Y - 20);
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), pos, Vector2.Zero, ModContent.ProjectileType<DummySpawn>(), 0, 0, player.whoAmI, ModContent.NPCType<NPCs.SuperDummy>());
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TargetDummy, 1);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
