namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Reading Glasses item.
	/// </summary>
	public class ReadingGlasses : Item
	{
		#region Class Constructors

		public ReadingGlasses ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private int count = 0;

		#endregion // Item Data

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Reset count
			count = 0;
		}

		public override ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			// Increment snaps
			count++;

			// Return no additional snaps
			return base.OnStanzaComplete ( model );
		}

		public override Performance.ApplauseModel OnApplause ( Performance.PerformanceModel model, int total )
		{
			// Check stanza count
			if ( count >= 4 )
			{
				// Apply applause
				return new Performance.ApplauseModel
				{
					ItemID = ID,
					ItemInstanceID = InstanceID,
					Applause = total / 2
				};
			}

			// Return that no applause was earned
			return base.OnApplause ( model, total );
		}

		#endregion // Item Override Functions
	}
}