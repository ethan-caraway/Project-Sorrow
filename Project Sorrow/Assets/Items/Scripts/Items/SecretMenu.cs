using FlightPaper.ProjectSorrow.Consumables;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Secret Menu item.
	/// </summary>
	public class SecretMenu : Item
	{
		#region Class Constructors

		public SecretMenu ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override bool OnPurchaseConsumable ( ConsumableScriptableObject consumable )
		{
			// Add extra instance
			GameManager.Run.AddConsumable ( consumable.ID, 1 );

			// Return that this item was triggered
			return true;
		}

		#endregion // Item Override Functions
	}
}