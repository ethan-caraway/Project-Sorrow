using FlightPaper.ProjectSorrow.Consumables;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Coupon item.
	/// </summary>
	public class Coupon : Item
	{
		#region Class Constructors

		public Coupon ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override float OnModifyPrices ( )
		{
			// Reduce prices by 50%
			return 0.5f;
		}

		public override bool OnPurchaseItem ( ItemScriptableObject item )
		{
			// Trigger item
			return true;
		}

		public override bool OnPurchaseConsumable ( ConsumableScriptableObject consumable )
		{
			// Trigger item
			return true;
		}

		#endregion // Item Override Functions
	}
}