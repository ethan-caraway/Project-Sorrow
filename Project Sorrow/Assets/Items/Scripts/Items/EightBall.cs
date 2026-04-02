namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the 8 Ball item.
	/// </summary>
	public class EightBall : Item
	{
		#region Class Constructors

		public EightBall ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for word exactly 8 characters long
			if ( length == 8 )
			{
				// Return snaps earned for a word exactly 8 characters long
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = "+80"
					},
					Snaps = 80
				};
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		#endregion // Item Override Functions
	}
}