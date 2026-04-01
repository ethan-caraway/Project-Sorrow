namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Improved Time upgrade.
	/// </summary>
	public class ImprovedTime : Upgrade
	{
		#region Class Constructors

		public ImprovedTime ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitPerformance ( Performance.PerformanceModel model )
		{
			model.MoneyPerTime += 1;
		}

		#endregion // Upgrade Override Functions
	}
}