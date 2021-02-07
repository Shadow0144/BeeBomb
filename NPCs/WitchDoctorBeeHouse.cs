using BeeBomb.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.NPCs
{
	public class WitchDoctorBeeHouse : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.WitchDoctor 
				&& NPC.downedQueenBee 
				&& Main.player[Main.myPlayer].ZoneJungle)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<BeeHouse>());
				nextSlot++;
			}
			else { }
		}
	}
}