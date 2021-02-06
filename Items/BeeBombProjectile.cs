using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
    public class BeeBombProjectile : ModProjectile
    {
        private int radius = 10;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Bomb");
		}

		public override void SetDefaults()
		{
			projectile.arrow = false;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.maxPenetrate = 0;
			projectile.tileCollide = true;
            projectile.timeLeft = 35;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(new Vector2());
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
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, false);
                        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);
                    }
                    else { }
                }
            }

            Random random = new Random();
            float speed = projectile.velocity.Length();
            for (int i = 0; i < bees; i++)
            {
                int projectileID = Projectile.NewProjectile(projectile.position, 
                    new Vector2(
                        ((float)random.NextDouble()) * speed * (random.NextDouble() > 0.5 ? 1 : -1), 
                        ((float)random.NextDouble()) * speed * (random.NextDouble() > 0.5 ? 1 : -1)),
                        ProjectileID.Bee, 350, 10);
                Main.projectile[projectileID].friendly = true;
            }
        }

        public virtual void ExplosionDamage()
        {
            //if (Main.player[projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if (dist / 16f <= radius && !npc.friendly)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    //npc.StrikeNPC(projectile.damage, projectile.knockBack, dir, crit);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                //if (player.EE().BlastShielding &&
                //    player.EE().BlastShieldingActive) continue;
                float dist = Vector2.Distance(player.Center, projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer)
                {
                    //player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient && dist / 16f <= radius)
                {
                    //NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }

        }

        public static void ExplosionDust(int Radius, Vector2 Center, Color color = default, Color cloudColor = default, int type = 1, Vector2 Direction = default, bool shake = true, int dustType = 6)
        {
            //Check to see if the type is 1, 2 or 3, else default to 1
            List<int> types = new List<int> { 1, 2, 3, 4 };

            if (!types.Contains(type))
            {
                type = 1;
            }

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

            //lighting - brief flash
            Lighting.AddLight(Center, new Vector3(Radius / 2.3f, Radius / 2.3f, Radius / 2.3f));
            Lighting.maxX = Radius / 10;
            Lighting.maxY = Radius / 10;

            //shake 
            if (Radius >= 15 && type != 2 && shake)
            {
                //Main.LocalPlayer.EE().shake = true;
            }
        }

    }
}
