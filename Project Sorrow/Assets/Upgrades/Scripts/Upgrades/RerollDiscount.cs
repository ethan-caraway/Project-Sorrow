using FlightPaper.ProjectSorrow.Performance;
using FlightPaper.ProjectSorrow.Shop;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Reroll Discount upgrade.
	/// </summary>
	public class RerollDiscount : Upgrade
	{
		#region Class Constructors

		public RerollDiscount ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override void OnInitShop ( ShopModel model )
		{
			// Decrease initial reroll cost
			model.RerollStartPrice -= 2;
		}

		#endregion // Upgrade Override Functions
	}
}