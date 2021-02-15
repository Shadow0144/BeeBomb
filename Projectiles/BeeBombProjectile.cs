using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BeeBomb.Buffs;

namespace BeeBomb.Projectiles
{
    public class BeeBombProjectile : ModProjectile
    {
        // Dynamite is 250, but we'll scale back because it is has to burst through a hive, doesn't break tiles, and does a lot of bee damage and debuffs
        private const int DAMAGE = 150;
        private const int TIMER = 100; // 180 is the default
        private const int FIRE_RADIUS = 4 * 16;
        private const int RADIUS = 8 * 16;
        private const int LOW_BEES = 35; // Beenade launches 15-25
        private const int HIGH_BEES = 45; // Beenade launches 15-25
        private const int BEES_DAMAGE = 12; // 12 is default
        private const float BEES_KNOCKBACK = 1.0f;
        private const int HONEY_TIMER = 500;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Bomb");
        }

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 48;
            projectile.CloneDefaults(ProjectileID.StickyBomb);
            aiType = ProjectileID.StickyBomb;
            projectile.damage = DAMAGE;
            projectile.timeLeft = TIMER;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 32;
            height = 32;
            fallThrough = true; // Will not fall through because of sticky AI
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.position;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            
            ExplosionDamage();

            const int STEP = 16;
            for (int x = -RADIUS; x <= RADIUS; x += STEP)
            {
                for (int y = -RADIUS; y <= RADIUS; y += STEP)
                {
                    Vector2 offset = new Vector2(x, y);
                    float dist = offset.Length();
                    if (dist <= RADIUS + 0.5)
                    {
                        Dust.NewDust(position + offset, 10, 10, ModContent.DustType<Dusts.HoneyDust>());
                        if (dist <= FIRE_RADIUS + 0.5)
                        {
                            Dust.NewDust(position + offset, 10, 10, DustID.Fire);
                        }
                        else { }
                    }
                    else { }
                }
            }

            // Bee-spawning method used by the beenade
            if (projectile.owner == Main.myPlayer)
            {
                int num = Main.rand.Next(LOW_BEES, HIGH_BEES);
                for (int i = 0; i < num; ++i)
                {
                    Projectile.NewProjectile((float)projectile.Center.X, (float)projectile.Center.Y, // Using position seems to spawn them in the wrong location
                        (float)Main.rand.Next(-35, 36) * 0.02f, (float)Main.rand.Next(-35, 36) * 0.02f,
                        Main.player[projectile.owner].beeType(), Main.player[projectile.owner].beeDamage(BEES_DAMAGE),
                        Main.player[projectile.owner].beeKB(BEES_KNOCKBACK), Main.myPlayer, 0.0f, 0.0f);
                }
            }
            else { }
        }

        public override void AI()
        {
            base.AI();
            projectile.damage = DAMAGE; // Damage is normally overridden when the bomb begins to exlode
        }

        public virtual void ExplosionDamage()
        {
            // Damage is handled by the base AI

            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if ((dist <= RADIUS) && (!npc.friendly))
                {
                    npc.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                float dist = Vector2.Distance(player.Center, projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if ((dist <= RADIUS) && (Main.netMode == NetmodeID.SinglePlayer))
                {
                    player.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                }
                else if ((Main.netMode != NetmodeID.MultiplayerClient) && (dist <= RADIUS))
                {
                    player.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                }
                else { }
            }

        }
    }
}
