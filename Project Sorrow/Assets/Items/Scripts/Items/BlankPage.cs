namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Blank Page item.
	/// </summary>
	public class BlankPage : Item
	{
		#region Class Constructors

		public BlankPage ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for white space
			if ( text == " " )
			{
				// Return additional snaps
				return 2;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions
	}
}