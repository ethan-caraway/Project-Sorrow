namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Thesaurus item.
	/// </summary>
	public class Thesaurus : Item
	{
		#region Class Constructors

		public Thesaurus ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Return snaps earned for a word shorter than 5 letters
			return new ItemTriggerModel
			{
				ID = ID,
				InstanceID = InstanceID,
				Highlight = new HUD.ItemHighlightModel
				{
					IsPositive = true,
					SplashColor = length < 5 ? Enums.SplashColorType.SNAPS_GOLD : Enums.SplashColorType.NONE,
					SplashText = "<b>x2</b>"
				},
				Snaps = length < 5 ? total : 0
			};
		}

		#endregion // Item Override Functions
	}
}