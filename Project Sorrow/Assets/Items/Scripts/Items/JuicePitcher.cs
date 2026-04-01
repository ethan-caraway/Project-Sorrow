namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Juice Pitcher item.
	/// </summary>
	public class JuicePitcher : Item
	{
		#region Class Constructors

		public JuicePitcher ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for small modifier
			if ( modifier == Enums.WordModifierType.SMALL )
			{
				// Return additional snaps
				return 4;
			}

			// Return no additional snaps
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions
	}
}