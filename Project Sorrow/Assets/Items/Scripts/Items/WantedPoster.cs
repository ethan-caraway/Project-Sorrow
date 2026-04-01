namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Wanted Poster item.
	/// </summary>
	public class WantedPoster : Item
	{
		#region Class Constructors

		public WantedPoster ( int itemID, string instanceID ) : base ( itemID, instanceID )
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

		public override int OnReputation ( int current )
		{
			// Return current scale value
			return GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increase scael value
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 2 );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 2 );
		}

		#endregion // Item Override Functions
	}
}