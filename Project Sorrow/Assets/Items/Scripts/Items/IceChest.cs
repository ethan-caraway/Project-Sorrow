using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Ice Chest item.
	/// </summary>
	public class IceChest : Item
	{
		#region Class Constructors

		public IceChest ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data

		private bool isPerfect = true;

		#endregion // Item Data

		#region Item Override Functions

		public override bool OnFlub ( )
		{
			// Mark flub
			isPerfect = false;

			// Return that the item was triggered
			return true;
		}

		public override bool IsFlubEffectPositive ( )
		{
			// Return that the effect is negative
			return false;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Check for flubs and 10% chance
			if ( isPerfect && Random.Range ( 0f, 1f ) < 0.1f )
			{
				// Generate a random common consumable
				Consumables.ConsumableScriptableObject consumable = GameManager.Run.RarityData.GenerateConsumable ( -1f );

				// Check if an instance of the consumable is owned or if there is an available slot for a new consumable
				if ( GameManager.Run.HasConsumable ( consumable.ID ) || GameManager.Run.CanAddConsumable ( ) )
				{
					// Add consumable
					GameManager.Run.AddConsumable ( consumable.ID, 1 );
				}

				// Trigger item
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID
				};
			}

			// Reset line tracking
			isPerfect = true;

			// Return no additional snaps
			return base.OnLineComplete ( total );
			
		}

		#endregion // Item Override Functions
	}
}