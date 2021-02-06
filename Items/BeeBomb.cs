using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
	public class BeeBomb : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("BeeBomb"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Explodes into a swarm of bees!");
		}

		public override void SetDefaults() 
		{
			item.thrown = true;
			item.noMelee = true;
			item.width = 10;
			item.height = 10;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 99;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("BeeBombProjectile");
			item.shootSpeed = 8f;
			item.useTurn = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 99);
			recipe.AddRecipe();
		}
	}
}