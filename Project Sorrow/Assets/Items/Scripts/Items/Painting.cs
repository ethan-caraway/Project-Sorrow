namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Painting item.
	/// </summary>
	public class Painting : Item
	{
		#region Class Constructors

		public Painting ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "1" );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increase scale value
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 2 );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 1 );
		}

		public override int OnRemove ( Shop.ShopModel model )
		{
			// Return current scale value
			return GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );
		}

		#endregion // Item Override Functions
	}
}