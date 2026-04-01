namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Shots! encounter.
	/// </summary>
	public class Shots : Encounter
	{
		#region Encounter Override Functions

		public override ResultModel GetResults ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					// Appy Impaired
					GameManager.Run.AddStatusEffect ( Enums.StatusEffectType.IMPAIRED, 3 );
					break;

				case 1:
					// Decrease Reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							Reputation = -5
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}