using UnityEngine;

namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Sample Platter consumable.
	/// </summary>
	public class SamplePlatter : Consumable
	{
		#region Consumable Override Functions

		public override Enums.WordModifierType [ ] OnModifyWords ( int instances )
		{
			// Store list of possible modifiers
			Enums.WordModifierType [ ] modifierOptions =
			{
				Enums.WordModifierType.BOLD,
				Enums.WordModifierType.ITALICS,
				Enums.WordModifierType.STRIKETHROUGH,
				Enums.WordModifierType.UNDERLINE,
				Enums.WordModifierType.CAPS,
				Enums.WordModifierType.SMALL
			};

			// Create list of modifiers
			Enums.WordModifierType [ ] modifiers = new Enums.WordModifierType [ 20 * instances ];
			for ( int i = 0; i < modifiers.Length; i++ )
			{
				modifiers [ i ] = modifierOptions [ Random.Range ( 0, modifierOptions.Length ) ];
			}

			// Return modifiers
			return modifiers;
		}

		#endregion // Consumable Override Functions
	}
}