namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Megaphone item.
	/// </summary>
	public class Megaphone : Item
	{
		#region Class Constructors

		public Megaphone ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for capital letter
			if ( char.IsUpper ( text [ 0 ] ) )
			{
				// Return additional snaps
				return 4;
			}

			// Return that no additional snaps were earned
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions
	}
}