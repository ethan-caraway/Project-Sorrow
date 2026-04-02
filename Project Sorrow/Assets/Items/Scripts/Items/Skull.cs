using UnityEngine;

namespace FlightPaper.ProjectSorrow.Items
{
	/// <summary>
	/// This class controls the functionality of the Skull item.
	/// </summary>
	public class Skull : Item
	{
		#region Class Constructors

		public Skull ( int itemID, string instanceID ) : base ( itemID, instanceID )
		{

		}

		#endregion // Class Constructors

		#region Item Override Functions

		public override ItemTriggerModel OnWordComplete ( int total, int length, Enums.WordModifierType modifier, Performance.PerformanceModel model )
		{
			// Check for 10% chance
			if ( Random.Range ( 0f, 1f ) < 0.1f )
			{
				// Apply Dramatic
				return new ItemTriggerModel
				{
					ID = ID,
					InstanceID = InstanceID,
					Highlight = new HUD.ItemHighlightModel
					{
						IsPositive = true,
						SplashColor = Enums.SplashColorType.TIME_YELLOW,
						SplashText = "Dramatic"
					},
					StatusEffect = new StatusEffects.StatusEffectModel
					{
						Type = Enums.StatusEffectType.DRAMATIC,
						Count = 1
					}
				};
			}

			// Return that the item was not triggered
			return base.OnWordComplete ( total, length, modifier, model );
		}

		#endregion // Item Override Functions
	}
}