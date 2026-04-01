namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class stores the data for the shop.
	/// </summary>
	[System.Serializable]
	public class ShopModel
	{
		#region Shop Data Constants

		/// <summary>
		/// The default number of items available in the shop.
		/// </summary>
		public const int DEFAULT_ITEM_COUNT = 3;

		/// <summary>
		/// The default number of consumables available in the shop.
		/// </summary>
		public const int DEFAULT_CONSUMABLE_COUNT = 2;

		#endregion // Shop Data Constants

		#region Shop Data

		/// <summary>
		/// The number of items available in the shop.
		/// </summary>
		public int ItemCount = DEFAULT_ITEM_COUNT;

		/// <summary>
		/// The number of consumables available in the shop.
		/// </summary>
		public int ConsumableCount = DEFAULT_CONSUMABLE_COUNT;

		/// <summary>
		/// The prices for items and consumables in the shop.
		/// </summary>
		public PriceModel Prices = new PriceModel ( );

		/// <summary>
		/// The rarity chances for items and consumables in the shop.
		/// </summary>
		public RarityModel RarityChances = new RarityModel ( );

		/// <summary>
		/// The start price for a reroll.
		/// </summary>
		public int RerollStartPrice = 5;

		/// <summary>
		/// The amount of money that the reroll price increases with each reroll.
		/// </summary>
		public int RerollPriceIncrement = 3;


		#endregion // Shop Data
	}
}