namespace FlightPaper.ProjectSorrow.Shop
{
	/// <summary>
	/// This class stores the data for an item in the shop.
	/// </summary>
	public class ShopItemModel
	{
		#region Item Data

		/// <summary>
		/// The data for the item.
		/// </summary>
		public Items.ItemScriptableObject Item;

		/// <summary>
		/// The price for the item.
		/// </summary>
		public int Price;

		#endregion // Item Data
	}
}