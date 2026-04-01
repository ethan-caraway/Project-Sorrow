namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Investment Strategies encounter.
	/// </summary>
	public class InvestmentStrategies : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 2:
					return GameManager.Run.MaxConfidence > 10;
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
					// Increase commission
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Commission = 1
						}
					};

				case 1:
					// Increase interest cap
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							InterestCap = 3
						}
					};

				case 2:
					// Increase arrogance
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxArrogance = 2
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}