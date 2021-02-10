using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Buffs
{
    public class HoneyedBuff : ModBuff
    {
        private const float SPEED_MOD = 0.5f;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Honey'd");
            Description.SetDefault("Slows movement.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= SPEED_MOD;
        }
    }
}
