namespace FlightPaper.ProjectSorrow.Consumables
{
	/// <summary>
	/// This class controls the functionality of the Black Tea consumable.
	/// </summary>
	public class BlackTea : Consumable
	{
		#region Consumable Override Functions

		public override Enums.WordModifierType [ ] OnModifyWords ( int instances )
		{
			// Create list of modifiers
			Enums.WordModifierType [ ] modifiers = new Enums.WordModifierType [ 3 * instances ];
			for ( int i = 0; i < modifiers.Length; i++ )
			{
				modifiers [ i ] = Enums.WordModifierType.ITALICS;
			}

			// Return modifiers
			return modifiers;
		}

		#endregion // Consumable Override Functions
	}
}