using Terraria;
using Terraria.ModLoader;

namespace BeeBomb.Dusts
{
	public class HoneyDripDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust = Dust.CloneDust(211); // Borrow the settings from dripping
		}
	}
}