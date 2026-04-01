namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Sports Jersey item.
	/// </summary>
	public class SportsJersey : Item
	{
		#region Class Constructors

		public SportsJersey ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "2" );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Get scale value
			int snaps = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Increment scale value
			snaps += (int)( model.TimeRemaining / 5 ) * 2;

			// Update scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, snaps );

			// Apply applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = snaps
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 2 );
		}

		#endregion // Item Override Functions
	}
}