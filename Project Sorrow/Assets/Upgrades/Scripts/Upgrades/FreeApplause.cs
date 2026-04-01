using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Free Applause upgrade.
	/// </summary>
	public class FreeApplause : Upgrade
	{
		#region Class Constructors

		public FreeApplause ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override ApplauseModel OnApplause ( PerformanceModel model, int total )
		{
			return new ApplauseModel
			{
				Applause = 250
			};
		}

		#endregion // Upgrade Override Functions
	}
}