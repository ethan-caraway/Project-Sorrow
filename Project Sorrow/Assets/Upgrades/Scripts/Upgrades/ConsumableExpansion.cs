namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Consumable Expansion upgrade.
	/// </summary>
	public class ConsumableExpansion : Upgrade
	{
		#region Class Constructors

		public ConsumableExpansion ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnMaxConsumables ( )
		{
			return 2;
		}

		#endregion // Upgrade Override Functions
	}
}