using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BeeBomb.Tiles
{
	public class BeeHouseTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			Main.tileLavaDeath[Type] = true;
			ModTranslation name = CreateMapEntryName();
			AddMapEntry(new Color(39, 62, 68), name);
			//animationFrameHeight = 56;
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.WorkBenches };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<Items.BeeHouse>());
		}

		/*public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = Main.tileFrame[TileID.LunarMonolith];
			frameCounter = Main.tileFrameCounter[TileID.LunarMonolith];
		}*/
	}
}