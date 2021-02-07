using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
	public class BeeHouse : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Green;
			item.value = Item.buyPrice(0, 6, 0, 0);
			item.createTile = ModContent.TileType<Tiles.BeeHouseTile>();
		}
	}
}