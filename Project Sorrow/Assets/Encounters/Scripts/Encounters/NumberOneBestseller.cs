namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the #1 Bestseller encounter.
	/// </summary>
	public class NumberOneBestseller : Encounter
	{
		#region Encounter Override Functions

		public override ResultModel GetResults ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					// Apply Popular
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.POPULAR, 3 );

					// Apply Anxious
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.ANXIOUS, 3 );
					break;

				case 1:
					// Apply Stubborn
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.STUBBORN, 4 );

					// Deduct reuputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Reputation = -3
						}
					};

				case 2:
					// Apply Serious
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.SERIOUS, 2 );

					// Increase arrogance but decrease reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxConfidence = -1
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}