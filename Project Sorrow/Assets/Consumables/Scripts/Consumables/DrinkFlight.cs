using UnityEngine;

namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Drink Flight consumable.
	/// </summary>
	public class DrinkFlight : Consumable
	{
		#region Consumable Override Functions

		public override StatusEffects.StatusEffectModel OnStatusEffects ( int instances )
		{
			// Store list of possible modifiers
			Enums.StatusEffectType [ ] statusEffectOptions =
			{
				Enums.StatusEffectType.STUBBORN,
				Enums.StatusEffectType.GREEDY,
				Enums.StatusEffectType.DRAMATIC,
				Enums.StatusEffectType.POPULAR,
				Enums.StatusEffectType.EXCITED,
				Enums.StatusEffectType.SERIOUS
			};

			// Return status effect
			return new StatusEffects.StatusEffectModel
			{
				Type = statusEffectOptions [ Random.Range ( 0, statusEffectOptions.Length ) ],
				Count = 30 * instances
			};
		}

		#endregion // Consumable Override Functions
	}
}