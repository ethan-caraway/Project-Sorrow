namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Gold Bar item.
	/// </summary>
	public class GoldBar : Item
	{
		#region Class Constructors

		public GoldBar ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int MAX_COUNT = 2;
		private const int START_COMMISSION = 2;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override string GetVariableDescription ( string description )
		{
			// Get commission
			int commission = (int)GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Add commission value to description
			return description.Replace ( "{0}", commission.ToString ( ) );
		}

		public override string GetWouldBeVariableDescription ( string description )
		{
			return description.Replace ( "{0}", START_COMMISSION.ToString ( ) );
		}

		public override int GetWouldBeIntScaleValue ( )
		{
			return MAX_COUNT;
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Add commission
			stats.Commission += (int)GameManager.Run.GetItemFloatScaleValue ( ID, InstanceID );

			// Decrement count
			int count = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID ) - 1;

			// Check for enough performances
			if ( count < 1 )
			{
				// Reset count
				count = MAX_COUNT;

				// Increment commission
				GameManager.Run.AddItemFloatScaleValue ( ID, InstanceID, 2 );
			}

			// Store count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, count );
		}

		public override bool OnCommission ( int commission )
		{
			// Trigger item
			return true;
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set starting commission
			GameManager.Run.SetItemFloatScaleValue ( ID, InstanceID, START_COMMISSION );

			// Set starting count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, MAX_COUNT );
		}

		#endregion // Item Override Functions
	}
}