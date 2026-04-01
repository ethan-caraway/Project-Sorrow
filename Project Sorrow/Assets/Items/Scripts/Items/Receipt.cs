namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Receipt item.
	/// </summary>
	public class Receipt : Item
	{
		#region Class Constructors

		public Receipt ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool OnItemSellPrice ( )
		{
			// Return that the item should sell for full price
			return true;
		}

		public override bool OnConsumableSellPrice ( )
		{
			// Return that the consumable should sell for full price
			return true;
		}

		#endregion // Item Override Functions
	}
}