using Terraria;
using Terraria.ModLoader;

namespace BeeBomb.Dusts
{
	public class HoneyDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true;
		}
	}
}