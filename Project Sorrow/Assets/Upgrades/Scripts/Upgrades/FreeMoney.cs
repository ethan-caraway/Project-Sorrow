namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Free Money upgrade.
	/// </summary>
	public class FreeMoney : Upgrade
	{
		#region Class Constructors

		public FreeMoney ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnAdd ( )
		{
			return 30;
		}

		#endregion // Upgrade Override Functions
	}
}