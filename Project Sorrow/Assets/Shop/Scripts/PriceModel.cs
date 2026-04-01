namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class stores the data for buy and sell prices of items.
	/// </summary>
	[System.Serializable]
	public class PriceModel
	{
		#region Price Data Constants

		/// <summary>
		/// The default price for common items in the shop.
		/// </summary>
		public const int DEFAULT_COMMON_ITEM_PRICE = 7;

		/// <summary>
		/// The default price for uncommon items in the shop.
		/// </summary>
		public const int DEFAULT_UNCOMMON_ITEM_PRICE = 10;

		/// <summary>
		/// The default price for rare items in the shop.
		/// </summary>
		public const int DEFAULT_RARE_ITEM_PRICE = 15;

		/// <summary>
		/// The default price for legendary items in the shop.
		/// </summary>
		public const int DEFAULT_LEGENDARY_ITEM_PRICE = 20;

		/// <summary>
		/// The default price for common consumables in the shop.
		/// </summary>
		public const int DEFAULT_COMMON_CONSUMABLE_PRICE = 3;

		/// <summary>
		/// The default price for uncommon consumables in the shop.
		/// </summary>
		public const int DEFAULT_UNCOMMON_CONSUMABLE_PRICE = 5;

		/// <summary>
		/// The default price for rare consumables in the shop.
		/// </summary>
		public const int DEFAULT_RARE_CONSUMABLE_PRICE = 7;

		/// <summary>
		/// The default price for legendary consumables in the shop.
		/// </summary>
		public const int DEFAULT_LEGENDARY_CONSUMABLE_PRICE = 10;

		#endregion // Price Data Constants

		#region Price Data

		/// <summary>
		/// The amount of money required to purchase a common item.
		/// </summary>
		public int CommonItemPrice = DEFAULT_COMMON_ITEM_PRICE;

		/// <summary>
		/// The amount of money required to purchase an uncommon item.
		/// </summary>
		public int UncommonItemPrice = DEFAULT_UNCOMMON_ITEM_PRICE;

		/// <summary>
		/// The amount of money required to purchase a rare item.
		/// </summary>
		public int RareItemPrice = DEFAULT_RARE_ITEM_PRICE;

		/// <summary>
		/// The amount of money required to purchase a legendary item.
		/// </summary>
		public int LegendaryItemPrice = DEFAULT_LEGENDARY_ITEM_PRICE;

		/// <summary>
		/// The amount of money required to purchase a common consumable.
		/// </summary>
		public int CommonConsumablePrice = DEFAULT_COMMON_CONSUMABLE_PRICE;

		/// <summary>
		/// The amount of money required to purchase an uncommon consumable.
		/// </summary>
		public int UncommonConsumablePrice = DEFAULT_UNCOMMON_CONSUMABLE_PRICE;

		/// <summary>
		/// The amount of money required to purchase a rare consumable.
		/// </summary>
		public int RareConsumablePrice = DEFAULT_RARE_CONSUMABLE_PRICE;

		/// <summary>
		/// The amount of money required to purchase a legendary consumable.
		/// </summary>
		public int LegendaryConsumablePrice = DEFAULT_LEGENDARY_CONSUMABLE_PRICE;

		#endregion // Price Data

		#region Public Functions

		/// <summary>
		/// Modifies the prices by a percentage.
		/// </summary>
		/// <param name="modifier"> The percentage to increase or decrease prices by. </param>
		public void ModifyPrices ( float modifier )
		{
			// Modify each item price
			CommonItemPrice = ModifyPrice ( CommonItemPrice, modifier );
			UncommonItemPrice = ModifyPrice ( UncommonItemPrice, modifier );
			RareItemPrice = ModifyPrice ( RareItemPrice, modifier );
			LegendaryItemPrice = ModifyPrice ( LegendaryItemPrice, modifier );

			// Modify each consumable price
			CommonConsumablePrice = ModifyPrice ( CommonConsumablePrice, modifier );
			UncommonConsumablePrice = ModifyPrice ( UncommonConsumablePrice, modifier );
			RareConsumablePrice = ModifyPrice ( RareConsumablePrice, modifier );
			LegendaryConsumablePrice = ModifyPrice ( LegendaryConsumablePrice, modifier );
		}

		/// <summary>
		/// Gets the price of an item by its rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item. </param>
		/// <returns> The price of the item. </returns>
		public int GetItemPrice ( Enums.Rarity rarity )
		{
			// Get price
			int price = 0;

			// Check rarity
			switch ( rarity )
			{
				// Get common price
				case Enums.Rarity.COMMON:
					price = CommonItemPrice;
					break;

				// Get uncommon price
				case Enums.Rarity.UNCOMMON:
					price = UncommonItemPrice;
					break;

				// Get rare price
				case Enums.Rarity.RARE:
					price = RareItemPrice;
					break;

				// Get legendary price
				case Enums.Rarity.LEGENDARY:
					price = LegendaryItemPrice;
					break;
			}

			// Return a minimum price of 1
			return price < 1 ? 1 : price;
		}

		/// <summary>
		/// Gets the sell price of an item by its rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the item. </param>
		/// <param name="isFullPrice"> Whether or not the item sells for full price. </param>
		/// <returns> The sell price of the item. </returns>
		public int GetItemSellPrice ( Enums.Rarity rarity, bool isFullPrice )
		{
			// Get price
			int price = 0;

			// Check rarity
			switch ( rarity )
			{
				// Get common price
				case Enums.Rarity.COMMON:
					price = CommonItemPrice;
					break;

				// Get uncommon price
				case Enums.Rarity.UNCOMMON:
					price = UncommonItemPrice;
					break;

				// Get rare price
				case Enums.Rarity.RARE:
					price = RareItemPrice;
					break;

				// Get legendary price
				case Enums.Rarity.LEGENDARY:
					price = LegendaryItemPrice;
					break;
			}

			// Check for full price sale
			if ( !isFullPrice )
			{
				price /= 2;
			}

			// Return a minimum price of 1
			return price < 1 ? 1 : price;
		}

		/// <summary>
		/// Gets the price of an consumable by its rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the consumable. </param>
		/// <returns> The price of the consumable. </returns>
		public int GetConsumablePrice ( Enums.Rarity rarity )
		{
			// Get price
			int price = 0;

			// Check rarity
			switch ( rarity )
			{
				// Get common price
				case Enums.Rarity.COMMON:
					price = CommonConsumablePrice;
					break;

				// Get uncommon price
				case Enums.Rarity.UNCOMMON:
					price = UncommonConsumablePrice;
					break;

				// Get rare price
				case Enums.Rarity.RARE:
					price = RareConsumablePrice;
					break;

				// Get legendary price
				case Enums.Rarity.LEGENDARY:
					price = LegendaryConsumablePrice;
					break;
			}

			// Return a minimum price of 1
			return price < 1 ? 1 : price;
		}

		/// <summary>
		/// Gets the sell price of an consumable by its rarity.
		/// </summary>
		/// <param name="rarity"> The rarity of the consumable. </param>
		/// <param name="isFullPrice"> Whether or not the consumable sells for full price. </param>
		/// <returns> The sell price of the consumable. </returns>
		public int GetConsumableSellPrice ( Enums.Rarity rarity, bool isFullPrice )
		{
			// Get price
			int price = 0;

			// Check rarity
			switch ( rarity )
			{
				// Get common price
				case Enums.Rarity.COMMON:
					price = CommonConsumablePrice;
					break;

				// Get uncommon price
				case Enums.Rarity.UNCOMMON:
					price = UncommonConsumablePrice;
					break;

				// Get rare price
				case Enums.Rarity.RARE:
					price = RareConsumablePrice;
					break;

				// Get legendary price
				case Enums.Rarity.LEGENDARY:
					price = LegendaryConsumablePrice;
					break;
			}

			// Check for full price sale
			if ( !isFullPrice )
			{
				price /= 2;
			}

			// Return a minimum price of 1
			return price < 1 ? 1 : price;
		}

		#endregion // Public Functions

		#region Private Functions

		/// <summary>
		/// Modifies a price by a percentage.
		/// </summary>
		/// <param name="price"> The price to be modified. </param>
		/// <param name="modifier"> The percentage to increase or decrease prices by. </param>
		/// <returns> The modified price. </returns>
		private int ModifyPrice ( int price, float modifier )
		{
			// Get the new price
			int newPrice = (int)( price * modifier );

			// Return at least a price of $1
			return newPrice < 1 ? 1 : newPrice;
		}

		#endregion // Private Functions
	}
}