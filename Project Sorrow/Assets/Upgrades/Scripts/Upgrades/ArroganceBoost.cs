namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Arrogance Boost upgrade.
	/// </summary>
	public class ArroganceBoost : Upgrade
	{
		#region Class Constructors

		public ArroganceBoost ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnMaxArrogance ( int current )
		{
			return 4;
		}

		#endregion // Upgrade Override Functions
	}
}