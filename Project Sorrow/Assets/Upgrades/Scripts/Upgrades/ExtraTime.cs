namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Extra Time upgrade.
	/// </summary>
	public class ExtraTime : Upgrade
	{
		#region Class Constructors

		public ExtraTime ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override float OnTimeAllowance ( float current )
		{
			return 60f;
		}

		#endregion // Upgrade Override Functions
	}
}