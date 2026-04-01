namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Grill item.
	/// </summary>
	public class Grill : Item
	{
		#region Class Constructors

		public Grill ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Add the current scale value to the description
			return description.Replace ( "{0}", GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID ).ToString ( "0.#" ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			// Add the starting scale value to the description
			return description.Replace ( "{0}", "1" );
		}

		public override bool OnBoldTrigger ( )
		{
			// Increment scale value
			GameManager.Run.AddItemFloatScaleValue ( ID, InstanceID, 0.2f );

			// Return that the item was triggered
			return true;
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Get scale value
			float scale = GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Return applause
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = (int)( total * ( scale - 1f ) )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, 1f );
		}

		#endregion // Item Override Functions
	}
}