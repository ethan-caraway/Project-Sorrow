namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Interest Boost upgrade.
	/// </summary>
	public class InterestBoost : Upgrade
	{
		#region Class Constructors

		public InterestBoost ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			model.InterestCap += 3;
		}

		#endregion // Upgrade Override Functions
	}
}