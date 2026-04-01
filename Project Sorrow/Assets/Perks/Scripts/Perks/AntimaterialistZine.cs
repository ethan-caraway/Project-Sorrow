namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Antimaterialist Zine perk.
	/// </summary>
	public class AntimaterialistZine : Perk
	{
		#region Class Constructors

		public AntimaterialistZine ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override void OnStartRun ( )
		{
			// Add the Improved Rarity upgrade
			GameManager.Run.AddUpgrade ( 15 );
		}

		public override int OnMaxItems ( )
		{
			// Reduce item slots
			return -1;
		}

		#endregion // Perk Override Functions
	}
}