namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Sombrero item.
	/// </summary>
	public class Sombrero : Item
	{
		#region Class Constructors

		public Sombrero ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for Underline modifier
			if ( modifier == Enums.WordModifierType.UNDERLINE )
			{
				// Return snaps based on confidence remaining
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = $"+{2 * model.ConfidenceRemaining}"
					},
					Snaps = 2 * model.ConfidenceRemaining
				};
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		#endregion // Item Override Functions
	}
}