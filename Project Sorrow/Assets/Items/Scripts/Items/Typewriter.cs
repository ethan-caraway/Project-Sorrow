namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Typewriter item.
	/// </summary>
	public class Typewriter : Item
	{
		#region Class Constructors

		public Typewriter ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnStanzaComplete ( Performance.PerformanceModel model )
		{
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = Enums.SplashColorType.SNAPS_GOLD,
					SplashText = "+40"
				},
				Snaps = 40
			};
		}

		#endregion // Item Override Functions
	}
}