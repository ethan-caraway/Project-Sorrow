namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Self-Promotion encounter.
	/// </summary>
	public class SelfPromotion : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 1:
					return GameManager.Run.CanPlayerAffordPrice ( 20 );

				case 2:
					return GameManager.Run.HasItem ( option.Item.ID );
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
					// Earn money and decrease reputation
					return new ResultModel
					{
						Money = 30,
						Bonus = new EncounterBonusModel
						{
							Reputation = -15
						}
					};

				case 1:
					// Earn reputation and lose money
					return new ResultModel
					{
						Money = -20,
						Bonus = new EncounterBonusModel
						{
							Reputation = 10
						}
					};

				case 2:
					// Remove Giant Check
					GameManager.Run.RemoveItemFirstInstance ( option.Item.ID );

					// Increase reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Reputation = 20
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}