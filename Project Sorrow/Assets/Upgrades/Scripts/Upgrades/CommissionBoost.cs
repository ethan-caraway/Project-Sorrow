namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Commission Boost upgrade.
	/// </summary>
	public class CommissionBoost : Upgrade
	{
		#region Class Constructors

		public CommissionBoost ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			model.Commission += 5;
		}

		#endregion // Upgrade Override Functions
	}
}