using System.Collections.Generic;

namespace FlightPaper.ProjectSorrow.Upgrades
{
	/// <summary>
	/// This class controls the functionality of the Free Consuambles upgrade.
	/// </summary>
	public class FreeConsumables : Upgrade
	{
		#region Class Constructors

		public FreeConsumables ( int upgradeID ) : base ( upgradeID )
		{

		}

		#endregion // Class Constructors

		#region Upgrade Override Functions

		public override int OnAdd ( )
		{
			// Get consumable stats
			int consumableCount = GameManager.Run.ConsumableSlotCount;
			int maxConsumables = GameManager.Run.MaxConsumables;

			// Generate items if there is room
			for ( int i = consumableCount; i < maxConsumables; i++ )
			{
				// Generate consumable
				Consumables.ConsumableScriptableObject consumable = GameManager.Run.RarityData.GenerateConsumable ( -1f );
				if ( consumable != null )
				{
					// Add consumable
					GameManager.Run.AddConsumable ( consumable.ID, 1 );
				}
			}

			return base.OnAdd ( );
		}

		#endregion // Upgrade Override Functions
	}
}