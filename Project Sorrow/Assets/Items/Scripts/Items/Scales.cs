namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Scales item.
	/// </summary>
	public class Scales : Item
	{
		#region Class Constructors

		public Scales ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Return the offset to ensure the minimum number of snaps is earned
			return total > 0 && total < 4 ? 4 - total : 0;
		}

		#endregion // Item Override Functions
	}
}