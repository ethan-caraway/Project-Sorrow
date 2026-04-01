namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Candle item.
	/// </summary>
	public class Candle : Item
	{
		#region Class Constructors

		public Candle ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
			return description.Replace ( "{0}", "300" );
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

		public override bool OnFlub ( )
		{
			// Deduct scale value for flub
			GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -20 );

			// Trigger item
			return true;
		}

		public override bool IsFlubEffectPositive ( )
		{
			// Show negative effect
			return false;
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting scale value
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, 300 );
		}

		#endregion // Item Override Functions
	}
}