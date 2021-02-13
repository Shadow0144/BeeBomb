using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BeeBomb.Buffs;

namespace BeeBomb.Projectiles
{
    public class BeeBombBee : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.CloneDefaults(ProjectileID.Bee);
            projectile.penetrate = 3; // 3 is default
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = -oldVelocity;
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; ++i)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 150, 
                    projectile.velocity.X, projectile.velocity.Y, 50, new Color(), 1.0f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.0f;
            }
            base.Kill(timeLeft);
        }
    }
}
