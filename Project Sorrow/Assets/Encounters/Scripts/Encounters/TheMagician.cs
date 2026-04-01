namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of The Magician encounter.
	/// </summary>
	public class TheMagician : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					return GameManager.Run.CanAddItem ( );

				case 1:
					return GameManager.Run.CanAddItem ( );

				case 2:
					return GameManager.Run.CanAddItem ( );
			}

			// Return true by default
			return base.IsOptionAvailable ( index, option );
		}

		public override ResultModel GetResults ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					// Add 4 of Diamonds
					GameManager.Run.AddItem ( option.Item.ID );
					break;

				case 1:
					// Add Flashcard
					GameManager.Run.AddItem ( option.Item.ID );
					break;

				case 2:
					// Add Wheel of Fortune
					GameManager.Run.AddItem ( option.Item.ID );
					break;

				case 3:
					// Increase arrogance but decrease reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxArrogance = 1
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}