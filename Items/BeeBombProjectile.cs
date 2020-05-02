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
			projectile.arrow = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.maxPenetrate = -1;
			projectile.tileCollide = true;
            projectile.timeLeft = 30;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 4;
            int bee_cols = 3;
            int bee_spacing = 12;

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

            for (int i = 0; i < bee_cols; i++)
            {
                for (int j = 0; j < bee_cols; j++)
                {
                    NPC.NewNPC((int)position.X+(i*bee_spacing)-bee_spacing, (int)position.Y+(j*bee_spacing)-bee_spacing, NPCID.Bee);
                }
            }
        }
    }
}
