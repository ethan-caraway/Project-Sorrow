namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Improved Bar upgrade.
	/// </summary>
	public class ImprovedBar : Upgrade
	{
		#region Class Constructors

		public ImprovedBar ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override bool OnDuplicateItems ( )
		{
			// Return that duplicate items can appear
			return true;
		}

		#endregion // Upgrade Override Functions
	}
}