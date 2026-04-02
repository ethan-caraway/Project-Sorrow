using FlightPaper.ProjectSorrow.Performance;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Cat Ears item.
	/// </summary>
	public class CatEars : Item
	{
		#region Class Constructors

		public CatEars ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Data Constants

		private const int COUNT_TOTAL = 100;

		#endregion // Item Data Constants

		#region Item Override Functions

		public override int GetWouldBeIntScaleValue ( )
		{
			return COUNT_TOTAL;
		}

		public override ItemTriggerModel OnCharacterComplete ( Enums.WordModifierType modifier, PerformanceModel model )
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

				// Apply Popular
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.SNAPS_GOLD,
						SplashText = "Popular"
					},
					StatusEffect = new StatusEffects.StatusEffectModel
					{
						Type = Enums.StatusEffectType.POPULAR,
						Count = 1
					}
				};
			}
			else
			{
				// Store decremented count
				GameManager.Run.AddItemIntScaleValue ( ID, InstanceID, -1 );

				// Return that the item was not triggered
				return base.OnCharacterComplete ( modifier, model );
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