using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Perks
{
	/// <summary>
	/// This class controls the functionality of the Art Grant perk.
	/// </summary>
	public class ArtGrant : Perk
	{
		#region Class Constructors

		public ArtGrant ( int perkID ) : base ( perkID )
		{

		}

		#endregion // Class Constructors

		#region Perk Override Functions

		public override void OnInitPerformance ( PerformanceModel model )
		{
			// Reduce interest cap to $0
			model.InterestCap = 0;
		}

		public override void OnCompletePerformance ( PerformanceModel model, SummaryStatsModel stats )
		{
			// Double commission
			stats.Commission *= 2;
		}

		#endregion // Perk Override Functions
	}
}