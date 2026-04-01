namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Juicy Giveaway encounter.
	/// </summary>
	public class JuicyGiveaway : Encounter
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
					// Earn 3 Grape Juice
					GameManager.Run.AddConsumable ( option.Consumable.ID, 3 );
					break;

				case 1:
					// Earn 2 Apple Juice
					GameManager.Run.AddConsumable ( option.Consumable.ID, 2 );
					break;

				case 2:
					// Earn Orange Juice
					GameManager.Run.AddConsumable ( option.Consumable.ID, 1 );
					break;

				case 3:
					// Increase reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Reputation = 5
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}