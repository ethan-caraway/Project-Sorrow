namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Tenure perk.
	/// </summary>
	public class Tenure : Perk
	{
		#region Class Constructors

		public Tenure ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			// Start with 50 snaps
			model.SnapsCount += 50;
		}

		#endregion // Perk Override Functions
	}
}