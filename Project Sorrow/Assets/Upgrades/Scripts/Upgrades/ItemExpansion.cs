namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Item Expansion upgrade.
	/// </summary>
	public class ItemExpansion : Upgrade
	{
		#region Class Constructors

		public ItemExpansion ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnMaxItems ( )
		{
			return 1;
		}

		#endregion // Upgrade Override Functions
	}
}