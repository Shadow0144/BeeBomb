using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BeeBomb.Buffs;

namespace BeeBomb.Projectiles
{
    public class BeeBombProjectile : ModProjectile
    {
        private const int TIMER = 155;
        private const int RADIUS = 10;
        private const int BEES = 12;
        private const int BEES_SPEED = 1;
        private const int BEES_DAMAGE = 100;
        private const float BEES_KNOCKBACK = 10.0f;
        private const int HONEY_TIMER = 3000;

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
            base.OnHitNPC(target, damage, knockback, crit);
            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            
            ExplosionDamage();

            /*const int RADIUS = 4;
            for (int x = -RADIUS; x <= RADIUS; x++)
            {
                for (int y = -RADIUS; y <= RADIUS; y++)
                {
                    //int xPosition = (int)(x + position.X / 16.0f);
                    //int yPosition = (int)(y + position.Y / 16.0f);
                    if (Math.Sqrt(x * x + y * y) <= RADIUS + 0.5)
                    {
                        //WorldGen.KillTile(xPosition, yPosition, false, false, false);
                        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);
                    }
                    else { }
                }
            }*/

            Random random = new Random();
            for (int i = 0; i < BEES; i++)
            {
                int projectileID = Projectile.NewProjectile(projectile.Center, 
                    new Vector2(
                        ((float)random.NextDouble()) * BEES_SPEED * (random.NextDouble() > 0.5 ? 1 : -1), 
                        ((float)random.NextDouble()) * BEES_SPEED * (random.NextDouble() > 0.5 ? 1 : -1)),
                        ProjectileID.Bee, BEES_DAMAGE, BEES_KNOCKBACK);
                //Main.projectile[projectileID].friendly = true;
            }
        }

        public virtual void ExplosionDamage()
        {
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if (((dist / 16.0f) <= RADIUS) && (!npc.friendly))
                {
                    int dir = (dist > 0) ? 1 : -1;
                    npc.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                    npc.StrikeNPC(projectile.damage, projectile.knockBack, dir);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                float dist = Vector2.Distance(player.Center, projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (((dist / 16.0f) <= RADIUS) && (Main.netMode == NetmodeID.SinglePlayer))
                {
                    player.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir); // 1 - never crit
                    player.hurtCooldowns[0] += 15;
                }
                else if ((Main.netMode != NetmodeID.MultiplayerClient) && ((dist / 16.0f) <= RADIUS))
                {
                    player.AddBuff(ModContent.BuffType<HoneyedBuff>(), HONEY_TIMER, false);
                    NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir, false, pvp: true, 0); // 1 - never crit
                }
                else { }
            }

        }
    }
}
