namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Meal Ticket item.
	/// </summary>
	public class MealTicket : Item
	{
		#region Class Constructors

		public MealTicket ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			return 4;
		}

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Reset count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 4 );
		}

		public override bool OnConsumableBuyPrice ( )
		{
			// Check count
			if ( GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ) > 0 )
			{
				// Decrement count
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );

				// Return that the consumable is free
				return true;
			}

			// Return that consumable is not free
			return base.OnConsumableBuyPrice ( );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 4 );
		}

		#endregion // Item Override Functions
	}
}