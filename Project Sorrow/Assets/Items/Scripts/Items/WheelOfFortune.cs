namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Wheel of Fortune item.
	/// </summary>
	public class WheelOfFortune : Item
	{
		#region Class Constructors

		public WheelOfFortune ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private int lineCount = 0;

		#endregion // Item Data

		#region Item Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Reset count
			lineCount = 0;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Increment count
			lineCount++;

			// Check for 10 line
			if ( lineCount % 10 == 0 )
			{
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SERIOUS_GREY,
						SplashText = $"<color=green>{lineCount}</color>"
					}
				};
			}

			// Return no additional snaps
			return base.OnLineComplete ( total );
		}

		public override void OnCompletePerformance ( Performance.PerformanceModel model, Performance.SummaryStatsModel stats )
		{
			// Increase commission per 10 lines
			stats.Commission += 4 * ( lineCount / 10 );
		}

		public override bool OnCommission ( int commission )
		{
			// Trigger item
			return lineCount >= 10;
		}

		#endregion // Item Override Functions
	}
}