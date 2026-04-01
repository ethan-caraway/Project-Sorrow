namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Bar Expansion upgrade.
	/// </summary>
	public class BarExpansion : Upgrade
	{
		#region Class Constructors

		public BarExpansion ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitShop ( Shop.ShopModel model )
		{
			// Add items and consumables in the shop
			model.ItemCount += 1;
			model.ConsumableCount += 1;
		}

		#endregion // Upgrade Override Functions
	}
}