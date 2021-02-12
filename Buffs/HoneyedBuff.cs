using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace BeeBomb.Buffs
{
    public class HoneyedBuff : ModBuff
    {
        private const float SPEED_MOD = 0.5f;
        private Color COLOR = new Color(227, 222, 68, 255);

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Honey'd");
            Description.SetDefault("Movement speed extremely decreased; taste extremely increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetImmuneAlpha(COLOR, 1);
            //Main.buffColor(COLOR);
            //GameShaders.Armor.Apply(2869, player);
            //GameShaders.Armor.ApplySecondary(2869, player);
            player.moveSpeed *= SPEED_MOD;
            player.dripping = true;
            player.honeyWet = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
            npc.velocity.X *= SPEED_MOD;
            npc.velocity.Y *= SPEED_MOD;
            if (npc.buffTime[buffIndex] <= 0)
            {
                npc.honeyWet = false;
                npc.dripping = false;
                npc.color = default(Color);
            }
            else
            {
                npc.honeyWet = true;
                npc.dripping = true;
                npc.color = COLOR;
            }
        }
    }
}
