using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Monocle item.
	/// </summary>
	public class Monocle : Item
	{
		#region Class Constructors

		public Monocle ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int COUNT_TOTAL = 5;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			return COUNT_TOTAL;
		}

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, PerformanceModel model )
		{
			// Check for modifier
			if ( modifier != Enums.WordModifierType.NONE )
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
							Type = Enums.StatusEffectType.GREEDY,
							Count = 2
						}
					};
				}
				else
				{
					// Store decremented count
					GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );
				}
			}

			// Return that the item was not triggered
			return base.OnWordComplete ( total, length, modifier, model );
		}

		public override void OnAdd ( Shop.ShopModel model )
		{
			// Set count
			GameManager.Run.SetItemIntScaleValue ( ID, InstanceID, COUNT_TOTAL );
		}

		#endregion // Item Override Functions
	}
}