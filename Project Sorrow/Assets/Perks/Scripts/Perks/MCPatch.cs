namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the MC Patch perk.
	/// </summary>
	public class MCPatch : Perk
	{
		#region Class Constructors

		public MCPatch ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override int OnReputation ( int current )
		{
			// Check for judge
			if ( GameManager.Run.IsLastPerformanceOfRound ( ) && GameManager.Run.IsPerforming ( ) )
			{
				// Return reputation boost
				return 20;
			}

			// Return no additional reputation
			return base.OnReputation ( current );
		}

		#endregion // Perk Override Functions
	}
}