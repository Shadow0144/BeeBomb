using Terraria.ModLoader;

namespace BeeBomb.Projectiles
{
    public class BeeBombBee : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Bomb Bee");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.maxPenetrate = 0;
            projectile.tileCollide = true;
            projectile.timeLeft = 135;
        }
    }
}
