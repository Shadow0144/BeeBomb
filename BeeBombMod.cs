using Terraria.ID;
using Terraria.ModLoader;
using BeeBomb.Tiles;

namespace BeeBomb
{
	public class BeeBombMod : Mod
	{
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this);
			//recipe.AddIngredient(ItemID.BottledHoney, 1);
			recipe.AddIngredient(ItemID.Daybloom, 1);
			recipe.AddTile(ModContent.TileType<BeeHouseTile>());
			recipe.SetResult(ItemID.Hive, 10);
			recipe.AddRecipe();
		}
	}
}