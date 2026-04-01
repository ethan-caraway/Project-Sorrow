namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Salad Tongs item.
	/// </summary>
	public class SaladTongs : Item
	{
		#region Class Constructors

		public SaladTongs ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for Italics
			if ( modifier == Enums.WordModifierType.STRIKETHROUGH )
			{
				// Return additional snaps
				return 10;
			}

			// Return no additional snaps
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions
	}
}