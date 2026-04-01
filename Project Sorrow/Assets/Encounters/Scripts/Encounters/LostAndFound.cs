namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Lost and Found encounter.
	/// </summary>
	public class LostAndFound : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
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
					// Apply anxious
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.ANXIOUS, 5 );

					// Earn money
					return new ResultModel
					{
						Money = 10,
					};

				case 1:
					// Add Credit Card
					GameManager.Run.AddItem ( option.Item.ID );

					// Apply anxious
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.ANXIOUS, 5 );
					break;

				case 2:
					// Add Gift Card
					GameManager.Run.AddItem ( option.Item.ID );

					// Apply anxious
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.ANXIOUS, 5 );
					break;

				case 3:
					// Increase arrogance but decrease reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxConfidence = 1
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}