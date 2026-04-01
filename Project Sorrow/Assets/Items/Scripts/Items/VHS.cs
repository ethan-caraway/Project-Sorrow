namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the VHS item.
	/// </summary>
	public class VHS : Item
	{
		#region Class Constructors

		public VHS ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "20" );
		}

		public override bool OnStatusEffectExpire ( int total )
		{
			// Increment scale value
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, 20 * total );

			// Trigger item
			return true;
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			return new Performance.ApplauseModel
			{
				ItemID = ID,
				ItemInstanceID = InstanceID,
				Applause = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID )
			};
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 20 );
		}

		#endregion // Item Override Functions
	}
}