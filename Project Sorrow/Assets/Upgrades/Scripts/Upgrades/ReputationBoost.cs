namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Reputation Boost upgrade.
	/// </summary>
	public class ReputationBoost : Upgrade
	{
		#region Class Constructors

		public ReputationBoost ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnReputation ( int current )
		{
			return 15;
		}

		#endregion // Upgrade Override Functions
	}
}