using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BeeBomb.Tiles
{
	public class BeeHouseTile : ModTile
	{
		const int FRAMES = 4;

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			Main.tileLavaDeath[Type] = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Bee House");
			AddMapEntry(new Color(39, 62, 68), name);
			animationFrameHeight = 38;
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.WorkBenches };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 32, j * 32, 32, 32, ModContent.ItemType<Items.BeeHouse>());
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (++frameCounter > 8)
			{
				frameCounter = 0;
				if (++frame >= FRAMES)
				{
					frame = 0;
				}
				else { }
			}
			else { }
		}
	}
}