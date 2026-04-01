namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Stopwatch item.
	/// </summary>
	public class Stopwatch : Item
	{
		#region Class Constructors

		public Stopwatch ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{
			
		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check time remaing
			if ( model.TimeRemaining < 10f )
			{
				// Apply applause
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = 200
				};
			}

			// Return that no applause was earned
			return base.OnApplause ( model, total );
		}

		#endregion // Item Override Functions
	}
}