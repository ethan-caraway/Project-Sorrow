namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Bottle Opener item.
	/// </summary>
	public class BottleOpener : Item
	{
		#region Class Constructors

		public BottleOpener ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for Underline modifier
			if ( modifier == Enums.WordModifierType.CAPS )
			{
				// Return additional snaps
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = "<b>x1.5</b>"
					},
					Snaps = (int)( total * 0.5f )
				};
			}

			// Return no additional snaps
			return base.OnWordComplete ( total, length, modifier, model );
		}

		#endregion // Item Override Functions
	}
}