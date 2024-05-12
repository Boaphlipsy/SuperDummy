using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SuperDummy.Projectiles
{
    class DummySpawn : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Super Dummy Spawn");
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            var n = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int)Projectile.Center.X, (int)Projectile.Center.Y, (int)Projectile.ai[0]);
            if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.SyncNPC, number: n);
        }
    }
}
