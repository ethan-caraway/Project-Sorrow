namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Keyboard item.
	/// </summary>
	public class Keyboard : Item
	{
		#region Class Constructors

		public Keyboard ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = Enums.SplashColorType.SNAPS_GOLD,
					SplashText = "+10"
				},
				Snaps = 10
			};
		}

		#endregion // Item Override Functions
	}
}