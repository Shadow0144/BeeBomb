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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
