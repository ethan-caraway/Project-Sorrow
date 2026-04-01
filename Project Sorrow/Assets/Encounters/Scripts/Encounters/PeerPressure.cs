namespace FlightPaper.ProjectSorrow.Encounters
{
	/// <summary>
	/// This class controls the functionality of the Peer Pressure encounter.
	/// </summary>
	public class PeerPressure : Encounter
	{
		#region Encounter Override Functions

		public override bool IsOptionAvailable ( int index, OptionModel option )
		{
			// Check option
			switch ( index )
			{
				case 0:
					return ( GameManager.Run.HasConsumable ( option.Consumable.ID ) || GameManager.Run.CanAddConsumable ( ) ) && GameManager.Run.CanPlayerAffordPrice ( 12 );

				case 1:
					return ( GameManager.Run.HasConsumable ( option.Consumable.ID ) || GameManager.Run.CanAddConsumable ( ) ) && GameManager.Run.CanPlayerAffordPrice ( 30 );
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
					// Add Light Beer
					GameManager.Run.AddConsumable ( option.Consumable.ID, 1 );

					// Deduct money
					return new ResultModel
					{
						Money = -12
					};

				case 1:
					// Add 2 Single Cask Whiskey
					GameManager.Run.AddConsumable ( option.Consumable.ID, 2 );

					// Deduct money
					return new ResultModel
					{
						Money = -30
					};

				case 2:
					// Increase arrogance but decrease reputation
					return new ResultModel
					{
						Bonus = new EncounterBonusModel
						{
							MaxArrogance = 3,
							Reputation = -10
						}
					};
			}

			// Return no results by default
			return base.GetResults ( index, option );
		}

		#endregion // Encounter Override Functions
	}
}