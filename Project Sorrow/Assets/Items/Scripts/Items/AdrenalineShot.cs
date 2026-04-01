namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Adrenaline Shot item.
	/// </summary>
	public class AdrenalineShot : Item
	{
		#region Class Constructors

		public AdrenalineShot ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override int OnScoreCharacter ( string text, int total, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check remaining confidence
			if ( model.ConfidenceRemaining <= 3 )
			{
				// Return additional snaps
				return 5;
			}

			// Return no additional snaps
			return base.OnScoreCharacter ( text, total, modifier, model );
		}

		#endregion // Item Override Functions
	}
}