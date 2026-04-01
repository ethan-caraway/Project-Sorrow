namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Social Conundrum encounter.
	/// </summary>
	public class SocialConundrum : Encounter
	{
		#region Encounter Override Functions

		public override ResultModel GetResults ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					// Decrease confidence
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxConfidence = -1
						}
					};

				case 1:
					// Decrease reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Reputation = -3
						}
					};

				case 2:
					// Decrease time
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							TimeAllowance = -20f
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}