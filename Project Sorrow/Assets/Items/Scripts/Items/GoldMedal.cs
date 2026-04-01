namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Gold Medal item.
	/// </summary>
	public class GoldMedal : Item
	{
		#region Class Constructors

		public GoldMedal ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check time remaining
			if ( model.TimeRemaining > 60f )
			{
				// Apply applause
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = total
				};
			}

			// Return that no applause was earned
			return base.OnApplause ( model, total );
		}

		#endregion // Item Override Functions
	}
}