namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Inventory Ledger item.
	/// </summary>
	public class InventoryLedger : Item
	{
		#region Class Constructors

		public InventoryLedger ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ).ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "10" );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Apply applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID )
			};
		}

		public override bool OnReroll ( )
		{
			// Increment applause
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 10 );

			// Return that this item was triggered
			return true;
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 10 );
		}

		#endregion // Item Override Functions
	}
}