namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Returning the Favor encounter.
	/// </summary>
	public class ReturningTheFavor : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					return GameManager.Run.HasConsumable ( option.Consumable.ID ) || GameManager.Run.CanAddConsumable ( );

				case 1:
					return GameManager.Run.HasConsumable ( option.Consumable.ID ) || GameManager.Run.CanAddConsumable ( );

				case 2:
					return GameManager.Run.HasConsumable ( option.Consumable.ID ) || GameManager.Run.CanAddConsumable ( );
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
					// Earn Salt & Vinegar Chips
					GameManager.Run.AddConsumable ( option.Consumable.ID, 1 );
					break;

				case 1:
					// Earn Smash Burger
					GameManager.Run.AddConsumable ( option.Consumable.ID, 1 );
					break;

				case 2:
					// Earn Fish Tacos
					GameManager.Run.AddConsumable ( option.Consumable.ID, 1 );
					break;

				case 3:
					// Earn money and decrease reputation
					return new ResultModel
					{
						Money = 5,
						Bonus = new EncounterBonusModel
						{
							Reputation = -2
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}