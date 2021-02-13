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
            base.Update(player, ref buffIndex);
            player.moveSpeed *= SPEED_MOD;
            player.honeyWet = true;
            addHoney(player.position, player.width, player.height, player.velocity);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
            npc.velocity.X *= SPEED_MOD;
            npc.velocity.Y *= SPEED_MOD;
            npc.honeyWet = true;
            addHoney(npc.position, npc.width, npc.height, npc.velocity);
        }

        private void addHoney(Vector2 position, int width, int height, Vector2 velocity)
        {
            position.X -= 2.0f;
            position.Y -= 2.0f;
            if (Main.rand.Next(2) == 0)
            {
                int index = Dust.NewDust(position, width + 4, height + 2,
                    ModContent.DustType<Dusts.HoneyDust>(), 0.0f, 0.0f, 50, new Color(), 0.8f);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index].alpha += 25;
                }
                else { }
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index].alpha += 25;
                }
                else { }
                Main.dust[index].noLight = true;
                Dust dust1 = Main.dust[index];
                dust1.velocity *= 0.2f;
                Main.dust[index].velocity.Y += 0.200000002980232f;
                Dust dust2 = Main.dust[index];
                dust2.velocity += velocity;
            }
            else
            {
                int index = Dust.NewDust(position, width + 8, height + 8,
                    ModContent.DustType<Dusts.HoneyDust>(), 0.0f, 0.0f, 50, new Color(), 1.1f);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index].alpha += 25;
                }
                else { }
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index].alpha += 25;
                }
                else { }
                Main.dust[index].noLight = true;
                Main.dust[index].noGravity = true;
                Dust dust1 = Main.dust[index];
                dust1.velocity *= 0.2f;
                Main.dust[index].velocity.Y += 1.0f;
                Dust dust2 = Main.dust[index];
                dust2.velocity += velocity;
            }
        }
    }
}
