using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeeBomb.Items
{
	public class BeeBomb : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Bee Bomb"); // By default, capitalization in classnames will add spaces to the display name
			Tooltip.SetDefault("Explodes into a swarm of bees!");
		}

		public override void SetDefaults() 
		{
			item.thrown = true;
			item.noMelee = true;
			item.width = 16;
			item.height = 24;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(silver: 30);
			item.rare = ItemRarityID.Orange;
			item.maxStack = 99;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = ModContent.ProjectileType<Projectiles.BeeBombProjectile>();
			item.shootSpeed = 8f;
			item.useTurn = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Hive, 5); // Switch to BeeHive (1) when 1.4 is available
			recipe.AddIngredient(ItemID.Dynamite, 1);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}