using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
	public class BeeHouse : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 32;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Orange;
			item.value = Item.buyPrice(0, 20, 0, 0);
			item.createTile = ModContent.TileType<Tiles.BeeHouseTile>();
		}

		// Temporary
		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}*/
	}
}