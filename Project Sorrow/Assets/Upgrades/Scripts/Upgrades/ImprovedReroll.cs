namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Improved Reroll upgrade.
	/// </summary>
	public class ImprovedReroll : Upgrade
	{
		#region Class Constructors

		public ImprovedReroll ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitShop ( Shop.ShopModel model )
		{
			// Decrease reroll cost increment
			model.RerollPriceIncrement -= 2;
		}

		#endregion // Upgrade Override Functions
	}
}