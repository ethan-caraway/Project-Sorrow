namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Confidence Boost upgrade.
	/// </summary>
	public class ConfidenceBoost : Upgrade
	{
		#region Class Constructors

		public ConfidenceBoost ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnMaxConfidence ( int current )
		{
			return 3;
		}

		#endregion // Upgrade Override Functions
	}
}