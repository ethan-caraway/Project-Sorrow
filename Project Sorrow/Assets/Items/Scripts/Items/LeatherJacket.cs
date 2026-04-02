namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Leather Jacket item.
	/// </summary>
	public class LeatherJacket : Item
	{
		#region Class Constructors

		public LeatherJacket ( int itemID, string instanceID ) : base ( itemID, instanceID )
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
					SplashText = $"+{5 * model.ConfidenceRemaining}"
				},
				Snaps = 5 * model.ConfidenceRemaining
			};
		}

		#endregion // Item Override Functions
	}
}