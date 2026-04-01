using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Soapbox item.
	/// </summary>
	public class Soapbox : Item
	{
		#region Class Constructors

		public Soapbox ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int COUNT_TOTAL = 8;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			return COUNT_TOTAL;
		}

		public override ItemTriggerModel OnLineComplete ( int total )
		{
			// Get count
			int count = GameManager.Run.GetItemIntScaleValue ( ID, InstanceID );

			// Decrement count
			count--;

			// Check for trigger
			if ( count == 0 )
			{
				// Reset count
				GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );

				// Apply Greedy
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					StatusEffect = new StatusEffects.StatusEffectModel
					{
						Type = Enums.StatusEffectType.SERIOUS,
						Count = 1
					}
				};
			}
			else
			{
				// Store decremented count
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );

				// Return that the item was not triggered
				return base.OnLineComplete ( total );
			}
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );
		}

		#endregion // Item Override Functions
	}
}