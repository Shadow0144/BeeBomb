using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace BeeBomb.Projectiles
{
    public class BeeBombProjectile : ModProjectile
    {
        private const int TIMER = 55;
        private const int RADIUS = 10;
        private const int BEES_SPEED = 1;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Bomb");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 24;
			projectile.aiStyle = 16;
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
            int radius = 4;
            int bees = 12; 
            
            ExplosionDust(radius, projectile.Center, shake: false);
            ExplosionDamage();

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    //int xPosition = (int)(x + position.X / 16.0f);
                    //int yPosition = (int)(y + position.Y / 16.0f);
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        //WorldGen.KillTile(xPosition, yPosition, false, false, false);
                        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);
                    }
                    else { }
                }
            }

            Random random = new Random();
            for (int i = 0; i < bees; i++)
            {
                int projectileID = Projectile.NewProjectile(projectile.Center, 
                    new Vector2(
                        ((float)random.NextDouble()) * BEES_SPEED * (random.NextDouble() > 0.5 ? 1 : -1), 
                        ((float)random.NextDouble()) * BEES_SPEED * (random.NextDouble() > 0.5 ? 1 : -1)),
                        ProjectileID.Bee, 350, 10);
                Main.projectile[projectileID].friendly = true;
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
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir); // 1 - never crit
                    player.hurtCooldowns[0] += 15;
                }
                else if ((Main.netMode != NetmodeID.MultiplayerClient) && ((dist / 16.0f) <= RADIUS))
                {
                    NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * 1), dir, false, pvp: true, 0); // 1 - never crit
                }
                else { }
            }

        }

        public static void ExplosionDust(int Radius, Vector2 Center, Color color = default, Color cloudColor = default, int type = 1, Vector2 Direction = default, bool shake = true, int dustType = 6)
        {
            // Check to see if the type is 1, 2 or 3, else default to 1
            List<int> types = new List<int> { 1, 2, 3, 4 };

            if (!types.Contains(type))
            {
                type = 1;
            }
            else { }

            //What to preform once the type has been found
            switch (type)
            {
                case 1:
                    //Type1Dust(Radius, Center, color, cloudColor, DustType: dustType);
                    break;
                case 2:
                    //Type2Dust(Radius, Center, color, cloudColor, Direction);
                    break;
                case 3:
                    //Type3Dust(Radius, Center, shader, color, cloudColor, DustType: dustType);
                    break;
                case 4:

                    break;
                default:

                    break;
            }

            // Lighting - brief flash
            Lighting.AddLight(Center, new Vector3(Radius / 2.3f, Radius / 2.3f, Radius / 2.3f));
            Lighting.maxX = Radius / 10;
            Lighting.maxY = Radius / 10;
        }
    }
}
