using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BeeBomb.Buffs;
using BeeBomb.Dusts;

namespace BeeBomb.Projectiles
{
    public class BeeBombProjectile : ModProjectile
    {
        // Dynamite is 250, but we'll scale back because it is has to burst through a hive and does a lot of bee damage and debuffs
        private const int DAMAGE = 200;
        private const int TIMER = 100; // 180 is the default
        private const int FIRE_RADIUS = 4 * 16;
        private const int RADIUS = 8 * 16;
        private const int BEES = 12;
        private const double BEES_OFFSET = 1.0;
        private const double BEES_SPEED = 1.0;
        private const int BEES_DAMAGE = 10;
        private const float BEES_KNOCKBACK = 10.0f;
        private const int HONEY_TIMER = 500;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Bomb");
        }

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 24;
            projectile.CloneDefaults(ProjectileID.StickyBomb);
            aiType = ProjectileID.StickyBomb;
            projectile.damage = DAMAGE;
            projectile.timeLeft = TIMER;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 16;
            height = 16;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
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

            Random random = new Random();
            for (int i = 0; i < BEES; i++)
            {
                double angle = ((2.0 * Math.PI * i) / BEES) + (Math.PI / BEES);
                Vector2 offset = new Vector2(((float)(Math.Cos(angle) * BEES_OFFSET)), ((float)(Math.Sin(angle) * BEES_OFFSET)));
                double direction = (2.0 * Math.PI * random.NextDouble());
                //if (WorldGen.TileEmpty(projectile.Center + offset))
                {
                    Projectile.NewProjectile(projectile.Center + offset,
                        new Vector2(((float)(Math.Cos(direction) * BEES_SPEED)), ((float)(Math.Sin(direction) * BEES_SPEED))),
                        ModContent.ProjectileType<Projectiles.BeeBombBee>(), BEES_DAMAGE, BEES_KNOCKBACK, projectile.owner);
                }
                //else { }
            }
        }

        public override void AI()
        {
            base.AI();
            projectile.damage = DAMAGE;
        }

        public virtual void ExplosionDamage()
        {
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                mod.Logger.Info("Distance: " + dist);
                if ((dist <= RADIUS) && (!npc.friendly))
                {
                    mod.Logger.Info("In range");
                    int dir = (dist > 0) ? 1 : -1;
                    npc.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                    //npc.StrikeNPC(projectile.damage, projectile.knockBack, dir);
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
                    //player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir); // 1 - never crit
                    player.hurtCooldowns[0] += 15;
                }
                else if ((Main.netMode != NetmodeID.MultiplayerClient) && (dist <= RADIUS))
                {
                    player.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                    //NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir, false, pvp: true, 0); // 1 - never crit
                }
                else { }
            }

        }
    }
}
