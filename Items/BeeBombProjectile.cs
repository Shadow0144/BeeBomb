using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
    public class BeeBombProjectile : ModProjectile
    {
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
            //int bee_cols = 1;
            //int bee_spacing = 12;

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
                        ProjectileID.Bee, 35, 10);
                //Main.projectile[projectileID].friendly = true;
            }

            /*for (int i = 0; i < bee_cols; i++)
            {
                for (int j = 0; j < bee_cols; j++)
                {
                    //int beeID = NPC.NewNPC((int)position.X+(i*bee_spacing)-bee_spacing, (int)position.Y+(j*bee_spacing)-bee_spacing, NPCID.Bee);
                    //Main.npc[beeID].friendly = true;
                    //Item.NewItem(projectile.getRect(), mod.ItemType("BeeBombBee"), 6);
                    //Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType("BeeBombBee"), 35, 10);
                }
            }*/
        }
    }
}
